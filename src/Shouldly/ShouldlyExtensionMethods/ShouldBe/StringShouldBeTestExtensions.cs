using Shouldly.Internals.AssertionFactories;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static partial class ShouldBeStringTestExtensions
    {
        /// <summary>
        /// Perform a string comparison with sensitivity options
        /// </summary>
        public static void ShouldBe(
            [NotNullIfNotNull("expected")] this string? actual,
            [NotNullIfNotNull("actual")] string? expected)
        {
            // ReSharper disable once IntroduceOptionalParameters.Global
            ShouldBe(actual, expected, (StringCompareShould)0);
        }

        /// <summary>
        /// Perform a string comparison with sensitivity options
        /// </summary>
        public static void ShouldBe(
            [NotNullIfNotNull("expected")] this string? actual,
            [NotNullIfNotNull("actual")] string? expected,
            string customMessage)
        {
            // ReSharper disable once IntroduceOptionalParameters.Global
            ShouldBe(actual, expected, customMessage, 0);
        }

        /// <summary>
        /// Perform a string comparison with sensitivity options
        /// </summary>
        public static void ShouldBe(
            [NotNullIfNotNull("expected")] this string? actual,
            [NotNullIfNotNull("actual")] string? expected,
            StringCompareShould options)
        {
            ShouldBe(actual, expected, null, options);
        }

        public static void ShouldBe(
            [NotNullIfNotNull("expected")] this string? actual,
            [NotNullIfNotNull("actual")] string? expected,
            string? customMessage,
            StringCompareShould options)
        {
            var assertion = StringShouldBeAssertionFactory.Create(expected, actual, options);
            ExecuteAssertion(assertion, customMessage);
        }

        static void ExecuteAssertion(Internals.Assertions.IAssertion assertion, string? customMessage)
        {
            try
            {
                if (assertion.IsSatisfied()) return;
            }
            catch (ArgumentException ex)
            {
                throw new ShouldAssertException(ex.Message, ex);
            }
            throw new ShouldAssertException(assertion.GenerateMessage(customMessage));
        }
    }
}