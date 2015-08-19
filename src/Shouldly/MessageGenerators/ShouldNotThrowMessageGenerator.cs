namespace Shouldly.MessageGenerators
{
    internal class ShouldNotThrowMessageGenerator : ShouldlyMessageGenerator
    {
        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return context is ShouldThrowAssertionContext && context.IsNegatedAssertion;
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            var throwContext = (ShouldThrowAssertionContext)context;
            var isExtensionMethod = context.ShouldMethod == "ShouldNotThrow";
            var codePart = context.CodePart;

            var expectedValue = context.Expected.ToStringAwesomely();

            string errorMessage;
            if (isExtensionMethod && !throwContext.IsAsync)
                errorMessage = string.Format("`{0}()` should not throw but threw {1}", codePart, expectedValue);
            else if (isExtensionMethod && throwContext.IsAsync)
                errorMessage = string.Format("Task `{0}` should not throw but threw {1}", codePart, expectedValue);
            else
                errorMessage = string.Format("`{0}` should not throw but threw {1}", codePart, expectedValue);

            var notThrowAssertionContext = context as ShouldThrowAssertionContext;
            errorMessage += (notThrowAssertionContext != null) ? string.Format(" with message \"{0}\"", notThrowAssertionContext.ExceptionMessage) : string.Empty;

            return errorMessage;
        }
    }
}