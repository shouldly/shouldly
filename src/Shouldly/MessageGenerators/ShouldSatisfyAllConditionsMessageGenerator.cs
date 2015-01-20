using System;
using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldSatisfyAllConditionsMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("ShouldSatisfyAllConditions", RegexOptions.Compiled);

        public override bool CanProcess(ShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod) && !context.HasRelevantActual;
        }

        public override string GenerateErrorMessage(ShouldlyAssertionContext context)
        {
            const string format = @"
        {0} should satisfy all the conditions specified, but does not.
        The following errors were found ...
{1}";

            var codePart = context.CodePart;
            var expectedValue = context.Expected.ToString();

            return String.Format(format, codePart, expectedValue);
        }
    }
}