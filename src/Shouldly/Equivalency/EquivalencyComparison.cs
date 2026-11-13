using NUnit.Framework.Constraints;
using Shouldly.Internals;
using Shouldly.Internals.Failures;

namespace Shouldly.Equivalency;

/// <summary>
/// The equivalency engine: walks the actual/expected graphs with a per-node strategy
/// (leaf / dictionary / set / sequence / member-wise), selecting members from the type of
/// each expected value with subset semantics, and collects every difference instead of
/// stopping at the first. Two values that an equality rule covers (<see cref="EqualityRules"/>) are not walked:
/// the rule decides.
/// </summary>
/// <remarks>
/// A value is compared by the shape of the type it is, not of the type its member or collection declares, so a
/// <c>Circle</c> in a <c>List&lt;Shape&gt;</c> is compared by a circle's members. The engine learns about types
/// only from the <see cref="IEquivalencyShapeProvider"/>, which it tells both types, so that a provider with a
/// closed set of shapes (a source-generated one) can stand in for the reflection provider: it resolves the types
/// it knows, and for a type it can't resolve the assertion throws, rather than comparing less.
/// </remarks>
internal sealed class EquivalencyComparison
{
    private readonly IEquivalencyShapeProvider _shapes;
    private readonly EquivalencyOptions _options;
    private readonly List<EquivalencyDifference> _differences = [];

    // The types, in the order first met, of values that differed member by member though their type's own equality
    // calls them equal (see ConsultOwnEquality). A failure names each once.
    private readonly List<Type> _equalByOwnEquality = [];
    private readonly HashSet<VisitedPair> _visitedPairs = [];

    // Pairs currently on the recursion stack (ancestors of the node being compared). Unlike
    // _visitedPairs (a per-comparison dedup cache that only grows), entries are pushed on the way
    // down and popped on the way back up, so this set always reflects the live ancestor chain.
    private readonly HashSet<VisitedPair> _activePairs;

    private EquivalencyComparison(IEquivalencyShapeProvider shapes, EquivalencyOptions options, HashSet<VisitedPair>? activePairs = null)
    {
        _shapes = shapes;
        _options = options;
        _activePairs = activePairs ?? [];
    }

    public static void Assert(
        object? actual,
        object? expected,
        EquivalencyOptions options,
        string? customMessage,
        string? actualExpression) =>
        Assert(actual, expected, options, customMessage, actualExpression, EquivalencyShapes.Current);

    /// <summary>Compares with the shapes of <paramref name="shapes"/>, rather than of the provider in use.</summary>
    internal static void Assert(
        object? actual,
        object? expected,
        EquivalencyOptions options,
        string? customMessage,
        string? actualExpression,
        IEquivalencyShapeProvider shapes)
    {
        var comparison = new EquivalencyComparison(shapes, options);
        comparison.CompareNodes(actual, expected, default, []);

        if (comparison._differences.Count == 0)
            return;

        throw Fail.ShouldBeEquivalentTo.With(comparison._differences, comparison._equalByOwnEquality).For(actual, customMessage, actualExpression);
    }

