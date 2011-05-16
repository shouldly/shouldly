// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.Reflection;
using System.Collections;
using NUnit.Framework;
using NUnit.Core.Builders;
using NUnit.TestUtilities;
using NUnit.TestData.AttributeDescriptionFixture;

namespace NUnit.Core.Tests
{
	// TODO: Review to see if we need these tests

	[TestFixture]
	public class TestAttributeFixture
	{
		static readonly Type FixtureType = typeof( MockFixture );

		[Test]
		public void ReflectionTest()
		{
			Test testCase = TestBuilder.MakeTestCase( FixtureType, "Method" );
			Assert.AreEqual( RunState.Runnable, testCase.RunState );
		}

        [Test]
        public void Description()
        {
            Test testCase = TestBuilder.MakeTestCase(FixtureType, "Method");
            Assert.AreEqual("Test Description", testCase.Description);
        }

        [Test]
        public void DescriptionInResult()
        {
            TestSuite suite = new TestSuite("Mock Fixture");
            suite.Add(TestBuilder.MakeFixture(typeof(MockFixture)));
            TestResult result = suite.Run(NullListener.NULL, TestFilter.Empty);

            TestResult caseResult = TestFinder.Find("Method", result, true);
            Assert.AreEqual("Test Description", caseResult.Description);

            caseResult = TestFinder.Find("NoDescriptionMethod", result, true);
            Assert.IsNull(caseResult.Description);
        }

        [Test]
		public void NoDescription()
		{
			Test testCase = TestBuilder.MakeTestCase( FixtureType, "NoDescriptionMethod" );
			Assert.IsNull(testCase.Description);
		}

		[Test]
		public void FixtureDescription()
		{
			TestSuite suite = new TestSuite("suite");
			suite.Add( TestBuilder.MakeFixture( typeof( MockFixture ) ) );

			IList tests = suite.Tests;
			TestSuite mockFixtureSuite = (TestSuite)tests[0];

			Assert.AreEqual("Fixture Description", mockFixtureSuite.Description);
		}

		[Test]
		public void FixtureDescriptionInResult()
		{
			TestSuite suite = new TestSuite("Mock Fixture");
			suite.Add( TestBuilder.MakeFixture( typeof( MockFixture ) ) );
            TestResult result = suite.Run(NullListener.NULL, TestFilter.Empty);

		    TestResult fixtureResult = TestFinder.Find("MockFixture", result, true);
            Assert.AreEqual("Fixture Description", fixtureResult.Description);
		}

        [Test]
        public void SeparateDescriptionAttribute()
        {
            Test testCase = TestBuilder.MakeTestCase(FixtureType, "SeparateDescriptionMethod");
            Assert.AreEqual("Separate Description", testCase.Description);
        }

        [Test]
        public void SeparateDescriptionInResult()
        {
            TestSuite suite = new TestSuite("Mock Fixture");
            suite.Add(TestBuilder.MakeFixture(typeof(MockFixture)));
            TestResult result = suite.Run(NullListener.NULL, TestFilter.Empty);

            TestResult caseResult = TestFinder.Find("SeparateDescriptionMethod", result, true);
            Assert.AreEqual("Separate Description", caseResult.Description);
        }

    }
}
