using System.ComponentModel;

namespace Shouldly;

/// <summary>
/// Extension methods for exception assertions
/// </summary>
[DebuggerStepThrough]
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldThrowExtensions
{
    /// <summary>
    /// Verifies that the action throws a <typeparamref name="TException"/> exception.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static TException ShouldThrow<TException>(this Action actual, string? customMessage = null)
        where TException : Exception =>
        Should.ThrowInternal<TException>(actual, customMessage);

    /// <summary>
    /// Verifies that the function throws a <typeparamref name="TException"/> exception.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static TException ShouldThrow<TException>(this Func<object?> actual, string? customMessage = null)
        where TException : Exception =>
        Should.ThrowInternal<TException>(actual, customMessage);

    /// <summary>
    /// Verifies that the action throws an exception of the specified type.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception ShouldThrow(this Action actual, Type exceptionType, string? customMessage = null) =>
        Should.ThrowInternal(actual, customMessage, exceptionType);

    /// <summary>
    /// Verifies that the function throws an exception of the specified type.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Exception ShouldThrow(this Func<object?> actual, Type exceptionType, string? customMessage = null) =>
        Should.ThrowInternal(actual, customMessage, exceptionType);

    /// <summary>
    /// Verifies that the action completes without throwing any exceptions.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotThrow(this Action action, string? customMessage = null) =>
        Should.NotThrowInternal(action, customMessage);

    /// <summary>
    /// Verifies that the function completes without throwing any exceptions and returns the result.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ShouldNotThrow<T>(this Func<T> action, string? customMessage = null) =>
        Should.NotThrowInternal(action, customMessage);
}