    private void CompareNodes(object? actual, object? expected, DeclaredTypes declared, List<string> path)
    {
        actual = NullEquivalence.Normalize(actual);
        expected = NullEquivalence.Normalize(expected);

        if (actual is null || expected is null)
        {
            // The path is deliberately left un-annotated with a type: with one side null there
            // is no single compared type to report.
            if (!(actual is null && expected is null))
                AddDifference(EquivalencyDifferenceKind.ValueMismatch, path, expected, actual);

            return;
        }

        // An equality rule that covers both values decides, as it does in ShouldBe: whoever registered it has said
        // what makes two of them the same, so they are compared whole, as a leaf is, and never by their members
        // or items. This comes before the shape, so a type with a rule needs none. Two JSON values are judged the
        // same way, by the rule Shouldly has for them without being told.
        if (EqualityRules.TryEquals(expected, actual, out var equalByRule))
        {
            if (!equalByRule)
            {
                AddDifference(
                    EquivalencyDifferenceKind.ValueMismatch,
                    AnnotateWithType(path, TypeToName(expected.GetType(), declared.Expected)),
                    expected,
                    actual,
                    EqualityRules.RuleTypeFor(expected, actual) is { } ruleType ? Inequality.ByRule(ruleType) : null);
            }

            return;
        }

        // The expected value's own type decides how the node is compared and which members are selected, so a
        // member or element declared as a base type or an interface is compared by all that the value has.
        var shape = ShapeOf(expected, declared.Expected, path);
        var annotatedPath = AnnotateWithType(path, TypeToName(shape.Type, declared.Expected));

        // Cycle / identity guard for composite reference nodes. Leaves compare by value and cannot
        // form cycles; value types are copied and cannot participate in reference-identity cycles.
        // Tracking every composite kind here means a cycle that closes through a
        // collection, dictionary or set terminates instead of recursing until StackOverflow.
        var tracked = shape.Kind != EquivalencyNodeKind.Leaf && actual is not ValueType && expected is not ValueType;
        VisitedPair pair = default;
        if (tracked)
        {
            if (ReferenceEquals(actual, expected))
                return;

            pair = new(actual, expected);

            // An in-progress ancestor pair reappearing is a cycle: treat it as equivalent (already
            // being compared). A pair already recorded in _visitedPairs was fully compared earlier in
            // this walk (a shared subgraph), so skip re-reporting the same differences.
            if (_activePairs.Contains(pair) || !_visitedPairs.Add(pair))
                return;

            _activePairs.Add(pair);
        }

        try
        {
            switch (shape.Kind)
            {
                case EquivalencyNodeKind.Leaf:
                    if (!LeafEquals(actual, expected))
                        AddDifference(EquivalencyDifferenceKind.ValueMismatch, annotatedPath, expected, actual);
                    break;

                case EquivalencyNodeKind.Dictionary:
                case EquivalencyNodeKind.Set:
                case EquivalencyNodeKind.Sequence:
                    CompareCollections(actual, expected, shape, declared.Actual, annotatedPath);
                    CompareMembers(actual, expected, shape, declared.Actual, annotatedPath);
                    break;

                default:
                    var differencesBefore = _differences.Count;
                    var differingMembers = new List<string>();
                    if (!CompareMembers(actual, expected, shape, declared.Actual, annotatedPath, differingMembers))
                        AddDifference(EquivalencyDifferenceKind.VacuousComparison, annotatedPath, expected, actual, GetDisplayName(shape.Type));
                    else if (differingMembers.Count > 0)
                        ConsultOwnEquality(actual, expected, shape, annotatedPath, differencesBefore, differingMembers);
                    break;
            }
        }
        finally
        {
            if (tracked)
                _activePairs.Remove(pair);
        }
    }

    /// <summary>
    /// Compares the members of <paramref name="shape"/>: every member of a complex type, or the members a
    /// collection type declares beyond its items. Returns whether any member was compared, rather than ignored, and
    /// adds the name of each member that differed to <paramref name="differingMembers"/> when given.
    /// </summary>
    private bool CompareMembers(object actual, object expected, EquivalencyTypeShape shape, Type? actualDeclaredType, List<string> path, List<string>? differingMembers = null)
    {
        if (shape.Members.Count == 0)
            return false;

        var actualType = actual.GetType();
        var actualShape = actualType == shape.Type ? shape : ShapeOf(actual, actualDeclaredType, path);

        var comparedAny = false;
        foreach (var member in shape.Members)
        {
            if (_options.MembersToIgnore.Contains(member.Name))
                continue;

            comparedAny = true;

            var differencesBefore = _differences.Count;
            var memberPath = Append(path, member.Name);
            var actualMember = actualShape.FindMember(member.Name);
            if (actualMember is null)
                AddDifference(EquivalencyDifferenceKind.MissingMember, memberPath, member.Name, actual, GetDisplayName(actualType));
            else
                CompareNodes(actualMember.GetValue(actual), member.GetValue(expected), new(actualMember.DeclaredType, member.DeclaredType), memberPath);

            if (_differences.Count > differencesBefore)
                differingMembers?.Add(member.Name);
        }

        return comparedAny;
    }

