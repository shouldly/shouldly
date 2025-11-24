using JetBrains.Annotations;

namespace Shouldly;

/// <summary>
/// Extension methods for string assertions
/// </summary>
public static partial class ShouldBeStringTestExtensions
{
    extension(string actual)
    {
        /// <summary>
        /// Strip out whitespace (whitespace, tabs, line-endings, etc) and compare the two strings
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldContainWithoutWhitespace(object? expected, string? customMessage = null)
        {
            var strippedActual = actual.Quotify().StripWhitespace();
            var strippedExpected = (expected?.ToString() ?? "NULL").Quotify().StripWhitespace();

            strippedActual.AssertAwesomely(v => v.Contains(strippedExpected), actual, expected, customMessage);
        }

        /// <summary>
        /// Asserts that a string contains another string
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldContain(string expected, Case caseSensitivity = Case.Insensitive, string? customMessage = null)
        {
            actual.AssertAwesomely(
                v => caseSensitivity == Case.Sensitive ? Is.StringContainingUsingCaseSensitivity(v, expected) : Is.StringContainingIgnoreCase(v, expected),
                actual.Clip(100, "..."),
                expected,
                caseSensitivity,
                customMessage);
        }

        /// <summary>
        /// Asserts that a string does not contain another string
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldNotContain(string expected, Case caseSensitivity = Case.Insensitive, string? customMessage = null)
        {
            actual.AssertAwesomely(v =>
            {
                var b = caseSensitivity == Case.Sensitive ? !Is.StringContainingUsingCaseSensitivity(v, expected) : !Is.StringContainingIgnoreCase(v, expected);
                return b;
            }, actual.Clip(100, "..."), expected, caseSensitivity, customMessage);
        }

        /// <summary>
        /// Asserts that a string matches a regular expression pattern
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldMatch([RegexPattern] string regexPattern, string? customMessage = null)
        {
            actual.AssertAwesomely(v => Is.StringMatchingRegex(v, regexPattern), actual, regexPattern, customMessage);
        }

        /// <summary>
        /// Asserts that a string does not match a regular expression pattern
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldNotMatch([RegexPattern] string regexPattern, string? customMessage = null)
        {
            actual.AssertAwesomely(v => !Is.StringMatchingRegex(v, regexPattern), actual, regexPattern, customMessage);
        }
    }
}