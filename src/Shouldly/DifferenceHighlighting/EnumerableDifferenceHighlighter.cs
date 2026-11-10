using Shouldly.Internals;

namespace Shouldly.DifferenceHighlighting;

/// <summary>
/// Highlights differences between IEnumerables of the same type,
/// marking differences with asterisks
/// </summary>
class EnumerableDifferenceHighlighter : IDifferenceHighlighter
{
    private const int MaxElementsToShow = 1000;

    // A default ImmutableArray<T> is IEnumerable but throws when enumerated, so it is left to the formatter, which
    // prints it as null (#1406).
    public bool CanProcess(IShouldlyAssertionContext context) =>
        context is
        {
            Expected: IEnumerable and not string,
            Actual: IEnumerable and not string
        } &&
        !NullEquivalence.IsNull(context.Expected) &&
        !NullEquivalence.IsNull(context.Actual);

    public string? HighlightDifferences(IShouldlyAssertionContext context)
    {
        var actual = context.Actual as IEnumerable;
        var expected = context.Expected as IEnumerable;
        if (CanProcess(context))
        {
            var actualList = actual!.Cast<object>();
            var expectedList = expected!.Cast<object>();

            var highestCount = Math.Max(actualList.Count(), expectedList.Count());

            // Compare as the assertion did, so a custom comparer's matches aren't highlighted as differences.
            var comparison = AssertionComparison.Of(context);

            return HighlightDifferencesBetweenLists(actualList, expectedList, highestCount, comparison.ElementsEqual);
        }

        return actual.ToStringAwesomely();
    }

    private string HighlightDifferencesBetweenLists(IEnumerable<object> actualList, IEnumerable<object> expectedList, int highestListCount, Func<object?, object?, bool> equal)
    {
        var returnMessage = new StringBuilder("[");

        for (var listItem = 0; listItem < highestListCount; listItem++)
        {
            if (listItem >= MaxElementsToShow)
            {
                returnMessage.Append("...");
                break;
            }

            returnMessage.Append(GetComparedItemString(actualList, expectedList, listItem, equal));

            if (listItem < highestListCount - 1)
            {
                returnMessage.Append(", ");
            }
        }

        return returnMessage.Append("]").ToString();
    }

    private string? GetComparedItemString(IEnumerable<object> actualList, IEnumerable<object> expectedList, int itemPosition, Func<object?, object?, bool> equal)
    {
        if (expectedList.Count() <= itemPosition)
        {
            return ItemDifferenceHighlighter.HighlightItem(actualList.ElementAt(itemPosition).ToStringAwesomely());
        }

        if (actualList.Count() <= itemPosition)
        {
            return ItemDifferenceHighlighter.HighlightCharacter;
        }

        if (equal(actualList.ElementAt(itemPosition), expectedList.ElementAt(itemPosition)))
        {
            return actualList.ElementAt(itemPosition).ToStringAwesomely();
        }

        return ItemDifferenceHighlighter.HighlightItem(actualList.ElementAt(itemPosition).ToStringAwesomely());
    }
}