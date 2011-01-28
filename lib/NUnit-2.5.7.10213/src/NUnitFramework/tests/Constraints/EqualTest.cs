// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System;
using System.IO;
using System.Drawing;
using System.Collections;
#if NET_2_0
using System.Collections.Generic;
#endif

namespace NUnit.Framework.Constraints
{
    [TestFixture]
    public class EqualConstraintTest : ConstraintTestBase
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = new EqualConstraint(4);
            expectedDescription = "4";
            stringRepresentation = "<equal 4>";
        }

        object[] SuccessData = new object[] { 4, 4.0f, 4.0d, 4.0000m };
            
        object[] FailureData = new object[] { 5, null, "Hello", double.NaN, double.PositiveInfinity };

        string[] ActualValues = new string[] { "5", "null", "\"Hello\"", "NaN", "Infinity" };

        [TestCase(double.NaN)]
        [TestCase(double.PositiveInfinity)]
        [TestCase(double.NegativeInfinity)]
        [TestCase(float.NaN)]
        [TestCase(float.PositiveInfinity)]
        [TestCase(float.NegativeInfinity)]
        public void CanMatchSpecialFloatingPointValues(object value)
        {
            Assert.That(value, new EqualConstraint(value));
        }

        #region DateTime Tests
        [Test]
        public void CanMatchDates()
        {
            DateTime expected = new DateTime(2007, 4, 1);
            DateTime actual = new DateTime(2007, 4, 1);
            Assert.That(actual, new EqualConstraint(expected));
        }

        [Test]
        public void CanMatchDatesWithinTimeSpan()
        {
            DateTime expected = new DateTime(2007, 4, 1, 13, 0, 0);
            DateTime actual = new DateTime(2007, 4, 1, 13, 1, 0);
            TimeSpan tolerance = TimeSpan.FromMinutes(5.0);
            Assert.That(actual, new EqualConstraint(expected).Within(tolerance));
        }

        [Test]
        public void CanMatchDatesWithinDays()
        {
            DateTime expected = new DateTime(2007, 4, 1, 13, 0, 0);
            DateTime actual = new DateTime(2007, 4, 4, 13, 0, 0);
            Assert.That(actual, new EqualConstraint(expected).Within(5).Days);
        }

        [Test]
        public void CanMatchDatesWithinHours()
        {
            DateTime expected = new DateTime(2007, 4, 1, 13, 0, 0);
            DateTime actual = new DateTime(2007, 4, 1, 16, 0, 0);
            Assert.That(actual, new EqualConstraint(expected).Within(5).Hours);
        }

        [Test]
        public void CanMatchDatesWithinMinutes()
        {
            DateTime expected = new DateTime(2007, 4, 1, 13, 0, 0);
            DateTime actual = new DateTime(2007, 4, 1, 13, 1, 0);
            Assert.That(actual, new EqualConstraint(expected).Within(5).Minutes);
        }

        [Test]
        public void CanMatchTimeSpanWithinMinutes()
        {
            TimeSpan expected = new TimeSpan( 10, 0, 0);
            TimeSpan actual = new TimeSpan(10, 2, 30);
            Assert.That(actual, new EqualConstraint(expected).Within(5).Minutes);
        }

        [Test]
        public void CanMatchDatesWithinSeconds()
        {
            DateTime expected = new DateTime(2007, 4, 1, 13, 0, 0);
            DateTime actual = new DateTime(2007, 4, 1, 13, 1, 0);
            Assert.That(actual, new EqualConstraint(expected).Within(300).Seconds);
        }

        [Test]
        public void CanMatchDatesWithinMilliseconds()
        {
            DateTime expected = new DateTime(2007, 4, 1, 13, 0, 0);
            DateTime actual = new DateTime(2007, 4, 1, 13, 1, 0);
            Assert.That(actual, new EqualConstraint(expected).Within(300000).Milliseconds);
        }

        [Test]
        public void CanMatchDatesWithinTicks()
        {
            DateTime expected = new DateTime(2007, 4, 1, 13, 0, 0);
            DateTime actual = new DateTime(2007, 4, 1, 13, 1, 0);
            Assert.That(actual, new EqualConstraint(expected).Within(TimeSpan.TicksPerMinute*5).Ticks);
        }

        #endregion

        #region Dictionary Tests
        // TODO: Move these to a separate fixture
