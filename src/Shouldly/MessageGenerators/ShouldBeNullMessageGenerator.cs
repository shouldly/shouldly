using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldBeNullMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new("Should(Not)?BeNull");

        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod);
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            var expected = context.Expected.ToStringAwesomely();
            var codePart = context.CodePart == "null" ? expected : context.CodePart;
            var expectedValue = context.IsNegatedAssertion || expected == codePart ? string.Empty : $@"
{expected}";

            return $@"{codePart}
    {context.ShouldMethod.PascalToSpaced()} but was{expectedValue}";
        }
    }
}