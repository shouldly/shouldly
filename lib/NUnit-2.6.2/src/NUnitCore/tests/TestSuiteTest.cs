// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using NUnit.Framework;
using NUnit.Core.Builders;
using NUnit.Util;
using NUnit.Tests.Assemblies;
using System.Collections;
using NUnit.Core.Filters;
using NUnit.TestUtilities;
using NUnit.TestData;

namespace NUnit.Core.Tests
{
	[TestFixture]
	public class TestSuiteTest
	{
		TestSuite mockTestFixture;
		TestSuite noTestSuite;

		[SetUp]
		public void SetUp()
		{
			mockTestFixture = TestBuilder.MakeFixture( typeof( MockTestFixture ) );
			TestSuite noTestFixture = TestBuilder.MakeFixture( typeof( EmptyFixture ) );

			noTestSuite = new TestSuite("No Tests");
			noTestSuite.Add( noTestFixture);
		}

		[Test]
		public void RunTestsInFixture()
		{
            TestResult result = mockTestFixture.Run(NullListener.NULL, TestFilter.Empty);
			ResultSummarizer summarizer = new ResultSummarizer( result );
			Assert.AreEqual( MockTestFixture.TestsRun, summarizer.TestsRun, "TestsRun" );
            Assert.AreEqual( MockTestFixture.NotRunnable, summarizer.NotRunnable, "NotRunnable");
            Assert.AreEqual(MockTestFixture.Ignored, summarizer.Ignored, "Ignored");
            Assert.AreEqual(MockTestFixture.Errors, summarizer.Errors, "Errors");
            Assert.AreEqual(MockTestFixture.Failures, summarizer.Failures, "Failures");

			result = TestFinder.Find( "ExplicitlyRunTest", result, true );
			Assert.IsNull( result, "ExplicitlyRunTest should not be in results" );

			// TODO: Decide if we want to include Explicit tests that are not run in results
			//			Assert.IsNotNull( result, "Cannot find ExplicitlyRunTest result" );
			//			Assert.IsFalse( result.Executed, "ExplicitlyRunTest should not be executed" );
			//			Assert.AreEqual( "Explicit selection required", result.Message );
		}

		[Test]
		public void RunExplicitTestDirectly()
		{
			Test test = TestFinder.Find( "ExplicitlyRunTest", mockTestFixture, true );
			Assert.IsNotNull( test, "Cannot find ExplicitlyRunTest" );
			Assert.AreEqual( RunState.Explicit, test.RunState );
            TestResult result = test.Run(NullListener.NULL, TestFilter.Empty);
			ResultSummarizer summarizer = new ResultSummarizer( result );
			Assert.AreEqual( 1, summarizer.TestsRun );
		}

		[Test]
		public void RunExplicitTestByName()
		{
			Test test = TestFinder.Find( "ExplicitlyRunTest", mockTestFixture, true );
			Assert.IsNotNull( test, "Cannot find ExplicitlyRunTest" );
			Assert.AreEqual( RunState.Explicit, test.RunState );

			NameFilter filter = new NameFilter( test.TestName );
			TestResult result = mockTestFixture.Run( NullListener.NULL, filter );
			ResultSummarizer summarizer = new ResultSummarizer( result );
			Assert.AreEqual( 1, summarizer.TestsRun );
		}

		[Test]
		public void RunExplicitTestByCategory()
		{
			CategoryFilter filter = new CategoryFilter( "Special" );
			TestResult result = mockTestFixture.Run( NullListener.NULL, filter );
			ResultSummarizer summarizer = new ResultSummarizer( result );
			Assert.AreEqual( 1, summarizer.TestsRun );
		}

		[Test]
		public void ExcludingCategoryDoesNotRunExplicitTestCases()
		{
			NotFilter filter = new NotFilter( new CategoryFilter( "MockCategory" ) );
            filter.TopLevel = true;
			TestResult result = mockTestFixture.Run( NullListener.NULL, filter );
			ResultSummarizer summarizer = new ResultSummarizer( result );
			Assert.AreEqual( MockTestFixture.TestsRun - MockTestFixture.MockCategoryTests, summarizer.TestsRun );
		}

