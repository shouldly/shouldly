namespace Shouldly.MessageGenerators
{
    internal abstract class ShouldlyMessageGenerator
    {
        public abstract bool CanProcess(IShouldlyAssertionContext context);
        public abstract string GenerateErrorMessage(IShouldlyAssertionContext context);
    }
}