namespace Shouldly.Internals.Failures;

/// <summary>
/// A number should have been positive.
/// </summary>
sealed class PositiveFailure([CallerMemberName] string name = "") : FailureWithoutFacts(name, negated: false)
{

    protected override string Word(Sentence sentence)
    {
        var codePart = sentence.Subject;
        var actual = sentence.ActualText;
        var actualValue = codePart != actual ? $"""

                                                {actual}
                                                    
                                                """ : " ";

        return
            $"""
             {codePart}
                 {sentence.Phrase} but{actualValue}is negative
             """;
    }
}
