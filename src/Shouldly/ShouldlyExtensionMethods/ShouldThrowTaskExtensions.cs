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
    extension(Task actual)
    {
        /// <summary>
        /// Verifies that the Task throws a <typeparamref name="TException"/> exception.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public TException ShouldThrow<TException>(string? customMessage = null)
            where TException : Exception
        {
            return ShouldThrow<TException>(() => actual, customMessage);
        }

        /// <summary>
        /// Verifies that the Task throws an exception of the specified type.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Exception ShouldThrow(Type exceptionType)
        {
            return ShouldThrow(() => actual, exceptionType);
        }

        /// <summary>
        /// Verifies that the Task throws an exception of the specified type with a custom message.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Exception ShouldThrow(string? customMessage, Type exceptionType)
        {
            return ShouldThrow(() => actual, customMessage, exceptionType);
        }
    }

    extension(Func<Task> actual)
    {
        /// <summary>
        /// Verifies that the function returning a Task throws a <typeparamref name="TException"/> exception.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public TException ShouldThrow<TException>(string? customMessage = null)
            where TException : Exception =>
            ShouldThrow<TException>(actual, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);

        /// <summary>
        /// Verifies that the function returning a Task throws an exception of the specified type.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Exception ShouldThrow(Type exceptionType) =>
            ShouldThrow(actual, null, exceptionType);

        /// <summary>
        /// Verifies that the function returning a Task throws an exception of the specified type with a custom message.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Exception ShouldThrow(string? customMessage, Type exceptionType) =>
            ShouldThrow(actual, ShouldlyConfiguration.DefaultTaskTimeout, customMessage, exceptionType);
    }

    extension(Task actual)
    {
        /// <summary>
        /// Verifies that the Task throws a <typeparamref name="TException"/> exception within the specified timeout.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public TException ShouldThrow<TException>(TimeSpan timeoutAfter, string? customMessage = null)
            where TException : Exception
        {
            return ShouldThrow<TException>(() => actual, timeoutAfter, customMessage);
        }

        /// <summary>
        /// Verifies that the Task throws an exception of the specified type within the specified timeout.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Exception ShouldThrow(TimeSpan timeoutAfter, Type exceptionType)
        {
            return ShouldThrow(() => actual, timeoutAfter, exceptionType);
        }

        /// <summary>
        /// Verifies that the Task throws an exception of the specified type within the specified timeout with a custom message.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Exception ShouldThrow(TimeSpan timeoutAfter, string? customMessage, Type exceptionType)
        {
            return ShouldThrow(() => actual, timeoutAfter, customMessage, exceptionType);
        }
    }

    extension(Func<Task> actual)
    {
        /// <summary>
        /// Verifies that the function returning a Task throws a <typeparamref name="TException"/> exception within the specified timeout.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public TException ShouldThrow<TException>(TimeSpan timeoutAfter, string? customMessage = null)
            where TException : Exception =>
            Should.ThrowInternal<TException>(actual, timeoutAfter, customMessage);

        /// <summary>
        /// Verifies that the function returning a Task throws an exception of the specified type within the specified timeout.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Exception ShouldThrow(TimeSpan timeoutAfter, Type exceptionType) =>
            ShouldThrow(actual, timeoutAfter, null, exceptionType);

        /// <summary>
        /// Verifies that the function returning a Task throws an exception of the specified type within the specified timeout with a custom message.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Exception ShouldThrow(TimeSpan timeoutAfter, string? customMessage, Type exceptionType) =>
            Should.ThrowInternal(actual, timeoutAfter, customMessage, exceptionType);
    }

    /// <summary>
    /// Verifies that the Task completes without throwing any exceptions.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotThrow(this Task action, string? customMessage = null)
    {
        Should.NotThrowInternal(() => action, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);
    }

    /// <summary>
    /// Verifies that the Task completes without throwing any exceptions and returns the result.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ShouldNotThrow<T>(this Task<T> action, string? customMessage = null)
    {
        return ShouldNotThrow(() => action, customMessage);
    }

    /// <summary>
    /// Verifies that the function returning a Task completes without throwing any exceptions.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotThrow(this Func<Task> action, string? customMessage = null)
    {
        Should.NotThrowInternal(action, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);
    }

    /// <summary>
    /// Verifies that the Task completes without throwing any exceptions within the specified timeout.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotThrow(this Task action, TimeSpan timeoutAfter, string? customMessage = null)
    {
        ShouldNotThrow(() => action, timeoutAfter, customMessage);
    }

    /// <summary>
    /// Verifies that the function returning a Task completes without throwing any exceptions within the specified timeout.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotThrow(this Func<Task> action, TimeSpan timeoutAfter, string? customMessage = null)
    {
        Should.NotThrowInternal(action, timeoutAfter, customMessage);
    }

    /// <summary>
    /// Verifies that the function returning a Task completes without throwing any exceptions and returns the result.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ShouldNotThrow<T>(this Func<Task<T>> action, string? customMessage = null) =>
        Should.NotThrowInternal(action, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);

    /// <summary>
    /// Verifies that the Task completes without throwing any exceptions within the specified timeout and returns the result.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ShouldNotThrow<T>(this Task<T> action, TimeSpan timeoutAfter, string? customMessage = null) =>
        ShouldNotThrow(() => action, timeoutAfter, customMessage);

    /// <summary>
    /// Verifies that the function returning a Task completes without throwing any exceptions within the specified timeout and returns the result.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ShouldNotThrow<T>(this Func<Task<T>> action, TimeSpan timeoutAfter, string? customMessage = null) =>
        Should.NotThrowInternal(action, timeoutAfter, customMessage);
}