namespace Shouldly.Internals.Failures;

/// <summary>
/// The items of a collection should have been unique.
/// </summary>
sealed class UniqueFailure([CallerMemberName] string name = "") : Failure(name, negated: false)
{
    // A custom assertion passes the collection as expected and its duplicates as actual.
    internal override SlotReading? FromSlots(Slots s) =>
        s.HasActual ? new(With(s.Actual), s.Expected) : null;

    /// <summary>
    /// The items that occurred more than once.
    /// </summary>
    public FailureMessage With(object? duplicates) =>
        new(this, sentence => Word(sentence, duplicates));

    internal static string Word(Sentence sentence, object? duplicates)
    {
        var subject = sentence.Subject;
        var actual = duplicates.ToStringAwesomely();

        if (subject == actual)
        {
            subject = sentence.ActualText;
        }

        return
            $"""
             {subject}
                 {sentence.Phrase} but
             {actual}
                 was duplicated
             """;
    }
}
