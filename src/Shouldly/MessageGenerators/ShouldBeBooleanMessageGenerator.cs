using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldBeBooleanMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("ShouldBe(True|False)");

        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod);
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            var codePart = context.CodePart;
            var expectedValue = context.Expected.ToStringAwesomely();

            var actual = context.Actual.ToStringAwesomely();
            var actualString = codePart == actual ? " not" : $@"
{actual}";

            return $@"{codePart}
    should be
{expectedValue}
    but was{actualString}";
        }
    }
}