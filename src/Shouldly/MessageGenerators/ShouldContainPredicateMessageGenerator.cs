using System.Linq.Expressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldContainPredicateMessageGenerator : ShouldlyMessageGenerator
    {
        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return context.ShouldMethod.StartsWith("Should")
                   && context.ShouldMethod.Contains("Contain")
                   && context.Expected is Expression;
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            var codePart = context.CodePart == "null" ? context.Actual.ToStringAwesomely() : context.CodePart;

            var elementPhrase = context.MatchCount.HasValue
                ? context.MatchCount.Value + " element(s)"
                : "an element";

            var should = context.ShouldMethod.PascalToSpaced();
            var expected = context.Expected.ToStringAwesomely();
            if (context.IsNegatedAssertion)
            {
                return
                    $@"{codePart}
    {should} {elementPhrase} satisfying the condition
{expected}
    but does{""}";
            }

            return
                $@"{codePart}
    {should} {elementPhrase} satisfying the condition
{expected}
    but does not";
        }
    }
}