    /// <summary>
    /// Compares the items of two collections: two dictionaries by key, and any other two in order, unless order
    /// isn't to be compared (see <see cref="OrderIsIgnored"/>).
    /// </summary>
    private void CompareCollections(object actual, object expected, EquivalencyTypeShape shape, Type? actualDeclaredType, List<string> path)
    {
        // A provider with a closed set of shapes may have none for the actual collection. It is then taken for a
        // plain sequence, which is the stricter reading: its order is compared.
        var actualShape = actual is IEnumerable ? _shapes.GetShape(actual.GetType(), actualDeclaredType) : null;

        // A string can be enumerated, and so can other values that are compared whole (a JsonArray), but none of
        // them is a collection: an empty string is not an empty dictionary.
        if (actual is string || actualShape?.Kind == EquivalencyNodeKind.Leaf)
        {
            AddDifference(EquivalencyDifferenceKind.ValueMismatch, path, expected, actual);
            return;
        }

        if (shape.Kind == EquivalencyNodeKind.Dictionary && actualShape?.Kind == EquivalencyNodeKind.Dictionary)
        {
            CompareDictionaries(actual, expected, shape, actualShape, path);
            return;
        }

        var elementTypes = new DeclaredTypes(actualShape?.ElementType, shape.ElementType);
        if (expected is Array { Rank: > 1 } || actual is Array { Rank: > 1 })
            CompareMultidimensionalArrays(actual, expected, elementTypes, path);
        else if (_options.IgnoreOrder || OrderIsIgnored(shape, actualShape))
            CompareUnordered(actual, expected, elementTypes, path);
        else
            CompareSequences(actual, expected, elementTypes, path);
    }

    /// <summary>
    /// Whether two collections are compared without regard to order, as <c>ShouldBe</c> decides it (see
    /// <see cref="UnorderedCollection"/>): both are dictionaries or sets, or one of them is and keeps no order of
    /// its own. A <c>HashSet&lt;T&gt;</c> has no order to compare with a list's. A <c>SortedSet&lt;T&gt;</c> does.
    /// </summary>
    private static bool OrderIsIgnored(EquivalencyTypeShape expected, EquivalencyTypeShape? actual)
    {
        var expectedIsUnordered = IsDictionaryOrSet(expected);
        var actualIsUnordered = actual is not null && IsDictionaryOrSet(actual);
        if (expectedIsUnordered && actualIsUnordered)
            return true;

        return expectedIsUnordered ? !expected.KeepsAnOrder : actualIsUnordered && !actual!.KeepsAnOrder;

        static bool IsDictionaryOrSet(EquivalencyTypeShape shape) =>
            shape.Kind is EquivalencyNodeKind.Dictionary or EquivalencyNodeKind.Set;
    }

    private void CompareSequences(object actual, object expected, DeclaredTypes elementType, List<string> path)
    {
        if (actual is not IEnumerable actualEnumerable)
        {
            AddDifference(EquivalencyDifferenceKind.ValueMismatch, path, expected, actual);
            return;
        }

        var expectedList = ((IEnumerable)expected).Cast<object?>().ToList();
        var actualList = actualEnumerable.Cast<object?>().ToList();

        if (actualList.Count != expectedList.Count)
        {
            AddDifference(EquivalencyDifferenceKind.ValueMismatch, Append(path, "Count"), expectedList.Count, actualList.Count);
            return;
        }

        for (var i = 0; i < actualList.Count; i++)
            CompareNodes(actualList[i], expectedList[i], elementType, Append(path, $"Element [{i}]"));
    }

    private void CompareMultidimensionalArrays(object actual, object expected, DeclaredTypes elementType, List<string> path)
    {
        if (expected is not Array expectedArray || actual is not Array actualArray || actualArray.Rank != expectedArray.Rank)
        {
            AddDifference(EquivalencyDifferenceKind.ValueMismatch, path, expected, actual);
            return;
        }

        for (var dimension = 0; dimension < expectedArray.Rank; dimension++)
        {
            if (actualArray.GetLength(dimension) != expectedArray.GetLength(dimension))
            {
                AddDifference(
                    EquivalencyDifferenceKind.ValueMismatch,
                    Append(path, $"GetLength({dimension})"),
                    expectedArray.GetLength(dimension),
                    actualArray.GetLength(dimension));
                return;
            }
        }

        var indices = new int[expectedArray.Rank];
        var expectedFlat = expectedArray.Cast<object?>().ToList();
        var actualFlat = actualArray.Cast<object?>().ToList();
        for (var i = 0; i < expectedFlat.Count; i++)
        {
            CompareNodes(actualFlat[i], expectedFlat[i], elementType, Append(path, $"Element [{string.Join(",", indices)}]"));

            for (var dimension = expectedArray.Rank - 1; dimension >= 0; dimension--)
            {
                if (++indices[dimension] < expectedArray.GetLength(dimension))
                    break;

                indices[dimension] = 0;
            }
        }
    }

