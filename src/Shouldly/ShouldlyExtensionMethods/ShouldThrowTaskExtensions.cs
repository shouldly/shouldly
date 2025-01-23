using System.ComponentModel;

namespace Shouldly;

[DebuggerStepThrough]
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldThrowTaskExtensions
{
    /*** ShouldThrow(Task) ***/
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static TException ShouldThrow<TException>(this Task actual, string? customMessage = null)
        where TException : Exception
    {
        return ShouldThrow<TException>(() => actual, customMessage);
    }

    /*** ShouldThrow(Task) ***/
    [MethodImpl(MethodImplOptions.NoInlining)]    
    public static Exception ShouldThrow(this Task actual, Type exceptionType)
    {
        return ShouldThrow(() => actual, exceptionType);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception ShouldThrow(this Task actual, string? customMessage, Type exceptionType)
    {
        return ShouldThrow(() => actual, customMessage, exceptionType);
    }

    /*** ShouldThrow(Func<Task>) ***/
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static TException ShouldThrow<TException>(this Func<Task> actual, string? customMessage = null)
        where TException : Exception =>
        ShouldThrow<TException>(actual, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);

    /*** ShouldThrow(Func<Task>) ***/
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception ShouldThrow(this Func<Task> actual, Type exceptionType) =>
        ShouldThrow(actual, (string?)null, exceptionType);

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception ShouldThrow(this Func<Task> actual, string? customMessage, Type exceptionType) =>
        ShouldThrow(actual, ShouldlyConfiguration.DefaultTaskTimeout, customMessage, exceptionType);

    /*** ShouldThrow(Task, TimeSpan) ***/
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static TException ShouldThrow<TException>(this Task actual, TimeSpan timeoutAfter, string? customMessage = null)
        where TException : Exception
    {
        return ShouldThrow<TException>(() => actual, timeoutAfter, customMessage);
    }

    /*** ShouldThrow(Task, TimeSpan) ***/
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception ShouldThrow(this Task actual, TimeSpan timeoutAfter, Type exceptionType)
    {
        return ShouldThrow(() => actual, timeoutAfter, exceptionType);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception ShouldThrow(this Task actual, TimeSpan timeoutAfter, string? customMessage, Type exceptionType)
    {
        return ShouldThrow(() => actual, timeoutAfter, customMessage, exceptionType);
    }

    /*** ShouldThrow(Func<Task>, TimeSpan) ***/
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static TException ShouldThrow<TException>(this Func<Task> actual, TimeSpan timeoutAfter, string? customMessage = null)
        where TException : Exception =>
        Should.ThrowInternal<TException>(actual, timeoutAfter, customMessage);

    /*** ShouldThrow(Func<Task>, TimeSpan) ***/
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception ShouldThrow(this Func<Task> actual, TimeSpan timeoutAfter, Type exceptionType) =>
        ShouldThrow(actual, timeoutAfter, (string?)null, exceptionType);

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception ShouldThrow(this Func<Task> actual, TimeSpan timeoutAfter, string? customMessage, Type exceptionType) =>
        Should.ThrowInternal(actual, timeoutAfter, customMessage, exceptionType);

    /*** ShouldNotThrow(Task) ***/
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotThrow(this Task action, string? customMessage = null)
    {
        Should.NotThrowInternal(() => action, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);
    }

    /*** ShouldNotThrow(Task<T>) ***/
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ShouldNotThrow<T>(this Task<T> action, string? customMessage = null)
    {
        return ShouldNotThrow(() => action, customMessage);
    }

    /*** ShouldNotThrow(Func<Task>) ***/
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotThrow(this Func<Task> action, string? customMessage = null)
    {
        Should.NotThrowInternal(action, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);
    }

    /*** ShouldNotThrow(Task, TimeSpan) ***/
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotThrow(this Task action, TimeSpan timeoutAfter, string? customMessage = null)
    {
        ShouldNotThrow(() => action, timeoutAfter, customMessage);
    }

    /*** ShouldNotThrow(Func<Task>, TimeSpan) ***/
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotThrow(this Func<Task> action, TimeSpan timeoutAfter, string? customMessage = null)
    {
        Should.NotThrowInternal(action, timeoutAfter, customMessage);
    }

    /*** ShouldNotThrow(Func<Task<T>>) ***/
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ShouldNotThrow<T>(this Func<Task<T>> action, string? customMessage = null) =>
        Should.NotThrowInternal(action, ShouldlyConfiguration.DefaultTaskTimeout, customMessage);

    /*** ShouldNotThrow(Task<T>, TimeSpan) ***/
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ShouldNotThrow<T>(this Task<T> action, TimeSpan timeoutAfter, string? customMessage = null) =>
        ShouldNotThrow(() => action, timeoutAfter, customMessage);

    /*** ShouldNotThrow(Func<Task<T>>, TimeSpan) ***/
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ShouldNotThrow<T>(this Func<Task<T>> action, TimeSpan timeoutAfter, string? customMessage = null) =>
        Should.NotThrowInternal(action, timeoutAfter, customMessage);
}