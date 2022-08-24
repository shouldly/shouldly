namespace Shouldly.MessageGenerators
{
    internal class ShouldContainWithinRangeMessageGenerator : ShouldlyMessageGenerator
    {
        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return context.ShouldMethod.StartsWith("Should", StringComparison.Ordinal)
                   && context.ShouldMethod.Contains("Contain")
                   && context.Tolerance != null;
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            var codePart = context.CodePart;
            var tolerance = context.Tolerance.ToStringAwesomely();
            var expected = context.Expected.ToStringAwesomely();
            var actualValue = context.Actual.ToStringAwesomely();
            var negated = context.ShouldMethod.Contains("Not") ? "not " : string.Empty;

            var actualValueString = codePart == actualValue ? " not" : $@"
{actualValue}";

            return
$@"{codePart}
    should {negated}contain
{expected}
    within
{tolerance}
    but was{actualValueString}";
        }
    }
}