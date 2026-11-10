namespace Shouldly.DifferenceHighlighting;

/// <summary>
/// Detects strings that differ ordinally but are canonically equivalent — the same text in a
/// different Unicode normalization form (e.g. precomposed "é" U+00E9 vs "e" + U+0301).
/// </summary>
static class UnicodeNormalization
{
    /// <summary>
    /// True when the strings are not ordinally equal (under <paramref name="caseSensitivity"/>)
    /// but are equal once both are normalized to NFC. Compatibility equivalence (NFKC, e.g. the
    /// "ﬁ" ligature vs "fi") is deliberately not considered: it can change meaning.
    /// </summary>
    internal static bool DifferOnlyInNormalization(string expected, string actual, Case caseSensitivity)
    {
        var comparer = caseSensitivity == Case.Insensitive
            ? StringComparer.OrdinalIgnoreCase
            : StringComparer.Ordinal;

        if (comparer.Equals(expected, actual))
            return false;

        // Pure ASCII text is invariant under every normalization form
        if (IsAscii(expected) && IsAscii(actual))
            return false;

        var normalizedExpected = TryNormalize(expected);
        var normalizedActual = TryNormalize(actual);
        return normalizedExpected != null
               && normalizedActual != null
               && comparer.Equals(normalizedExpected, normalizedActual);
    }

    /// <summary>
    /// Builds the hint shown when <see cref="DifferOnlyInNormalization"/> is true, naming each
    /// side's normalization form where it can be determined.
    /// </summary>
    internal static string BuildHint(string expected, string actual) =>
        $"Strings differ only in Unicode normalization: expected is {DescribeForm(expected)}, actual is {DescribeForm(actual)}";

    private static string DescribeForm(string value)
    {
        var isNfc = IsNormalized(value, NormalizationForm.FormC);
        var isNfd = IsNormalized(value, NormalizationForm.FormD);

        return (isNfc, isNfd) switch
        {
            (true, false) => "NFC",
            (false, true) => "NFD",
            // Text with nothing to compose or decompose (e.g. Ω U+03A9) satisfies both forms
            (true, true) => "both NFC and NFD",
            _ => "neither NFC nor NFD"
        };
    }

    private static string? TryNormalize(string value)
    {
        try
        {
            return value.Normalize(NormalizationForm.FormC);
        }
        catch (Exception e) when (IsNormalizationFailure(e))
        {
            return null;
        }
    }

    private static bool IsNormalized(string value, NormalizationForm form)
    {
        try
        {
            return value.IsNormalized(form);
        }
        catch (Exception e) when (IsNormalizationFailure(e))
        {
            return false;
        }
    }

    // Normalize/IsNormalized throw ArgumentException for ill-formed UTF-16 (lone surrogates),
    // and PlatformNotSupportedException where no normalization data is available (e.g. some
    // globalization-invariant or trimmed runtimes). Either way the hint is best-effort only.
    private static bool IsNormalizationFailure(Exception e) =>
        e is ArgumentException or PlatformNotSupportedException;

    private static bool IsAscii(string value)
    {
        foreach (var c in value)
        {
            if (c > 0x7F)
                return false;
        }
        return true;
    }
}
