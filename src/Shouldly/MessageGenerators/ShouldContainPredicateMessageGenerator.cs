using System.Linq.Expressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldContainPredicateMessageGenerator : ShouldlyMessageGenerator
    {
        public override bool CanProcess(ShouldlyAssertionContext context)
        {
            return context.ShouldMethod.StartsWith("Should")
                   && context.ShouldMethod.Contains("Contain")
                   && context.Expected is Expression;
        }

        public override string GenerateErrorMessage(ShouldlyAssertionContext context)
        {
            var codePart = context.CodePart;
            const string format = @"
    {0}
        {1} an element satisfying the condition
    {2}
        but does{3}";
            if (context.IsNegatedAssertion)
                return string.Format(format, codePart, context.ShouldMethod.PascalToSpaced(), context.Expected.ToStringAwesomely(), "");
            return string.Format(format, codePart, context.ShouldMethod.PascalToSpaced(), context.Expected.ToStringAwesomely(), " not");
        }
    }
}