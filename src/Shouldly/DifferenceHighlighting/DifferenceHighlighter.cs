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

    /// <summary>
    /// The highlighted differences between <paramref name="actual"/> and <paramref name="expected"/>, or null when
    /// they aren't values whose differences can be highlighted.
    /// </summary>
    /// <param name="actual">The value under test.</param>
    /// <param name="expected">The value it was compared against.</param>
    /// <param name="comparison">What the assertion established comparing them, so that the highlighting agrees with it.</param>
    public static string? Between(object? actual, object? expected, Internals.ComparisonRecord? comparison = null)
    {
        var context = new ShouldlyAssertionContext(string.Empty, expected, actual, actualExpression: string.Empty)
        {
            Comparison = comparison
        };
        return CanHighlightDifferences(context) ? HighlightDifferences(context) : null;
    }

    public static bool CanHighlightDifferences(IShouldlyAssertionContext context) =>
        GetDifferenceHighlighterFor(context) != null;

    private static IDifferenceHighlighter? GetDifferenceHighlighterFor(IShouldlyAssertionContext context) =>
        _differenceHighlighters.FirstOrDefault(x => x.CanProcess(context));
}