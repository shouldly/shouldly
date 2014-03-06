using NUnit.Framework;

namespace Shouldly.Tests
{
    [TestFixture]
    public class ShouldBeTests
    {
        [Test]
        public void ShouldBe_WhenGivenEqualArray_ShouldPass()
        {
            new[]{1,2,3,4}.ShouldBe(new []{1,2,3,4});
        }

        [Test]
        public void ShouldBe_WhenGivenEqualMultidimensionArray_ShouldPass()
        {
            new[,]{{"1","2"}, {"3", "4"}}.ShouldBe(new[,]{{"1","2"},{"3","4"}});
        }

        [Test]
        public void ShouldBe_WhenGivenNotEqualArrays_ShouldThrow()
        {
            Shouldly.Should.Throw<ChuckedAWobbly>(() => new[]{99,2,3,5}.ShouldBe(new []{1,2,3,4}));
        }

        [Test]
        public void ShouldBe_WhenGivenNotEqualMultidimensionArrays_ShouldThrow()
        {
            Shouldly.Should.Throw<ChuckedAWobbly>(() => new[,]{{"1","2"}, {"3", "5"}}.ShouldBe(new[,]{{"1","2"},{"3","4"}}));
        }
    }
}
