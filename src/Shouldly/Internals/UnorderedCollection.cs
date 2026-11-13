using System.Collections.Concurrent;
#if NET8_0_OR_GREATER
using System.Collections.Frozen;
using System.Collections.Immutable;
#endif

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

    // .NET's own sets, and its sorted and ordered collections, named as types where Shouldly can refer to them. A
    // type's interfaces and its name are read by reflection, which finds less in a trimmed or NativeAOT app: an
    // interface nothing else asked of the type may be gone. A reference to the type itself always resolves.
    private static readonly Type[] KnownSets =
    [
        typeof(HashSet<>),
        typeof(SortedSet<>),
#if NET8_0_OR_GREATER
        typeof(ImmutableHashSet<>),
        typeof(ImmutableSortedSet<>),
        typeof(FrozenSet<>),
#endif
    ];

    private static readonly Type[] KnownToKeepAnOrder =
    [
        typeof(SortedSet<>),
        typeof(SortedDictionary<,>),
        typeof(SortedList<,>),
        typeof(SortedList),
        typeof(System.Collections.Specialized.OrderedDictionary),
#if NET8_0_OR_GREATER
        typeof(ImmutableSortedSet<>),
        typeof(ImmutableSortedDictionary<,>),
#endif
#if NET9_0_OR_GREATER
        typeof(OrderedDictionary<,>),
#endif
    ];

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

    /// <summary>
    /// What kind of unordered collection two values are compared as, or <see cref="Kind.None"/> when they are
    /// compared in order: a dictionary when either is one, as the entries of a dictionary and of a list of key/value
    /// pairs are told apart by key.
    /// </summary>
    public static Kind KindOfPair(object? actual, object? expected)
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
        if (IsOrDerivesFrom(type, KnownToKeepAnOrder))
            return true;

        // The same collections where this build of Shouldly can't refer to them, found by name.
        for (var current = type; current is not null; current = current.BaseType)
        {
            var name = current.IsGenericType ? current.GetGenericTypeDefinition().FullName : current.FullName;
            if (name is
                "System.Collections.Generic.OrderedDictionary`2" or
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
    /// <para>
    /// Each item is tried first against the item at its own position, so two collections that happen to enumerate in
    /// the same order are matched in a single pass.
    /// </para>
    /// <para>
    /// Equality needn't be transitive: under a floating-point tolerance, or a comparer or an equality rule of the
    /// caller's, an item can equal two others that don't equal each other. Taking the first equal item could then
    /// leave a later one with no partner, and the answer would depend on the order the items happen to come in. So
    /// an item with no partner left looks for an earlier pairing that can be changed to free one for it, and a
    /// complete pairing is found whenever there is one.
    /// </para>
    /// <para>
    /// Neither sequence is read more than one item past the end of the other (see <see cref="TryReadBoth{T}"/>), so
    /// one that never ends is still told apart from one that does.
    /// </para>
    /// </remarks>
    public static bool MatchOneForOne<T>(IEnumerable<T> actual, IEnumerable<T> expected, Func<T, T, bool> equal)
    {
        if (actual is ICollection actualCollection &&
            expected is ICollection expectedCollection &&
            actualCollection.Count != expectedCollection.Count)
            return false;

        if (!TryReadBoth(actual, expected, out var actualItems, out var expectedItems))
            return false;

        var pairing = new Pairing<T>(expectedItems, equal);
        foreach (var actualItem in actualItems)
        {
            if (!pairing.TryAdd(actualItem))
                return false;
        }

        return true;
    }

    /// <summary>
    /// The items of <paramref name="actual"/> and of <paramref name="expected"/> left over when as many as can be
    /// are paired one for one by <paramref name="equal"/>, which is called as <c>equal(actual, expected)</c>. Each
    /// is in the order it was given in.
    /// </summary>
    /// <remarks>
    /// A failure message reports these, so they are paired as <see cref="MatchOneForOne{T}"/> pairs them: an item
    /// that equals more than one other is never reported for want of a partner that another item could have done
    /// without.
    /// </remarks>
    public static (List<T> Actual, List<T> Expected) Unpaired<T>(IReadOnlyList<T> actual, List<T> expected, Func<T, T, bool> equal)
    {
        var pairing = new Pairing<T>(expected, equal);
        var unpairedActual = actual.Where(actualItem => !pairing.TryAdd(actualItem)).ToList();
        var unpairedExpected = expected.Where((_, expectedIndex) => !pairing.HasPartner(expectedIndex)).ToList();

        return (unpairedActual, unpairedExpected);
    }

    /// <summary>
    /// Reads all of <paramref name="actual"/> and <paramref name="expected"/>, an item of each at a time. False as
    /// soon as one ends before the other: they don't hold the same number of items, so they can't be matched one
    /// for one.
    /// </summary>
    /// <remarks>
    /// Reading them in step means neither is read more than one item past the end of the other. Either may be a
    /// sequence that never ends, and reading all of one before looking at the other would never return.
    /// </remarks>
    public static bool TryReadBoth<T>(IEnumerable<T> actual, IEnumerable<T> expected, out List<T> actualItems, out List<T> expectedItems)
    {
        actualItems = [];
        expectedItems = [];

        using var expectedEnumerator = expected.GetEnumerator();
        using var actualEnumerator = actual.GetEnumerator();

        while (true)
        {
            var expectedHasItem = expectedEnumerator.MoveNext();
            var actualHasItem = actualEnumerator.MoveNext();
            if (expectedHasItem != actualHasItem)
                return false;

            if (!expectedHasItem)
                return true;

            expectedItems.Add(expectedEnumerator.Current);
            actualItems.Add(actualEnumerator.Current);
        }
    }

    /// <summary>
    /// A one-for-one pairing of actual items with expected ones, built up an actual item at a time: a maximum
    /// matching, grown along augmenting paths. An item that finds no partner when it is added never finds one
    /// later, and one that has a partner always keeps one, though not always the same one.
    /// </summary>
    private sealed class Pairing<T>(List<T> expectedItems, Func<T, T, bool> equal)
    {
        private const int None = -1;

        private readonly List<T> _actualItems = [];

        // The index of the expected item each actual item is paired with, and the reverse.
        private readonly List<int> _expectedOfActual = [];
        private readonly int[] _actualOfExpected = Enumerable.Repeat(None, expectedItems.Count).ToArray();
        private int _pairs;

        // What equal said of an actual and an expected item, kept once pairings are being revisited, as the same
        // two items are then asked about again.
        private Dictionary<(int Actual, int Expected), bool>? _verdicts;

        // Where a search for a path has been. Kept from one search to the next, as a search is made for every item
        // whose partner isn't free.
        private int[]? _reachedFrom;
        private bool[]? _reached;
        private Queue<int>? _toVisit;

        /// <summary>Whether the expected item at <paramref name="expectedIndex"/> is paired with an actual item.</summary>
        public bool HasPartner(int expectedIndex) => _actualOfExpected[expectedIndex] != None;

        /// <summary>
        /// Pairs <paramref name="actualItem"/> with an expected item, changing earlier pairings if it takes that.
        /// False when no pairing of the items so far gives it a partner.
        /// </summary>
        public bool TryAdd(T actualItem)
        {
            var added = _actualItems.Count;
            _actualItems.Add(actualItem);
            _expectedOfActual.Add(None);

            // With every expected item paired already, there is no partner to find.
            if (_pairs == expectedItems.Count)
                return false;

            if (!TryPairWithAFreeItem(added) && !TryPairAlongAPath(added))
                return false;

            _pairs++;
            return true;
        }

        // The item at the same position first, the common case, then any other that has no partner yet.
        private bool TryPairWithAFreeItem(int added)
        {
            if (added < expectedItems.Count && IsFreeAndEqual(added))
            {
                Pair(added, added);
                return true;
            }

            for (var expectedIndex = 0; expectedIndex < expectedItems.Count; expectedIndex++)
            {
                if (expectedIndex != added && IsFreeAndEqual(expectedIndex))
                {
                    Pair(added, expectedIndex);
                    return true;
                }
            }

            return false;

            bool IsFreeAndEqual(int expectedIndex) =>
                _actualOfExpected[expectedIndex] == None && equal(_actualItems[added], expectedItems[expectedIndex]);
        }

        // Breadth-first from the new item: over the paired expected items it equals, then over the items those are
        // paired with and the expected items they equal instead, until one is free. Each pairing on the way there
        // is then moved along by one.
        private bool TryPairAlongAPath(int added)
        {
            var reachedFrom = _reachedFrom ??= new int[expectedItems.Count];
            var reached = _reached ??= new bool[expectedItems.Count];
            var toVisit = _toVisit ??= new();
            Array.Clear(reached, 0, reached.Length);
            toVisit.Clear();
            toVisit.Enqueue(added);

            while (toVisit.Count > 0)
            {
                var actualIndex = toVisit.Dequeue();
                for (var expectedIndex = 0; expectedIndex < expectedItems.Count; expectedIndex++)
                {
                    // The new item equals none of the free ones, or it would have been paired with one.
                    var isFree = _actualOfExpected[expectedIndex] == None;
                    if (reached[expectedIndex] ||
                        isFree && actualIndex == added ||
                        !AreEqual(actualIndex, expectedIndex, remember: actualIndex != added))
                        continue;

                    reached[expectedIndex] = true;
                    reachedFrom[expectedIndex] = actualIndex;

                    if (isFree)
                    {
                        MoveAlong(expectedIndex, reachedFrom);
                        return true;
                    }

                    toVisit.Enqueue(_actualOfExpected[expectedIndex]);
                }
            }

            return false;
        }

        private void MoveAlong(int freeExpectedIndex, int[] reachedFrom)
        {
            var expectedIndex = freeExpectedIndex;
            while (expectedIndex != None)
            {
                var actualIndex = reachedFrom[expectedIndex];
                var gaveUp = _expectedOfActual[actualIndex];
                Pair(actualIndex, expectedIndex);
                expectedIndex = gaveUp;
            }
        }

        private void Pair(int actualIndex, int expectedIndex)
        {
            _expectedOfActual[actualIndex] = expectedIndex;
            _actualOfExpected[expectedIndex] = actualIndex;
        }

        private bool AreEqual(int actualIndex, int expectedIndex, bool remember)
        {
            if (!remember)
                return equal(_actualItems[actualIndex], expectedItems[expectedIndex]);

            _verdicts ??= [];
            if (!_verdicts.TryGetValue((actualIndex, expectedIndex), out var verdict))
            {
                verdict = equal(_actualItems[actualIndex], expectedItems[expectedIndex]);
                _verdicts[(actualIndex, expectedIndex)] = verdict;
            }

            return verdict;
        }
    }

    /// <summary>
    /// What kind of unordered collection <paramref name="value"/> is, if any.
    /// </summary>
    public static Kind KindOf(object value) => TraitsOf(value).Kind;

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

        var isSet = IsOrDerivesFrom(type, KnownSets);
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

    private static bool IsOrDerivesFrom(Type type, Type[] definitions)
    {
        for (var current = type; current is not null; current = current.BaseType)
        {
            if (Array.IndexOf(definitions, current.IsGenericType ? current.GetGenericTypeDefinition() : current) >= 0)
                return true;
        }

        return false;
    }

    /// <summary>Whether <paramref name="type"/> is one of .NET's own set types, or derives from one.</summary>
    public static bool IsKnownSet(Type type) => IsOrDerivesFrom(type, KnownSets);

    // What kind of unordered collection a type is, and whether it keeps an order of its own all the same.
    private readonly record struct Traits(Kind Kind, bool KeepsAnOrder);
}
