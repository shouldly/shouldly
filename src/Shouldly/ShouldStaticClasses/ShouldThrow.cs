using JetBrains.Annotations;

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
        ThrowInternal<TException>(actual, customMessage, actualExpression: actualExpression);

    [DebuggerDisableUserUnhandledExceptions]
    internal static TException ThrowInternal<TException>(
        [InstantHandle] Action actual,
        string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!,
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
            throw new ShouldAssertException(new ShouldlyThrowMessage(typeof(TException), e.GetType(), customMessage, shouldlyMethod, actualExpression).ToString(), e);
        }

        throw new ShouldAssertException(new ShouldlyThrowMessage(typeof(TException), customMessage, shouldlyMethod, actualExpression).ToString());
    }

    /// <summary>
    /// Verifies that the provided action throws an exception of the specified type
    /// </summary>
    public static Exception Throw([InstantHandle] Action actual, Type exceptionType, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
        ThrowInternal(actual, customMessage, exceptionType, actualExpression: actualExpression);

    [DebuggerDisableUserUnhandledExceptions]
    internal static Exception ThrowInternal([InstantHandle] Action actual, string? customMessage, Type exceptionType,
        [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null)
    {
        actualExpression = actualExpression.NormalizeDelegateExpression();
        try
        {
            actual();
        }
        catch (Exception e)
        {
            if (e.GetType() == exceptionType)
            {
                return e;
            }

            throw new ShouldAssertException(new ShouldlyThrowMessage(exceptionType, e.GetType(), customMessage, shouldlyMethod, actualExpression).ToString(), e);
        }

        throw new ShouldAssertException(new ShouldlyThrowMessage(exceptionType, customMessage, shouldlyMethod, actualExpression).ToString());
    }

    /// <summary>
    /// Verifies that the provided function throws an exception of type <typeparamref name="TException"/>
    /// </summary>
    public static TException Throw<TException>([InstantHandle] Func<object?> actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where TException : Exception =>
        ThrowInternal<TException>(actual, customMessage, actualExpression: actualExpression);

    [DebuggerDisableUserUnhandledExceptions]
    internal static TException ThrowInternal<TException>(
        [InstantHandle] Func<object?> actual,
        string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!,
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
            throw new ShouldAssertException(new ShouldlyThrowMessage(typeof(TException), e.GetType(), customMessage, shouldlyMethod, actualExpression).ToString(), e);
        }

        throw new ShouldAssertException(new ShouldlyThrowMessage(typeof(TException), customMessage, shouldlyMethod, actualExpression).ToString());
    }

    /// <summary>
    /// Verifies that the provided function throws an exception of the specified type
    /// </summary>
    public static Exception Throw([InstantHandle] Func<object?> actual, Type exceptionType,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
        ThrowInternal(actual, null, exceptionType, actualExpression: actualExpression);

    /// <summary>
    /// Verifies that the provided function throws an exception of the specified type
    /// </summary>
    public static Exception Throw([InstantHandle] Func<object?> actual, string? customMessage, Type exceptionType,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
        ThrowInternal(actual, customMessage, exceptionType, actualExpression: actualExpression);

    [DebuggerDisableUserUnhandledExceptions]
    internal static Exception ThrowInternal([InstantHandle] Func<object?> actual, string? customMessage, Type exceptionType,
        [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null)
    {
        actualExpression = actualExpression.NormalizeDelegateExpression();
        try
        {
            _ = actual();
        }
        catch (Exception e)
        {
            if (e.GetType() == exceptionType)
            {
                return e;
            }

            throw new ShouldAssertException(new ShouldlyThrowMessage(exceptionType, e.GetType(), customMessage, shouldlyMethod, actualExpression).ToString(), e);
        }

        throw new ShouldAssertException(new ShouldlyThrowMessage(exceptionType, customMessage, shouldlyMethod, actualExpression).ToString());
    }

    /// <summary>
    /// Verifies that the provided action does not throw any exceptions
    /// </summary>
    public static void NotThrow([InstantHandle] Action action, string? customMessage = null,
        [CallerArgumentExpression(nameof(action))] string? actualExpression = null)
    {
        NotThrowInternal(action, customMessage, actualExpression: actualExpression);
    }

    [DebuggerDisableUserUnhandledExceptions]
    internal static void NotThrowInternal([InstantHandle] Action action, string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null)
    {
        actualExpression = actualExpression.NormalizeDelegateExpression();
        try
        {
            action();
        }
        catch (Exception ex)
        {
            throw new ShouldAssertException(new ShouldlyThrowMessage(ex.GetType(), exceptionMessage: ex.Message, customMessage, shouldlyMethod, actualExpression).ToString());
        }
    }

    /// <summary>
    /// Verifies that the provided function does not throw any exceptions and returns its result
    /// </summary>
    public static T NotThrow<T>([InstantHandle] Func<T> action, string? customMessage = null,
        [CallerArgumentExpression(nameof(action))] string? actualExpression = null) =>
        NotThrowInternal(action, customMessage, actualExpression: actualExpression);

    /// <summary>
    /// Used to differentiate between the extension methods and the static methods
    /// </summary>
    [DebuggerDisableUserUnhandledExceptions]
    internal static T NotThrowInternal<T>([InstantHandle] Func<T> action, string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null)
    {
        actualExpression = actualExpression.NormalizeDelegateExpression();
        try
        {
            return action();
        }
        catch (Exception ex)
        {
            throw new ShouldAssertException(new ShouldlyThrowMessage(ex.GetType(), exceptionMessage: ex.Message, customMessage, shouldlyMethod, actualExpression).ToString());
        }
    }
}