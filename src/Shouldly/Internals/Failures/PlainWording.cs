using Shouldly.DifferenceHighlighting;

namespace Shouldly.Internals.Failures;

/// <summary>
/// The lines a plain failure message has after its subject and phrase. Values are rendered as they are stated, so
/// the lines hold text only: wording them later never reads a value that may since have been disposed or changed.
/// </summary>
readonly struct PlainLines
{
    private PlainLines(bool hasExpected, string? expectedText, bool expectedIsValue, bool hasActual, string? actualText, string? actualInWords, string? difference)
    {
        HasExpected = hasExpected;
        ExpectedText = expectedText;
        ExpectedIsValue = expectedIsValue;
        HasActual = hasActual;
        ActualText = actualText;
        ActualInWords = actualInWords;
        Difference = difference;
    }

    /// <summary>Whether there is an expected line.</summary>
    public bool HasExpected { get; }

    /// <summary>What was expected: a formatted value, or a description in words shown as written.</summary>
    public string? ExpectedText { get; }

    /// <summary>Whether <see cref="ExpectedText"/> is a formatted value, as opposed to a description in words.</summary>
    public bool ExpectedIsValue { get; }

    /// <summary>Whether there is a line for the actual value.</summary>
    public bool HasActual { get; }

    /// <summary>The actual value, formatted.</summary>
    public string? ActualText { get; }

    /// <summary>What was found, described in words and shown as written, in place of the actual value.</summary>
    public string? ActualInWords { get; }

    /// <summary>The highlighted differences between the actual and expected values, when they have any to show.</summary>
    public string? Difference { get; }

    /// <summary>Only the actual value: the lines a plain failure starts with.</summary>
    public static PlainLines For(object? actual) =>
        new(false, null, false, true, actual.ToStringAwesomely(), null, null);

    /// <summary>Adds the expected value, and the differences between it and the actual value.</summary>
    /// <param name="expected">The expected value.</param>
    /// <param name="actual">The actual value, to highlight differences against.</param>
    /// <param name="comparison">How the assertion compared them, so that the highlighting agrees with it.</param>
    /// <param name="highlightDifferences">Whether to look for differences to highlight at all.</param>
    public PlainLines WithExpected(object? expected, object? actual, ComparisonRecord? comparison = null, bool highlightDifferences = true) =>
        new(true, expected.ToStringAwesomely(), true, HasActual, ActualText, ActualInWords,
            highlightDifferences ? DifferenceHighlighter.Between(actual, expected, comparison) : null);

    public PlainLines WithExpectedInWords(string description) =>
        new(true, description, false, HasActual, ActualText, ActualInWords, null);

    public PlainLines WithActualInWords(string description) =>
        new(HasExpected, ExpectedText, ExpectedIsValue, true, ActualText, description, Difference);

    public PlainLines WithoutActual() =>
        new(HasExpected, ExpectedText, ExpectedIsValue, false, ActualText, null, Difference);
}

/// <summary>
/// The plain shape of a failure message: the subject, the phrase, what was expected, and what the actual value
/// was. Built-in assertions with no wording of their own use it, and so do custom assertions.
/// </summary>
static class PlainWording
{
    public static string Word(Sentence sentence, PlainLines lines)
    {
        var message =
            $"""
             {sentence.Subject}
                 {sentence.Phrase}
             """;

        if (lines.HasExpected)
        {
            message += $"""

                        {lines.ExpectedText}
                        """;
        }

        if (!lines.HasActual)
            return message;

        string actualString;
        if (lines.ActualInWords is { } found)
        {
            actualString = $"""

                 {found}
                 """;
        }
        else
        {
            // When the value stood in for the subject, repeating it would add nothing.
            actualString = sentence.SubjectShows(lines.ActualText) ? sentence.Negated ? string.Empty : " not" : $"""

                 {lines.ActualText}
                 """;
        }

        message += $"""

                        but was{actualString}
                    """;

        // A negated assertion fails because actual matched expected, so there's no difference to highlight. Nor is
        // there one between values when either side is described in words.
        if (!sentence.Negated && lines is { ExpectedIsValue: true, ActualInWords: null, Difference: { } difference })
        {
            message += $"""
                        
                            difference
                        {difference}
                        """;
        }

        return message;
    }
}
