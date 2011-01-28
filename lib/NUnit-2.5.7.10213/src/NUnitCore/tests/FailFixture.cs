// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using NUnit.Framework;
using NUnit.TestUtilities;
using NUnit.TestData.FailFixture;

namespace NUnit.Core.Tests
{
	[TestFixture]
	public class FailFixture
	{
		[Test]
		public void VerifyFailWorks()
		{
			TestResult result = TestBuilder.RunTestCase( 
				typeof(VerifyFailThrowsException), 
				"CallAssertFail" );
			Assert.IsTrue(result.IsFailure, "Should have failed");
			Assert.AreEqual(
				VerifyFailThrowsException.failureMessage, 
				result.Message);
		}

		[Test]
		[ExpectedException(typeof(AssertionException))]
		public void FailThrowsAssertionException()
		{
			Assert.Fail(String.Empty);
		}

		[Test]
		public void FailInheritsFromSystemException() 
		{
			try 
			{
				Assert.Fail();
			} 
			catch (System.Exception) 
			{
				return;
			}

			throw new AssertionException("fail"); // You can't call fail() here
		}

		[Test]
		public void FailRecordsInnerException()
		{
			Type fixtureType = typeof(VerifyTestResultRecordsInnerExceptions);
			string expectedMessage ="System.Exception : Outer Exception" + Environment.NewLine + "  ----> System.Exception : Inner Exception";
			NUnit.Core.TestResult result = TestBuilder.RunTestCase(fixtureType, "ThrowInnerException");
			Assert.AreEqual(ResultState.Error, result.ResultState );
			Assert.AreEqual(expectedMessage, result.Message);
		}

		[Test]
		public void BadStackTraceIsHandled()
		{
			TestResult result = TestBuilder.RunTestCase( typeof( BadStackTraceFixture ), "TestFailure" );
			Assert.AreEqual( ResultState.Error, result.ResultState );
			Assert.AreEqual( "NUnit.TestData.FailFixture.ExceptionWithBadStackTrace : thrown by me", result.Message );
			Assert.AreEqual( "No stack trace available", result.StackTrace );
		}

		[Test]
		public void CustomExceptionIsHandled()
		{
			TestResult result = TestBuilder.RunTestCase( typeof( CustomExceptionFixture ), "ThrowCustomException" );
			Assert.AreEqual( ResultState.Error, result.ResultState );
			Assert.AreEqual( "NUnit.TestData.FailFixture.CustomExceptionFixture+CustomException : message", result.Message );
		}
	}
}
