using System;
using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class DictionaryShouldOrNotContainKeyMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("Should(Not)?ContainKey", RegexOptions.Compiled);
        public override bool CanProcess(ShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod) && !context.HasRelevantActual;
        }

        public override string GenerateErrorMessage(ShouldlyAssertionContext context)
        {
            const string format = @"
    Dictionary
        ""{0}""
    {1}
        ""{2}""
    but does {3}";

            var codePart = context.CodePart;
            var expectedValue = context.Expected.Inspect();

            if (context.IsNegatedAssertion)
                return String.Format(format, codePart, context.ShouldMethod.PascalToSpaced(), context.Expected, "");

            return String.Format(format, codePart, context.ShouldMethod.PascalToSpaced(), expectedValue.Trim('"'), "not");
        }
    }
}