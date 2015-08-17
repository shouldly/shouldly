using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class ShouldBeNullExtensions
    {
        public static void ShouldBeNull<T>(this T actual)
        {
            ShouldBeNull(actual, () => null);
        }

        public static void ShouldBeNull<T>(this T actual, string customMessage)
        {
            ShouldBeNull(actual, () => customMessage);
        }

        public static void ShouldBeNull<T>(this T actual, [InstantHandle]Func<string> customMessage)
        {
            if (actual != null)
                throw new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage).ToString());
        }

        public static void ShouldNotBeNull<T>(this T actual)
        {
            ShouldNotBeNull(actual, () => null);
        }

        public static void ShouldNotBeNull<T>(this T actual, string customMessage)
        {
            ShouldNotBeNull(actual, () => customMessage);
        }

        public static void ShouldNotBeNull<T>(this T actual, [InstantHandle]Func<string> customMessage)
        {
            if (actual == null)
                throw new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage).ToString());
        }
    }
}