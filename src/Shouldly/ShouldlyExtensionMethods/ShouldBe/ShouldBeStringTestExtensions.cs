using System;
using System.Diagnostics;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class ShouldBeStringTestExtensions
    {
        /// <summary>
        /// Perform a string comparison, specifying the desired case sensitivity
        /// </summary>
        public static void ShouldBe(this string actual, string expected, Case caseSensitivity)
        {
            ShouldBe(actual, expected, caseSensitivity, () => null);
        }

        public static void ShouldBe(this string actual, string expected, Case caseSensitivity, string customMessage)
        {
            ShouldBe(actual, expected, caseSensitivity, () => customMessage);
        }

        public static void ShouldBe(this string actual, string expected, Case caseSensitivity, Func<string> customMessage)
        {
            actual.AssertAwesomely(v => (caseSensitivity == Case.Sensitive)
                ? Is.Equal(v, expected)
                : Is.StringEqualIgnoreCase(v, expected), actual, expected, customMessage);
        }

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

        public static void ShouldStartWith(this string actual, string expected)
        {
            ShouldStartWith(actual, expected, () => null);
        }

        public static void ShouldStartWith(this string actual, string expected, string customMessage)
        {
            ShouldStartWith(actual, expected, () => customMessage);
        }

        public static void ShouldStartWith(this string actual, string expected, Func<string> customMessage)
        {
            actual.AssertAwesomely(v => Is.StringStartingWithIgnoreCase(v, expected), actual, expected, customMessage);
        }

        public static void ShouldEndWith(this string actual, string expected)
        {
            ShouldEndWith(actual, expected, () => null);
        }

        public static void ShouldEndWith(this string actual, string expected, string customMessage)
        {
            ShouldEndWith(actual, expected, () => customMessage);
        }

        public static void ShouldEndWith(this string actual, string expected, Func<string> customMessage)
        {
            actual.AssertAwesomely(v => Is.EndsWithIgnoringCase(v, expected), actual, expected, customMessage);
        }

        public static void ShouldNotStartWith(this string actual, string expected)
        {
            ShouldNotStartWith(actual, expected, () => null);
        }

        public static void ShouldNotStartWith(this string actual, string expected, string customMessage)
        {
            ShouldNotStartWith(actual, expected, () => customMessage);
        }

        public static void ShouldNotStartWith(this string actual, string expected, Func<string> customMessage)
        {
            actual.AssertAwesomely(v => !Is.StringStartingWithIgnoreCase(v, expected), actual, expected, customMessage);
        }

        public static void ShouldNotEndWith(this string actual, string expected)
        {
            ShouldNotEndWith(actual, expected, () => null);
        }

        public static void ShouldNotEndWith(this string actual, string expected, string customMessage)
        {
            ShouldNotEndWith(actual, expected, () => customMessage);
        }

        public static void ShouldNotEndWith(this string actual, string expected, Func<string> customMessage)
        {
            actual.AssertAwesomely(v => !Is.EndsWithIgnoringCase(v, expected), actual, expected, customMessage);
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

        public static void ShouldBeNullOrEmpty(this string actual)
        {
            ShouldBeNullOrEmpty(actual, () => null);
        }

        public static void ShouldBeNullOrEmpty(this string actual, string customMessage)
        {
            ShouldBeNullOrEmpty(actual, () => customMessage);
        }

        public static void ShouldBeNullOrEmpty(this string actual, Func<string> customMessage)
        {
            if (!string.IsNullOrEmpty(actual))
                throw new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage()).ToString());
        }

        public static void ShouldNotBeNullOrEmpty(this string actual)
        {
            ShouldNotBeNullOrEmpty(actual, () => null);
        }

        public static void ShouldNotBeNullOrEmpty(this string actual, string customMessage)
        {
            ShouldNotBeNullOrEmpty(actual, () => customMessage);
        }

        public static void ShouldNotBeNullOrEmpty(this string actual, Func<string> customMessage)
        {
            if (string.IsNullOrEmpty(actual))
                throw new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage()).ToString());
        }
    }
}