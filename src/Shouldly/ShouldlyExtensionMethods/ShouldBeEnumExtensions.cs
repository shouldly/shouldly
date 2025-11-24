using System.ComponentModel;

namespace Shouldly.ShouldlyExtensionMethods;

/// <summary>
/// Extension methods for enum assertions
/// </summary>
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldHaveEnumExtensions
{
    extension(Enum actual)
    {
        /// <summary>
        /// Asserts that the enum value has the specified flag.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldHaveFlag(Enum expectedFlag, string? customMessage = null)
        {
            CheckEnumHasFlagAttribute(actual);
            if (!actual.HasFlag(expectedFlag))
            {
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expectedFlag, actual, customMessage).ToString());
            }
        }

        /// <summary>
        /// Asserts that the enum value does not have the specified flag.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldNotHaveFlag(Enum expectedFlag, string? customMessage = null)
        {
            CheckEnumHasFlagAttribute(actual);
            if (actual.HasFlag(expectedFlag))
            {
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expectedFlag, actual, customMessage).ToString());
            }
        }
    }

    private static void CheckEnumHasFlagAttribute(Enum actual)
    {
        if (!actual.GetType().IsDefined(typeof(FlagsAttribute), false))
        {
            throw new ArgumentException("Enum doesn't have Flags attribute", nameof(actual));
        }
    }
}