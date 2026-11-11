using Shouldly.DifferenceHighlighting;
using Shouldly.Equivalency;

namespace Shouldly.Internals;

/// <summary>
/// Says why <see cref="Equality"/> found two values unequal, for a failure message that doesn't show it: the two
/// values read the same, or they are sequences whose first difference takes finding. It follows the steps
/// Equality's comparer takes, so what it names (an equality rule, a difference in type, reference equality, the
/// type's own <c>Equals</c>) is what decided.
/// </summary>
/// <remarks>
/// An explanation is a courtesy. It reads members and calls <c>Equals</c> on values that have already failed an
/// assertion, so when any of that throws, the message goes without one.
/// </remarks>
static class Inequality
{
    // A marker this near the start of a list is found at a glance. Past it, an index saves the counting.
    private const int FirstIndexWorthStating = 10;

    // How far into members and items an explanation follows a difference.
    private const int MaxDepth = 8;

    private const string ByCustomComparer = "the custom comparer says they are not equal";

    /// <summary>
    /// The line that explains a failed comparison, or null when the message already shows why it failed.
    /// </summary>
    /// <param name="actual">The value under test.</param>
    /// <param name="expected">The value it was compared against.</param>
    /// <param name="comparison">How the assertion compared them.</param>
    /// <param name="difference">What walking the two found, when they are sequences already walked.</param>
    public static string? Explain(object? actual, object? expected, ComparisonRecord? comparison = null, SequenceDifference? difference = null)
    {
        try
        {
            return ExplainTopLevel(actual, expected, comparison ?? ComparisonRecord.Default, difference);
        }
        catch (Exception)
        {
            return null;
        }
    }

    private static string? ExplainTopLevel(object? actual, object? expected, ComparisonRecord comparison, SequenceDifference? difference)
    {
        if (NullEquivalence.IsNull(actual) || NullEquivalence.IsNull(expected))
            return null;

        // A comparer of the caller's, or an equality rule, judged the two whole. Nothing else had a say.
        if (comparison.ComparesValuesItself)
            return ReadTheSame(actual, expected) ? $"but {ByCustomComparer}" : null;

        if (!comparison.UsesCustomComparer && EqualityRules.RuleTypeFor(expected, actual) is { } ruleType)
            return ReadTheSame(actual, expected) ? $"but {ByRule(ruleType)}" : null;

        if (AreSequences(actual, expected, out var actualItems, out var expectedItems))
            return InSequences(difference ?? EnumerableDifferenceHighlighter.Find(actualItems, expectedItems, comparison), comparison);

        return ReadTheSame(actual, expected) && ByShouldlyEquality(actual, expected, depth: 0) is { } finding
            ? $"but {finding.Render()}"
            : null;
    }

    private static string? InSequences(SequenceDifference found, ComparisonRecord comparison)
    {
        if (found.FirstIndex is not { } index)
        {
            return found.CutShort
                ? $"the first difference is after the {ShouldlyConfiguration.MaxEnumerableItemsInMessages} items shown"
                : null;
        }

        var where = $"first difference at index {index}";
        if (found.FirstIsPair && ReadTheSame(found.FirstActual, found.FirstExpected))
        {
            var finding = comparison.ComparesElementsItself
                ? new(ByCustomComparer)
                : ByShouldlyEquality(found.FirstActual, found.FirstExpected, depth: 1);
            return finding is null ? where : $"{where}: {finding.Render()}";
        }

        return index >= FirstIndexWorthStating ? where : null;
    }

