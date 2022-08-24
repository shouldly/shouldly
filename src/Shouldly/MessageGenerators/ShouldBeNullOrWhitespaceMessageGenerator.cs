using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldBeNullOrWhiteSpaceMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new("Should(Not)?BeNullOrWhiteSpace");

        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod);
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            var format = context.CodePartMatchesActual ?
@"{0}
    {1}"
:
@"{0} ({2})
    {1}";

            var codePart = context.CodePart;

            var isNegatedAssertion = context.ShouldMethod.Contains("Not");
            var should = context.ShouldMethod.PascalToSpaced();
            var actual = context.Actual.ToStringAwesomely();
            if (isNegatedAssertion)
            {
                return string.Format(format, codePart, should, actual);
            }

            return string.Format(format, codePart, should, actual);
        }
    }
}