using JetBrains.Annotations;

namespace Shouldly;

public static partial class Should
{
    /// <summary>
    /// Asynchronously verifies that the provided task throws an exception of type <typeparamref name="TException"/>
    /// </summary>
    public static Task<TException> ThrowAsync<TException>(Task task, string? customMessage = null,
        [CallerArgumentExpression(nameof(task))] string? actualExpression = null)
        where TException : Exception
    {
        return ThrowAsyncInternal<TException>(() => task, customMessage, actualExpression: actualExpression);
    }

    /// <summary>
    /// Asynchronously verifies that the provided task throws an exception of the specified type
    /// </summary>
    public static Task<Exception> ThrowAsync(Task task, Type exceptionType, string? customMessage = null,
        [CallerArgumentExpression(nameof(task))] string? actualExpression = null)
    {
        return ThrowAsyncInternal(() => task, exceptionType, customMessage, actualExpression: actualExpression);
    }

    /// <summary>
    /// Asynchronously verifies that the provided task function throws an exception of type <typeparamref name="TException"/>
    /// </summary>
    public static Task<TException> ThrowAsync<TException>(Func<Task> actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where TException : Exception =>
        ThrowAsyncInternal<TException>(actual, customMessage, actualExpression: actualExpression);

    [DebuggerDisableUserUnhandledExceptions]
    internal static async Task<TException> ThrowAsyncInternal<TException>(Func<Task> actual, string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!,
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
            ThrowHelper.ThrowOrRecord(new ShouldAssertException(new AsyncShouldlyThrowShouldlyMessage(typeof(TException), e.GetType(), customMessage, stackTrace, actualExpression).ToString()));
            return default!;
        }

        ThrowHelper.ThrowOrRecord(new ShouldAssertException(new AsyncShouldlyThrowShouldlyMessage(typeof(TException), customMessage, stackTrace, shouldlyMethod, actualExpression).ToString()));
        return default!;
    }

    /// <summary>
    /// Asynchronously verifies that the provided task function throws an exception of the specified type
    /// </summary>
    public static Task<Exception> ThrowAsync(Func<Task> actual, Type exceptionType, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
        ThrowAsyncInternal(actual, exceptionType, customMessage, actualExpression: actualExpression);

    [DebuggerDisableUserUnhandledExceptions]
    internal static async Task<Exception> ThrowAsyncInternal(Func<Task> actual, Type exceptionType, string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!,
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
            if (e.GetType() == exceptionType)
                return e;

            ThrowHelper.ThrowOrRecord(new ShouldAssertException(new ExpectedActualShouldlyMessage(exceptionType, e.GetType(), customMessage, actualExpression: actualExpression).ToString()));
            return default!;
        }

        ThrowHelper.ThrowOrRecord(new ShouldAssertException(new AsyncShouldlyThrowShouldlyMessage(exceptionType, customMessage, stackTrace, shouldlyMethod, actualExpression).ToString()));
        return default!;
    }

    /// <summary>
    /// Asynchronously verifies that the provided task does not throw any exceptions
    /// </summary>
    public static Task NotThrowAsync(Task task, string? customMessage = null,
        [CallerArgumentExpression(nameof(task))] string? actualExpression = null)
    {
        return NotThrowAsyncInternal(() => task, customMessage, actualExpression: actualExpression);
    }

    /// <summary>
    /// Asynchronously verifies that the provided task function does not throw any exceptions
    /// </summary>
    public static Task NotThrowAsync(Func<Task> actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
        NotThrowAsyncInternal(actual, customMessage, actualExpression: actualExpression);

    [DebuggerDisableUserUnhandledExceptions]
    internal static async Task NotThrowAsyncInternal(
        [InstantHandle] Func<Task> actual,
        string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null)
    {
        actualExpression = actualExpression.NormalizeDelegateExpression();
        var stackTrace = actualExpression == null ? new StackTrace(true) : null;
        var task = actual();
        try
        {
            await task;
        }
        catch (Exception ex)
        {
            // task.Exception preserves the full AggregateException so we can distinguish
            // single-inner from multi-inner faulted tasks (await only surfaces the first).
            if (task.Exception is { } aggregate)
            {
                var flattened = aggregate.Flatten();
                if (flattened.InnerExceptions.Count == 1 && flattened.InnerException != null)
                {
                    var inner = flattened.InnerException;
                    ThrowHelper.ThrowOrRecord(new ShouldAssertException(new AsyncShouldlyNotThrowShouldlyMessage(inner.GetType(), customMessage, stackTrace, inner.Message, shouldlyMethod, actualExpression).ToString()));
                    return;
                }

                ThrowHelper.ThrowOrRecord(new ShouldAssertException(new AsyncShouldlyNotThrowShouldlyMessage(aggregate.GetType(), customMessage, stackTrace, aggregate.Message, shouldlyMethod, actualExpression).ToString()));
                return;
            }

            ThrowHelper.ThrowOrRecord(new ShouldAssertException(new AsyncShouldlyNotThrowShouldlyMessage(ex.GetType(), customMessage, stackTrace, ex.Message, shouldlyMethod, actualExpression).ToString()));
        }
    }
}
