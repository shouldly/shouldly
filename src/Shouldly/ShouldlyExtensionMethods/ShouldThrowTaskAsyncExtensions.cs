namespace Shouldly;

[DebuggerStepThrough]
[ShouldlyMethods]
public static partial class ShouldThrowAsyncExtensions
{
    /*** ShouldThrowAsync(Task) ***/
    public static Task<TException> ShouldThrowAsync<TException>(this Task task, string? customMessage = null)
        where TException : Exception =>
        Should.ThrowAsync<TException>(task, customMessage);

    /*** ShouldThrowAsync(Task) ***/
    public static Task<Exception> ShouldThrowAsync(this Task task, Type exceptionType, string? customMessage = null) =>
        Should.ThrowAsync(task, exceptionType, customMessage);

    /*** ShouldThrowAsync(Func<Task>) ***/
    public static Task<TException> ShouldThrowAsync<TException>(this Func<Task> actual, string? customMessage = null)
        where TException : Exception =>
        Should.ThrowAsync<TException>(actual, customMessage);

    /*** ShouldThrowAsync(Func<Task>) ***/
    public static Task<Exception> ShouldThrowAsync(this Func<Task> actual, Type exceptionType, string? customMessage = null) =>
        Should.ThrowAsync(actual, exceptionType, customMessage);
}