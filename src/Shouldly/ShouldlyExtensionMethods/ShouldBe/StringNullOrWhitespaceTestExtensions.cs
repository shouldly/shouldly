using System;
using JetBrains.Annotations;
using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;

namespace Shouldly
{
    public static partial class ShouldBeStringTestExtensions
    {
        [ContractAnnotation("actual:notnull => halt")]
        public static void ShouldBeNullOrWhiteSpace(this string? actual)
        {
            ShouldBeNullOrWhiteSpace(actual, () => null);
        }

        [ContractAnnotation("actual:notnull => halt")]
        public static void ShouldBeNullOrWhiteSpace(this string? actual, string? customMessage)
        {
            ShouldBeNullOrWhiteSpace(actual, () => customMessage);
        }

        [ContractAnnotation("actual:notnull => halt")]
        public static void ShouldBeNullOrWhiteSpace(this string? actual, [InstantHandle] Func<string?>? customMessage)
        {
            if (!actual.IsNullOrWhiteSpace())
                throw new ShouldAssertException(new ActualShouldlyMessage(actual, customMessage).ToString());
        }

        [ContractAnnotation("actual:null => halt")]
        public static void ShouldNotBeNullOrWhiteSpace([NotNull] this string? actual)
        {
            ShouldNotBeNullOrWhiteSpace(actual, () => null);
        }

        [ContractAnnotation("actual:null => halt")]
        public static void ShouldNotBeNullOrWhiteSpace([NotNull] this string? actual, string? customMessage)
        {
            ShouldNotBeNullOrWhiteSpace(actual, () => customMessage);
        }

        [ContractAnnotation("actual:null => halt")]
        public static void ShouldNotBeNullOrWhiteSpace([NotNull] this string? actual, [InstantHandle] Func<string?>? customMessage)
        {
            // TODO make this an extension method (str.IsNullOrWhitespace())
            if (actual == null || string.IsNullOrWhiteSpace(actual))
                throw new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage).ToString());
        }
    }
}
