namespace Shouldly.Internals;

/// <summary>
/// What tells an actual value apart from an expected one, as an <see cref="Internals.Equality"/> compares them: where two
/// sequences compared in order differ, what one of two dictionaries or sets has that the other doesn't, which items of
/// two sequences matched in any order found no partner, and the step of Shouldly's rules that compares them. A failure
/// message reads it, rather than comparing the two again its own way, so what it reports is what the check found.
/// </summary>
/// <remarks>
/// Each part is worked out the first time it is asked for, since a message asks for only some of them, and none of
/// them reads a sequence further than a message shows it.
/// </remarks>
sealed class Differences
{
    private Found<EqualityStep?> _step;
    private Found<SequenceDifference?> _inOrder;
    private Found<UnorderedDifferences?> _unordered;
    private Found<UnpairedItems?> _unpaired;

    internal Differences(Equality equality, object? actual, object? expected)
    {
        Equality = equality;
        Actual = actual;
        Expected = expected;
    }

    /// <summary>How the two were compared.</summary>
    public Equality Equality { get; }

    /// <summary>The value under test.</summary>
    public object? Actual { get; }

    /// <summary>The value it was compared against.</summary>
    public object? Expected { get; }

    /// <summary>
    /// The step of Shouldly's rules that compares the two, so that a message can say what decided. Null when either
    /// is null, or when a comparer of the caller's compared them whole and none of Shouldly's rules had a say.
    /// </summary>
    /// <remarks>
    /// An equality rule is considered only when Shouldly's own rules, or an element comparer a tolerance or case option
    /// stands for, compared the two. Two JSON values are compared as JSON, but where they differ is found in their
    /// shape, so for them this is the step their shape takes.
    /// </remarks>
    public EqualityStep? Step => _step.Get(this, static d => d.FindStep());

    /// <summary>
    /// Where two sequences compared in order differ, walking them in step as far as a message shows them. Null when
    /// they aren't sequences compared in order: when either is null or a string, or one is a dictionary or a set.
    /// </summary>
    public SequenceDifference? InOrder => _inOrder.Get(this, static d => d.FindInOrder());

    /// <summary>
    /// What one of two dictionaries or sets has that the other doesn't: the entries of two dictionaries matched by key,
    /// or the items of two sets matched one for one. Null when the two are compared in order (see
    /// <see cref="UnorderedCollection.IgnoresOrder"/>), and no differences when one of them is a sequence too long for
    /// all of it to have been kept for the message.
    /// </summary>
    /// <remarks>
    /// When a comparer of the caller's compared two items, a dictionary's entries are matched whole, as the comparer
    /// saw them, rather than by key.
    /// </remarks>
    public UnorderedDifferences? Unordered => _unordered.Get(this, static d => d.FindUnordered());

    /// <summary>
    /// The items of two sequences, matched one for one in any order, that found no partner, each side read no further
    /// than a message shows it. Null when either isn't a sequence.
    /// </summary>
    public UnpairedItems? Unpaired => _unpaired.Get(this, static d => d.FindUnpaired());

    private EqualityStep? FindStep()
    {
        if (NullEquivalence.IsNull(Actual) || NullEquivalence.IsNull(Expected) || Equality.ComparesValuesItself)
            return null;

        var step = Equality.UsesCustomComparer
            ? EqualityStep.Structural(Expected, Actual)
            : EqualityStep.For(Expected, Actual);
        return step.Kind == EqualityStepKind.Json ? EqualityStep.Structural(Expected, Actual) : step;
    }

    private SequenceDifference? FindInOrder()
    {
        if (NullEquivalence.IsNull(Actual) || NullEquivalence.IsNull(Expected) ||
            Actual is string || Expected is string ||
            !Actual.TryGetEnumerable(out var actualItems) || !Expected.TryGetEnumerable(out var expectedItems) ||
            UnorderedCollection.IgnoresOrder(Actual, Expected))
            return null;

        var limit = ShouldlyConfiguration.MaxEnumerableItemsInMessages;
        var positions = new List<WalkedPosition>();
        int? firstIndex = null;
        var firstIsPair = false;
        object? firstActual = null;
        object? firstExpected = null;
        var cutShort = false;

        // Walks both sequences in step, a single pass each, stopping after MaxEnumerableItemsInMessages positions so
        // an infinite sequence can't hang the message.
        var actualEnum = actualItems.GetEnumerator();
        var expectedEnum = expectedItems.GetEnumerator();
        try
        {
            while (true)
            {
                var actualHasItem = actualEnum.MoveNext();
                var expectedHasItem = expectedEnum.MoveNext();

                if (!actualHasItem && !expectedHasItem)
                    break;

                if (positions.Count == limit)
                {
                    cutShort = true;
                    break;
                }

                var actualItem = actualHasItem ? actualEnum.Current : null;
                var expectedItem = expectedHasItem ? expectedEnum.Current : null;
                var differs = !actualHasItem || !expectedHasItem || !ItemsEqual(actualItem, expectedItem);
                if (differs && firstIndex is null)
                {
                    firstIndex = positions.Count;
                    firstIsPair = actualHasItem && expectedHasItem;
                    firstActual = actualItem;
                    firstExpected = expectedItem;
                }

                positions.Add(new(actualHasItem, actualItem, differs));
            }
        }
        finally
        {
            (actualEnum as IDisposable)?.Dispose();
            (expectedEnum as IDisposable)?.Dispose();
        }

        return new(positions, firstIndex, firstIsPair, firstActual, firstExpected, cutShort);
    }

