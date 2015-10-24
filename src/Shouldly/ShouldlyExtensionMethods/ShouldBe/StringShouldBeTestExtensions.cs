using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Shouldly.Internals.AssertionFactories;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static partial class ShouldBeStringTestExtensions
    {
        /// <summary>
        /// Perform a case sensitive string comparison
        /// </summary>
        [Obsolete("Use the ShouldBeStringOptions instead of the Case enum")]
        public static void ShouldBe(this string actual, string expected, Case caseSensitivity)
        {
            ShouldBe(actual, expected, () => null, caseSensitivity.ToOptions());
        }

        [Obsolete("Use the ShouldBeStringOptions instead of the Case enum")]
        public static void ShouldBe(this string actual, string expected, Case caseSensitivity, string customMessage)
        {
            ShouldBe(actual, expected, () => customMessage, caseSensitivity.ToOptions());
        }

        [Obsolete("Use the ShouldBeStringOptions instead of the Case enum")]
        public static void ShouldBe(this string actual, string expected, Case caseSensitivity, [InstantHandle] Func<string> customMessage)
        {
            ShouldBe(actual, expected, customMessage, caseSensitivity.ToOptions());
        }

        /// <summary>
        /// Perform a string comparison with sensitivity options
        /// </summary>
        public static void ShouldBe(
            this string actual,
            string expected,
            ShouldBeStringOptions options = ShouldBeStringOptions.None)
        {
            ShouldBe(actual, expected, () => null, options);
        }
        public static void ShouldBe(
            this string actual,
            string expected,
            string customMessage,
            ShouldBeStringOptions options = ShouldBeStringOptions.None)
        {
            ShouldBe(actual, expected, () => customMessage, options);
        }
        public static void ShouldBe(
            this string actual,
            string expected,
            Func<string> customMessage,
            ShouldBeStringOptions options = ShouldBeStringOptions.None)
        {
            var assertion = StringShouldBeAssertionFactory.Create(expected, actual, options);
            ExecuteAssertion(assertion, customMessage);
        }
        private static void ExecuteAssertion(Internals.Assertions.IAssertion assertion, Func<string> customMessage)
        {
            try
            {
                if (assertion.IsSatisfied()) return;
            }
            catch (ArgumentException ex)
            {
                throw new ShouldAssertException(ex.Message, ex);
            }
            throw new ShouldAssertException(assertion.GenerateMessage(customMessage()));
        }
    }
}