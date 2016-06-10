namespace Shouldly.MessageGenerators
{
    internal class ShouldCompleteInMessageGenerator : ShouldlyMessageGenerator
    {
        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return context.Timeout.HasValue;
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
        return $@"
{context.Expected}
    should complete in
{context.Timeout}
    but did not";
        }
    }
}