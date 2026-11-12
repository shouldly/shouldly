using System.Collections.Concurrent;

namespace Shouldly.Internals;

/// <summary>
/// Dictionaries and sets, the collections whose enumeration order is an implementation detail of the collection
/// rather than part of its value. <see cref="Equality"/> matches the items of two collections one for one in any
/// order when both are dictionaries or sets, or when one is and has no order of its own to compare (a
/// <c>HashSet&lt;T&gt;</c> against an array). A failure message then reports what one has that the other doesn't,
/// rather than where they first differ (issue #1430).
/// </summary>
/// <remarks>
/// <para>
/// A sorted or ordered collection (a <c>SortedSet&lt;T&gt;</c>, an <c>OrderedDictionary</c>) does have an order of
/// its own, so against a list or an array it is compared in order, like any other sequence: the test may be about
/// that order.
/// </para>
/// <para>
/// A dictionary's entries are matched by key and their values compared, each by <see cref="Equality"/>, as
/// <c>ShouldBeEquivalentTo</c> matches them. Neither dictionary's own key comparer, nor a set's own element comparer,
/// takes part: Shouldly never consults a collection's comparer when it compares two collections.
/// </para>
/// </remarks>
static class UnorderedCollection
{
    private static readonly ConcurrentDictionary<Type, Traits> TraitsByType = new();

    public enum Kind
    {
        None,
        Dictionary,
        Set
    }

    /// <summary>
    /// Whether <paramref name="actual"/> and <paramref name="expected"/> are compared without regard to order: both
    /// are dictionaries or sets, or one of them is and keeps no order of its own. A captured sequence is judged by
    /// the sequence it captured.
    /// </summary>
    public static bool IgnoresOrder(object? actual, object? expected) =>
        KindOfPair(actual, expected) != Kind.None;

    // Dictionary when either is one, as the entries of a dictionary and of a list of key/value pairs are told apart by key.
    private static Kind KindOfPair(object? actual, object? expected)
    {
        if (actual is null || expected is null)
            return Kind.None;

        var actualTraits = TraitsOf(CapturedSequence.Original(actual));
        var expectedTraits = TraitsOf(CapturedSequence.Original(expected));
        var ignoresOrder = actualTraits.Kind != Kind.None && expectedTraits.Kind != Kind.None ||
                           actualTraits.Kind != Kind.None && !actualTraits.KeepsAnOrder ||
                           expectedTraits.Kind != Kind.None && !expectedTraits.KeepsAnOrder;
        if (!ignoresOrder)
            return Kind.None;

        return actualTraits.Kind == Kind.Dictionary || expectedTraits.Kind == Kind.Dictionary ? Kind.Dictionary : Kind.Set;
    }

    /// <summary>
    /// Whether a dictionary or set of <paramref name="type"/> keeps its items in an order that is part of what it
    /// is, rather than an accident of how it stores them: one of .NET's sorted or ordered collections, or one that
    /// can be indexed by position.
    /// </summary>
    [UnconditionalSuppressMessage("Trimming", "IL2070",
        Justification = "Reads the interfaces of a type that has a live instance. If trimming removed one, the collection is taken to have no order of its own unless it is one of the sorted types named here.")]
    public static bool KeepsAnOrder(Type type)
    {
        for (var current = type; current is not null; current = current.BaseType)
        {
            var name = current.IsGenericType ? current.GetGenericTypeDefinition().FullName : current.FullName;
            if (name is
                "System.Collections.Generic.SortedSet`1" or
                "System.Collections.Generic.SortedDictionary`2" or
                "System.Collections.Generic.SortedList`2" or
                "System.Collections.Generic.OrderedDictionary`2" or
                "System.Collections.SortedList" or
                "System.Collections.Specialized.OrderedDictionary" or
                "System.Collections.Immutable.ImmutableSortedSet`1" or
                "System.Collections.Immutable.ImmutableSortedDictionary`2")
                return true;
        }

        if (typeof(IList).IsAssignableFrom(type))
            return true;

        foreach (var @interface in type.GetInterfaces())
        {
            if (@interface.IsGenericType &&
                @interface.GetGenericTypeDefinition() is var definition &&
                (definition == typeof(IList<>) || definition == typeof(IReadOnlyList<>)))
                return true;
        }

        return false;
    }

