using System.Collections;
using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class DictionaryShouldNotContainValueForKeyMessageGenerator : ShouldlyMessageGenerator
    {
        static readonly Regex Validator = new Regex("ShouldNotContainValueForKey");

        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod);
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            const string format =
@"{0}
    should not contain key
{1}
    with value
{2}
    {3}";

            var codePart = context.CodePart;
            var dictionary = (IDictionary)context.Actual;
            var keyExists = dictionary.Contains(context.Key);
            var expectedValue = context.Expected.ToStringAwesomely();
            var keyValue = context.Key.ToStringAwesomely();

            if (keyExists)
            {
                var valueString = "but does";
                return string.Format(format, codePart, keyValue, expectedValue, valueString);
            }

            return string.Format(format, codePart, keyValue, expectedValue, "but the key does not exist");
        }
    }
}