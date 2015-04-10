using System;
using System.Diagnostics;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class StartEndTestExtensions
    {
        /*** ShouldStartWith ***/
        public static void ShouldStartWith(this string actual, string expected, Case caseSensitivity = Case.Insensitive)
        {
            ShouldStartWith(actual, expected, () => null, caseSensitivity);
        }

        public static void ShouldStartWith(this string actual, string expected, string customMessage, Case caseSensitivity = Case.Insensitive)
        {
            ShouldStartWith(actual, expected, () => customMessage, caseSensitivity);
        }

        public static void ShouldStartWith(this string actual, string expected, Func<string> customMessage, Case caseSensitivity = Case.Insensitive)
        {
            actual.AssertAwesomely(v => Is.StringStartingWithUsingCaseSensitivity(v, expected, caseSensitivity), actual, expected, customMessage);
        }

        /*** ShouldEndWith ***/
        public static void ShouldEndWith(this string actual, string expected, Case caseSensitivity = Case.Insensitive)
        {
            ShouldEndWith(actual, expected, () => null, caseSensitivity);
        }

        public static void ShouldEndWith(this string actual, string expected, string customMessage, Case caseSensitivity = Case.Insensitive)
        {
            ShouldEndWith(actual, expected, () => customMessage, caseSensitivity);
        }

        public static void ShouldEndWith(this string actual, string expected, Func<string> customMessage, Case caseSensitivity = Case.Insensitive)
        {
            actual.AssertAwesomely(v => Is.EndsWithUsingCaseSensitivity(v, expected, caseSensitivity), actual, expected, customMessage);
        }

        /*** ShouldNotStartWith ***/
        public static void ShouldNotStartWith(this string actual, string expected, Case caseSensitivity = Case.Insensitive)
        {
            ShouldNotStartWith(actual, expected, () => null, caseSensitivity);
        }

        public static void ShouldNotStartWith(this string actual, string expected, string customMessage, Case caseSensitivity = Case.Insensitive)
        {
            ShouldNotStartWith(actual, expected, () => customMessage, caseSensitivity);
        }

        public static void ShouldNotStartWith(this string actual, string expected, Func<string> customMessage, Case caseSensitivity = Case.Insensitive)
        {
            actual.AssertAwesomely(v => !Is.StringStartingWithUsingCaseSensitivity(v, expected, caseSensitivity), actual, expected, customMessage);
        }

        /*** ShouldNotEndWith ***/
        public static void ShouldNotEndWith(this string actual, string expected, Case caseSensitivity = Case.Insensitive)
        {
            ShouldNotEndWith(actual, expected, () => null, caseSensitivity);
        }

        public static void ShouldNotEndWith(this string actual, string expected, string customMessage, Case caseSensitivity = Case.Insensitive)
        {
            ShouldNotEndWith(actual, expected, () => customMessage, caseSensitivity);
        }

        public static void ShouldNotEndWith(this string actual, string expected, Func<string> customMessage, Case caseSensitivity = Case.Insensitive)
        {
            actual.AssertAwesomely(v => !Is.EndsWithUsingCaseSensitivity(v, expected, caseSensitivity), actual, expected, customMessage);
        }
    }
}