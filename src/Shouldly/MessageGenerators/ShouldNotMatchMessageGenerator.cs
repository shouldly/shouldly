using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldNotMatchMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new("ShouldNotMatch");

        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod);
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            const string format = @"{0} should not match {1} but did";

            var codePart = context.CodePart;
            var expected = context.Expected.ToStringAwesomely();

            return string.Format(format, codePart, expected);
        }
    }
}