using System;
using JetBrains.Annotations;
using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;

namespace Shouldly
{
    public static partial class ShouldBeStringTestExtensions
    {
        [ContractAnnotation("actual:notnull => halt")]
        public static void ShouldBeNullOrEmpty(this string? actual, string? customMessage = null)
        {
            ShouldBeNullOrEmpty(actual, () => customMessage);
        }

        [ContractAnnotation("actual:notnull => halt")]
        public static void ShouldBeNullOrEmpty(this string? actual, [InstantHandle] Func<string?>? customMessage)
        {
            if (!string.IsNullOrEmpty(actual))
                throw new ShouldAssertException(new ActualShouldlyMessage(actual, customMessage).ToString());
        }

        [ContractAnnotation("actual:null => halt")]
        public static void ShouldNotBeNullOrEmpty([NotNull] this string? actual, string? customMessage = null)
        {
            ShouldNotBeNullOrEmpty(actual, () => customMessage);
        }

        [ContractAnnotation("actual:null => halt")]
        public static void ShouldNotBeNullOrEmpty([NotNull] this string? actual, [InstantHandle] Func<string?>? customMessage)
        {
            if (string.IsNullOrEmpty(actual))
                throw new ShouldAssertException(new ActualShouldlyMessage(actual, customMessage).ToString());
        }
    }
}