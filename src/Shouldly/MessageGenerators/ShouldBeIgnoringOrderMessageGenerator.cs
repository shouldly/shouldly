using System.Collections;
using System.Diagnostics;
using System.Linq;

namespace Shouldly.MessageGenerators
{
    internal class ShouldBeIgnoringOrderMessageGenerator : ShouldlyMessageGenerator
    {
        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return context.IgnoreOrder;
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            Debug.Assert(context.Expected is IEnumerable);
            Debug.Assert(context.Actual is IEnumerable);

            var expected = ((IEnumerable)context.Expected).Cast<object>().ToArray();
            var actual = ((IEnumerable)context.Actual).Cast<object>().ToArray();
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
}