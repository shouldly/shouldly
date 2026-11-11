namespace Shouldly.Internals.Failures;

/// <summary>
/// A delegate or task should have thrown an exception of a type.
/// </summary>
sealed class ThrowFailure(bool extensionForm = false, bool exactly = false, [CallerMemberName] string name = "") : Failure(name, negated: false)
{
    internal override SlotReading? FromSlots(Slots s) =>
        s.Shape == MessageShape.Throw
            ? new(new(this, sentence => Word(sentence, s.Expected, s.HasActual ? s.Actual : null, s.ExceptionMessage, isTask: false, extensionForm, exactly)), s.Actual)
            : null;

    /// <summary>
    /// The exception type required and the type thrown instead, or null when nothing was thrown.
    /// </summary>
    /// <param name="expected">The exception type that should have been thrown.</param>
    /// <param name="thrown">The type thrown instead, or null when nothing was thrown.</param>
    /// <param name="isTask">Whether the subject is a task, as opposed to a delegate.</param>
    public FailureMessage With(Type expected, Type? thrown, bool isTask) =>
        new(this, sentence => Word(sentence, expected, thrown, exceptionMessage: null, isTask, extensionForm, exactly));

    internal static string Word(Sentence sentence, object? expectedValue, object? thrown, string? exceptionMessage, bool isTask, bool extensionForm, bool exactly)
    {
        var isExtensionMethod = extensionForm;
        var maybeInvokeMethod = isExtensionMethod && !isTask ? "()" : string.Empty;
        string subject;
        if (sentence.Subject == "null" || !sentence.SubjectFromSource)
        {
            subject = isTask ? "Task" : "delegate";
        }
        else
        {
            subject = $"`{sentence.Subject}{maybeInvokeMethod}`";
            if (isTask)
                subject = "Task " + subject;
        }

        var expected = expectedValue.ToStringAwesomely();
        var shouldThrow = exactly ? "should throw exactly" : "should throw";

        string errorMessage;
        if (thrown != null)
        {
            errorMessage = string.Format($"""
                                          {subject.Replace("{", "{{").Replace("}", "}}")}
                                              {shouldThrow}
                                          {expected}
                                              but threw
                                          {thrown}
                                          """, subject, expected, thrown, maybeInvokeMethod);
        }
        else
        {
            errorMessage = $"""
                            {subject}
                                {shouldThrow}
                            {expected}
                                but did not
                            """;
        }

        errorMessage += exceptionMessage != null ?
            $"""
              with message
             "{exceptionMessage}"
             """
            : string.Empty;

        return errorMessage;
    }
}
