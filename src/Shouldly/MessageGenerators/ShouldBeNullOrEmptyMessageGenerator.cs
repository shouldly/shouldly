using System;
using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldBeNullOrEmptyMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("Should(Not)?BeNullOrEmpty", RegexOptions.Compiled);
        public override bool CanProcess(TestEnvironment environment)
        {
            return Validator.IsMatch(environment.ShouldMethod) && !environment.HasActual;
        }

        public override string GenerateErrorMessage(TestEnvironment environment)
        {
            const string format = @"
    {0}
            {1}";

            var codePart = environment.CodePart;

            var isNegatedAssertion = environment.ShouldMethod.Contains("Not");
            if (isNegatedAssertion)
                return String.Format(format, codePart, environment.ShouldMethod.PascalToSpaced());

            return String.Format(format, codePart, environment.ShouldMethod.PascalToSpaced());
        }
    }
}