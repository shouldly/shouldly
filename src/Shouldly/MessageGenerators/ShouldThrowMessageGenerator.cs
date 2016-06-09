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
            var maybeInvokeMethod = isExtensionMethod && !throwContext.IsAsync ? "()" : string.Empty;
            string codePart;
            if (context.CodePart == "null" || context.CodePartMatchesActual)
            {
                codePart = throwContext.IsAsync ? "Task" : "delegate";
            }
            else
            {
                codePart = $"`{context.CodePart}{maybeInvokeMethod}`";
                if (throwContext.IsAsync)
                    codePart = "Task " + codePart;
            }

            var expectedValue = context.Expected.ToStringAwesomely();

            string errorMessage;
            if (context.HasRelevantActual)
            {
                errorMessage = string.Format($@"{codePart.Replace("{", "{{").Replace("}", "}}")}
    should throw
{expectedValue}
    but threw
{context.Actual}", codePart, expectedValue, context.Actual, maybeInvokeMethod);
            }
            else
            {
                errorMessage = $@"{codePart}
    should throw
{expectedValue}
    but did not";
            }

            errorMessage += (throwContext.ExceptionMessage != null) ?
                $@" with message
""{throwContext.ExceptionMessage}"""
                : string.Empty;

            return errorMessage;
        }
    }
}