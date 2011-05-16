// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.Collections;
using NUnit.Framework;
using NUnit.Core;
using NUnit.Tests.Assemblies;
using NUnit.TestUtilities;
	
namespace NUnit.Util.Tests
{
	/// <summary>
	/// Summary description for TestResultTests.
	/// </summary>
	[TestFixture]
	public class SummaryResultFixture
	{
		private TestResult result;

		[SetUp]
		public void CreateResult()
		{
			Test testFixture = TestFixtureBuilder.BuildFrom( typeof( MockTestFixture ) );
            result = testFixture.Run(NullListener.NULL, TestFilter.Empty);
		}

		[Test]
		public void SummaryMatchesResult()
		{
			ResultSummarizer summary = new ResultSummarizer( result );

			Assert.AreEqual(result.Name, summary.Name);
			Assert.AreEqual(result.Time, summary.Time);
			Assert.AreEqual(result.IsSuccess, summary.Success, "Success");
        
            Assert.AreEqual(MockTestFixture.ResultCount, summary.ResultCount );
			Assert.AreEqual(MockTestFixture.TestsRun, summary.TestsRun, "TestsRun");
			Assert.AreEqual(MockTestFixture.Failures, summary.Failures, "Failures");
            Assert.AreEqual(MockTestFixture.Errors, summary.Errors, "Errors");
			Assert.AreEqual(MockTestFixture.Ignored, summary.Ignored, "Ignored");
            Assert.AreEqual(MockTestFixture.NotRunnable, summary.NotRunnable, "NotRunnable");
		}
	}
}
