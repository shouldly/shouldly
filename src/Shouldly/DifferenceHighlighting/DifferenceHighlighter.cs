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
        public static string? HighlightDifferences(IShouldlyAssertionContext context)
        {
            var validDifferenceHighlighter = GetDifferenceHighlighterFor(context);

            if (validDifferenceHighlighter == null)
            {
                return context.Actual.ToStringAwesomely();
            }

            return validDifferenceHighlighter.HighlightDifferences(context);
        }

        public static bool CanHighlightDifferences(IShouldlyAssertionContext context)
        {
            return GetDifferenceHighlighterFor(context) != null;
        }

        static IDifferenceHighlighter? GetDifferenceHighlighterFor(IShouldlyAssertionContext context)
        {
            return _differenceHighlighters.FirstOrDefault(x => x.CanProcess(context));
        }
    }
}
