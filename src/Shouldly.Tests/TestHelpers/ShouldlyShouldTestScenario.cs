using Xunit;

namespace Shouldly.Tests.TestHelpers
{
    public abstract class ShouldlyShouldTestScenario : ShouldlyShouldFailureTestScenario
    {
        protected abstract void ShouldPass();

        [Fact]
        public void ShouldMethodShouldNotFail()
        {
            Should.NotError(ShouldPass);
        }
    }
}