    /// <summary>
    /// Why two values that read the same are unequal by Shouldly's own equality, following the steps of
    /// <see cref="EqualityComparer{T}"/>.
    /// </summary>
    private static Finding? ByShouldlyEquality(object? actual, object? expected, int depth)
    {
        if (NullEquivalence.IsNull(actual) || NullEquivalence.IsNull(expected) || depth > MaxDepth)
            return null;

        if (EqualityRules.RuleTypeFor(expected, actual) is { } ruleType)
            return new(ByRule(ruleType));

        if (AreSequences(actual, expected, out var actualItems, out var expectedItems))
        {
            var found = EnumerableDifferenceHighlighter.Find(actualItems, expectedItems, ComparisonRecord.Default);
            if (found.FirstIndex is not { } index)
                return null;

            var finding = found.FirstIsPair && ReadTheSame(found.FirstActual, found.FirstExpected)
                ? ByShouldlyEquality(found.FirstActual, found.FirstExpected, depth + 1)
                : null;
            return (finding ?? new("they differ")).Under($"[{index}]");
        }

        // A key/value pair or a tuple is compared item by item, each by Shouldly's equality.
        if (Tuples.TryFindUnequalComponent(expected, actual, ObjectComparer, out var name, out var expectedComponent, out var actualComponent))
        {
            var finding = ReadTheSame(actualComponent, expectedComponent)
                ? ByShouldlyEquality(actualComponent, expectedComponent, depth + 1)
                : new(Differ(actualComponent, expectedComponent));
            return finding?.Under($".{name}");
        }

        return ByTheirOwnEquality(actual, expected, depth);
    }

    /// <summary>
    /// Why two values that read the same are unequal by their types' own equality, which is all that compares them
    /// once Shouldly's rules have had their say, and all that compares the members of a record or a tuple.
    /// </summary>
    private static Finding ByTheirOwnEquality(object actual, object expected, int depth)
    {
        var actualType = TypeOf(actual);
        var expectedType = TypeOf(expected);

        if (actualType != expectedType)
        {
            // Two tuples, or two anonymous types, of different element types: say which element.
            var inMember = HaveOneGenericDefinition(actualType, expectedType) && ComparesMemberByMember(expectedType)
                ? InMembers(actual, expected, depth)
                : null;
            return inMember ?? new($"they are different types (expected {FullName(expectedType)}, actual {FullName(actualType)})");
        }

        if (ComparesByReference(expectedType))
            return new($"they are different instances, and {ShortName(expectedType)} compares by reference; use ShouldBeEquivalentTo to compare members");

        if (ComparesMemberByMember(expectedType) && InMembers(actual, expected, depth) is { } memberFinding)
            return memberFinding;

        return new(ByOwnVerdict(actual, expected, expectedType));
    }

    // Which of the type's own members Equality asked depends on the type the assertion was made at, which isn't
    // known here, so this states only what calling them again bears out.
    private static string ByOwnVerdict(object actual, object expected, Type type)
    {
        var definesEquals = type.IsValueType || EqualsDeclaringType(type) != typeof(object) || ImplementsForItself(type, typeof(IEquatable<>));
        if (definesEquals && Returns(() => !expected.Equals(actual)))
            return $"{ShortName(type)}.Equals returned false";

        if (expected is IComparable comparable && Returns(() => comparable.CompareTo(actual) != 0))
            return $"{ShortName(type)}.CompareTo did not return 0";

        return $"they are not equal as {ShortName(type)} compares them";
    }

    /// <summary>
    /// The first member whose values are unequal, and why, for a type whose equality compares its members with
    /// their own <c>Equals</c>. Null when no public member differs, or when the type's members can't be read.
    /// </summary>
    private static Finding? InMembers(object actual, object expected, int depth)
    {
        if (depth >= MaxDepth ||
            EquivalencyShapes.TryGetShape(expected.GetType()) is not { Kind: EquivalencyNodeKind.Complex } expectedShape ||
            EquivalencyShapes.TryGetShape(actual.GetType()) is not { Kind: EquivalencyNodeKind.Complex } actualShape)
            return null;

        try
        {
            foreach (var expectedMember in expectedShape.Members)
            {
                if (actualShape.FindMember(expectedMember.Name) is not { } actualMember)
                    continue;

                var expectedValue = expectedMember.GetValue(expected);
                var actualValue = actualMember.GetValue(actual);
                if (Equals(expectedValue, actualValue))
                    continue;

                var finding = actualValue is not null && expectedValue is not null && ReadTheSame(actualValue, expectedValue)
                    ? ByTheirOwnEquality(actualValue, expectedValue, depth + 1)
                    : new(Differ(actualValue, expectedValue));
                return finding.Under($".{expectedMember.Name}");
            }
        }
        catch (Exception)
        {
            // A member that can't be read: fall back to what is known of the value as a whole.
        }

        return null;
    }

