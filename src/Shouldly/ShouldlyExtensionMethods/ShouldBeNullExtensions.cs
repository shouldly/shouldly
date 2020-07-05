using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class ShouldBeNullExtensions
    {
        [ContractAnnotation("actual:notnull => halt")]
        public static void ShouldBeNull<T>([MaybeNull] this T actual)
            where T : class
        {
            ShouldBeNull(actual, () => null);
        }

        [ContractAnnotation("actual:notnull => halt")]
        public static void ShouldBeNull<T>([MaybeNull] this T actual, string? customMessage)
            where T : class
        {
            ShouldBeNull(actual, () => customMessage);
        }

        [ContractAnnotation("actual:notnull => halt")]
        public static void ShouldBeNull<T>([MaybeNull] this T actual, [InstantHandle] Func<string?>? customMessage)
            where T : class
        {
            if (actual != null)
                throw new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage).ToString());
        }

        [ContractAnnotation("actual:null => halt")]
        public static void ShouldNotBeNull<T>([NotNull] this T actual)
            where T : class
        {
            ShouldNotBeNull(actual, () => null);
        }

        [ContractAnnotation("actual:null => halt")]
        public static void ShouldNotBeNull<T>([NotNull] this T actual, string? customMessage)
            where T : class
        {
            ShouldNotBeNull(actual, () => customMessage);
        }

        [ContractAnnotation("actual:null => halt")]
        public static void ShouldNotBeNull<T>([NotNull] this T actual, [InstantHandle] Func<string?>? customMessage)
            where T : class
        {
            if (actual == null)
                throw new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage).ToString());
        }
    }
}