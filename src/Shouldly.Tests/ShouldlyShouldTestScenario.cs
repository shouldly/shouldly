using NUnit.Framework;

namespace Shouldly.Tests
{
    [TestFixture]
    public abstract class ShouldlyShouldTestScenario : ShouldlyShouldFailureTestScenario
    {
        protected abstract void ShouldPass();

        [Test]
        public void ShouldMethodShouldNotFail()
        {
            TestHelpers.Should.NotError(ShouldPass);
        }
    }
}