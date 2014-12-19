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

        public bool CanProcess(ITestEnvironment testEnvironment)
        {
            return testEnvironment.Expected != null && testEnvironment.Actual != null
                   && (testEnvironment.Expected is IEnumerable)
                   && !(testEnvironment.Expected is string)
                   && (testEnvironment.Actual is IEnumerable)
                   && !(testEnvironment.Actual is string);
        }

        public string HighlightDifferences(ITestEnvironment testEnvironment)
        {
            var actual = testEnvironment.Actual as IEnumerable;
            var expected = testEnvironment.Expected as IEnumerable;
            if (CanProcess(testEnvironment))
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