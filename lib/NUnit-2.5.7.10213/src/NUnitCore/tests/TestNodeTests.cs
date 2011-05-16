// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using NUnit.Framework;
using NUnit.Tests.Assemblies;
using NUnit.Core.Builders;
using NUnit.TestUtilities;

namespace NUnit.Core.Tests
{
	/// <summary>
	/// TestNode construction tests. Does not repeat tests
	/// for the TestInfo base class.
	/// </summary>
	[TestFixture]	
	public class TestNodeTests
	{
		TestSuite testSuite;
		TestSuite testFixture;
		Test testCase1;

		[SetUp]
		public void SetUp()
		{
			testSuite = new TestSuite("MyTestSuite");
			testFixture = TestBuilder.MakeFixture( typeof( MockTestFixture ) );
			testSuite.Add( testFixture );

			testCase1 = (Test)testFixture.Tests[0];
		}

		[Test]
		public void ConstructFromSuite()
		{
			TestNode test = new TestNode( testSuite );
			Assert.IsNotNull( test.Tests );
			Assert.AreEqual( test.TestCount, CountTests( test ) );
			Assert.AreSame( test, ((TestNode)test.Tests[0]).Parent );
		}

		private int CountTests( TestNode node )
		{
			if ( !node.IsSuite )
				return 1;

			int count = 0;
			if ( node.Tests != null )
				foreach( TestNode child in node.Tests )
					count += CountTests( child );
				
			return count;
		}

		[Test]
		public void ConstructFromTestCase()
		{
			TestNode test = new TestNode( testCase1 );
			Assert.IsNull( test.Tests );
		}

		[Test]
		public void ConstructFromMultipleTests()
		{
			ITest[] tests = new ITest[testFixture.Tests.Count];
			for( int index = 0; index < tests.Length; index++ )
				tests[index] = (ITest)testFixture.Tests[index];

			TestName testName = new TestName();
			testName.FullName = testName.Name = "Combined";
			testName.TestID = new TestID( 1000 );
			TestNode test = new TestNode( testName, tests );
			Assert.AreEqual( "Combined", test.TestName.Name );
			Assert.AreEqual( "Combined", test.TestName.FullName );
			Assert.AreEqual( RunState.Runnable, test.RunState );
			Assert.IsTrue( test.IsSuite, "IsSuite" );
			Assert.AreEqual( tests.Length, test.Tests.Count );
			Assert.AreEqual( MockTestFixture.Tests, test.TestCount );
			Assert.AreEqual( 0, test.Categories.Count, "Categories");
			Assert.AreNotEqual( testFixture.TestName.Name, test.TestName.Name, "TestName" );
		}
	}
}