    private void CompareUnordered(object actual, object expected, DeclaredTypes elementType, List<string> path)
    {
        if (actual is not IEnumerable actualEnumerable)
        {
            AddDifference(EquivalencyDifferenceKind.ValueMismatch, path, expected, actual);
            return;
        }

        var expectedList = ((IEnumerable)expected).Cast<object?>().ToList();
        var actualList = actualEnumerable.Cast<object?>().ToList();

        if (actualList.Count != expectedList.Count)
        {
            AddDifference(EquivalencyDifferenceKind.ValueMismatch, Append(path, "Count"), expectedList.Count, actualList.Count);
            return;
        }

        // Pair expected elements to distinct equivalent actual elements with maximum bipartite
        // matching, so a complete pairing is found whenever one exists. A greedy first-match can
        // otherwise orphan an expected element whose only equivalent partner was already claimed.
        var matchOfExpected = MatchByEquivalence(
            expectedList.Count,
            actualList.Count,
            (e, a) => IsEquivalent(actualList[a], expectedList[e], elementType));

        for (var i = 0; i < expectedList.Count; i++)
        {
            if (matchOfExpected[i] < 0)
                AddDifference(EquivalencyDifferenceKind.MissingElement, path, expectedList[i], actual);
        }
    }

    private void CompareDictionaries(object actual, object expected, EquivalencyTypeShape shape, EquivalencyTypeShape actualShape, List<string> path)
    {
        var expectedEntries = shape.GetEntries(expected).ToList();
        var actualEntries = actualShape.GetEntries(actual).ToList();

        // Match keys with maximum bipartite matching (see CompareUnordered) so that, when a key is
        // equivalent to more than one candidate (complex keys, or floating-point keys within
        // DefaultFloatingPointTolerance of each other), a greedy pairing cannot wrongly leave a
        // matchable key unmatched. Entries whose values also match are paired first, so an ambiguous
        // key isn't paired with the wrong entry and its value reported as a mismatch; the leftovers are
        // then paired by key alone, so a genuine value mismatch is still reported against its key.
        var keyTypes = new DeclaredTypes(actualShape.KeyType, shape.KeyType);
        var valueTypes = new DeclaredTypes(actualShape.ValueType, shape.ValueType);
        bool KeysMatch(int e, int a) => IsEquivalent(actualEntries[a].Key, expectedEntries[e].Key, keyTypes);

        var matchOfExpected = MatchByEquivalence(
            expectedEntries.Count,
            actualEntries.Count,
            (e, a) => KeysMatch(e, a) && IsEquivalent(actualEntries[a].Value, expectedEntries[e].Value, valueTypes));

        var matchedActual = new bool[actualEntries.Count];
        foreach (var matchIndex in matchOfExpected)
        {
            if (matchIndex >= 0)
                matchedActual[matchIndex] = true;
        }

        var unmatchedExpected = Enumerable.Range(0, expectedEntries.Count).Where(e => matchOfExpected[e] < 0).ToList();
        if (unmatchedExpected.Count > 0)
        {
            var unmatchedActual = Enumerable.Range(0, actualEntries.Count).Where(a => !matchedActual[a]).ToList();
            var keyOnlyMatch = MatchByEquivalence(
                unmatchedExpected.Count,
                unmatchedActual.Count,
                (e, a) => KeysMatch(unmatchedExpected[e], unmatchedActual[a]));

            for (var i = 0; i < unmatchedExpected.Count; i++)
            {
                if (keyOnlyMatch[i] >= 0)
                    matchOfExpected[unmatchedExpected[i]] = unmatchedActual[keyOnlyMatch[i]];
            }
        }

        for (var i = 0; i < expectedEntries.Count; i++)
        {
            var matchIndex = matchOfExpected[i];
            if (matchIndex < 0)
            {
                AddDifference(EquivalencyDifferenceKind.MissingKey, path, expectedEntries[i].Key, actual);
                continue;
            }

            matchedActual[matchIndex] = true;
            CompareNodes(actualEntries[matchIndex].Value, expectedEntries[i].Value, valueTypes, Append(path, $"[{expectedEntries[i].Key.ToStringAwesomely()}]"));
        }

        var unexpectedKeys = new List<object?>();
        for (var j = 0; j < actualEntries.Count; j++)
        {
            if (!matchedActual[j])
                unexpectedKeys.Add(actualEntries[j].Key);
        }

        if (unexpectedKeys.Count > 0)
            AddDifference(EquivalencyDifferenceKind.UnexpectedKeys, path, expected, unexpectedKeys);
    }

