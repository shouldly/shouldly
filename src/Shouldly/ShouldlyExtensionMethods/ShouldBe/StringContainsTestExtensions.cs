﻿using System;
using JetBrains.Annotations;

namespace Shouldly
{
    public static partial class ShouldBeStringTestExtensions
    {
        /// <summary>
        /// Strip out whitespace (whitespace, tabs, line-endings, etc) and compare the two strings
        /// </summary>
        public static void ShouldContainWithoutWhitespace(this string actual, object? expected, string? customMessage = null)
        {
            var strippedActual = actual.Quotify().StripWhitespace();
            var strippedExpected = (expected?.ToString() ?? "NULL").Quotify().StripWhitespace();

            strippedActual.AssertAwesomely(v => v.Contains(strippedExpected), actual, expected, customMessage);
        }

        public static void ShouldContain(this string actual, string expected, Case caseSensitivity)
        {
            ShouldContain(actual, expected, (string?)null, caseSensitivity);
        }

        public static void ShouldContain(this string actual, string expected, string? customMessage = null)
        {
            ShouldContain(actual, expected, customMessage, Case.Insensitive);
        }

        public static void ShouldContain(this string actual, string expected, string? customMessage, Case caseSensitivity)
        {
            actual.AssertAwesomely(
                v => (caseSensitivity == Case.Sensitive) ? Is.StringContainingUsingCaseSensitivity(v, expected) : Is.StringContainingIgnoreCase(v, expected),
                actual.Clip(100, "..."),
                expected,
                caseSensitivity,
                customMessage);
        }

        public static void ShouldNotContain(this string actual, string expected, Case caseSensitivity)
        {
            ShouldNotContain(actual, expected, (string?)null, caseSensitivity);
        }

        public static void ShouldNotContain(this string actual, string expected, string? customMessage = null)
        {
            ShouldNotContain(actual, expected, customMessage, Case.Insensitive);
        }

        public static void ShouldNotContain(this string actual, string expected, string? customMessage, Case caseSensitivity)
        {
            actual.AssertAwesomely(v =>
            {
                var b = (caseSensitivity == Case.Sensitive) ? !Is.StringContainingUsingCaseSensitivity(v, expected) : !Is.StringContainingIgnoreCase(v, expected);
                return b;
            }, actual.Clip(100, "..."), expected, caseSensitivity, customMessage);
        }

        public static void ShouldMatch(this string actual, string regexPattern, string? customMessage = null)
        {
            actual.AssertAwesomely(v => Is.StringMatchingRegex(v, regexPattern), actual, regexPattern, customMessage);
        }

        public static void ShouldNotMatch(this string actual, string regexPattern, string? customMessage = null)
        {
            actual.AssertAwesomely(v => !Is.StringMatchingRegex(v, regexPattern), actual, regexPattern, customMessage);
        }
    }
}