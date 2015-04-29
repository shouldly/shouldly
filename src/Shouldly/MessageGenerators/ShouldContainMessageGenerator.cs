using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldContainMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("ShouldContain", RegexOptions.Compiled);

        public override bool CanProcess(IShouldlyAssertionContext context)
        {
           return Validator.IsMatch(context.ShouldMethod) && !(context.Expected is Expression);
        }

/*
        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            var res = context.ShouldMethod.StartsWith("Should");
            var res2 = context.ShouldMethod.Contains("Contain");
            var res3 = !(context.Expected is Expression);

            return res && res2 && res3;
        }
*/

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            var codePart = context.CodePart;
            const string format = @"
    {0}
        {1}
    {2}
        but does{3}";
            if (context.IsNegatedAssertion)
                return string.Format(format, codePart, context.ShouldMethod.PascalToSpaced(), context.Expected.ToStringAwesomely(), "");
            return string.Format(format, codePart, context.ShouldMethod.PascalToSpaced(), context.Expected.ToStringAwesomely(), " not");
        }
    }
}