using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldThrowMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("Throw", RegexOptions.Compiled);

        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod);
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            var codePart = context.CodePart;
            
            var expectedValue = context.Expected.ToStringAwesomely();

            const string format = @"{0} should throw {1} but did not";
            string errorMessage = string.Format(format, codePart, expectedValue);

            var notThrowAssertionContext = context as ShouldThrowAssertionContext;
            errorMessage += (notThrowAssertionContext != null) ? string.Format(" with message \"{0}\"", notThrowAssertionContext.ExceptionMessage) : string.Empty;

            return errorMessage;
        }
    }
}