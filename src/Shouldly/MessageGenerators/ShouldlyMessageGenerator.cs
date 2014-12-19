namespace Shouldly.MessageGenerators
{
    internal abstract class ShouldlyMessageGenerator
    {
        public abstract bool CanProcess(ShouldlyAssertionContext context);
        public abstract string GenerateErrorMessage(ShouldlyAssertionContext context);
    }
}