using Shouldly.Internals;

namespace Shouldly.DifferenceHighlighting;

/// <summary>
/// Highlights the differences between an actual and an expected sequence in a failure message: the actual
/// sequence's items, with each that differs from the expected one marked with asterisks.
/// </summary>
static class DifferenceHighlighter
{
    private const string HighlightCharacter = "*";

    /// <summary>
    /// The highlighted differences, or null when the two aren't sequences a message shows item by item, or when no
    /// item the message shows differs: a list with nothing marked in it would only repeat the actual value.
    /// </summary>
    public static string? Between(Differences differences) =>
        Find(differences) is { FirstIndex: not null } found
            ? Text(found)
            : null;

    /// <summary>
    /// What walking the two in step found, when they are sequences a message shows item by item. A
    /// <see cref="Memory{T}"/> is compared as a sequence but shown as a value, so it isn't highlighted.
    /// </summary>
    public static SequenceDifference? Find(Differences differences) =>
        differences.Actual is IEnumerable && differences.Expected is IEnumerable
            ? differences.InOrder
            : null;

    private static string Text(SequenceDifference found)
    {
        var items = found.Positions.Select(Format).ToList();
        if (found.CutShort)
            items.Add(StringHelpers.EnumerableTruncationMarker);

        return "[" + string.Join(", ", items) + "]";
    }

    private static string? Format(WalkedPosition position)
    {
        if (!position.HasActual)
            return HighlightCharacter;

        var formatted = position.Actual.ToStringAwesomely();
        return position.Differs ? HighlightCharacter + formatted + HighlightCharacter : formatted;
    }
}
