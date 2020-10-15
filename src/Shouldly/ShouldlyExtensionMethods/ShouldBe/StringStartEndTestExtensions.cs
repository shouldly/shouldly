using System.Diagnostics;
using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;

namespace Shouldly
{
    public static partial class ShouldBeStringTestExtensions
    {
        public static void ShouldStartWith([NotNull] this string? actual, string expected, Case caseSensitivity = Case.Insensitive, string? customMessage = null)
        {
            actual.AssertAwesomely(v => Is.StringStartingWithUsingCaseSensitivity(v, expected, caseSensitivity), actual, expected, customMessage);
            Debug.Assert(actual != null);
        }

        public static void ShouldEndWith([NotNull] this string? actual, string expected, Case caseSensitivity = Case.Insensitive, string? customMessage = null)
        {
            actual.AssertAwesomely(v => Is.EndsWithUsingCaseSensitivity(v, expected, caseSensitivity), actual, expected, customMessage);
            Debug.Assert(actual != null);
        }

        /*** ShouldNotStartWith ***/
        public static void ShouldNotStartWith(this string? actual, string expected, Case caseSensitivity = Case.Insensitive, string? customMessage = null)
        {
            actual.AssertAwesomely(v => !Is.StringStartingWithUsingCaseSensitivity(v, expected, caseSensitivity), actual, expected, customMessage);
        }

        /*** ShouldNotEndWith ***/
        public static void ShouldNotEndWith(this string? actual, string expected, Case caseSensitivity)
        {
            ShouldNotEndWith(actual, expected, (string?)null, caseSensitivity);
        }

        public static void ShouldNotEndWith(this string? actual, string expected, string? customMessage = null, Case caseSensitivity = Case.Insensitive)
        {
            actual.AssertAwesomely(v => !Is.EndsWithUsingCaseSensitivity(v, expected, caseSensitivity), actual, expected, customMessage);
        }
    }
}