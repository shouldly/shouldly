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

            if (context.IsNegatedAssertion)
            {
                return
$@"{codePart}
    {context.ShouldMethod.PascalToSpaced()} {elementPhrase} satisfying the condition
{context.Expected.ToStringAwesomely()}
    but does{""}{(!context.HasListedFailures ? "" : $@"
        {context.ListedFailures.ToStringAwesomely()}")}";
            }

            return
$@"{codePart}
    {context.ShouldMethod.PascalToSpaced()} {elementPhrase} satisfying the condition
{context.Expected.ToStringAwesomely()}
    but does not{(!context.HasListedFailures ? "" : $@"
        {context.ListedFailures.ToStringAwesomely()}")}";

        }
    }
}