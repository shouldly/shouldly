using NUnit.Framework;

namespace Shouldly.Tests
{
    [TestFixture]
    public abstract class ShouldlyShouldFailureTestScenario
    {
        protected abstract void ShouldThrowAWobbly();
        protected abstract string ChuckedAWobblyErrorMessage { get; }

        [Test]
        public void ShouldMethodShouldThrowAWobbly()
        {
            Should.Error(ShouldThrowAWobbly, ChuckedAWobblyErrorMessage);
        }
    }
}