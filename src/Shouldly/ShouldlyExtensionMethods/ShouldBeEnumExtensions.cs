using System.ComponentModel;
using Shouldly.Internals.Failures;

namespace Shouldly.ShouldlyExtensionMethods;

/// <summary>
/// Extension methods for enum assertions
/// </summary>
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldHaveEnumExtensions
{
    /// <summary>
    /// Asserts that the enum value has the specified flag.
    /// </summary>
    public static void ShouldHaveFlag(this Enum actual, Enum expectedFlag, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        CheckEnumHasFlagAttribute(actual);
        if (!actual.HasFlag(expectedFlag))
        {
            throw Fail.ShouldHaveFlag.With(expectedFlag).For(actual, customMessage, actualExpression);
        }
    }

    /// <summary>
    /// Asserts that the enum value does not have the specified flag.
    /// </summary>
    public static void ShouldNotHaveFlag(this Enum actual, Enum expectedFlag, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        CheckEnumHasFlagAttribute(actual);
        if (actual.HasFlag(expectedFlag))
        {
            throw Fail.ShouldNotHaveFlag.With(expectedFlag).For(actual, customMessage, actualExpression);
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