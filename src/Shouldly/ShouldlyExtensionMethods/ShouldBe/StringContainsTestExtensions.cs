using JetBrains.Annotations;

namespace Shouldly;

/// <summary>
/// Extension methods for string assertions
/// </summary>
public static partial class ShouldBeStringTestExtensions
{
    /// <summary>
    /// Strip out whitespace (whitespace, tabs, line-endings, etc) and compare the two strings
    /// </summary>
    public static void ShouldContainWithoutWhitespace(this string actual, object? expected, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var strippedActual = actual.Quotify().StripWhitespace();
        var strippedExpected = (expected?.ToString() ?? "NULL").Quotify().StripWhitespace();

        strippedActual.AssertAwesomely(v => v.Contains(strippedExpected), actual, expected, customMessage, actualExpression: actualExpression);
    }

    /// <summary>
    /// Asserts that a string contains another string
    /// </summary>
    public static void ShouldContain(this string actual, string expected, Case caseSensitivity = Case.Sensitive, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual.AssertAwesomely(
            v => caseSensitivity == Case.Sensitive ? Is.StringContainingUsingCaseSensitivity(v, expected) : Is.StringContainingIgnoreCase(v, expected),
            actual?.Clip(100, "..."),
            expected,
            caseSensitivity,
            customMessage, actualExpression: actualExpression);
    }

    /// <summary>
    /// Asserts that a string does not contain another string
    /// </summary>
    public static void ShouldNotContain(this string actual, string expected, Case caseSensitivity = Case.Sensitive, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual.AssertAwesomely(v =>
        {
            var b = caseSensitivity == Case.Sensitive ? !Is.StringContainingUsingCaseSensitivity(v, expected) : !Is.StringContainingIgnoreCase(v, expected);
            return b;
        }, actual?.Clip(100, "..."), expected, caseSensitivity, customMessage, actualExpression: actualExpression);
    }

    /// <summary>
    /// Asserts that a string matches a regular expression pattern
    /// </summary>
    public static void ShouldMatch(this string actual, [RegexPattern] string regexPattern, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual.AssertAwesomely(v => Is.StringMatchingRegex(v, regexPattern), actual, regexPattern, customMessage, actualExpression: actualExpression);
    }

    /// <summary>
    /// Asserts that a string does not match a regular expression pattern
    /// </summary>
    public static void ShouldNotMatch(this string actual, [RegexPattern] string regexPattern, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual.AssertAwesomely(v => !Is.StringMatchingRegex(v, regexPattern), actual, regexPattern, customMessage, actualExpression: actualExpression);
    }

    /// <summary>
    /// Asserts that the specified string contains all the expected values.
    /// </summary>
    public static void ShouldContainAll(this string actual, string[] expectedValues, Case caseSensitivity = Case.Sensitive, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual.AssertAwesomely(v =>
        {
            return expectedValues.All(expected =>
                caseSensitivity == Case.Sensitive
                    ? Is.StringContainingUsingCaseSensitivity(v, expected)
                    : Is.StringContainingIgnoreCase(v, expected));
        }, actual?.Clip(100, "..."), string.Join(", ", expectedValues), caseSensitivity, customMessage, actualExpression: actualExpression);
    }

    /// <summary>
    /// Asserts that the specified string contains at least one of the provided expected values.
    /// </summary>
    public static void ShouldContainAny(this string actual, string[] expectedValues, Case caseSensitivity = Case.Sensitive, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual.AssertAwesomely(v =>
        {
            return expectedValues.Any(expected =>
                caseSensitivity == Case.Sensitive
                    ? Is.StringContainingUsingCaseSensitivity(v, expected)
                    : Is.StringContainingIgnoreCase(v, expected));
        }, actual?.Clip(100, "..."), string.Join(", ", expectedValues), caseSensitivity, customMessage, actualExpression: actualExpression);
    }

    /// <summary>
    /// Asserts that the specified string does not contain all of the provided expected values.
    /// </summary>
    public static void ShouldNotContainAll(this string actual, string[] expectedValues, Case caseSensitivity = Case.Sensitive, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual.AssertAwesomely(v =>
        {
            return !expectedValues.All(expected =>
                caseSensitivity == Case.Sensitive
                    ? Is.StringContainingUsingCaseSensitivity(v, expected)
                    : Is.StringContainingIgnoreCase(v, expected));
        }, actual?.Clip(100, "..."), string.Join(", ", expectedValues), caseSensitivity, customMessage, actualExpression: actualExpression);
    }

    /// <summary>
    /// Asserts that the specified string does not contain any of the provided expected values.
    /// </summary>
    public static void ShouldNotContainAny(this string actual, string[] expectedValues, Case caseSensitivity = Case.Sensitive, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual.AssertAwesomely(v =>
        {
            return !expectedValues.Any(expected =>
                caseSensitivity == Case.Sensitive
                    ? Is.StringContainingUsingCaseSensitivity(v, expected)
                    : Is.StringContainingIgnoreCase(v, expected));
        }, actual?.Clip(100, "..."), string.Join(", ", expectedValues), caseSensitivity, customMessage, actualExpression: actualExpression);
    }
}