    /// <summary>
    /// Whether <paramref name="actual"/> holds the items of <paramref name="expected"/>, matched one for one in any
    /// order by <paramref name="equal"/>, which is called as <c>equal(actual, expected)</c>.
    /// </summary>
    /// <remarks>
    /// Each item is tried first against the item at its own position, so two collections that happen to enumerate in
    /// the same order are matched in a single pass.
    /// </remarks>
    public static bool MatchOneForOne<T>(IEnumerable<T> actual, IEnumerable<T> expected, Func<T, T, bool> equal)
    {
        if (actual is ICollection actualCollection &&
            expected is ICollection expectedCollection &&
            actualCollection.Count != expectedCollection.Count)
            return false;

        var expectedItems = expected.ToList();
        var matched = new bool[expectedItems.Count];
        var unmatched = expectedItems.Count;
        var position = 0;
        foreach (var actualItem in actual)
        {
            if (FindUnmatched(expectedItems, matched, position++, expectedItem => equal(actualItem, expectedItem)) is not { } index)
                return false;

            matched[index] = true;
            unmatched--;
        }

        return unmatched == 0;
    }

    /// <summary>
    /// What one of two dictionaries or sets has that the other doesn't, for a failure message: the entries of two
    /// dictionaries matched by key, or the items of two sets matched one for one. Null when
    /// <paramref name="actual"/> and <paramref name="expected"/> are compared in order (see
    /// <see cref="IgnoresOrder"/>).
    /// </summary>
    /// <param name="actual">The value under test.</param>
    /// <param name="expected">The value it was compared against.</param>
    /// <param name="comparison">
    /// How the assertion compared two items. When a comparer of the caller's compared them, a dictionary's entries
    /// are matched whole, as the comparer saw them, rather than by key.
    /// </param>
    public static Differences? Find(object? actual, object? expected, ComparisonRecord comparison)
    {
        var kind = KindOfPair(actual, expected);
        if (kind == Kind.None ||
            CapturedSequence.Original(actual!) is not IEnumerable actualItems ||
            CapturedSequence.Original(expected!) is not IEnumerable expectedItems)
            return null;

        var actualList = actualItems.Cast<object?>().ToList();
        var expectedList = expectedItems.Cast<object?>().ToList();

        // A dictionary is read by key only when its entries are key/value pairs (a Hashtable's are DictionaryEntry
        // structs) and Shouldly's own equality compared them.
        var byKey = kind == Kind.Dictionary &&
                    !comparison.UsesCustomComparer &&
                    actualList.Concat(expectedList).All(entry => entry is not null && Tuples.TryDeconstructKeyValuePair(entry, out _, out _));

        var differences = new List<Difference>();
        var matchedActual = new bool[actualList.Count];

        // Items that are equal whole are matched first, so that a key two entries share isn't paired with the wrong
        // one and its value reported as different.
        var unmatchedExpected = new List<object?>();
        for (var e = 0; e < expectedList.Count; e++)
        {
            var expectedItem = expectedList[e];
            if (FindUnmatched(actualList, matchedActual, e, actualItem => comparison.ElementsEqual(actualItem, expectedItem)) is { } a)
                matchedActual[a] = true;
            else
                unmatchedExpected.Add(expectedItem);
        }

        foreach (var expectedItem in unmatchedExpected)
        {
            if (byKey && FindUnmatched(actualList, matchedActual, 0, actualItem => Tuples.KeysEqual(expectedItem!, actualItem!, ObjectComparer)) is { } a)
            {
                matchedActual[a] = true;
                Tuples.TryDeconstructKeyValuePair(expectedItem!, out var key, out var expectedValue);
                Tuples.TryDeconstructKeyValuePair(actualList[a]!, out _, out var actualValue);
                differences.Add(new(DifferenceKind.ValueDiffers, key, expectedValue, actualValue));
            }
            else
            {
                differences.Add(new(DifferenceKind.Missing, KeyOf(expectedItem, byKey), expectedItem, null));
            }
        }

        for (var a = 0; a < actualList.Count; a++)
        {
            if (!matchedActual[a])
                differences.Add(new(DifferenceKind.Unexpected, KeyOf(actualList[a], byKey), null, actualList[a]));
        }

        return new(byKey, differences);
    }

