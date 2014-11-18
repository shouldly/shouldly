using System;
using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class DictionaryShouldOrNotContainKeyMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("Should(Not)?ContainKey", RegexOptions.Compiled);
        public override bool CanProcess(TestEnvironment environment)
        {
            return Validator.IsMatch(environment.ShouldMethod) && !environment.HasActual;
        }

        public override string GenerateErrorMessage(TestEnvironment environment)
        {
            const string format = @"
    Dictionary
        ""{0}""
    {1}
        ""{2}""
    but does {3}";

            var codePart = environment.GetCodePart();
            var expectedValue = environment.Expected.Inspect();

            if (environment.IsNegatedAssertion)
                return String.Format(format, codePart, environment.ShouldMethod.PascalToSpaced(), environment.Expected, "");

            return String.Format(format, codePart, environment.ShouldMethod.PascalToSpaced(), expectedValue.Trim('"'), "not");
        }
    }
}