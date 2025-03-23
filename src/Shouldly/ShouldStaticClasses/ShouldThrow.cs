using JetBrains.Annotations;

namespace Shouldly;

[DebuggerStepThrough]
[ShouldlyMethods]
public static partial class Should
{
    /*** Should.Throw(Action) ***/
    /// <summary>
    /// Verifies that the provided action throws an exception of type <typeparamref name="TException"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static TException Throw<TException>([InstantHandle] Action actual, string? customMessage = null)
        where TException : Exception =>
        ThrowInternal<TException>(actual, customMessage);

    internal static TException ThrowInternal<TException>(
        [InstantHandle] Action actual,
        string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!)
        where TException : Exception
    {
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
            throw new ShouldAssertException(new ShouldlyThrowMessage(typeof(TException), e.GetType(), customMessage, shouldlyMethod).ToString(), e);
        }

        throw new ShouldAssertException(new ShouldlyThrowMessage(typeof(TException), customMessage: customMessage, shouldlyMethod).ToString());
    }

    /*** Should.Throw(Action) ***/
    /// <summary>
    /// Verifies that the provided action throws an exception of the specified type
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception Throw([InstantHandle] Action actual, Type exceptionType, string? customMessage = null) =>
        ThrowInternal(actual, customMessage, exceptionType);

    internal static Exception ThrowInternal([InstantHandle] Action actual, string? customMessage, Type exceptionType,
        [CallerMemberName] string shouldlyMethod = null!)
    {
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

            throw new ShouldAssertException(new ShouldlyThrowMessage(exceptionType, e.GetType(), customMessage, shouldlyMethod).ToString(), e);
        }

        throw new ShouldAssertException(new ShouldlyThrowMessage(exceptionType, customMessage: customMessage, shouldlyMethod).ToString());
    }

    /*** Should.Throw(Func<T>) ***/
    /// <summary>
    /// Verifies that the provided function throws an exception of type <typeparamref name="TException"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static TException Throw<TException>([InstantHandle] Func<object?> actual, string? customMessage = null)
        where TException : Exception =>
        ThrowInternal<TException>(actual, customMessage);

    internal static TException ThrowInternal<TException>(
        [InstantHandle] Func<object?> actual,
        string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!)
        where TException : Exception
    {
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
            throw new ShouldAssertException(new ShouldlyThrowMessage(typeof(TException), e.GetType(), customMessage: customMessage, shouldlyMethod).ToString(), e);
        }

        throw new ShouldAssertException(new ShouldlyThrowMessage(typeof(TException), customMessage: customMessage, shouldlyMethod).ToString());
    }

    /*** Should.Throw(Func<T>) ***/
    /// <summary>
    /// Verifies that the provided function throws an exception of the specified type
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception Throw([InstantHandle] Func<object?> actual, Type exceptionType) =>
        ThrowInternal(actual, null, exceptionType);

    /// <summary>
    /// Verifies that the provided function throws an exception of the specified type
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception Throw([InstantHandle] Func<object?> actual, string? customMessage, Type exceptionType) =>
        ThrowInternal(actual, customMessage, exceptionType);

    internal static Exception ThrowInternal([InstantHandle] Func<object?> actual, string? customMessage, Type exceptionType,
        [CallerMemberName] string shouldlyMethod = null!)
    {
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

            throw new ShouldAssertException(new ShouldlyThrowMessage(exceptionType, e.GetType(), customMessage, shouldlyMethod).ToString(), e);
        }

        throw new ShouldAssertException(new ShouldlyThrowMessage(exceptionType, customMessage: customMessage, shouldlyMethod).ToString());
    }

    /*** Should.NotThrow(Action) ***/
    /// <summary>
    /// Verifies that the provided action does not throw any exceptions
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void NotThrow([InstantHandle] Action action, string? customMessage = null)
    {
        NotThrowInternal(action, customMessage);
    }

    internal static void NotThrowInternal([InstantHandle] Action action, string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            throw new ShouldAssertException(new ShouldlyThrowMessage(ex.GetType(), ex.Message, customMessage, shouldlyMethod).ToString());
        }
    }

    /*** Should.NotThrow(Func<T>) ***/
    /// <summary>
    /// Verifies that the provided function does not throw any exceptions and returns its result
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T NotThrow<T>([InstantHandle] Func<T> action, string? customMessage = null) =>
        NotThrowInternal(action, customMessage);

    /// <summary>
    /// Used to differentiate between the extension methods and the static methods
    /// </summary>
    internal static T NotThrowInternal<T>([InstantHandle] Func<T> action, string? customMessage,
        [CallerMemberName] string shouldlyMethod = null!)
    {
        try
        {
            return action();
        }
        catch (Exception ex)
        {
            throw new ShouldAssertException(new ShouldlyThrowMessage(ex.GetType(), ex.Message, customMessage, shouldlyMethod).ToString());
        }
    }
}