    /// <summary>Runs a full sub-comparison, discarding differences; used for order-insensitive matching.</summary>
    private bool IsEquivalent(object? actual, object? expected, DeclaredTypes declared)
    {
        // Share the live ancestor set. Entries are pushed and popped in CompareNodes, so the set is restored
        // when this returns and sibling trials never observe each other's entries. The sub-comparison
        // still gets a fresh _visitedPairs, keeping each trial's shared-subgraph dedup independent - a
        // pair fully compared in one trial must be re-derived in another rather than assumed equal.
        var comparison = new EquivalencyComparison(_shapes, _options, _activePairs);
        comparison.CompareNodes(actual, expected, declared, []);
        return comparison._differences.Count == 0;
    }

    /// <summary>
    /// Maximum bipartite matching between expected and actual elements, where
    /// <paramref name="equivalent"/>(e, a) reports whether expected element e is equivalent to
    /// actual element a. Returns an array indexed by expected element giving its matched actual
    /// index, or -1 when it cannot be matched.
    ///
    /// A greedy first pass resolves the common case (everything matches, or a genuine mismatch)
    /// without a full N*N equivalence matrix; only the expected elements it leaves unmatched fall
    /// back to augmenting-path search, which can reshuffle earlier assignments to free a partner.
    /// Equivalence results are memoized, so no element pair is ever compared twice. This keeps the
    /// order-insensitive path close to greedy cost on normal inputs while still finding a complete
    /// matching whenever one exists.
    /// </summary>
    private static int[] MatchByEquivalence(int expectedCount, int actualCount, Func<int, int, bool> equivalent)
    {
        var cache = new Dictionary<long, bool>();
        bool CanMatch(int expectedIndex, int actualIndex)
        {
            var key = (long)expectedIndex * actualCount + actualIndex;
            if (!cache.TryGetValue(key, out var result))
            {
                result = equivalent(expectedIndex, actualIndex);
                cache[key] = result;
            }

            return result;
        }

        var matchOfActual = new int[actualCount];
        for (var actualIndex = 0; actualIndex < actualCount; actualIndex++)
            matchOfActual[actualIndex] = -1;

        // Greedy warm-start: each expected element claims the first still-free equivalent actual.
        List<int>? unmatchedGreedily = null;
        for (var expectedIndex = 0; expectedIndex < expectedCount; expectedIndex++)
        {
            var matched = false;
            for (var actualIndex = 0; actualIndex < actualCount; actualIndex++)
            {
                if (matchOfActual[actualIndex] < 0 && CanMatch(expectedIndex, actualIndex))
                {
                    matchOfActual[actualIndex] = expectedIndex;
                    matched = true;
                    break;
                }
            }

            if (!matched)
                (unmatchedGreedily ??= []).Add(expectedIndex);
        }

        // Only the greedy leftovers can start an augmenting path; already-matched elements stay
        // matched, so attempting to augment them would never improve the matching.
        if (unmatchedGreedily != null)
        {
            foreach (var expectedIndex in unmatchedGreedily)
                Augment(expectedIndex, matchOfActual, CanMatch, actualCount, new bool[actualCount]);
        }

        var matchOfExpected = new int[expectedCount];
        for (var expectedIndex = 0; expectedIndex < expectedCount; expectedIndex++)
            matchOfExpected[expectedIndex] = -1;

        for (var actualIndex = 0; actualIndex < actualCount; actualIndex++)
        {
            if (matchOfActual[actualIndex] >= 0)
                matchOfExpected[matchOfActual[actualIndex]] = actualIndex;
        }

        return matchOfExpected;

        static bool Augment(int expectedIndex, int[] matchOfActual, Func<int, int, bool> canMatch, int actualCount, bool[] seen)
        {
            for (var actualIndex = 0; actualIndex < actualCount; actualIndex++)
            {
                if (seen[actualIndex] || !canMatch(expectedIndex, actualIndex))
                    continue;

                seen[actualIndex] = true;
                if (matchOfActual[actualIndex] < 0 || Augment(matchOfActual[actualIndex], matchOfActual, canMatch, actualCount, seen))
                {
                    matchOfActual[actualIndex] = expectedIndex;
                    return true;
                }
            }

            return false;
        }
    }

