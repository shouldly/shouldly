namespace Shouldly.Internals;

/// <summary>
/// Shouldly's single definition of when an actual value equals an expected one. The assertions that compare values
/// (<c>ShouldBe</c>, <c>ShouldNotBe</c>, sequences, membership, dictionary values and key/value pairs) ask this
/// module rather than assembling their own comparer, so the rules (null equivalence, equality rules registered with
/// <see cref="ShouldlyConfiguration.UseEquality{T}(Func{T, T, bool})"/>, numerics, key/value pairs and tuples,
/// <see cref="IEquatable{T}"/>, <c>Equals</c> on the expected value) are decided in one place.
/// </summary>
/// <remarks>
/// The only option is an element comparer: one supplied by the caller, or the one a tolerance or case option of the
/// assertion's stands for (<see cref="WithinTolerance{T}(decimal)"/>, <see cref="ForCase"/>). Leaving it out means
/// Shouldly's own rules.
/// Every comparison calls the comparer as <c>comparer.Equals(expected, actual)</c>, so the default rules call
/// <c>Equals</c> on the expected value: a broken <c>Equals</c> on the value under test can't make an assertion pass
/// (issue #1076).
/// </remarks>
static class Equality
{
    /// <summary>
    /// Whether <paramref name="actual"/> equals <paramref name="expected"/>, by <paramref name="comparer"/> when given,
    /// and otherwise by <see cref="ComparerFor{T}"/>.
    /// </summary>
    public static bool Equal<T>(T? actual, T? expected, IEqualityComparer<T>? comparer = null) =>
        (comparer ?? ComparerFor<T>()).Equals(expected!, actual!);

    /// <summary>
    /// Whether <paramref name="actual"/> and <paramref name="expected"/> hold equal elements: in the same order, or,
    /// with <paramref name="ignoreOrder"/>, or when they are dictionaries or sets whose order isn't compared (see
    /// <see cref="UnorderedCollection"/>), matched one for one in any order. A default <c>ImmutableArray&lt;T&gt;</c>
    /// counts as null, and null equals only null. Elements are compared by <paramref name="comparer"/> when given.
    /// Without one, an equality rule that applies to both sequences as whole values decides instead, so a type such as
    /// <c>JToken</c>, which enumerates its children, isn't compared by shape alone. With <paramref name="ignoreOrder"/>
    /// the caller has asked for the elements to be matched in any order, which a whole-value rule (such as the
    /// order-sensitive <c>JToken.DeepEquals</c>) can't honour, so the elements are matched instead, each by its rules.
    /// </summary>
    public static bool SequenceEqual<T>(
        IEnumerable<T>? actual,
        IEnumerable<T>? expected,
        bool ignoreOrder = false,
        IEqualityComparer<T>? comparer = null)
    {
        actual = NullEquivalence.Normalize(actual);
        expected = NullEquivalence.Normalize(expected);

        if (actual == null || expected == null)
            return actual == null && expected == null;

        if (comparer == null && !ignoreOrder && EqualityRules.TryEquals(expected, actual, out var ruleResult))
            return ruleResult;

        comparer ??= ComparerFor<T>();
        var elementComparer = comparer;

        // The order a dictionary or a set enumerates in isn't part of its value (#1430).
        return ignoreOrder || UnorderedCollection.IgnoresOrder(actual, expected)
            ? UnorderedCollection.MatchOneForOne(actual, expected, (a, e) => Equal(a, e, elementComparer))
            : MatchInOrder(actual, expected, comparer);
    }

    /// <summary>
    /// The comparer behind <see cref="Equal{T}"/>, for code that hands one on. It can't hash:
    /// <see cref="IEqualityComparer{T}.GetHashCode(T)"/> throws.
    /// </summary>
    public static IEqualityComparer<T> ComparerFor<T>() => new EqualityComparer<T>();

    /// <summary>
    /// The element comparer of an assertion that takes a <see cref="decimal"/> tolerance: two decimals are equal when
    /// they are within <paramref name="tolerance"/> of each other. Any other pair is compared by Shouldly's own rules,
    /// as a custom assertion's message may hold values of another type than its tolerance. It can't hash, and it
    /// isn't transitive, which <see cref="SequenceEqual{T}"/> allows for when it matches in any order.
    /// </summary>
    /// <remarks>
    /// The assertion checks with it and hands the same comparer to its failure message, so that what the message
    /// marks as different is what the check found different.
    /// </remarks>
    public static IEqualityComparer<T> WithinTolerance<T>(decimal tolerance)
    {
        var otherwise = ComparerFor<T>();
        return new ElementComparer<T>((expected, actual) =>
            expected is decimal expectedNumber && actual is decimal actualNumber
                ? Is.Equal(actualNumber, expectedNumber, tolerance)
                : otherwise.Equals(expected!, actual!));
    }

    /// <summary>
    /// The element comparer of an assertion that takes a <see cref="double"/> tolerance: two doubles or floats are
    /// equal when they are within <paramref name="tolerance"/> of each other, a float widened to a double. Like
    /// <see cref="WithinTolerance{T}(decimal)"/>, it compares any other pair by Shouldly's own rules, can't hash and
    /// isn't transitive.
    /// </summary>
    public static IEqualityComparer<T> WithinTolerance<T>(double tolerance)
    {
        var otherwise = ComparerFor<T>();
        return new ElementComparer<T>((expected, actual) =>
            AsDouble(expected) is { } expectedNumber && AsDouble(actual) is { } actualNumber
                ? Is.Equal(actualNumber, expectedNumber, tolerance)
                : otherwise.Equals(expected!, actual!));
    }

    /// <summary>
    /// The element comparer of an assertion on strings that takes a <see cref="Case"/> option: ordinal, ignoring
    /// case or not.
    /// </summary>
    public static IEqualityComparer<string> ForCase(Case caseSensitivity) =>
        caseSensitivity == Case.Insensitive ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal;

    private static double? AsDouble<T>(T value) =>
        value switch
        {
            double number => number,
            float number => number,
            _ => null
        };

    // Called as equal(expected, actual), as every comparer here is (#1076).
    private sealed class ElementComparer<T>(Func<T?, T?, bool> equal) : IEqualityComparer<T>
    {
        public bool Equals(T? x, T? y) => equal(x, y);

        public int GetHashCode(T obj) =>
            throw new NotSupportedException("Values within a tolerance of each other can't be hashed alike.");
    }

    private static bool MatchInOrder<T>(IEnumerable<T> actual, IEnumerable<T> expected, IEqualityComparer<T> comparer)
    {
        using var expectedEnum = expected.GetEnumerator();
        using var actualEnum = actual.GetEnumerator();

        while (true)
        {
            var expectedHasData = expectedEnum.MoveNext();
            var actualHasData = actualEnum.MoveNext();

            if (!expectedHasData || !actualHasData)
                return expectedHasData == actualHasData;

            if (!Equal(actualEnum.Current, expectedEnum.Current, comparer))
                return false;
        }
    }
}
