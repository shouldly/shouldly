namespace Shouldly.Internals.Failures;

/// <summary>
/// How a sequence failing to match another in any order is worded.
/// </summary>
static class IgnoringOrderWording
{

    internal static string Word(Sentence sentence, object? expectedValue, ComparisonRecord? record, Case? caseOption)
    {
        if (expectedValue is not IEnumerable expectedEnumerable)
        {
            throw new("`expectedValue` should be IEnumerable");
        }

        if (sentence.Actual is not IEnumerable actualEnumerable)
        {
            throw new("`sentence.Actual` should be IEnumerable");
        }

        // Read only as far as a message can show, so an infinite sequence can't hang it. A side
        // that was cut short may still hold matches for the other side's items further on.
        var expected = expectedEnumerable.ReadForMessage(out var expectedTruncated);
        var actual = actualEnumerable.ReadForMessage(out var actualTruncated);
        var codePart = sentence.Subject;
        var expectedFormattedValue = expected.ToStringAwesomely();

        // Compare as the assertion did, so any custom comparer is honored.
        var comparison = (record ?? ComparisonRecord.Default);

        // Match occurrences one-for-one, as Equality.SequenceEqual does, so a difference in how many times an item
        // occurs is reported rather than hidden by an earlier match.
        var missingFromActual = expected.ToList();
        var missingFromExpected = new List<object?>();
        foreach (var actualItem in actual)
        {
            var index = missingFromActual.FindIndex(expectedItem => comparison.ElementsEqual(actualItem, expectedItem));
            if (index < 0)
                missingFromExpected.Add(actualItem);
            else
                missingFromActual.RemoveAt(index);
        }

        // An item is only known to be missing from a side that was read to the end; the unread
        // rest of the side it came from may hold more such items.
        var actualMissingMessage = !actualTruncated && missingFromActual.Any()
            ? $"""
               {codePart}
                   is missing
               {missingFromActual.ToStringAwesomely(moreMayFollow: expectedTruncated)}
               """
            : string.Empty;
        var expectedMissingMessage = !expectedTruncated && missingFromExpected.Any()
            ? $"""
               {expectedFormattedValue}
                   is missing
               {missingFromExpected.ToStringAwesomely(moreMayFollow: actualTruncated)}
               """
            : string.Empty;

        // A custom comparer decides case sensitivity itself, so only report a Case the caller passed.
        var caseSensitivity = comparison.UsesCustomComparer
            ? string.Empty
            : caseOption switch
            {
                Case.Insensitive => ", case insensitive comparison",
                Case.Sensitive => ", case sensitive comparison",
                _ => string.Empty
            };

        if (actualMissingMessage.Length == 0 && expectedMissingMessage.Length == 0)
        {
            // Both sides were too long to tell what's missing, so show them as far as they were read.
            return $"""
                    {codePart}
                        {sentence.Phrase} (ignoring order{caseSensitivity})
                    {expectedFormattedValue}
                        but was
                    {actual.ToStringAwesomely()}
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
        return string.Format(format, codePart, sentence.Phrase, expectedFormattedValue, missingMessage, caseSensitivity);
    }
}
