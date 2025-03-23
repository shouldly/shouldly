using System.ComponentModel;
using JetBrains.Annotations;
using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;

namespace Shouldly;

[DebuggerStepThrough]
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldBeNullExtensions
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    [ContractAnnotation("actual:notnull => halt")]
    public static void ShouldBeNull<T>(this T? actual, string? customMessage = null)
        where T : class
    {
        if (actual != null)
            throw new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage).ToString());
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    [ContractAnnotation("actual:notnull => halt")]
    public static void ShouldBeNull<T>(this T? actual, string? customMessage = null)
        where T : struct
    {
        if (actual != null)
            throw new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage).ToString());
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    [ContractAnnotation("actual:null => halt")]
    public static T ShouldNotBeNull<T>([NotNull] this T? actual, string? customMessage = null)
        where T : class =>
        actual ?? throw new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage).ToString());

    [MethodImpl(MethodImplOptions.NoInlining)]
    [ContractAnnotation("actual:null => halt")]
    public static T ShouldNotBeNull<T>([NotNull] this T? actual, string? customMessage = null)
        where T : struct =>
        actual ?? throw new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage).ToString());
}