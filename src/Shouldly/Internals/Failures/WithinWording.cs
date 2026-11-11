using Shouldly.DifferenceHighlighting;

namespace Shouldly.Internals.Failures;

/// <summary>
/// How a value failing to be, or not to be, within a tolerance of another is worded.
/// </summary>
static class WithinWording
{

    internal static string Word(Sentence sentence, object? expectedValue, object? toleranceValue)
    {
        var codePart = sentence.Subject;
        var tolerance = toleranceValue.ToStringAwesomely();
        var expected = expectedValue.ToStringAwesomely();
        var actualValue = sentence.ActualText;
        string actual;
        if (codePart == actualValue)
        {
            actual = " not";
        }
        else
        {
            actual = $"""

                      {actualValue}
                      """;
        }

        var negated = sentence.Negated ? "not " : string.Empty;

        var message =
            $"""
             {codePart}
                 should {negated}be within
             {tolerance}
                 of
             {expected}
                 but was{actual}
             """;

        if (DifferenceHighlighter.Between(sentence.Actual, expectedValue) is { } difference)
        {
            message += $"""
                        
                            difference
                        {difference}
                        """;
        }

        return message;
    }
}
