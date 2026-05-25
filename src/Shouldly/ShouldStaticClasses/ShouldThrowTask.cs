using JetBrains.Annotations;

namespace Shouldly;

public static partial class Should
{
    /// <summary>
    /// Verifies that the provided task throws an exception of type <typeparamref name="TException"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static TException Throw<TException>(Task actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where TException : Exception
    {
        return ThrowInternal<TException>(() => actual, ShouldlyConfiguration.DefaultTaskTimeout, customMessage, actualExpression: actualExpression);
    }

    /// <summary>
    /// Verifies that the provided task throws an exception of the specified type
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception Throw(Task actual, Type exceptionType, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
        ThrowInternal(() => actual, ShouldlyConfiguration.DefaultTaskTimeout, customMessage, exceptionType, actualExpression: actualExpression);

    /// <summary>
    /// Verifies that the provided task function throws an exception of type <typeparamref name="TException"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static TException Throw<TException>([InstantHandle] Func<Task> actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where TException : Exception =>
        ThrowInternal<TException>(actual, ShouldlyConfiguration.DefaultTaskTimeout, customMessage, actualExpression: actualExpression);

    /// <summary>
    /// Verifies that the provided task function throws an exception of the specified type
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception Throw([InstantHandle] Func<Task> actual, Type exceptionType, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
        ThrowInternal(actual, ShouldlyConfiguration.DefaultTaskTimeout, customMessage, exceptionType, actualExpression: actualExpression);

    /// <summary>
    /// Verifies that the provided task throws an exception of type <typeparamref name="TException"/> within the specified timeout
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static TException Throw<TException>(Task actual, TimeSpan timeoutAfter, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where TException : Exception =>
        ThrowInternal<TException>(() => actual, timeoutAfter, customMessage, actualExpression: actualExpression);

    /// <summary>
    /// Verifies that the provided task throws an exception of the specified type within the specified timeout
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception Throw(Task actual, TimeSpan timeoutAfter, Type exceptionType, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        return ThrowInternal(() => actual, timeoutAfter, customMessage, exceptionType, actualExpression: actualExpression);
    }

    /// <summary>
    /// Verifies that the provided task function throws an exception of type <typeparamref name="TException"/> within the specified timeout
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static TException Throw<TException>([InstantHandle] Func<Task> actual, TimeSpan timeoutAfter, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where TException : Exception =>
        ThrowInternal<TException>(actual, timeoutAfter, customMessage, actualExpression: actualExpression);

    internal static TException ThrowInternal<TException>(
        [InstantHandle] Func<Task> actual, TimeSpan timeoutAfter,
        string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null)
        where TException : Exception
    {
        actualExpression = actualExpression.NormalizeDelegateExpression();
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

            throw new ShouldAssertException(new TaskShouldlyThrowMessage(typeof(TException), e.GetType(), customMessage, shouldlyMethod, actualExpression).ToString());
        }

        throw new ShouldAssertException(new TaskShouldlyThrowMessage(typeof(TException), customMessage, shouldlyMethod, actualExpression).ToString());
    }

    /// <summary>
    /// Verifies that the provided task function throws an exception of the specified type within the specified timeout
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception Throw([InstantHandle] Func<Task> actual, TimeSpan timeoutAfter, Type exceptionType,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
        ThrowInternal(actual, timeoutAfter, null, exceptionType, actualExpression: actualExpression);

    /// <summary>
    /// Verifies that the provided task function throws an exception of the specified type within the specified timeout
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception Throw([InstantHandle] Func<Task> actual, TimeSpan timeoutAfter, string? customMessage, Type exceptionType,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
        ThrowInternal(actual, timeoutAfter, customMessage, exceptionType, actualExpression: actualExpression);

    internal static Exception ThrowInternal(
        [InstantHandle] Func<Task> actual, TimeSpan timeoutAfter,
        string? customMessage,
        Type exceptionType,
        [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null)
    {
        actualExpression = actualExpression.NormalizeDelegateExpression();
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

            throw new ShouldAssertException(new TaskShouldlyThrowMessage(exceptionType, e.GetType(), customMessage, shouldlyMethod, actualExpression).ToString());
        }

        throw new ShouldAssertException(new TaskShouldlyThrowMessage(exceptionType, customMessage, shouldlyMethod, actualExpression).ToString());
    }

    /// <summary>
    /// Verifies that the provided task does not throw any exceptions
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void NotThrow(Task action, string? customMessage = null,
        [CallerArgumentExpression(nameof(action))] string? actualExpression = null)
    {
        NotThrowInternal(() => action, ShouldlyConfiguration.DefaultTaskTimeout, customMessage, actualExpression: actualExpression);
    }

    /// <summary>
    /// Verifies that the provided task does not throw any exceptions and returns its result
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T NotThrow<T>(Task<T> action, string? customMessage = null,
        [CallerArgumentExpression(nameof(action))] string? actualExpression = null)
    {
        return NotThrowInternal(() => action, ShouldlyConfiguration.DefaultTaskTimeout, customMessage, actualExpression: actualExpression);
    }

    /// <summary>
    /// Verifies that the provided task function does not throw any exceptions
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void NotThrow([InstantHandle] Func<Task> action, string? customMessage = null,
        [CallerArgumentExpression(nameof(action))] string? actualExpression = null)
    {
        NotThrowInternal(action, ShouldlyConfiguration.DefaultTaskTimeout, customMessage, actualExpression: actualExpression);
    }

    /// <summary>
    /// Verifies that the provided task does not throw any exceptions within the specified timeout
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void NotThrow(Task action, TimeSpan timeoutAfter, string? customMessage = null,
        [CallerArgumentExpression(nameof(action))] string? actualExpression = null)
    {
        NotThrowInternal(() => action, timeoutAfter, customMessage, actualExpression: actualExpression);
    }

    /// <summary>
    /// Verifies that the provided task function does not throw any exceptions within the specified timeout
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void NotThrow([InstantHandle] Func<Task> action, TimeSpan timeoutAfter, string? customMessage = null,
        [CallerArgumentExpression(nameof(action))] string? actualExpression = null)
    {
        NotThrowInternal(action, timeoutAfter, customMessage, actualExpression: actualExpression);
    }

    internal static void NotThrowInternal(
        [InstantHandle] Func<Task> action, TimeSpan timeoutAfter,
        string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null)
    {
        actualExpression = actualExpression.NormalizeDelegateExpression();
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

            throw new ShouldAssertException(new TaskShouldlyThrowMessage(ex.GetType(), ex, customMessage, shouldlyMethod, actualExpression).ToString());
        }
    }

    /// <summary>
    /// Verifies that the provided task function does not throw any exceptions and returns its result
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T NotThrow<T>([InstantHandle] Func<Task<T>> action, string? customMessage = null,
        [CallerArgumentExpression(nameof(action))] string? actualExpression = null) =>
        NotThrowInternal(action, ShouldlyConfiguration.DefaultTaskTimeout, customMessage, actualExpression: actualExpression);

    /// <summary>
    /// Verifies that the provided task does not throw any exceptions within the specified timeout and returns its result
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T NotThrow<T>(Task<T> action, TimeSpan timeoutAfter, string? customMessage = null,
        [CallerArgumentExpression(nameof(action))] string? actualExpression = null) =>
        NotThrowInternal(() => action, timeoutAfter, customMessage, actualExpression: actualExpression);

    /// <summary>
    /// Verifies that the provided task function does not throw any exceptions within the specified timeout and returns its result
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T NotThrow<T>([InstantHandle] Func<Task<T>> action, TimeSpan timeoutAfter, string? customMessage = null,
        [CallerArgumentExpression(nameof(action))] string? actualExpression = null) =>
        NotThrowInternal(action, timeoutAfter, customMessage, actualExpression: actualExpression);

    internal static T NotThrowInternal<T>(
        [InstantHandle] Func<Task<T>> action, TimeSpan timeoutAfter,
        [InstantHandle] string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null)
    {
        actualExpression = actualExpression.NormalizeDelegateExpression();
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

            throw new ShouldAssertException(new TaskShouldlyThrowMessage(ex.GetType(), ex, customMessage, shouldlyMethod, actualExpression).ToString());
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

    private static Exception HandleTaskAggregateException(AggregateException exceptionFromTask, string? customMessage, Type exceptionType, string? actualExpression = null)
    {
        var innerException = exceptionFromTask.InnerException
                             ?? throw new ArgumentException("The specified exception is not from Task.Exception or it would have at least one inner exception.", nameof(exceptionFromTask));

        if (innerException.GetType() == exceptionType)
            return innerException;

        throw new ShouldAssertException(new ExpectedActualShouldlyMessage(exceptionType, innerException.GetType(), customMessage, actualExpression: actualExpression).ToString());
    }
}