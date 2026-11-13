using Shouldly.DifferenceHighlighting;

namespace Shouldly.Internals.Failures;

/// <summary>
/// How a value failing to be, or not to be, within a tolerance of another is worded.
/// </summary>
static class WithinWording
{
    /// <param name="sentence">The sentence the message is written for.</param>
    /// <param name="expectedValue">The value the actual value should have been within the tolerance of.</param>
    /// <param name="toleranceValue">The tolerance.</param>
    /// <param name="comparison">
    /// How the assertion compared the elements of two sequences. Left out, it is taken to be how the built-in
    /// assertions compare with a tolerance of that type.
    /// </param>
    internal static string Word(Sentence sentence, object? expectedValue, object? toleranceValue, ComparisonRecord? comparison = null)
    {
        comparison ??= ImpliedBy(toleranceValue);

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

        if (DifferenceHighlighter.Between(sentence.Actual, expectedValue, comparison) is { } difference)
        {
            message += $"""
                        
                            difference
                        {difference}
                        """;
        }
        else if (!sentence.Negated && comparison is not null)
        {
            // Two sets of numbers aren't compared in order, so they are told apart by what one has that the other doesn't.
            message += UnorderedWording.Difference(sentence.Actual, expectedValue, comparison);
        }

        return message;
    }

    // A custom assertion states its tolerance but not how it compared with it, so it is taken to have compared as the
    // built-in assertions on sequences of numbers do with a tolerance of that type.
    private static ComparisonRecord? ImpliedBy(object? tolerance) =>
        tolerance switch
        {
            decimal decimalTolerance => ComparisonRecord.ComparedElementsWith(Equality.WithinTolerance<object?>(decimalTolerance), isCustom: false),
            double doubleTolerance => ComparisonRecord.ComparedElementsWith(Equality.WithinTolerance<object?>(doubleTolerance), isCustom: false),
            _ => null
        };
}
