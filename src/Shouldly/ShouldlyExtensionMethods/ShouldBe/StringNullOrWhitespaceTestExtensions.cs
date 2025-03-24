using JetBrains.Annotations;
using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;

namespace Shouldly;

public static partial class ShouldBeStringTestExtensions
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    [ContractAnnotation("actual:notnull => halt")]
    public static void ShouldBeNullOrWhiteSpace(this string? actual, string? customMessage = null)
    {
        if (!string.IsNullOrWhiteSpace(actual))
            throw new ShouldAssertException(new ActualShouldlyMessage(actual, customMessage).ToString());
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    [ContractAnnotation("actual:null => halt")]
    public static void ShouldNotBeNullOrWhiteSpace([NotNull] this string? actual, string? customMessage = null)
    {
        if (string.IsNullOrWhiteSpace(actual))
            throw new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage).ToString());
    }
}