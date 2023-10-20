namespace Shouldly.MessageGenerators;

abstract class ShouldlyMessageGenerator
{
    public abstract bool CanProcess(IShouldlyAssertionContext context);
    public abstract string GenerateErrorMessage(IShouldlyAssertionContext context);
}