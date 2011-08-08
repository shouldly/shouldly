using System.Text.RegularExpressions;
using System.Diagnostics;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class ShouldBeStringTestExtensions
    {
        /// <summary>
        /// Strip out whitespace (whitespace, tabs, line-endings, etc) and compare the two strings
        /// </summary>
        public static void ShouldContainWithoutWhitespace(this string actual, object expected)
        {
            ShouldBeCloseTo(actual, expected);
        }

        /// <summary>
        /// Strip out whitespace (whitespace, tabs, line-endings, etc) and compare the two strings
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        public static void ShouldBeCloseTo(this string actual, object expected)
        {
            var strippedActual = actual.Quotify().StripWhitespace();
            var strippedExpected = (expected ?? "NULL").ToString().Quotify().StripWhitespace();

            strippedActual.AssertAwesomely(Is.EqualTo(strippedExpected), actual, expected);
        }

        /// <summary>
        /// Compare two strings ignoring case
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        public static void ShouldBeEqualIgnoringCase(this string actual, string expected)
        {
            actual.AssertAwesomely(Is.EqualTo(expected).IgnoreCase, actual, expected);
        }

        public static void ShouldStartWith(this string actual, string expected)
        {
            actual.AssertAwesomely(Is.StringStarting(expected).IgnoreCase, actual, expected);
        }

        public static void ShouldEndWith(this string actual, string expected)
        {
            actual.AssertAwesomely(Is.StringEnding(expected).IgnoreCase, actual, expected);
        }

        public static void ShouldContain(this string actual, string expected)
        {
            actual.AssertAwesomely(Is.StringContaining(expected).IgnoreCase, actual, expected);
        }

        public static void ShouldNotContain(this string actual, string expected)
        {
            actual.AssertAwesomely(Is.Not.StringContaining(expected).IgnoreCase, actual, expected);
        }

        public static void ShouldMatch(this string actual, string regexPattern)
        {
            actual.AssertAwesomely(Is.StringMatching(regexPattern), actual, regexPattern);
        }
    }
}