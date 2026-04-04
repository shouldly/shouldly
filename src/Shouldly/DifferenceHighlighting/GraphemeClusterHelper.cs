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
        return clusters.ToArray();
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
