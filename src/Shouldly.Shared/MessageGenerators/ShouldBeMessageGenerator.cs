using System;
using Shouldly.DifferenceHighlighting;

namespace Shouldly.MessageGenerators
{
    internal class ShouldBeMessageGenerator: ShouldlyMessageGenerator
    {
        const string ShouldBeAssertion = "ShouldBe";
        const string ShouldNotBeAssertion = "ShouldNotBe";

        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return context.ShouldMethod.Equals(ShouldBeAssertion, StringComparison.OrdinalIgnoreCase) ||
                   context.ShouldMethod.Equals(ShouldNotBeAssertion, StringComparison.OrdinalIgnoreCase);
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            var codePart = context.CodePart;
            var expected = context.Expected.ToStringAwesomely();
            var actualValue = context.Actual.ToStringAwesomely();
            string actual;
            if (context.IsNegatedAssertion)
            {
                actual = string.Empty;
            }
            else if (codePart == actualValue)
            {
                actual = " not";
            }
            else
            {
                actual = $"\r\n{actualValue}";
            }
            var message =
$@"{codePart}
    {context.ShouldMethod.PascalToSpaced()}
{expected}
    but was{actual}";

            if (DifferenceHighlighter.CanHighlightDifferences(context))
            {
                message += $@"
    difference
{DifferenceHighlighter.HighlightDifferences(context)}";
            }
            return message;
        }
    }
}
