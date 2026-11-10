namespace Shouldly.MessageGenerators;

class ShouldBeIgnoringOrderMessageGenerator : ShouldlyMessageGenerator
{
    public override bool CanProcess(IShouldlyAssertionContext context) =>
        context.IgnoreOrder && context.Actual is IEnumerable && context.Expected is IEnumerable;

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

        var expected = expectedEnumerable.Cast<object>().ToArray();
        var actual = actualEnumerable.Cast<object>().ToArray();
        var codePart = context.CodePart;
        var expectedFormattedValue = expected.ToStringAwesomely();

        // ShouldBe passes the element equality it compared with, so any custom comparer is honored. Fall back to
        // default equality for other callers.
        var equal = context is ShouldlyAssertionContext { ElementEquals: { } elementEquals }
            ? elementEquals
            : (a, e) => Is.Equal(a, e);

        // Match occurrences one-for-one, as Is.EqualIgnoreOrder does, so a difference in how many times an item
        // occurs is reported rather than hidden by an earlier match.
        var missingFromActual = expected.ToList();
        var missingFromExpected = new List<object>();
        foreach (var actualItem in actual)
        {
            var index = missingFromActual.FindIndex(expectedItem => equal(actualItem, expectedItem));
            if (index < 0)
                missingFromExpected.Add(actualItem);
            else
                missingFromActual.RemoveAt(index);
        }

        var actualMissingMessage = missingFromActual.Any()
            ? $"""
               {codePart}
                   is missing
               {missingFromActual.ToStringAwesomely()}
               """
            : string.Empty;
        var expectedMissingMessage = missingFromExpected.Any()
            ? $"""
               {expected.ToStringAwesomely()}
                   is missing
               {missingFromExpected.ToStringAwesomely()}
               """
            : string.Empty;

        // "first should be second (ignoring order) but first is missing [4] and second is missing [2]"

        const string format =
            """
            {0}
                {1} (ignoring order)
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
        return string.Format(format, codePart, context.ShouldMethod.PascalToSpaced(), expectedFormattedValue, missingMessage);
    }
}