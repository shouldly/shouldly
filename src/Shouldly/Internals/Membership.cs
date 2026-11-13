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
/// The set checks hash with <see cref="Equality{T}.HashingComparer"/>: a comparer of the caller's, as the documentation
/// for those assertions says, or the BCL's for the types where it provably agrees with Shouldly's rules. Otherwise
/// they match pairwise: O(n·m) for a subset and O(n²) for uniqueness.
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
        var equality = Equality<T>.Default;
        if (actual is HashSet<T> hashSet &&
            equality.HashingComparer is { } hashing &&
            hashSet.Comparer.Equals(hashing))
            return false;

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
    public static List<T> Unmatched<T>(IEnumerable<T> actual, IEnumerable<T> expected) =>
        Unmatched(actual, expected, Equality<T>.Default);

    /// <summary>
    /// The elements of <paramref name="actual"/> that <paramref name="comparer"/>, a comparer of the caller's, finds
    /// equal to none of <paramref name="expected"/>, each reported once, in the order first found.
    /// </summary>
    public static List<T> Unmatched<T>(IEnumerable<T> actual, IEnumerable<T> expected, IEqualityComparer<T> comparer) =>
        Unmatched(actual, expected, Equality.Using(comparer));

    private static List<T> Unmatched<T>(IEnumerable<T> actual, IEnumerable<T> expected, Equality<T> equality)
    {
        // Except puts the expected elements in a set first, so each actual element is the argument to Equals.
        if (equality.HashingComparer is { } hashing)
            return actual.Except(expected, hashing).ToList();

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
    /// The elements of <paramref name="items"/> that equal an earlier element, each value reported once, in the
    /// order first duplicated.
    /// </summary>
    public static List<T> Duplicates<T>(IEnumerable<T> items) =>
        Duplicates(items, Equality<T>.Default);

    /// <summary>
    /// The elements of <paramref name="items"/> that <paramref name="comparer"/>, a comparer of the caller's, finds
    /// equal to an earlier element, each value reported once, in the order first duplicated.
    /// </summary>
    public static List<T> Duplicates<T>(IEnumerable<T> items, IEqualityComparer<T> comparer) =>
        Duplicates(items, Equality.Using(comparer));

    private static List<T> Duplicates<T>(IEnumerable<T> items, Equality<T> equality)
    {
        var duplicates = new List<T>();
        if (equality.HashingComparer is { } hashing)
        {
            // A set calls Equals on the element it already holds, the earlier one.
            var uniqueItems = new HashSet<T>(hashing);
            var reported = new HashSet<T>(hashing);
            foreach (var item in items)
            {
                if (!uniqueItems.Add(item) && reported.Add(item))
                    duplicates.Add(item);
            }

            return duplicates;
        }

        // Compared with every earlier element, and deduplicated against the reported values themselves, rather than
        // against one representative per value: tolerance-based equality isn't transitive.
        var earlierItems = new List<T>();
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
}
