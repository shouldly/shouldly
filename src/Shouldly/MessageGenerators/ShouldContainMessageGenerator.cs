using System.Linq.Expressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldContainMessageGenerator : ShouldlyMessageGenerator
    {
        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return context.ShouldMethod.StartsWith("Should", StringComparison.Ordinal)
                    && context.ShouldMethod.Contains("Contain")
                    && context.Expected is not Expression;
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            var codePart = context.CodePart;
            var caseSensitivity = context.CaseSensitivity == Case.Insensitive ? " (case insensitive comparison)" : string.Empty;
            var actual = context.Actual.ToStringAwesomely();
            var but = codePart == actual ? context.IsNegatedAssertion ? "did" : "did not" : $@"was actually
{actual}";

            return
$@"{codePart}
    {context.ShouldMethod.PascalToSpaced()}{caseSensitivity}
{context.Expected.ToStringAwesomely()}
    but {but}";
        }
    }
}