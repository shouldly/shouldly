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
            if (codePart == "null" && !throwContext.IsAsync)
            {
                errorMessage = 
$@"delegate
    should not throw but threw
{expectedValue}";
            }
            else if (codePart == "null" && throwContext.IsAsync)
            {
                errorMessage =
$@"Task
    should not throw but threw
{expectedValue}";
            }
            else if (isExtensionMethod && !throwContext.IsAsync)
            {
                errorMessage = 
$@"`{codePart}()`
    should not throw but threw
{expectedValue}";
            }
            else if (isExtensionMethod && throwContext.IsAsync)
            {
                errorMessage =
$@"Task `{codePart}`
    should not throw but threw
{expectedValue}";
            }
            else
            {
                errorMessage = 
$@"`{codePart}`
    should not throw but threw
{expectedValue}";
            }
            
            errorMessage += $@"
    with message
""{throwContext.ExceptionMessage}""";

            return errorMessage;
        }
    }
}