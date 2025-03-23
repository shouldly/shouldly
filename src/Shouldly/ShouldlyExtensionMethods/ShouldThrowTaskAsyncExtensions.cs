using System.ComponentModel;

namespace Shouldly;

[DebuggerStepThrough]
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldThrowAsyncExtensions
{
    /*** ShouldThrowAsync(Task) ***/
    /// <summary>
    /// Asynchronously verifies that the Task throws a <typeparamref name="TException"/> exception.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Task<TException> ShouldThrowAsync<TException>(this Task task, string? customMessage = null)
        where TException : Exception =>
        Should.ThrowAsync<TException>(task, customMessage);

    /*** ShouldThrowAsync(Task) ***/
    /// <summary>
    /// Asynchronously verifies that the Task throws an exception of the specified type.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Task<Exception> ShouldThrowAsync(this Task task, Type exceptionType, string? customMessage = null) =>
        Should.ThrowAsync(task, exceptionType, customMessage);

    /*** ShouldThrowAsync(Func<Task>) ***/
    /// <summary>
    /// Asynchronously verifies that the function returning a Task throws an <typeparamref name="TException"/> exception.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Task<TException> ShouldThrowAsync<TException>(this Func<Task> actual, string? customMessage = null)
        where TException : Exception =>
        Should.ThrowAsync<TException>(actual, customMessage);

    /*** ShouldThrowAsync(Func<Task>) ***/
    /// <summary>
    /// Asynchronously verifies that the function returning a Task throws an exception of the specified type.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Task<Exception> ShouldThrowAsync(this Func<Task> actual, Type exceptionType, string? customMessage = null) =>
        Should.ThrowAsync(actual, exceptionType, customMessage);
}