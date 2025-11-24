using System.ComponentModel;

namespace Shouldly;

/// <summary>
/// Extension methods for asynchronous exception assertions
/// </summary>
[DebuggerStepThrough]
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldThrowAsyncExtensions
{
    extension(Task task)
    {
        /// <summary>
        /// Asynchronously verifies that the Task throws a <typeparamref name="TException"/> exception.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Task<TException> ShouldThrowAsync<TException>(string? customMessage = null)
            where TException : Exception =>
            Should.ThrowAsync<TException>(task, customMessage);

        /// <summary>
        /// Asynchronously verifies that the Task throws an exception of the specified type.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Task<Exception> ShouldThrowAsync(Type exceptionType, string? customMessage = null) =>
            Should.ThrowAsync(task, exceptionType, customMessage);
    }

    extension(Func<Task> actual)
    {
        /// <summary>
        /// Asynchronously verifies that the function returning a Task throws an <typeparamref name="TException"/> exception.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Task<TException> ShouldThrowAsync<TException>(string? customMessage = null)
            where TException : Exception =>
            Should.ThrowAsync<TException>(actual, customMessage);

        /// <summary>
        /// Asynchronously verifies that the function returning a Task throws an exception of the specified type.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Task<Exception> ShouldThrowAsync(Type exceptionType, string? customMessage = null) =>
            Should.ThrowAsync(actual, exceptionType, customMessage);
    }
}