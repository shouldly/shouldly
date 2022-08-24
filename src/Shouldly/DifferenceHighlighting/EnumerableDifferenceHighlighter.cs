using System.Collections;
using System.Text;

namespace Shouldly.DifferenceHighlighting;

/// <summary>
/// Highlights differences between IEnumerables of the same type,
/// marking differences with asterisks
/// </summary>
internal class EnumerableDifferenceHighlighter : IDifferenceHighlighter
{
    private const int MaxElementsToShow = 1000;
    private readonly ItemDifferenceHighlighter _itemDifferenceHighlighter;

    public EnumerableDifferenceHighlighter()
    {
        _itemDifferenceHighlighter = new ItemDifferenceHighlighter();
    }

    public bool CanProcess(IShouldlyAssertionContext context)
    {
        return context is { Expected: IEnumerable and not string, Actual: IEnumerable and not string };
    }

    public string? HighlightDifferences(IShouldlyAssertionContext context)
    {
        var actual = context.Actual as IEnumerable;
        var expected = context.Expected as IEnumerable;
        if (CanProcess(context))
        {
            var actualList = actual!.Cast<object>();
            var expectedList = expected!.Cast<object>();

            var highestCount = Math.Max(actualList.Count(), expectedList.Count());

            return HighlightDifferencesBetweenLists(actualList, expectedList, highestCount);
        }

        return actual.ToStringAwesomely();
    }

    private string HighlightDifferencesBetweenLists(IEnumerable<object> actualList, IEnumerable<object> expectedList, int highestListCount)
    {
        var returnMessage = new StringBuilder("[");

        for (var listItem = 0; listItem < highestListCount; listItem++)
        {
            if (listItem >= MaxElementsToShow)
            {
                returnMessage.Append("...");
                break;
            }

            returnMessage.Append(GetComparedItemString(actualList, expectedList, listItem));

            if (listItem < highestListCount - 1)
            {
                returnMessage.Append(", ");
            }
        }

        return returnMessage.Append("]").ToString();
    }

    private string? GetComparedItemString(IEnumerable<object> actualList, IEnumerable<object> expectedList, int itemPosition)
    {
        if (expectedList.Count() <= itemPosition)
        {
            return _itemDifferenceHighlighter.HighlightItem(actualList.ElementAt(itemPosition).ToStringAwesomely());
        }

        if (actualList.Count() <= itemPosition)
        {
            return ItemDifferenceHighlighter.HighlightCharacter;
        }

        if (Is.Equal(actualList.ElementAt(itemPosition), expectedList.ElementAt(itemPosition)))
        {
            return actualList.ElementAt(itemPosition).ToStringAwesomely();
        }

        return _itemDifferenceHighlighter.HighlightItem(actualList.ElementAt(itemPosition).ToStringAwesomely());
    }
}