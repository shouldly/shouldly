namespace Shouldly.MessageGenerators
{
    internal class ShouldHaveFlagMessageGenerator : ShouldlyMessageGenerator
    {
        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return context.ShouldMethod == "ShouldHaveFlag";
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            var codePart = context.CodePart;
            var expectedValue = context.Expected.ToStringAwesomely();
            
            var actual = context.Actual.ToStringAwesomely();
            var actualString = codePart == actual ? " did not" : $@" had
{actual}";

            return $@"{codePart}
    should have flag
{expectedValue}
    but{actualString}";
        }
    }
}