namespace Shouldly.Internals.Failures;

/// <summary>
/// A number should have been negative.
/// </summary>
sealed class NegativeFailure([CallerMemberName] string name = "") : FailureWithoutFacts(name, negated: false)
{

    protected override string Word(Sentence sentence)
    {
        var codePart = sentence.Subject;
        var actual = sentence.ActualText;
        var actualValue = codePart != actual ? $"""

                                                {actual}
                                                    
                                                """ : " ";

        var should = sentence.Phrase;
        return
            $"""
             {codePart}
                 {should} but{actualValue}is positive
             """;
    }
}
