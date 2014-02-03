using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NUnit.Framework;

namespace Shouldly.Tests
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
        public void ShouldNotBe_WhenTrue_ShouldNotThrow()
        {
            "this string".ShouldNotBe("some other string");
        }

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
            1.ShouldBeLessThan(7);
            Shouldly.Should.Throw<ChuckedAWobbly>(() => 7.ShouldBeLessThan(0));
            Shouldly.Should.Throw<ChuckedAWobbly>(() => 0.ShouldBeGreaterThan(7));
        }

        [Test]
        public void ShouldBe_GreaterThanOrEqualTo()
        {
            7.ShouldBeGreaterThanOrEqualTo(1);
            1.ShouldBeGreaterThanOrEqualTo(1);
            Shouldly.Should.Throw<ChuckedAWobbly>(() => 0.ShouldBeGreaterThanOrEqualTo(1));
        }

        [Test]
        public void ShouldBe_EnumerableOfDoubles_ShouldAllowTolerance()
        {
            new[] { Math.PI, Math.PI }.ShouldBe(new[] { 3.14, 3.14 }, 0.01);
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
        public void ShouldBeTypeOf_ShouldNotThrowForStrings()
        {
            "Sup yo".ShouldBeTypeOf(typeof(string));
        }

        [Test]
        public void ShouldBeTypeOfWithGenericParameter_ShouldNotThrowForStrings()
        {
            "Sup yo".ShouldBeTypeOf<string>();
        }

        [Test]
        public void ShouldBeTypeOfWithGenericParameter_ShouldReturnThis()
        {
            string str = "Sup yo";
            string ret = str.ShouldBeTypeOf<string>();
            ret.ShouldBe(str);
        }

        [Test]
        public void ShouldNotBeTypeOf_ShouldNotThrowForNonMatchingType()
        {
            "Sup yo".ShouldNotBeTypeOf(typeof(int));
        }

        [Test]
        public void ShouldNotBeTypeOfWithGenericParameter_ShouldNotThrowForNonMatchingTypes()
        {
            "Sup yo".ShouldNotBeTypeOf<int>();
        }

        class MyBase { }
        class MyThing : MyBase { }

        [Test]
        public void ShouldBeTypeOf_ShouldNotThrowForInheritance()
        {
            new MyThing().ShouldBeTypeOf<MyBase>();
        }
    }
}
