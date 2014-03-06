using NUnit.Framework;

namespace Shouldly.Tests
{
    [TestFixture]
    public class ShouldBeStringTests
    {
        [Test]
        public void ShouldContainWithoutWhitespace_IsPrettyLoose()
        {
            "Fun   with space   and \"quotes\"".ShouldContainWithoutWhitespace("Fun with space and 'quotes'");
        }

        [Test]
        public void ShouldEndWith_Should_Return_True_For_ez_In_Cheez()
        {
            "Cheez".ShouldEndWith("ez");
        }

        [Test]
        public void ShouldEndWith_Should_Ignore_Case_And_Return_True_For_EZ_In_Cheez()
        {
            "Cheez".ShouldEndWith("EZ");
        }

        [Test]
        public void ShouldMatch_Should_Match_Based_On_RegEx_Pattern()
        {
            "Cheese".ShouldMatch(@"C.e{2}s[e]");
        }

        [Test]
        public void ShouldBeNullOrEmpty_GivenAnEmptyString_ShouldPass()
        {
            "".ShouldBeNullOrEmpty();
        }

        [Test]
        public void ShouldBeNullOrEmpty_GivenANullString_ShouldPass()
        {
            string nullstring = null;
            nullstring.ShouldBeNullOrEmpty();
        }

        [Test]
        public void ShouldNotBeNullOrEmpty_GivenANonEmptyString_ShouldPass()
        {
            "a".ShouldNotBeNullOrEmpty();
        }
    }
}