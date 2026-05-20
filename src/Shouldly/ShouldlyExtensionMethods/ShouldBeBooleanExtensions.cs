using System.ComponentModel;

namespace Shouldly;

/// <summary>
/// Extension methods for boolean assertions
/// </summary>
[DebuggerStepThrough]
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldBeBooleanExtensions
{
    /// <summary>
    /// Asserts that the boolean value is true
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeTrue([DoesNotReturnIf(false)] this bool actual, string? customMessage = null)
    {
        if (!actual)
            ThrowHelper.ThrowOrRecord(new ShouldAssertException(new ExpectedActualShouldlyMessage(true, actual, customMessage).ToString()));
    }

    /// <summary>
    /// Asserts that the nullable boolean value is true
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeTrue([DoesNotReturnIf(false)] this bool? actual, string? customMessage = null)
    {
        if (actual is null)
        {
            ThrowHelper.ThrowOrRecord(new ShouldAssertException(new ExpectedShouldlyMessage(true, customMessage).ToString()));
            return;
        }

        if (actual.Value != true)
            ThrowHelper.ThrowOrRecord(new ShouldAssertException(new ExpectedActualShouldlyMessage(true, actual, customMessage).ToString()));
    }

    /// <summary>
    /// Asserts that the boolean value is false
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeFalse([DoesNotReturnIf(true)] this bool actual, string? customMessage = null)
    {
        if (actual)
            ThrowHelper.ThrowOrRecord(new ShouldAssertException(new ExpectedActualShouldlyMessage(false, actual, customMessage).ToString()));
    }

    /// <summary>
    /// Asserts that the nullable boolean value is false
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeFalse([DoesNotReturnIf(true)] this bool? actual, string? customMessage = null)
    {
        if (actual is null)
        {
            ThrowHelper.ThrowOrRecord(new ShouldAssertException(new ExpectedShouldlyMessage(false, customMessage).ToString()));
            return;
        }

        if (actual.Value != false)
            ThrowHelper.ThrowOrRecord(new ShouldAssertException(new ExpectedActualShouldlyMessage(false, actual, customMessage).ToString()));
    }
}