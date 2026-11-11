namespace Shouldly.Internals.Failures;

/// <summary>
/// A string should have started or ended with another, or should not have.
/// </summary>
sealed class StringStartOrEndFailure(bool negated = false, [CallerMemberName] string name = "") : Failure(name, negated)
{
    protected override SlotReading? FromSlots(Slots s) =>
        new(With(s.Expected), s.Actual);

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
