namespace Shouldly.Internals.Failures;

/// <summary>
/// How a sequence of strings failing to equal another is worded.
/// </summary>
static class StringSequenceWording
{
    internal static string Word(Sentence sentence, object? expectedValue, Equality? equality, Case? caseOption)
    {
        var subject = sentence.Subject;
        // A custom comparer decides case sensitivity itself, so there's nothing accurate to say about it.
        var caseSensitivity = equality is { UsesCustomComparer: true }
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
        var message =
            $"""
             {subject}
                 {sentence.Phrase}
             {expected}
                 but was{caseSensitivity}{actual}
             """;

        return message + ShouldBeFailure.DifferenceAndReason(sentence.Actual, expectedValue, equality);
    }
}
