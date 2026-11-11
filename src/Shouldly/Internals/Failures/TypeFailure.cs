namespace Shouldly.Internals.Failures;

/// <summary>
/// A value should have been of, or assignable to, a type, or should not have been.
/// </summary>
sealed class TypeFailure(bool negated = false, [CallerMemberName] string name = "") : Failure(name, negated)
{
    protected override SlotReading? FromSlots(Slots s) =>
        new(new(this, sentence => Word(sentence, s.Expected)), s.Actual);

    public FailureMessage With(Type expected) =>
        new(this, sentence => Word(sentence, expected));

    internal static string Word(Sentence sentence, object? expected)
    {
        var actualType = sentence.Actual?.GetType().FullName;

        var actualString = sentence.SubjectShows(actualType) || sentence.Subject == "null" ? " not" : $"""

             {actualType ?? "null"}
             """;

        return
            $"""
             {sentence.Subject}
                 {sentence.Phrase}
             {expected.ToStringAwesomely()}
                 but was{actualString}
             """;
    }
}
