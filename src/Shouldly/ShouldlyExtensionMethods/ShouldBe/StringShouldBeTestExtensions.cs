using System;
using System.Diagnostics;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static partial class ShouldBeStringTestExtensions
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
    }
}