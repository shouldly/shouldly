using System.Diagnostics;

namespace Shouldly.Internals
{
#if DOTNET5_4
    internal interface ICodeTextGetter
    {
        string GetCodeText(object actual);
    }
#else
    internal interface ICodeTextGetter
    {
        string GetCodeText(object actual, StackTrace stackTrace = null);
    }
#endif
}
