namespace Shouldly.Internals.Failures;

/// <summary>
/// A collection should have had exactly one item.
/// </summary>
sealed class SingleItemFailure([CallerMemberName] string name = "") : FailureWithoutFacts(name, negated: false)
{
    // A custom assertion may pass the value under test in either slot.
    internal override SlotReading? FromSlots(Slots s) =>
        new(new(this, Word), s.HasActual ? s.Actual : s.Expected);


    protected override string Word(Sentence sentence)
    {
        var codePart = sentence.Subject;
        var expected = sentence.Actual;
        var should = sentence.Phrase;

        if (expected is null)
        {
            if (codePart != "null")
            {
                return
                    $"""
                     {codePart}
                         {should} but was
                     null
                     """;
            }

            return
                $"""
                 null
                     {should}
                 """;
        }

        var expectedString = expected.ToStringAwesomely();
        var count = (expected as IEnumerable)?.Cast<object>().Count() ?? 0;

        if (codePart != "null" && sentence.SubjectFromSource)
        {
            return
                $"""
                 {codePart}
                     {should} but had
                 {count}
                     items and was
                 {expectedString}
                 """;
        }

        return
            $"""
             {expectedString}
                 {should} but had
             {count}
                 items
             """;
    }
}
