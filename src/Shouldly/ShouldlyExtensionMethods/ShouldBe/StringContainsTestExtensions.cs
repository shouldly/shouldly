using System;

namespace Shouldly
{
    public static partial class ShouldBeStringTestExtensions
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
            ShouldContain(actual, expected, () => null, Case.Insensitive);
        }

        public static void ShouldContain(this string actual, string expected, Case caseSensitivity)
        {
            ShouldContain(actual, expected, () => null, caseSensitivity);
        }

        public static void ShouldContain(this string actual, string expected, string customMessage)
        {
            ShouldContain(actual, expected, () => customMessage, Case.Insensitive);
        }

        public static void ShouldContain(this string actual, string expected, string customMessage, Case caseSensitivity)
        {
            ShouldContain(actual, expected, () => customMessage, caseSensitivity);
        }

        public static void ShouldContain(this string actual, string expected, Func<string> customMessage)
        {
            ShouldContain(actual, expected, customMessage, Case.Insensitive);
        }

        public static void ShouldContain(this string actual, string expected, Func<string> customMessage, Case caseSensitivity)
        {
            actual.AssertAwesomely(
                v => (caseSensitivity == Case.Sensitive) ? Is.StringContainingUsingCaseSensitivity(v, expected) : Is.StringContainingIgnoreCase(v, expected), 
                actual.Clip(100, "..."),
                expected,
                caseSensitivity,
                customMessage);
        }

        public static void ShouldNotContain(this string actual, string expected)
        {
            ShouldNotContain(actual, expected, () => null, Case.Insensitive);
        }

        public static void ShouldNotContain(this string actual, string expected, Case caseSensitivity)
        {
            ShouldNotContain(actual, expected, () => null, caseSensitivity);
        }

        public static void ShouldNotContain(this string actual, string expected, string customMessage)
        {
            ShouldNotContain(actual, expected, () => customMessage, Case.Insensitive);
        }

        public static void ShouldNotContain(this string actual, string expected, string customMessage, Case caseSensitivity)
        {
            ShouldNotContain(actual, expected, () => customMessage, caseSensitivity);
        }

        public static void ShouldNotContain(this string actual, string expected, Func<string> customMessage)
        {
            ShouldNotContain(actual, expected, customMessage, Case.Insensitive);
        }

        public static void ShouldNotContain(this string actual, string expected, Func<string> customMessage, Case caseSensitivity)
        {
            actual.AssertAwesomely(v =>
            {
                var b = (caseSensitivity == Case.Sensitive) ? !Is.StringContainingUsingCaseSensitivity(v, expected) : !Is.StringContainingIgnoreCase(v, expected);
                return b;
            }, actual.Clip(100, "..."), expected, caseSensitivity, customMessage);
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