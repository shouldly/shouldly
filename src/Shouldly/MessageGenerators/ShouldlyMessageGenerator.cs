namespace Shouldly.MessageGenerators
{
    internal abstract class ShouldlyMessageGenerator
    {
        public abstract bool CanProcess(TestEnvironment environment);
        public abstract string GenerateErrorMessage(TestEnvironment environment);
    }
}