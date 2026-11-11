namespace Shouldly.Internals.Failures;

/// <summary>
/// The plain shape of a failure message: the subject, the phrase, the expected value, and what the actual value
/// was. Assertions with no wording of their own use it.
/// </summary>
sealed class PlainFailure(bool negated = false, [CallerMemberName] string name = "") : Failure(name, negated)
{
    public FailureMessage With(object? expected) =>
        new(this, sentence => Word(sentence, expected));

    internal static string Word(Sentence sentence, object? expected, ComparisonRecord? comparison = null) =>
        PlainWording.Word(sentence, PlainLines.For(sentence.Actual).WithExpected(expected, sentence.Actual, comparison, highlightDifferences: !sentence.Negated));
}

/// <summary>
/// The plain shape of a failure message for an assertion with nothing to show for the actual value: the subject,
/// the phrase and the expected value.
/// </summary>
sealed class ExpectedOnlyFailure(bool negated = false, [CallerMemberName] string name = "") : Failure(name, negated)
{
    public FailureMessage With(object? expected) =>
        new(this, sentence => Word(sentence, expected));

    internal static string Word(Sentence sentence, object? expected)
    {
        var conditionString = expected is BinaryExpression
            ? "an element satisfying the condition"
            : "";
        return
            $"""
             {sentence.Subject}
                 {sentence.Phrase} {conditionString}
             {expected.ToStringAwesomely()}
                 but does{(sentence.Negated ? "" : " not")}
             """;
    }
}
