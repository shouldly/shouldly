// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;
#if NET_2_0
using System.Collections.Generic;
#endif

namespace NUnit.Framework.Constraints
{
    #region ComparisonTest
    public abstract class ComparisonConstraintTest : ConstraintTestBaseWithArgumentException
    {
        protected ComparisonConstraint comparisonConstraint;

        [Test]
        public void UsesProvidedIComparer()
        {
            MyComparer comparer = new MyComparer();
            comparisonConstraint.Using(comparer).Matches(0);
            Assert.That(comparer.Called, "Comparer was not called");
        }

        class MyComparer : IComparer
        {
            public bool Called;

            public int Compare(object x, object y)
            {
                Called = true;
                return Comparer.Default.Compare(x, y);
            }
        }

#if NET_2_0
        [Test]
        public void UsesProvidedComparerOfT()
        {
            MyComparer<int> comparer = new MyComparer<int>();
            comparisonConstraint.Using(comparer).Matches(0);
            Assert.That(comparer.Called, "Comparer was not called");
        }

        class MyComparer<T> : IComparer<T>
        {
            public bool Called;

            public int Compare(T x, T y)
            {
                Called = true;
                return Comparer<T>.Default.Compare(x, y);
            }
        }

        [Test]
        public void UsesProvidedComparisonOfT()
        {
            MyComparison<int> comparer = new MyComparison<int>();
            comparisonConstraint.Using(new Comparison<int>(comparer.Compare)).Matches(0);
            Assert.That(comparer.Called, "Comparer was not called");
        }

        class MyComparison<T>
        {
            public bool Called;

            public int Compare(T x, T y)
            {
                Called = true;
                return Comparer<T>.Default.Compare(x, y);
            }
        }

#if CS_3_0
        [Test]
        public void UsesProvidedLambda()
        {
            Comparison<int> comparer = (x, y) => x.CompareTo(y);
            comparisonConstraint.Using(comparer).Matches(0);
        }
#endif
#endif
    }
    #endregion

    #region GreaterThan
    [TestFixture]
    public class GreaterThanTest : ComparisonConstraintTest
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = comparisonConstraint = new GreaterThanConstraint(5);
            expectedDescription = "greater than 5";
            stringRepresentation = "<greaterthan 5>";
        }

        object[] SuccessData = new object[] { 6, 5.001 };

        object[] FailureData = new object[] { 4, 5 };

        string[] ActualValues = new string[] { "4", "5" };

        object[] InvalidData = new object[] { null, "xxx" };

        [Test]
        public void CanCompareIComparables()
        {
            ClassWithIComparable expected = new ClassWithIComparable(0);
            ClassWithIComparable actual = new ClassWithIComparable(42);
            Assert.That(actual, Is.GreaterThan(expected));
        }

#if NET_2_0
        [Test]
        public void CanCompareIComparablesOfT()
        {
            ClassWithIComparableOfT expected = new ClassWithIComparableOfT(0);
            ClassWithIComparableOfT actual = new ClassWithIComparableOfT(42);
            Assert.That(actual, Is.GreaterThan(expected));
        }
#endif
    }
    #endregion

    #region GreaterThanOrEqual
    [TestFixture]
    public class GreaterThanOrEqualTest : ComparisonConstraintTest
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = comparisonConstraint = new GreaterThanOrEqualConstraint(5);
            expectedDescription = "greater than or equal to 5";
            stringRepresentation = "<greaterthanorequal 5>";
        }

        object[] SuccessData = new object[] { 6, 5 };

        object[] FailureData = new object[] { 4 };

        string[] ActualValues = new string[] { "4" };

        object[] InvalidData = new object[] { null, "xxx" };

        [Test]
        public void CanCompareIComparables()
        {
            ClassWithIComparable expected = new ClassWithIComparable(0);
            ClassWithIComparable actual = new ClassWithIComparable(42);
            Assert.That(actual, Is.GreaterThanOrEqualTo(expected));
        }

#if NET_2_0
        [Test]
        public void CanCompareIComparablesOfT()
        {
            ClassWithIComparableOfT expected = new ClassWithIComparableOfT(0);
            ClassWithIComparableOfT actual = new ClassWithIComparableOfT(42);
            Assert.That(actual, Is.GreaterThanOrEqualTo(expected));
        }
#endif
    }
    #endregion

    #region LessThan
    [TestFixture]
    public class LessThanTest : ComparisonConstraintTest
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = comparisonConstraint = new LessThanConstraint(5);
            expectedDescription = "less than 5";
            stringRepresentation = "<lessthan 5>";
        }

        object[] SuccessData = new object[] { 4, 4.999 };

        object[] FailureData = new object[] { 6, 5 };

        string[] ActualValues = new string[] { "6", "5" };

        object[] InvalidData = new object[] { null, "xxx" };

        [Test]
        public void CanCompareIComparables()
        {
            ClassWithIComparable expected = new ClassWithIComparable(42);
            ClassWithIComparable actual = new ClassWithIComparable(0);
            Assert.That(actual, Is.LessThan(expected));
        }

