using JetBrains.Annotations;

namespace Shouldly;

public static partial class Should
{
    /// <summary>
    /// Asynchronously verifies that the provided task throws an exception of type <typeparamref name="TException"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Task<TException> ThrowAsync<TException>(Task task, string? customMessage = null)
        where TException : Exception
    {
        return ThrowAsync<TException>(() => task, customMessage);
    }

    /// <summary>
    /// Asynchronously verifies that the provided task throws an exception of the specified type
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Task<Exception> ThrowAsync(Task task, Type exceptionType, string? customMessage = null)
    {
        return ThrowAsync(() => task, exceptionType, customMessage);
    }

    /// <summary>
    /// Asynchronously verifies that the provided task function throws an exception of type <typeparamref name="TException"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Task<TException> ThrowAsync<TException>(Func<Task> actual, string? customMessage = null)
        where TException : Exception
    {
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
                        return new ShouldAssertException(new AsyncShouldlyThrowShouldlyMessage(typeof(TException), t.Exception.InnerException!.GetType(), customMessage, stackTrace).ToString());
                    }

                    if (t.IsCanceled)
                    {
                        return new TaskCanceledException(t);
                    }

                    return new ShouldAssertException(new AsyncShouldlyThrowShouldlyMessage(typeof(TException), customMessage, stackTrace).ToString());
                }))
                .ContinueWith(x =>
                {
                    switch (x.Result)
                    {
                        case ShouldAssertException assert:
                            ThrowHelper.ThrowOrRecord(assert);
                            return default!;
                        case TException expectedException:
                            return expectedException;
                        default:
                            ThrowHelper.ThrowOrRecord(new ShouldAssertException(new AsyncShouldlyThrowShouldlyMessage(typeof(TException), x.Result.GetType(), customMessage, stackTrace).ToString(), x.Result));
                            return default!;
                    }
                });
        }
        catch (Exception e)
        {
            if (e is TException exception)
                return Task.FromResult(exception);

            ThrowHelper.ThrowOrRecord(new ShouldAssertException(new AsyncShouldlyThrowShouldlyMessage(typeof(TException), e.GetType(), customMessage, stackTrace).ToString()));
            return Task.FromResult(default(TException)!);
        }
    }

    /// <summary>
    /// Asynchronously verifies that the provided task function throws an exception of the specified type
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Task<Exception> ThrowAsync(Func<Task> actual, Type exceptionType, string? customMessage = null)
    {
        var stackTrace = new StackTrace(true);
        return actual().ContinueWith(t =>
        {
            if (t.IsFaulted)
            {
                if (t.Exception == null)
                {
                    ThrowHelper.ThrowOrRecord(new ShouldAssertException(new AsyncShouldlyThrowShouldlyMessage(exceptionType, customMessage, stackTrace).ToString()));
                    return default!;
                }

                return HandleTaskAggregateException(t.Exception, customMessage, exceptionType);
            }

            if (t.IsCanceled)
            {
                ThrowHelper.ThrowOrRecord(new ShouldAssertException(new AsyncShouldlyThrowShouldlyMessage(exceptionType, customMessage, stackTrace).ToString(),
                    new TaskCanceledException("Task is cancelled")));
                return default!;
            }

            ThrowHelper.ThrowOrRecord(new ShouldAssertException(new AsyncShouldlyThrowShouldlyMessage(exceptionType, customMessage, stackTrace).ToString()));
            return default!;
        });
    }

    /// <summary>
    /// Asynchronously verifies that the provided task does not throw any exceptions
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Task NotThrowAsync(Task task, string? customMessage = null)
    {
        return NotThrowAsyncInternal(() => task, customMessage);
    }

    /// <summary>
    /// Asynchronously verifies that the provided task function does not throw any exceptions
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Task NotThrowAsync(Func<Task> actual, string? customMessage = null) =>
        NotThrowAsyncInternal(actual, customMessage);

    internal static Task NotThrowAsyncInternal(
        [InstantHandle] Func<Task> actual,
        string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!)
    {
        var stackTrace = new StackTrace(true);
        return actual().ContinueWith(t =>
        {
            if (t.IsFaulted)
            {
                var flattened = t.Exception!.Flatten();
                if (flattened.InnerExceptions.Count == 1 && flattened.InnerException != null)
                {
                    var inner = flattened.InnerException;
                    ThrowHelper.ThrowOrRecord(new ShouldAssertException(new AsyncShouldlyNotThrowShouldlyMessage(inner.GetType(), customMessage, stackTrace, inner.Message, shouldlyMethod).ToString()));
                    return;
                }

                ThrowHelper.ThrowOrRecord(new ShouldAssertException(new AsyncShouldlyNotThrowShouldlyMessage(t.Exception.GetType(), customMessage, stackTrace, t.Exception.Message, shouldlyMethod).ToString()));
            }
        });
    }
}