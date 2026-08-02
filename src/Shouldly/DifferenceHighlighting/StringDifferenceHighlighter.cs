namespace Shouldly.DifferenceHighlighting;

class StringDifferenceHighlighter : IStringDifferenceHighlighter
{
    private const int MaxContextChars = 20;
    private const int MaxDisplayLength = 60;
    private const int MaxDiffRegions = 3;
    private const int MaxHintScanLength = 100_000;

    // Line mode splits both whole strings into per-line arrays before diffing, so its memory
    // cost is proportional to the input. Past this size fall back to character mode, which walks
    // the differing span in O(1) memory, rather than allocating line arrays for a multi-MB value.
    private const int MaxLineModeLength = 100_000;

    private readonly Case _sensitivity;
    private readonly Func<string, string> _transform;

    public StringDifferenceHighlighter(Case sensitivity, Func<string, string>? transform = null)
    {
        _sensitivity = sensitivity;
        _transform = transform ?? (s => s);
    }

    public string? HighlightDifferences(string? expected, string? actual)
    {
        if (expected == null || actual == null) return null;

        expected = _transform(expected);
        actual = _transform(actual);

        if (StringsEqual(expected, actual)) return null;

        // Mode selection: prefer line mode when both sides are multi-line, or when either
        // is multi-line and the strings are too long for char-mode markers to be readable.
        var expectedHasNewline = expected.Contains('\n');
        var actualHasNewline = actual.Contains('\n');
        var maxLen = Math.Max(expected.Length, actual.Length);

        if (maxLen <= MaxLineModeLength
            && ((expectedHasNewline && actualHasNewline)
                || ((expectedHasNewline || actualHasNewline) && maxLen > MaxDisplayLength)))
            return FormatLineMode(expected, actual);

        return FormatCharacterMode(expected, actual);
    }

    private string FormatLineMode(string expected, string actual)
    {
        var formatter = new LineDiffFormatter(_sensitivity);
        var result = formatter.FormatLineDiff(expected, actual);
        var hint = DetectSmartHint(expected, actual);
        if (hint != null)
            result += Environment.NewLine + hint;
        return result;
    }

    private string? FormatCharacterMode(string expected, string actual)
    {
        // Find the diff region using prefix/suffix matching
        var commonPrefix = FindCommonPrefixLength(expected, actual);
        var commonSuffix = FindCommonSuffixLength(expected, actual, commonPrefix);

        var maxLen = Math.Max(expected.Length, actual.Length);

        // If both strings are short enough, show them fully
        if (maxLen <= MaxDisplayLength)
        {
            var diff = new FormattedDetailedDifferenceString(
                actual, expected, _sensitivity);
            var result = diff.GenerateFormattedString();
            if (result == null) return null;
            var shortHint = DetectSmartHint(expected, actual);
            if (shortHint != null)
                result += Environment.NewLine + shortHint;
            return result;
        }

        // For long strings, find individual diff regions within the diff span
        var (regions, totalDiffs) = FindDiffRegions(expected, actual, commonPrefix, commonSuffix);

        if (totalDiffs == 0) return "";

        var output = new StringBuilder();

        var showCount = regions.Count;

        if (totalDiffs > 1)
            output.AppendLine($"{totalDiffs} differences");

        for (var i = 0; i < showCount; i++)
        {
            var region = regions[i];
            var window = ExtractContextWindow(expected, actual, region.Start, region.End);

            if (i > 0)
            {
                output.AppendLine();
                output.AppendLine();
            }

            if (totalDiffs > 1)
                output.AppendLine($"[{i + 1}] at index {region.Start}:");

            var diff = new FormattedDetailedDifferenceString(
                window.Actual, window.Expected, _sensitivity,
                window.ExpectedPrefixEllipsis, window.ExpectedSuffixEllipsis,
                window.ActualPrefixEllipsis, window.ActualSuffixEllipsis);

            var formatted = diff.GenerateFormattedString();
            if (formatted == null) continue;
            if (totalDiffs > 1)
                formatted = IndentLines(formatted, "    ");

            output.Append(formatted);
        }

        if (totalDiffs > showCount)
        {
            output.AppendLine();
            output.Append($"  ... and {totalDiffs - showCount} more difference(s)");
        }

        var hint = DetectSmartHint(expected, actual);
        if (hint != null)
        {
            output.AppendLine();
            output.Append(hint);
        }

        return output.ToString();
    }

