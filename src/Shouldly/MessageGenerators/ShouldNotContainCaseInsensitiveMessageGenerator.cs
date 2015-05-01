namespace Shouldly.MessageGenerators
{
    internal class ShouldNotContainCaseInsensitiveMessageGenerator : ShouldlyMessageGenerator
    {
        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            bool canProcess = context is ExpectedActualCaseInsensitiveShouldlyContext;

            return canProcess;
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