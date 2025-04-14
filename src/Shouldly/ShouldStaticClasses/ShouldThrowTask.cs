using JetBrains.Annotations;

namespace Shouldly;

public static partial class Should
{
    /// <summary>
    /// Verifies that the provided task throws an exception of type <typeparamref name="TException"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static TException Throw<TException>(Task actual, string? customMessage = null)
        where TException : Exception
    {
        return Throw<TException>(() => actual, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);
    }

    /// <summary>
    /// Verifies that the provided task throws an exception of the specified type
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception Throw(Task actual, Type exceptionType, string? customMessage = null) =>
        ThrowInternal(() => actual, ShouldlyConfiguration.DefaultTaskTimeout, customMessage, exceptionType);

    /// <summary>
    /// Verifies that the provided task function throws an exception of type <typeparamref name="TException"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static TException Throw<TException>([InstantHandle] Func<Task> actual, string? customMessage = null)
        where TException : Exception =>
        Throw<TException>(actual, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);

    /// <summary>
    /// Verifies that the provided task function throws an exception of the specified type
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception Throw([InstantHandle] Func<Task> actual, Type exceptionType, string? customMessage = null) => ThrowInternal(actual, ShouldlyConfiguration.DefaultTaskTimeout, customMessage, exceptionType);

    /// <summary>
    /// Verifies that the provided task throws an exception of type <typeparamref name="TException"/> within the specified timeout
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static TException Throw<TException>(Task actual, TimeSpan timeoutAfter, string? customMessage = null)
        where TException : Exception =>
        Throw<TException>(() => actual, timeoutAfter, customMessage);

    /// <summary>
    /// Verifies that the provided task throws an exception of the specified type within the specified timeout
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception Throw(Task actual, TimeSpan timeoutAfter, Type exceptionType, string? customMessage = null)
    {
        return ThrowInternal(() => actual, timeoutAfter, customMessage, exceptionType);
    }

    /// <summary>
    /// Verifies that the provided task function throws an exception of type <typeparamref name="TException"/> within the specified timeout
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static TException Throw<TException>([InstantHandle] Func<Task> actual, TimeSpan timeoutAfter, string? customMessage = null)
        where TException : Exception =>
        ThrowInternal<TException>(actual, timeoutAfter, customMessage);

    internal static TException ThrowInternal<TException>(
        [InstantHandle] Func<Task> actual, TimeSpan timeoutAfter,
        string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!)
        where TException : Exception
    {
        try
        {
            RunAndWait(actual, timeoutAfter, customMessage);
        }
        catch (ShouldlyTimeoutException)
        {
            throw;
        }
        catch (Exception e)
        {
            e = (e as AggregateException)?.InnerException ?? e;

            if (e is TException exception)
                return exception;

            throw new ShouldAssertException(new TaskShouldlyThrowMessage(typeof(TException), e.GetType(), customMessage, shouldlyMethod).ToString());
        }

        throw new ShouldAssertException(new TaskShouldlyThrowMessage(typeof(TException), customMessage, shouldlyMethod).ToString());
    }

    /// <summary>
    /// Verifies that the provided task function throws an exception of the specified type within the specified timeout
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception Throw([InstantHandle] Func<Task> actual, TimeSpan timeoutAfter, Type exceptionType) =>
        ThrowInternal(actual, timeoutAfter, null, exceptionType);

    /// <summary>
    /// Verifies that the provided task function throws an exception of the specified type within the specified timeout
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception Throw([InstantHandle] Func<Task> actual, TimeSpan timeoutAfter, string? customMessage, Type exceptionType) =>
        ThrowInternal(actual, timeoutAfter, customMessage, exceptionType);

    internal static Exception ThrowInternal(
        [InstantHandle] Func<Task> actual, TimeSpan timeoutAfter,
        string? customMessage,
        Type exceptionType,
        [CallerMemberName] string shouldlyMethod = null!)
    {
        try
        {
            RunAndWait(actual, timeoutAfter, customMessage);
        }
        catch (ShouldlyTimeoutException)
        {
            throw;
        }
        catch (Exception e)
        {
            e = (e as AggregateException)?.InnerException ?? e;

            if (e.GetType() == exceptionType)
            {
                return e;
            }

            throw new ShouldAssertException(new TaskShouldlyThrowMessage(exceptionType, e.GetType(), customMessage, shouldlyMethod).ToString());
        }

        throw new ShouldAssertException(new TaskShouldlyThrowMessage(exceptionType, customMessage, shouldlyMethod).ToString());
    }

