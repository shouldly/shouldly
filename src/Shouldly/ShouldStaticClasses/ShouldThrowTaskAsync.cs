using JetBrains.Annotations;
using Shouldly.Internals.Failures;

namespace Shouldly;

public static partial class Should
{
    /// <summary>
    /// Asynchronously verifies that the provided task throws an exception of type <typeparamref name="TException"/>
    /// </summary>
    [Obsolete(TaskOverloadObsolescence.Message, error: true)]
    public static Task<TException> ThrowAsync<TException>(Task task, string? customMessage = null,
        [CallerArgumentExpression(nameof(task))] string? actualExpression = null)
        where TException : Exception
    {
        return ThrowAsyncInternal<TException>(() => task, customMessage, Fail.ThrowAsync, actualExpression);
    }

    /// <summary>
    /// Asynchronously verifies that the provided task throws an exception of the specified type
    /// </summary>
    [Obsolete(TaskOverloadObsolescence.Message, error: true)]
    public static Task<Exception> ThrowAsync(Task task, Type exceptionType, string? customMessage = null,
        [CallerArgumentExpression(nameof(task))] string? actualExpression = null)
    {
        return ThrowAsyncInternal(() => task, exceptionType, customMessage, Fail.ThrowAsync, actualExpression);
    }

    /// <summary>
    /// Asynchronously verifies that the provided task function throws an exception of type <typeparamref name="TException"/>
    /// </summary>
    public static Task<TException> ThrowAsync<TException>(Func<Task> actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where TException : Exception =>
        ThrowAsyncInternal<TException>(actual, customMessage, Fail.ThrowAsync, actualExpression);

    [DebuggerDisableUserUnhandledExceptions]
    internal static async Task<TException> ThrowAsyncInternal<TException>(Func<Task> actual, string? customMessage,
        ThrowFailure failure,
        string? actualExpression = null)
        where TException : Exception
    {
        actualExpression = actualExpression.NormalizeDelegateExpression();
        // Stack trace is only consulted when CAE didn't provide an expression. Allocate
        // lazily so the happy path (CAE supplied) doesn't pay for a PDB-reading stack walk
        // it will never read.
        var stackTrace = actualExpression == null ? new StackTrace(true) : null;
        try
        {
            await actual();
        }
        catch (TException expected)
        {
            return expected;
        }
        catch (Exception e)
        {
            throw failure.With(typeof(TException), e.GetType(), isTask: true).ForDelegate(customMessage, actualExpression, stackTrace: stackTrace);
        }

        throw failure.With(typeof(TException), thrown: null, isTask: true).ForDelegate(customMessage, actualExpression, stackTrace: stackTrace);
    }

    /// <summary>
    /// Asynchronously verifies that the provided task function throws an exception of the specified type
    /// </summary>
    public static Task<Exception> ThrowAsync(Func<Task> actual, Type exceptionType, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
        ThrowAsyncInternal(actual, exceptionType, customMessage, Fail.ThrowAsync, actualExpression);

    [DebuggerDisableUserUnhandledExceptions]
    internal static async Task<Exception> ThrowAsyncInternal(Func<Task> actual, Type exceptionType, string? customMessage,
        ThrowFailure failure,
        string? actualExpression = null)
    {
        actualExpression = actualExpression.NormalizeDelegateExpression();
        var stackTrace = actualExpression == null ? new StackTrace(true) : null;
        try
        {
            await actual();
        }
        catch (Exception e)
        {
            if (exceptionType.IsInstanceOfType(e))
                return e;

            throw Fail.ThrowAsyncInternal.With(exceptionType).For(e.GetType(), customMessage, actualExpression);
        }

        throw failure.With(exceptionType, thrown: null, isTask: true).ForDelegate(customMessage, actualExpression, stackTrace: stackTrace);
    }

    /// <summary>
    /// Asynchronously verifies that the provided task function throws an exception of exactly type <typeparamref name="TException"/>.
    /// A derived exception type does not match.
    /// </summary>
    public static Task<TException> ThrowExactlyAsync<TException>(Func<Task> actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where TException : Exception =>
        ThrowExactlyAsyncInternal<TException>(actual, customMessage, Fail.ThrowExactlyAsync, actualExpression);

    [DebuggerDisableUserUnhandledExceptions]
    internal static async Task<TException> ThrowExactlyAsyncInternal<TException>(Func<Task> actual, string? customMessage,
        ThrowFailure failure,
        string? actualExpression = null)
        where TException : Exception
    {
        actualExpression = actualExpression.NormalizeDelegateExpression();
        var stackTrace = actualExpression == null ? new StackTrace(true) : null;
        try
        {
            await actual();
        }
        catch (Exception e)
        {
            if (e.GetType() == typeof(TException))
                return (TException)e;

            throw failure.With(typeof(TException), e.GetType(), isTask: true).ForDelegate(customMessage, actualExpression, stackTrace: stackTrace);
        }

        throw failure.With(typeof(TException), thrown: null, isTask: true).ForDelegate(customMessage, actualExpression, stackTrace: stackTrace);
    }

    /// <summary>
    /// Asynchronously verifies that the provided task does not throw any exceptions
    /// </summary>
    [Obsolete(TaskOverloadObsolescence.Message, error: true)]
    public static Task NotThrowAsync(Task task, string? customMessage = null,
        [CallerArgumentExpression(nameof(task))] string? actualExpression = null)
    {
        return NotThrowAsyncInternal(() => task, customMessage, Fail.NotThrowAsync, actualExpression);
    }

    /// <summary>
    /// Asynchronously verifies that the provided task function does not throw any exceptions
    /// </summary>
    public static Task NotThrowAsync(Func<Task> actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
        NotThrowAsyncInternal(actual, customMessage, Fail.NotThrowAsync, actualExpression);

    /// <summary>
    /// Asynchronously verifies that the provided task function does not throw any exceptions and returns its result
    /// </summary>
    public static async Task<T> NotThrowAsync<T>(Func<Task<T>> actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        Task<T>? task = null;
        await NotThrowAsyncInternal(() => task = actual(), customMessage, Fail.NotThrowAsync, actualExpression);
        // NotThrowAsyncInternal has awaited the task and verified it completed successfully
        return await task!;
    }

    [DebuggerDisableUserUnhandledExceptions]
    internal static async Task NotThrowAsyncInternal(
        [InstantHandle] Func<Task> actual,
        string? customMessage,
        NotThrowFailure failure,
        string? actualExpression = null)
    {
        actualExpression = actualExpression.NormalizeDelegateExpression();
        var stackTrace = actualExpression == null ? new StackTrace(true) : null;
        Task? task = null;
        try
        {
            // Invoked inside the try so an exception thrown before a task is returned fails the assertion too.
            task = actual();
            await task;
        }
        catch (Exception ex)
        {
            // task.Exception preserves the full AggregateException so we can distinguish
            // single-inner from multi-inner faulted tasks (await only surfaces the first).
            if (task?.Exception is { } aggregate)
            {
                var flattened = aggregate.Flatten();
                if (flattened.InnerExceptions.Count == 1 && flattened.InnerException != null)
                {
                    var inner = flattened.InnerException;
                    throw failure.With(inner.GetType(), inner.Message, isTask: true).ForDelegate(customMessage, actualExpression, stackTrace: stackTrace);
                }

                throw failure.With(aggregate.GetType(), aggregate.Message, isTask: true).ForDelegate(customMessage, actualExpression, stackTrace: stackTrace);
            }

            throw failure.With(ex.GetType(), ex.Message, isTask: true).ForDelegate(customMessage, actualExpression, stackTrace: stackTrace);
        }
    }
}
