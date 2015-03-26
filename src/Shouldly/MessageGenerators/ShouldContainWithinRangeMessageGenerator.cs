using System.Linq;

namespace Shouldly.MessageGenerators
{
    internal class ShouldContainWithinRangeMessageGenerator : ShouldlyMessageGenerator
    {
        public override bool CanProcess(ShouldlyAssertionContext context)
        {
            return context.ShouldMethod.StartsWith("Should")
                   && context.ShouldMethod.Contains("Contain")
                   && context.UnderlyingShouldMethod.GetParameters().Any(p => p.Name == "tolerance");
        }

        public override string GenerateErrorMessage(ShouldlyAssertionContext context)
        {
            const string format = @"
        {0}
    should {1}contain
        {2}
    within
        {3}
    but was
        {4}";

            var codePart = context.CodePart;
            var tolerance = context.Tolerance.ToStringAwesomely();
            var expectedValue = context.Expected.ToStringAwesomely();
            var actualValue = context.Actual.ToStringAwesomely();
            var negated = context.ShouldMethod.Contains("Not") ? "not " : string.Empty;
            
            return string.Format(format, codePart, negated, expectedValue, tolerance, actualValue);
        }
    }
}