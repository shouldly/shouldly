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
    /// <param name="equality">
    /// How the assertion compared the two, or null when that isn't known: a custom assertion that states a tolerance
    /// of a type no built-in assertion on sequences takes.
    /// </param>
    internal static string Word(Sentence sentence, object? expectedValue, object? toleranceValue, Equality? equality)
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

        if (DifferenceHighlighter.Between(sentence.Actual, expectedValue, equality) is { } difference)
        {
            message += $"""
                        
                            difference
                        {difference}
                        """;
        }
        else if (!sentence.Negated && equality is not null)
        {
            // Two sets of numbers aren't compared in order, so they are told apart by what one has that the other doesn't.
            message += UnorderedWording.Difference(sentence.Actual, expectedValue, equality);
        }

        return message;
    }

    /// <summary>
    /// How a custom assertion that states a tolerance, but not how it compared with it, is taken to have compared: as
    /// the built-in assertions on sequences of numbers do with a tolerance of that type. Null for a tolerance of any
    /// other type.
    /// </summary>
    public static Equality? ImpliedBy(object? tolerance) =>
        tolerance switch
        {
            decimal decimalTolerance => Equality.OfSequences(Equality.WithinTolerance<object?>(decimalTolerance)),
            double doubleTolerance => Equality.OfSequences(Equality.WithinTolerance<object?>(doubleTolerance)),
            _ => null
        };
}
