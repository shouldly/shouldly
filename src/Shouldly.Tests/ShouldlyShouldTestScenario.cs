using NUnit.Framework;

namespace Shouldly.Tests
{
    [TestFixture]
    public abstract class ShouldlyShouldTestScenario
    {
        protected abstract void ShouldPass();
        protected abstract void ShouldThrowAWobbly();
        protected abstract string ChuckedAWobblyErrorMessage { get; }

        [Test]
        public void RunScenario()
        {
            Should.NotError(ShouldPass);
            Should.Error(ShouldThrowAWobbly, ChuckedAWobblyErrorMessage);
        }
    }
}