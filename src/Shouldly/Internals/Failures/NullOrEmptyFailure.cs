namespace Shouldly.Internals.Failures;

/// <summary>
/// A string or collection should have been null or empty, or should not have been.
/// </summary>
sealed class NullOrEmptyFailure(bool negated = false, [CallerMemberName] string name = "") : FailureWithoutFacts(name, negated)
{
    // A custom assertion may pass the value under test in either slot.
    protected override SlotReading? FromSlots(Slots s) =>
        new(new(this, Word), s.HasActual ? s.Actual : s.Expected);

    protected override string Word(Sentence sentence)
    {
        var subject = sentence.Subject;
        var should = sentence.Phrase;
        var actual = sentence.ActualText;

        // A non-empty collection fails ShouldBeNullOrEmpty; report how many items it had, as ShouldBeEmpty does.
        if (!sentence.Negated && sentence.Actual is IEnumerable enumerable and not string)
        {
            var (count, noun) = enumerable.DescribeCountForMessage();
            return
                $"""
                 {subject}
                     {should} but had
                 {count}
                     {noun} and was
                 {actual}
                 """;
        }

        var format = sentence.SubjectShows(sentence.ActualText) ?
            """
            {0}
                {1}
            """
            :
            """
            {0} ({2})
                {1}
            """;

        return string.Format(format, subject, should, actual);
    }
}
