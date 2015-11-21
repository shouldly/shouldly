using System;
using Shouldly.DifferenceHighlighting;

namespace Shouldly.MessageGenerators
{
    internal class ShouldBeMessageGenerator: ShouldlyMessageGenerator
    {
        private const string ShouldBeAssertion = "ShouldBe";
        private const string ShouldNotBeAssertion = "ShouldNotBe";

        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return context.ShouldMethod.Equals(ShouldBeAssertion, StringComparison.OrdinalIgnoreCase) ||
                   context.ShouldMethod.Equals(ShouldNotBeAssertion, StringComparison.OrdinalIgnoreCase);
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            var codePart = context.CodePart;
            var message = string.Format(@"
    {0}
        {1}
    {2}
        but was
    {3}",
                codePart, context.ShouldMethod.PascalToSpaced(), context.Expected.ToStringAwesomely(),
                context.IsNegatedAssertion ? string.Empty : context.Actual.ToStringAwesomely());

            if (DifferenceHighlighter.CanHighlightDifferences(context))
            {
                message += string.Format(@"
        difference
    {0}",
                DifferenceHighlighter.HighlightDifferences(context));
            }
            return message;
        }
    }
}
