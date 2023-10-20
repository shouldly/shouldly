namespace Shouldly;

[DebuggerStepThrough]
[ShouldlyMethods]
public static partial class ShouldNotThrowTaskAsyncExtensions
{
    /*** ShouldNotThrowAsync(Task) ***/
    public static Task ShouldNotThrowAsync(this Task task, string? customMessage = null)
    {
        return Should.NotThrowAsync(task, customMessage);
    }

    /*** ShouldNotThrowAsync(Func<Task>) ***/
    public static Task ShouldNotThrowAsync(this Func<Task> actual, string? customMessage = null)
    {
        return Should.NotThrowAsync(actual, customMessage);
    }
}