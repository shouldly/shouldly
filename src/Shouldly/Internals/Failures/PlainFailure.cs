using Shouldly.DifferenceHighlighting;

namespace Shouldly.Internals.Failures;

/// <summary>
/// The plain shape of a failure message: the subject, the phrase, the expected value, and what the actual value
/// was. Assertions with no wording of their own use it.
/// </summary>
sealed class PlainFailure(bool negated = false, [CallerMemberName] string name = "") : Failure(name, negated)
{
    public FailureMessage With(object? expected) =>
        new(this, sentence => Word(sentence, expected));

    internal static string Word(Sentence sentence, object? expected, ComparisonRecord? comparison = null)
    {
        var actual = sentence.ActualText;
        var actualString = sentence.SubjectShows(actual) ? sentence.Negated ? string.Empty : " not" : $"""

             {actual}
             """;

        var message =
            $"""
             {sentence.Subject}
                 {sentence.Phrase}
             {expected.ToStringAwesomely()}
                 but was{actualString}
             """;

        // A negated assertion fails because actual matched expected, so there's no difference to highlight.
        if (!sentence.Negated && DifferenceHighlighter.Between(sentence.Actual, expected, comparison) is { } difference)
        {
            message += $"""
                        
                            difference
                        {difference}
                        """;
        }

        return message;
    }
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
