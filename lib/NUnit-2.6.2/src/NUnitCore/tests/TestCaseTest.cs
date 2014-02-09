// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.Reflection;
using NUnit.Framework;
using NUnit.Tests.Assemblies;
using NUnit.Util;
using NUnit.TestUtilities;
using NUnit.TestData.TestCaseTest;

namespace NUnit.Core.Tests
{
	/// <summary>
	/// Summary description for TestCaseTest.
	/// </summary>
	/// 
	[TestFixture]
	public class TestCaseTest
	{
		[Test]
		public void CreateIgnoredTestCase()
		{
			Type fixtureType = typeof(MockTestFixture);
			Test testCase = TestBuilder.MakeTestCase( fixtureType, "MockTest4" );
			Assert.AreEqual(1, testCase.TestCount);
			Assert.AreEqual( RunState.Ignored, testCase.RunState );
			Assert.AreEqual("ignoring this test method for now", testCase.IgnoreReason);
		}

		[Test]
		public void RunIgnoredTestCase()
		{
			Type fixtureType = typeof(MockTestFixture);
			Test testCase = TestBuilder.MakeTestCase( fixtureType, "MockTest4" );
			Assert.AreEqual(1, testCase.TestCount);

            TestResult result = testCase.Run(NullListener.NULL, TestFilter.Empty);
			ResultSummarizer summarizer = new ResultSummarizer(result);
			Assert.AreEqual(0, summarizer.TestsRun);
			Assert.AreEqual(1, summarizer.TestsNotRun);
		}

		[Test]
		public void LoadMethodCategories() 
		{
			Type fixtureType = typeof(HasCategories);
			Test testCase = TestBuilder.MakeTestCase( fixtureType, "ATest" );
			Assert.IsNotNull(testCase);
			Assert.IsNotNull(testCase.Categories);
			Assert.AreEqual(2, testCase.Categories.Count);
		}
	}
}
