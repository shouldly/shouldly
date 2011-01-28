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

	/// <summary>
	/// Summary description for TestSuiteTreeNodeTests.
	/// </summary>
	[TestFixture]
	public class TestSuiteTreeNodeTests
	{
		TestSuite testSuite;
		Test testFixture;
		Test testCase;

		TestInfo suiteInfo;
		TestInfo fixtureInfo;
		TestInfo testCaseInfo;

		[SetUp]
		public void SetUp()
		{
			testSuite = new TestSuite("MyTestSuite");
			testFixture = TestFixtureBuilder.BuildFrom( typeof( MockTestFixture ) );
			testSuite.Add( testFixture );

			suiteInfo = new TestInfo( testSuite );
			fixtureInfo = new TestInfo( testFixture );

			testCase = (NUnit.Core.Test)testFixture.Tests[0];
			testCaseInfo = new TestInfo( testCase );
		}

		[Test]
		public void ConstructFromTestInfo()
		{
			TestSuiteTreeNode node;
			
			node = new TestSuiteTreeNode( suiteInfo );
			Assert.AreEqual( "MyTestSuite", node.Text );
			Assert.AreEqual( "TestSuite", node.TestType );

			node = new TestSuiteTreeNode( fixtureInfo );
			Assert.AreEqual( "MockTestFixture", node.Text );
			Assert.AreEqual( "TestFixture", node.TestType );

			node = new TestSuiteTreeNode( testCaseInfo );
			Assert.AreEqual( "MockTest1", node.Text );
			Assert.AreEqual( "TestMethod", node.TestType );
		}

        [Test]
        public void ResultNotSet()
        {
            TestSuiteTreeNode node = new TestSuiteTreeNode(testCaseInfo);

            Assert.AreEqual(TestSuiteTreeNode.InitIndex, node.ImageIndex);
            Assert.AreEqual(TestSuiteTreeNode.InitIndex, node.SelectedImageIndex);
        }

        [Test]
        public void SetResult_Inconclusive()
        {
            TestSuiteTreeNode node = new TestSuiteTreeNode(testCaseInfo);
            TestResult result = new TestResult(testCaseInfo);

            result.SetResult(ResultState.Inconclusive, null, null);
            node.Result = result;
            Assert.AreEqual("MockTest1", node.Result.Name);
            Assert.AreEqual(TestSuiteTreeNode.InconclusiveIndex, node.ImageIndex);
            Assert.AreEqual(TestSuiteTreeNode.InconclusiveIndex, node.SelectedImageIndex);
            Assert.AreEqual(result.ResultState.ToString(), node.StatusText);
        }

        [Test]
		public void SetResult_Ignore()
		{
			TestSuiteTreeNode node = new TestSuiteTreeNode( testCaseInfo );
			TestResult result = new TestResult( testCaseInfo );

			result.Ignore( "reason" );
			node.Result = result;
			Assert.AreEqual( "MockTest1", node.Result.Name );
			Assert.AreEqual( TestSuiteTreeNode.IgnoredIndex, node.ImageIndex );
			Assert.AreEqual( TestSuiteTreeNode.IgnoredIndex, node.SelectedImageIndex );
			Assert.AreEqual( "Ignored", node.StatusText );
		}

		[Test]
		public void SetResult_Success()
		{
			TestSuiteTreeNode node = new TestSuiteTreeNode( testCaseInfo );
			TestResult result = new TestResult( testCaseInfo );

			result.Success();
			node.Result = result;
			Assert.AreEqual( TestSuiteTreeNode.SuccessIndex, node.ImageIndex );
			Assert.AreEqual( TestSuiteTreeNode.SuccessIndex, node.SelectedImageIndex );
			Assert.AreEqual( "Success", node.StatusText );
		}

		[Test]
		public void SetResult_Failure()
		{
			TestSuiteTreeNode node = new TestSuiteTreeNode( testCaseInfo );
			TestResult result = new TestResult( testCaseInfo );

			result.Failure("message", "stacktrace");
			node.Result = result;
			Assert.AreEqual( TestSuiteTreeNode.FailureIndex, node.ImageIndex );
			Assert.AreEqual( TestSuiteTreeNode.FailureIndex, node.SelectedImageIndex );
			Assert.AreEqual( "Failure", node.StatusText );
		}

		[Test]
		public void SetResult_Skipped()
		{
			TestSuiteTreeNode node = new TestSuiteTreeNode( testCaseInfo );
			TestResult result = new TestResult( testCaseInfo );

            result.Skip("");
			node.Result = result;
			Assert.AreEqual( TestSuiteTreeNode.SkippedIndex, node.ImageIndex );
			Assert.AreEqual( TestSuiteTreeNode.SkippedIndex, node.SelectedImageIndex );
			Assert.AreEqual( "Skipped", node.StatusText );
		}

		[Test]
		public void ClearResult()
		{
			TestResult result = new TestResult( testCaseInfo );
			result.Failure("message", "stacktrace");

			TestSuiteTreeNode node = new TestSuiteTreeNode( result );
			Assert.AreEqual( TestSuiteTreeNode.FailureIndex, node.ImageIndex );
			Assert.AreEqual( TestSuiteTreeNode.FailureIndex, node.SelectedImageIndex );

			node.ClearResults();
			Assert.AreEqual( null, node.Result );
			Assert.AreEqual( TestSuiteTreeNode.InitIndex, node.ImageIndex );
			Assert.AreEqual( TestSuiteTreeNode.InitIndex, node.SelectedImageIndex );
		}
		
		[Test]
		public void ClearNestedResults()
		{
			TestResult testCaseResult = new TestResult( testCaseInfo );
			testCaseResult.Success();
			TestResult testSuiteResult = new TestResult( fixtureInfo );
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
