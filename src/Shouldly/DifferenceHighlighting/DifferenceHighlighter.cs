using Shouldly.Internals;

namespace Shouldly.DifferenceHighlighting;

/// <summary>
/// Highlights the differences between an actual and an expected value in a failure message.
/// </summary>
static class DifferenceHighlighter
{
    /// <summary>
    /// The highlighted differences between <paramref name="actual"/> and <paramref name="expected"/>, or null when
    /// they aren't values whose differences can be highlighted, or when no item the message shows differs: a list
    /// with nothing marked in it would only repeat the actual value.
    /// </summary>
    /// <param name="actual">The value under test.</param>
    /// <param name="expected">The value it was compared against.</param>
    /// <param name="equality">
    /// How the assertion compared them, so that the highlighting agrees with it, or null when that isn't known: items
    /// are then told apart by Shouldly's own rules.
    /// </param>
    public static string? Between(object? actual, object? expected, Equality? equality) =>
        Find(actual, expected, equality) is { FirstIndex: not null } difference
            ? difference.Text
            : null;

    /// <summary>
    /// What walking <paramref name="actual"/> and <paramref name="expected"/> in step found, or null when they aren't
    /// sequences that can be walked in step: dictionaries and sets aren't compared in order, so where their
    /// items first differ in order says nothing (see <see cref="UnorderedCollection"/>).
    /// </summary>
    public static SequenceDifference? Find(object? actual, object? expected, Equality? equality) =>
        EnumerableDifferenceHighlighter.CanHighlight(actual, expected) &&
        !UnorderedCollection.IgnoresOrder(actual, expected)
            ? EnumerableDifferenceHighlighter.Find((IEnumerable)actual!, (IEnumerable)expected!, equality ?? Equality<object?>.Default)
            : null;
}
