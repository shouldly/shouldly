// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;
using NUnit.Framework;
using NUnit.Util;
using NUnit.TestUtilities;
using NUnit.TestData;

namespace NUnit.Core.Tests
{
	[TestFixture]
	public class TestMethodSignatureTests
	{
        private static Type fixtureType = typeof(TestMethodSignatureFixture);
		private Test fixture;

		[SetUp]
		public void CreateFixture()
		{
			fixture = TestBuilder.MakeFixture( typeof( TestMethodSignatureFixture ) );
		}

        [Test]
		public void InstanceTestMethodIsRunnable()
		{
			TestAssert.IsRunnable( fixtureType, "InstanceTestMethod" );
		}

		[Test]
		public void StaticTestMethodIsRunnable()
		{
			TestAssert.IsRunnable( fixtureType, "StaticTestMethod" );
		}

		[Test]
		public void TestMethodWithoutParametersWithArgumentsProvidedIsNotRunnable()
		{
			TestAssert.ChildNotRunnable(fixtureType, "TestMethodWithoutParametersWithArgumentsProvided");
		}

        [Test]
        public void TestMethodWithArgumentsNotProvidedIsNotRunnable()
        {
            TestAssert.IsNotRunnable(fixtureType, "TestMethodWithArgumentsNotProvided");
        }

        [Test]
        public void TestMethodWithArgumentsProvidedIsRunnable()
        {
            TestAssert.IsRunnable(fixtureType, "TestMethodWithArgumentsProvided");
        }

        [Test]
        public void TestMethodWithWrongNumberOfArgumentsProvidedIsNotRunnable()
        {
            TestAssert.ChildNotRunnable(fixtureType, "TestMethodWithWrongNumberOfArgumentsProvided");
        }

        [Test]
        public void TestMethodWithWrongArgumentTypesProvidedGivesError()
        {
            TestAssert.IsRunnable(fixtureType, "TestMethodWithWrongArgumentTypesProvided", ResultState.Error);
        }

        [Test]
        public void StaticTestMethodWithArgumentsNotProvidedIsNotRunnable()
        {
            TestAssert.IsNotRunnable(fixtureType, "StaticTestMethodWithArgumentsNotProvided");
        }

        [Test]
        public void StaticTestMethodWithArgumentsProvidedIsRunnable()
        {
            TestAssert.IsRunnable(fixtureType, "StaticTestMethodWithArgumentsProvided");
        }

        [Test]
        public void StaticTestMethodWithWrongNumberOfArgumentsProvidedIsNotRunnable()
        {
            TestAssert.ChildNotRunnable(fixtureType, "StaticTestMethodWithWrongNumberOfArgumentsProvided");
        }

        [Test]
        public void StaticTestMethodWithWrongArgumentTypesProvidedGivesError()
        {
            TestAssert.IsRunnable(fixtureType, "StaticTestMethodWithWrongArgumentTypesProvided", ResultState.Error);
        }

        [Test]
        public void TestMethodWithConvertibleArgumentsIsRunnable()
        {
            TestAssert.IsRunnable(fixtureType, "TestMethodWithConvertibleArguments");
        }

        [Test]
        public void TestMethodWithNonConvertibleArgumentsGivesError()
        {
            TestAssert.IsRunnable(fixtureType, "TestMethodWithNonConvertibleArguments", ResultState.Error);
        }

        [Test]
		public void ProtectedTestMethodIsNotRunnable()
		{
			TestAssert.IsNotRunnable( fixtureType, "ProtectedTestMethod" );
		}

		[Test]
		public void PrivateTestMethodIsNotRunnable()
		{
			TestAssert.IsNotRunnable( fixtureType, "PrivateTestMethod" );
		}

		[Test]
		public void TestMethodWithReturnTypeIsNotRunnable()
		{
			TestAssert.IsNotRunnable( fixtureType, "TestMethodWithReturnType" );
		}

