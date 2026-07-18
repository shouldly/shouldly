using System.ComponentModel;

namespace Shouldly;

/// <summary>
/// Extension methods for asynchronous exception assertions
/// </summary>
[DebuggerStepThrough]
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldNotThrowTaskAsyncExtensions
{
    /// <summary>
    /// Asynchronously verifies that the Task completes without throwing any exceptions.
    /// </summary>
    public static Task ShouldNotThrowAsync(this Task task, string? customMessage = null,
        [CallerArgumentExpression(nameof(task))] string? actualExpression = null) =>
        Should.NotThrowAsync(task, customMessage, actualExpression);

    /// <summary>
    /// Asynchronously verifies that the function returning a Task completes without throwing any exceptions.
    /// </summary>
    public static Task ShouldNotThrowAsync(this Func<Task> actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
        Should.NotThrowAsync(actual, customMessage, actualExpression);
}