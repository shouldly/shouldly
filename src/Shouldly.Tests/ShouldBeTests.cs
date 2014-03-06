using NUnit.Framework;

namespace Shouldly.Tests
{
    [TestFixture]
    public class ShouldBeTests
    {
        [Test]
        public void ShouldBeAssignableTo_ShouldNotThrowForStrings()
        {
            "Sup yo".ShouldBeAssignableTo(typeof(string));
        }

        [Test]
        public void ShouldBeOfType_ShouldNotThrowForStrings()
        {
            "Sup yo".ShouldBeOfType(typeof(string));
        }

        [Test]
        public void ShouldBeAssignableToWithGenericParameter_ShouldNotThrowForStrings()
        {
            "Sup yo".ShouldBeAssignableTo<string>();
        }

        [Test]
        public void ShouldBeOfTypeWithGenericParameter_ShouldNotThrowForStrings()
        {
            "Sup yo".ShouldBeOfType<string>();
        }

        [Test]
        public void ShouldBeAssignableToWithGenericParameter_ShouldReturnThis()
        {
            string str = "Sup yo";
            string ret = str.ShouldBeAssignableTo<string>();
            ret.ShouldBe(str);
        }

        [Test]
        public void ShouldBeOfTypeWithGenericParameter_ShouldReturnThis()
        {
            string str = "Sup yo";
            string ret = str.ShouldBeOfType<string>();
            ret.ShouldBe(str);
        }

        [Test]
        public void ShouldNotBeAssignableTo_ShouldNotThrowForNonMatchingType()
        {
            "Sup yo".ShouldNotBeAssignableTo(typeof(int));
        }

        [Test]
        public void ShouldNotBeOfType_ShouldNotThrowForNonMatchingType()
        {
            "Sup yo".ShouldNotBeOfType(typeof(int));
        }

        [Test]
        public void ShouldNotBeAssignableToWithGenericParameter_ShouldNotThrowForNonMatchingTypes()
        {
            "Sup yo".ShouldNotBeAssignableTo<int>();
        }

        [Test]
        public void ShouldNotBeOfTypeWithGenericParameter_ShouldNotThrowForNonMatchingTypes()
        {
            "Sup yo".ShouldNotBeOfType<int>();
        }

        [Test]
        public void ShouldNotBeOfType_TreatsNullAsNotMatchingAndDoesNotThrow()
        {
            object o = null;
            o.ShouldNotBeOfType<int>();
        }

        [Test]
        public void ShouldBeAssignableTo_ShouldNotThrowForInheritance()
        {
            new MyThing().ShouldBeAssignableTo<MyBase>();
        }

        [Test]
        public void ShouldBeOfType_ShouldNotThrowForSameType()
        {
            new MyThing().ShouldBeAssignableTo<MyThing>();
        }

        [Test]
        public void ShouldBe_WhenThingsAreDifferentTypes_ThatOverrideEqualsPoorly_ShouldThrow()
        {
            Shouldly.Should.Throw<ChuckedAWobbly>(() => new Strange().ShouldBe("hello"));
        }

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
