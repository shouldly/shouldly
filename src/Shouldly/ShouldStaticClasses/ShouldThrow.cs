using JetBrains.Annotations;
using Shouldly.Internals.Failures;

namespace Shouldly;

[DebuggerStepThrough]
[ShouldlyMethods]
public static partial class Should
{
    /// <summary>
    /// Verifies that the provided action throws an exception of type <typeparamref name="TException"/>
    /// </summary>
    public static TException Throw<TException>([InstantHandle] Action actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where TException : Exception =>
        ThrowInternal<TException>(actual, customMessage, Fail.Throw, actualExpression);

    [DebuggerDisableUserUnhandledExceptions]
    internal static TException ThrowInternal<TException>(
        [InstantHandle] Action actual,
        string? customMessage,
        ThrowFailure failure,
        string? actualExpression = null)
        where TException : Exception
    {
        actualExpression = actualExpression.NormalizeDelegateExpression();
        try
        {
            actual();
        }
        catch (TException e)
        {
            return e;
        }
        catch (Exception e)
        {
            throw failure.With(typeof(TException), e.GetType(), isTask: false).ForDelegate(customMessage, actualExpression, inner: e);
        }

        throw failure.With(typeof(TException), thrown: null, isTask: false).ForDelegate(customMessage, actualExpression);
    }

    /// <summary>
    /// Verifies that the provided action throws an exception of the specified type
    /// </summary>
    public static Exception Throw([InstantHandle] Action actual, Type exceptionType, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
        ThrowInternal(actual, customMessage, exceptionType, Fail.Throw, actualExpression);

    [DebuggerDisableUserUnhandledExceptions]
    internal static Exception ThrowInternal([InstantHandle] Action actual, string? customMessage, Type exceptionType,
        ThrowFailure failure,
        string? actualExpression = null)
    {
        actualExpression = actualExpression.NormalizeDelegateExpression();
        try
        {
            actual();
        }
        catch (Exception e)
        {
            if (exceptionType.IsInstanceOfType(e))
            {
                return e;
            }

            throw failure.With(exceptionType, e.GetType(), isTask: false).ForDelegate(customMessage, actualExpression, inner: e);
        }

        throw failure.With(exceptionType, thrown: null, isTask: false).ForDelegate(customMessage, actualExpression);
    }

    /// <summary>
    /// Verifies that the provided function throws an exception of type <typeparamref name="TException"/>
    /// </summary>
    public static TException Throw<TException>([InstantHandle] Func<object?> actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where TException : Exception =>
        ThrowInternal<TException>(actual, customMessage, Fail.Throw, actualExpression);

    [DebuggerDisableUserUnhandledExceptions]
    internal static TException ThrowInternal<TException>(
        [InstantHandle] Func<object?> actual,
        string? customMessage,
        ThrowFailure failure,
        string? actualExpression = null)
        where TException : Exception
    {
        actualExpression = actualExpression.NormalizeDelegateExpression();
        try
        {
            _ = actual();
        }
        catch (TException e)
        {
            return e;
        }
        catch (Exception e)
        {
            throw failure.With(typeof(TException), e.GetType(), isTask: false).ForDelegate(customMessage, actualExpression, inner: e);
        }

        throw failure.With(typeof(TException), thrown: null, isTask: false).ForDelegate(customMessage, actualExpression);
    }

    /// <summary>
    /// Verifies that the provided function throws an exception of the specified type
    /// </summary>
    public static Exception Throw([InstantHandle] Func<object?> actual, Type exceptionType,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
        ThrowInternal(actual, null, exceptionType, Fail.Throw, actualExpression);

    /// <summary>
    /// Verifies that the provided function throws an exception of the specified type
    /// </summary>
    public static Exception Throw([InstantHandle] Func<object?> actual, string? customMessage, Type exceptionType,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
        ThrowInternal(actual, customMessage, exceptionType, Fail.Throw, actualExpression);

    [DebuggerDisableUserUnhandledExceptions]
    internal static Exception ThrowInternal([InstantHandle] Func<object?> actual, string? customMessage, Type exceptionType,
        ThrowFailure failure,
        string? actualExpression = null)
    {
        actualExpression = actualExpression.NormalizeDelegateExpression();
        try
        {
            _ = actual();
        }
        catch (Exception e)
        {
            if (exceptionType.IsInstanceOfType(e))
            {
                return e;
            }

            throw failure.With(exceptionType, e.GetType(), isTask: false).ForDelegate(customMessage, actualExpression, inner: e);
        }

        throw failure.With(exceptionType, thrown: null, isTask: false).ForDelegate(customMessage, actualExpression);
    }

    /// <summary>
    /// Verifies that the provided action throws an exception of exactly type <typeparamref name="TException"/>.
    /// A derived exception type does not match.
    /// </summary>
    public static TException ThrowExactly<TException>([InstantHandle] Action actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where TException : Exception =>
        ThrowExactlyInternal<TException>(actual, customMessage, Fail.ThrowExactly, actualExpression);

    /// <summary>
    /// Verifies that the provided function throws an exception of exactly type <typeparamref name="TException"/>.
    /// A derived exception type does not match.
    /// </summary>
    public static TException ThrowExactly<TException>([InstantHandle] Func<object?> actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where TException : Exception =>
        ThrowExactlyInternal<TException>(actual, customMessage, Fail.ThrowExactly, actualExpression);

    [DebuggerDisableUserUnhandledExceptions]
    internal static TException ThrowExactlyInternal<TException>(
        [InstantHandle] Action actual,
        string? customMessage,
        ThrowFailure failure,
        string? actualExpression = null)
        where TException : Exception
    {
        actualExpression = actualExpression.NormalizeDelegateExpression();
        try
        {
            actual();
        }
        catch (Exception e)
        {
            if (e.GetType() == typeof(TException))
            {
                return (TException)e;
            }

            throw failure.With(typeof(TException), e.GetType(), isTask: false).ForDelegate(customMessage, actualExpression, inner: e);
        }

        throw failure.With(typeof(TException), thrown: null, isTask: false).ForDelegate(customMessage, actualExpression);
    }

    [DebuggerDisableUserUnhandledExceptions]
    internal static TException ThrowExactlyInternal<TException>(
        [InstantHandle] Func<object?> actual,
        string? customMessage,
        ThrowFailure failure,
        string? actualExpression = null)
        where TException : Exception =>
        ThrowExactlyInternal<TException>(() => { _ = actual(); }, customMessage, failure, actualExpression);

    /// <summary>
    /// Verifies that the provided action does not throw any exceptions
    /// </summary>
    public static void NotThrow([InstantHandle] Action action, string? customMessage = null,
        [CallerArgumentExpression(nameof(action))] string? actualExpression = null)
    {
        NotThrowInternal(action, customMessage, Fail.NotThrow, actualExpression);
    }

    [DebuggerDisableUserUnhandledExceptions]
    internal static void NotThrowInternal([InstantHandle] Action action, string? customMessage,
        NotThrowFailure failure,
        string? actualExpression = null)
    {
        actualExpression = actualExpression.NormalizeDelegateExpression();
        try
        {
            action();
        }
        catch (Exception ex)
        {
            throw failure.With(ex.GetType(), ex.Message, isTask: false).ForDelegate(customMessage, actualExpression);
        }
    }

    /// <summary>
    /// Verifies that the provided function does not throw any exceptions and returns its result
    /// </summary>
    public static T NotThrow<T>([InstantHandle] Func<T> action, string? customMessage = null,
        [CallerArgumentExpression(nameof(action))] string? actualExpression = null) =>
        NotThrowInternal(action, customMessage, Fail.NotThrow, actualExpression);

    /// <summary>
    /// Verifies that the provided action does not throw an exception of type <typeparamref name="TException"/>.
    /// Exceptions of other types are not caught and will propagate to the caller.
    /// </summary>
    public static void NotThrow<TException>([InstantHandle] Action action, string? customMessage = null,
        [CallerArgumentExpression(nameof(action))] string? actualExpression = null)
        where TException : Exception =>
        NotThrowInternal<TException>(action, customMessage, Fail.NotThrow, actualExpression);

    /// <summary>
    /// Used to differentiate between the extension methods and the static methods
    /// </summary>
    [DebuggerDisableUserUnhandledExceptions]
    internal static T NotThrowInternal<T>([InstantHandle] Func<T> action, string? customMessage,
        NotThrowFailure failure,
        string? actualExpression = null)
    {
        actualExpression = actualExpression.NormalizeDelegateExpression();
        try
        {
            return action();
        }
        catch (Exception ex)
        {
            throw failure.With(ex.GetType(), ex.Message, isTask: false).ForDelegate(customMessage, actualExpression);
        }
    }

    [DebuggerDisableUserUnhandledExceptions]
    internal static void NotThrowInternal<TException>([InstantHandle] Action action, string? customMessage,
        NotThrowFailure failure,
        string? actualExpression = null)
        where TException : Exception
    {
        actualExpression = actualExpression.NormalizeDelegateExpression();
        try
        {
            action();
        }
        catch (TException ex)
        {
            throw failure.WithDisallowed(typeof(TException), ex, isTask: false).ForDelegate(customMessage, actualExpression, inner: ex);
        }
    }
}
