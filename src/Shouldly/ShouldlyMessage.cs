using Shouldly.Internals.Failures;

namespace Shouldly;

/// <summary>
/// Message for assertions that only have an expected value
/// </summary>
public class ExpectedShouldlyMessage : ShouldlyMessage
{
    /// <summary>
    /// Creates a new message with the expected value
    /// </summary>
    public ExpectedShouldlyMessage(object? expected, string? customMessage, [CallerMemberName] string shouldlyMethod = null!, string? actualExpression = null) =>
        Describe(shouldlyMethod, new(MessageShape.Expected, expected), customMessage, actualExpression);
}

/// <summary>
/// Message for assertions that only have an actual value
/// </summary>
public class ActualShouldlyMessage : ShouldlyMessage
{
    /// <summary>
    /// Creates a new message with the actual value
    /// </summary>
    public ActualShouldlyMessage(object? actual, string? customMessage, [CallerMemberName] string shouldlyMethod = null!, string? actualExpression = null) =>
        Describe(shouldlyMethod, new(MessageShape.Actual, actual: actual, hasActual: true), customMessage, actualExpression);
}

/// <summary>
/// Message for assertions that have both expected and actual values
/// </summary>
public class ExpectedActualShouldlyMessage : ShouldlyMessage
{
    /// <summary>
    /// Creates a new message with expected and actual values
    /// </summary>
    public ExpectedActualShouldlyMessage(object? expected, object? actual, string? customMessage, [CallerMemberName] string shouldlyMethod = null!, string? actualExpression = null) =>
        Describe(shouldlyMethod, new(MessageShape.ExpectedActual, expected, actual, hasActual: true), customMessage, actualExpression);
}

/// <summary>
/// Message for assertions that compare expected and actual values with case sensitivity options
/// </summary>
public class ExpectedActualWithCaseSensitivityShouldlyMessage : ShouldlyMessage
{
    /// <summary>
    /// Creates a new message with expected and actual values and case sensitivity setting
    /// </summary>
    public ExpectedActualWithCaseSensitivityShouldlyMessage(object? expected, object? actual,
        Case? caseSensitivity,
        string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null) =>
        Describe(shouldlyMethod, new(MessageShape.CaseSensitivity, expected, actual, hasActual: true, caseSensitivity: caseSensitivity), customMessage, actualExpression);
}

/// <summary>
/// Message for assertions that compare expected and actual values with a tolerance
/// </summary>
public class ExpectedActualToleranceShouldlyMessage : ShouldlyMessage
{
    /// <summary>
    /// Creates a new message with expected and actual values and a tolerance
    /// </summary>
    public ExpectedActualToleranceShouldlyMessage(object? expected, object? actual, object tolerance,
        string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null) =>
        Describe(shouldlyMethod, new(MessageShape.Tolerance, expected, actual, hasActual: true, tolerance: tolerance), customMessage, actualExpression);
}

/// <summary>
/// Message for assertions that check object equivalence
/// </summary>
public class ExpectedEquivalenceShouldlyMessage : ShouldlyMessage
{
    /// <summary>
    /// Creates a new message for equivalence assertions
    /// </summary>
    public ExpectedEquivalenceShouldlyMessage(object? expected, object? actual, IEnumerable<string> path, string? customMessage, [CallerMemberName] string shouldlyMethod = null!, string? actualExpression = null) =>
        Describe(shouldlyMethod, new(MessageShape.Equivalence, expected, actual, path: path), customMessage, actualExpression);
}

/// <summary>
/// Message for assertions that check exceptions
/// </summary>
public class ShouldlyThrowMessage : ShouldlyMessage
{
    /// <summary>
    /// Creates a new message for exception assertions with an exception message
    /// </summary>
    public ShouldlyThrowMessage(object? expected, string exceptionMessage, string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null) =>
        Describe(shouldlyMethod, new(MessageShape.Throw, expected, exceptionMessage: exceptionMessage), customMessage, actualExpression);

    /// <summary>
    /// Creates a new message for exception assertions with expected and actual values
    /// </summary>
    public ShouldlyThrowMessage(object? expected, object? actual, string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null) =>
        Describe(shouldlyMethod, new(MessageShape.Throw, expected, actual, hasActual: true), customMessage, actualExpression);

    /// <summary>
    /// Creates a new message for exception assertions with only an expected value
    /// </summary>
    public ShouldlyThrowMessage(object? expected, string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null) =>
        Describe(shouldlyMethod, new(MessageShape.Throw, expected), customMessage, actualExpression);
}

/// <summary>
/// Base class for all Shouldly assertion messages
/// </summary>
public abstract class ShouldlyMessage
{
    private string? _message;

    /// <summary>
    /// States the failure. The subject is resolved here, so the trip-wire that proves caller argument expressions
    /// are in use trips when the message is constructed.
    /// </summary>
    private protected void Describe(string shouldlyMethod, Slots slots, string? customMessage, string? actualExpression) =>
        _message = CustomAssertionMessage.Text(shouldlyMethod, slots, customMessage, actualExpression);

    /// <summary>
    /// Converts the message to a string representation
    /// </summary>
    public override string ToString() => _message ?? string.Empty;
}
