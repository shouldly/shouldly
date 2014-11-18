using System;
using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class DictionaryShouldContainKeyAndValueMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("ShouldContainKeyAndValue", RegexOptions.Compiled);
        public override bool CanProcess(TestEnvironment environment)
        {
            return Validator.IsMatch(environment.ShouldMethod);
        }

        public override string GenerateErrorMessage(TestEnvironment environment)
        {
            const string format = @"
    Dictionary
        ""{0}""
    should contain key
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
                var valueString = string.Format("but value was \"{0}\"", actualValue.Trim('"'));
                return String.Format(format, codePart, keyValue.Trim('"'), expectedValue.Trim('"'), valueString);
            }
            else
            {
                return String.Format(format, codePart, actualValue.Trim('"'), expectedValue.Trim('"'), "but the key does not exist");
            }
        }
    }
}