    // The assertion stops at its first difference, but a message compares every position it shows, so a nested
    // sequence there may never end. When one is longer than a message shows, compare it only as far as it's shown, as
    // the top level is. A caller-supplied comparer is left to decide for itself. A default ImmutableArray<T> can't be
    // read, so the comparison treats it as null (#1406). Dictionaries and sets aren't compared in order, so a part of
    // each can't stand for the whole.
    private bool ItemsEqual(object? actual, object? expected)
    {
        if (!Equality.ComparesElementsItself
            && !UnorderedCollection.IgnoresOrder(actual, expected)
            && actual is IEnumerable actualItems and not string
            && expected is IEnumerable expectedItems and not string
            && !NullEquivalence.IsNull(actual)
            && !NullEquivalence.IsNull(expected))
        {
            var actualRead = actualItems.ReadForMessage(out var actualTruncated);
            var expectedRead = expectedItems.ReadForMessage(out var expectedTruncated);
            if (actualTruncated || expectedTruncated)
                return Equality<List<object?>>.Default.Equal(actualRead, expectedRead);
        }

        return Equality.ElementsEqual(actual, expected);
    }

    private UnorderedDifferences? FindUnordered()
    {
        var kind = UnorderedCollection.KindOfPair(Actual, Expected);
        if (kind == UnorderedCollection.Kind.None || Actual is not IEnumerable || Expected is not IEnumerable)
            return null;

        if (!TryReadWhole(Actual, out var actualList) || !TryReadWhole(Expected, out var expectedList))
            return new(ByKey: false, []);

        // A dictionary is read by key only when its entries are key/value pairs (a Hashtable's are DictionaryEntry
        // structs) and Shouldly's own equality compared them.
        var byKey = kind == UnorderedCollection.Kind.Dictionary &&
                    !Equality.UsesCustomComparer &&
                    actualList.Concat(expectedList).All(entry => entry is not null && Tuples.TryDeconstructKeyValuePair(entry, out _, out _));

        var differences = new List<UnorderedDifference>();

        // Items that are equal whole are matched first, so that a key two entries share isn't paired with the wrong
        // one and its value reported as different.
        var (onlyInActual, onlyInExpected) = UnorderedCollection.Unpaired(actualList, expectedList, Equality.ElementsEqual);

        foreach (var expectedItem in onlyInExpected)
        {
            var sameKey = byKey
                ? onlyInActual.FindIndex(actualItem => Tuples.KeysEqual(expectedItem!, actualItem!, Equality<object?>.Default.Comparer))
                : -1;
            if (sameKey >= 0)
            {
                Tuples.TryDeconstructKeyValuePair(expectedItem!, out var key, out var expectedValue);
                Tuples.TryDeconstructKeyValuePair(onlyInActual[sameKey]!, out _, out var actualValue);
                onlyInActual.RemoveAt(sameKey);
                differences.Add(new(UnorderedDifferenceKind.ValueDiffers, key, expectedValue, actualValue));
            }
            else
            {
                differences.Add(new(UnorderedDifferenceKind.Missing, KeyOf(expectedItem, byKey), expectedItem, null));
            }
        }

        foreach (var actualItem in onlyInActual)
            differences.Add(new(UnorderedDifferenceKind.Unexpected, KeyOf(actualItem, byKey), null, actualItem));

        return new(byKey, differences);
    }

    // The items of one side. A collection is read whole, from the collection itself: it can be read again, and a
    // message may have kept only the part of it that it shows. Any other sequence is read from what was given, never
    // from the sequence behind it, which may be readable only once and was read by the assertion already. It is read
    // no further than a message shows, as it may never end. False when that isn't all of it, so that what the two
    // sides don't share can't be told.
    private static bool TryReadWhole(object value, out List<object?> items)
    {
        var original = CapturedSequence.Original(value);
        if (original is ICollection || UnorderedCollection.KindOf(original) != UnorderedCollection.Kind.None)
        {
            items = ((IEnumerable)original).Cast<object?>().ToList();
            return true;
        }

        items = ((IEnumerable)value).ReadForMessage(out var cutShort);
        return !cutShort;
    }

    private static object? KeyOf(object? item, bool byKey)
    {
        if (!byKey)
            return item;

        Tuples.TryDeconstructKeyValuePair(item!, out var key, out _);
        return key;
    }

