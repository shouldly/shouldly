using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;

namespace Shouldly;

public static partial class ShouldBeStringTestExtensions
{
    /// <summary>
    /// Asserts that a string starts with another string
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldStartWith([NotNull] this string? actual, string expected, Case caseSensitivity = Case.Insensitive, string? customMessage = null)
    {
        actual.AssertAwesomely(v => Is.StringStartingWithUsingCaseSensitivity(v, expected, caseSensitivity), actual, expected, customMessage);
        Debug.Assert(actual != null);
    }

    /// <summary>
    /// Asserts that a string ends with another string
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldEndWith([NotNull] this string? actual, string expected, Case caseSensitivity = Case.Insensitive, string? customMessage = null)
    {
        actual.AssertAwesomely(v => Is.EndsWithUsingCaseSensitivity(v, expected, caseSensitivity), actual, expected, customMessage);
        Debug.Assert(actual != null);
    }

    /*** ShouldNotStartWith ***/
    /// <summary>
    /// Asserts that a string does not start with another string
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotStartWith(this string? actual, string expected, Case caseSensitivity = Case.Insensitive, string? customMessage = null)
    {
        actual.AssertAwesomely(v => !Is.StringStartingWithUsingCaseSensitivity(v, expected, caseSensitivity), actual, expected, customMessage);
    }

    /*** ShouldNotEndWith ***/
    /// <summary>
    /// Asserts that a string does not end with another string
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotEndWith(this string? actual, string expected, Case caseSensitivity)
    {
        ShouldNotEndWith(actual, expected, null, caseSensitivity);
    }

    /// <summary>
    /// Asserts that a string does not end with another string
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotEndWith(this string? actual, string expected, string? customMessage = null, Case caseSensitivity = Case.Insensitive)
    {
        actual.AssertAwesomely(v => !Is.EndsWithUsingCaseSensitivity(v, expected, caseSensitivity), actual, expected, customMessage);
    }
}