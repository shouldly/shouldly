using Shouldly.Internals;

namespace Shouldly.DifferenceHighlighting;

/// <summary>
/// Highlights the differences between an actual and an expected value in a failure message.
/// </summary>
static class DifferenceHighlighter
{
    /// <summary>
    /// The highlighted differences between <paramref name="actual"/> and <paramref name="expected"/>, or null when
    /// they aren't values whose differences can be highlighted.
    /// </summary>
    /// <param name="actual">The value under test.</param>
    /// <param name="expected">The value it was compared against.</param>
    /// <param name="comparison">How the assertion compared them, so that the highlighting agrees with it.</param>
    public static string? Between(object? actual, object? expected, ComparisonRecord? comparison = null) =>
        EnumerableDifferenceHighlighter.CanHighlight(actual, expected)
            ? EnumerableDifferenceHighlighter.Highlight((IEnumerable)actual!, (IEnumerable)expected!, comparison ?? ComparisonRecord.Default)
            : null;
}
