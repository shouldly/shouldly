namespace Shouldly.Internals.Failures;

/// <summary>
/// How a sequence failing to match another in any order is worded.
/// </summary>
static class IgnoringOrderWording
{
    internal static string Word(Sentence sentence, object? expectedValue, Equality equality)
    {
        var unpaired = equality.DifferencesBetween(sentence.Actual, expectedValue).Unpaired
                       ?? throw new("`sentence.Actual` and `expectedValue` should be IEnumerable");
        var subject = sentence.Subject;
        var expectedFormattedValue = unpaired.Expected.ToStringAwesomely();

        // An item is only known to be missing from a side that was read to the end; the unread
        // rest of the side it came from may hold more such items.
        var actualMissingMessage = !unpaired.ActualCutShort && unpaired.OnlyInExpected.Any()
            ? $"""
               {subject}
                   is missing
               {unpaired.OnlyInExpected.ToStringAwesomely(moreMayFollow: unpaired.ExpectedCutShort)}
               """
            : string.Empty;
        var expectedMissingMessage = !unpaired.ExpectedCutShort && unpaired.OnlyInActual.Any()
            ? $"""
               {expectedFormattedValue}
                   is missing
               {unpaired.OnlyInActual.ToStringAwesomely(moreMayFollow: unpaired.ActualCutShort)}
               """
            : string.Empty;

        // A custom comparer decides case sensitivity itself, so only report a Case the caller passed.
        var caseSensitivity = equality.UsesCustomComparer
            ? string.Empty
            : equality.Case switch
            {
                Case.Insensitive => ", case insensitive comparison",
                Case.Sensitive => ", case sensitive comparison",
                _ => string.Empty
            };

        if (actualMissingMessage.Length == 0 && expectedMissingMessage.Length == 0)
        {
            // Both sides were too long to tell what's missing, so show them as far as they were read.
            return $"""
                    {subject}
                        {sentence.Phrase} (ignoring order{caseSensitivity})
                    {expectedFormattedValue}
                        but was
                    {unpaired.Actual.ToStringAwesomely()}
                    """;
        }

        // "first should be second (ignoring order) but first is missing [4] and second is missing [2]"

        const string format =
            """
            {0}
                {1} (ignoring order{4})
            {2}
                but
            {3}
            """;

        var hasBothActualAndExpectedMissingItems = !string.IsNullOrEmpty(actualMissingMessage) && !string.IsNullOrEmpty(expectedMissingMessage);
        var missingMessage = hasBothActualAndExpectedMissingItems
            ? $"""
               {actualMissingMessage}
                   and
               {expectedMissingMessage}
               """
            : $"{actualMissingMessage}{expectedMissingMessage}";
        return string.Format(format, subject, sentence.Phrase, expectedFormattedValue, missingMessage, caseSensitivity);
    }
}
