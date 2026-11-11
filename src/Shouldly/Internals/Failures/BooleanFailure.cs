namespace Shouldly.Internals.Failures;

/// <summary>
/// A boolean should have been true, or should have been false.
/// </summary>
sealed class BooleanFailure(bool expected, [CallerMemberName] string name = "") : FailureWithoutFacts(name, negated: false)
{
    // A custom assertion states its own expected value.
    internal override SlotReading? FromSlots(Slots s) =>
        new(new(this, sentence => Word(sentence, s.Expected)), s.Actual);

    protected override string Word(Sentence sentence) => Word(sentence, expected);

    internal static string Word(Sentence sentence, object? expectedValue)
    {
        var subject = sentence.Subject;
        var expected = expectedValue.ToStringAwesomely();

        var actual = sentence.ActualText;
        var actualString = subject == actual ? " not" : $"""

                                                          {actual}
                                                          """;

        return $"""
                {subject}
                    should be
                {expected}
                    but was{actualString}
                """;
    }
}
