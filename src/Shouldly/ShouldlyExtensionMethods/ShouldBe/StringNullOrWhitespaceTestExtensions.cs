using JetBrains.Annotations;
using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;

namespace Shouldly;

public static partial class ShouldBeStringTestExtensions
{
    /// <summary>
    /// Asserts that a string is null or contains only whitespace characters
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [ContractAnnotation("actual:notnull => halt")]
    public static void ShouldBeNullOrWhiteSpace(this string? actual, string? customMessage = null)
    {
        if (!string.IsNullOrWhiteSpace(actual))
            throw new ShouldAssertException(new ActualShouldlyMessage(actual, customMessage).ToString());
    }

    /// <summary>
    /// Asserts that a string is not null and contains at least one non-whitespace character
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [ContractAnnotation("actual:null => halt")]
    public static void ShouldNotBeNullOrWhiteSpace([NotNull] this string? actual, string? customMessage = null)
    {
        if (string.IsNullOrWhiteSpace(actual))
            throw new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage).ToString());
    }
}