using System;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Shouldly
{
    public static class DifferenceHighlighterHelpers
    {
        private const string HIGHLIGHT_CHARACTER = "*";

        private static List<IHighlighter> _highlighters = new List<IHighlighter>() {
            new EnumerableHighlighter()
        };

        /// <summary>
        /// Compares an actual value against an expected one and creates
        /// a string with the differences highlighted
        /// </summary>
        public static string HighlightDifferencesFrom(this object actualValue, object expectedValue)
        {
            IHighlighter validHighlighter = GetHighlighterFor(expectedValue, actualValue);

            if (validHighlighter != null)
            {
                return validHighlighter.HighlightDifferences(expectedValue, actualValue);
            }
            else
            {
                return actualValue.Inspect();
            }
        }

        public static bool CanCompareAgainst(this object actual, object expected)
        {
            return GetHighlighterFor(expected, actual) != null;
        }

        private static IHighlighter GetHighlighterFor(object expected, object actual)
        {
            return (from highlighter in _highlighters
                    where highlighter.CanProcess(expected, actual)
                    select highlighter).FirstOrDefault();
        }

        private static string HighlightItem(string item)
        {
            return HIGHLIGHT_CHARACTER + item + HIGHLIGHT_CHARACTER;
        }

        private static IList ToList(this IEnumerable value)
        {
            if (value is IList)
            {
                return value.As<IList>();
            }

            ArrayList convertedList = new ArrayList();
            foreach (var item in value)
            {
                convertedList.Add(item);
            }

            return convertedList;
        }

        /// <summary>
        /// Interface for classes which can highlight differences in things
        /// </summary>
        private interface IHighlighter
        {
            bool CanProcess(object expected, object actual);
            string HighlightDifferences(object expected, object actual);
        }

        /// <summary>
        /// Highlights differences between IEnumerables of the same type,
        /// marking differences with asterisks
        /// </summary>
        private class EnumerableHighlighter : IHighlighter
        {
            private const int MAX_ELEMENTS_TO_SHOW = 1000;

            public bool CanProcess(object expected, object actual)
            {
                return (expected is IEnumerable)
                        & !(expected is string)
                        && (expected.GetType() == actual.GetType());
            }

            public string HighlightDifferences(object expected, object actual)
            {

                if (CanProcess(expected, actual))
                {
                    StringBuilder returnMessage = new StringBuilder("[");

                    var actualList = actual.As<IEnumerable>().ToList();
                    var expectedList = expected.As<IEnumerable>().ToList();

                    int highestCount = actualList.Count > expectedList.Count ? actualList.Count : expectedList.Count;

                    return HighlightDifferencesBetweenLists(actualList, expectedList, highestCount);
                }
                else
                {
                    return actual.Inspect();
                }
            }

            private static string HighlightDifferencesBetweenLists(IList actualList, IList expectedList, int highestListCount)
            {
                StringBuilder returnMessage = new StringBuilder("[");

                for (int listItem = 0; listItem < highestListCount; listItem++)
                {
                    if (listItem >= MAX_ELEMENTS_TO_SHOW)
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

            private static string GetComparedItemString(IList actualList, IList expectedList, int itemPosition)
            {
                if (expectedList.Count <= itemPosition)
                {
                    return HighlightItem(actualList[itemPosition].Inspect());
                }

                if (actualList.Count <= itemPosition)
                {
                    return HIGHLIGHT_CHARACTER;
                }

                if (Is.EqualTo(actualList[itemPosition]).Matches(expectedList[itemPosition]))
                {
                    return actualList[itemPosition].Inspect();
                }
                else
                {
                    return HighlightItem(actualList[itemPosition].Inspect());
                }
            }
        }
    }
}
