using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class DictionaryShouldOrNotContainKeyMessageGenerator : ShouldlyMessageGenerator
    {
        static readonly Regex Validator = new Regex("Should(Not)?ContainKey", RegexOptions.Compiled);

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
            var expectedValue = context.Expected.ToStringAwesomely();

            if (context.IsNegatedAssertion)
                return string.Format(format, codePart, context.ShouldMethod.PascalToSpaced(), expectedValue, "");

            return string.Format(format, codePart, context.ShouldMethod.PascalToSpaced(), expectedValue, " not");
        }
    }
}