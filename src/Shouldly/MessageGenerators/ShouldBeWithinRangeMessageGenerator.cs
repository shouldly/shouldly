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
            var tolerance = context.Tolerance.Inspect();
            var expectedValue = context.Expected.Inspect();
            var actualValue = context.Actual.Inspect();
            var negated = context.ShouldMethod.Contains("Not") ? "not " : string.Empty;

            var message = string.Format(format, codePart, negated, tolerance, expectedValue, actualValue);

            if (DifferenceHighlighterExtensions.CanGenerateDifferencesBetween(context))
            {
                message += string.Format(@"
        difference
    {0}",
                    DifferenceHighlighterExtensions.HighlightDifferencesBetween(context));
            }

            return message;
        }
    }
}