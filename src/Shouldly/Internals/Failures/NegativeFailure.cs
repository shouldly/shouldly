namespace Shouldly.Internals.Failures;

/// <summary>
/// A number should have been negative.
/// </summary>
sealed class NegativeFailure([CallerMemberName] string name = "") : FailureWithoutFacts(name, negated: false)
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

        var should = sentence.Phrase;
        return
            $"""
             {subject}
                 {should} but{actualValue}is positive
             """;
    }
}
