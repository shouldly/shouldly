namespace Shouldly.DifferenceHighlighting;

static class DifferenceHighlighter
{
    private static readonly List<IDifferenceHighlighter> _differenceHighlighters = [new EnumerableDifferenceHighlighter()];

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

    public static bool CanHighlightDifferences(IShouldlyAssertionContext context) =>
        GetDifferenceHighlighterFor(context) != null;

    private static IDifferenceHighlighter? GetDifferenceHighlighterFor(IShouldlyAssertionContext context) =>
        _differenceHighlighters.FirstOrDefault(x => x.CanProcess(context));
}