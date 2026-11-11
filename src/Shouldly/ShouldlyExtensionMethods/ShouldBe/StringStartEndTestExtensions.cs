using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;
using Shouldly.Internals.Failures;

namespace Shouldly;

public static partial class ShouldBeStringTestExtensions
{
    /// <summary>
    /// Asserts that a string starts with another string
    /// </summary>
    public static void ShouldStartWith([NotNull] this string? actual, string expected, Case caseSensitivity = Case.Sensitive, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (!actual.Satisfies(v => Is.StringStartingWithUsingCaseSensitivity(v, expected, caseSensitivity)))
            throw Fail.ShouldStartWith.With(expected).For(actual, customMessage, actualExpression);
        Debug.Assert(actual != null);
    }

    /// <summary>
    /// Asserts that a string ends with another string
    /// </summary>
    public static void ShouldEndWith([NotNull] this string? actual, string expected, Case caseSensitivity = Case.Sensitive, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (!actual.Satisfies(v => Is.EndsWithUsingCaseSensitivity(v, expected, caseSensitivity)))
            throw Fail.ShouldEndWith.With(expected).For(actual, customMessage, actualExpression);
        Debug.Assert(actual != null);
    }

    /// <summary>
    /// Asserts that a string does not start with another string
    /// </summary>
    public static void ShouldNotStartWith(this string? actual, string expected, Case caseSensitivity = Case.Sensitive, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (!actual.Satisfies(v => !Is.StringStartingWithUsingCaseSensitivity(v, expected, caseSensitivity)))
            throw Fail.ShouldNotStartWith.With(expected).For(actual, customMessage, actualExpression);
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
        if (!actual.Satisfies(v => !Is.EndsWithUsingCaseSensitivity(v, expected, caseSensitivity)))
            throw Fail.ShouldNotEndWith.With(expected).For(actual, customMessage, actualExpression);
    }
}