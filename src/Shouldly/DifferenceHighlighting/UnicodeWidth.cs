namespace Shouldly.DifferenceHighlighting;

/// <summary>
/// Calculates the terminal display width of Unicode codepoints (wcwidth),
/// using the Unicode 17.0.0 tables in <see cref="UnicodeWidthTables"/>.
/// Ported from Wcwidth.Sources (https://github.com/spectreconsole/wcwidth),
/// a port of Jeff Quast's python wcwidth, originally based on Markus Kuhn's
/// wcwidth.c. MIT licensed. Trimmed to a single Unicode version.
/// </summary>
static class UnicodeWidth
{
    /// <summary>
    /// Gets the display width in terminal columns of a single codepoint:
    /// -1 for C0/C1 control characters, 0 for zero-width (combining marks,
    /// format characters), 2 for wide (East Asian W/F, emoji), otherwise 1.
    /// </summary>
    internal static int GetWidth(int codepoint)
    {
        // Zero-width characters that can't be identified from Unicode category
        // alone (hand-curated in wcwidth: some Cf, Cc, Mn, Zl and Zp)
        if (IsZeroWidthSpecial(codepoint))
            return 0;

        // C0/C1 control characters
        if (codepoint < 32 || codepoint is >= 0x07F and < 0x0A0)
            return -1;

        if (TableContains(UnicodeWidthTables.Zero, codepoint))
            return 0;

        return TableContains(UnicodeWidthTables.Wide, codepoint) ? 2 : 1;
    }

    /// <summary>
    /// Returns true for narrow codepoints that are rendered wide (emoji
    /// presentation) when followed by U+FE0F Variation Selector-16.
    /// </summary>
    internal static bool IsVs16NarrowToWide(int codepoint) =>
        TableContains(UnicodeWidthTables.Vs16NarrowToWide, codepoint);

    private static bool IsZeroWidthSpecial(int codepoint) =>
        codepoint == 0 // Null (Cc)
        || codepoint == 0x034F // Combining grapheme joiner (Mn)
        || codepoint is >= 0x200B and <= 0x200F // Zero width space/non-joiner/joiner, LTR/RTL marks
        || codepoint == 0x2028 // Line separator (Zl)
        || codepoint == 0x2029 // Paragraph separator (Zp)
        || codepoint is >= 0x202A and <= 0x202E // Directional embedding/override controls
        || codepoint is >= 0x2060 and <= 0x2063; // Word joiner, invisible operators

    /// <summary>
    /// Binary search over a sorted table of inclusive [start, end] codepoint ranges.
    /// </summary>
    private static bool TableContains(int[,] table, int value)
    {
        var min = 0;
        var max = table.GetUpperBound(0);

        if (value < table[0, 0] || value > table[max, 1])
            return false;

        while (max >= min)
        {
            var mid = (min + max) / 2;
            if (value > table[mid, 1])
                min = mid + 1;
            else if (value < table[mid, 0])
                max = mid - 1;
            else
                return true;
        }

        return false;
    }
}
