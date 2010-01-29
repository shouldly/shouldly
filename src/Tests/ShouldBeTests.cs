using NUnit.Framework;
using Shouldly;

namespace Tests
{
    [TestFixture]
    public class ShouldBeTests
    {
        [Test]
        public void ShouldBe_WhenTrue_ShouldNotThrow()
        {
            true.ShouldBe(true);
        }

        [Test]
        public void ShouldBe_WhenFalse_ShouldThrow_WithAwesomeMessage()
        {
            Assert.Throws<AssertionException>(() =>
                "this string".ShouldBe("some other string"));
        }
    }
}
