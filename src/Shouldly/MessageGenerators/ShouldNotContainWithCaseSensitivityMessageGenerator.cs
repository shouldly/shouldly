namespace Shouldly.MessageGenerators
{
    internal class ShouldNotContainWithCaseSensitivityMessageGenerator : ShouldlyMessageGenerator
    {
        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return context is ExpectedActualWithCaseSensitivityShouldlyContext;
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            const string format = @"{0} should not contain {1}{2} but was actually {3}";

            var codePart = context.CodePart;
            var expectedValue = context.Expected.ToStringAwesomely();
            var actualValue = context.Actual.ToStringAwesomely();
            var sensitivity = ((ExpectedActualWithCaseSensitivityShouldlyContext)context).CaseSensitivity;
            var caseSensitivity = sensitivity == Case.Insensitive ? " (case insensitive comparison)" : string.Empty;

            return string.Format(format, codePart, expectedValue, caseSensitivity, actualValue);
        }
    }
}