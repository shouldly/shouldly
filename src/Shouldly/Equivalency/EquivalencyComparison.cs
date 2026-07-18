namespace Shouldly.Equivalency;

/// <summary>
/// The equivalency engine: walks the actual/expected graphs with a per-node strategy
/// (leaf / dictionary / set / sequence / member-wise), selecting members from the
/// expectation's declared types with subset semantics, and collects every difference
/// instead of stopping at the first.
/// </summary>
internal sealed class EquivalencyComparison
{
    private readonly IEquivalencyShapeProvider _shapes;
    private readonly EquivalencyOptions _options;
    private readonly List<EquivalencyDifference> _differences = [];
    private readonly HashSet<VisitedPair> _visitedPairs = [];

    private EquivalencyComparison(IEquivalencyShapeProvider shapes, EquivalencyOptions options)
    {
        _shapes = shapes;
        _options = options;
    }

    public static void Assert(
        object? actual,
        object? expected,
        EquivalencyOptions options,
        string? customMessage,
        string shouldlyMethod,
        string? actualExpression)
    {
        var comparison = new EquivalencyComparison(EquivalencyShapes.Current, options);
        comparison.CompareNodes(actual, expected, null, []);

        if (comparison._differences.Count == 0)
            return;

        throw new ShouldAssertException(
            new ExpectedEquivalenceShouldlyMessage(comparison._differences, expected, actual, customMessage, shouldlyMethod, actualExpression).ToString());
    }

    private void CompareNodes(object? actual, object? expected, Type? declaredType, List<string> path)
    {
        if (actual is null || expected is null)
        {
            // The path is deliberately left un-annotated with a type: with one side null there
            // is no single compared type to report.
            if (!(actual is null && expected is null))
                AddDifference(EquivalencyDifferenceKind.ValueMismatch, path, expected, actual);

            return;
        }

        // Member selection is driven by declared types; members declared as object fall back to
        // the expectation's runtime type so anonymous types and object-typed members compare
        // their real members rather than none.
        var comparisonType = declaredType is null || declaredType == typeof(object)
            ? expected.GetType()
            : declaredType;
        comparisonType = Nullable.GetUnderlyingType(comparisonType) ?? comparisonType;

        var annotatedPath = AnnotateWithType(path, comparisonType);

        var shape = _shapes.GetShape(comparisonType);
        switch (shape.Kind)
        {
            case EquivalencyNodeKind.Leaf:
                if (!LeafEquals(actual, expected))
                    AddDifference(EquivalencyDifferenceKind.ValueMismatch, annotatedPath, expected, actual);
                break;

            case EquivalencyNodeKind.Dictionary:
                CompareDictionaries(actual, expected, shape, annotatedPath);
                break;

            case EquivalencyNodeKind.Set:
                CompareUnordered(actual, expected, shape.ElementType, annotatedPath);
                break;

            case EquivalencyNodeKind.Sequence:
                CompareSequences(actual, expected, shape.ElementType, annotatedPath);
                break;

            default:
                CompareComplex(actual, expected, shape, annotatedPath);
                break;
        }
    }

    private void CompareComplex(object actual, object expected, EquivalencyTypeShape shape, List<string> path)
    {
        if (ReferenceEquals(actual, expected))
            return;

        // Cycle tracking by reference identity; value types cannot participate in cycles.
        if (actual is not ValueType && expected is not ValueType && !_visitedPairs.Add(new(actual, expected)))
            return;

        var actualType = actual.GetType();
        var actualShape = actualType == shape.Type ? shape : _shapes.GetShape(actualType);

        var comparedAny = false;
        foreach (var member in shape.Members)
        {
            if (_options.MembersToIgnore.Contains(member.Name))
                continue;

            comparedAny = true;

            var memberPath = Append(path, member.Name);
            var actualMember = actualShape.FindMember(member.Name);
            if (actualMember is null)
            {
                AddDifference(EquivalencyDifferenceKind.MissingMember, memberPath, member.Name, actual, GetDisplayName(actualType));
                continue;
            }

            CompareNodes(actualMember.GetValue(actual), member.GetValue(expected), member.DeclaredType, memberPath);
        }

        if (!comparedAny)
            AddDifference(EquivalencyDifferenceKind.VacuousComparison, path, expected, actual, GetDisplayName(shape.Type));
    }

    private void CompareSequences(object actual, object expected, Type? elementType, List<string> path)
    {
        if (expected is Array { Rank: > 1 } || actual is Array { Rank: > 1 })
        {
            CompareMultidimensionalArrays(actual, expected, elementType, path);
            return;
        }

        if (_options.IgnoreOrder)
        {
            CompareUnordered(actual, expected, elementType, path);
            return;
        }

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

    private void CompareMultidimensionalArrays(object actual, object expected, Type? elementType, List<string> path)
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

    private void CompareUnordered(object actual, object expected, Type? elementType, List<string> path)
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

    private void CompareDictionaries(object actual, object expected, EquivalencyTypeShape shape, List<string> path)
    {
        var actualShape = _shapes.GetShape(actual.GetType());
        if (actualShape.Kind != EquivalencyNodeKind.Dictionary)
        {
            AddDifference(EquivalencyDifferenceKind.ValueMismatch, path, expected, actual);
            return;
        }

        var expectedEntries = shape.GetEntries(expected).ToList();
        var actualEntries = actualShape.GetEntries(actual).ToList();

        // Match keys with maximum bipartite matching (see CompareUnordered) so that, when keys are
        // themselves complex and a key is equivalent to more than one candidate, a greedy pairing
        // cannot wrongly leave a matchable key unmatched.
        var matchOfExpected = MatchByEquivalence(
            expectedEntries.Count,
            actualEntries.Count,
            (e, a) => IsEquivalent(actualEntries[a].Key, expectedEntries[e].Key, shape.KeyType));

        var matchedActual = new bool[actualEntries.Count];

        for (var i = 0; i < expectedEntries.Count; i++)
        {
            var matchIndex = matchOfExpected[i];
            if (matchIndex < 0)
            {
                AddDifference(EquivalencyDifferenceKind.MissingKey, path, expectedEntries[i].Key, actual);
                continue;
            }

            matchedActual[matchIndex] = true;
            CompareNodes(actualEntries[matchIndex].Value, expectedEntries[i].Value, shape.ValueType, Append(path, $"[{expectedEntries[i].Key.ToStringAwesomely()}]"));
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
    private bool IsEquivalent(object? actual, object? expected, Type? declaredType)
    {
        var comparison = new EquivalencyComparison(_shapes, _options);
        comparison.CompareNodes(actual, expected, declaredType, []);
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

    private static bool LeafEquals(object actual, object expected)
    {
        if (actual.GetType() != expected.GetType() && IsNumeric(actual) && IsNumeric(expected))
            return NumericEquals(actual, expected);

        return actual.Equals(expected);
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

    private static bool NumericEquals(object actual, object expected)
    {
        if (actual is float or double || expected is float or double)
            return Convert.ToDouble(actual).Equals(Convert.ToDouble(expected));

        // Integral and decimal values compare exactly through decimal.
        return Convert.ToDecimal(actual) == Convert.ToDecimal(expected);
    }

    private void AddDifference(EquivalencyDifferenceKind kind, List<string> path, object? expected, object? actual, string? detail = null) =>
        _differences.Add(new(kind, path, expected, actual, detail));

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
