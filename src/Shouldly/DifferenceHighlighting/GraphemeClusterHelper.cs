using System.Globalization;

namespace Shouldly.DifferenceHighlighting;

static class GraphemeClusterHelper
{
    /// <summary>
    /// Splits a string into grapheme clusters using StringInfo.GetTextElementEnumerator.
    /// Each element in the returned array is a single user-perceived character (which may
    /// be multiple UTF-16 chars, e.g. emoji with skin tone modifiers, flag sequences).
    /// </summary>
    internal static string[] GetGraphemeClusters(string value)
    {
        // Pre-size: cluster count <= char count (equality for pure ASCII)
        var clusters = new List<string>(value.Length);
        var enumerator = StringInfo.GetTextElementEnumerator(value);
        while (enumerator.MoveNext())
        {
            clusters.Add(enumerator.GetTextElement());
        }

        // Post-process: merge emoji sequences that older .NET runtimes (e.g. net48)
        // don't recognize as single grapheme clusters. Modern .NET (8+) handles these
        // natively, but net48's StringInfo uses an older Unicode standard.
        // Fast-path: skip merge entirely when no surrogate pairs exist (pure BMP text).
        if (!ContainsAnySurrogatePairs(value))
            return clusters.ToArray();

        return MergeEmojiSequences(clusters);
    }

    private static bool ContainsAnySurrogatePairs(string value)
    {
        for (var i = 0; i < value.Length - 1; i++)
        {
            if (char.IsHighSurrogate(value[i]))
                return true;
        }
        return false;
    }

    /// <summary>
    /// Merges adjacent clusters that form a single visual emoji:
    /// - Regional indicator pairs (flag emoji like 🇫🇷)
    /// - Emoji + skin tone modifier (👋🏽)
    /// - Emoji + variation selector (❤️)
    /// - ZWJ sequences (emoji joined by U+200D)
    /// </summary>
    private static string[] MergeEmojiSequences(List<string> clusters)
    {
        if (clusters.Count <= 1) return clusters.ToArray();

        var merged = new List<string>(clusters.Count);
        var i = 0;
        while (i < clusters.Count)
        {
            var current = clusters[i];

            // Try to merge with subsequent clusters
            while (i + 1 < clusters.Count && ShouldMergeWithNext(current, clusters[i + 1]))
            {
                i++;
                current += clusters[i];
            }

            merged.Add(current);
            i++;
        }

        return merged.ToArray();
    }

    private static bool ShouldMergeWithNext(string current, string next)
    {
        // Merge regional indicator pairs into flag emoji
        if (IsRegionalIndicator(current) && IsRegionalIndicator(next))
            return true;

        // Merge emoji + skin tone modifier
        if (ContainsSurrogatePairEmoji(current) && IsSkinToneModifier(next))
            return true;

        // Merge emoji + variation selector (U+FE0F or U+FE0E)
        if (next.Length == 1 && (next[0] == '\uFE0F' || next[0] == '\uFE0E'))
            return true;

        // Merge ZWJ sequences: current + ZWJ + next emoji
        if (next.Length == 1 && next[0] == '\u200D') // ZWJ
            return true;
        if (current.Length > 0 && current[current.Length - 1] == '\u200D')
            return true;

        return false;
    }

    private static bool IsRegionalIndicator(string cluster)
    {
        // Regional indicators are U+1F1E6 to U+1F1FF, each encoded as a surrogate pair
        if (cluster.Length < 2) return false;
        if (!char.IsHighSurrogate(cluster[0]) || !char.IsLowSurrogate(cluster[1])) return false;
        var cp = char.ConvertToUtf32(cluster[0], cluster[1]);
        return cp >= 0x1F1E6 && cp <= 0x1F1FF;
    }

    private static bool IsSkinToneModifier(string cluster)
    {
        // Skin tone modifiers are U+1F3FB to U+1F3FF, each a surrogate pair
        if (cluster.Length < 2) return false;
        if (!char.IsHighSurrogate(cluster[0]) || !char.IsLowSurrogate(cluster[1])) return false;
        var cp = char.ConvertToUtf32(cluster[0], cluster[1]);
        return cp >= 0x1F3FB && cp <= 0x1F3FF;
    }

