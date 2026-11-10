namespace Shouldly.Internals;

/// <summary>
/// Shouldly's single definition of when an actual value equals an expected one. Every assertion that compares
/// values asks this module rather than assembling its own comparer, so the rules (null equivalence, numerics,
/// compare-as-object types, key/value pairs, <see cref="IEquatable{T}"/>, <c>Equals</c> on the expected value) are
/// decided in one place.
/// </summary>
/// <remarks>
/// The only option is an element comparer: a caller-supplied one, or one of <see cref="Within(double)"/>,
/// <see cref="Within(decimal)"/> and <see cref="For(Case)"/>. Leaving it out means Shouldly's own rules.
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

        if (comparer == null && !ignoreOrder && IsCompareAsObjectType(typeof(T)))
            return Equal(actual, expected, new ObjectEqualityComparer<IEnumerable<T>>());

        comparer ??= StructuralComparer<T>();
        return ignoreOrder
            ? MatchOneForOne(actual, expected, comparer)
            : MatchInOrder(actual, expected, comparer);
    }

    /// <summary>
    /// The comparer behind <see cref="Equal{T}"/>, for code that hands one on: Shouldly's structural comparer, except
    /// for <see cref="string"/> and the compare-as-object types (<see cref="ShouldlyConfiguration.CompareAsObjectTypes"/>),
    /// which are compared with their own <see cref="object.Equals(object)"/> rather than walked as enumerables.
    /// It can't hash: <see cref="IEqualityComparer{T}.GetHashCode(T)"/> throws.
    /// </summary>
    public static IEqualityComparer<T> ComparerFor<T>() =>
        typeof(T) == typeof(string) || IsCompareAsObjectType(typeof(T))
            ? new ObjectEqualityComparer<T>()
            : StructuralComparer<T>();

    /// <summary>
    /// Compares numbers as equal when they differ by no more than <paramref name="tolerance"/>.
    /// </summary>
    public static AbsoluteTolerance Within(double tolerance) => new(tolerance);

    /// <summary>
    /// Compares decimals as equal when they differ by no more than <paramref name="tolerance"/>.
    /// </summary>
    public static IEqualityComparer<decimal> Within(decimal tolerance) => new DecimalTolerance(tolerance);

    /// <summary>
    /// Compares strings ordinally, ignoring case when <paramref name="caseSensitivity"/> is <see cref="Case.Insensitive"/>.
    /// </summary>
    public static IEqualityComparer<string?> For(Case caseSensitivity) =>
        caseSensitivity == Case.Insensitive ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal;

    private static bool IsCompareAsObjectType(Type type) =>
        ShouldlyConfiguration.CompareAsObjectTypes.Contains(type.FullName!);

    private static IEqualityComparer<T> StructuralComparer<T>() => new EqualityComparer<T>();

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

    /// <summary>
    /// Compares <see cref="double"/>s and <see cref="float"/>s (widened to <see cref="double"/>) as equal when they
    /// differ by no more than the tolerance. Can't hash.
    /// </summary>
    internal sealed class AbsoluteTolerance(double tolerance) : IEqualityComparer<double>, IEqualityComparer<float>
    {
        public bool Equals(double x, double y) => Math.Abs(x - y) <= tolerance;

        public bool Equals(float x, float y) => Equals((double)x, y);

        public int GetHashCode(double obj) => throw new NotSupportedException();

        public int GetHashCode(float obj) => throw new NotSupportedException();
    }

    private sealed class DecimalTolerance(decimal tolerance) : IEqualityComparer<decimal>
    {
        public bool Equals(decimal x, decimal y) => Math.Abs(x - y) <= tolerance;

        public int GetHashCode(decimal obj) => throw new NotSupportedException();
    }
}
