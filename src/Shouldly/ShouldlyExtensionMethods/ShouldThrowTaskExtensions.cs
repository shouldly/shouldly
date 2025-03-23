using System.ComponentModel;

namespace Shouldly;

[DebuggerStepThrough]
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldThrowTaskExtensions
{
    /*** ShouldThrow(Task) ***/
    /// <summary>
    /// Verifies that the Task throws a <typeparamref name="TException"/> exception.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static TException ShouldThrow<TException>(this Task actual, string? customMessage = null)
        where TException : Exception
    {
        return ShouldThrow<TException>(() => actual, customMessage);
    }

    /*** ShouldThrow(Task) ***/
    /// <summary>
    /// Verifies that the Task throws an exception of the specified type.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception ShouldThrow(this Task actual, Type exceptionType)
    {
        return ShouldThrow(() => actual, exceptionType);
    }

    /// <summary>
    /// Verifies that the Task throws an exception of the specified type with a custom message.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception ShouldThrow(this Task actual, string? customMessage, Type exceptionType)
    {
        return ShouldThrow(() => actual, customMessage, exceptionType);
    }

    /*** ShouldThrow(Func<Task>) ***/
    /// <summary>
    /// Verifies that the function returning a Task throws a <typeparamref name="TException"/> exception.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static TException ShouldThrow<TException>(this Func<Task> actual, string? customMessage = null)
        where TException : Exception =>
        ShouldThrow<TException>(actual, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);

    /*** ShouldThrow(Func<Task>) ***/
    /// <summary>
    /// Verifies that the function returning a Task throws an exception of the specified type.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception ShouldThrow(this Func<Task> actual, Type exceptionType) =>
        ShouldThrow(actual, null, exceptionType);

    /// <summary>
    /// Verifies that the function returning a Task throws an exception of the specified type with a custom message.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception ShouldThrow(this Func<Task> actual, string? customMessage, Type exceptionType) =>
        ShouldThrow(actual, ShouldlyConfiguration.DefaultTaskTimeout, customMessage, exceptionType);

    /*** ShouldThrow(Task, TimeSpan) ***/
    /// <summary>
    /// Verifies that the Task throws a <typeparamref name="TException"/> exception within the specified timeout.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static TException ShouldThrow<TException>(this Task actual, TimeSpan timeoutAfter, string? customMessage = null)
        where TException : Exception
    {
        return ShouldThrow<TException>(() => actual, timeoutAfter, customMessage);
    }

    /*** ShouldThrow(Task, TimeSpan) ***/
    /// <summary>
    /// Verifies that the Task throws an exception of the specified type within the specified timeout.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception ShouldThrow(this Task actual, TimeSpan timeoutAfter, Type exceptionType)
    {
        return ShouldThrow(() => actual, timeoutAfter, exceptionType);
    }

    /// <summary>
    /// Verifies that the Task throws an exception of the specified type within the specified timeout with a custom message.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception ShouldThrow(this Task actual, TimeSpan timeoutAfter, string? customMessage, Type exceptionType)
    {
        return ShouldThrow(() => actual, timeoutAfter, customMessage, exceptionType);
    }

    /*** ShouldThrow(Func<Task>, TimeSpan) ***/
    /// <summary>
    /// Verifies that the function returning a Task throws a <typeparamref name="TException"/> exception within the specified timeout.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static TException ShouldThrow<TException>(this Func<Task> actual, TimeSpan timeoutAfter, string? customMessage = null)
        where TException : Exception =>
        Should.ThrowInternal<TException>(actual, timeoutAfter, customMessage);

    /*** ShouldThrow(Func<Task>, TimeSpan) ***/
    /// <summary>
    /// Verifies that the function returning a Task throws an exception of the specified type within the specified timeout.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception ShouldThrow(this Func<Task> actual, TimeSpan timeoutAfter, Type exceptionType) =>
        ShouldThrow(actual, timeoutAfter, null, exceptionType);

    /// <summary>
    /// Verifies that the function returning a Task throws an exception of the specified type within the specified timeout with a custom message.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception ShouldThrow(this Func<Task> actual, TimeSpan timeoutAfter, string? customMessage, Type exceptionType) =>
        Should.ThrowInternal(actual, timeoutAfter, customMessage, exceptionType);

    /*** ShouldNotThrow(Task) ***/
    /// <summary>
    /// Verifies that the Task completes without throwing any exceptions.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotThrow(this Task action, string? customMessage = null)
    {
        Should.NotThrowInternal(() => action, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);
    }

    /*** ShouldNotThrow(Task<T>) ***/
    /// <summary>
    /// Verifies that the Task completes without throwing any exceptions and returns the result.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ShouldNotThrow<T>(this Task<T> action, string? customMessage = null)
    {
        return ShouldNotThrow(() => action, customMessage);
    }

    /*** ShouldNotThrow(Func<Task>) ***/
    /// <summary>
    /// Verifies that the function returning a Task completes without throwing any exceptions.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotThrow(this Func<Task> action, string? customMessage = null)
    {
        Should.NotThrowInternal(action, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);
    }

    /*** ShouldNotThrow(Task, TimeSpan) ***/
    /// <summary>
    /// Verifies that the Task completes without throwing any exceptions within the specified timeout.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotThrow(this Task action, TimeSpan timeoutAfter, string? customMessage = null)
    {
        ShouldNotThrow(() => action, timeoutAfter, customMessage);
    }

    /*** ShouldNotThrow(Func<Task>, TimeSpan) ***/
    /// <summary>
    /// Verifies that the function returning a Task completes without throwing any exceptions within the specified timeout.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotThrow(this Func<Task> action, TimeSpan timeoutAfter, string? customMessage = null)
    {
        Should.NotThrowInternal(action, timeoutAfter, customMessage);
    }

    /*** ShouldNotThrow(Func<Task<T>>) ***/
    /// <summary>
    /// Verifies that the function returning a Task completes without throwing any exceptions and returns the result.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ShouldNotThrow<T>(this Func<Task<T>> action, string? customMessage = null) =>
        Should.NotThrowInternal(action, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);

    /*** ShouldNotThrow(Task<T>, TimeSpan) ***/
    /// <summary>
    /// Verifies that the Task completes without throwing any exceptions within the specified timeout and returns the result.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ShouldNotThrow<T>(this Task<T> action, TimeSpan timeoutAfter, string? customMessage = null) =>
        ShouldNotThrow(() => action, timeoutAfter, customMessage);

    /*** ShouldNotThrow(Func<Task<T>>, TimeSpan) ***/
    /// <summary>
    /// Verifies that the function returning a Task completes without throwing any exceptions within the specified timeout and returns the result.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ShouldNotThrow<T>(this Func<Task<T>> action, TimeSpan timeoutAfter, string? customMessage = null) =>
        Should.NotThrowInternal(action, timeoutAfter, customMessage);
}