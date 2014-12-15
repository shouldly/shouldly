using System;
using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldSatisfyAllConditionsMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("ShouldSatisfyAllConditions", RegexOptions.Compiled);

        public override bool CanProcess(TestEnvironment environment)
        {
            return Validator.IsMatch(environment.ShouldMethod) && !environment.HasActual;
        }

        public override string GenerateErrorMessage(TestEnvironment environment)
        {
            const string format = @"
        {0} should satisfy all the conditions specified, but does not.
        The following errors were found ...
{1}";

            var codePart = environment.GetCodePart();
            var expectedValue = environment.Expected.ToString();

            return String.Format(format, codePart, expectedValue);
        }
    }
}