using JetBrains.Annotations;
using Shouldly.Internals.Failures;

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

        if (!strippedActual.Satisfies(v => v.Contains(strippedExpected)))
            throw Fail.ShouldContainWithoutWhitespace.With(expected).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that a string contains another string
    /// </summary>
    public static void ShouldContain(this string actual, string expected, Case caseSensitivity = Case.Sensitive, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (!actual.Satisfies(v => caseSensitivity == Case.Sensitive ? Is.StringContainingUsingCaseSensitivity(v, expected) : Is.StringContainingIgnoreCase(v, expected)))
        {
            var nearMiss = CaseNearMiss.Within(actual, expected, caseSensitivity);
            throw Fail.ShouldContain.With(expected, caseSensitivity, nearMiss)
                .For(nearMiss?.Excerpt(actual!, 100) ?? actual?.Clip(100, "..."), customMessage, actualExpression);
        }
    }

    /// <summary>
    /// Asserts that a string does not contain another string
    /// </summary>
    public static void ShouldNotContain(this string actual, string expected, Case caseSensitivity = Case.Sensitive, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (!actual.Satisfies(v =>
        {
            var b = caseSensitivity == Case.Sensitive ? !Is.StringContainingUsingCaseSensitivity(v, expected) : !Is.StringContainingIgnoreCase(v, expected);
            return b;
        }))
            throw Fail.ShouldNotContain.With(expected, caseSensitivity).For(actual?.Clip(100, "..."), customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that a string matches a regular expression pattern
    /// </summary>
    public static void ShouldMatch(this string actual, [RegexPattern] string regexPattern, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (!actual.Satisfies(v => Is.StringMatchingRegex(v, regexPattern)))
            throw Fail.ShouldMatch.With(regexPattern).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that a string does not match a regular expression pattern
    /// </summary>
    public static void ShouldNotMatch(this string actual, [RegexPattern] string regexPattern, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (!actual.Satisfies(v => !Is.StringMatchingRegex(v, regexPattern)))
            throw Fail.ShouldNotMatch.With(regexPattern).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the specified string contains all the expected values.
    /// </summary>
    public static void ShouldContainAll(this string actual, string[] expectedValues, Case caseSensitivity = Case.Sensitive, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (!actual.Satisfies(v =>
        {
            return expectedValues.All(expected =>
                caseSensitivity == Case.Sensitive
                    ? Is.StringContainingUsingCaseSensitivity(v, expected)
                    : Is.StringContainingIgnoreCase(v, expected));
        }))
            throw Fail.ShouldContainAll.With(string.Join(", ", expectedValues), caseSensitivity).For(actual?.Clip(100, "..."), customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the specified string contains at least one of the provided expected values.
    /// </summary>
    public static void ShouldContainAny(this string actual, string[] expectedValues, Case caseSensitivity = Case.Sensitive, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (!actual.Satisfies(v =>
        {
            return expectedValues.Any(expected =>
                caseSensitivity == Case.Sensitive
                    ? Is.StringContainingUsingCaseSensitivity(v, expected)
                    : Is.StringContainingIgnoreCase(v, expected));
        }))
            throw Fail.ShouldContainAny.With(string.Join(", ", expectedValues), caseSensitivity).For(actual?.Clip(100, "..."), customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the specified string does not contain all of the provided expected values.
    /// </summary>
    public static void ShouldNotContainAll(this string actual, string[] expectedValues, Case caseSensitivity = Case.Sensitive, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (!actual.Satisfies(v =>
        {
            return !expectedValues.All(expected =>
                caseSensitivity == Case.Sensitive
                    ? Is.StringContainingUsingCaseSensitivity(v, expected)
                    : Is.StringContainingIgnoreCase(v, expected));
        }))
            throw Fail.ShouldNotContainAll.With(string.Join(", ", expectedValues), caseSensitivity).For(actual?.Clip(100, "..."), customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the specified string does not contain any of the provided expected values.
    /// </summary>
    public static void ShouldNotContainAny(this string actual, string[] expectedValues, Case caseSensitivity = Case.Sensitive, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (!actual.Satisfies(v =>
        {
            return !expectedValues.Any(expected =>
                caseSensitivity == Case.Sensitive
                    ? Is.StringContainingUsingCaseSensitivity(v, expected)
                    : Is.StringContainingIgnoreCase(v, expected));
        }))
            throw Fail.ShouldNotContainAny.With(string.Join(", ", expectedValues), caseSensitivity).For(actual?.Clip(100, "..."), customMessage, actualExpression);
    }
}