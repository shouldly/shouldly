using System.Diagnostics;
using Shouldly.DifferenceHighlighting;

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
            actual.AssertAwesomely(v => (caseSensitivity == Case.Sensitive)
                ? Is.Equal(v, expected)
                : Is.StringEqualIgnoreCase(v, expected), actual, expected);
        }

        /// <summary>
        /// Strip out whitespace (whitespace, tabs, line-endings, etc) and compare the two strings
        /// </summary>
        public static void ShouldContainWithoutWhitespace(this string actual, object expected)
        {
            var strippedActual = actual.Quotify().StripWhitespace();
            var strippedExpected = (expected ?? "NULL").ToString().Quotify().StripWhitespace();

            strippedActual.AssertAwesomely(v => Is.Equal(v, strippedExpected), actual, expected);
        }

        public static void ShouldStartWith(this string actual, string expected)
        {
            actual.AssertAwesomely(v => Is.StringStartingWithIgnoreCase(v, expected), actual, expected);
        }

        public static void ShouldEndWith(this string actual, string expected)
        {
            actual.AssertAwesomely(v => Is.EndsWithIgnoringCase(v, expected), actual, expected);
        }

        public static void ShouldContain(this string actual, string expected)
        {
            actual.AssertAwesomely(v => Is.StringContainingIgnoreCase(v, expected), actual.Clip(100), expected);
        }

        public static void ShouldNotContain(this string actual, string expected)
        {
            actual.AssertAwesomely(v => !Is.StringContainingIgnoreCase(v, expected), actual, expected);
        }

        public static void ShouldMatch(this string actual, string regexPattern)
        {
            actual.AssertAwesomely(v => Is.StringMatchingRegex(v, regexPattern), actual, regexPattern);
        }

        public static void ShouldBeNullOrEmpty(this string actual)
        {
            if (!string.IsNullOrEmpty(actual))
                throw new ChuckedAWobbly(new ShouldlyMessage(actual).ToString());
        }

        public static void ShouldNotBeNullOrEmpty(this string actual)
        {
            if (string.IsNullOrEmpty(actual))
                throw new ChuckedAWobbly(new ShouldlyMessage(actual).ToString());
        }

    }
}