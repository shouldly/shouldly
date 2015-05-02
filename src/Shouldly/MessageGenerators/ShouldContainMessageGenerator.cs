using System.Linq.Expressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldContainMessageGenerator : ShouldlyMessageGenerator
    {
        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return context.ShouldMethod.StartsWith("Should")
                    && context.ShouldMethod.Contains("Contain")
                    && !(context.Expected is Expression);
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            var codePart = context.CodePart;
            var caseSensitivity = context.CaseSensitivity == Case.Insensitive ? " (case insensitive comparison)" : string.Empty;
            const string format = @"
    {0}
        {1}
    {2}{3}
        but was actually
    {4}";

            return string.Format(format,
                    codePart,
                    context.ShouldMethod.PascalToSpaced(),
                    context.Expected.ToStringAwesomely(),
                    caseSensitivity,
                    context.Actual.ToStringAwesomely());
        }
    }
}