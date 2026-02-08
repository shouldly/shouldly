namespace Shouldly.DifferenceHighlighting;

class LineDiffFormatter
{
    private const int MaxContextLines = 2;

    private readonly Case _caseSensitivity;

    public LineDiffFormatter(Case caseSensitivity)
    {
        _caseSensitivity = caseSensitivity;
    }

    private const string ExpectedPrefix = "Expected: ";
    private const string ActualPrefix =   "Actual:   ";
    private static readonly string ContextPrefix = new(' ', ExpectedPrefix.Length);

    public string FormatLineDiff(string expected, string actual)
    {
        var expectedLines = expected.Split('\n');
        var actualLines = actual.Split('\n');

        // Find common prefix/suffix lines
        var commonPrefixLines = FindCommonPrefixLineCount(expectedLines, actualLines);
        var commonSuffixLines = FindCommonSuffixLineCount(expectedLines, actualLines, commonPrefixLines);

        var expectedChangeStart = commonPrefixLines;
        var expectedChangeEnd = expectedLines.Length - commonSuffixLines;
        var actualChangeStart = commonPrefixLines;
        var actualChangeEnd = actualLines.Length - commonSuffixLines;

        var sb = new StringBuilder();

        var downMarker = ShouldlyConfiguration.DiffStyle == DiffStyle.Unicode ? '▼' : 'v';
        var upMarker = ShouldlyConfiguration.DiffStyle == DiffStyle.Unicode ? '▲' : '^';

        // Pre-compute character-level marker position when exactly one line changed
        var removedCount = expectedChangeEnd - expectedChangeStart;
        var addedCount = actualChangeEnd - actualChangeStart;
        var charDiffPos = removedCount == 1 && addedCount == 1
            ? FindFirstCharDifference(expectedLines[expectedChangeStart], actualLines[actualChangeStart])
            : -1;

        // Leading context
        var contextStart = Math.Max(0, commonPrefixLines - MaxContextLines);
        if (contextStart > 0)
            sb.AppendLine($"{ContextPrefix}...");

        for (var i = contextStart; i < commonPrefixLines; i++)
        {
            sb.AppendLine($"{ContextPrefix}{DisplayLine(expectedLines[i])}");
        }

        // Down marker above expected line
        if (charDiffPos >= 0)
        {
            sb.Append(' ', charDiffPos + ExpectedPrefix.Length);
            sb.AppendLine(downMarker.ToString());
        }

        // Expected lines (removed from expected)
        for (var i = expectedChangeStart; i < expectedChangeEnd; i++)
        {
            sb.AppendLine($"{ExpectedPrefix}{DisplayLine(expectedLines[i])}");
        }

        // Actual lines (added in actual)
        for (var i = actualChangeStart; i < actualChangeEnd; i++)
        {
            sb.AppendLine($"{ActualPrefix}{DisplayLine(actualLines[i])}");
        }

        // Up marker below actual line
        if (charDiffPos >= 0)
        {
            sb.Append(' ', charDiffPos + ActualPrefix.Length);
            sb.AppendLine(upMarker.ToString());
        }

        // Trailing context
        var contextEnd = Math.Min(expectedLines.Length, expectedChangeEnd + MaxContextLines);
        for (var i = expectedChangeEnd; i < contextEnd; i++)
        {
            sb.AppendLine($"{ContextPrefix}{DisplayLine(expectedLines[i])}");
        }

        if (contextEnd < expectedLines.Length)
            sb.AppendLine($"{ContextPrefix}...");

        // Remove final newline
        if (sb.Length > 0 && sb[^1] == '\n')
            sb.Length -= Environment.NewLine.Length;

        return sb.ToString();
    }

    private static string DisplayLine(string line)
    {
        // Show trailing \r explicitly since it's invisible
        if (line.Length > 0 && line[line.Length - 1] == '\r')
            return line[..^1] + "\\r";
        return line;
    }

    private int FindFirstCharDifference(string a, string b)
    {
        a = a.TrimEnd('\r');
        b = b.TrimEnd('\r');

        var minLen = Math.Min(a.Length, b.Length);
        for (var i = 0; i < minLen; i++)
        {
            if (!CharsEqual(a[i], b[i]))
                return i;
        }
        if (a.Length != b.Length)
            return minLen;
        return -1;
    }

    private int FindCommonPrefixLineCount(string[] expected, string[] actual)
    {
        var minLen = Math.Min(expected.Length, actual.Length);
        for (var i = 0; i < minLen; i++)
        {
            if (!LinesEqual(expected[i], actual[i]))
                return i;
        }
        return minLen;
    }

    private int FindCommonSuffixLineCount(string[] expected, string[] actual, int prefixCount)
    {
        var maxSuffix = Math.Min(expected.Length, actual.Length) - prefixCount;
        for (var i = 0; i < maxSuffix; i++)
        {
            if (!LinesEqual(
                    expected[expected.Length - 1 - i],
                    actual[actual.Length - 1 - i]))
                return i;
        }
        return maxSuffix;
    }

    private bool LinesEqual(string a, string b)
    {
        a = a.TrimEnd('\r');
        b = b.TrimEnd('\r');

        if (_caseSensitivity == Case.Insensitive)
            return StringComparer.OrdinalIgnoreCase.Equals(a, b);
        return StringComparer.Ordinal.Equals(a, b);
    }

    private bool CharsEqual(char a, char b)
    {
        if (_caseSensitivity == Case.Insensitive)
            return StringComparer.OrdinalIgnoreCase.Equals(a.ToString(), b.ToString());
        return a == b;
    }
}
