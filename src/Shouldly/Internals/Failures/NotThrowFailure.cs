namespace Shouldly.Internals.Failures;

/// <summary>
/// A delegate or task should not have thrown.
/// </summary>
sealed class NotThrowFailure(bool extensionForm = false, [CallerMemberName] string name = "") : Failure(name, negated: true)
{
    /// <summary>
    /// The exception that was thrown, when none was allowed.
    /// </summary>
    /// <param name="thrownType">The type of the exception thrown.</param>
    /// <param name="thrownMessage">Its message.</param>
    /// <param name="isTask">Whether the subject is a task, as opposed to a delegate.</param>
    public FailureMessage With(Type thrownType, string thrownMessage, bool isTask) =>
        new(this, sentence => Word(sentence, thrownType, thrownException: null, thrownMessage, isTask, extensionForm));

    /// <summary>
    /// The exception that was thrown, when one type in particular was not allowed.
    /// </summary>
    /// <param name="disallowed">The exception type that should not have been thrown.</param>
    /// <param name="thrown">The exception thrown.</param>
    /// <param name="isTask">Whether the subject is a task, as opposed to a delegate.</param>
    public FailureMessage WithDisallowed(Type disallowed, Exception thrown, bool isTask) =>
        new(this, sentence => Word(sentence, disallowed, thrown, exceptionMessage: null, isTask, extensionForm));

    internal static string Word(Sentence sentence, object? expectedValue, Exception? thrownException, string? exceptionMessage, bool isTask, bool extensionForm)
    {
        var isExtensionMethod = extensionForm;
        var codePart = sentence.Subject;

        string subject;
        if (codePart == "null" || !sentence.SubjectFromSource)
        {
            subject = isTask ? "Task" : "delegate";
        }
        else if (isExtensionMethod)
        {
            subject = isTask ? $"Task `{codePart}`" : $"`{codePart}()`";
        }
        else
        {
            subject = $"`{codePart}`";
        }

        // NotThrow<TException> carries the disallowed type in Expected and the thrown
        // exception in Actual; the non-generic NotThrow stores the thrown type in Expected.
        if (expectedValue is Type expectedType && thrownException is { } thrown)
        {
            var thrownType = thrown.GetType();
            if (thrownType == expectedType)
            {
                return
                    $"""
                     {subject}
                         should not throw
                     {expectedType.ToStringAwesomely()}
                         but did, with message
                     "{thrown.Message}"
                     """;
            }

            return
                $"""
                 {subject}
                     should not throw
                 {expectedType.ToStringAwesomely()}
                     but threw
                 {thrownType.ToStringAwesomely()}
                     with message
                 "{thrown.Message}"
                 """;
        }

        return
            $"""
             {subject}
                 should not throw but threw
             {expectedValue.ToStringAwesomely()}
                 with message
             "{exceptionMessage}"
             """;
    }
}
