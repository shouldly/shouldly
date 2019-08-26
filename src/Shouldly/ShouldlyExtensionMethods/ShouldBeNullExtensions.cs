using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class ShouldBeNullExtensions
    {
        [ContractAnnotation("actual:notnull => halt")]
        public static void ShouldBeNull<T>(this T actual)
        {
            ShouldBeNull(actual, () => null);
        }

        [ContractAnnotation("actual:notnull => halt")]
        public static void ShouldBeNull<T>(this T actual, string customMessage)
        {
            ShouldBeNull(actual, () => customMessage);
        }

        [ContractAnnotation("actual:notnull => halt")]
        public static void ShouldBeNull<T>(this T actual, [InstantHandle]Func<string> customMessage)
        {
            if (actual != null)
                throw new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage).ToString());
        }

        [ContractAnnotation("actual:null => halt")]
        public static T ShouldNotBeNull<T>(this T actual)
        {
            return ShouldNotBeNull(actual, () => null);
        }

        [ContractAnnotation("actual:null => halt")]
        public static T ShouldNotBeNull<T>(this T actual, string customMessage)
        {
            return ShouldNotBeNull(actual, () => customMessage);
        }

        [ContractAnnotation("actual:null => halt")]
        public static T ShouldNotBeNull<T>(this T actual, [InstantHandle]Func<string> customMessage)
        {
            if (actual == null)
                throw new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage).ToString());

            return actual;
        }
    }
}