    private static bool ContainsSurrogatePairEmoji(string cluster)
    {
        for (var i = 0; i < cluster.Length - 1; i++)
        {
            if (char.IsHighSurrogate(cluster[i]) && char.IsLowSurrogate(cluster[i + 1]))
                return true;
        }
        return false;
    }

    /// <summary>
    /// Estimates the terminal display width of a grapheme cluster.
    /// - Control chars that need escaping: width of their escape representation
    /// - Emoji / surrogate pairs: 2 columns
    /// - CJK ideographs: 2 columns
    /// - Combining marks (zero-width): 0 columns
    /// - Everything else: 1 column
    /// </summary>
    internal static int ClusterDisplayWidth(string cluster) =>
        ClusterDisplayWidth(cluster, escapeStyleOverride: null);

    internal static int ClusterDisplayWidth(string cluster, EscapeStyle? escapeStyleOverride)
    {
        if (cluster.Length == 0) return 0;

        // Single char that needs escaping — use escape string length
        if (cluster.Length == 1 && cluster[0].NeedsEscaping())
            return cluster[0].ToSafeString(escapeStyleOverride).Length;

        // Multi-char cluster containing surrogate pairs or variation selectors → emoji, likely 2 columns
        if (cluster.Length > 1)
        {
            // Check if it contains surrogate pairs (emoji) or emoji variation selector (U+FE0F)
            for (var i = 0; i < cluster.Length; i++)
            {
                if (i < cluster.Length - 1 && char.IsHighSurrogate(cluster[i]) && char.IsLowSurrogate(cluster[i + 1]))
                    return 2;
                if (cluster[i] == '\uFE0F') // Variation Selector-16 (emoji presentation)
                    return 2;
            }

            // Multi-char cluster with combining marks — base char width
            // (e.g. 'e' + combining acute = 1 column)
            var baseWidth = 0;
            for (var i = 0; i < cluster.Length; i++)
            {
                var category = char.GetUnicodeCategory(cluster[i]);
                if (category is UnicodeCategory.NonSpacingMark
                    or UnicodeCategory.SpacingCombiningMark
                    or UnicodeCategory.EnclosingMark)
                    continue;

                if (cluster[i].NeedsEscaping())
                    baseWidth += cluster[i].ToSafeString(escapeStyleOverride).Length;
                else
                    baseWidth += IsCjkOrWideChar(cluster[i]) ? 2 : 1;
            }
            return baseWidth > 0 ? baseWidth : 1;
        }

        // Single char
        var c = cluster[0];

        var cat = char.GetUnicodeCategory(c);
        if (cat is UnicodeCategory.NonSpacingMark
            or UnicodeCategory.SpacingCombiningMark
            or UnicodeCategory.EnclosingMark
            or UnicodeCategory.Format)
            return 0;

        if (IsCjkOrWideChar(c))
            return 2;

        return 1;
    }

    /// <summary>
    /// Returns true if a grapheme cluster is visually ambiguous — invisible or modifies
    /// an adjacent character rather than being distinct on its own.
    /// </summary>
    internal static bool IsClusterVisuallyAmbiguous(string cluster)
    {
        for (var i = 0; i < cluster.Length; i++)
        {
            var category = char.GetUnicodeCategory(cluster, i);
            if (category is UnicodeCategory.NonSpacingMark
                or UnicodeCategory.SpacingCombiningMark
                or UnicodeCategory.EnclosingMark
                or UnicodeCategory.Format)
                return true;
        }
        return false;
    }

