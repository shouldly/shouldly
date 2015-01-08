using System;
using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldBeEmptyMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("Should(Not)?BeEmpty", RegexOptions.Compiled);

        public override bool CanProcess(ShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod) && !context.HasActual;
        }

        public override string GenerateErrorMessage(ShouldlyAssertionContext context)
        {
            const string format = @"
    {0}
            {1}
        but was {2}";

            var codePart = context.CodePart;
            var expectedValue = context.Expected.Inspect();

            if (context.IsNegatedAssertion)
                return String.Format(format, codePart, context.ShouldMethod.PascalToSpaced(), context.Expected == null ? "null" : "");

            return String.Format(format, codePart, context.ShouldMethod.PascalToSpaced(), expectedValue);
        }
    }
}