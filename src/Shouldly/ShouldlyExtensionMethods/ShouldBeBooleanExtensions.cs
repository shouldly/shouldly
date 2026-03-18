using System.ComponentModel;

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
            ThrowHelper.ThrowOrRecord(new ShouldAssertException(new ExpectedActualShouldlyMessage(true, actual, customMessage, actualExpression: actualExpression).ToString()));
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
        {
            ThrowHelper.ThrowOrRecord(new ShouldAssertException(new ExpectedShouldlyMessage(true, customMessage, actualExpression: actualExpression).ToString()));
            return;
        }

        if (actual.Value != true)
            ThrowHelper.ThrowOrRecord(new ShouldAssertException(new ExpectedActualShouldlyMessage(true, actual, customMessage, actualExpression: actualExpression).ToString()));
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
            ThrowHelper.ThrowOrRecord(new ShouldAssertException(new ExpectedActualShouldlyMessage(false, actual, customMessage, actualExpression: actualExpression).ToString()));
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
        {
            ThrowHelper.ThrowOrRecord(new ShouldAssertException(new ExpectedShouldlyMessage(false, customMessage, actualExpression: actualExpression).ToString()));
            return;
        }

        if (actual.Value != false)
            ThrowHelper.ThrowOrRecord(new ShouldAssertException(new ExpectedActualShouldlyMessage(false, actual, customMessage, actualExpression: actualExpression).ToString()));
    }
}