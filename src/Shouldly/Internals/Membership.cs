namespace Shouldly.Internals;

/// <summary>
/// Membership and set checks for <c>ShouldContain</c>, <c>ShouldBeOneOf</c>, <c>ShouldBeSubsetOf</c>,
/// <c>ShouldBeUnique</c> and their negations. Without a comparer of the caller's, elements are compared with
/// <see cref="Equality{T}.Default"/> so that they agree with <c>ShouldBe</c> on what counts as equal
/// (issue #1404). Each comparison goes through <see cref="Equality{T}.Equal"/>, so
/// <c>Equals</c> is called on the expected value, and a caller's comparer is called as
/// <c>comparer.Equals(expected, element)</c> (issue #1076).
/// </summary>
/// <remarks>
/// Shouldly's comparer can't hash, so without a comparer of the caller's the set checks match pairwise: O(n·m) for a
/// subset and O(n²) for uniqueness. A comparer of the caller's is hashed, as the documentation for those assertions
/// says. Where <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> provably gives the same answer
/// (<see cref="DefaultEqualityAgrees{T}"/>), they hash instead.
/// </remarks>
static class Membership
{
    /// <summary>
    /// Whether <paramref name="actual"/> has an element equal to <paramref name="expected"/>. A set, or a
    /// dictionary's key collection, also counts what its own lookup finds, since its comparer defines its membership,
    /// as a dictionary's key comparer does for <c>ShouldContainKey</c>.
    /// </summary>
    public static bool Contains<T>(IEnumerable<T> actual, T expected)
    {
        if (OwnLookupContains(actual, expected))
            return true;

        // A hash set whose comparer agrees with Shouldly's has answered exactly, so don't scan it.
        if (CanHash<T>() &&
            actual is HashSet<T> hashSet &&
            hashSet.Comparer.Equals(System.Collections.Generic.EqualityComparer<T>.Default))
            return false;

        var equality = Equality<T>.Default;
        foreach (var item in actual)
        {
            if (equality.Equal(item, expected))
                return true;
        }

        return false;
    }

    /// <summary>
    /// Whether <paramref name="actual"/> has an element that <paramref name="comparer"/>, a comparer of the caller's,
    /// finds equal to <paramref name="expected"/>. The comparer alone decides, so a set's own lookup isn't consulted.
    /// </summary>
    public static bool Contains<T>(IEnumerable<T> actual, T expected, IEqualityComparer<T> comparer)
    {
        var equality = Equality.Using(comparer);
        foreach (var item in actual)
        {
            if (equality.Equal(item, expected))
                return true;
        }

        return false;
    }

    /// <summary>
    /// Whether <paramref name="actual"/> equals one of <paramref name="candidates"/>.
    /// </summary>
    public static bool IsOneOf<T>(T? actual, IEnumerable<T> candidates)
    {
        var equality = Equality<T>.Default;
        foreach (var candidate in candidates)
        {
            if (equality.Equal(actual, candidate))
                return true;
        }

        return false;
    }

    /// <summary>
    /// Whether <paramref name="comparer"/>, a comparer of the caller's, finds <paramref name="actual"/> equal to one of
    /// <paramref name="candidates"/>.
    /// </summary>
    public static bool IsOneOf<T>(T? actual, IEnumerable<T> candidates, IEqualityComparer<T> comparer)
    {
        var equality = Equality.Using(comparer);
        foreach (var candidate in candidates)
        {
            if (equality.Equal(actual, candidate))
                return true;
        }

        return false;
    }

    /// <summary>
    /// The elements of <paramref name="actual"/> that equal none of <paramref name="expected"/>, each reported once,
    /// in the order first found.
    /// </summary>
    public static List<T> Unmatched<T>(IEnumerable<T> actual, IEnumerable<T> expected)
    {
        if (CanHash<T>())
            return actual.Except(expected).ToList();

        var equality = Equality<T>.Default;
        var expectedItems = expected.ToList();
        var unmatched = new List<T>();
        foreach (var item in actual)
        {
            if (!expectedItems.Exists(expectedItem => equality.Equal(item, expectedItem)) &&
                !unmatched.Exists(found => equality.Equal(item, found)))
            {
                unmatched.Add(item);
            }
        }

        return unmatched;
    }

    /// <summary>
    /// The elements of <paramref name="actual"/> that <paramref name="comparer"/>, a comparer of the caller's, finds
    /// equal to none of <paramref name="expected"/>, each reported once, in the order first found. The comparer is
    /// hashed, so a large collection is matched quickly (documented for <c>ShouldBeSubsetOf</c>), and called as
    /// <c>comparer.Equals(expected, element)</c>.
    /// </summary>
    public static List<T> Unmatched<T>(IEnumerable<T> actual, IEnumerable<T> expected, IEqualityComparer<T> comparer) =>
        // Except puts the expected elements in a set first, so each actual element is the argument to Equals.
        actual.Except(expected, comparer).ToList();

