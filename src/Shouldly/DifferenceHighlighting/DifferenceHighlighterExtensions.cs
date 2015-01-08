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
        public static string HighlightDifferencesBetween(IShouldlyAssertionContext context)
        {
            var validHighlighter = GetHighlighterFor(context);

            if (validHighlighter == null)
            {
                return context.Actual.Inspect();
            }

            return validHighlighter.HighlightDifferences(context);
        }

        public static bool CanGenerateDifferencesBetween(IShouldlyAssertionContext context)
        {
            return GetHighlighterFor(context) != null;
        }

        private static IHighlighter GetHighlighterFor(IShouldlyAssertionContext context)
        {
            return Highlighters.FirstOrDefault(x => x.CanProcess(context));
        }
    }
}
