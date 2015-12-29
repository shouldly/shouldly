using System;
using System.Collections.Generic;
using Xunit;

namespace Shouldly.Tests.InternalTests
{
    public class EqualityComparerTests
    {
        /* 
         * Code heavily influenced by code from xunit assertion tests
         * at https://github.com/xunit/xunit/blob/master/test/test.xunit2.assert/Asserts/EqualityAssertsTests.cs
         */

        [Fact]
        public void EqualityComparer_WhenGivenEqualLists_ShouldBeTrue()
        {
            var x = new List<object> { new List<object> { new List<object> { new List<object>() } } };
            var y = new List<object> { new List<object> { new List<object> { new List<object>() } } };

            var comparer = new EqualityComparer<List<object>>();
            comparer.Equals(x,y).ShouldBe(true);
        }

        [Fact]
        public void EqualityComparer_WhenGivenNonComaprableObject_ShouldBeTrue()
        {
            var nco1 = new NonComparableObject();
            var nco2 = new NonComparableObject();

            var comparer = new EqualityComparer<NonComparableObject>();
            comparer.Equals(nco1, nco2).ShouldBe(true);
        }

        [Fact]
        public void EqualityComparer_WhenGivenComparableObject_ShouldBeTrue()
        {
            var co1 = new SpyComparable();
            var co2 = new SpyComparable();

            var comparer = new EqualityComparer<SpyComparable>();
            comparer.Equals(co1, co2).ShouldBe(true);
        }

        [Fact]
        public void EqualityComparer_WhenGivenComparableGeneric_ShouldBeTrue()
        {
           var co1 = new SpyComparableGeneric(); 
           var co2 = new SpyComparableGeneric(); 

           var comparer = new EqualityComparer<SpyComparableGeneric>();
           comparer.Equals(co1,co2).ShouldBe(true);
           co1.CompareCalled.ShouldBe(true);
        }

        [Fact]
        public void EqualityComparer_WhenGivenOverriddenEquatable_ShouldBeTrue()
        {
           var eq1 = new SpyEquatable(); 
           var eq2 = new SpyEquatable(); 

           var comparer = new EqualityComparer<SpyEquatable>();
           comparer.Equals(eq1,eq2).ShouldBe(true);
           eq1.EqualsCalled.ShouldBe(true);
           eq2.ShouldBeSameAs(eq1.EqualsOther);
        }

        class NonComparableObject
        {
            public override bool Equals(object obj)
            {
                return true;
            }

            public override int GetHashCode()
            {
                return 42;
            }
        }

        class SpyComparable : IComparable
        {
            public int CompareTo(object obj)
            {
                return 0;
            }
        }

        class SpyComparableGeneric : IComparable<SpyComparableGeneric>
        {
            public bool CompareCalled;

            public int CompareTo(SpyComparableGeneric other)
            {
                CompareCalled = true;
                return 0;
            }
        }

        public class SpyEquatable : IEquatable<SpyEquatable>
        {
            public bool EqualsCalled;
            public SpyEquatable EqualsOther;

            public bool Equals(SpyEquatable other)
            {
                EqualsCalled = true;
                EqualsOther = other;

                return true;
            }
        }
    }
}
