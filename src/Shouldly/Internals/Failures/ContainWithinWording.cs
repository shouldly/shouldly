namespace Shouldly.Internals.Failures;

/// <summary>
/// How a failure to contain a value to within a tolerance is worded.
/// </summary>
static class ContainWithinWording
{
    internal static string Word(Sentence sentence, object? expectedValue, object? tolerance)
    {
        var subject = sentence.Subject;
        var toleranceText = tolerance.ToStringAwesomely();
        var expected = expectedValue.ToStringAwesomely();
        var actualValue = sentence.ActualText;
        var negated = sentence.Negated ? "not " : string.Empty;

        var actualValueString = subject == actualValue ? " not" : $"""

                                                                    {actualValue}
                                                                    """;

        return
            $"""
             {subject}
                 should {negated}contain
             {expected}
                 within
             {toleranceText}
                 but was{actualValueString}
             """;
    }
}
