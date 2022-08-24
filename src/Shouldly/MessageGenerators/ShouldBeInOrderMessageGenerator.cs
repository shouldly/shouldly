namespace Shouldly.MessageGenerators;

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
    should be in {context.SortDirection.ToString().ToLower()} order but was not.
    The first out-of-order item was found at index {context.OutOfOrderIndex}:
{context.OutOfOrderObject}";
    }
}