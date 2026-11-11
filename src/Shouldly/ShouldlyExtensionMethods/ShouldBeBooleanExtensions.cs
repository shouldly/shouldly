using System.ComponentModel;
using Shouldly.Internals.Failures;

namespace Shouldly;

/// <summary>
/// Extension methods for boolean assertions
/// </summary>
[DebuggerStepThrough]
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldBeBooleanExtensions
{
    /// <summary>
    /// Asserts that the boolean value is true
    /// </summary>
    public static void ShouldBeTrue(
        [DoesNotReturnIf(false)] this bool actual,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (!actual)
            throw Fail.ShouldBeTrue.For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the nullable boolean value is true
    /// </summary>
    public static void ShouldBeTrue(
        [DoesNotReturnIf(false)] this bool? actual,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (actual is null)
            throw Fail.ShouldBeTrue.For(actual, customMessage, actualExpression);

        if (actual.Value != true)
            throw Fail.ShouldBeTrue.For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the boolean value is false
    /// </summary>
    public static void ShouldBeFalse(
        [DoesNotReturnIf(true)] this bool actual,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (actual)
            throw Fail.ShouldBeFalse.For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the nullable boolean value is false
    /// </summary>
    public static void ShouldBeFalse(
        [DoesNotReturnIf(true)] this bool? actual,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (actual is null)
            throw Fail.ShouldBeFalse.For(actual, customMessage, actualExpression);

        if (actual.Value != false)
            throw Fail.ShouldBeFalse.For(actual, customMessage, actualExpression);
    }
}