using System;
using System.Diagnostics;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class NullOrEmptyTestExtensions
    {
        public static void ShouldBeNullOrEmpty(this string actual)
        {
            ShouldBeNullOrEmpty(actual, () => null);
        }

        public static void ShouldBeNullOrEmpty(this string actual, string customMessage)
        {
            ShouldBeNullOrEmpty(actual, () => customMessage);
        }

        public static void ShouldBeNullOrEmpty(this string actual, Func<string> customMessage)
        {
            if (!string.IsNullOrEmpty(actual))
                throw new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage()).ToString());
        }

        public static void ShouldNotBeNullOrEmpty(this string actual)
        {
            ShouldNotBeNullOrEmpty(actual, () => null);
        }

        public static void ShouldNotBeNullOrEmpty(this string actual, string customMessage)
        {
            ShouldNotBeNullOrEmpty(actual, () => customMessage);
        }

        public static void ShouldNotBeNullOrEmpty(this string actual, Func<string> customMessage)
        {
            if (string.IsNullOrEmpty(actual))
                throw new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage()).ToString());
        } 
    }
}