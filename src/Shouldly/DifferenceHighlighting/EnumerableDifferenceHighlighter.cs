using Shouldly.Internals;

namespace Shouldly.DifferenceHighlighting;

/// <summary>
/// Highlights differences between IEnumerables of the same type,
/// marking differences with asterisks
/// </summary>
static class EnumerableDifferenceHighlighter
{
    // A default ImmutableArray<T> is IEnumerable but throws when enumerated, so it is left to the formatter, which
    // prints it as null (#1406).
    public static bool CanHighlight(object? actual, object? expected) =>
        expected is IEnumerable and not string &&
        actual is IEnumerable and not string &&
        !NullEquivalence.IsNull(expected) &&
        !NullEquivalence.IsNull(actual);

    // Compares as the assertion did, so a custom comparer's matches aren't highlighted as differences.
    // Walks both sequences in step, a single pass each, stopping after
    // MaxEnumerableItemsInMessages positions so an infinite sequence can't hang the message.
    public static SequenceDifference Find(IEnumerable actualList, IEnumerable expectedList, ComparisonRecord comparison)
    {
        var limit = ShouldlyConfiguration.MaxEnumerableItemsInMessages;
        var items = new List<string?>();
        int? firstIndex = null;
        var firstIsPair = false;
        object? firstActual = null;
        object? firstExpected = null;
        var cutShort = false;

        var actualEnum = actualList.GetEnumerator();
        var expectedEnum = expectedList.GetEnumerator();
        try
        {
            while (true)
            {
                var actualHasItem = actualEnum.MoveNext();
                var expectedHasItem = expectedEnum.MoveNext();

                if (!actualHasItem && !expectedHasItem)
                    break;

                if (items.Count == limit)
                {
                    items.Add(StringHelpers.EnumerableTruncationMarker);
                    cutShort = true;
                    break;
                }

                var actualItem = actualHasItem ? actualEnum.Current : null;
                var expectedItem = expectedHasItem ? expectedEnum.Current : null;
                var differs = !actualHasItem || !expectedHasItem || !ItemsEqual(actualItem, expectedItem, comparison);
                if (differs && firstIndex is null)
                {
                    firstIndex = items.Count;
                    firstIsPair = actualHasItem && expectedHasItem;
                    firstActual = actualItem;
                    firstExpected = expectedItem;
                }

                items.Add(Format(actualHasItem, actualItem, differs));
            }
        }
        finally
        {
            (actualEnum as IDisposable)?.Dispose();
            (expectedEnum as IDisposable)?.Dispose();
        }

        return new("[" + string.Join(", ", items) + "]", firstIndex, firstIsPair, firstActual, firstExpected, cutShort);
    }

    // The assertion stops at its first difference, but the highlighter compares every position it
    // shows, so a nested sequence there may never end. When one is longer than a message shows,
    // compare it only as far as it's shown, as the top level is. A caller-supplied comparer is
    // left to decide for itself. A default ImmutableArray<T> can't be read, so the comparison treats it as null (#1406).
    // Two dictionaries or two sets aren't compared in order, so a part of each can't stand for the whole.
    private static bool ItemsEqual(object? actual, object? expected, ComparisonRecord comparison)
    {
        if (!comparison.ComparesElementsItself
            && UnorderedCollection.BothAre(actual, expected) == UnorderedCollection.Kind.None
            && actual is IEnumerable actualItems and not string
            && expected is IEnumerable expectedItems and not string
            && !NullEquivalence.IsNull(actual)
            && !NullEquivalence.IsNull(expected))
        {
            var actualRead = actualItems.ReadForMessage(out var actualTruncated);
            var expectedRead = expectedItems.ReadForMessage(out var expectedTruncated);
            if (actualTruncated || expectedTruncated)
                return Is.Equal(actualRead, expectedRead);
        }

        return comparison.ElementsEqual(actual, expected);
    }

    private static string? Format(bool actualHasItem, object? actualItem, bool differs)
    {
        if (!actualHasItem)
            return ItemDifferenceHighlighter.HighlightCharacter;

        var formatted = actualItem.ToStringAwesomely();
        return differs ? ItemDifferenceHighlighter.HighlightItem(formatted) : formatted;
    }
}
