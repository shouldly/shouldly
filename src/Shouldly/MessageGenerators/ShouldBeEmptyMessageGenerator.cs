using System;
using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldBeEmptyMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("Should(Not)?BeEmpty", RegexOptions.Compiled);

        public override bool CanProcess(TestEnvironment environment)
        {
            return Validator.IsMatch(environment.ShouldMethod) && !environment.HasActual;
        }

        public override string GenerateErrorMessage(TestEnvironment environment)
        {
            const string format = @"
    {0}
            {1}
        but was {2}";

            var codePart = environment.CodePart;
            var expectedValue = environment.Expected.Inspect();

            if (environment.IsNegatedAssertion)
                return String.Format(format, codePart, environment.ShouldMethod.PascalToSpaced(), environment.Expected == null ? "null" : "");

            return String.Format(format, codePart, environment.ShouldMethod.PascalToSpaced(), expectedValue);
        }
    }
}