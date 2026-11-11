namespace Shouldly.Internals.Failures;

/// <summary>
/// A string should have ended with another, or should not have.
/// </summary>
sealed class StringEndFailure(bool negated = false, [CallerMemberName] string name = "") : Failure(name, negated)
{
    public FailureMessage With(object? expected) =>
        new(this, sentence => Word(sentence, expected));

    internal static string Word(Sentence sentence, object? expected)
    {
        var actualValue = sentence.ActualText;
        string actual;
        if (sentence.SubjectShows(actualValue))
        {
            actual = sentence.Negated ? " did" : " did not";
        }
        else
        {
            actual = $"""
                       was
                      {actualValue}
                      """;
        }

        return $"""
                {sentence.Subject}
                    {sentence.Phrase}
                {expected.ToStringAwesomely()}
                    but{actual}
                """;
    }
}
