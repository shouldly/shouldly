using System;
using System.Diagnostics;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class ContainsTestExtensions
    {
        /// <summary>
        /// Strip out whitespace (whitespace, tabs, line-endings, etc) and compare the two strings
        /// </summary>
        public static void ShouldContainWithoutWhitespace(this string actual, object expected)
        {
            ShouldContainWithoutWhitespace(actual, expected, () => null);
        }

        public static void ShouldContainWithoutWhitespace(this string actual, object expected, string customMessage)
        {
            ShouldContainWithoutWhitespace(actual, expected, () => customMessage);
        }

        public static void ShouldContainWithoutWhitespace(this string actual, object expected, Func<string> customMessage)
        {
            var strippedActual = actual.Quotify().StripWhitespace();
            var strippedExpected = (expected ?? "NULL").ToString().Quotify().StripWhitespace();

            strippedActual.AssertAwesomely(v => Is.Equal(v, strippedExpected), actual, expected, customMessage);
        }

        public static void ShouldContain(this string actual, string expected)
        {
            ShouldContain(actual, expected, () => null);
        }

        public static void ShouldContain(this string actual, string expected, string customMessage)
        {
            ShouldContain(actual, expected, () => customMessage);
        }

        public static void ShouldContain(this string actual, string expected, Func<string> customMessage)
        {
            actual.AssertAwesomely(v => Is.StringContainingIgnoreCase(v, expected), actual.Clip(100), expected, customMessage);
        }

        public static void ShouldNotContain(this string actual, string expected)
        {
            ShouldNotContain(actual, expected, () => null);
        }

        public static void ShouldNotContain(this string actual, string expected, string customMessage)
        {
            ShouldNotContain(actual, expected, () => customMessage);
        }

        public static void ShouldNotContain(this string actual, string expected, Func<string> customMessage)
        {
            actual.AssertAwesomely(v => !Is.StringContainingIgnoreCase(v, expected), actual, expected, customMessage);
        }

        public static void ShouldMatch(this string actual, string regexPattern)
        {
            ShouldMatch(actual, regexPattern, () => null);
        }

        public static void ShouldMatch(this string actual, string regexPattern, string customMessage)
        {
            ShouldMatch(actual, regexPattern, () => customMessage);
        }

        public static void ShouldMatch(this string actual, string regexPattern, Func<string> customMessage)
        {
            actual.AssertAwesomely(v => Is.StringMatchingRegex(v, regexPattern), actual, regexPattern, customMessage);
        } 
    }
}