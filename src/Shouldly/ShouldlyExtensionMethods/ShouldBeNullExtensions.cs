using System.ComponentModel;
using JetBrains.Annotations;
using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;

namespace Shouldly;

/// <summary>
/// Extension methods for null assertions
/// </summary>
[DebuggerStepThrough]
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldBeNullExtensions
{
    /// <summary>
    /// Asserts that the actual value is null.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [ContractAnnotation("actual:notnull => halt")]
    public static void ShouldBeNull<T>(this T? actual, string? customMessage = null)
        where T : class
    {
        if (actual != null)
            ThrowHelper.ThrowOrRecord(new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage).ToString()));
    }

    /// <summary>
    /// Asserts that the actual value is null.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [ContractAnnotation("actual:notnull => halt")]
    public static void ShouldBeNull<T>(this T? actual, string? customMessage = null)
        where T : struct
    {
        if (actual != null)
            ThrowHelper.ThrowOrRecord(new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage).ToString()));
    }

    /// <summary>
    /// Asserts that the actual value is not null.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [ContractAnnotation("actual:null => halt")]
    public static T ShouldNotBeNull<T>([NotNull] this T? actual, string? customMessage = null)
        where T : class
    {
        if (actual != null) return actual;
        ThrowHelper.ThrowOrRecord(new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage).ToString()));
#pragma warning disable CS8777 // Parameter must have a non-null value when exiting - ThrowHelper.ThrowOrRecord may not throw when AssertionScope is active
        return default!;
#pragma warning restore CS8777
    }

    /// <summary>
    /// Asserts that the actual value is not null.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [ContractAnnotation("actual:null => halt")]
    public static T ShouldNotBeNull<T>([NotNull] this T? actual, string? customMessage = null)
        where T : struct
    {
        if (actual != null) return actual.Value;
        ThrowHelper.ThrowOrRecord(new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage).ToString()));
#pragma warning disable CS8777
        return default;
#pragma warning restore CS8777
    }
}