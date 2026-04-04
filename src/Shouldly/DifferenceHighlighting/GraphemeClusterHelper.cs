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
        var clusters = new List<string>();
        var enumerator = StringInfo.GetTextElementEnumerator(value);
        while (enumerator.MoveNext())
        {
            clusters.Add(enumerator.GetTextElement());
        }

        // Post-process: merge emoji sequences that older .NET runtimes (e.g. net48)
        // don't recognize as single grapheme clusters. Modern .NET (8+) handles these
        // natively, but net48's StringInfo uses an older Unicode standard.
        return MergeEmojiSequences(clusters);
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

        var merged = new List<string>();
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
    internal static int ClusterDisplayWidth(string cluster)
    {
        if (cluster.Length == 0) return 0;

        // Single char that needs escaping — use escape string length
        if (cluster.Length == 1 && cluster[0].NeedsEscaping())
            return cluster[0].ToSafeString().Length;

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
                    baseWidth += cluster[i].ToSafeString().Length;
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
        var parts = new List<string>();
        foreach (var cluster in clusters)
        {
            var codepoints = new List<string>();
            for (var i = 0; i < cluster.Length; i++)
            {
                if (char.IsHighSurrogate(cluster[i]) && i + 1 < cluster.Length && char.IsLowSurrogate(cluster[i + 1]))
                {
                    var cp = char.ConvertToUtf32(cluster[i], cluster[i + 1]);
                    codepoints.Add($"U+{cp:X4}");
                    i++; // skip low surrogate
                }
                else
                {
                    codepoints.Add($"U+{(int)cluster[i]:X4}");
                }
            }
            parts.Add(string.Join(" ", codepoints));
        }
        return string.Join(", ", parts);
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
