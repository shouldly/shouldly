using JetBrains.Annotations;

namespace Shouldly;

public static partial class ShouldBeStringTestExtensions
{
    /// <summary>
    /// Strip out whitespace (whitespace, tabs, line-endings, etc) and compare the two strings
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldContainWithoutWhitespace(this string actual, object? expected, string? customMessage = null)
    {
        var strippedActual = actual.Quotify().StripWhitespace();
        var strippedExpected = (expected?.ToString() ?? "NULL").Quotify().StripWhitespace();

        strippedActual.AssertAwesomely(v => v.Contains(strippedExpected), actual, expected, customMessage);
    }

    /// <summary>
    /// Asserts that a string contains another string
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldContain(this string actual, string expected, Case caseSensitivity = Case.Insensitive, string? customMessage = null)
    {
        actual.AssertAwesomely(
            v => caseSensitivity == Case.Sensitive ? Is.StringContainingUsingCaseSensitivity(v, expected) : Is.StringContainingIgnoreCase(v, expected),
            actual.Clip(100, "..."),
            expected,
            caseSensitivity,
            customMessage);
    }

    /// <summary>
    /// Asserts that a string does not contain another string
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotContain(this string actual, string expected, Case caseSensitivity = Case.Insensitive, string? customMessage = null)
    {
        actual.AssertAwesomely(v =>
        {
            var b = caseSensitivity == Case.Sensitive ? !Is.StringContainingUsingCaseSensitivity(v, expected) : !Is.StringContainingIgnoreCase(v, expected);
            return b;
        }, actual.Clip(100, "..."), expected, caseSensitivity, customMessage);
    }

    /// <summary>
    /// Asserts that a string matches a regular expression pattern
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldMatch(this string actual, [RegexPattern] string regexPattern, string? customMessage = null)
    {
        actual.AssertAwesomely(v => Is.StringMatchingRegex(v, regexPattern), actual, regexPattern, customMessage);
    }

    /// <summary>
    /// Asserts that a string does not match a regular expression pattern
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotMatch(this string actual, [RegexPattern] string regexPattern, string? customMessage = null)
    {
        actual.AssertAwesomely(v => !Is.StringMatchingRegex(v, regexPattern), actual, regexPattern, customMessage);
    }
}