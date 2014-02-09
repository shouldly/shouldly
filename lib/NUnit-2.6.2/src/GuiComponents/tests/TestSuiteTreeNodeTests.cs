// ****************************************************************
// Copyright 2002-2003, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

namespace NUnit.UiKit.Tests
{
	using System;
	using NUnit.Core;
	using NUnit.Core.Builders;
	using NUnit.Framework;
	using NUnit.Util;
	using NUnit.Tests.Assemblies;
    using NUnit.TestUtilities;

	/// <summary>
	/// Summary description for TestSuiteTreeNodeTests.
	/// </summary>
	[TestFixture]
	public class TestSuiteTreeNodeTests
	{
		TestSuite testSuite;
		Test testFixture;
		Test testCase;

		[SetUp]
		public void SetUp()
		{
			testSuite = new TestSuite("MyTestSuite");
			testFixture = TestFixtureBuilder.BuildFrom( typeof( MockTestFixture ) );
			testSuite.Add( testFixture );

			testCase = TestFinder.Find("MockTest1", testFixture, false);
		}

		[Test]
		public void CanConstructFromTestSuite()
		{
			TestSuiteTreeNode node = new TestSuiteTreeNode( new TestInfo(testSuite) );
			Assert.AreEqual( "MyTestSuite", node.Text );
			Assert.AreEqual( "TestSuite", node.TestType );
        }

        [Test]
        public void CanConstructFromTestFixture()
        {
			TestSuiteTreeNode node = new TestSuiteTreeNode( new TestInfo(testFixture) );
			Assert.AreEqual( "MockTestFixture", node.Text );
			Assert.AreEqual( "TestFixture", node.TestType );
        }

        [Test]
        public void CanConstructFromTestCase()
        {
			TestSuiteTreeNode node = new TestSuiteTreeNode( new TestInfo(testCase) );
			Assert.AreEqual( "MockTest1", node.Text );
			Assert.AreEqual( "TestMethod", node.TestType );
		}

        [TestCase("MockTest1", TestSuiteTreeNode.InitIndex)]
        [TestCase("MockTest4", TestSuiteTreeNode.IgnoredIndex)]
        [TestCase("NotRunnableTest", TestSuiteTreeNode.FailureIndex)]
        public void WhenResultIsNotSet_IndexReflectsRunState(string testName, int expectedIndex)
        {
            Test test = TestFinder.Find(testName, testFixture, false);
            TestSuiteTreeNode node = new TestSuiteTreeNode(new TestInfo(test));

            Assert.AreEqual(expectedIndex, node.ImageIndex);
            Assert.AreEqual(expectedIndex, node.SelectedImageIndex);
        }

        [TestCase(ResultState.Inconclusive, TestSuiteTreeNode.InconclusiveIndex)]
        [TestCase(ResultState.NotRunnable, TestSuiteTreeNode.FailureIndex)]
        [TestCase(ResultState.Skipped, TestSuiteTreeNode.SkippedIndex)]
        [TestCase(ResultState.Ignored, TestSuiteTreeNode.IgnoredIndex)]
        [TestCase(ResultState.Success, TestSuiteTreeNode.SuccessIndex)]
        [TestCase(ResultState.Failure, TestSuiteTreeNode.FailureIndex)]
        [TestCase(ResultState.Error, TestSuiteTreeNode.FailureIndex)]
        [TestCase(ResultState.Cancelled, TestSuiteTreeNode.FailureIndex)]
        public void WhenResultIsSet_IndexReflectsResultState(ResultState resultState, int expectedIndex)
        {
            TestSuiteTreeNode node = new TestSuiteTreeNode(new TestInfo(testCase));
            TestResult result = new TestResult(testCase);

            result.SetResult(resultState, null, null);
            node.Result = result;
            Assert.AreEqual(expectedIndex, node.ImageIndex);
            Assert.AreEqual(expectedIndex, node.SelectedImageIndex);
            Assert.AreEqual(resultState.ToString(), node.StatusText);
        }

        [TestCase("MockTest1", TestSuiteTreeNode.InitIndex)]
        [TestCase("MockTest4", TestSuiteTreeNode.IgnoredIndex)]
        [TestCase("NotRunnableTest", TestSuiteTreeNode.FailureIndex)]
        public void WhenResultIsCleared_IndexReflectsRunState(string testName, int expectedIndex)
		{
            Test test = TestFinder.Find(testName, testFixture, false);
			TestResult result = new TestResult( test );
			result.Failure("message", "stacktrace");

			TestSuiteTreeNode node = new TestSuiteTreeNode( result );
			Assert.AreEqual( TestSuiteTreeNode.FailureIndex, node.ImageIndex );
			Assert.AreEqual( TestSuiteTreeNode.FailureIndex, node.SelectedImageIndex );

			node.ClearResults();
			Assert.AreEqual( null, node.Result );
			Assert.AreEqual( expectedIndex, node.ImageIndex );
			Assert.AreEqual( expectedIndex, node.SelectedImageIndex );
		}
		
		[Test]
		public void WhenResultIsCleared_NestedResultsAreAlsoCleared()
		{
			TestResult testCaseResult = new TestResult( testCase );
			testCaseResult.Success();
			TestResult testSuiteResult = new TestResult( testFixture );
			testSuiteResult.AddResult( testCaseResult );
            testSuiteResult.Success();

			TestSuiteTreeNode node1 = new TestSuiteTreeNode( testSuiteResult );
			TestSuiteTreeNode node2 = new TestSuiteTreeNode( testCaseResult );
			node1.Nodes.Add( node2 );

			Assert.AreEqual( TestSuiteTreeNode.SuccessIndex, node1.ImageIndex );
			Assert.AreEqual( TestSuiteTreeNode.SuccessIndex, node1.SelectedImageIndex );
			Assert.AreEqual( TestSuiteTreeNode.SuccessIndex, node2.ImageIndex );
			Assert.AreEqual( TestSuiteTreeNode.SuccessIndex, node2.SelectedImageIndex );

			node1.ClearResults();

			Assert.AreEqual( TestSuiteTreeNode.InitIndex, node1.ImageIndex );
			Assert.AreEqual( TestSuiteTreeNode.InitIndex, node1.SelectedImageIndex );
			Assert.AreEqual( TestSuiteTreeNode.InitIndex, node2.ImageIndex );
			Assert.AreEqual( TestSuiteTreeNode.InitIndex, node2.SelectedImageIndex );
		}
	}
}
