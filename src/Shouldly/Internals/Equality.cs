namespace Shouldly.Internals;

/// <summary>
/// Shouldly's single definition of when an actual value equals an expected one. The assertions that compare values
/// without a tolerance or case option (<c>ShouldBe</c>, <c>ShouldNotBe</c>, sequences, membership, dictionary values
/// and key/value pairs) ask this module rather than assembling their own comparer, so the rules (null equivalence,
/// equality rules registered with <see cref="ShouldlyConfiguration.UseEquality{T}(Func{T, T, bool})"/>, numerics,
/// key/value pairs and tuples, <see cref="IEquatable{T}"/>, <c>Equals</c> on the expected value) are decided in one place.
/// </summary>
/// <remarks>
/// The only option is an element comparer supplied by the caller. Leaving it out means Shouldly's own rules.
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
    /// with <paramref name="ignoreOrder"/>, matched one for one in any order. A default <c>ImmutableArray&lt;T&gt;</c>
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
        return ignoreOrder
            ? MatchOneForOne(actual, expected, comparer)
            : MatchInOrder(actual, expected, comparer);
    }

    /// <summary>
    /// The comparer behind <see cref="Equal{T}"/>, for code that hands one on. It can't hash:
    /// <see cref="IEqualityComparer{T}.GetHashCode(T)"/> throws.
    /// </summary>
    public static IEqualityComparer<T> ComparerFor<T>() => new EqualityComparer<T>();

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

    private static bool MatchOneForOne<T>(IEnumerable<T> actual, IEnumerable<T> expected, IEqualityComparer<T> comparer)
    {
        if (actual is ICollection actualCollection &&
            expected is ICollection expectedCollection &&
            actualCollection.Count != expectedCollection.Count)
            return false;

        var expectedList = expected.ToList();
        foreach (var actualElement in actual)
        {
            // Match by index rather than FirstOrDefault + Remove: when no element is equivalent,
            // FirstOrDefault returns default(T), and Remove(default(T)) would succeed whenever
            // default(T) (e.g. 0 or null) is present in the list.
            var index = expectedList.FindIndex(expectedElement => Equal(actualElement, expectedElement, comparer));
            if (index < 0)
                return false;

            expectedList.RemoveAt(index);
        }

        return expectedList.Count == 0;
    }
}
