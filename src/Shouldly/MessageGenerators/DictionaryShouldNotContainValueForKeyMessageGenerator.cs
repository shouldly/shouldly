using System;
using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class DictionaryShouldNotContainValueForKeyMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("ShouldNotContainValueForKey", RegexOptions.Compiled);
        public override bool CanProcess(TestEnvironment environment)
        {
            return Validator.IsMatch(environment.ShouldMethod);
        }

        public override string GenerateErrorMessage(TestEnvironment environment)
        {
            const string format = @"
    Dictionary
        ""{0}""
    should not contain key
        ""{1}""
    with value
        ""{2}""
    {3}";

            var codePart = environment.GetCodePart();
            var expectedValue = environment.Expected.Inspect();
            var actualValue = environment.Actual.Inspect();
            var keyValue = environment.Key.Inspect();

            if (environment.HasKey)
            {
                var valueString = "but does";
                return String.Format(format, codePart, keyValue.Trim('"'), expectedValue.Trim('"'), valueString);
            }
            else
            {
                return String.Format(format, codePart, actualValue.Trim('"'), expectedValue.Trim('"'), "but the key does not exist");
            }
        }
    }
}