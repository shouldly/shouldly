namespace Shouldly.Internals;

interface ICodeTextGetter
{
    string? GetCodeText(object? actual, System.Diagnostics.StackTrace? stackTrace = null);
}