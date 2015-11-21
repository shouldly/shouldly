using Xunit;

namespace Shouldly.Tests.TestHelpers
{
    public abstract class ShouldlyShouldFailureTestScenario
    {
        protected abstract void ShouldThrowAWobbly();
        protected abstract string ChuckedAWobblyErrorMessage { get; }

        [Fact]
        public void ShouldMethodShouldThrowAWobbly()
        {
            Should.Error(ShouldThrowAWobbly, ChuckedAWobblyErrorMessage);
        }
    }
}