#if NET_2_0
        [Test]
        public void CanCompareIComparablesOfT()
        {
            ClassWithIComparableOfT expected = new ClassWithIComparableOfT(42);
            ClassWithIComparableOfT actual = new ClassWithIComparableOfT(0);
            Assert.That(actual, Is.LessThan(expected));
        }
#endif
    }
    #endregion

    #region LessThanOrEqual
    [TestFixture]
    public class LessThanOrEqualTest : ComparisonConstraintTest
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = comparisonConstraint = new LessThanOrEqualConstraint(5);
            expectedDescription = "less than or equal to 5";
            stringRepresentation = "<lessthanorequal 5>";
        }

        object[] SuccessData = new object[] { 4, 5 };

        object[] FailureData = new object[] { 6 };

        string[] ActualValues = new string[] { "6" };

        object[] InvalidData = new object[] { null, "xxx" };

        [Test]
        public void CanCompareIComparables()
        {
            ClassWithIComparable expected = new ClassWithIComparable(42);
            ClassWithIComparable actual = new ClassWithIComparable(0);
            Assert.That(actual, Is.LessThanOrEqualTo(expected));
        }

#if NET_2_0
        [Test]
        public void CanCompareIComparablesOfT()
        {
            ClassWithIComparableOfT expected = new ClassWithIComparableOfT(42);
            ClassWithIComparableOfT actual = new ClassWithIComparableOfT(0);
            Assert.That(actual, Is.LessThanOrEqualTo(expected));
        }
#endif
    }
    #endregion

    #region RangeConstraint
    [TestFixture]
    public class RangeConstraintTest : ConstraintTestBaseWithArgumentException
    {
        RangeConstraint rangeConstraint;

        [SetUp]
        public void SetUp()
        {
            theConstraint = rangeConstraint = new RangeConstraint(5, 42);
            expectedDescription = "in range (5,42)";
            stringRepresentation = "<range 5 42>";
        }

        object[] SuccessData = new object[] { 5, 23, 42 };

        object[] FailureData = new object[] { 4, 43 };

        string[] ActualValues = new string[] { "4", "43" };

        object[] InvalidData = new object[] { null, "xxx" };

        [Test]
        public void UsesProvidedIComparer()
        {
            MyComparer comparer = new MyComparer();
            Assert.That(rangeConstraint.Using(comparer).Matches(19));
            Assert.That(comparer.Called, "Comparer was not called");
        }

        class MyComparer : IComparer
        {
            public bool Called;

            public int Compare(object x, object y)
            {
                Called = true;
                return Comparer.Default.Compare(x, y);
            }
        }

#if NET_2_0
        [Test]
        public void UsesProvidedComparerOfT()
        {
            MyComparer<int> comparer = new MyComparer<int>();
            Assert.That(rangeConstraint.Using(comparer).Matches(19));
            Assert.That(comparer.Called, "Comparer was not called");
        }

        class MyComparer<T> : IComparer<T>
        {
            public bool Called;

            public int Compare(T x, T y)
            {
                Called = true;
                return Comparer<T>.Default.Compare(x, y);
            }
        }

        [Test]
        public void UsesProvidedComparisonOfT()
        {
            MyComparison<int> comparer = new MyComparison<int>();
            Assert.That(rangeConstraint.Using(new Comparison<int>(comparer.Compare)).Matches(19));
            Assert.That(comparer.Called, "Comparer was not called");
        }

        class MyComparison<T>
        {
            public bool Called;

            public int Compare(T x, T y)
            {
                Called = true;
                return Comparer<T>.Default.Compare(x, y);
            }
        }

#if CS_3_0
        [Test]
        public void UsesProvidedLambda()
        {
            Comparison<int> comparer = (x, y) => x.CompareTo(y);
            Assert.That(rangeConstraint.Using(comparer).Matches(19));
        }
#endif
#endif
    }
    #endregion

    #region Test Classes
    class ClassWithIComparable : IComparable
    {
        private int val;

        public ClassWithIComparable(int val)
        {
            this.val = val;
        }

        public int CompareTo(object x)
        {
            ClassWithIComparable other = x as ClassWithIComparable;
            if (x is ClassWithIComparable)
                return val.CompareTo(other.val);

            throw new ArgumentException();
        }
    }

#if NET_2_0
    class ClassWithIComparableOfT : IComparable<ClassWithIComparableOfT>
    {
        private int val;

        public ClassWithIComparableOfT(int val)
        {
            this.val = val;
        }

        public int CompareTo(ClassWithIComparableOfT other)
        {
            return val.CompareTo(other.val);
        }
    }
#endif
    #endregion
}