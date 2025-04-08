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
    /*** ShouldNotThrowAsync(Task) ***/
    /// <summary>
    /// Asynchronously verifies that the Task completes without throwing any exceptions.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Task ShouldNotThrowAsync(this Task task, string? customMessage = null) =>
        Should.NotThrowAsync(task, customMessage);

    /*** ShouldNotThrowAsync(Func<Task>) ***/
    /// <summary>
    /// Asynchronously verifies that the function returning a Task completes without throwing any exceptions.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Task ShouldNotThrowAsync(this Func<Task> actual, string? customMessage = null) =>
        Should.NotThrowAsync(actual, customMessage);
}