    // Walks the differing span once, consolidating adjacent positions into regions as it goes.
    // Only the first MaxDiffRegions regions are retained — the rest are counted and dropped — so
    // comparing two entirely different multi-megabyte strings stays O(1) in memory.
    private (List<DiffRegion> Regions, int Total) FindDiffRegions(string expected, string actual, int commonPrefix, int commonSuffix)
    {
        var regions = new List<DiffRegion>();
        var total = 0;
        var expectedEnd = expected.Length - commonSuffix;
        var actualEnd = actual.Length - commonSuffix;
        var maxEnd = Math.Max(expectedEnd, actualEnd);

        var start = -1;
        var end = -1;

        void Close()
        {
            total++;
            if (regions.Count < MaxDiffRegions)
                regions.Add(new DiffRegion(start, end));
        }

        for (var i = commonPrefix; i < maxEnd; i++)
        {
            if (CharAtIndexIsEqual(expected, actual, i))
                continue;

            if (start < 0)
            {
                start = end = i;
            }
            else if (i - end <= 5) // Merge nearby diffs
            {
                end = i;
            }
            else
            {
                Close();
                start = end = i;
            }
        }

        if (start >= 0)
            Close();

        return (regions, total);
    }

    private static ContextWindow ExtractContextWindow(string expected, string actual, int diffStart, int diffEnd)
    {
        var maxLen = Math.Max(expected.Length, actual.Length);
        var windowStart = Math.Max(0, diffStart - MaxContextChars);
        var windowEnd = Math.Min(maxLen, diffEnd + MaxContextChars + 1);

        // Cap the extracted window so a very wide diff region (e.g. an entirely
        // different long string) can't bypass MaxDisplayLength and bloat the message.
        const int maxWindowSize = MaxDisplayLength + 2 * MaxContextChars;
        if (windowEnd - windowStart > maxWindowSize)
            windowEnd = windowStart + maxWindowSize;

        var expectedWindow = SafeSubstring(expected, windowStart, windowEnd);
        var actualWindow = SafeSubstring(actual, windowStart, windowEnd);

        // Per-side ellipsis: only mark a side as truncated when its own length
        // extends past the window — otherwise short sides get a misleading "...".
        return new ContextWindow(
            expectedWindow,
            actualWindow,
            ExpectedPrefixEllipsis: windowStart > 0 && expectedWindow.Length > 0,
            ActualPrefixEllipsis: windowStart > 0 && actualWindow.Length > 0,
            ExpectedSuffixEllipsis: windowEnd < expected.Length,
            ActualSuffixEllipsis: windowEnd < actual.Length);
    }

    private readonly struct ContextWindow
    {
        public string Expected { get; }
        public string Actual { get; }
        public bool ExpectedPrefixEllipsis { get; }
        public bool ActualPrefixEllipsis { get; }
        public bool ExpectedSuffixEllipsis { get; }
        public bool ActualSuffixEllipsis { get; }

        public ContextWindow(string Expected, string Actual,
            bool ExpectedPrefixEllipsis, bool ActualPrefixEllipsis,
            bool ExpectedSuffixEllipsis, bool ActualSuffixEllipsis)
        {
            this.Expected = Expected;
            this.Actual = Actual;
            this.ExpectedPrefixEllipsis = ExpectedPrefixEllipsis;
            this.ActualPrefixEllipsis = ActualPrefixEllipsis;
            this.ExpectedSuffixEllipsis = ExpectedSuffixEllipsis;
            this.ActualSuffixEllipsis = ActualSuffixEllipsis;
        }
    }

