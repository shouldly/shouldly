#define PORTABLE

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Shouldly.ShouldlyExtensionMethods
{
    public static class ShouldHaveEnumExtensions
    {
        public static void ShouldHaveFlag(this Enum actual, Enum expectedFlag) 
            => ShouldHaveFlag(actual, expectedFlag, () => null);

        public static void ShouldHaveFlag(this Enum actual, Enum expectedFlag, string customMessage) 
            => ShouldHaveFlag(actual, expectedFlag, () => customMessage);
        
        public static void ShouldHaveFlag(this Enum actual, Enum expectedFlag, [InstantHandle] Func<string> customMessage)
        {
            CheckEnumHasFlagAttribute(actual);
#if net35
            if (!actual.HasFlag(expectedFlag))
#elif net40
            if (!actual.HasFlag(expectedFlag))
#endif
            {
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expectedFlag, actual, customMessage).ToString());
            }
        }

        public static void ShouldNotHaveFlag(this Enum actual, Enum expectedFlag)
            => ShouldNotHaveFlag(actual, expectedFlag, () => null);

        public static void ShouldNotHaveFlag(this Enum actual, Enum expectedFlag, string customMessage)
            => ShouldNotHaveFlag(actual, expectedFlag, () => customMessage);

        public static void ShouldNotHaveFlag(this Enum actual, Enum expectedFlag,
            [InstantHandle] Func<string> customMessage)
        {
            CheckEnumHasFlagAttribute(actual);
#if net35
            if (actual.HasFlag(expectedFlag))
#elif net40
            if (actual.HasFlag(expectedFlag))
#endif
            {
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expectedFlag, actual, customMessage).ToString());
            }    
        }

        private static void CheckEnumHasFlagAttribute(Enum actual)
        {
            if (!actual.GetType().IsDefined(typeof(FlagsAttribute), false))
            {
                throw new ArgumentException("Enum doesn't have Flags attribute", nameof(actual));
            }
        }

#if net35
        /* If the .NET Framework doesn't have a HasFlag, patch in our own version. 
            Made by decompiling the HasFlag function on enum in .NET Framework version 4.5.1 */
        private static bool HasFlag(this Enum enumeration, Enum value)
        {
            if (enumeration == null)
            {
                return false;
            }

            if (value == null)
            {
                throw new ArgumentException(nameof(value));
            }

            if (!Enum.IsDefined(enumeration.GetType(), value))
            {
                throw new ArgumentException($"Enumeration type mismatch. The flag is of type '{value.GetType()}', was expecting {enumeration.GetType()}");
            }

            ulong longValue = Convert.ToUInt64(value);

            return (Convert.ToUInt64(enumeration) & longValue) == longValue;
        }
#endif
    }
}