    /// <summary>
    /// The elements of <paramref name="items"/> that equal an earlier element, each value reported once, in the
    /// order first duplicated. Compares with <paramref name="comparer"/>, a comparer of the caller's, when one is given,
    /// by hashing it, so a large collection is matched quickly (documented for <c>ShouldBeUnique</c>).
    /// </summary>
    public static List<T> Duplicates<T>(IEnumerable<T> items, IEqualityComparer<T>? comparer = null)
    {
        if (comparer != null || CanHash<T>())
            return HashedDuplicates(items, comparer);

        // Compared with every earlier element, and deduplicated against the reported values themselves, rather than
        // against one representative per value: tolerance-based equality isn't transitive.
        var equality = Equality<T>.Default;
        var earlierItems = new List<T>();
        var duplicates = new List<T>();
        foreach (var item in items)
        {
            // The earlier element is the one Equals is called on.
            if (earlierItems.Exists(earlier => equality.Equal(item, earlier)) &&
                !duplicates.Exists(reported => equality.Equal(item, reported)))
            {
                duplicates.Add(item);
            }

            earlierItems.Add(item);
        }

        return duplicates;
    }

    private static List<T> HashedDuplicates<T>(IEnumerable<T> items, IEqualityComparer<T>? comparer)
    {
        var uniqueItems = new HashSet<T>(comparer);
        var reported = new HashSet<T>(comparer);
        var duplicates = new List<T>();
        foreach (var item in items)
        {
            if (!uniqueItems.Add(item) && reported.Add(item))
                duplicates.Add(item);
        }

        return duplicates;
    }

    private static bool OwnLookupContains<T>(IEnumerable<T> actual, T expected) =>
        actual switch
        {
            ISet<T> set => set.Contains(expected),
#if NET5_0_OR_GREATER
            IReadOnlySet<T> set => set.Contains(expected),
#endif
            ICollection<T> keys when IsDictionaryKeyCollection(keys.GetType()) => keys.Contains(expected),
            _ => false
        };

    // The BCL dictionaries' key views, whose Contains goes through the dictionary's key comparer. Matched by generic
    // type definition, so no MakeGenericType is needed and it stays AOT-safe. SortedList's view is a private nested
    // type, so it is matched by its declaring type and name.
    private static bool IsDictionaryKeyCollection(Type type) =>
        type.IsGenericType &&
        type.GetGenericTypeDefinition() is var definition &&
        (definition == typeof(Dictionary<,>.KeyCollection) ||
         definition == typeof(SortedDictionary<,>.KeyCollection) ||
         definition == typeof(System.Collections.ObjectModel.ReadOnlyDictionary<,>.KeyCollection) ||
         (definition.DeclaringType == typeof(SortedList<,>) && definition.Name == "KeyList"));

    /// <summary>
    /// Whether elements of <typeparamref name="T"/> can be hashed with the BCL's default comparer: it agrees with
    /// Shouldly's, and no registered equality rule could apply to them instead.
    /// </summary>
    private static bool CanHash<T>() =>
        DefaultEqualityAgrees<T>.Value && (EqualityRules.IsEmpty || !EqualityRules.CouldApplyTo(typeof(T)));

    /// <summary>
    /// Whether <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> (and so its hashing) gives the same answers as
    /// <see cref="Equality{T}.Default"/>: true for a closed list of BCL types whose <c>Equals</c> Shouldly's
    /// comparer reaches unchanged, when no equality rule applies (see <see cref="CanHash{T}"/>). Not for the
    /// floating-point types, which honour <see cref="ShouldlyConfiguration.DefaultFloatingPointTolerance"/> (see
    /// <see cref="Numbers.HonoursDefaultTolerance"/>), nor for <c>object</c> or user types, whose elements may compare across types, structurally or through
    /// <see cref="IEquatable{T}"/>/<see cref="IComparable{T}"/> without a matching <c>GetHashCode</c>.
    /// </summary>
    private static class DefaultEqualityAgrees<T>
    {
        public static readonly bool Value = Agrees(Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T));

        private static bool Agrees(Type type) =>
            type.IsEnum ||
            type == typeof(string) ||
            type == typeof(bool) ||
            type == typeof(char) ||
            type == typeof(sbyte) ||
            type == typeof(byte) ||
            type == typeof(short) ||
            type == typeof(ushort) ||
            type == typeof(int) ||
            type == typeof(uint) ||
            type == typeof(long) ||
            type == typeof(ulong) ||
            type == typeof(decimal) ||
            type == typeof(Guid) ||
            type == typeof(DateTime) ||
            type == typeof(DateTimeOffset) ||
            type == typeof(TimeSpan);
    }
}
