namespace Shouldly;

/// <summary>
/// Exception thrown when an operation does not complete within the specified time
/// </summary>
public class ShouldCompleteInException : ShouldlyTimeoutException // Need to do this to not break existing API
{
    /// <summary>
    /// Creates a new ShouldCompleteInException with the specified message and inner exception
    /// </summary>
    public ShouldCompleteInException(string? message, ShouldlyTimeoutException? inner) : base(message, inner)
    {
    }
}