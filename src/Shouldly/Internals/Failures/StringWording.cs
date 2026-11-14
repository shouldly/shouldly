using Shouldly.DifferenceHighlighting;

namespace Shouldly.Internals.Failures;

/// <summary>
/// How a string failing to equal another under string comparison options is worded.
/// </summary>
static class StringWording
{
    internal static string Word(Sentence sentence, ComparedStrings strings)
    {
        var actualValue = Echo(strings.Actual);
        var expectedValue = Echo(strings.Expected);

        // With no source text to show, the rendered value stands in for it. Using the same echo means the
        // "but was" line below collapses to " not" instead of repeating it.
        var subject = sentence.SubjectFromSource ? sentence.Subject : actualValue;
        var withOption = string.IsNullOrEmpty(strings.Options) ? null : $" with options: {strings.Options}";

        // Differences come from the full values. Truncating first would let a difference that
        // sits past the echo budget disappear from the message entirely.
        var differences = new StringDifferenceHighlighter(strings.Sensitivity).HighlightDifferences(strings.Expected, strings.Actual);

        var actual = subject == actualValue ?
            " not" :
            $"""

             {actualValue}
             """;
        var message =
            $"""
             {subject}
                 {sentence.Phrase}{withOption}
             {expectedValue}
                 but was{actual}
             """;

        if (strings.DifferOnlyInCase)
        {
            message +=
                """

                    which differs only in case
                """;
        }

        if (differences != null)
        {
            message +=
                $"""
                 
                     difference
                 {differences}
                 """;
        }

        return message;
    }

    // Renders a value for the "should be X but was Y" lines, clipped to
    // ShouldlyConfiguration.MaxStringLengthInMessages. Truncation is always announced —
    // silently swallowing the tail leaves no clue that the printed value is partial.
    private static string? Echo(string? value)
    {
        var limit = ShouldlyConfiguration.MaxStringLengthInMessages;

        if (value == null || value.Length <= limit)
        {
            return value.ToStringAwesomely();
        }

        return $"{value[..limit].ToStringAwesomely()} " +
               $"(truncated to {limit} of {value.Length} characters, see " +
               $"{nameof(ShouldlyConfiguration)}.{nameof(ShouldlyConfiguration.MaxStringLengthInMessages)})";
    }
}
