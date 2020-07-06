using System.Diagnostics;

namespace Shouldly.Internals
{
    internal interface ICodeTextGetter
    {
        string? GetCodeText(object? actual, StackTrace? stackTrace = null);
    }
}
