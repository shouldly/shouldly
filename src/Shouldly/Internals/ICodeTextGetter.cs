namespace Shouldly.Internals;

interface ICodeTextGetter
{
    [RequiresUnreferencedCode("Implementations may walk the stack trace via StackFrame.GetMethod() to locate the call site. Method metadata may be trimmed away.")]
    string? GetCodeText(object? actual, StackTrace? stackTrace = null);
}
