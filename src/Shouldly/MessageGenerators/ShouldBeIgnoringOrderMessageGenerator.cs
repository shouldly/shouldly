using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Shouldly.MessageGenerators
{
    internal class ShouldBeIgnoringOrderMessageGenerator : ShouldlyMessageGenerator
    {
        public override bool CanProcess(ShouldlyAssertionContext context)
        {
            return context.IgnoreOrder;
        }

        public override string GenerateErrorMessage(ShouldlyAssertionContext context)
        {
            var expected = ((IEnumerable)context.Expected).Cast<object>().ToArray();
            var actual = ((IEnumerable)context.Actual).Cast<object>().ToArray();
            var codePart = context.CodePart;
            var expectedFormattedValue = expected.Inspect();

            var missingFromExpected = actual.Where(a => !expected.Any(e => Is.Equal(e, a))).ToArray();
            var missingFromActual = expected.Where(e => !actual.Any(a => Is.Equal(e, a))).ToArray();

            var actualMissingMessage = missingFromActual.Any() ? string.Format("{0} is missing {1}", codePart,
                missingFromActual.Inspect()) : string.Empty;
            var expectedMissingMessage = missingFromExpected.Any() ? string.Format("{0} is missing {1}", expectedFormattedValue,
                missingFromExpected.Inspect()) : string.Empty;

            //"first should be second (ignoring order) but first is missing [4] and second is missing [2]"

            const string format = @"
    {0}
            {1}
    {2} (ignoring order)
            but
    {3}";

            string missingMessage = !string.IsNullOrEmpty(actualMissingMessage) && !string.IsNullOrEmpty(expectedMissingMessage)
                ? string.Format("{0} and {1}", actualMissingMessage, expectedMissingMessage)
                : string.Format("{0}{1}", actualMissingMessage, expectedMissingMessage);
            return string.Format(format, codePart, context.ShouldMethod.PascalToSpaced(), expectedFormattedValue, missingMessage);
        }
    }
}