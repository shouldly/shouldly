using System.Collections;

namespace Shouldly.MessageGenerators;

class ShouldBeIgnoringOrderMessageGenerator : ShouldlyMessageGenerator
{
    public override bool CanProcess(IShouldlyAssertionContext context)
    {
        return context.IgnoreOrder;
    }

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

        var missingFromExpected = actual.Where(a => !expected.Any(e => Is.Equal(e, a))).ToArray();
        var missingFromActual = expected.Where(e => !actual.Any(a => Is.Equal(e, a))).ToArray();

        var actualMissingMessage = missingFromActual.Any()
            ? $@"{codePart}
    is missing
{missingFromActual.ToStringAwesomely()}"
            : string.Empty;
        var expectedMissingMessage = missingFromExpected.Any()
            ? $@"{expected.ToStringAwesomely()}
    is missing
{missingFromExpected.ToStringAwesomely()}"
            : string.Empty;

        // "first should be second (ignoring order) but first is missing [4] and second is missing [2]"

        const string format =
            @"{0}
    {1} (ignoring order)
{2}
    but
{3}";

        var hasBothActualAndExpectedMissingItems = !string.IsNullOrEmpty(actualMissingMessage) && !string.IsNullOrEmpty(expectedMissingMessage);
        var missingMessage = hasBothActualAndExpectedMissingItems
            ? $@"{actualMissingMessage}
    and
{expectedMissingMessage}"
            : $"{actualMissingMessage}{expectedMissingMessage}";
        return string.Format(format, codePart, context.ShouldMethod.PascalToSpaced(), expectedFormattedValue, missingMessage);
    }
}