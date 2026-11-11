using Shouldly.DifferenceHighlighting;

namespace Shouldly.Internals.Failures;

/// <summary>
/// How a sequence of strings failing to equal another is worded.
/// </summary>
static class StringSequenceWording
{
    internal static string Word(Sentence sentence, object? expectedValue, ComparisonRecord? record, Case? caseOption)
    {
        var subject = sentence.Subject;
        // A custom comparer decides case sensitivity itself, so there's nothing accurate to say about it.
        var caseSensitivity = (record ?? ComparisonRecord.Default).UsesCustomComparer
            ? string.Empty
            : caseOption == Case.Insensitive
                ? " (case insensitive comparison)"
                : " (case sensitive comparison)";
        var actualValue = sentence.ActualText;
        string actual;
        if (sentence.Negated)
        {
            actual = string.Empty;
        }
        else if (subject == actualValue)
        {
            actual = $" not{caseSensitivity}";
            caseSensitivity = string.Empty;
        }
        else
        {
            actual = $"\r\n{actualValue}";
        }

        var expected = expectedValue.ToStringAwesomely();
        var format =
            $"""
             {subject}
                 {sentence.Phrase}
             {expected}
                 but was{caseSensitivity}{actual}
             """;

        if (DifferenceHighlighter.Between(sentence.Actual, expectedValue, record) is { } difference)
        {
            format +=
                $"""
                 
                     difference
                 {difference}
                 """;
        }

        return format;
    }
}
