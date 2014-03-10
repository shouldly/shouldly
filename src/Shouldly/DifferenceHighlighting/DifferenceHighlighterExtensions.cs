using System.Collections.Generic;
using System.Linq;

namespace Shouldly.DifferenceHighlighting
{
    internal static class DifferenceHighlighterExtensions
    {
        public static readonly List<IHighlighter> Highlighters = new List<IHighlighter> {
            new EnumerableHighlighter()
        };

        /// <summary>
        /// Compares an actual value against an expected one and creates
        /// a string with the differences highlighted
        /// </summary>
        public static string HighlightDifferencesBetween(this object actualValue, object expectedValue)
        {
            var validHighlighter = GetHighlighterFor(expectedValue, actualValue);

            if (validHighlighter == null)
            {
                return actualValue.Inspect();
            }

            return validHighlighter.HighlightDifferences(expectedValue, actualValue);
        }

        public static bool CanGenerateDifferencesBetween<T1, T2>(this T1 actual, T2 expected)
        {
            return GetHighlighterFor(expected, actual) != null;
        }

        public static IHighlighter GetHighlighterFor<T1, T2>(T1 expected, T2 actual)
        {
            return Highlighters.FirstOrDefault(x => x.CanProcess(expected, actual));
        }
    }
}
