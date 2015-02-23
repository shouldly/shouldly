using System.Linq;
using Shouldly.DifferenceHighlighting;

namespace Shouldly.MessageGenerators
{
    internal class ShouldBeWithinRangeMessageGenerator : ShouldlyMessageGenerator
    {
        public override bool CanProcess(ShouldlyAssertionContext context)
        {
            return context.ShouldMethod.StartsWith("Should")
                   && !context.ShouldMethod.Contains("Contain")
                   && context.UnderlyingShouldMethod.GetParameters().Last().Name == "tolerance";
        }

        public override string GenerateErrorMessage(ShouldlyAssertionContext context)
        {
            const string format = @"
        {0}
    should {1}be within
        {2}
    of
        {3}
    but was 
        {4}";

            var codePart = context.CodePart;
            var tolerance = context.Tolerance.ToStringAwesomely();
            var expectedValue = context.Expected.ToStringAwesomely();
            var actualValue = context.Actual.ToStringAwesomely();
            var negated = context.ShouldMethod.Contains("Not") ? "not " : string.Empty;

            var message = string.Format(format, codePart, negated, tolerance, expectedValue, actualValue);

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