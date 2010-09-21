using System;
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
        public void ShouldBe_WhenFalse_ShouldThrow()
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
        }

        [Test]
        public void ShouldBeTypeOf_ShouldNotThrowForStrings() {
            "Sup yo".ShouldBeTypeOf(typeof(string));
        }

        class MyBase{

        }
        class MyThing : MyBase {

        }


        [Test]
        public void ShouldBeTypeOf_ShouldNotThrowForInheritance() {
            new MyThing().ShouldBeTypeOf(typeof(MyBase));
        }

    }
}
