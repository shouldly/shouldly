using Shouldly.Internals;
using Shouldly.Internals.XunitV3Markers;

namespace Shouldly;

/// <summary>
/// Exception thrown when an operation times out
/// </summary>
public class ShouldlyTimeoutException : TimeoutException, ITestTimeoutException
{
    /// <summary>
    /// Creates a new ShouldlyTimeoutException
    /// </summary>
    public ShouldlyTimeoutException()
    {
    }

    /// <summary>
    /// Creates a new ShouldlyTimeoutException with the specified message and inner exception
    /// </summary>
    public ShouldlyTimeoutException(string? message, ShouldlyTimeoutException? inner) : base(message, inner)
    {
    }

    private string? stackTrace;

    /// <inheritdoc/>
    public override string StackTrace => StackTraceHelpers.GetStackTrace(this, ref stackTrace);
}