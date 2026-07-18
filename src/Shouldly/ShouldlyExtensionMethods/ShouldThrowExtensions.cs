using System.ComponentModel;

namespace Shouldly;

/// <summary>
/// Extension methods for exception assertions
/// </summary>
[DebuggerStepThrough]
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldThrowExtensions
{
    /// <summary>
    /// Verifies that the action throws a <typeparamref name="TException"/> exception.
    /// </summary>
    public static TException ShouldThrow<TException>(this Action actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where TException : Exception =>
        Should.ThrowInternal<TException>(actual, customMessage, actualExpression: actualExpression);

    /// <summary>
    /// Verifies that the function throws a <typeparamref name="TException"/> exception.
    /// </summary>
    public static TException ShouldThrow<TException>(this Func<object?> actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where TException : Exception =>
        Should.ThrowInternal<TException>(actual, customMessage, actualExpression: actualExpression);

    /// <summary>
    /// Verifies that the action throws an exception of the specified type.
    /// </summary>
    public static Exception ShouldThrow(this Action actual, Type exceptionType, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
        Should.ThrowInternal(actual, customMessage, exceptionType, actualExpression: actualExpression);

    /// <summary>
    /// Verifies that the function throws an exception of the specified type.
    /// </summary>
    public static Exception ShouldThrow(this Func<object?> actual, Type exceptionType, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
        Should.ThrowInternal(actual, customMessage, exceptionType, actualExpression: actualExpression);

    /// <summary>
    /// Verifies that the action completes without throwing any exceptions.
    /// </summary>
    public static void ShouldNotThrow(this Action action, string? customMessage = null,
        [CallerArgumentExpression(nameof(action))] string? actualExpression = null) =>
        Should.NotThrowInternal(action, customMessage, actualExpression: actualExpression);

    /// <summary>
    /// Verifies that the function completes without throwing any exceptions and returns the result.
    /// </summary>
    public static T ShouldNotThrow<T>(this Func<T> action, string? customMessage = null,
        [CallerArgumentExpression(nameof(action))] string? actualExpression = null) =>
        Should.NotThrowInternal(action, customMessage, actualExpression: actualExpression);

    /// <summary>
    /// Verifies that the action does not throw an exception of type <typeparamref name="TException"/>.
    /// Exceptions of other types are not caught and will propagate to the caller.
    /// </summary>
    public static void ShouldNotThrow<TException>(this Action action, string? customMessage = null,
        [CallerArgumentExpression(nameof(action))] string? actualExpression = null)
        where TException : Exception =>
        Should.NotThrowInternal<TException>(action, customMessage, actualExpression: actualExpression);
}