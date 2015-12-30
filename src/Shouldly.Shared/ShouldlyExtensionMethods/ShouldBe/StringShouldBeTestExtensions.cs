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
        [Obsolete("Use the StringCompareShould enum instead of the Case enum")]
        public static void ShouldBe(this string actual, string expected, Case caseSensitivity)
        {
            ShouldBe(actual, expected, caseSensitivity, () => null);
        }

        [Obsolete("Use the StringCompareShould enum instead of the Case enum")]
        public static void ShouldBe(this string actual, string expected, Case caseSensitivity, string customMessage)
        {
            ShouldBe(actual, expected, caseSensitivity, () => customMessage);
        }

        [Obsolete("Use the StringCompareShould enum instead of the Case enum")]
        public static void ShouldBe(this string actual, string expected, Case caseSensitivity, [InstantHandle] Func<string> customMessage)
        {
            if (caseSensitivity == Case.Sensitive)
                ShouldBe(actual, expected, customMessage);
            else
                ShouldBe(actual, expected, customMessage, StringCompareShould.IgnoreCase);
        }

        /// <summary>
        /// Perform a string comparison with sensitivity options
        /// </summary>
        public static void ShouldBe(
            this string actual,
            string expected)
        {
            // ReSharper disable once IntroduceOptionalParameters.Global
            ShouldBe(actual, expected, (StringCompareShould)0);
        }

        /// <summary>
        /// Perform a string comparison with sensitivity options
        /// </summary>
        public static void ShouldBe(
            this string actual,
            string expected,
            string customMessage)
        {
            // ReSharper disable once IntroduceOptionalParameters.Global
            ShouldBe(actual, expected, () => customMessage, 0);
        }

        /// <summary>
        /// Perform a string comparison with sensitivity options
        /// </summary>
        public static void ShouldBe(
            this string actual,
            string expected,
            Func<string> customMessage)
        {
            // ReSharper disable once IntroduceOptionalParameters.Global
            ShouldBe(actual, expected, customMessage, 0);
        }
        /// <summary>
        /// Perform a string comparison with sensitivity options
        /// </summary>
        public static void ShouldBe(
            this string actual,
            string expected,
            StringCompareShould options)
        {
            ShouldBe(actual, expected, () => null, options);
        }
        public static void ShouldBe(
            this string actual,
            string expected,
            string customMessage,
            StringCompareShould option)
        {
            ShouldBe(actual, expected, () => customMessage, option);
        }

        public static void ShouldBe(
            this string actual,
            string expected,
            Func<string> customMessage,
            StringCompareShould options)
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