namespace Shouldly.Internals.Failures;

/// <summary>
/// A string or collection should have been empty, or should not have been.
/// </summary>
sealed class EmptyFailure(bool negated = false, [CallerMemberName] string name = "") : FailureWithoutFacts(name, negated)
{
    // A custom assertion that passes an actual value is comparing it with an empty one.
    internal override SlotReading? FromSlots(Slots s) =>
        s.HasActual ? new(ComparedWith(s.Expected), s.Actual) : new(new(this, Word), s.Expected);

    /// <summary>
    /// A value compared with its type's empty value, which gets the plain shape.
    /// </summary>
    public FailureMessage ComparedWith(object? expected) =>
        new(this, sentence => PlainFailure.Word(sentence, expected));

    protected override string Word(Sentence sentence)
    {
        var subject = sentence.Subject;
        var expected = sentence.ActualText;
        var shouldMethod = sentence.Phrase;

        if (sentence.Negated)
        {
            if (subject == "null" || !sentence.SubjectFromSource)
            {
                subject = expected;
            }

            return
                $"""
                 {subject}
                     {shouldMethod} but was{(sentence.Actual == null ? " null" : "")}
                 """;
        }

        string details;
        if (sentence.Actual is IEnumerable enumerable and not string)
        {
            var (count, noun) = enumerable.DescribeCountForMessage();
            details = $"""
                        had
                       {count}
                           {noun} and
                       """;
        }
        else
        {
            details = string.Empty;
        }

        string expectedString;
        if (subject == "null" || !sentence.SubjectFromSource)
        {
            subject = expected;
            expectedString = " not empty";
        }
        else
        {
            expectedString = $"""

                              {expected}
                              """;
        }

        return
            $"""
             {subject}
                 {shouldMethod} but{details} was{expectedString}
             """;
    }
}
