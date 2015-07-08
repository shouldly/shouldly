using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldBeNullMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("Should(Not)?BeNull", RegexOptions.Compiled);

        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod);
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            const string format = @"{0} {1} but was {2}";

            var codePart = context.CodePart;
            var expectedValue = context.Expected.ToStringAwesomely();

            return string.Format(format, codePart, context.ShouldMethod.PascalToSpaced(), expectedValue);
        }
    }
}