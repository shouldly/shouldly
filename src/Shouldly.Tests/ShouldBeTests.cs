using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Shouldly.Tests
{
    [TestFixture]
    public class ShouldBeTests
    {
        [Test]
        public void Should_WithNumbers_ShouldAllowTolerance()
        {
            Math.PI.ShouldNotBe(3.14);
            Math.PI.ShouldBe(3.14, 0.01);
            ((float)Math.PI).ShouldBe(3.14f, 0.01);
        }

        [Test]
        public void ShouldBe_GreaterThan()
        {
            7.ShouldBeGreaterThan(1);
            "b".ShouldBeGreaterThan("a");
            "b".ShouldBeGreaterThan(null);
            Shouldly.Should.Throw<ChuckedAWobbly>(() => 0.ShouldBeGreaterThan(7));
            Shouldly.Should.Throw<ChuckedAWobbly>(() => "a".ShouldBeGreaterThan("b"));
            Shouldly.Should.Throw<ChuckedAWobbly>(() => ((string)null).ShouldBeGreaterThan("b"));
        }

        [Test]
        public void ShouldBe_LessThan()
        {
            1.ShouldBeLessThan(7);
            "a".ShouldBeLessThan("b");
            ((string)null).ShouldBeLessThan("b");
            Shouldly.Should.Throw<ChuckedAWobbly>(() => 7.ShouldBeLessThan(0));
            Shouldly.Should.Throw<ChuckedAWobbly>(() => "b".ShouldBeLessThan("a"));
            Shouldly.Should.Throw<ChuckedAWobbly>(() => "b".ShouldBeLessThan(null));
        }

        [Test]
        public void ShouldBe_GreaterThanOrEqualTo()
        {
            7.ShouldBeGreaterThanOrEqualTo(1);
            1.ShouldBeGreaterThanOrEqualTo(1);
            Shouldly.Should.Throw<ChuckedAWobbly>(() => 0.ShouldBeGreaterThanOrEqualTo(1));
        }

        [Test]
        public void ShouldBe_LessThanOrEqualTo()
        {
            1.ShouldBeLessThanOrEqualTo(7);
            1.ShouldBeLessThanOrEqualTo(1);
            Shouldly.Should.Throw<ChuckedAWobbly>(() => 2.ShouldBeLessThanOrEqualTo(1));
        }

        [Test]
        public void ShouldBe_EnumerableOfDoubles_ShouldAllowTolerance()
        {
            new[] { Math.PI, Math.PI }.ShouldBe(new[] { 3.14, 3.14 }, 0.01);
        }

        [Test]
        public void ShouldBe_EnumerableOfFloats_ShouldAllowTolerance()
        {
            new[] { (float)Math.PI, (float)Math.PI }.ShouldBe(new[] { 3.14f, 3.14f }, 0.01);
        }

        [Test]
        public void Should_WithDecimal_ShouldAllowTolerance()
        {
            var pi = (decimal)Math.PI;
            pi.ShouldNotBe(3.14m);
            pi.ShouldBe(3.14m, 0.01m);
        }

        [Test]
        public void ShouldBe_DecimalEnumerable_ShouldAllowTolerance()
        {
            var firstSet = new[] { 1.23m, 2.34m, 3.45001m };
            var secondSet = new[] { 1.2301m, 2.34m, 3.45m };
            firstSet.ShouldBe(secondSet, 0.01m);
        }

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
        public void ShouldBe_ComparingObjectWithString_ShouldThrow()
        {
            Shouldly.Should.Throw<ChuckedAWobbly>(() => new object().ShouldBe("this string"));
        }

        [Test]
        public void ShouldBe_ComparingBaseWithDerived_ShouldThrow()
        {
            Shouldly.Should.Throw<ChuckedAWobbly>(() => new MyBase().ShouldBe(new MyThing()));
        }

        [Test]
        public void ShouldBe_WithIEnumerablesOfDifferentOrderWhenTypeNotComparable_ShouldNotThrow()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                new List<object> {new object(), new object()}.ShouldBe(new[] {new object(), new object()},
                    ignoreOrder: true));

            ex.Message.ShouldBe("At least one object must implement IComparable.");
        }

        class Strange : IEnumerable<Strange>
        {
            public IEnumerator<Strange> GetEnumerator()
            {
                return new List<Strange>().GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public static implicit operator Strange(string thing)
            {
                return new Strange();
            }
        }

        [Test]
        public void ShouldBe_WhenThingsAreDifferentTypes_ThatOverrideEqualsPoorly_ShouldThrow()
        {
            Shouldly.Should.Throw<ChuckedAWobbly>(() => new Strange().ShouldBe("hello"));
        }
    }
}
