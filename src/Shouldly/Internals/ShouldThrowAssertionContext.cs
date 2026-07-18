namespace Shouldly;

class ShouldThrowAssertionContext : ShouldlyAssertionContext
{
    public string? ExceptionMessage { get; }

    public bool IsAsync { get; }

    /// <summary>
    /// Only pass stacktrace if asynchronous
    /// </summary>
    internal ShouldThrowAssertionContext(object? expected, object? actual = null, string? exceptionMessage = null,
        bool isAsync = false,
        StackTrace? stackTrace = null,
        [CallerMemberName] string shouldlyMethod = null!,
        string? actualExpression = null) : base(shouldlyMethod, expected, actual, stackTrace, actualExpression)
    {
        ExceptionMessage = exceptionMessage;
        IsAsync = isAsync;
    }
}