    /// <summary>
    /// Verifies that the provided task does not throw any exceptions
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void NotThrow(Task action, string? customMessage = null)
    {
        NotThrowInternal(() => action, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);
    }

    /// <summary>
    /// Verifies that the provided task does not throw any exceptions and returns its result
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T NotThrow<T>(Task<T> action, string? customMessage = null)
    {
        return NotThrow(() => action, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);
    }

    /// <summary>
    /// Verifies that the provided task function does not throw any exceptions
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void NotThrow([InstantHandle] Func<Task> action, string? customMessage = null)
    {
        NotThrowInternal(action, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);
    }

    /// <summary>
    /// Verifies that the provided task does not throw any exceptions within the specified timeout
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void NotThrow(Task action, TimeSpan timeoutAfter, string? customMessage = null)
    {
        NotThrowInternal(() => action, timeoutAfter, customMessage);
    }

    /// <summary>
    /// Verifies that the provided task function does not throw any exceptions within the specified timeout
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void NotThrow([InstantHandle] Func<Task> action, TimeSpan timeoutAfter, string? customMessage = null)
    {
        NotThrowInternal(action, timeoutAfter, customMessage);
    }

    internal static void NotThrowInternal(
        [InstantHandle] Func<Task> action, TimeSpan timeoutAfter,
        string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!)
    {
        try
        {
            RunAndWait(action, timeoutAfter, customMessage);
        }
        catch (ShouldlyTimeoutException)
        {
            throw;
        }
        catch (Exception ex)
        {
            ex = (ex as AggregateException)?.InnerException ?? ex;

            throw new ShouldAssertException(new TaskShouldlyThrowMessage(ex.GetType(), ex, customMessage, shouldlyMethod).ToString());
        }
    }

    /// <summary>
    /// Verifies that the provided task function does not throw any exceptions and returns its result
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T NotThrow<T>([InstantHandle] Func<Task<T>> action, string? customMessage = null) =>
        NotThrow(action, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);

    /// <summary>
    /// Verifies that the provided task does not throw any exceptions within the specified timeout and returns its result
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T NotThrow<T>(Task<T> action, TimeSpan timeoutAfter, string? customMessage = null) =>
        NotThrow(() => action, timeoutAfter, customMessage);

    /// <summary>
    /// Verifies that the provided task function does not throw any exceptions within the specified timeout and returns its result
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T NotThrow<T>([InstantHandle] Func<Task<T>> action, TimeSpan timeoutAfter, string? customMessage = null) =>
        NotThrowInternal(action, timeoutAfter, customMessage);

    internal static T NotThrowInternal<T>(
        [InstantHandle] Func<Task<T>> action, TimeSpan timeoutAfter,
        [InstantHandle] string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!)
    {
        try
        {
            // Drop the sync context so continuations will not post to it, causing a deadlock
            using (Utils.WithSynchronizationContext(null))
            {
                return CompleteIn(action, timeoutAfter, customMessage);
            }
        }
        catch (ShouldlyTimeoutException)
        {
            throw;
        }
        catch (Exception ex)
        {
            ex = (ex as AggregateException)?.InnerException ?? ex;

            throw new ShouldAssertException(new TaskShouldlyThrowMessage(ex.GetType(), ex, customMessage, shouldlyMethod).ToString());
        }
    }

    private static void RunAndWait(Func<Task> actual, TimeSpan timeoutAfter, string? customMessage)
    {
        // Drop the sync context so continuations will not post to it, causing a deadlock
        using (Utils.WithSynchronizationContext(null))
        {
            CompleteIn(actual, timeoutAfter, customMessage);
        }
    }

    private static Exception HandleTaskAggregateException(AggregateException exceptionFromTask, string? customMessage, Type exceptionType)
    {
        var innerException = exceptionFromTask.InnerException
                             ?? throw new ArgumentException("The specified exception is not from Task.Exception or it would have at least one inner exception.", nameof(exceptionFromTask));

        if (innerException.GetType() == exceptionType)
            return innerException;

        throw new ShouldAssertException(new ExpectedActualShouldlyMessage(exceptionType, innerException.GetType(), customMessage).ToString());
    }
}