    /// <summary>
    /// Formats a list of grapheme clusters as their constituent codepoints for hint display.
    /// </summary>
    internal static string FormatClusterCodepoints(List<string> clusters)
    {
        var sb = new StringBuilder();
        for (var c = 0; c < clusters.Count; c++)
        {
            if (c > 0) sb.Append(", ");
            var cluster = clusters[c];
            var first = true;
            for (var i = 0; i < cluster.Length; i++)
            {
                if (!first) sb.Append(' ');
                first = false;
                if (char.IsHighSurrogate(cluster[i]) && i + 1 < cluster.Length && char.IsLowSurrogate(cluster[i + 1]))
                {
                    var cp = char.ConvertToUtf32(cluster[i], cluster[i + 1]);
                    sb.Append($"U+{cp:X4}");
                    i++; // skip low surrogate
                }
                else
                {
                    sb.Append($"U+{(int)cluster[i]:X4}");
                }
            }
        }
        return sb.ToString();
    }

    /// <summary>
    /// Compares two grapheme clusters for equality, respecting case sensitivity.
    /// </summary>
    internal static bool ClustersEqual(string a, string b, Case caseSensitivity)
    {
        if (caseSensitivity == Case.Insensitive)
            return string.Compare(a, b, StringComparison.OrdinalIgnoreCase) == 0;
        return string.Equals(a, b, StringComparison.Ordinal);
    }

    /// <summary>
    /// Finds the display-column offset of the first cluster that differs between two strings.
    /// For BMP-only text (the common case), uses lazy enumeration to avoid materializing full
    /// cluster arrays. Falls back to full array approach when surrogates are present (emoji).
    /// Returns -1 if strings are equal.
    /// </summary>
    internal static int FindFirstClusterDifference(string a, string b, Case caseSensitivity)
    {
        // If either string has surrogates, fall back to array approach for correct emoji merging
        if (ContainsAnySurrogatePairs(a) || ContainsAnySurrogatePairs(b))
        {
            return FindFirstClusterDifferenceViaArrays(a, b, caseSensitivity);
        }

        // Fast path: BMP-only text — enumerate lazily, stop at first difference
        var enumA = StringInfo.GetTextElementEnumerator(a);
        var enumB = StringInfo.GetTextElementEnumerator(b);

        var displayOffset = 0;
        while (true)
        {
            var hasA = enumA.MoveNext();
            var hasB = enumB.MoveNext();

            if (!hasA && !hasB) return -1;
            if (!hasA || !hasB) return displayOffset;

            var clusterA = enumA.GetTextElement();
            var clusterB = enumB.GetTextElement();

            if (!ClustersEqual(clusterA, clusterB, caseSensitivity))
                return displayOffset;

            displayOffset += ClusterDisplayWidth(clusterA);
        }
    }

    private static int FindFirstClusterDifferenceViaArrays(string a, string b, Case caseSensitivity)
    {
        var aClusters = GetGraphemeClusters(a);
        var bClusters = GetGraphemeClusters(b);

        var minLen = Math.Min(aClusters.Length, bClusters.Length);
        var displayOffset = 0;
        for (var i = 0; i < minLen; i++)
        {
            if (!ClustersEqual(aClusters[i], bClusters[i], caseSensitivity))
                return displayOffset;
            displayOffset += ClusterDisplayWidth(aClusters[i]);
        }
        if (aClusters.Length != bClusters.Length)
            return displayOffset;
        return -1;
    }

    private static bool IsCjkOrWideChar(char c)
    {
        // CJK Unified Ideographs and common CJK ranges
        // Note: this is a simplified check covering the most common ranges
        return c >= '\u4E00' && c <= '\u9FFF'   // CJK Unified Ideographs
            || c >= '\u3400' && c <= '\u4DBF'    // CJK Extension A
            || c >= '\uF900' && c <= '\uFAFF'    // CJK Compatibility Ideographs
            || c >= '\u3000' && c <= '\u303F'     // CJK Symbols and Punctuation
            || c >= '\uFF01' && c <= '\uFF60'     // Fullwidth Forms
            || c >= '\uFFE0' && c <= '\uFFE6'     // Fullwidth Signs
            || c >= '\uAC00' && c <= '\uD7AF';    // Hangul Syllables
    }
}
