using NUnit.Framework;

namespace Shouldly.Tests
{
    [TestFixture]
    public abstract class ShouldlyShouldFailTestScenario
    {
        protected abstract void ShouldThrowAWobbly();
        protected abstract string ChuckedAWobblyErrorMessage { get; }

        [Test]
        public void RunScenario()
        {
            Should.Error(ShouldThrowAWobbly, ChuckedAWobblyErrorMessage);
        }
    }
}