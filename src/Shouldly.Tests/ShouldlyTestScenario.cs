using NUnit.Framework;

namespace Shouldly.Tests
{
    [TestFixture]
    public abstract class ShouldlyTestScenario
    {
        protected abstract void ShouldPass();
        protected abstract void ShouldThrowAWobbly();
        protected abstract string ChuckedAWobblyErrorMessage { get; }
        protected abstract void NotVersionShouldPass();
        protected abstract void NotVersionShouldThrowAWobbly();
        protected abstract string NotVersionChuckedAWobblyErrorMessage { get; }

        [Test]
        public void RunScenario()
        {
            Should.NotError(ShouldPass);
            Should.Error(ShouldThrowAWobbly, ChuckedAWobblyErrorMessage);
            Should.NotError(NotVersionShouldPass);
            Should.Error(NotVersionShouldThrowAWobbly, NotVersionChuckedAWobblyErrorMessage);
        }
    }
}