    private UnpairedItems? FindUnpaired()
    {
        if (Expected is not IEnumerable expectedEnumerable || Actual is not IEnumerable actualEnumerable)
            return null;

        // Read only as far as a message can show, so an infinite sequence can't hang it. A side that was cut short
        // may still hold matches for the other side's items further on.
        var expected = expectedEnumerable.ReadForMessage(out var expectedCutShort);
        var actual = actualEnumerable.ReadForMessage(out var actualCutShort);

        // Occurrences are matched one for one, as the check matches them, so a difference in how many times an item
        // occurs is reported rather than hidden by an earlier match.
        var (onlyInActual, onlyInExpected) = UnorderedCollection.Unpaired(actual, expected, Equality.ElementsEqual);
        return new(actual, actualCutShort, expected, expectedCutShort, onlyInActual, onlyInExpected);
    }

    // A part, worked out once.
    private struct Found<T>
    {
        private bool _found;
        private T _value;

        public T Get(Differences differences, Func<Differences, T> find)
        {
            if (!_found)
            {
                _value = find(differences);
                _found = true;
            }

            return _value;
        }
    }
}

/// <summary>
/// What walking an actual and an expected sequence in step found, as far as a failure message shows them.
/// </summary>
sealed class SequenceDifference(
    IReadOnlyList<WalkedPosition> positions,
    int? firstIndex,
    bool firstIsPair,
    object? firstActual,
    object? firstExpected,
    bool cutShort)
{
    /// <summary>Each position walked: the actual sequence's item there, if it has one, and whether it differs.</summary>
    public IReadOnlyList<WalkedPosition> Positions { get; } = positions;

    /// <summary>The index of the first position that differs, or null when none of those walked does.</summary>
    public int? FirstIndex { get; } = firstIndex;

    /// <summary>
    /// Whether both sequences have an item at <see cref="FirstIndex"/>. When one of them ended there, they don't.
    /// </summary>
    public bool FirstIsPair { get; } = firstIsPair;

    /// <summary>The actual sequence's item at <see cref="FirstIndex"/>, when <see cref="FirstIsPair"/>.</summary>
    public object? FirstActual { get; } = firstActual;

    /// <summary>The expected sequence's item at <see cref="FirstIndex"/>, when <see cref="FirstIsPair"/>.</summary>
    public object? FirstExpected { get; } = firstExpected;

    /// <summary>Whether the sequences go on past what a failure message shows, so the walk stopped there.</summary>
    public bool CutShort { get; } = cutShort;
}

/// <summary>
/// One position of two sequences walked in step.
/// </summary>
/// <param name="HasActual">Whether the actual sequence has an item there; it may have ended.</param>
/// <param name="Actual">The actual sequence's item there.</param>
/// <param name="Differs">Whether it differs from the expected sequence's, or only one of them has an item there.</param>
readonly record struct WalkedPosition(bool HasActual, object? Actual, bool Differs);

enum UnorderedDifferenceKind
{
    /// <summary>The expected collection has an item, or a dictionary a key, that the actual one doesn't.</summary>
    Missing,

    /// <summary>The actual collection has an item, or a dictionary a key, that the expected one doesn't.</summary>
    Unexpected,

    /// <summary>Both dictionaries have the key, with different values.</summary>
    ValueDiffers
}

/// <summary>
/// One thing one of two dictionaries or sets has that the other doesn't.
/// </summary>
/// <param name="Kind">What differs.</param>
/// <param name="Item">The key, when two dictionaries were matched by key, and otherwise the item.</param>
/// <param name="Expected">The expected item, or the expected value for the key.</param>
/// <param name="Actual">The actual item, or the actual value for the key.</param>
sealed record UnorderedDifference(UnorderedDifferenceKind Kind, object? Item, object? Expected, object? Actual);

/// <summary>
/// What one of two dictionaries or sets has that the other doesn't.
/// </summary>
/// <param name="ByKey">Whether two dictionaries were matched by key, so each difference is about a key.</param>
/// <param name="Items">The differences: the expected side's first, in its order, then what only the actual side has.</param>
sealed record UnorderedDifferences(bool ByKey, IReadOnlyList<UnorderedDifference> Items);

/// <summary>
/// The items of two sequences matched one for one in any order, as far as a message shows them.
/// </summary>
/// <param name="Actual">The actual sequence's items, as far as they were read.</param>
/// <param name="ActualCutShort">Whether the actual sequence goes on past what was read.</param>
/// <param name="Expected">The expected sequence's items, as far as they were read.</param>
/// <param name="ExpectedCutShort">Whether the expected sequence goes on past what was read.</param>
/// <param name="OnlyInActual">The actual items read that found no partner, in their order.</param>
/// <param name="OnlyInExpected">The expected items read that found no partner, in their order.</param>
sealed record UnpairedItems(
    List<object?> Actual,
    bool ActualCutShort,
    List<object?> Expected,
    bool ExpectedCutShort,
    List<object?> OnlyInActual,
    List<object?> OnlyInExpected);
