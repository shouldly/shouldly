namespace Shouldly.DifferenceHighlighting;

class FormattedDetailedDifferenceString
{
    private readonly string _actualValue;
    private readonly string _expectedValue;
    private readonly Case _caseSensitivity;
    private readonly bool _prefixWithEllipsis;
    private readonly bool _suffixWithEllipsis;

    internal FormattedDetailedDifferenceString(
        string actualValue,
        string expectedValue,
        Case? caseSensitivity,
        bool prefixWithEllipsis = false,
        bool suffixWithEllipsis = false)
    {
        _actualValue = actualValue;
        _expectedValue = expectedValue;
        _caseSensitivity = caseSensitivity ?? Case.Sensitive;
        _prefixWithEllipsis = prefixWithEllipsis;
        _suffixWithEllipsis = suffixWithEllipsis;
    }

    public override string ToString() => GenerateFormattedString();

    public string GenerateFormattedString()
    {
        var expectedDisplay = BuildDisplayString(_expectedValue);
        var actualDisplay = BuildDisplayString(_actualValue);

        // Find diff region via prefix/suffix matching on source chars
        var commonPrefixLen = FindCommonPrefixLength();
        var commonSuffixLen = FindCommonSuffixLength(commonPrefixLen);

        var expectedDiffEnd = _expectedValue.Length - commonSuffixLen;
        var actualDiffEnd = _actualValue.Length - commonSuffixLen;

        // Map to display positions
        var displayDiffStart = ComputeDisplayOffset(commonPrefixLen);
        var expectedDisplayWidth = ComputeDisplayWidth(_expectedValue, commonPrefixLen, expectedDiffEnd);
        var actualDisplayWidth = ComputeDisplayWidth(_actualValue, commonPrefixLen, actualDiffEnd);

        var downMarker = ShouldlyConfiguration.DiffStyle == DiffStyle.Unicode ? '▼' : 'v';
        var upMarker = ShouldlyConfiguration.DiffStyle == DiffStyle.Unicode ? '▲' : '^';

        var prefix = "Expected: ";
        var markerOffset = prefix.Length + displayDiffStart;

        var sb = new StringBuilder();

        var expectedDiffLen = expectedDiffEnd - commonPrefixLen;
        var actualDiffLen = actualDiffEnd - commonPrefixLen;
        var canComparePerChar = expectedDiffLen == actualDiffLen;

        // Top markers (expected diff region)
        if (expectedDisplayWidth > 0)
        {
            sb.Append(' ', markerOffset);
            sb.AppendLine(BuildMarkerLine(downMarker, commonPrefixLen, expectedDiffEnd,
                _expectedValue, canComparePerChar ? _actualValue : null, expectedDisplayWidth));
        }

        sb.AppendLine($"{prefix}{expectedDisplay}");
        sb.Append($"Actual:   {actualDisplay}");

        // Bottom markers (actual diff region)
        if (actualDisplayWidth > 0)
        {
            sb.AppendLine();
            sb.Append(' ', markerOffset);
            sb.Append(BuildMarkerLine(upMarker, commonPrefixLen, actualDiffEnd,
                _actualValue, canComparePerChar ? _expectedValue : null, actualDisplayWidth));
        }

        return sb.ToString();
    }

    private string BuildDisplayString(string value)
    {
        var sb = new StringBuilder();

        if (_prefixWithEllipsis)
            sb.Append("...");

        sb.Append('"');

        foreach (var c in value)
        {
            if (c.NeedsEscaping())
                sb.Append(c.ToSafeString());
            else
                sb.Append(c);
        }

        sb.Append('"');

        if (_suffixWithEllipsis)
            sb.Append("...");

        return sb.ToString();
    }

    private int ComputeDisplayOffset(int sourceIndex)
    {
        // Account for optional "..." prefix and opening quote
        var offset = _prefixWithEllipsis ? 4 : 1;

        // Use expected for offset computation (prefix is identical in both strings)
        var value = _expectedValue.Length > 0 ? _expectedValue : _actualValue;
        var len = Math.Min(sourceIndex, value.Length);

        for (var i = 0; i < len; i++)
        {
            offset += CharDisplayWidth(value[i]);
        }

        return offset;
    }

    private static int ComputeDisplayWidth(string value, int startIndex, int endIndex)
    {
        var width = 0;
        for (var i = startIndex; i < endIndex && i < value.Length; i++)
        {
            width += CharDisplayWidth(value[i]);
        }
        return width;
    }

    private static int CharDisplayWidth(char c) =>
        c.NeedsEscaping() ? c.ToSafeString().Length : 1;

    private string BuildMarkerLine(char marker, int startIndex, int endIndex, string value, string? otherValue, int totalWidth)
    {
        if (otherValue == null)
            return new string(marker, totalWidth);

        var markerSb = new StringBuilder(totalWidth);
        for (var i = startIndex; i < endIndex; i++)
        {
            var w = CharDisplayWidth(value[i]);
            if (!CharsEqual(value[i], otherValue[i]))
                markerSb.Append(marker, w);
            else
                markerSb.Append(' ', w);
        }

        // Trim trailing spaces
        while (markerSb.Length > 0 && markerSb[markerSb.Length - 1] == ' ')
            markerSb.Length--;

        return markerSb.ToString();
    }

    private int FindCommonPrefixLength()
    {
        var minLen = Math.Min(_expectedValue.Length, _actualValue.Length);
        for (var i = 0; i < minLen; i++)
        {
            if (!CharsEqual(_expectedValue[i], _actualValue[i]))
                return i;
        }
        return minLen;
    }

    private int FindCommonSuffixLength(int commonPrefixLength)
    {
        var maxSuffixLen = Math.Min(_expectedValue.Length, _actualValue.Length) - commonPrefixLength;
        for (var i = 0; i < maxSuffixLen; i++)
        {
            if (!CharsEqual(
                    _expectedValue[_expectedValue.Length - 1 - i],
                    _actualValue[_actualValue.Length - 1 - i]))
                return i;
        }
        return maxSuffixLen;
    }

    private bool CharsEqual(char a, char b)
    {
        if (_caseSensitivity == Case.Insensitive)
            return StringComparer.OrdinalIgnoreCase.Equals(a.ToString(), b.ToString());
        return a == b;
    }
}
