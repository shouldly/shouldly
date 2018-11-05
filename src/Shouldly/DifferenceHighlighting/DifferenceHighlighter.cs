using System.Collections.Generic;
using System.Linq;

namespace Shouldly.DifferenceHighlighting
{
    internal static class DifferenceHighlighter
    {
        static readonly List<IDifferenceHighlighter> _differenceHighlighters = new List<IDifferenceHighlighter> {
            new EnumerableDifferenceHighlighter()
        };

        /// <summary>
        /// Compares an actual value against an expected one and creates
        /// a string with the differences highlighted
        /// </summary>
        public static string HighlightDifferences(IShouldlyAssertionContext context)
        {
            var validDifferenceHighlighter = GetDifferenceHighlighterFor(context);
            return validDifferenceHighlighter == null ? context.Actual.ToStringAwesomely() : validDifferenceHighlighter.HighlightDifferences(context);
        }

        public static bool CanHighlightDifferences(IShouldlyAssertionContext context)
            => !ShouldlyConfiguration.IsDifferenceHighlightingDisabled() && GetDifferenceHighlighterFor(context) != null;

        private static IDifferenceHighlighter GetDifferenceHighlighterFor(IShouldlyAssertionContext context)
            => _differenceHighlighters.FirstOrDefault(x => x.CanProcess(context));
    }
}
