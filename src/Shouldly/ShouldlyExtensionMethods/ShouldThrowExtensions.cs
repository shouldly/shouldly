using System.ComponentModel;

namespace Shouldly;

[DebuggerStepThrough]
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldThrowExtensions
{
    /*** ShouldThrow(Action) ***/
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static TException ShouldThrow<TException>(this Action actual, string? customMessage = null)
        where TException : Exception =>
        Should.ThrowInternal<TException>(actual, customMessage);

    /*** ShouldThrow(Func<T>) ***/
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static TException ShouldThrow<TException>(this Func<object?> actual, string? customMessage = null)
        where TException : Exception =>
        Should.ThrowInternal<TException>(actual, customMessage);

    /*** ShouldThrow(Action) ***/
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception ShouldThrow(this Action actual, Type exceptionType, string? customMessage = null) =>
        Should.ThrowInternal(actual, customMessage, exceptionType);

    /*** ShouldThrow(Func<T>) ***/
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception ShouldThrow(this Func<object?> actual, Type exceptionType, string? customMessage = null) =>
        Should.ThrowInternal(actual, customMessage, exceptionType);

    /*** ShouldNotThrow(Action) ***/
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotThrow(this Action action, string? customMessage = null) =>
        Should.NotThrowInternal(action, customMessage);

    /*** ShouldNotThrow(Func<T>) ***/
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ShouldNotThrow<T>(this Func<T> action, string? customMessage = null) =>
        Should.NotThrowInternal(action, customMessage);
}