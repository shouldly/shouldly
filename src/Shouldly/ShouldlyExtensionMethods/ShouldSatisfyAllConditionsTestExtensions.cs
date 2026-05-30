using System.ComponentModel;
using JetBrains.Annotations;

namespace Shouldly;

/// <summary>
/// Provides extension methods for validating that objects satisfy multiple conditions
/// </summary>
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldSatisfyAllConditionsTestExtensions
{
    /// <summary>
    /// Asserts that the actual value satisfies all specified conditions.
    /// </summary>
    public static void ShouldSatisfyAllConditions<T>(this T actual, [InstantHandle] Action<T>[] conditions, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        ShouldSatisfyAllConditions(actual, CreateParameterlessActions(actual, conditions), customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the actual object satisfies all specified conditions with a custom message
    /// </summary>
    public static void ShouldSatisfyAllConditions(this object? actual, [InstantHandle] Action[] conditions, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var errorMessages = new List<Exception>();
        foreach (var action in conditions)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception exc)
            {
                errorMessages.Add(exc);
            }
        }

        if (errorMessages.Any())
        {
            var errorMessageString = BuildErrorMessageString(errorMessages);
            ThrowHelper.ThrowOrRecord(new ShouldAssertException(new ExpectedActualShouldlyMessage(errorMessageString, actual, customMessage, actualExpression: actualExpression).ToString()));
        }
    }

    private static Action[] CreateParameterlessActions<T>(T parameter, params Action<T>[] actions) {
        return actions.Select(a => new Action(() => a(parameter))).ToArray();
    }

    private static string BuildErrorMessageString(IEnumerable<Exception> errorMessages) =>
        AssertionErrorFormatter.FormatErrors(errorMessages.Select(e => e.Message));
}
