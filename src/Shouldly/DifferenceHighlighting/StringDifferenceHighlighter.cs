namespace Shouldly.DifferenceHighlighting;

class StringDifferenceHighlighter : IStringDifferenceHighlighter
{
    private const int MaxContextChars = 20;
    private const int MaxDisplayLength = 60;
    private const int MaxDiffRegions = 3;

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

        // Mode selection
        if (expected.Contains('\n') && actual.Contains('\n'))
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

    private string FormatCharacterMode(string expected, string actual)
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
            var shortHint = DetectSmartHint(expected, actual);
            if (shortHint != null)
                result += Environment.NewLine + shortHint;
            return result;
        }

        // For long strings, find individual diff positions within the diff region
        var diffPositions = FindDiffPositions(expected, actual, commonPrefix, commonSuffix);
        var regions = ConsolidateDiffPositions(diffPositions);

        if (regions.Count == 0) return "";

        var output = new StringBuilder();

        var totalDiffs = regions.Count;
        var showCount = Math.Min(totalDiffs, MaxDiffRegions);

        if (totalDiffs > 1)
            output.AppendLine($"{totalDiffs} difference{(totalDiffs > 1 ? "s" : "")}");

        for (var i = 0; i < showCount; i++)
        {
            var region = regions[i];
            var (windowExpected, windowActual, prefixEllipsis, suffixEllipsis) =
                ExtractContextWindow(expected, actual, region.Start, region.End, maxLen);

            if (i > 0)
            {
                output.AppendLine();
                output.AppendLine();
            }

            if (totalDiffs > 1)
                output.AppendLine($"[{i + 1}] at index {region.Start}:");

            var diff = new FormattedDetailedDifferenceString(
                windowActual, windowExpected, _sensitivity,
                prefixEllipsis, suffixEllipsis);

            var formatted = diff.GenerateFormattedString();
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

    private List<int> FindDiffPositions(string expected, string actual, int commonPrefix, int commonSuffix)
    {
        var positions = new List<int>();
        var expectedEnd = expected.Length - commonSuffix;
        var actualEnd = actual.Length - commonSuffix;
        var maxEnd = Math.Max(expectedEnd, actualEnd);

        for (var i = commonPrefix; i < maxEnd; i++)
        {
            if (!CharAtIndexIsEqual(expected, actual, i))
                positions.Add(i);
        }

        return positions;
    }

    private static List<DiffRegion> ConsolidateDiffPositions(List<int> positions)
    {
        if (positions.Count == 0) return [];

        var regions = new List<DiffRegion>();
        var start = positions[0];
        var end = positions[0];

        for (var i = 1; i < positions.Count; i++)
        {
            if (positions[i] - end <= 5) // Merge nearby diffs
            {
                end = positions[i];
            }
            else
            {
                regions.Add(new DiffRegion(start, end));
                start = positions[i];
                end = positions[i];
            }
        }

        regions.Add(new DiffRegion(start, end));
        return regions;
    }

    private static (string expected, string actual, bool prefixEllipsis, bool suffixEllipsis)
        ExtractContextWindow(string expected, string actual, int diffStart, int diffEnd, int maxLen)
    {
        var windowStart = Math.Max(0, diffStart - MaxContextChars);
        var windowEnd = Math.Min(maxLen, diffEnd + MaxContextChars + 1);

        var expectedWindow = SafeSubstring(expected, windowStart, windowEnd);
        var actualWindow = SafeSubstring(actual, windowStart, windowEnd);

        return (expectedWindow, actualWindow, windowStart > 0, windowEnd < maxLen);
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

    private static string? DetectSmartHint(string expected, string actual)
    {
        // Check for CRLF vs LF difference
        var expectedHasCrlf = expected.Contains("\r\n");
        var actualHasCrlf = actual.Contains("\r\n");
        var expectedHasLf = expected.Contains('\n') && !expectedHasCrlf;
        var actualHasLf = actual.Contains('\n') && !actualHasCrlf;

        // More nuanced: check if after normalizing line endings the strings are equal
        var normalizedExpected = expected.Replace("\r\n", "\n");
        var normalizedActual = actual.Replace("\r\n", "\n");

        if (expectedHasCrlf && !actualHasCrlf && normalizedExpected == normalizedActual)
            return "Line endings differ: expected uses CRLF (\\r\\n), actual uses LF (\\n)";

        if (!expectedHasCrlf && actualHasCrlf && normalizedExpected == normalizedActual)
            return "Line endings differ: expected uses LF (\\n), actual uses CRLF (\\r\\n)";

        // Check for tab vs space difference
        if (expected.Contains('\t') && !actual.Contains('\t') && actual.Contains(' '))
        {
            var tabNormalized = expected.Replace("\t", "    ");
            if (tabNormalized == actual || expected.Replace('\t', ' ') == actual.Replace('\t', ' '))
                return "Whitespace differs: expected uses tab (\\t), actual uses spaces";
        }

        if (!expected.Contains('\t') && actual.Contains('\t') && expected.Contains(' '))
        {
            var tabNormalized = actual.Replace("\t", "    ");
            if (tabNormalized == expected || expected.Replace('\t', ' ') == actual.Replace('\t', ' '))
                return "Whitespace differs: expected uses spaces, actual uses tab (\\t)";
        }

        return null;
    }

    private bool CharAtIndexIsEqual(string expected, string actual, int index)
    {
        if (index >= expected.Length || index >= actual.Length)
            return false;

        if (_sensitivity == Case.Insensitive)
            return StringComparer.OrdinalIgnoreCase.Equals(
                expected[index].ToString(), actual[index].ToString());

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
            if (_sensitivity == Case.Insensitive
                ? !StringComparer.OrdinalIgnoreCase.Equals(
                    expected[expected.Length - 1 - i].ToString(),
                    actual[actual.Length - 1 - i].ToString())
                : expected[expected.Length - 1 - i] != actual[actual.Length - 1 - i])
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
