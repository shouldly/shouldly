using Shouldly.Internals;

namespace Shouldly.MessageGenerators;

class ShouldBeIgnoringOrderMessageGenerator : ShouldlyMessageGenerator
{
    public override bool CanProcess(IShouldlyAssertionContext context) =>
        context.IgnoreOrder && IsWalkable(context.Actual) && IsWalkable(context.Expected);

    // A default ImmutableArray<T> is IEnumerable but throws when enumerated; it is reported as null (#1406).
    private static bool IsWalkable(object? value) =>
        value is IEnumerable && !NullEquivalence.IsNull(value);

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        if (context.Expected is not IEnumerable expectedEnumerable)
        {
            throw new("`context.Expected` should be IEnumerable");
        }

        if (context.Actual is not IEnumerable actualEnumerable)
        {
            throw new("`context.Actual` should be IEnumerable");
        }

        // Read only as far as a message can show, so an infinite sequence can't hang it. A side
        // that was cut short may still hold matches for the other side's items further on.
        var expected = expectedEnumerable.ReadForMessage(out var expectedTruncated);
        var actual = actualEnumerable.ReadForMessage(out var actualTruncated);
        var codePart = context.CodePart;
        var expectedFormattedValue = expected.ToStringAwesomely();

        // Compare as the assertion did, so any custom comparer is honored.
        var comparison = AssertionComparison.Of(context);

        // Match occurrences one-for-one, as Is.EqualIgnoreOrder does, so a difference in how many times an item
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
            : context.CaseSensitivity switch
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
                        {context.ShouldMethod.PascalToSpaced()} (ignoring order{caseSensitivity})
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
        return string.Format(format, codePart, context.ShouldMethod.PascalToSpaced(), expectedFormattedValue, missingMessage, caseSensitivity);
    }
}