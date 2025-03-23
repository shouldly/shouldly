namespace Shouldly.Internals;

interface ICodeTextGetter
{
    string? GetCodeText(object? actual, StackTrace? stackTrace = null);
}