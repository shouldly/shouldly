using NUnit.Framework;

namespace Shouldly.Tests
{
    public class ShouldNotBeInRangeTests
    {
        [Test]
        public void ShouldThrowWhenInRange()
        {
            Shouldly.Should.Throw<ChuckedAWobbly>(() => 1.5m.ShouldNotBeInRange(1.4m, 1.6m));
        }

        [Test]
        public void ShouldNotThrowWhenOutOfRange()
        {
            1.5m.ShouldNotBeInRange(1.6m, 1.7m);
        }

        [Test]
        public void ErrorMessageIsNice()
        {
            TestHelpers.Should.Error(
                () => 1.5m.ShouldNotBeInRange(1.4m, 1.6m),
                "() => 1.5m should not be in range { from = 1.4, to = 1.6 } but was 1.5");
        }
    }
}