#if CS_3_0
        [Test]
        public void CanMatchHashtables_SameOrder()
        {
            Assert.AreEqual(new Hashtable { { 0, 0 }, { 1, 1 }, { 2, 2 } },
                            new Hashtable { { 0, 0 }, { 1, 1 }, { 2, 2 } });
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void CanMatchHashtables_Failure()
        {
            Assert.AreEqual(new Hashtable { { 0, 0 }, { 1, 1 }, { 2, 2 } },
                            new Hashtable { { 0, 0 }, { 1, 5 }, { 2, 2 } });
        }

        [Test]
        public void CanMatchHashtables_DifferentOrder()
        {
            Assert.AreEqual(new Hashtable { { 0, 0 }, { 1, 1 }, { 2, 2 } },
                            new Hashtable { { 0, 0 }, { 2, 2 }, { 1, 1 } });
        }

#if NET_2_0
        [Test]
        public void CanMatchDictionaries_SameOrder()
        {
            Assert.AreEqual(new Dictionary<int, int> { { 0, 0 }, { 1, 1 }, { 2, 2 } },
                            new Dictionary<int, int> { { 0, 0 }, { 1, 1 }, { 2, 2 } });
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void CanMatchDictionaries_Failure()
        {
            Assert.AreEqual(new Dictionary<int, int> { { 0, 0 }, { 1, 1 }, { 2, 2 } },
                            new Dictionary<int, int> { { 0, 0 }, { 1, 5 }, { 2, 2 } });
        }

        [Test]
        public void CanMatchDictionaries_DifferentOrder()
        {
            Assert.AreEqual(new Dictionary<int, int> { { 0, 0 }, { 1, 1 }, { 2, 2 } },
                            new Dictionary<int, int> { { 0, 0 }, { 2, 2 }, { 1, 1 } });
        }

        [Test]
        public void CanMatchHashtableWithDictionary()
        {
            Assert.AreEqual(new Hashtable { { 0, 0 }, { 1, 1 }, { 2, 2 } },
                            new Dictionary<int, int> { { 0, 0 }, { 2, 2 }, { 1, 1 } });
        }
#endif
#endif
        #endregion

        [TestCase(20000000000000004.0)]
        [TestCase(19999999999999996.0)]
        public void CanMatchDoublesWithUlpTolerance(object value)
        {
          Assert.That(value, new EqualConstraint(20000000000000000.0).Within(1).Ulps);
        }

        [ExpectedException(typeof(AssertionException))]
        [TestCase(20000000000000008.0)]
        [TestCase(19999999999999992.0)]
        public void FailsOnDoublesOutsideOfUlpTolerance(object value)
        {
          Assert.That(value, new EqualConstraint(20000000000000000.0).Within(1).Ulps);
        }

        [TestCase(19999998.0f)]
        [TestCase(20000002.0f)]
        public void CanMatchSinglesWithUlpTolerance(object value)
        {
          Assert.That(value, new EqualConstraint(20000000.0f).Within(1).Ulps);
        }

        [ExpectedException(typeof(AssertionException))]
        [TestCase(19999996.0f)]
        [TestCase(20000004.0f)]
        public void FailsOnSinglesOutsideOfUlpTolerance(object value)
        {
          Assert.That(value, new EqualConstraint(20000000.0f).Within(1).Ulps);
        }

        [TestCase(9500.0)]
        [TestCase(10000.0)]
        [TestCase(10500.0)]
        public void CanMatchDoublesWithRelativeTolerance(object value)
        {
            Assert.That(value, new EqualConstraint(10000.0).Within(10.0).Percent);
        }

        [ExpectedException(typeof(AssertionException))]
        [TestCase(8500.0)]
        [TestCase(11500.0)]
        public void FailsOnDoublesOutsideOfRelativeTolerance(object value)
        {
            Assert.That(value, new EqualConstraint(10000.0).Within(10.0).Percent);
        }

        [TestCase(9500.0f)]
        [TestCase(10000.0f)]
        [TestCase(10500.0f)]
        public void CanMatchSinglesWithRelativeTolerance(object value)
        {
            Assert.That(value, new EqualConstraint(10000.0f).Within(10.0f).Percent);
        }

        [ExpectedException(typeof(AssertionException))]
        [TestCase(8500.0f)]
        [TestCase(11500.0f)]
        public void FailsOnSinglesOutsideOfRelativeTolerance(object value)
        {
            Assert.That(value, new EqualConstraint(10000.0f).Within(10.0f).Percent);
        }

        /// <summary>Applies both the Percent and Ulps modifiers to cause an exception</summary>
        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void ErrorWithPercentAndUlpsToleranceModes()
        {
            EqualConstraint shouldFail = new EqualConstraint(100.0f).Within(10.0f).Percent.Ulps;
        }

        /// <summary>Applies both the Ulps and Percent modifiers to cause an exception</summary>
        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void ErrorWithUlpsAndPercentToleranceModes() {
          EqualConstraint shouldFail = new EqualConstraint(100.0f).Within(10.0f).Ulps.Percent;
        }

        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void ErrorIfPercentPrecedesWithin()
        {
            Assert.That(1010, Is.EqualTo(1000).Percent.Within(5));
        }

        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void ErrorIfUlpsPrecedesWithin()
        {
            Assert.That(1010.0, Is.EqualTo(1000.0).Ulps.Within(5));
        }

        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void ErrorIfDaysPrecedesWithin()
        {
            Assert.That(DateTime.Now, Is.EqualTo(DateTime.Now).Days.Within(5));
        }

        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void ErrorIfHoursPrecedesWithin()
        {
            Assert.That(DateTime.Now, Is.EqualTo(DateTime.Now).Hours.Within(5));
        }

        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void ErrorIfMinutesPrecedesWithin()
        {
            Assert.That(DateTime.Now, Is.EqualTo(DateTime.Now).Minutes.Within(5));
        }

        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void ErrorIfSecondsPrecedesWithin()
        {
            Assert.That(DateTime.Now, Is.EqualTo(DateTime.Now).Seconds.Within(5));
        }

        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void ErrorIfMillisecondsPrecedesWithin()
        {
            Assert.That(DateTime.Now, Is.EqualTo(DateTime.Now).Milliseconds.Within(5));
        }

        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void ErrorIfTicksPrecedesWithin()
        {
            Assert.That(DateTime.Now, Is.EqualTo(DateTime.Now).Ticks.Within(5));
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestCase(1000)]
        [TestCase(1000U)]
        [TestCase(1000L)]
        [TestCase(1000UL)]
        public void ErrorIfUlpsIsUsedOnIntegralType(object x)
        {
            Assert.That(x, Is.EqualTo(x).Within(2).Ulps);
        }

        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void ErrorIfUlpsIsUsedOnDecimal()
        {
            Assert.That(100m, Is.EqualTo(100m).Within(2).Ulps);
        }

        [Test]
        public void UsesProvidedIComparer()
        {
            MyComparer comparer = new MyComparer();
            Assert.That(2 + 2, Is.EqualTo(4).Using(comparer));
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
        public void UsesProvidedEqualityComparer()
        {
            MyEqualityComparer comparer = new MyEqualityComparer();
            Assert.That(2 + 2, Is.EqualTo(4).Using(comparer));
            Assert.That(comparer.Called, "Comparer was not called");
        }

        class MyEqualityComparer : IEqualityComparer
        {
            public bool Called;

            bool IEqualityComparer.Equals(object x, object y)
            {
                Called = true;
                return Comparer.Default.Compare(x, y) == 0;
            }

            int IEqualityComparer.GetHashCode(object x)
            {
                return x.GetHashCode();
            }
        }

        [Test]
        public void UsesProvidedEqualityComparerOfT()
        {
            MyEqualityComparerOfT<int> comparer = new MyEqualityComparerOfT<int>();
            Assert.That(2 + 2, Is.EqualTo(4).Using(comparer));
            Assert.That(comparer.Called, "Comparer was not called");
        }

        class MyEqualityComparerOfT<T> : IEqualityComparer<T>
        {
            public bool Called;

            bool IEqualityComparer<T>.Equals(T x, T y)
            {
                Called = true;
                return Comparer<T>.Default.Compare(x, y) == 0;
            }

            int IEqualityComparer<T>.GetHashCode(T x)
            {
                return x.GetHashCode();
            }
        }

        [Test]
        public void UsesProvidedComparerOfT()
        {
            MyComparer<int> comparer = new MyComparer<int>();
            Assert.That( 2+2, Is.EqualTo(4).Using(comparer));
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
            Assert.That(2 + 2, Is.EqualTo(4).Using(new Comparison<int>(comparer.Compare)));
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
        public void UsesProvidedLambda_IntArgs()
        {
            Assert.That(2 + 2, Is.EqualTo(4).Using<int>( (x, y) => x.CompareTo(y) ) );
        }

        [Test]
        public void UsesProvidedLambda_StringArgs()
        {
            Assert.That("hello", Is.EqualTo("HELLO").Using<string>((x,y) => String.Compare(x, y, true)));
        }
#endif
#endif
    }

    [TestFixture]
    public class EqualTest : IExpectException
    {

        [Test, ExpectedException(typeof(AssertionException))]
        public void FailedStringMatchShowsFailurePosition()
        {
            Assert.That( "abcdgfe", new EqualConstraint( "abcdefg" ) );
        }

        static readonly string testString = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        [Test, ExpectedException(typeof(AssertionException))]
        public void LongStringsAreTruncated()
        {
            string expected = testString;
            string actual = testString.Replace('k', 'X');

            Assert.That(actual, new EqualConstraint(expected));
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void LongStringsAreTruncatedAtBothEndsIfNecessary()
        {
            string expected = testString;
            string actual = testString.Replace('Z', '?');

            Assert.That(actual, new EqualConstraint(expected));
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void LongStringsAreTruncatedAtFrontEndIfNecessary()
        {
            string expected = testString;
            string actual = testString  + "+++++";

            Assert.That(actual, new EqualConstraint(expected));
        }

//        [Test]
//        public void NamedAndUnnamedColorsCompareAsEqual()
//        {
//            EqualConstraint.SetConstraintForType(typeof(Color), typeof(SameColorAs));
//            Assert.That(System.Drawing.Color.Red,
//                Is.EqualTo(System.Drawing.Color.FromArgb(255, 0, 0)));
//        }

        public void HandleException(Exception ex)
        {
            StringReader rdr = new StringReader(ex.Message);
            /* skip */ rdr.ReadLine();
            string expected = rdr.ReadLine();
            if (expected != null && expected.Length > 11)
                expected = expected.Substring(11);
            string actual = rdr.ReadLine();
            if (actual != null && actual.Length > 11)
                actual = actual.Substring(11);
            string line = rdr.ReadLine();
            Assert.That(line, new NotConstraint(new EqualConstraint(null)), "No caret line displayed");
            int caret = line.Substring(11).IndexOf('^');

            int minLength = Math.Min(expected.Length, actual.Length);
            int minMatch = Math.Min(caret, minLength);

            if (caret != minLength)
            {
                if (caret > minLength ||
                    expected.Substring(0, minMatch) != actual.Substring(0, minMatch) ||
                    expected[caret] == actual[caret])
                    Assert.Fail("Message Error: Caret does not point at first mismatch..." + Environment.NewLine + ex.Message);
            }

            if (expected.Length > 68 || actual.Length > 68 || caret > 68)
                Assert.Fail("Message Error: Strings are not truncated..." + Environment.NewLine + ex.Message);
        }

        public class SameColorAs : Constraint
        {
            private Color expectedColor;

            public SameColorAs(Color expectedColor)
            {
                this.expectedColor = expectedColor;
            }

            public override bool Matches(object actual)
            {
                this.actual = actual;
                return actual is Color && ((Color)actual).ToArgb() == expectedColor.ToArgb();
            }

            public override void WriteDescriptionTo(MessageWriter writer)
            {
                writer.WriteExpectedValue( "same color as " + expectedColor );
            }
        }

#if NET_2_0
        [Test]
        public void TestPropertyWithPrivateSetter()
        {
            SomeClass obj = new SomeClass();
            Assert.That(obj.BrokenProp, Is.EqualTo(string.Empty));
        }

        private class SomeClass
        {
            public string BrokenProp
            {
                get { return string.Empty; }
                private set { }
            }
        }
#endif
    }
}