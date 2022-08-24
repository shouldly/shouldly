namespace Shouldly.Internals;

internal interface ICodeTextGetter
{
    string? GetCodeText(object? actual, System.Diagnostics.StackTrace? stackTrace = null);
}