    private static object? KeyOf(object? item, bool byKey)
    {
        if (!byKey)
            return item;

        Tuples.TryDeconstructKeyValuePair(item!, out var key, out _);
        return key;
    }

    private static IEqualityComparer ObjectComparer => new EqualityComparerAdapter(Equality.ComparerFor<object>());

    // The first unmatched item that satisfies the predicate, trying the one at the given position before the rest.
    private static int? FindUnmatched<T>(List<T> items, bool[] matched, int position, Func<T, bool> predicate)
    {
        if (position < items.Count && !matched[position] && predicate(items[position]))
            return position;

        for (var index = 0; index < items.Count; index++)
        {
            if (index != position && !matched[index] && predicate(items[index]))
                return index;
        }

        return null;
    }

    private static Traits TraitsOf(object value) =>
        value is string ? default : TraitsByType.GetOrAdd(value.GetType(), static type => TraitsOfType(type));

    private static Traits TraitsOfType(Type type) =>
        KindOfType(type) is var kind and not Kind.None ? new(kind, KeepsAnOrder(type)) : default;

    [UnconditionalSuppressMessage("Trimming", "IL2070",
        Justification = "Reads the interfaces of a type that has a live instance. If trimming removed one, the collection is compared in order, as any other sequence is.")]
    private static Kind KindOfType(Type type)
    {
        if (typeof(IDictionary).IsAssignableFrom(type))
            return Kind.Dictionary;

        var isSet = false;
        foreach (var @interface in type.GetInterfaces())
        {
            if (!@interface.IsGenericType)
                continue;

            var definition = @interface.GetGenericTypeDefinition();
            if (definition == typeof(IDictionary<,>) || definition == typeof(IReadOnlyDictionary<,>))
                return Kind.Dictionary;

            isSet |= definition == typeof(ISet<>) ||
                     definition.FullName == "System.Collections.Generic.IReadOnlySet`1";
        }

        return isSet ? Kind.Set : Kind.None;
    }

    // What kind of unordered collection a type is, and whether it keeps an order of its own all the same.
    private readonly record struct Traits(Kind Kind, bool KeepsAnOrder);

    public enum DifferenceKind
    {
        /// <summary>The expected collection has an item, or a dictionary a key, that the actual one doesn't.</summary>
        Missing,

        /// <summary>The actual collection has an item, or a dictionary a key, that the expected one doesn't.</summary>
        Unexpected,

        /// <summary>Both dictionaries have the key, with different values.</summary>
        ValueDiffers
    }

    /// <summary>
    /// One thing one collection has that the other doesn't.
    /// </summary>
    /// <param name="Kind">What differs.</param>
    /// <param name="Item">The key, when two dictionaries were matched by key, and otherwise the item.</param>
    /// <param name="Expected">The expected item, or the expected value for the key.</param>
    /// <param name="Actual">The actual item, or the actual value for the key.</param>
    public sealed record Difference(DifferenceKind Kind, object? Item, object? Expected, object? Actual);

    /// <summary>
    /// What one of two dictionaries or sets has that the other doesn't.
    /// </summary>
    /// <param name="ByKey">Whether two dictionaries were matched by key, so each difference is about a key.</param>
    /// <param name="Items">The differences: the expected side's first, in its order, then what only the actual side has.</param>
    public sealed record Differences(bool ByKey, IReadOnlyList<Difference> Items);
}
