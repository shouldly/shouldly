using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class DictionaryShouldOrNotContainKeyMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new("Should(Not)?ContainKey");

        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod);
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            const string format =
@"{0}
    {1}
{2}
    but does{3}";

            var codePart = context.CodePart ?? context.Actual.ToStringAwesomely();
            var expected = context.Expected.ToStringAwesomely();

            var should = context.ShouldMethod.PascalToSpaced();
            if (context.IsNegatedAssertion)
            {
                return string.Format(format, codePart, should, expected, "");
            }

            return string.Format(format, codePart, should, expected, " not");
        }
    }
}