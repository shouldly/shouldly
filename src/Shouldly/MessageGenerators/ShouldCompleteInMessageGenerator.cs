namespace Shouldly.MessageGenerators;

class ShouldCompleteInMessageGenerator : ShouldlyMessageGenerator
{
    public override bool CanProcess(IShouldlyAssertionContext context) =>
        context.Timeout.HasValue;

    public override string GenerateErrorMessage(IShouldlyAssertionContext context) =>
        $"""

         {context.Expected}
             should complete in
         {context.Timeout}
             but did not
         """;
}