using Shouldly.Internals;
using Shouldly.Internals.XunitV3Markers;

namespace Shouldly;

public class ShouldlyTimeoutException : TimeoutException, ITestTimeoutException
{
    public ShouldlyTimeoutException()
    {
    }

    public ShouldlyTimeoutException(string? message, ShouldlyTimeoutException? inner) : base(message, inner)
    {
    }

    private string? stackTrace;

    public override string StackTrace => StackTraceHelpers.GetStackTrace(this, ref stackTrace);
}