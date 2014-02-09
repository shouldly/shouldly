// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using NUnit.Framework;
using NUnit.Tests.Assemblies;

namespace NUnit.Core.Tests
{
	/// <summary>
	/// Summary description for EventTestFixture.
	/// </summary>
	/// 
	[TestFixture(Description="Tests that proper events are generated when running  test")]
	public class EventTestFixture
	{
        private string testsDll = MockAssembly.AssemblyPath;

		internal class EventCounter : EventListener
		{
			internal int runStarted = 0;
			internal int runFinished = 0;
			internal int testCaseStart = 0;
			internal int testCaseFinished = 0;
			internal int suiteStarted = 0;
			internal int suiteFinished = 0;

			public void RunStarted(string name, int testCount)
			{
				runStarted++;
			}

			public void RunFinished(NUnit.Core.TestResult result)
			{
				runFinished++;
			}

			public void RunFinished(Exception exception)
			{
				runFinished++;
			}

			public void TestStarted(TestName testName)
			{
				testCaseStart++;
			}
			
			public void TestFinished(TestResult result)
			{
				testCaseFinished++;
			}

			public void SuiteStarted(TestName suiteName)
			{
				suiteStarted++;
			}

			public void SuiteFinished(TestResult result)
			{
				suiteFinished++;
			}

			public void UnhandledException( Exception exception )
			{
			}

			public void TestOutput(TestOutput testOutput)
			{
			}
		}

		[Test]
		public void CheckEventListening()
		{
			TestSuiteBuilder builder = new TestSuiteBuilder();
			Test testSuite = builder.Build( new TestPackage( testsDll ) );
			
			EventCounter counter = new EventCounter();
            testSuite.Run(counter, TestFilter.Empty);
			Assert.AreEqual(testSuite.CountTestCases(TestFilter.Empty), counter.testCaseStart);
			Assert.AreEqual(testSuite.CountTestCases(TestFilter.Empty), counter.testCaseFinished);

			Assert.AreEqual(MockAssembly.SuitesRun, counter.suiteStarted);
			Assert.AreEqual(MockAssembly.SuitesRun, counter.suiteFinished);
		}
	}
}

