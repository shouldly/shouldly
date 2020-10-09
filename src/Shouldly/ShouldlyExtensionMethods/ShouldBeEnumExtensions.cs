using System;

namespace Shouldly.ShouldlyExtensionMethods
{
    [ShouldlyMethods]
    public static partial class ShouldHaveEnumExtensions
    {
        public static void ShouldHaveFlag(this Enum actual, Enum expectedFlag, string? customMessage = null)
        {
            CheckEnumHasFlagAttribute(actual);
            if (!actual.HasFlag(expectedFlag))
            {
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expectedFlag, actual, customMessage).ToString());
            }
        }

        public static void ShouldNotHaveFlag(this Enum actual, Enum expectedFlag, string? customMessage = null)
        {
            CheckEnumHasFlagAttribute(actual);
            if (actual.HasFlag(expectedFlag))
            {
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expectedFlag, actual, customMessage).ToString());
            }
        }

        static void CheckEnumHasFlagAttribute(Enum actual)
        {
            if (!actual.GetType().IsDefined(typeof(FlagsAttribute), false))
            {
                throw new ArgumentException("Enum doesn't have Flags attribute", nameof(actual));
            }
        }
    }
}
