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

    public override string? ToString() => GenerateFormattedString();

    public string? GenerateFormattedString()
    {
        var expectedDisplay = BuildDisplayString(_expectedValue);
        var actualDisplay = BuildDisplayString(_actualValue);

        // Detect collision: display strings look identical but source strings differ
        // This happens when one has real control chars and the other has literal escape text
        if (expectedDisplay == actualDisplay && _expectedValue != _actualValue)
        {
            var expectedHasControlChars = _expectedValue.Any(c => c.NeedsEscaping());
            var actualHasControlChars = _actualValue.Any(c => c.NeedsEscaping());

            // Use ASCII-safe descriptive names when DiffStyle is Ascii, otherwise Unicode control pictures
            var fallbackEscape = ShouldlyConfiguration.DiffStyle == DiffStyle.Ascii
                ? EscapeStyle.Descriptive
                : EscapeStyle.ControlPictures;

            if (expectedHasControlChars && !actualHasControlChars)
                expectedDisplay = BuildDisplayString(_expectedValue, fallbackEscape);
            else if (actualHasControlChars && !expectedHasControlChars)
                actualDisplay = BuildDisplayString(_actualValue, fallbackEscape);
            else if (expectedHasControlChars && actualHasControlChars)
            {
                expectedDisplay = BuildDisplayString(_expectedValue, fallbackEscape);
                actualDisplay = BuildDisplayString(_actualValue, fallbackEscape);
            }
        }

        // Split into grapheme clusters for alignment
        var expectedClusters = GraphemeClusterHelper.GetGraphemeClusters(_expectedValue);
        var actualClusters = GraphemeClusterHelper.GetGraphemeClusters(_actualValue);

        // Find diff region via prefix/suffix matching on clusters
        var commonPrefixLen = FindCommonClusterPrefixLength(expectedClusters, actualClusters);
        var commonSuffixLen = FindCommonClusterSuffixLength(expectedClusters, actualClusters, commonPrefixLen);

        var expectedDiffEnd = expectedClusters.Length - commonSuffixLen;
        var actualDiffEnd = actualClusters.Length - commonSuffixLen;

        // Extract diff region clusters
        var expectedDiffClusters = SubArray(expectedClusters, commonPrefixLen, expectedDiffEnd);
        var actualDiffClusters = SubArray(actualClusters, commonPrefixLen, actualDiffEnd);

        // Run alignment on grapheme clusters
        var (expectedEdits, actualEdits) = EditDistanceAligner.AlignClusters(
            expectedDiffClusters, actualDiffClusters, _caseSensitivity);

        // Suppress markers when alignment was skipped (too large) or strings are too different
        var showMarkers = false;
        if (expectedEdits != null && actualEdits != null)
        {
            var totalClusters = expectedClusters.Length + actualClusters.Length;
            var editCount = CountEdits(expectedEdits) + CountEdits(actualEdits);
            showMarkers = totalClusters > 0 && editCount * 2 <= totalClusters;
        }

        var sb = new StringBuilder();
        var prefix = "Expected: ";

        if (showMarkers)
        {
            // Compute display offset to the start of the diff region
            var displayDiffStart = ComputeDisplayOffset(expectedClusters, commonPrefixLen);

            var downMarker = ShouldlyConfiguration.DiffStyle == DiffStyle.Unicode ? '▼' : 'v';
            var upMarker = ShouldlyConfiguration.DiffStyle == DiffStyle.Unicode ? '▲' : '^';

            var markerOffset = prefix.Length + displayDiffStart;

            // Top markers (expected edits) — one marker per display column of each edited cluster
            var topMarkers = BuildClusterMarkerLine(downMarker, expectedDiffClusters, expectedEdits!);
            if (topMarkers.Length > 0)
            {
                sb.Append(' ', markerOffset);
                sb.AppendLine(topMarkers);
            }

            sb.AppendLine($"{prefix}{expectedDisplay}");
            sb.Append($"Actual:   {actualDisplay}");

            // Bottom markers (actual edits)
            var bottomMarkers = BuildClusterMarkerLine(upMarker, actualDiffClusters, actualEdits!);
            if (bottomMarkers.Length > 0)
            {
                sb.AppendLine();
                sb.Append(' ', markerOffset);
                sb.Append(bottomMarkers);
            }

            // Codepoint hints for visually ambiguous clusters
            var codepointHint = BuildClusterCodepointHint(
                expectedDiffClusters, expectedEdits!,
                actualDiffClusters, actualEdits!,
                commonPrefixLen);
            if (codepointHint != null)
            {
                sb.AppendLine();
                sb.Append(codepointHint);
            }
        }
        else
        {
            // Show Expected/Actual lines without markers, plus a hint
            sb.AppendLine($"{prefix}{expectedDisplay}");
            sb.AppendLine($"Actual:   {actualDisplay}");
            sb.Append("Strings differ significantly");
        }

        return sb.ToString();
    }

    private string BuildDisplayString(string value, EscapeStyle? escapeStyleOverride = null)
    {
        var sb = new StringBuilder();

        if (_prefixWithEllipsis)
            sb.Append("...");

        sb.Append('"');

        foreach (var c in value)
        {
            if (c.NeedsEscaping())
                sb.Append(c.ToSafeString(escapeStyleOverride));
            else
                sb.Append(c);
        }

        sb.Append('"');

        if (_suffixWithEllipsis)
            sb.Append("...");

        return sb.ToString();
    }

    /// <summary>
    /// Computes the display column offset from the start of the quoted string to
    /// the given cluster index, accounting for ellipsis prefix, quote char,
    /// and the display width of each preceding cluster.
    /// </summary>
    private int ComputeDisplayOffset(string[] clusters, int clusterIndex)
    {
        // Account for optional "..." prefix and opening quote
        var offset = _prefixWithEllipsis ? 4 : 1;

        var len = Math.Min(clusterIndex, clusters.Length);
        for (var i = 0; i < len; i++)
        {
            offset += GraphemeClusterHelper.ClusterDisplayWidth(clusters[i]);
        }

        return offset;
    }

    /// <summary>
    /// Builds a marker line where each edited cluster gets markers spanning its display width.
    /// </summary>
    private static string BuildClusterMarkerLine(char marker, string[] clusters, bool[] edits)
    {
        var markerSb = new StringBuilder();
        for (var i = 0; i < clusters.Length; i++)
        {
            var w = GraphemeClusterHelper.ClusterDisplayWidth(clusters[i]);
            if (w == 0) w = 1; // Ensure zero-width clusters still get a marker
            if (edits[i])
                markerSb.Append(marker, w);
            else
                markerSb.Append(' ', w);
        }

        // Trim trailing spaces
        while (markerSb.Length > 0 && markerSb[markerSb.Length - 1] == ' ')
            markerSb.Length--;

        return markerSb.ToString();
    }

    private static int CountEdits(bool[] edits)
    {
        var count = 0;
        foreach (var e in edits)
            if (e) count++;
        return count;
    }

    private int FindCommonClusterPrefixLength(string[] expected, string[] actual)
    {
        var minLen = Math.Min(expected.Length, actual.Length);
        for (var i = 0; i < minLen; i++)
        {
            if (!GraphemeClusterHelper.ClustersEqual(expected[i], actual[i], _caseSensitivity))
                return i;
        }
        return minLen;
    }

    private int FindCommonClusterSuffixLength(string[] expected, string[] actual, int commonPrefixLength)
    {
        var maxSuffixLen = Math.Min(expected.Length, actual.Length) - commonPrefixLength;
        for (var i = 0; i < maxSuffixLen; i++)
        {
            if (!GraphemeClusterHelper.ClustersEqual(
                    expected[expected.Length - 1 - i],
                    actual[actual.Length - 1 - i],
                    _caseSensitivity))
                return i;
        }
        return maxSuffixLen;
    }

    private static string[] SubArray(string[] source, int start, int end)
    {
        var length = end - start;
        if (length <= 0) return Array.Empty<string>();
        var result = new string[length];
        Array.Copy(source, start, result, 0, length);
        return result;
    }

    private static string? BuildClusterCodepointHint(
        string[] expectedClusters, bool[] expectedEdits,
        string[] actualClusters, bool[] actualEdits,
        int regionStartIndex)
    {
        var expectedEditedClusters = GetEditedClusters(expectedClusters, expectedEdits);
        var actualEditedClusters = GetEditedClusters(actualClusters, actualEdits);

        // Only show hint when the edit is small and contains visually ambiguous clusters
        var hasAmbiguous = expectedEditedClusters.Any(GraphemeClusterHelper.IsClusterVisuallyAmbiguous)
                        || actualEditedClusters.Any(GraphemeClusterHelper.IsClusterVisuallyAmbiguous);
        if (!hasAmbiguous) return null;
        if (expectedEditedClusters.Count > 3 || actualEditedClusters.Count > 3) return null;

        var sb = new StringBuilder();
        sb.Append($"Difference at index {regionStartIndex}: ");

        if (expectedEditedClusters.Count > 0)
            sb.Append(GraphemeClusterHelper.FormatClusterCodepoints(expectedEditedClusters));
        else
            sb.Append("(empty)");

        sb.Append(" vs ");

        if (actualEditedClusters.Count > 0)
            sb.Append(GraphemeClusterHelper.FormatClusterCodepoints(actualEditedClusters));
        else
            sb.Append("(empty)");

        return sb.ToString();
    }

    private static List<string> GetEditedClusters(string[] clusters, bool[] edits)
    {
        var result = new List<string>();
        for (var i = 0; i < clusters.Length && i < edits.Length; i++)
        {
            if (edits[i])
                result.Add(clusters[i]);
        }
        return result;
    }
}
