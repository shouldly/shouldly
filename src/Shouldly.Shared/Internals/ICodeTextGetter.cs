using System.Diagnostics;

namespace Shouldly.Internals
{
    internal interface ICodeTextGetter
    {
#if PORTABLE
        string GetCodeText(object actual);
#else
        string GetCodeText(object actual, StackTrace stackTrace = null);
#endif
    }
}
