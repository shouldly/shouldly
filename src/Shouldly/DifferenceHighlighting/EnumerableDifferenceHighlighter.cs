using Shouldly.Internals;

namespace Shouldly.DifferenceHighlighting;

/// <summary>
/// Highlights differences between IEnumerables of the same type,
/// marking differences with asterisks
/// </summary>
class EnumerableDifferenceHighlighter : IDifferenceHighlighter
{
    public bool CanProcess(IShouldlyAssertionContext context) =>
        context is
        {
            Expected: IEnumerable and not string,
            Actual: IEnumerable and not string
        };

    public string? HighlightDifferences(IShouldlyAssertionContext context)
    {
        if (!CanProcess(context))
            return context.Actual.ToStringAwesomely();

        // Compare as the assertion did, so a custom comparer's matches aren't highlighted as differences.
        var comparison = AssertionComparison.Of(context);

        return HighlightDifferencesBetweenLists((IEnumerable)context.Actual!, (IEnumerable)context.Expected!, comparison.ElementsEqual);
    }

    // Walks both sequences in step, a single pass each, stopping after
    // MaxEnumerableItemsInMessages positions so an infinite sequence can't hang the message.
    private static string HighlightDifferencesBetweenLists(IEnumerable actualList, IEnumerable expectedList, Func<object?, object?, bool> equal)
    {
        var limit = ShouldlyConfiguration.MaxEnumerableItemsInMessages;
        var items = new List<string?>();

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
                    break;
                }

                items.Add(GetComparedItemString(actualHasItem, actualEnum, expectedHasItem, expectedEnum, equal));
            }
        }
        finally
        {
            (actualEnum as IDisposable)?.Dispose();
            (expectedEnum as IDisposable)?.Dispose();
        }

        return "[" + string.Join(", ", items) + "]";
    }

    private static string? GetComparedItemString(
        bool actualHasItem, IEnumerator actualEnum,
        bool expectedHasItem, IEnumerator expectedEnum,
        Func<object?, object?, bool> equal)
    {
        if (!actualHasItem)
            return ItemDifferenceHighlighter.HighlightCharacter;

        var actualItem = actualEnum.Current;
        var formatted = actualItem.ToStringAwesomely();

        return expectedHasItem && equal(actualItem, expectedEnum.Current)
            ? formatted
            : ItemDifferenceHighlighter.HighlightItem(formatted);
    }
}