		[Test]
		public void TestMethodWithMultipleTestCasesExecutesMultipleTimes()
		{
			Test test = TestFinder.Find( "TestMethodWithMultipleTestCases", fixture, false );
			Assert.That( test.RunState, Is.EqualTo( RunState.Runnable ) );
            TestResult result = test.Run(NullListener.NULL, TestFilter.Empty);
			Assert.That( result.ResultState, Is.EqualTo(ResultState.Success) );
            ResultSummarizer summary = new ResultSummarizer(result);
		    Assert.That(summary.TestsRun, Is.EqualTo(3));
		}

        [Test]
        public void TestMethodWithMultipleTestCasesUsesCorrectNames()
        {
            string name = "TestMethodWithMultipleTestCases";
            string fullName = typeof (TestMethodSignatureFixture).FullName + "." + name;
            TestSuite suite = (TestSuite)TestFinder.Find(name, fixture, false);
            Assert.That(suite.TestCount, Is.EqualTo(3));

            ArrayList names = new ArrayList();
            ArrayList fullNames = new ArrayList();

            foreach (Test test in suite.Tests)
            {
                names.Add(test.TestName.Name);
                fullNames.Add(test.TestName.FullName);
            }

            Assert.That(names, Has.Member(name + "(12,3,4)"));
            Assert.That(names, Has.Member(name + "(12,2,6)"));
            Assert.That(names, Has.Member(name + "(12,4,3)"));

            Assert.That(fullNames, Has.Member(fullName + "(12,3,4)"));
            Assert.That(fullNames, Has.Member(fullName + "(12,2,6)"));
            Assert.That(fullNames, Has.Member(fullName + "(12,4,3)"));
        }

        [Test]
        public void TestMethodWithParamsArgumentIsRunnable()
        {
            TestAssert.IsRunnable(fixtureType, "TestMethodWithParamsArgument");
        }

        // TODO: Make this work in a later release
        //[Test]
        public void TestMethodWithParamsArgumentUsesCorrectName()
        {
            string name = "TestMethodWithParamsArgument";
            string fullName = typeof(TestMethodSignatureFixture).FullName + "." + name;
            TestSuite suite = (TestSuite)TestFinder.Find(name, fixture, false);
            Test test = suite.Tests[0] as Test;

            Assert.That(test.TestName.Name, Is.EqualTo(name + "(1, 2, 3)"));
            Assert.That(test.TestName.FullName, Is.EqualTo(fullName + "(1, 2, 3)"));
        }

#if CS_3_0
        [TestCase()]
#endif
        [TestCase(@"one")]
        [TestCase(@"one", @"two")]
        [TestCase(@"one", @"two", @"three")]
        [TestCase(@"one", @"two", @"three", @"four")]
        public void TestParams(params string[] values) 
        {
            Assert.That(values.Length <= 4);
        }

        [Test]
        public void RunningTestsThroughFixtureGivesCorrectResults()
		{
            TestResult result = fixture.Run(NullListener.NULL, TestFilter.Empty);
			ResultSummarizer summary = new ResultSummarizer( result );

			Assert.That( 
				summary.ResultCount, 
				Is.EqualTo( TestMethodSignatureFixture.Tests ) );
			Assert.That( 
				summary.TestsRun, 
				Is.EqualTo( TestMethodSignatureFixture.Runnable ) );
			Assert.That( 
				summary.NotRunnable, 
				Is.EqualTo( TestMethodSignatureFixture.NotRunnable ) );
            Assert.That(
                summary.Errors,
                Is.EqualTo(TestMethodSignatureFixture.Errors));
            Assert.That(
                summary.Failures,
                Is.EqualTo(TestMethodSignatureFixture.Failures));
            Assert.That( 
				summary.TestsNotRun, 
				Is.EqualTo( TestMethodSignatureFixture.NotRunnable ) );
		}
    }
}
