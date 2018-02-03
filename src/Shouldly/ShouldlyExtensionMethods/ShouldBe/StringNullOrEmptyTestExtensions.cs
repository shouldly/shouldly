using System;
using JetBrains.Annotations;

namespace Shouldly
{
    public static partial class ShouldBeStringTestExtensions
    {
        [ContractAnnotation("actual:notnull => halt")]
        public static void ShouldBeNullOrEmpty(this string actual)
        {
            ShouldBeNullOrEmpty(actual, () => null);
        }

        [ContractAnnotation("actual:notnull => halt")]
        public static void ShouldBeNullOrEmpty(this string actual, string customMessage)
        {
            ShouldBeNullOrEmpty(actual, () => customMessage);
        }

        [ContractAnnotation("actual:notnull => halt")]
        public static void ShouldBeNullOrEmpty(this string actual, [InstantHandle] Func<string> customMessage)
        {
            if (!string.IsNullOrEmpty(actual))
                throw new ShouldAssertException(new ActualShouldlyMessage(actual, customMessage));
        }

        [ContractAnnotation("actual:null => halt")]
        public static void ShouldNotBeNullOrEmpty(this string actual)
        {
            ShouldNotBeNullOrEmpty(actual, () => null);
        }

        [ContractAnnotation("actual:null => halt")]
        public static void ShouldNotBeNullOrEmpty(this string actual, string customMessage)
        {
            ShouldNotBeNullOrEmpty(actual, () => customMessage);
        }

        [ContractAnnotation("actual:null => halt")]
        public static void ShouldNotBeNullOrEmpty(this string actual, [InstantHandle] Func<string> customMessage)
        {
            if (string.IsNullOrEmpty(actual))
                throw new ShouldAssertException(new ActualShouldlyMessage(actual, customMessage));
        }
    }
}