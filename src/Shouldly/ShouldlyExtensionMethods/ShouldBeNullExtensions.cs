using System.ComponentModel;
using JetBrains.Annotations;
using Shouldly.Internals;
using Shouldly.Internals.Failures;
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
    [ContractAnnotation("actual:notnull => halt")]
    public static void ShouldBeNull<T>(
        this T? actual,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where T : class
    {
        if (!NullEquivalence.IsNull(actual))
            throw Fail.ShouldBeNull.For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the actual value is null.
    /// </summary>
    [ContractAnnotation("actual:notnull => halt")]
    public static void ShouldBeNull<T>(
        this T? actual,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where T : struct
    {
        if (!NullEquivalence.IsNull(actual))
            throw Fail.ShouldBeNull.For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the actual value is not null.
    /// </summary>
    [ContractAnnotation("actual:null => halt")]
    public static T ShouldNotBeNull<T>(
        [NotNull] this T? actual,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where T : class =>
        actual is { } value && !NullEquivalence.IsNull(value)
            ? value
            : throw Fail.ShouldNotBeNull.For(actual, customMessage, actualExpression);

    /// <summary>
    /// Asserts that the actual value is not null.
    /// </summary>
    [ContractAnnotation("actual:null => halt")]
    public static T ShouldNotBeNull<T>(
        [NotNull] this T? actual,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where T : struct =>
        actual is { } value && !NullEquivalence.IsNull(value)
            ? value
            : throw Fail.ShouldNotBeNull.For(actual, customMessage, actualExpression);
}