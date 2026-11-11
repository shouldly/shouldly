namespace Shouldly.Internals.Failures;

/// <summary>
/// A collection should have had a number of items.
/// </summary>
sealed class CountFailure([CallerMemberName] string name = "") : Failure(name, negated: false)
{
    internal override SlotReading? FromSlots(Slots s) =>
        new(new(this, sentence => Word(sentence, s.Expected)), s.Actual);

    public FailureMessage With(int expected) =>
        new(this, sentence => Word(sentence, expected));

    internal static string Word(Sentence sentence, object? expectedValue)
    {
        var subject = sentence.Subject;
        var expected = expectedValue.ToStringAwesomely();
        var count = sentence.Actual switch
        {
            // The assertion always hands us a materialized collection, so read its count cheaply rather than re-enumerating.
            ICollection collection => collection.Count,
            IEnumerable enumerable => enumerable.Cast<object>().Count(),
            _ => 0
        };
        var actual = sentence.ActualText;
        var expectedItems = expected == "1" ? "item" : "items";
        var actualItems = count == 1 ? "item" : "items";

        if (!sentence.SubjectShows(sentence.ActualText))
        {
            return
                $"""
                 {subject}
                     should have {expected} {expectedItems} but had
                 {count}
                     {actualItems} and was
                 {actual}
                 """;
        }

        return
            $"""
             {subject}
                 should have {expected} {expectedItems} but had
             {count}
                 {actualItems}
             """;
    }
}
