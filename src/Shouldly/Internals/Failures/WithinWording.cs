using Shouldly.DifferenceHighlighting;

namespace Shouldly.Internals.Failures;

/// <summary>
/// How a value failing to be, or not to be, within a tolerance of another is worded.
/// </summary>
static class WithinWording
{
    internal static string Word(Sentence sentence, object? expectedValue, object? toleranceValue)
    {
        var subject = sentence.Subject;
        var tolerance = toleranceValue.ToStringAwesomely();
        var expected = expectedValue.ToStringAwesomely();
        var actualValue = sentence.ActualText;
        string actual;
        if (subject == actualValue)
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
             {subject}
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
        else if (!sentence.Negated && WithinTolerance(toleranceValue) is { } withinTolerance)
        {
            // Two sets of numbers aren't compared in order, so they are told apart by what one has that the other doesn't.
            message += UnorderedWording.Difference(sentence.Actual, expectedValue, ComparisonRecord.ComparedElementsBy(withinTolerance));
        }

        return message;
    }

    // Whether two numbers are within the tolerance of each other, as the assertions on sequences of numbers compare them.
    private static Func<object?, object?, bool>? WithinTolerance(object? tolerance) =>
        tolerance switch
        {
            decimal decimalTolerance => (actual, expected) =>
                actual is decimal actualNumber && expected is decimal expectedNumber &&
                Is.Equal(actualNumber, expectedNumber, decimalTolerance),
            double doubleTolerance => (actual, expected) =>
                actual is double or float && expected is double or float &&
                Is.Equal(Convert.ToDouble(actual), Convert.ToDouble(expected), doubleTolerance),
            _ => null
        };
}
