using System;
using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class DictionaryShouldContainKeyAndValueMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("ShouldContainKeyAndValue", RegexOptions.Compiled);
        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod);
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            const string format = @"
    Dictionary
        ""{0}""
    should contain key
        ""{1}""
    with value
        ""{2}""
    {3}";

            var codePart = context.CodePart;
            var expectedValue = context.Expected.ToStringAwesomely();
            var actualValue = context.Actual.ToStringAwesomely();
            var keyValue = context.Key.ToStringAwesomely();

            if (context.HasRelevantKey)
            {
                var actualValueString = context.Actual == null
                    ? actualValue
                    : string.Format("\"{0}\"", actualValue.Trim('"'));
                var valueString = string.Format("but value was {0}", actualValueString);
                return String.Format(format, codePart, keyValue.Trim('"'), expectedValue.Trim('"'), valueString);
            }
            else
            {
                return String.Format(format, codePart, actualValue.Trim('"'), expectedValue.Trim('"'), "but the key does not exist");
            }
        }
    }
}