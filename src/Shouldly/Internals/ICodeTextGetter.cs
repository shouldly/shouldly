namespace Shouldly.Internals
{
    internal interface ICodeTextGetter
    {
#if StackTrace
        string GetCodeText(object actual, System.Diagnostics.StackTrace stackTrace = null);
#else
        string GetCodeText(object actual);
#endif
    }
}
