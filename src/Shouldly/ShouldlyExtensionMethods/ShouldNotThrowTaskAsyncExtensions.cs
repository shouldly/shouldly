using System.ComponentModel;

namespace Shouldly;

[DebuggerStepThrough]
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldNotThrowTaskAsyncExtensions
{
    /*** ShouldNotThrowAsync(Task) ***/
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Task ShouldNotThrowAsync(this Task task, string? customMessage = null) =>
        Should.NotThrowAsync(task, customMessage);

    /*** ShouldNotThrowAsync(Func<Task>) ***/
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Task ShouldNotThrowAsync(this Func<Task> actual, string? customMessage = null) =>
        Should.NotThrowAsync(actual, customMessage);
}