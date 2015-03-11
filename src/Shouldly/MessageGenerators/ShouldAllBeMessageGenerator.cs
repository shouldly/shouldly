using System;
using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldAllBeMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("ShouldAllBe", RegexOptions.Compiled);

        public override bool CanProcess(ShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod);
        }

        public override string GenerateErrorMessage(ShouldlyAssertionContext context)
        {
            const string format = @"{0} should satisfy the condition {1} but {2} do not";

            var codePart = context.CodePart;
            var expectedValue = context.Expected.Inspect();

            return String.Format(format, codePart, expectedValue, context.Actual.Inspect());
        }
    }
}