using Shouldly.Internals;
using Shouldly.Internals.XunitV3Markers;

namespace Shouldly;

[Serializable]
public class ShouldAssertException : Exception, IAssertionException
{
    public ShouldAssertException(string? message) : base(message)
    {
    }

    public ShouldAssertException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    private string? stackTrace;

    public override string StackTrace => StackTraceHelpers.GetStackTrace(this, ref stackTrace);
}