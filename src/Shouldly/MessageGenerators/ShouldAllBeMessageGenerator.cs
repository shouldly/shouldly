using System;
using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldAllBeMessageGenerator : ShouldlyMessageGenerator
    {
        static readonly Regex Validator = new Regex("ShouldAllBe", RegexOptions.Compiled);

        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod);
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            var codePart = context.CodePart;
            var expectedValue = context.Expected.ToStringAwesomely();
#if net40
            var expression = ExpressionToString.ExpressionStringBuilder.ToString(context.Filter);
#else
            var expression = context.Filter;
#endif
            return $@"{codePart}
    should satisfy the condition
{expression}
    but
{expectedValue}
    do not";
        }
    }
}