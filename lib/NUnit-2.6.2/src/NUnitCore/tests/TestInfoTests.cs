// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;
using NUnit.Tests.Assemblies;
using NUnit.TestUtilities;

namespace NUnit.Core.Tests
{
	/// <summary>
	/// Summary description for TestInfoTests.
	/// </summary>
	[TestFixture]
	public class TestInfoTests
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

		private void CheckConstructionFromTest( ITest expected, string expectedClassName, string expectedMethodName )
		{
			TestInfo actual = new TestInfo( expected );
			Assert.AreEqual( expected.TestName, actual.TestName );
			Assert.AreEqual( expected.TestType, actual.TestType );
			Assert.AreEqual( expected.RunState, actual.RunState );
			Assert.AreEqual( expected.IsSuite, actual.IsSuite, "IsSuite" );
			Assert.AreEqual( expected.TestCount, actual.TestCount, "TestCount" );

            Assert.AreEqual(expectedClassName, actual.ClassName);
            Assert.AreEqual(expectedMethodName, actual.MethodName);
            
            if (expected.Categories == null)
				Assert.AreEqual( 0, actual.Categories.Count, "Categories" );
			else
			{
				Assert.AreEqual( expected.Categories.Count, actual.Categories.Count, "Categories" );
				for ( int index = 0; index < expected.Categories.Count; index++ )
					Assert.AreEqual( expected.Categories[index], actual.Categories[index], "Category {0}", index );
			}

			Assert.AreEqual( expected.TestName, actual.TestName, "TestName" );
		}

		[Test]
		public void ConstructFromFixture()
		{
			CheckConstructionFromTest( testFixture, "NUnit.Tests.Assemblies.MockTestFixture", null );
		}

		[Test]
		public void ConstructFromSuite()
		{
			CheckConstructionFromTest( testSuite, null, null );
		}

		[Test]
		public void ConstructFromTestCase()
		{
			CheckConstructionFromTest( testCase1, "NUnit.Tests.Assemblies.MockTestFixture", "MockTest1" );
		}
	}
}
