// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;
using NUnit.TestUtilities;
using NUnit.TestData;

namespace NUnit.Core.Tests
{
    public class TheoryTests
    {
        static Type fixtureType = typeof(TheoryFixture);

        [Test]
        public void TheoryWithNoArgumentsIsTreatedAsTest()
        {
            TestAssert.IsRunnable(fixtureType, "TheoryWithNoArguments");
        }

        [Test]
        public void TheoryWithNoDatapointsIsNotRunnable()
        {
            TestAssert.IsNotRunnable(fixtureType, "TheoryWithArgumentsButNoDatapoints");
        }

        [Test]
        public void BooleanArgumentsAreSuppliedAutomatically()
        {
            Test test = TestBuilder.MakeTestCase(fixtureType, "TestWithBooleanArguments");
            TestAssert.IsRunnable(test);
            Assert.That(test.TestCount, Is.EqualTo(4));
        }

        [Theory]
        public void NullDatapointIsOK(object o)
        {
        }

        [Datapoint]
        internal object objData = null;

        [Test]
        public void EnumArgumentsAreSuppliedAutomatically()
        {
            Test test = TestBuilder.MakeTestCase(fixtureType, "TestWithEnumAsArgument");
            TestAssert.IsRunnable(test);
            Assert.That(test.TestCount, Is.EqualTo(3));
        }

        [Test]
        public void TheoryWithDatapointsIsRunnable()
        {
            Test test = TestBuilder.MakeTestCase(fixtureType, "TheoryWithArgumentsAndDatapoints");
            TestAssert.IsRunnable(test);
            Assert.That(test.TestCount, Is.EqualTo(9));
        }

        [Theory]
        public void SquareRootWithAllGoodValues(
            [Values(12.0, 4.0, 9.0)] double d)
        {
            SquareRootTest(d);
        }

        [Theory]
        public void SquareRootWithOneBadValue(
            [Values(12.0, -4.0, 9.0)] double d)
        {
            SquareRootTest(d);
        }

        [Datapoints]
        internal string[] vals = new string[] { "xyz1", "xyz2", "xyz3" };

        [Theory]
        public void ArrayWithDatapointsAttributeIsUsed(string s)
        {
            Assert.That(s.StartsWith("xyz"));
        }

        //[Datapoint]
        //public double[,] Array2X2 = new double[,] { { 1, 0 }, { 0, 1 } };

        //[Theory]
        //public void TestForArbitraryArray<T>(T[,] array)
        //{
        //}


        private void SquareRootTest(double d)
        {
            Assume.That(d > 0);
            double root = Math.Sqrt(d);
            Assert.That(root * root, Is.EqualTo(d).Within(0.000001));
            Assert.That(root > 0);
        }

        [Test]
        public void SimpleTestIgnoresDataPoints()
        {
            Test test = TestBuilder.MakeTestCase(fixtureType, "TestWithArguments");
            Assert.That(test.TestCount, Is.EqualTo(2));
        }

        [Theory]
        public void TheoryFailsIfAllTestsAreInconclusive()
        {
            TestResult result = TestBuilder.RunTestCase(fixtureType, "TestWithAllBadValues");
            Assert.That(result.ResultState, Is.EqualTo(ResultState.Failure));
            Assert.That(result.Message, Is.EqualTo("All test cases were inconclusive"));
        }

        public class SqrtTests_DisplayResults
        {
            [Datapoints]
            public double[] values = new double[] { 0.0, 1.0, -1.0, double.MaxValue, double.PositiveInfinity };

            [Theory]
            public void SqrtTimesItselfGivesOriginal(double num)
            {
                Assume.That(num >= 0.0 && num < double.MaxValue);

                double sqrt = Math.Sqrt(num);

                Assert.That(sqrt >= 0.0);
                Assert.That(sqrt * sqrt, Is.EqualTo(num).Within(0.000001));
            }
        }
    }
}
