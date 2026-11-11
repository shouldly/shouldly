using JetBrains.Annotations;
using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;
using Shouldly.Internals.Failures;

namespace Shouldly;

public static partial class ShouldBeStringTestExtensions
{
    /// <summary>
    /// Asserts that a string is null or empty
    /// </summary>
    public static void ShouldBeNullOrEmpty(this string? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (!string.IsNullOrEmpty(actual))
            throw Fail.ShouldBeNullOrEmpty.For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that a string is not null or empty
    /// </summary>
    [ContractAnnotation("actual:null => halt")]
    public static void ShouldNotBeNullOrEmpty([NotNull] this string? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (string.IsNullOrEmpty(actual))
            throw Fail.ShouldNotBeNullOrEmpty.For(actual, customMessage, actualExpression);
    }
}