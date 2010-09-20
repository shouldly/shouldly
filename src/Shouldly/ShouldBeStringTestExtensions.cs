using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using NUnit.Framework;
using Shouldly;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class ShouldBeStringTestExtensions
    {
        /// <summary>
        /// This method will strip out whitespace (whitespace, tabs, line-endings, etc) and compare the two strings
        /// </summary>
        public static void ShouldContainWithoutWhitespace(this string actual, object expected) {
            ShouldBeCloseTo(actual, expected);
        }
        /// <summary>
        /// This method will strip out whitespace (whitespace, tabs, line-endings, etc) and compare the two strings
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        public static void ShouldBeCloseTo(this string actual, object expected)
        {
//            const string pattern = @"\s+";
//            var testActual = Regex.Split(actual, pattern);
//            var testExpected = Regex.Split((expected ?? "NULL").ToString(), pattern);
//
//            var actualIndex = 0;
//            var expectedIndex = 0; //new StringReader((expected ?? "NULL").ToString());
//
//            var actualBuffer = actual;
//            var expectedBuffer = (expected ?? "NULL").ToString();
//
//            var stringsAreMatching = true;
//            while (stringsAreMatching)
//            {
//                var actualNextBreak = Regex.Match(actualBuffer, pattern).Index;
//                var expectedNextBreak = Regex.Match(expectedBuffer, pattern).Index;
//                if (actualNextBreak > -1 && expectedNextBreak > -1)
//                {
//                    actualIndex += actualNextBreak;
//                    expectedIndex += expectedNextBreak;
//
//                    var actualTest = actualBuffer.Substring(0, actualNextBreak);
//                    var expectedTest = expectedBuffer.Substring(0, expectedNextBreak);
//                    stringsAreMatching = actualTest.Equals(expectedTest, StringComparison.CurrentCultureIgnoreCase);
//
//                    actualBuffer = actualBuffer.Substring(actualNextBreak);
//                    expectedBuffer = expectedBuffer.Substring(actualNextBreak);
//                }
//            }
            var strippedActual = actual.Quotify().StripWhitespace();
            var strippedExpected = (expected ?? "NULL").ToString().Quotify().StripWhitespace();

            strippedActual.AssertAwesomely(Is.EqualTo(strippedExpected), actual, expected);
        }

        public static void ShouldStartWith(this string actual, string expected) {
            actual.AssertAwesomely(Is.StringStarting(expected).IgnoreCase, actual, expected);
        }
        public static void ShouldEndWith(this string actual, string expected) {
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
    }
}