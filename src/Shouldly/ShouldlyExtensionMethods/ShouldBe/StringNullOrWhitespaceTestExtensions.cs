using System;
using JetBrains.Annotations;

#if net40
namespace Shouldly
{
    public static partial class ShouldBeStringTestExtensions
    {
        public static void ShouldBeNullOrWhiteSpace(this string actual)
        {
            ShouldBeNullOrWhiteSpace(actual, () => null);
        }

        public static void ShouldBeNullOrWhiteSpace(this string actual, string customMessage)
        {
            ShouldBeNullOrWhiteSpace(actual, () => customMessage);
        }

        public static void ShouldBeNullOrWhiteSpace(this string actual, [InstantHandle] Func<string> customMessage)
        {
            if (!string.IsNullOrWhiteSpace(actual))
                throw new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage).ToString());
        }

        public static void ShouldNotBeNullOrWhiteSpace(this string actual)
        {
            ShouldNotBeNullOrWhiteSpace(actual, () => null);
        }

        public static void ShouldNotBeNullOrWhiteSpace(this string actual, string customMessage)
        {
            ShouldNotBeNullOrWhiteSpace(actual, () => customMessage);
        }

        public static void ShouldNotBeNullOrWhiteSpace(this string actual, [InstantHandle] Func<string> customMessage)
        {
            if (string.IsNullOrWhiteSpace(actual))
                throw new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage).ToString());
        }
    }
}
#endif