    /// <summary>
    /// Asks the type's own equality about two values that differ member by member, once the members have decided.
    /// It never changes the outcome, only what the failure says:
    /// <list type="bullet">
    /// <item>When it calls the values equal (an entity equal by Id, a value whose Equals ignores case), the failure
    /// names the type, so the reader knows why <c>ShouldBe</c> would pass and how to compare the type that way.</item>
    /// <item>When it agrees they differ, and the differences span several members, they are reported as one: the two
    /// values as their type writes them, and the members that differ. A NodaTime <c>LocalDate</c> is one difference
    /// of dates, not three of <c>Day</c>, <c>DayOfWeek</c> and <c>DayOfYear</c>. A record's equality is its
    /// members, which say more than the record does as a whole, so a record is left as it is.</item>
    /// </list>
    /// </summary>
    private void ConsultOwnEquality(
        object actual,
        object expected,
        EquivalencyTypeShape shape,
        List<string> path,
        int differencesBefore,
        List<string> differingMembers)
    {
        // An anonymous type's Equals compares its members as ShouldBe would, and it has no name to register a rule by.
        if (!shape.DefinesEquality || shape.Type.Name.Contains("AnonymousType"))
            return;

        bool equal;
        try
        {
            equal = Equality.Equal(actual, expected);
        }
        catch (Exception)
        {
            // An Equals that throws says nothing either way.
            return;
        }

        if (equal)
        {
            if (!_equalByOwnEquality.Contains(shape.Type))
                _equalByOwnEquality.Add(shape.Type);
        }
        else if (differingMembers.Count > 1 && !shape.IsRecord && ReadsAsAValue(actual, expected))
        {
            _differences.RemoveRange(differencesBefore, _differences.Count - differencesBefore);
            AddDifference(EquivalencyDifferenceKind.ValueMismatch, path, expected, actual, DifferAt(differingMembers));
        }
    }

    /// <summary>
    /// Whether a failure can show the two values whole: they are of one type, which writes them as text of its own,
    /// different for each. Values of different types differ in which members they have, which only the members show;
    /// and a type without a <c>ToString</c> of its own reads as its name.
    /// </summary>
    private static bool ReadsAsAValue(object actual, object expected)
    {
        if (actual.GetType() != expected.GetType())
            return false;

        try
        {
            var expectedText = expected.ToString();
            return expectedText != expected.GetType().ToString() &&
                   expected.ToStringAwesomely() != actual.ToStringAwesomely();
        }
        catch (Exception)
        {
            return false;
        }
    }

    private const int DifferingMembersNamed = 5;

    private static string DifferAt(List<string> members)
    {
        var named = string.Join(", ", members.Take(DifferingMembersNamed));
        var others = members.Count - DifferingMembersNamed;
        return others > 0
            ? $"they differ at {named} and {others} other members"
            : $"they differ at {named}";
    }

    private static bool LeafEquals(object actual, object expected)
    {
        if (IsNumeric(actual) && IsNumeric(expected))
        {
            if (actual is float or double || expected is float or double)
                return FloatingPointEquals(actual, expected);

            // Mixed integral and decimal values compare exactly through decimal.
            if (actual.GetType() != expected.GetType())
                return Convert.ToDecimal(actual) == Convert.ToDecimal(expected);
        }

        // Equals is called on the expected value, as in ShouldBe (see Is.Equal), so a broken Equals on the value
        // under test can't make the comparison pass.
        return expected.Equals(actual);
    }

    private static bool IsNumeric(object value) =>
        value is not Enum
        && Convert.GetTypeCode(value) is
            TypeCode.SByte or TypeCode.Byte or
            TypeCode.Int16 or TypeCode.UInt16 or
            TypeCode.Int32 or TypeCode.UInt32 or
            TypeCode.Int64 or TypeCode.UInt64 or
            TypeCode.Single or TypeCode.Double or
            TypeCode.Decimal;

    /// <summary>
    /// Compares through <see cref="Numerics"/>, as ShouldBe does, so mixed types are compared exactly (#1427) and
    /// <see cref="ShouldlyConfiguration.DefaultFloatingPointTolerance"/> applies. Under a default tolerance, mixed
    /// types are widened to double first: Numerics would narrow a float compared with an integer or decimal to float.
    /// </summary>
    private static bool FloatingPointEquals(object actual, object expected)
    {
        if (actual.GetType() != expected.GetType() && ShouldlyConfiguration.DefaultFloatingPointTolerance > 0.0d)
        {
            actual = Convert.ToDouble(actual);
            expected = Convert.ToDouble(expected);
        }

        return Numerics.AreEqual(expected, actual);
    }

