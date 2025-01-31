using System.ComponentModel;

namespace Shouldly;

[DebuggerStepThrough]
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldBeBooleanExtensions
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeTrue([DoesNotReturnIf(false)] this bool actual, string? customMessage = null)
    {
        if (!actual)
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(true, actual, customMessage).ToString());
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeTrue([DoesNotReturnIf(false)] this bool? actual, string? customMessage = null)
    {
        if (actual is null)
            throw new ShouldAssertException(new ExpectedShouldlyMessage(true, customMessage).ToString());

        if (actual.Value != true)
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(true, actual, customMessage).ToString());
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeFalse([DoesNotReturnIf(true)] this bool actual, string? customMessage = null)
    {
        if (actual)
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(false, actual, customMessage).ToString());
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeFalse([DoesNotReturnIf(true)] this bool? actual, string? customMessage = null)
    {
        if (actual is null)
            throw new ShouldAssertException(new ExpectedShouldlyMessage(false, customMessage).ToString());

        if (actual.Value != false)
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(false, actual, customMessage).ToString());
    }
}