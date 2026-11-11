namespace Shouldly.Internals.Failures;

/// <summary>
/// A value should have been null, or should not have been.
/// </summary>
sealed class NullFailure(bool negated = false, [CallerMemberName] string name = "") : FailureWithoutFacts(name, negated)
{
    // A custom assertion may pass the value under test in either slot.
    internal override SlotReading? FromSlots(Slots s) =>
        new(new(this, Word), s.HasActual ? s.Actual : s.Expected);

    protected override string Word(Sentence sentence)
    {
        var value = sentence.ActualText;
        var subject = sentence.Subject == "null" ? value : sentence.Subject;
        var valuePart = sentence.Negated || subject == value ? string.Empty : $"""

             {value}
             """;

        return $"""
                {subject}
                    {sentence.Phrase} but was{valuePart}
                """;
    }
}
