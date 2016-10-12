namespace Shouldly.MessageGenerators
{
    internal class ShouldBeInOrderMessageGenerator : ShouldlyMessageGenerator
    {
        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return context.ShouldMethod == "ShouldBeInOrder";
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            return
$@"{context.CodePart}
    should be in {context.Expected.ToString().ToLower()} order
    but item at index {context.Key} was not.";
        }
    }
}