using NUnit.Framework;

namespace Shouldly.Tests
{
    [TestFixture]
    public class ShouldBeStringTests
    {
        [Test]
        public void ShouldBeCloseTo_IsPrettyLoose()
        {
            "Fun   with space   and \"quotes\"".ShouldBeCloseTo("Fun with space and 'quotes'");
        }

        [Test]
        public void ShouldStartWith_Should_Return_True_For_Ch_In_Cheese()
        {
            "Cheese".ShouldStartWith("Ch");
        }

        [Test]
        public void ShouldStartWith_Should_Ignore_Case_And_Return_True_For_CH_In_Cheese()
        {
            "Cheese".ShouldStartWith("CH");
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
        public void ShouldBe_WithoutCaseSpecification()
        {
            "Talk about, pop music".ShouldBe("Talk about, pop music");
        }

        [Test]
        public void ShouldBe_GivenCaseInsensitiveOptionAndStringsDifferingOnlyInCase_ShouldPass()
        {
            "SamplE".ShouldBe("sAMPLe", Case.Insensitive);
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