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
        public void ShouldBe_WhenTrue_ShouldNotThrow()
        {
            true.ShouldBe(true);
        }

        [Test]
        public void ShouldBe_ShouldNotThrow_WhenDifferentConvertableTypes()
        {
            const long number = 3;
            number.ShouldBe(3);
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
        public void ShouldBe_WithIEnumerablesOfDifferentCollectionTypes_ShouldNotThrow()
        {
            new List<int> { 1, 2, 3 }.ShouldBe(new[] { 1, 2, 3 });
        }

        [Test]
        public void ShouldBe_WithIEnumerablesOfDifferentOrder_ShouldNotThrow()
        {
            new List<int> { 1, 3, 2 }.ShouldBe(new[] { 1, 2, 3 }, ignoreOrder: true);
        }

        [Test]
        public void ShouldBe_WithIEnumerableOfString_ShouldNotThrow()
        {
            new []{"foo"}.ShouldBe<string>(new[]{"foo"});
        }

        [Test]
        public void ShouldBe_WithIEnumerablesOfDifferentOrder_WithMissingItems_ShouldThrow()
        {
            Should.Error(()=> 
                new List<int> { 1, 3 }.ShouldBe(new[] { 1, 2, 3 }, ignoreOrder: true), 
                "new List<int> { 1, 3 } should be [1, 2, 3] but was [1, 3] difference [1, *3*, *]");
            Should.Error(()=>
                new List<int> { 1, 3, 2 }.ShouldBe(new[] { 1, 3 }, ignoreOrder: true),
                "new List<int> { 1, 3, 2 } should be [1, 3] but was [1, 3, 2] difference [1, 3, *2*]");
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
