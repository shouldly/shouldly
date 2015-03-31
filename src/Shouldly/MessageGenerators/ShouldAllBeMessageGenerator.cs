using System;
using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldAllBeMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("ShouldAllBe", RegexOptions.Compiled);

        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod);
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            const string format = @"{0} should satisfy the condition {1} but {2} do not";

            var codePart = context.CodePart;
            var expectedValue = context.Expected.ToStringAwesomely();

            return string.Format(format, codePart, expectedValue, context.Actual.ToStringAwesomely());
        }
    }
}