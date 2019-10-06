using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class DictionaryShouldContainKeyAndValueMessageGenerator : ShouldlyMessageGenerator
    {
        static readonly Regex Validator = new Regex("ShouldContainKeyAndValue");

        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod);
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            Debug.Assert(context.Actual is IDictionary);
            Debug.Assert(context.Key is object);

            const string format =
@"{0}
    should contain key
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
                var actualValueString = dictionary[context.Key].ToStringAwesomely();
                var valueString =
$@"    but value was
{actualValueString}";
                return string.Format(format, codePart, keyValue, expectedValue, valueString);
            }

            return string.Format(format, codePart, keyValue, expectedValue, "    but the key does not exist");
        }
    }
}