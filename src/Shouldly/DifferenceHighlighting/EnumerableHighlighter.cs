using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shouldly.DifferenceHighlighting
{
    /// <summary>
    /// Highlights differences between IEnumerables of the same type,
    /// marking differences with asterisks
    /// </summary>
    internal class EnumerableHighlighter : IHighlighter
    {
        private const int MaxElementsToShow = 1000;
        private readonly DifferenceHighlighter _differenceHighlighter;

        public EnumerableHighlighter(DifferenceHighlighter differenceHighlighter)
        {
            _differenceHighlighter = differenceHighlighter;
        }

        public EnumerableHighlighter() : this(new DifferenceHighlighter())
        {
        }

        public bool CanProcess(IShouldlyAssertionContext context)
        {
            return context.Expected != null && context.Actual != null
                   && (context.Expected is IEnumerable)
                   && !(context.Expected is string)
                   && (context.Actual is IEnumerable)
                   && !(context.Actual is string);
        }

        public string HighlightDifferences(IShouldlyAssertionContext context)
        {
            var actual = context.Actual as IEnumerable;
            var expected = context.Expected as IEnumerable;
            if (CanProcess(context))
            {
                var actualList = actual.Cast<object>();
                var expectedList = expected.Cast<object>();

                var highestCount = actualList.Count() > expectedList.Count() ? actualList.Count() : expectedList.Count();

                return HighlightDifferencesBetweenLists(actualList, expectedList, highestCount);
            }

            return actual.Inspect();
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

        private string GetComparedItemString(IEnumerable<object> actualList, IEnumerable<object> expectedList, int itemPosition)
        {
            if (expectedList.Count() <= itemPosition)
            {
                return _differenceHighlighter.HighlightItem(actualList.ElementAt(itemPosition).Inspect());
            }

            if (actualList.Count() <= itemPosition)
            {
                return DifferenceHighlighter.HighlightCharacter;
            }

            if (Is.Equal(actualList.ElementAt(itemPosition), expectedList.ElementAt(itemPosition)))
            {
                return actualList.ElementAt(itemPosition).Inspect();
            }

            return _differenceHighlighter.HighlightItem(actualList.ElementAt(itemPosition).Inspect());
        }
    }
}