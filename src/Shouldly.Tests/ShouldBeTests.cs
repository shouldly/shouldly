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
            Shouldly.Should.Throw<ChuckedAWobbly>(() => 7.ShouldBeLessThan(0));
            Shouldly.Should.Throw<ChuckedAWobbly>(() => 0.ShouldBeGreaterThan(7));
        }

        [Test]
        public void ShouldBe_GreaterThanOrEqualTo()
        {
            7.ShouldBeGreaterThanOrEqualTo(1);
            1.ShouldBeGreaterThanOrEqualTo(1);
            Shouldly.Should.Throw<Shouldly.ChuckedAWobbly>(() => 0.ShouldBeGreaterThanOrEqualTo(1));
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
        public void ShouldNotBeTypeOf_ShouldNotThrowForNonMatchingType()
        {
            "Sup yo".ShouldNotBeTypeOf(typeof(int));
        }

        [Test]
        public void ShouldNotBeTypeOfWithGenericParameter_ShouldNotThrowForNonMatchingTypes()
        {
            "Sup yo".ShouldNotBeTypeOf<int>();
        }

        [Test]
        public void ShouldBe_ShouldNotThrowWhenCalledOnANullEnumerableReference()
        {
            IEnumerable<int> something = null;
            Should.Error(
                () => something.ShouldBe(new[] { 1, 2, 3 }),
                "() => something should be [1, 2, 3] but was null");
        }

        class MyBase { }
        class MyThing : MyBase { }

        [Test]
        public void ShouldBeTypeOf_ShouldNotThrowForInheritance()
        {
            new MyThing().ShouldBeTypeOf<MyBase>();
        }

        [Test]
        public void ShouldBe_Action()
        {
            Action a = () => 1.ShouldBe(2);

            Should.Error(a,
                "Action a = () => 1 should be 2 but was 1");
        }

        [Test]
        public void ShouldBe_Expression()
        {
            Expression<Action> lambda = () => 1.ShouldBe(2);

            Should.Error(lambda.Compile(),
            "The provided expression should be 2 but was 1");
        }
    }
}
