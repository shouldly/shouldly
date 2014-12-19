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
        public static string HighlightDifferencesBetween(ITestEnvironment testEnvironment)
        {
            var validHighlighter = GetHighlighterFor(testEnvironment);

            if (validHighlighter == null)
            {
                return testEnvironment.Actual.Inspect();
            }

            return validHighlighter.HighlightDifferences(testEnvironment);
        }

        public static bool CanGenerateDifferencesBetween(ITestEnvironment testEnvironment)
        {
            return GetHighlighterFor(testEnvironment) != null;
        }

        private static IHighlighter GetHighlighterFor(ITestEnvironment testEnvironment)
        {
            return Highlighters.FirstOrDefault(x => x.CanProcess(testEnvironment));
        }
    }
}
