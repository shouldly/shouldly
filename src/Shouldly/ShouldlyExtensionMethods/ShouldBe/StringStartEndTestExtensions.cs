using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;

namespace Shouldly;

public static partial class ShouldBeStringTestExtensions
{
    /// <summary>
    /// Asserts that a string starts with another string
    /// </summary>
    public static void ShouldStartWith([NotNull] this string? actual, string expected, Case caseSensitivity = Case.Sensitive, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual.AssertAwesomely(v => Is.StringStartingWithUsingCaseSensitivity(v, expected, caseSensitivity), actual, expected, customMessage, actualExpression: actualExpression);
        Debug.Assert(actual != null);
    }

    /// <summary>
    /// Asserts that a string ends with another string
    /// </summary>
    public static void ShouldEndWith([NotNull] this string? actual, string expected, Case caseSensitivity = Case.Sensitive, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual.AssertAwesomely(v => Is.EndsWithUsingCaseSensitivity(v, expected, caseSensitivity), actual, expected, customMessage, actualExpression: actualExpression);
        Debug.Assert(actual != null);
    }

    /// <summary>
    /// Asserts that a string does not start with another string
    /// </summary>
    public static void ShouldNotStartWith(this string? actual, string expected, Case caseSensitivity = Case.Sensitive, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual.AssertAwesomely(v => !Is.StringStartingWithUsingCaseSensitivity(v, expected, caseSensitivity), actual, expected, customMessage, actualExpression: actualExpression);
    }

    /// <summary>
    /// Asserts that a string does not end with another string
    /// </summary>
    public static void ShouldNotEndWith(this string? actual, string expected, Case caseSensitivity,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        ShouldNotEndWith(actual, expected, null, caseSensitivity, actualExpression);
    }

    /// <summary>
    /// Asserts that a string does not end with another string
    /// </summary>
    public static void ShouldNotEndWith(this string? actual, string expected, string? customMessage = null, Case caseSensitivity = Case.Sensitive,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual.AssertAwesomely(v => !Is.EndsWithUsingCaseSensitivity(v, expected, caseSensitivity), actual, expected, customMessage, actualExpression: actualExpression);
    }
}