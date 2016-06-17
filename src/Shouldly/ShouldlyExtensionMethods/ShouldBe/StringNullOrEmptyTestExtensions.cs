using System;
using JetBrains.Annotations;

namespace Shouldly
{
    public static partial class ShouldBeStringTestExtensions
    {
        public static void ShouldBeNullOrEmpty(this string actual)
        {
            ShouldBeNullOrEmpty(actual, () => null);
        }

        public static void ShouldBeNullOrEmpty(this string actual, string customMessage)
        {
            ShouldBeNullOrEmpty(actual, () => customMessage);
        }

        public static void ShouldBeNullOrEmpty(this string actual, [InstantHandle] Func<string> customMessage)
        {
            if (!string.IsNullOrEmpty(actual))
                throw new ShouldAssertException(new ActualShouldlyMessage(actual, customMessage).ToString());
        }

        public static void ShouldNotBeNullOrEmpty(this string actual)
        {
            ShouldNotBeNullOrEmpty(actual, () => null);
        }

        public static void ShouldNotBeNullOrEmpty(this string actual, string customMessage)
        {
            ShouldNotBeNullOrEmpty(actual, () => customMessage);
        }

        public static void ShouldNotBeNullOrEmpty(this string actual, [InstantHandle] Func<string> customMessage)
        {
            if (string.IsNullOrEmpty(actual))
                throw new ShouldAssertException(new ActualShouldlyMessage(actual, customMessage).ToString());
        } 
    }
}