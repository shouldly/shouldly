using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldSatisfyAllConditionsMessageGenerator : ShouldlyMessageGenerator
    {
        static readonly Regex Validator = new Regex("ShouldSatisfyAllConditions");

        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod);
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            var codePart = context.CodePart;
            var expectedValue = context.Expected.ToString();

            return
$@"{codePart}
    should satisfy all the conditions specified, but does not.
The following errors were found ...
{expectedValue}";
        }
    }
}