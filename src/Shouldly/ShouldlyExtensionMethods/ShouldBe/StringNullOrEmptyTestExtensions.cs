using JetBrains.Annotations;
using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;

namespace Shouldly;

public static partial class ShouldBeStringTestExtensions
{
    /// <summary>
    /// Asserts that a string is null or empty
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [ContractAnnotation("actual:notnull => halt")]
    public static void ShouldBeNullOrEmpty(this string? actual, string? customMessage = null)
    {
        if (!string.IsNullOrEmpty(actual))
            ThrowHelper.ThrowOrRecord(new ShouldAssertException(new ActualShouldlyMessage(actual, customMessage).ToString()));
    }

    /// <summary>
    /// Asserts that a string is not null or empty
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [ContractAnnotation("actual:null => halt")]
#pragma warning disable CS8777 // Parameter must have a non-null value when exiting - ThrowHelper.ThrowOrRecord may not throw when AssertionScope is active
    public static void ShouldNotBeNullOrEmpty([NotNull] this string? actual, string? customMessage = null)
    {
        if (string.IsNullOrEmpty(actual))
            ThrowHelper.ThrowOrRecord(new ShouldAssertException(new ActualShouldlyMessage(actual, customMessage).ToString()));
    }
#pragma warning restore CS8777
}