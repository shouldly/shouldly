using NUnit.Framework;

namespace Shouldly.Tests
{
    public class ShouldBeInRangeTests
    {
        [Test]
        public void ShouldNotThrowWhenInRange()
        {
            1.5m.ShouldBeInRange(1.4m, 1.6m);
        }

        [Test]
        public void ShouldThrowWhenOutOfRange()
        {
            Shouldly.Should.Throw<ChuckedAWobbly>(() => 1.5m.ShouldBeInRange(1.6m, 1.7m));
        }

        [Test]
        public void ErrorMessageIsNice()
        {
            Should.Error(
                () => 1.5m.ShouldBeInRange(1.6m, 1.7m),
                "() => 1.5m should be in range { from = 1.6, to = 1.7 } but was 1.5");
        }
    }
}
