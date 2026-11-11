namespace Shouldly.Internals.Failures;

/// <summary>
/// A number should have been positive.
/// </summary>
sealed class PositiveFailure([CallerMemberName] string name = "") : FailureWithoutFacts(name, negated: false)
{
    internal override SlotReading? FromSlots(Slots s) =>
        new(new(this, Word), s.Actual);

    protected override string Word(Sentence sentence)
    {
        var subject = sentence.Subject;
        var actual = sentence.ActualText;
        var actualValue = subject != actual ? $"""

                                                {actual}
                                                    
                                                """ : " ";

        return
            $"""
             {subject}
                 {sentence.Phrase} but{actualValue}is negative
             """;
    }
}