    private static string SafeSubstring(string value, int start, int end)
    {
        if (start >= value.Length) return "";
        var actualEnd = Math.Min(end, value.Length);
        return value[start..actualEnd];
    }

    private static string IndentLines(string text, string indent)
    {
        var lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        return string.Join(Environment.NewLine, lines.Select(l => indent + l));
    }

    private string? DetectSmartHint(string expected, string actual)
    {
        // Every check below needs a whole-string copy (line-ending normalisation, tab expansion).
        // Past this size those allocations cost more than the hint is worth.
        if (Math.Max(expected.Length, actual.Length) > MaxHintScanLength)
            return null;

        var comparer = _sensitivity == Case.Insensitive
            ? StringComparer.OrdinalIgnoreCase
            : StringComparer.Ordinal;

        // Check for CRLF vs LF difference
        var expectedHasCrlf = expected.Contains("\r\n");
        var actualHasCrlf = actual.Contains("\r\n");

        // More nuanced: check if after normalizing line endings the strings are equal
        var normalizedExpected = expected.NormalizeLineEndings()!;
        var normalizedActual = actual.NormalizeLineEndings()!;

        if (expectedHasCrlf && !actualHasCrlf && comparer.Equals(normalizedExpected, normalizedActual))
            return "Line endings differ: expected uses CRLF (\\r\\n), actual uses LF (\\n)";

        if (!expectedHasCrlf && actualHasCrlf && comparer.Equals(normalizedExpected, normalizedActual))
            return "Line endings differ: expected uses LF (\\n), actual uses CRLF (\\r\\n)";

        // Check for tab vs space difference
        if (expected.Contains('\t') && !actual.Contains('\t') && actual.Contains(' '))
        {
            var tabNormalized = expected.Replace("\t", "    ");
            if (comparer.Equals(tabNormalized, actual)
                || comparer.Equals(expected.Replace('\t', ' '), actual.Replace('\t', ' ')))
                return "Whitespace differs: expected uses tab (\\t), actual uses spaces";
        }

        if (!expected.Contains('\t') && actual.Contains('\t') && expected.Contains(' '))
        {
            var tabNormalized = actual.Replace("\t", "    ");
            if (comparer.Equals(tabNormalized, expected)
                || comparer.Equals(expected.Replace('\t', ' '), actual.Replace('\t', ' ')))
                return "Whitespace differs: expected uses spaces, actual uses tab (\\t)";
        }

        return null;
    }

    private bool CharAtIndexIsEqual(string expected, string actual, int index)
    {
        if (index >= expected.Length || index >= actual.Length)
            return false;

        if (_sensitivity == Case.Insensitive)
            return char.ToUpperInvariant(expected[index]) == char.ToUpperInvariant(actual[index]);

        return expected[index] == actual[index];
    }

    private bool StringsEqual(string expected, string actual)
    {
        if (_sensitivity == Case.Insensitive)
            return StringComparer.OrdinalIgnoreCase.Equals(expected, actual);
        return expected == actual;
    }

    private int FindCommonPrefixLength(string expected, string actual)
    {
        var minLen = Math.Min(expected.Length, actual.Length);
        for (var i = 0; i < minLen; i++)
        {
            if (!CharAtIndexIsEqual(expected, actual, i))
                return i;
        }
        return minLen;
    }

    private int FindCommonSuffixLength(string expected, string actual, int commonPrefix)
    {
        var maxSuffix = Math.Min(expected.Length, actual.Length) - commonPrefix;
        for (var i = 0; i < maxSuffix; i++)
        {
            var ec = expected[expected.Length - 1 - i];
            var ac = actual[actual.Length - 1 - i];
            var equal = _sensitivity == Case.Insensitive
                ? char.ToUpperInvariant(ec) == char.ToUpperInvariant(ac)
                : ec == ac;
            if (!equal)
                return i;
        }
        return maxSuffix;
    }

    private class DiffRegion
    {
        public int Start { get; }
        public int End { get; }

        public DiffRegion(int start, int end)
        {
            Start = start;
            End = end;
        }
    }
}
