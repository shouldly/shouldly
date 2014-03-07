using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Shouldly.Tests
{
    public class EqualityComparerTests
    {
        /* 
         * Code heavily influenced by code from xunit assertion tests
         * at https://github.com/xunit/xunit/blob/master/test/test.xunit2.assert/Asserts/EqualityAssertsTests.cs
         */

        [Test]
        public void EqualityComparer_WhenGivenEqualLists_ShouldBeTrue()
        {
            var x = new List<object> { new List<object> { new List<object> { new List<object>() } } };
            var y = new List<object> { new List<object> { new List<object> { new List<object>() } } };

            EqualityComparer<List<object>> comparer = new EqualityComparer<List<object>>();
            comparer.Equals(x,y).ShouldBe(true);
        }

        [Test]
        public void EqualityComparer_WhenGivenNonComaprableObject_ShouldBeTrue()
        {
            NonComparableObject nco1 = new NonComparableObject();
            NonComparableObject nco2 = new NonComparableObject();

            EqualityComparer<NonComparableObject> comparer = new EqualityComparer<NonComparableObject>();
            comparer.Equals(nco1, nco2).ShouldBe(true);
        }

        [Test]
        public void EqualityComparer_WhenGivenComparableObject_ShouldBeTrue()
        {
            var co1 = new SpyComparable();
            var co2 = new SpyComparable();

            EqualityComparer<SpyComparable> comparer = new EqualityComparer<SpyComparable>();
            comparer.Equals(co1, co2).ShouldBe(true);
        }

        [Test]
        public void EqualityComparer_WhenGivenComparableGeneric_ShouldBeTrue()
        {
           SpyComparable_Generic co1 = new SpyComparable_Generic(); 
           SpyComparable_Generic co2 = new SpyComparable_Generic(); 

           EqualityComparer<SpyComparable_Generic> comparer = new EqualityComparer<SpyComparable_Generic>();
           comparer.Equals(co1,co2).ShouldBe(true);
           co1.CompareCalled.ShouldBe(true);
        }

        [Test]
        public void EqualityComparer_WhenGivenOverriddenEquatable_ShouldBeTrue()
        {
           SpyEquatable eq1 = new SpyEquatable(); 
           SpyEquatable eq2 = new SpyEquatable(); 

           EqualityComparer<SpyEquatable> comparer = new EqualityComparer<SpyEquatable>();
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
            public bool CompareCalled;

            public int CompareTo(object obj)
            {
                CompareCalled = true;
                return 0;
            }
        }

        class SpyComparable_Generic : IComparable<SpyComparable_Generic>
        {
            public bool CompareCalled;

            public int CompareTo(SpyComparable_Generic other)
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
