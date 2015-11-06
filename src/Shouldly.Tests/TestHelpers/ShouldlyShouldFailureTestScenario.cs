using System.Globalization;
using System.Threading;
using NUnit.Framework;

namespace Shouldly.Tests.TestHelpers
{
    [TestFixture]
    public abstract class ShouldlyShouldFailureTestScenario
    {
        protected abstract void ShouldThrowAWobbly();
        protected abstract string ChuckedAWobblyErrorMessage { get; }

        [TestFixtureSetUp]
        public void Setup()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
        }

        [Test]
        public void ShouldMethodShouldThrowAWobbly()
        {
            Should.Error(ShouldThrowAWobbly, ChuckedAWobblyErrorMessage);
        }
    }
}