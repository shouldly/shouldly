using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;

namespace Shouldly;

public static partial class ShouldBeStringTestExtensions
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldStartWith([NotNull] this string? actual, string expected, Case caseSensitivity = Case.Insensitive, string? customMessage = null)
    {
        actual.AssertAwesomely(v => Is.StringStartingWithUsingCaseSensitivity(v, expected, caseSensitivity), actual, expected, customMessage);
        Debug.Assert(actual != null);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldEndWith([NotNull] this string? actual, string expected, Case caseSensitivity = Case.Insensitive, string? customMessage = null)
    {
        actual.AssertAwesomely(v => Is.EndsWithUsingCaseSensitivity(v, expected, caseSensitivity), actual, expected, customMessage);
        Debug.Assert(actual != null);
    }

    /*** ShouldNotStartWith ***/
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotStartWith(this string? actual, string expected, Case caseSensitivity = Case.Insensitive, string? customMessage = null)
    {
        actual.AssertAwesomely(v => !Is.StringStartingWithUsingCaseSensitivity(v, expected, caseSensitivity), actual, expected, customMessage);
    }

    /*** ShouldNotEndWith ***/
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotEndWith(this string? actual, string expected, Case caseSensitivity)
    {
        ShouldNotEndWith(actual, expected, (string?)null, caseSensitivity);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotEndWith(this string? actual, string expected, string? customMessage = null, Case caseSensitivity = Case.Insensitive)
    {
        actual.AssertAwesomely(v => !Is.EndsWithUsingCaseSensitivity(v, expected, caseSensitivity), actual, expected, customMessage);
    }
}