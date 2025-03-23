using Shouldly.Internals;
using Shouldly.Internals.XunitV3Markers;

namespace Shouldly;

/// <summary>
/// Exception thrown when a Shouldly assertion fails
/// </summary>
[Serializable]
public class ShouldAssertException : Exception, IAssertionException
{
    /// <summary>
    /// Creates a new ShouldAssertException with the specified message
    /// </summary>
    public ShouldAssertException(string? message) : base(message)
    {
    }

    /// <summary>
    /// Creates a new ShouldAssertException with the specified message and inner exception
    /// </summary>
    public ShouldAssertException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    private string? stackTrace;

    /// <summary>
    /// Gets the stack trace for this exception
    /// </summary>
    public override string StackTrace => StackTraceHelpers.GetStackTrace(this, ref stackTrace);
}