namespace Shouldly.Internals
{
    internal interface ICodeTextGetter
    {
#if !HasStackTraceSupport
        string GetCodeText(object actual);
#else
        string GetCodeText(object actual, System.Diagnostics.StackTrace stackTrace = null);
#endif
    }
}
