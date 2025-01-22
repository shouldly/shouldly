using System.ComponentModel;

namespace Shouldly.ShouldlyExtensionMethods;

[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldHaveEnumExtensions
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldHaveFlag(this Enum actual, Enum expectedFlag, string? customMessage = null)
    {
        CheckEnumHasFlagAttribute(actual);
        if (!actual.HasFlag(expectedFlag))
        {
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expectedFlag, actual, customMessage).ToString());
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotHaveFlag(this Enum actual, Enum expectedFlag, string? customMessage = null)
    {
        CheckEnumHasFlagAttribute(actual);
        if (actual.HasFlag(expectedFlag))
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
}