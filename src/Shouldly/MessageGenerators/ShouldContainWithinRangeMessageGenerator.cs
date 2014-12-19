using System.Linq;

namespace Shouldly.MessageGenerators
{
    internal class ShouldContainWithinRangeMessageGenerator : ShouldlyMessageGenerator
    {
        public override bool CanProcess(TestEnvironment environment)
        {
            return environment.ShouldMethod.StartsWith("Should")
                   && environment.ShouldMethod.Contains("Contain")
                   && environment.UnderlyingShouldMethod.GetParameters().Last().Name == "tolerance";
        }

        public override string GenerateErrorMessage(TestEnvironment environment)
        {
            const string format = @"
        {0}
    should {1}contain
        {2}
    within
        {3}
    but was
        {4}";

            var codePart = environment.CodePart;
            var tolerance = environment.Tolerance.Inspect();
            var expectedValue = environment.Expected.Inspect();
            var actualValue = environment.Actual.Inspect();
            var negated = environment.ShouldMethod.Contains("Not") ? "not " : string.Empty;
            
            return string.Format(format, codePart, negated, expectedValue, tolerance, actualValue);
        }
    }
}