    /// <summary>
    /// Whether a failure message shows the two values as the same text. The formatter tells some values apart by a
    /// decoration alone, which doesn't tell a reader what is different: the suffix of a numeric type, or the hash
    /// code that stands in for a type with no text of its own.
    /// </summary>
    private static bool ReadTheSame(object? actual, object? expected)
    {
        var actualText = actual.ToStringAwesomely();
        var expectedText = expected.ToStringAwesomely();
        if (actualText == expectedText)
            return true;

        if (NullEquivalence.IsNull(actual) || NullEquivalence.IsNull(expected) || actualText is null || expectedText is null)
            return false;

        var undecorated = actual.ToString();
        return !string.IsNullOrEmpty(undecorated) &&
               undecorated == expected.ToString() &&
               actualText.StartsWith(undecorated, StringComparison.Ordinal) &&
               expectedText.StartsWith(undecorated, StringComparison.Ordinal);
    }

    // As EqualityComparer<T> decides it: anything it can enumerate, a Memory<T> included, but never a string.
    private static bool AreSequences(object actual, object expected, [NotNullWhen(true)] out IEnumerable? actualItems, [NotNullWhen(true)] out IEnumerable? expectedItems)
    {
        actualItems = expectedItems = null;
        return actual is not string &&
               expected is not string &&
               actual.TryGetEnumerable(out actualItems) &&
               expected.TryGetEnumerable(out expectedItems);
    }

    private static IEqualityComparer ObjectComparer => new EqualityComparerAdapter(Equality.ComparerFor<object>());

    private static string ByRule(Type ruleType) =>
        $"the equality rule for {ShortName(ruleType)} says they are not equal";

    private static string Differ(object? actual, object? expected) =>
        $"they differ (expected {expected.ToStringAwesomely()}, actual {actual.ToStringAwesomely()})";

    // Equals and CompareTo are called on the expected value, as Equality calls them.
    private static bool Returns(Func<bool> verdict)
    {
        try
        {
            return verdict();
        }
        catch (Exception)
        {
            return false;
        }
    }

    private static Type TypeOf(object value) =>
        CapturedSequence.Original(value).GetType();

    private static bool HaveOneGenericDefinition(Type one, Type other) =>
        one.IsGenericType && other.IsGenericType && one.GetGenericTypeDefinition() == other.GetGenericTypeDefinition();

    /// <summary>
    /// Whether nothing of <paramref name="type"/> defines equality, so that two instances are equal only when they
    /// are the same instance.
    /// </summary>
    private static bool ComparesByReference(Type type) =>
        !type.IsValueType &&
        EqualsDeclaringType(type) == typeof(object) &&
        !typeof(IComparable).IsAssignableFrom(type) &&
        !ImplementsForItself(type, typeof(IEquatable<>)) &&
        !ImplementsForItself(type, typeof(IComparable<>));

    /// <summary>
    /// Whether the equality of <paramref name="type"/> is the compiler's or the runtime's, which compares each member
    /// with that member's own <c>Equals</c>: a tuple, an anonymous type, a record, or a struct that doesn't override
    /// <c>Equals</c>.
    /// </summary>
    private static bool ComparesMemberByMember(Type type) =>
        IsTuple(type) ||
        IsAnonymous(type) ||
        IsRecord(type) ||
        (type.IsValueType && EqualsDeclaringType(type) == typeof(ValueType));

