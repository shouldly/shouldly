namespace Shouldly.Internals.Failures;

/// <summary>
/// Where a case-sensitive string assertion would have found what it expected had it ignored case. A match that
/// differs only in case is the likeliest reason such an assertion fails, so its failure message points at it.
/// </summary>
readonly struct CaseNearMiss
{
    private CaseNearMiss(int index, string text)
    {
        Index = index;
        Text = text;
    }

    /// <summary>Where the match starts in the actual string.</summary>
    public int Index { get; }

    /// <summary>The match, as the actual string has it.</summary>
    public string Text { get; }

    /// <summary>
    /// The first match for <paramref name="expected"/> in <paramref name="actual"/> when case is ignored, for an
    /// assertion that found none with case counting. Null when case already didn't count, or there is no such match.
    /// </summary>
    public static CaseNearMiss? Within(string? actual, string? expected, Case sensitivity) =>
        sensitivity == Case.Sensitive && actual != null && expected != null &&
        actual.IndexOf(expected, StringComparison.OrdinalIgnoreCase) is >= 0 and var index
            ? new CaseNearMiss(index, actual.Substring(index, expected.Length))
            : null;

    /// <summary>
    /// The start of <paramref name="actual"/> when it starts with <paramref name="expected"/> once case is ignored.
    /// </summary>
    public static CaseNearMiss? AtStart(string? actual, string? expected, Case sensitivity) =>
        sensitivity == Case.Sensitive && actual != null && expected != null &&
        actual.StartsWith(expected, StringComparison.OrdinalIgnoreCase)
            ? new CaseNearMiss(0, actual.Substring(0, expected.Length))
            : null;

    /// <summary>
    /// The end of <paramref name="actual"/> when it ends with <paramref name="expected"/> once case is ignored.
    /// </summary>
    public static CaseNearMiss? AtEnd(string? actual, string? expected, Case sensitivity) =>
        sensitivity == Case.Sensitive && actual != null && expected != null &&
        actual.EndsWith(expected, StringComparison.OrdinalIgnoreCase)
            ? new CaseNearMiss(actual.Length - expected.Length, actual.Substring(actual.Length - expected.Length))
            : null;

    /// <summary>
    /// <paramref name="actual"/>, the string this match is in, clipped to <paramref name="length"/> characters so
    /// that the match shows: from the start when it ends within them, and otherwise centred on it. An ellipsis marks
    /// each end that was cut.
    /// </summary>
    public string Excerpt(string actual, int length)
    {
        if (Index + Text.Length <= length)
            return actual.Clip(length, "...");

        var start = Text.Length >= length
            ? Index
            : Math.Max(0, Math.Min(Index + Text.Length / 2 - length / 2, actual.Length - length));
        var end = Math.Min(actual.Length, start + length);

        return (start > 0 ? "..." : "") + actual[start..end] + (end < actual.Length ? "..." : "");
    }

    /// <summary>The lines a failure message ends with to point at the match.</summary>
    public string Word() =>
        $"""

             found differing only in case at index {Index}
         {Text.ToStringAwesomely()}
         """;
}
