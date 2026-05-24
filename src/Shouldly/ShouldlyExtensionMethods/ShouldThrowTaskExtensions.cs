using System.ComponentModel;

namespace Shouldly;

/// <summary>
/// Extension methods for task-based exception assertions
/// </summary>
[DebuggerStepThrough]
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldThrowTaskExtensions
{
    /// <summary>
    /// Verifies that the Task throws a <typeparamref name="TException"/> exception.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static TException ShouldThrow<TException>(this Task actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where TException : Exception
    {
        return ShouldThrow<TException>(() => actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Verifies that the Task throws an exception of the specified type.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception ShouldThrow(this Task actual, Type exceptionType,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        return ShouldThrow(() => actual, exceptionType, actualExpression);
    }

    /// <summary>
    /// Verifies that the Task throws an exception of the specified type with a custom message.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception ShouldThrow(this Task actual, string? customMessage, Type exceptionType,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        return ShouldThrow(() => actual, customMessage, exceptionType, actualExpression);
    }

    /// <summary>
    /// Verifies that the function returning a Task throws a <typeparamref name="TException"/> exception.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static TException ShouldThrow<TException>(this Func<Task> actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where TException : Exception =>
        ShouldThrow<TException>(actual, ShouldlyConfiguration.DefaultTaskTimeout, customMessage, actualExpression);

    /// <summary>
    /// Verifies that the function returning a Task throws an exception of the specified type.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception ShouldThrow(this Func<Task> actual, Type exceptionType,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
        ShouldThrow(actual, null, exceptionType, actualExpression);

    /// <summary>
    /// Verifies that the function returning a Task throws an exception of the specified type with a custom message.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception ShouldThrow(this Func<Task> actual, string? customMessage, Type exceptionType,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
        ShouldThrow(actual, ShouldlyConfiguration.DefaultTaskTimeout, customMessage, exceptionType, actualExpression);

    /// <summary>
    /// Verifies that the Task throws a <typeparamref name="TException"/> exception within the specified timeout.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static TException ShouldThrow<TException>(this Task actual, TimeSpan timeoutAfter, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where TException : Exception
    {
        return ShouldThrow<TException>(() => actual, timeoutAfter, customMessage, actualExpression);
    }

    /// <summary>
    /// Verifies that the Task throws an exception of the specified type within the specified timeout.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception ShouldThrow(this Task actual, TimeSpan timeoutAfter, Type exceptionType,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        return ShouldThrow(() => actual, timeoutAfter, exceptionType, actualExpression);
    }

    /// <summary>
    /// Verifies that the Task throws an exception of the specified type within the specified timeout with a custom message.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception ShouldThrow(this Task actual, TimeSpan timeoutAfter, string? customMessage, Type exceptionType,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        return ShouldThrow(() => actual, timeoutAfter, customMessage, exceptionType, actualExpression);
    }

    /// <summary>
    /// Verifies that the function returning a Task throws a <typeparamref name="TException"/> exception within the specified timeout.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static TException ShouldThrow<TException>(this Func<Task> actual, TimeSpan timeoutAfter, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where TException : Exception =>
        Should.ThrowInternal<TException>(actual, timeoutAfter, customMessage);

    /// <summary>
    /// Verifies that the function returning a Task throws an exception of the specified type within the specified timeout.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception ShouldThrow(this Func<Task> actual, TimeSpan timeoutAfter, Type exceptionType,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
        ShouldThrow(actual, timeoutAfter, null, exceptionType, actualExpression);

    /// <summary>
    /// Verifies that the function returning a Task throws an exception of the specified type within the specified timeout with a custom message.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception ShouldThrow(this Func<Task> actual, TimeSpan timeoutAfter, string? customMessage, Type exceptionType,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
        Should.ThrowInternal(actual, timeoutAfter, customMessage, exceptionType);

    /// <summary>
    /// Verifies that the Task completes without throwing any exceptions.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotThrow(this Task action, string? customMessage = null,
        [CallerArgumentExpression(nameof(action))] string? actualExpression = null)
    {
        Should.NotThrowInternal(() => action, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);
    }

    /// <summary>
    /// Verifies that the Task completes without throwing any exceptions and returns the result.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ShouldNotThrow<T>(this Task<T> action, string? customMessage = null,
        [CallerArgumentExpression(nameof(action))] string? actualExpression = null)
    {
        return ShouldNotThrow(() => action, customMessage, actualExpression);
    }

    /// <summary>
    /// Verifies that the function returning a Task completes without throwing any exceptions.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotThrow(this Func<Task> action, string? customMessage = null,
        [CallerArgumentExpression(nameof(action))] string? actualExpression = null)
    {
        Should.NotThrowInternal(action, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);
    }

    /// <summary>
    /// Verifies that the Task completes without throwing any exceptions within the specified timeout.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotThrow(this Task action, TimeSpan timeoutAfter, string? customMessage = null,
        [CallerArgumentExpression(nameof(action))] string? actualExpression = null)
    {
        ShouldNotThrow(() => action, timeoutAfter, customMessage, actualExpression);
    }

    /// <summary>
    /// Verifies that the function returning a Task completes without throwing any exceptions within the specified timeout.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotThrow(this Func<Task> action, TimeSpan timeoutAfter, string? customMessage = null,
        [CallerArgumentExpression(nameof(action))] string? actualExpression = null)
    {
        Should.NotThrowInternal(action, timeoutAfter, customMessage);
    }

    /// <summary>
    /// Verifies that the function returning a Task completes without throwing any exceptions and returns the result.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ShouldNotThrow<T>(this Func<Task<T>> action, string? customMessage = null,
        [CallerArgumentExpression(nameof(action))] string? actualExpression = null) =>
        Should.NotThrowInternal(action, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);

    /// <summary>
    /// Verifies that the Task completes without throwing any exceptions within the specified timeout and returns the result.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ShouldNotThrow<T>(this Task<T> action, TimeSpan timeoutAfter, string? customMessage = null,
        [CallerArgumentExpression(nameof(action))] string? actualExpression = null) =>
        ShouldNotThrow(() => action, timeoutAfter, customMessage, actualExpression);

    /// <summary>
    /// Verifies that the function returning a Task completes without throwing any exceptions within the specified timeout and returns the result.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ShouldNotThrow<T>(this Func<Task<T>> action, TimeSpan timeoutAfter, string? customMessage = null,
        [CallerArgumentExpression(nameof(action))] string? actualExpression = null) =>
        Should.NotThrowInternal(action, timeoutAfter, customMessage);
}