    private static bool IsTuple(Type type) =>
        type.IsGenericType &&
        type.GetGenericTypeDefinition().FullName is { } name &&
        (name.StartsWith("System.ValueTuple`", StringComparison.Ordinal) || name.StartsWith("System.Tuple`", StringComparison.Ordinal));

    private static bool IsAnonymous(Type type) =>
        type.Name.Contains("AnonymousType") &&
        (type.Name.StartsWith("<>", StringComparison.Ordinal) || type.Name.StartsWith("VB$", StringComparison.Ordinal)) &&
        type.IsDefined(typeof(CompilerGeneratedAttribute), inherit: false);

    // A record class has a compiler-generated <Clone>$ method. A record struct doesn't, but it has the PrintMembers
    // method its ToString calls.
    [UnconditionalSuppressMessage("Trimming", "IL2070",
        Justification = "Looks for a record's compiler-generated methods on a type that has a live instance. If trimming removed them, the type isn't taken for a record, and the explanation says only that its Equals returned false.")]
    private static bool IsRecord(Type type)
    {
        const BindingFlags anyInstance = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        if (!type.IsValueType)
            return type.GetMethod("<Clone>$", anyInstance) is not null;

        return type.GetMethod("PrintMembers", anyInstance, binder: null, [typeof(StringBuilder)], modifiers: null) is { } printMembers &&
               printMembers.IsDefined(typeof(CompilerGeneratedAttribute), inherit: false);
    }

    [UnconditionalSuppressMessage("Trimming", "IL2070",
        Justification = "Equals(object) is virtual on System.Object, so it or its override is kept for any type that has a live instance.")]
    private static Type? EqualsDeclaringType(Type type) =>
        type.GetMethod(nameof(Equals), BindingFlags.Public | BindingFlags.Instance, binder: null, [typeof(object)], modifiers: null)?.DeclaringType;

    // Whether the type implements IEquatable<T> or IComparable<T> for a T it is itself an instance of.
    [UnconditionalSuppressMessage("Trimming", "IL2070",
        Justification = "Reads the interfaces of a type that has a live instance. If trimming removed an implementation, the comparison didn't use it either.")]
    private static bool ImplementsForItself(Type type, Type openInterface)
    {
        foreach (var @interface in type.GetInterfaces())
        {
            if (@interface.IsGenericType &&
                @interface.GetGenericTypeDefinition() == openInterface &&
                @interface.GenericTypeArguments[0].IsAssignableFrom(type))
                return true;
        }

        return false;
    }

    private static string FullName(Type type) => Name(type, qualified: true);

    private static string ShortName(Type type) => Name(type, qualified: false);

    // A type's name as source code would spell it, where FullName writes List`1[[System.String, System.Private.CoreLib, ...]].
    private static string Name(Type type, bool qualified)
    {
        if (type.IsArray)
            return $"{Name(type.GetElementType()!, qualified)}[{new string(',', type.GetArrayRank() - 1)}]";

        if (type.IsGenericParameter)
            return type.Name;

        var name = WithoutArity(type.Name);
        if (qualified)
        {
            for (var outer = type.DeclaringType; outer is not null; outer = outer.DeclaringType)
                name = $"{WithoutArity(outer.Name)}+{name}";

            if (!string.IsNullOrEmpty(type.Namespace))
                name = $"{type.Namespace}.{name}";
        }

        return type.IsGenericType
            ? $"{name}<{string.Join(", ", type.GetGenericArguments().Select(argument => Name(argument, qualified)))}>"
            : name;
    }

    private static string WithoutArity(string name) =>
        name.IndexOf('`') is var tick and >= 0 ? name[..tick] : name;

    /// <summary>
    /// What decided that two values are unequal, and where in them it was decided: a path of members and indexes
    /// from the values a failure message shows.
    /// </summary>
    private sealed class Finding(string reason, string path = "")
    {
        public Finding Under(string step) => new(reason, step + path);

        public string Render() =>
            path.TrimStart('.') is { Length: > 0 } at ? $"at {at}, {reason}" : reason;
    }
}
