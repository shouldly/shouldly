namespace Shouldly.Internals.Failures;

/// <summary>
/// A failure with what the check established bound to it, ready to be placed at the call site that failed.
/// </summary>
readonly struct FailureMessage
{
    private readonly Failure _failure;
    private readonly Func<Sentence, string> _wording;

    public FailureMessage(Failure failure, Func<Sentence, string> wording)
    {
        _failure = failure;
        _wording = wording;
    }

    /// <summary>
    /// The exception to throw for a failing assertion. The assertion itself throws it, so the stack trace and the
    /// compiler's flow analysis see the assertion method.
    /// </summary>
    /// <param name="actual">The value under test.</param>
    /// <param name="customMessage">The caller's own message, shown as additional info.</param>
    /// <param name="actualExpression">The expression captured by <c>[CallerArgumentExpression]</c>, if any.</param>
    /// <param name="inner">The exception that caused the failure, if any.</param>
    /// <param name="stackTrace">A stack trace captured before an <c>await</c>, for the stack-walking fallback.</param>
    public ShouldAssertException For(object? actual, string? customMessage, string? actualExpression, Exception? inner = null, StackTrace? stackTrace = null) =>
        new(Text(actual, customMessage, actualExpression, stackTrace), inner);

    /// <summary>
    /// The exception to throw for a failing assertion on a delegate or task, which has no value to show.
    /// </summary>
    public ShouldAssertException ForDelegate(string? customMessage, string? actualExpression, Exception? inner = null, StackTrace? stackTrace = null) =>
        For(null, customMessage, actualExpression, inner, stackTrace);

    /// <summary>
    /// The failure message, without throwing.
    /// </summary>
    public string Text(object? actual, string? customMessage, string? actualExpression, StackTrace? stackTrace = null)
    {
        var subject = Subject.Resolve(_failure.Name, actual, actualExpression, stackTrace, out var fromSource);
        return WithAdditionalInfo(_wording(new(_failure, subject, fromSource, actual)), customMessage);
    }

    /// <summary>
    /// Appends the caller's own message to a failure message.
    /// </summary>
    public static string WithAdditionalInfo(string message, string? customMessage)
    {
        if (customMessage == null)
            return message;

        return message +
               $"""


                Additional Info:
                    {customMessage}
                """;
    }
}
