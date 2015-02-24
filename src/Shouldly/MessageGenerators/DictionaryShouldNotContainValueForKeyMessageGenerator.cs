using System;
using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class DictionaryShouldNotContainValueForKeyMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("ShouldNotContainValueForKey", RegexOptions.Compiled);
        public override bool CanProcess(ShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod);
        }

        public override string GenerateErrorMessage(ShouldlyAssertionContext context)
        {
            const string format = @"
    Dictionary
        ""{0}""
    should not contain key
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