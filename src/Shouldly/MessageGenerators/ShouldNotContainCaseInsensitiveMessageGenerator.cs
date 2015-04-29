using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldNotContainCaseInsensitiveMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("ShouldNotContain", RegexOptions.Compiled);

        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            // Cannot check to see if caseSensitivity paramter is Case.Insensitive
            ParameterInfo methodParameterInfo = context.UnderlyingShouldMethod.GetParameters().FirstOrDefault(x => x.Name == "caseSensitivity");

            return Validator.IsMatch(context.ShouldMethod);
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            // const string format = @"{0} should satisfy the condition {1} but {2} do not";
            const string format = @"{0} should not contain case insensitive {1} but does";

            var codePart = context.CodePart;
            var expectedValue = context.Expected.ToStringAwesomely();
            var actualValue = context.Actual.ToStringAwesomely();

            return string.Format(format, codePart, expectedValue);
        }
    }
}