		[Test]
		public void ExcludingCategoryDoesNotRunExplicitTestFixtures()
		{
			NotFilter filter = new NotFilter( new CategoryFilter( "MockCategory" ) );
            filter.TopLevel = true;
			TestAssemblyBuilder builder = new TestAssemblyBuilder();
			TestSuite suite = builder.Build( MockAssembly.AssemblyPath, true );
			TestResult result = suite.Run( NullListener.NULL, filter );
			ResultSummarizer summarizer = new ResultSummarizer( result );
			Assert.AreEqual( MockAssembly.TestsRun - 2, summarizer.TestsRun );
		}

		[Test]
		public void InheritedTestCount()
		{
			TestSuite suite = TestBuilder.MakeFixture( typeof( InheritedTestFixture ) );
			Assert.AreEqual(InheritedTestFixture.Tests, suite.TestCount);
		}

		[Test]
		public void SuiteRunInitialized()
		{
			Assert.AreEqual( RunState.Runnable, mockTestFixture.RunState );
		}

		[Test]
		public void SuiteWithNoTests()
		{
			IList tests = noTestSuite.Tests;
			Assert.AreEqual(1, tests.Count);
			TestSuite testSuite = (TestSuite)tests[0];

            // NOTE: Beginning with NUnit 2.5.3, a suite with no tests is now runnable
			Assert.AreEqual( RunState.Runnable, testSuite.RunState );
			//Assert.AreEqual(testSuite.TestName.Name + " does not have any tests", testSuite.IgnoreReason);
		}

		[Test]
		public void RunNoTestSuite()
		{
			Assert.AreEqual(0, noTestSuite.TestCount);

            TestResult result = noTestSuite.Run(NullListener.NULL, TestFilter.Empty);

			ResultSummarizer summarizer = new ResultSummarizer(result);
			Assert.AreEqual(0, summarizer.TestsRun);
			Assert.AreEqual(0, summarizer.TestsNotRun);
		}

		[Test]
		public void RunTestByName() 
		{
			TestSuite testSuite = new TestSuite("Mock Test Suite");
			testSuite.Add(mockTestFixture);

			Assert.IsNull(testSuite.Parent);
			Test firstTest = (Test)testSuite.Tests[0];
			Assert.AreEqual(testSuite, firstTest.Parent);
			Test bottom = (Test)firstTest.Tests[2];
			
			RecordingListener listener = new RecordingListener();
			NameFilter filter = new NameFilter(bottom.TestName);
			testSuite.Run(listener, filter);
			Assert.AreEqual(1, listener.testStarted.Count);
			Assert.AreEqual("MockTest3", (string)listener.testStarted[0]);
		}

		[Test]
		public void RunSuiteByName() 
		{
			TestSuite testSuite = new TestSuite("Mock Test Suite");
			testSuite.Add(mockTestFixture);
			
			RecordingListener listener = new RecordingListener();
            testSuite.Run(listener, TestFilter.Empty);

			Assert.AreEqual(MockTestFixture.ResultCount, listener.testStarted.Count);
			Assert.AreEqual(2, listener.suiteStarted.Count);
		}

		[Test]
		public void CountTestCasesFilteredByName() 
		{
			TestSuite testSuite = new TestSuite("Mock Test Suite");
			testSuite.Add(mockTestFixture);
			Assert.AreEqual(MockTestFixture.Tests, testSuite.TestCount);
			
			Test mock3 = TestFinder.Find("MockTest3", testSuite, true);
			Test mock1 = TestFinder.Find("MockTest1", testSuite, true);
			NameFilter filter = new NameFilter(mock3.TestName);
			Assert.AreEqual(1, testSuite.CountTestCases(filter));

			filter = new NameFilter();
			filter.Add(mock3.TestName);
			filter.Add(mock1.TestName);

			Assert.AreEqual(2, testSuite.CountTestCases(filter));

			filter = new NameFilter(testSuite.TestName);

			Assert.AreEqual(MockTestFixture.ResultCount, testSuite.CountTestCases(filter));
		}

