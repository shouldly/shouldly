namespace Shouldly.Internals.Failures;

/// <summary>
/// An enum value should have had a flag, or should not have had it.
/// </summary>
sealed class FlagFailure(bool negated = false, [CallerMemberName] string name = "") : Failure(name, negated)
{
    internal override SlotReading? FromSlots(Slots s) =>
        new(With(s.Expected), s.Actual);

    public FailureMessage With(object? expectedFlag) =>
        new(this, sentence => Word(sentence, expectedFlag));

    internal static string Word(Sentence sentence, object? expectedFlag)
    {
        var actual = sentence.ActualText;
        var expected = expectedFlag.ToStringAwesomely();

        if (sentence.Negated)
        {
            var hadString = sentence.SubjectShows(actual) ? " had" : $"""
                                                                        it had
                                                                       {actual}
                                                                       """;
            return $"""
                    {sentence.Subject}
                        should not have flag
                    {expected}
                        but{hadString}
                    """;
        }

        var actualString = sentence.SubjectShows(actual) ? " did not" : $"""
                                                                          had
                                                                         {actual}
                                                                         """;
        return $"""
                {sentence.Subject}
                    should have flag
                {expected}
                    but{actualString}
                """;
    }
}
