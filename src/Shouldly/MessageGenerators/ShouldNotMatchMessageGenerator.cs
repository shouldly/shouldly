using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldNotMatchMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("ShouldNotMatch", RegexOptions.Compiled);

        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod);
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            const string format = @"{0} should not match {1} but did";

            var codePart = context.CodePart;
            var expectedValue = context.Expected.ToStringAwesomely();

            return string.Format(format, codePart, expectedValue);
        }
    }
}