		[Test]
		public void RunTestByCategory()
		{
			TestSuite testSuite = new TestSuite("Mock Test Suite");
			testSuite.Add(mockTestFixture);

			CategoryFilter filter = new CategoryFilter();
			filter.AddCategory("MockCategory");
			RecordingListener listener = new RecordingListener();
			testSuite.Run(listener, filter);
			CollectionAssert.AreEquivalent(
				new string[] { "MockTest2", "MockTest3" },
				listener.testStarted );
		}

		[Test]
		public void RunTestExcludingCategory()
		{
			TestSuite testSuite = new TestSuite("Mock Test Suite");
			testSuite.Add(mockTestFixture);

			CategoryFilter filter = new CategoryFilter();
			filter.AddCategory("MockCategory");
			RecordingListener listener = new RecordingListener();
            NotFilter notFilter = new NotFilter(filter);
            notFilter.TopLevel = true;
			testSuite.Run(listener, notFilter);
			CollectionAssert.AreEquivalent( 
				new string[] { "MockTest1", "MockTest4", "MockTest5", 
                    "TestWithManyProperties", "NotRunnableTest", "FailingTest", 
                    "TestWithException", "InconclusiveTest" },
				listener.testStarted );
		}

		[Test]
		public void RunSuiteByCategory() 
		{
			TestSuite testSuite = new TestSuite("Mock Test Suite");
			testSuite.Add(mockTestFixture);

			CategoryFilter filter = new CategoryFilter();
			filter.AddCategory("FixtureCategory");
			RecordingListener listener = new RecordingListener();
			testSuite.Run(listener, filter);
			Assert.AreEqual(MockTestFixture.ResultCount, listener.testStarted.Count);
		}

		[Test]
		public void RunSingleTest()
		{
			TestSuite fixture = TestBuilder.MakeFixture( typeof( NUnit.Tests.Assemblies.MockTestFixture ) );
			Test test = (Test) fixture.Tests[0];
			RecordingListener listener = new RecordingListener();
			test.Run(listener, null);
			Assert.IsFalse(listener.lastResult.IsFailure);
		}

		[Test]
		public void DefaultSortIsByName()
		{
			mockTestFixture.Sort();
			Assert.AreEqual( "ExplicitlyRunTest", ((Test)mockTestFixture.Tests[0]).TestName.Name );
		}

		[Test]
		public void CanSortUsingExternalComparer()
		{
			IComparer comparer = new ReverseSortComparer();
			mockTestFixture.Sort(comparer);
			Assert.AreEqual( "TestWithManyProperties", ((Test)mockTestFixture.Tests[0]).TestName.Name );
		}

		private class ReverseSortComparer : IComparer
		{
			public int Compare(object t1, object t2)
			{
				int result = Comparer.Default.Compare( t1, t2 );
				return -result;
			}
		}

	}

	[Serializable]
	public class RecordingListener : EventListener
	{
		public ArrayList testStarted = new ArrayList();
		public ArrayList testFinished = new ArrayList();
		public ArrayList suiteStarted = new ArrayList();
		public ArrayList suiteFinished = new ArrayList();

		public TestResult lastResult = null;

		public void RunStarted(string name, int testCount)
		{
		}

		public void RunFinished(NUnit.Core.TestResult result)
		{
		}

		public void RunFinished(Exception exception)
		{
		}

		public void TestStarted(TestName testName) 
		{
			testStarted.Add(testName.Name);
		}
			
		public void TestFinished(TestResult result)
		{
			testFinished.Add(result.Name);
			lastResult = result;
		}

		public void SuiteStarted(TestName suiteName)
		{
			suiteStarted.Add(suiteName.Name);
		}

		public void SuiteFinished(TestResult result)
		{
			suiteFinished.Add(result.Name);
		}

		public void UnhandledException(Exception exception )
		{
		}

		public void TestOutput(TestOutput testOutput)
		{
		}
	}
}
