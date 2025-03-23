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
            throw new ShouldAssertException(new ActualShouldlyMessage(actual, customMessage).ToString());
    }

    /// <summary>
    /// Asserts that a string is not null or empty
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [ContractAnnotation("actual:null => halt")]
    public static void ShouldNotBeNullOrEmpty([NotNull] this string? actual, string? customMessage = null)
    {
        if (string.IsNullOrEmpty(actual))
            throw new ShouldAssertException(new ActualShouldlyMessage(actual, customMessage).ToString());
    }
}