using Shouldly.DifferenceHighlighting;

namespace Shouldly.Internals.Failures;

/// <summary>
/// Which lines a plain failure message has after its subject and phrase, and what they show.
/// </summary>
readonly struct PlainLines
{
    private PlainLines(bool hasExpected, object? expected, string? expectedInWords, bool hasActual, string? actualInWords)
    {
        HasExpected = hasExpected;
        Expected = expected;
        ExpectedInWords = expectedInWords;
        HasActual = hasActual;
        ActualInWords = actualInWords;
    }

    /// <summary>Whether there is an expected line.</summary>
    public bool HasExpected { get; }

    /// <summary>The expected value, shown formatted unless <see cref="ExpectedInWords"/> describes it.</summary>
    public object? Expected { get; }

    /// <summary>What was expected, described in words and shown as written.</summary>
    public string? ExpectedInWords { get; }

    /// <summary>Whether there is a line for the actual value.</summary>
    public bool HasActual { get; }

    /// <summary>What was found, described in words and shown as written, in place of the actual value.</summary>
    public string? ActualInWords { get; }

    /// <summary>Only the actual value: the lines a plain failure starts with.</summary>
    public static PlainLines ActualOnly { get; } = new(false, null, null, true, null);

    /// <summary>An expected value and the actual value.</summary>
    public static PlainLines ExpectedAndActual(object? expected) => new(true, expected, null, true, null);

    public PlainLines WithExpected(object? expected) => new(true, expected, null, HasActual, ActualInWords);

    public PlainLines WithExpectedInWords(string description) => new(true, null, description, HasActual, ActualInWords);

    public PlainLines WithActualInWords(string description) => new(HasExpected, Expected, ExpectedInWords, true, description);

    public PlainLines WithoutActual() => new(HasExpected, Expected, ExpectedInWords, false, null);
}

/// <summary>
/// The plain shape of a failure message: the subject, the phrase, what was expected, and what the actual value
/// was. Built-in assertions with no wording of their own use it, and so do custom assertions.
/// </summary>
static class PlainWording
{
    public static string Word(Sentence sentence, PlainLines lines, ComparisonRecord? comparison = null)
    {
        var message =
            $"""
             {sentence.Subject}
                 {sentence.Phrase}
             """;

        if (lines.HasExpected)
        {
            message += $"""

                        {lines.ExpectedInWords ?? lines.Expected.ToStringAwesomely()}
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
            var actual = sentence.ActualText;
            actualString = sentence.SubjectShows(actual) ? sentence.Negated ? string.Empty : " not" : $"""

                 {actual}
                 """;
        }

        message += $"""

                        but was{actualString}
                    """;

        // A negated assertion fails because actual matched expected, so there's no difference to highlight. Nor is
        // there one between values when either side is described in words.
        if (!sentence.Negated &&
            lines is { HasExpected: true, ExpectedInWords: null, ActualInWords: null } &&
            DifferenceHighlighter.Between(sentence.Actual, lines.Expected, comparison) is { } difference)
        {
            message += $"""
                        
                            difference
                        {difference}
                        """;
        }

        return message;
    }
}
