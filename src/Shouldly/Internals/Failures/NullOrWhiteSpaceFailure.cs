namespace Shouldly.Internals.Failures;

/// <summary>
/// A string should have been null or white space, or should not have been.
/// </summary>
sealed class NullOrWhiteSpaceFailure(bool negated = false, [CallerMemberName] string name = "") : FailureWithoutFacts(name, negated)
{
    // A custom assertion may pass the value under test in either slot.
    internal override SlotReading? FromSlots(Slots s) =>
        new(new(this, Word), s.HasActual ? s.Actual : s.Expected);


    protected override string Word(Sentence sentence)
    {
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

        var codePart = sentence.Subject;

        var isNegatedAssertion = sentence.Negated;
        var should = sentence.Phrase;
        var actual = sentence.ActualText;
        if (isNegatedAssertion)
        {
            return string.Format(format, codePart, should, actual);
        }

        return string.Format(format, codePart, should, actual);
    }
}
