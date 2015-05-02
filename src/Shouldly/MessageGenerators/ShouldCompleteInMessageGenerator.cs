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
            return string.Format(@"
    {0}
        should complete in
    {1}
        but did not", context.Expected, context.Timeout);
        }
    }
}