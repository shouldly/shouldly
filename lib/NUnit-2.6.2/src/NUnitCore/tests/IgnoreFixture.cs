// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.Reflection;
using NUnit.Framework;
using NUnit.Core.Builders;
using NUnit.TestUtilities;
using NUnit.TestData.IgnoreFixture;

namespace NUnit.Core.Tests
{
	/// <summary>
	/// Tests of IgnoreException and Assert.Ignore
	/// </summary>
	[TestFixture]
	public class IgnoreFixture
	{
		[Test]
		public void IgnoreThrowsIgnoreException()
		{
			//Note that we can't use ExpectedException here because
			//Assert.Ignore takes precedence and the test is ignored.
			try
			{
				Assert.Ignore("I threw this!");
			}
			catch(IgnoreException ex)
			{
				Assert.AreEqual("I threw this!", ex.Message);
			}
		}

		[Test]
		public void IgnoreWorksForTestCase()
		{
            Type fixtureType = typeof(IgnoredTestCaseFixture);
            Test test = TestBuilder.MakeTestCase(fixtureType, "CallsIgnore");
            TestResult result = test.Run(NullListener.NULL, TestFilter.Empty);
            Assert.IsFalse(result.Executed, "Test should not run");
            Assert.AreEqual("Ignore me", result.Message);
        }

        [Test]
        public void IgnoreTakesPrecedenceOverExpectedException()
        {
            Type fixtureType = typeof(IgnoredTestCaseFixture);
            Test test = TestBuilder.MakeTestCase(fixtureType, "CallsIgnoreWithExpectedException");
            TestResult result = test.Run(NullListener.NULL, TestFilter.Empty);
            Assert.IsFalse(result.Executed, "Test should not run");
            Assert.AreEqual("Ignore me", result.Message);
        }

		[Test]
		public void IgnoreWorksForTestSuite()
		{
			//IgnoredTestSuiteFixture testFixture = new IgnoredTestSuiteFixture();
			TestSuite suite = new TestSuite("IgnoredTestFixture");
			suite.Add( TestBuilder.MakeFixture( typeof( IgnoredTestSuiteFixture ) ) );
            TestResult result = suite.Run(NullListener.NULL, TestFilter.Empty);

			TestResult fixtureResult = (TestResult)result.Results[0];
			Assert.IsFalse( fixtureResult.Executed, "Fixture should not have been executed" );
			
			foreach( TestResult testResult in fixtureResult.Results )
				Assert.IsFalse( testResult.Executed, "Test case should not have been executed" );
		}

		[Test]
		public void IgnoreWorksFromSetUp()
		{
			TestSuite testFixture = TestBuilder.MakeFixture( typeof( IgnoreInSetUpFixture ) );
            TestResult fixtureResult = testFixture.Run(NullListener.NULL, TestFilter.Empty);
		
			foreach( TestResult testResult in fixtureResult.Results )
				Assert.IsFalse( testResult.Executed, "Test case should not have been executed" );
		}

		[Test]
		public void IgnoreWithUserMessage()
		{
			try
			{
				Assert.Ignore( "my message" );
			}
			catch( IgnoreException ex )
			{
				Assert.AreEqual( "my message", ex.Message );
			}
		}

		[Test]
		public void IgnoreWithUserMessage_OneArg()
		{
			try
			{
				Assert.Ignore( "The number is {0}", 5 );
			}
			catch( IgnoreException ex )
			{
				Assert.AreEqual( "The number is 5", ex.Message );
			}
		}

		[Test]
		public void IgnoreWithUserMessage_ThreeArgs()
		{
			try
			{
				Assert.Ignore( "The numbers are {0}, {1} and {2}", 1, 2, 3 );
			}
			catch( IgnoreException ex )
			{
				Assert.AreEqual( "The numbers are 1, 2 and 3", ex.Message );
			}
		}

		[Test]
		public void IgnoreWithUserMessage_ArrayOfArgs()
		{
			try
			{
			Assert.Ignore( "The numbers are {0}, {1} and {2}", new object[] { 1, 2, 3 } );
			}
			catch( IgnoreException ex )
			{
				Assert.AreEqual( "The numbers are 1, 2 and 3", ex.Message );
			}
		}
	}
}