    /// <summary>
    /// The shape of <paramref name="value"/>'s own type. A provider that knows a closed set of types can't always
    /// give one, and then the comparison stops: carrying on with the shape of the declared type would compare
    /// fewer members than the value has, and pass when they differ.
    /// </summary>
    private EquivalencyTypeShape ShapeOf(object value, Type? declaredType, List<string> path) =>
        _shapes.GetShape(value.GetType(), declaredType)
        ?? throw new NotSupportedException(
            $"ShouldBeEquivalentTo can't compare a value of type {GetDisplayName(value.GetType())}" +
            (path.Count == 0 ? "" : $" (at {string.Join(" > ", path).Trim()})") +
            ": the registered equivalency shape provider has no shape for it. Register a shape for the type.");

    private void AddDifference(EquivalencyDifferenceKind kind, List<string> path, object? expected, object? actual, string? detail = null) =>
        _differences.Add(new(kind, path, expected, actual, detail));

    /// <summary>
    /// The type a path names a node by: the type it was compared as, unless that is one of .NET's own internal
    /// types (<c>RuntimeType</c>, a LINQ iterator), which the type it is declared as says more plainly.
    /// </summary>
    private static Type TypeToName(Type comparedType, Type? declaredType) =>
        declaredType is not null &&
        declaredType != typeof(object) &&
        !comparedType.IsVisible &&
        comparedType.Namespace is { } @namespace &&
        (@namespace == "System" || @namespace.StartsWith("System.", StringComparison.Ordinal))
            ? declaredType
            : comparedType;

    private static List<string> AnnotateWithType(List<string> path, Type type)
    {
        var annotated = new List<string>(path);
        var typeName = $" [{GetDisplayName(type)}]";
        if (annotated.Count == 0)
            annotated.Add(typeName);
        else
            annotated[annotated.Count - 1] += typeName;

        return annotated;
    }

    /// <summary>
    /// A stable, readable type name: generic arguments rendered recursively rather than
    /// assembly-qualified (which would leak runtime versions into failure messages), and
    /// compiler-generated anonymous type names hidden.
    /// </summary>
    private static string GetDisplayName(Type type)
    {
        if (type.IsArray)
            return GetDisplayName(type.GetElementType()!) + "[" + new string(',', type.GetArrayRank() - 1) + "]";

        if (Nullable.GetUnderlyingType(type) is { } underlying)
            return GetDisplayName(underlying) + "?";

        if (!type.IsGenericType)
            return type.FullName ?? type.Name;

        if (type.Name.Contains("AnonymousType"))
            return "<anonymous type>";

        var name = type.FullName ?? type.Name;
        var backtick = name.IndexOf('`');
        if (backtick >= 0)
            name = name.Substring(0, backtick);

        return $"{name}<{string.Join(", ", type.GetGenericArguments().Select(GetDisplayName))}>";
    }

    private static List<string> Append(List<string> path, string segment) =>
        [.. path, segment];

    /// <summary>
    /// The types the members or collections holding the two values declare them as, when known. They don't decide
    /// how a value is compared. They are passed to the shape provider, which may need them for a value whose own
    /// type it has no shape for, such as a LINQ iterator held as an <c>IEnumerable&lt;T&gt;</c>.
    /// </summary>
    private readonly struct DeclaredTypes(Type? actual, Type? expected)
    {
        public Type? Actual { get; } = actual;

        public Type? Expected { get; } = expected;
    }

    private readonly struct VisitedPair : IEquatable<VisitedPair>
    {
        private readonly object _actual;
        private readonly object _expected;

        public VisitedPair(object actual, object expected)
        {
            _actual = actual;
            _expected = expected;
        }

        public bool Equals(VisitedPair other) =>
            ReferenceEquals(_actual, other._actual) && ReferenceEquals(_expected, other._expected);

        public override bool Equals(object? obj) =>
            obj is VisitedPair other && Equals(other);

        public override int GetHashCode() =>
            (RuntimeHelpers.GetHashCode(_actual) * 397) ^ RuntimeHelpers.GetHashCode(_expected);
    }
}
