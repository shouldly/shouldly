using JetBrains.Annotations;

namespace Shouldly;

public static partial class Should
{
    /// <summary>
    /// Asynchronously verifies that the provided task throws an exception of type <typeparamref name="TException"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Task<TException> ThrowAsync<TException>(Task task, string? customMessage = null,
        [CallerArgumentExpression(nameof(task))] string? actualExpression = null)
        where TException : Exception
    {
        return ThrowAsyncInternal<TException>(() => task, customMessage, actualExpression: actualExpression);
    }

    /// <summary>
    /// Asynchronously verifies that the provided task throws an exception of the specified type
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Task<Exception> ThrowAsync(Task task, Type exceptionType, string? customMessage = null,
        [CallerArgumentExpression(nameof(task))] string? actualExpression = null)
    {
        return ThrowAsyncInternal(() => task, exceptionType, customMessage, actualExpression: actualExpression);
    }

    /// <summary>
    /// Asynchronously verifies that the provided task function throws an exception of type <typeparamref name="TException"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Task<TException> ThrowAsync<TException>(Func<Task> actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where TException : Exception =>
        ThrowAsyncInternal<TException>(actual, customMessage, actualExpression: actualExpression);

    internal static Task<TException> ThrowAsyncInternal<TException>(Func<Task> actual, string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null)
        where TException : Exception
    {
        actualExpression = actualExpression.NormalizeDelegateExpression();
        var stackTrace = new StackTrace(true);
        try
        {
            return actual()
                .ContinueWith(new Func<Task, Exception>(t =>
                {
                    if (t.IsFaulted)
                    {
                        if (t.Exception!.InnerException is TException expectedException)
                            return expectedException;

                        // If Task.IsFaulted is true, there is at least one inner exception.
                        return new ShouldAssertException(new AsyncShouldlyThrowShouldlyMessage(typeof(TException), t.Exception.InnerException!.GetType(), customMessage, stackTrace, actualExpression).ToString());
                    }

                    if (t.IsCanceled)
                    {
                        return new TaskCanceledException(t);
                    }

                    return new ShouldAssertException(new AsyncShouldlyThrowShouldlyMessage(typeof(TException), customMessage, stackTrace, shouldlyMethod, actualExpression).ToString());
                }))
                .ContinueWith(x =>
                {
                    switch (x.Result)
                    {
                        case ShouldAssertException assert:
                            throw assert;
                        case TException expectedException:
                            return expectedException;
                        default:
                            throw new ShouldAssertException(new AsyncShouldlyThrowShouldlyMessage(typeof(TException), x.Result.GetType(), customMessage, stackTrace, actualExpression).ToString(), x.Result);
                    }
                });
        }
        catch (Exception e)
        {
            if (e is TException exception)
                return Task.FromResult(exception);

            throw new ShouldAssertException(new AsyncShouldlyThrowShouldlyMessage(typeof(TException), e.GetType(), customMessage, stackTrace, actualExpression).ToString());
        }
    }

    /// <summary>
    /// Asynchronously verifies that the provided task function throws an exception of the specified type
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Task<Exception> ThrowAsync(Func<Task> actual, Type exceptionType, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
        ThrowAsyncInternal(actual, exceptionType, customMessage, actualExpression: actualExpression);

    internal static Task<Exception> ThrowAsyncInternal(Func<Task> actual, Type exceptionType, string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null)
    {
        actualExpression = actualExpression.NormalizeDelegateExpression();
        var stackTrace = new StackTrace(true);
        return actual().ContinueWith(t =>
        {
            if (t.IsFaulted)
            {
                if (t.Exception == null)
                    throw new ShouldAssertException(new AsyncShouldlyThrowShouldlyMessage(exceptionType, customMessage, stackTrace, shouldlyMethod, actualExpression).ToString());

                return HandleTaskAggregateException(t.Exception, customMessage, exceptionType, actualExpression);
            }

            if (t.IsCanceled)
            {
                throw new ShouldAssertException(new AsyncShouldlyThrowShouldlyMessage(exceptionType, customMessage, stackTrace, shouldlyMethod, actualExpression).ToString(),
                    new TaskCanceledException("Task is cancelled"));
            }

            throw new ShouldAssertException(new AsyncShouldlyThrowShouldlyMessage(exceptionType, customMessage, stackTrace, shouldlyMethod, actualExpression).ToString());
        });
    }

    /// <summary>
    /// Asynchronously verifies that the provided task does not throw any exceptions
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Task NotThrowAsync(Task task, string? customMessage = null,
        [CallerArgumentExpression(nameof(task))] string? actualExpression = null)
    {
        return NotThrowAsyncInternal(() => task, customMessage, actualExpression: actualExpression);
    }

    /// <summary>
    /// Asynchronously verifies that the provided task function does not throw any exceptions
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Task NotThrowAsync(Func<Task> actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
        NotThrowAsyncInternal(actual, customMessage, actualExpression: actualExpression);

    internal static Task NotThrowAsyncInternal(
        [InstantHandle] Func<Task> actual,
        string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null)
    {
        actualExpression = actualExpression.NormalizeDelegateExpression();
        var stackTrace = new StackTrace(true);
        return actual().ContinueWith(t =>
        {
            if (t.IsFaulted)
            {
                var flattened = t.Exception!.Flatten();
                if (flattened.InnerExceptions.Count == 1 && flattened.InnerException != null)
                {
                    var inner = flattened.InnerException;
                    throw new ShouldAssertException(new AsyncShouldlyNotThrowShouldlyMessage(inner.GetType(), customMessage, stackTrace, inner.Message, shouldlyMethod, actualExpression).ToString());
                }

                throw new ShouldAssertException(new AsyncShouldlyNotThrowShouldlyMessage(t.Exception.GetType(), customMessage, stackTrace, t.Exception.Message, shouldlyMethod, actualExpression).ToString());
            }
        });
    }
}