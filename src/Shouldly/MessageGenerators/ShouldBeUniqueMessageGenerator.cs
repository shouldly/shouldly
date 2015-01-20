using System;
using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldBeUniqueMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("ShouldBeUnique", RegexOptions.Compiled);

        public override bool CanProcess(ShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod) && context.HasRelevantActual;
        }

        public override string GenerateErrorMessage(ShouldlyAssertionContext context)
        {
            const string format = @"
    {0}
            {1}
    but {2}
            was duplicated";

            var codePart = context.CodePart;
            var actual = context.Actual.Inspect();

            return String.Format(format, codePart, context.ShouldMethod.PascalToSpaced(), actual);
        }
    }
}