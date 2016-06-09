using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldBeNullOrEmptyMessageGenerator : ShouldlyMessageGenerator
    {
        static readonly Regex Validator = new Regex("Should(Not)?BeNullOrEmpty");

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
            if (isNegatedAssertion)
                return string.Format(format, codePart, context.ShouldMethod.PascalToSpaced(), context.Actual.ToStringAwesomely());

            return string.Format(format, codePart, context.ShouldMethod.PascalToSpaced(), context.Actual.ToStringAwesomely());
        }
    }
}