namespace Shouldly.Internals.Failures;

/// <summary>
/// A collection should have had a number of items.
/// </summary>
sealed class CountFailure([CallerMemberName] string name = "") : Failure(name, negated: false)
{
    protected override SlotReading? FromSlots(Slots s) =>
        new(new(this, sentence => Word(sentence, s.Expected, count: null)), s.Actual);

    /// <summary>
    /// The number of items there should have been, and the number the assertion found.
    /// </summary>
    public FailureMessage With(int expected, int count) =>
        new(this, sentence => Word(sentence, expected, count));

    /// <summary>
    /// The number of items there should have been, for an actual value that wasn't counted, such as null.
    /// </summary>
    public FailureMessage With(int expected) =>
        new(this, sentence => Word(sentence, expected, count: null));

    internal static string Word(Sentence sentence, object? expectedValue, int? count)
    {
        var subject = sentence.Subject;
        var expected = expectedValue.ToStringAwesomely();
        count ??= sentence.Actual switch
        {
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
