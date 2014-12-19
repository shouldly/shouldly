using System;
using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldBeUniqueMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("ShouldBeUnique", RegexOptions.Compiled);

        public override bool CanProcess(TestEnvironment environment)
        {
            return Validator.IsMatch(environment.ShouldMethod) && environment.HasActual;
        }

        public override string GenerateErrorMessage(TestEnvironment environment)
        {
            const string format = @"
    {0}
            {1}
    but {2}
            was duplicated";

            var codePart = environment.CodePart;
            var actual = environment.Actual.Inspect();

            return String.Format(format, codePart, environment.ShouldMethod.PascalToSpaced(), actual);
        }
    }
}