namespace Shouldly.MessageGenerators
{
    internal class ShouldThrowMessageGenerator : ShouldlyMessageGenerator
    {
        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return context is ShouldThrowAssertionContext && !context.IsNegatedAssertion;
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            var throwContext = (ShouldThrowAssertionContext)context;
            var isExtensionMethod = context.ShouldMethod.StartsWith("ShouldThrow");
            var codePart = context.CodePart;

            var expectedValue = context.Expected.ToStringAwesomely();

            var maybeInvokeMethod = isExtensionMethod && !throwContext.IsAsync ? "()" : string.Empty;
            var errorMessage = context.HasRelevantActual 
                ? string.Format(@"`{0}{3}` should throw {1} but threw {2}", codePart, expectedValue, context.Actual, maybeInvokeMethod)
                : string.Format(@"`{0}{2}` should throw {1} but did not", codePart, expectedValue, maybeInvokeMethod);

            errorMessage += (throwContext.ExceptionMessage != null) ? string.Format(" with message \"{0}\"", throwContext.ExceptionMessage) : string.Empty;

            return errorMessage;
        }
    }
}