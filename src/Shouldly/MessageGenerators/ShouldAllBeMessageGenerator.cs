using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldAllBeMessageGenerator : ShouldlyMessageGenerator
    {
        static readonly Regex Validator = new Regex("ShouldAllBe");

        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod);
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            var codePart = context.CodePart;
            var expectedValue = context.Expected.ToStringAwesomely();
            var expression = ExpressionToString.ExpressionStringBuilder.ToString(context.Filter);
            return $@"{codePart}
    should satisfy the condition
{expression}
    but
{expectedValue}
    do not";
        }
    }
}