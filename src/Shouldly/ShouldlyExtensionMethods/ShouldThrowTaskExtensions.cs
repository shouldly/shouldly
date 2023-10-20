namespace Shouldly;

[DebuggerStepThrough]
[ShouldlyMethods]
public static partial class ShouldThrowTaskExtensions
{
    /*** ShouldThrow(Task) ***/
    public static TException ShouldThrow<TException>(this Task actual, string? customMessage = null)
        where TException : Exception
    {
        return ShouldThrow<TException>(() => actual, customMessage);
    }

    /*** ShouldThrow(Task) ***/
    public static Exception ShouldThrow(this Task actual, Type exceptionType)
    {
        return ShouldThrow(() => actual, exceptionType);
    }

    public static Exception ShouldThrow(this Task actual, string? customMessage, Type exceptionType)
    {
        return ShouldThrow(() => actual, customMessage, exceptionType);
    }

    /*** ShouldThrow(Func<Task>) ***/
    public static TException ShouldThrow<TException>(this Func<Task> actual, string? customMessage = null)
        where TException : Exception
    {
        return ShouldThrow<TException>(actual, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);
    }

    /*** ShouldThrow(Func<Task>) ***/
    public static Exception ShouldThrow(this Func<Task> actual, Type exceptionType)
    {
        return ShouldThrow(actual, (string?)null, exceptionType);
    }

    public static Exception ShouldThrow(this Func<Task> actual, string? customMessage, Type exceptionType)
    {
        return ShouldThrow(actual, ShouldlyConfiguration.DefaultTaskTimeout, customMessage, exceptionType);
    }

    /*** ShouldThrow(Task, TimeSpan) ***/
    public static TException ShouldThrow<TException>(this Task actual, TimeSpan timeoutAfter, string? customMessage = null)
        where TException : Exception
    {
        return ShouldThrow<TException>(() => actual, timeoutAfter, customMessage);
    }

    /*** ShouldThrow(Task, TimeSpan) ***/
    public static Exception ShouldThrow(this Task actual, TimeSpan timeoutAfter, Type exceptionType)
    {
        return ShouldThrow(() => actual, timeoutAfter, exceptionType);
    }

    public static Exception ShouldThrow(this Task actual, TimeSpan timeoutAfter, string? customMessage, Type exceptionType)
    {
        return ShouldThrow(() => actual, timeoutAfter, customMessage, exceptionType);
    }

    /*** ShouldThrow(Func<Task>, TimeSpan) ***/
    public static TException ShouldThrow<TException>(this Func<Task> actual, TimeSpan timeoutAfter, string? customMessage = null)
        where TException : Exception
    {
        return Should.ThrowInternal<TException>(actual, timeoutAfter, customMessage);
    }

    /*** ShouldThrow(Func<Task>, TimeSpan) ***/
    public static Exception ShouldThrow(this Func<Task> actual, TimeSpan timeoutAfter, Type exceptionType)
    {
        return ShouldThrow(actual, timeoutAfter, (string?)null, exceptionType);
    }

    public static Exception ShouldThrow(this Func<Task> actual, TimeSpan timeoutAfter, string? customMessage, Type exceptionType)
    {
        return Should.ThrowInternal(actual, timeoutAfter, customMessage, exceptionType);
    }

    /*** ShouldNotThrow(Task) ***/
    public static void ShouldNotThrow(this Task action, string? customMessage = null)
    {
        Should.NotThrowInternal(() => action, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);
    }

    /*** ShouldNotThrow(Task<T>) ***/
    public static T ShouldNotThrow<T>(this Task<T> action, string? customMessage = null)
    {
        return ShouldNotThrow(() => action, customMessage);
    }

    /*** ShouldNotThrow(Func<Task>) ***/
    public static void ShouldNotThrow(this Func<Task> action, string? customMessage = null)
    {
        Should.NotThrowInternal(action, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);
    }

    /*** ShouldNotThrow(Task, TimeSpan) ***/
    public static void ShouldNotThrow(this Task action, TimeSpan timeoutAfter, string? customMessage = null)
    {
        ShouldNotThrow(() => action, timeoutAfter, customMessage);
    }

    /*** ShouldNotThrow(Func<Task>, TimeSpan) ***/
    public static void ShouldNotThrow(this Func<Task> action, TimeSpan timeoutAfter, string? customMessage = null)
    {
        Should.NotThrowInternal(action, timeoutAfter, customMessage);
    }

    /*** ShouldNotThrow(Func<Task<T>>) ***/
    public static T ShouldNotThrow<T>(this Func<Task<T>> action, string? customMessage = null)
    {
        return Should.NotThrowInternal(action, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);
    }

    /*** ShouldNotThrow(Task<T>, TimeSpan) ***/
    public static T ShouldNotThrow<T>(this Task<T> action, TimeSpan timeoutAfter, string? customMessage = null)
    {
        return ShouldNotThrow(() => action, timeoutAfter, customMessage);
    }

    /*** ShouldNotThrow(Func<Task<T>>, TimeSpan) ***/
    public static T ShouldNotThrow<T>(this Func<Task<T>> action, TimeSpan timeoutAfter, string? customMessage = null)
    {
        return Should.NotThrowInternal(action, timeoutAfter, customMessage);
    }
}