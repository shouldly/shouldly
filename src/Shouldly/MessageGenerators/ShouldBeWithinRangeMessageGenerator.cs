using System.Linq;
using Shouldly.DifferenceHighlighting;

namespace Shouldly.MessageGenerators
{
    internal class ShouldBeWithinRangeMessageGenerator : ShouldlyMessageGenerator
    {
        public override bool CanProcess(TestEnvironment environment)
        {
            return environment.ShouldMethod.StartsWith("Should")
                   && !environment.ShouldMethod.Contains("Contain")
                   && environment.UnderlyingShouldMethod.GetParameters().Last().Name == "tolerance";
        }

        public override string GenerateErrorMessage(TestEnvironment environment)
        {
            const string format = @"
        {0}
    should {1}be within
        {2}
    of
        {3}
    but was 
        {4}";

            var codePart = environment.CodePart;
            var tolerance = environment.Tolerance.Inspect();
            var expectedValue = environment.Expected.Inspect();
            var actualValue = environment.Actual.Inspect();
            var negated = environment.ShouldMethod.Contains("Not") ? "not " : string.Empty;

            var message = string.Format(format, codePart, negated, tolerance, expectedValue, actualValue);

            if (DifferenceHighlighterExtensions.CanGenerateDifferencesBetween(environment))
            {
                message += string.Format(@"
        difference
    {0}",
                    DifferenceHighlighterExtensions.HighlightDifferencesBetween(environment));
            }

            return message;
        }
    }
}