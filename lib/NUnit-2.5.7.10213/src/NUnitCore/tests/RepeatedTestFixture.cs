// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Reflection;
using NUnit.Framework;
using NUnit.TestData.RepeatedTestFixture;

namespace NUnit.Core.Tests
{
    [TestFixture]
    public class RepeatedTestFixture
	{
		private MethodInfo successMethod;
		private MethodInfo failOnFirstMethod;
		private MethodInfo failOnThirdMethod;

		[SetUp]
		public void SetUp()
		{
			Type testType = typeof(RepeatSuccessFixture);
			successMethod = testType.GetMethod ("RepeatSuccess");
			testType = typeof(RepeatFailOnFirstFixture);
			failOnFirstMethod = testType.GetMethod("RepeatFailOnFirst");
			testType = typeof(RepeatFailOnThirdFixture);
			failOnThirdMethod = testType.GetMethod("RepeatFailOnThird");
		}

		private TestResult RunTestOnFixture( object fixture )
		{
			Test suite = TestFixtureBuilder.BuildFrom( fixture );
			Assert.AreEqual( 1, suite.Tests.Count, "Test case count" );
            return suite.Run(NullListener.NULL, TestFilter.Empty);
		}

		[Test]
		public void RepeatSuccess()
		{
			Assert.IsNotNull (successMethod);
			RepeatSuccessFixture fixture = new RepeatSuccessFixture();
			TestResult result = RunTestOnFixture( fixture );

			Assert.IsTrue(result.IsSuccess);
			Assert.AreEqual(1, fixture.FixtureSetupCount);
			Assert.AreEqual(1, fixture.FixtureTeardownCount);
			Assert.AreEqual(3, fixture.SetupCount);
			Assert.AreEqual(3, fixture.TeardownCount);
			Assert.AreEqual(3, fixture.Count);
		}

		[Test]
		public void RepeatFailOnFirst()
		{
			Assert.IsNotNull (failOnFirstMethod);
			RepeatFailOnFirstFixture fixture = new RepeatFailOnFirstFixture();
			TestResult result = RunTestOnFixture( fixture );

			Assert.IsFalse(result.IsSuccess);
			Assert.AreEqual(1, fixture.SetupCount);
			Assert.AreEqual(1, fixture.TeardownCount);
			Assert.AreEqual(1, fixture.Count);
		}

		[Test]
		public void RepeatFailOnThird()
		{
			Assert.IsNotNull (failOnThirdMethod);
			RepeatFailOnThirdFixture fixture = new RepeatFailOnThirdFixture();
			TestResult result = RunTestOnFixture( fixture );

			Assert.IsFalse(result.IsSuccess);
			Assert.AreEqual(3, fixture.SetupCount);
			Assert.AreEqual(3, fixture.TeardownCount);
			Assert.AreEqual(3, fixture.Count);
		}

		[Test]
		public void IgnoreWorksWithRepeatedTest()
		{
			RepeatedTestWithIgnore fixture = new RepeatedTestWithIgnore();
			RunTestOnFixture( fixture );

			Assert.AreEqual( 0, fixture.SetupCount );
			Assert.AreEqual( 0, fixture.TeardownCount );
			Assert.AreEqual( 0, fixture.Count );
		}

        [Test]
        public void CategoryWorksWithRepeatedTest()
        {
            Test suite = TestFixtureBuilder.BuildFrom(typeof(RepeatedTestWithCategory));
            Test test = suite.Tests[0] as Test;
            Assert.IsNotNull(test.Categories);
            Assert.AreEqual(1, test.Categories.Count);
            Assert.AreEqual("SAMPLE", test.Categories[0]);
        }
	}
}
