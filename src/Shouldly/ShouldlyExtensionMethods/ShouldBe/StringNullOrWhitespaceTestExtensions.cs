using JetBrains.Annotations;
using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;

namespace Shouldly;

public static partial class ShouldBeStringTestExtensions
{
    [ContractAnnotation("actual:notnull => halt")]
    public static void ShouldBeNullOrWhiteSpace(this string? actual, string? customMessage = null)
    {
        if (!actual.IsNullOrWhiteSpace())
            throw new ShouldAssertException(new ActualShouldlyMessage(actual, customMessage).ToString());
    }

    [ContractAnnotation("actual:null => halt")]
    public static void ShouldNotBeNullOrWhiteSpace([NotNull] this string? actual, string? customMessage = null)
    {
        // TODO make this an extension method (str.IsNullOrWhitespace())
        if (string.IsNullOrWhiteSpace(actual))
            throw new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage).ToString());
    }
}