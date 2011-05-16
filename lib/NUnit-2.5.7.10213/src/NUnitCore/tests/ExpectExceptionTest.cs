// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.Reflection;
using System.Runtime.Serialization;
using NUnit.Framework;
using NUnit.TestUtilities;
using NUnit.TestData.ExpectExceptionTest;

namespace NUnit.Core.Tests
{
	/// <summary>
	/// 
	/// </summary>
	[TestFixture]
	public class ExpectExceptionTest 
	{
		[Test, ExpectedException]
		public void CanExpectUnspecifiedException()
		{
			throw new ArgumentException();
		}

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestSucceedsWithSpecifiedExceptionType()
        {
            throw new ArgumentException("argument exception");
        }

        [Test]
        [ExpectedException(ExpectedException=typeof(ArgumentException))]
        public void TestSucceedsWithSpecifiedExceptionTypeAsNamedParameter()
        {
            throw new ArgumentException("argument exception");
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void TestSucceedsWithSpecifiedExceptionName()
        {
            throw new ArgumentException("argument exception");
        }

        [Test]
        [ExpectedException(ExpectedExceptionName="System.ArgumentException")]
        public void TestSucceedsWithSpecifiedExceptionNameAsNamedParameter()
        {
            throw new ArgumentException("argument exception");
        }

        [Test]
		[ExpectedException(typeof(ArgumentException),ExpectedMessage="argument exception")]
		public void TestSucceedsWithSpecifiedExceptionTypeAndMessage()
		{
			throw new ArgumentException("argument exception");
		}

		[Test]
		[ExpectedException(typeof(ArgumentException), ExpectedMessage="argument exception", MatchType=MessageMatch.Exact)]
		public void TestSucceedsWithSpecifiedExceptionTypeAndExactMatch()
		{
			throw new ArgumentException("argument exception");
		}

		[Test]
		[ExpectedException(typeof(ArgumentException),ExpectedMessage="invalid", MatchType=MessageMatch.Contains)]
		public void TestSucceedsWithSpecifiedExceptionTypeAndContainsMatch()
		{
			throw new ArgumentException("argument invalid exception");
		}

		[Test]
		[ExpectedException(typeof(ArgumentException),ExpectedMessage="exception$", MatchType=MessageMatch.Regex)]
		public void TestSucceedsWithSpecifiedExceptionTypeAndRegexMatch()
		{
			throw new ArgumentException("argument invalid exception");
		}

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "argument invalid", MatchType = MessageMatch.StartsWith)]
        public void TestSucceedsWithSpecifiedExceptionTypeAndStartsWithMatch()
        {
            throw new ArgumentException("argument invalid exception");
        }

//        [Test]
//        [ExpectedException("System.ArgumentException", "argument exception")]
//        public void TestSucceedsWithSpecifiedExceptionNameAndMessage_OldFormat()
//        {
//            throw new ArgumentException("argument exception");
//        }

        [Test]
        [ExpectedException("System.ArgumentException", ExpectedMessage = "argument exception")]
        public void TestSucceedsWithSpecifiedExceptionNameAndMessage_NewFormat()
        {
            throw new ArgumentException("argument exception");
        }

        [Test]
		[ExpectedException("System.ArgumentException",ExpectedMessage="argument exception",MatchType=MessageMatch.Exact)]
		public void TestSucceedsWithSpecifiedExceptionNameAndExactMatch()
		{
			throw new ArgumentException("argument exception");
		}

		[Test]
		[ExpectedException("System.ArgumentException",ExpectedMessage="invalid", MatchType=MessageMatch.Contains)]
		public void TestSucceedsWhenSpecifiedExceptionNameAndContainsMatch()
		{
			throw new ArgumentException("argument invalid exception");
		}

		[Test]
		[ExpectedException("System.ArgumentException",ExpectedMessage="exception$", MatchType=MessageMatch.Regex)]
		public void TestSucceedsWhenSpecifiedExceptionNameAndRegexMatch()
		{
			throw new ArgumentException("argument invalid exception");
		}

		[Test]
		public void TestFailsWhenBaseExceptionIsThrown()
		{
			Type fixtureType = typeof(BaseException);
			Test test = TestBuilder.MakeTestCase( fixtureType, "BaseExceptionTest" );
            TestResult result = test.Run(NullListener.NULL, TestFilter.Empty);
			Assert.IsTrue(result.IsFailure, "BaseExceptionTest should have failed");
			StringAssert.StartsWith(
				"An unexpected exception type was thrown" + Environment.NewLine +
				"Expected: System.ArgumentException" + Environment.NewLine +
				" but was: System.Exception",
				result.Message);
		}

		[Test]
		public void TestFailsWhenDerivedExceptionIsThrown()
		{
			Type fixtureType = typeof(DerivedException);
			Test test = TestBuilder.MakeTestCase( fixtureType, "DerivedExceptionTest" );
            TestResult result = test.Run(NullListener.NULL, TestFilter.Empty);
			Assert.IsTrue(result.IsFailure, "DerivedExceptionTest should have failed");
			StringAssert.StartsWith( 
				"An unexpected exception type was thrown" + Environment.NewLine +
				"Expected: System.Exception" + Environment.NewLine +
				" but was: System.ArgumentException",
				result.Message);
		}

        [Test]
        public void TestMismatchedExceptionType()
        {
            Type fixtureType = typeof(MismatchedException);
            Test test = TestBuilder.MakeTestCase(fixtureType, "MismatchedExceptionType");
            TestResult result = test.Run(NullListener.NULL, TestFilter.Empty);
            Assert.IsTrue(result.IsFailure, "MismatchedExceptionType should have failed");
            StringAssert.StartsWith(
                "An unexpected exception type was thrown" + Environment.NewLine +
                "Expected: System.ArgumentException" + Environment.NewLine +
                " but was: System.ArgumentOutOfRangeException",
                result.Message);
        }

        [Test]
        public void TestMismatchedExceptionTypeAsNamedParameter()
        {
            Type fixtureType = typeof(MismatchedException);
            Test test = TestBuilder.MakeTestCase(fixtureType, "MismatchedExceptionTypeAsNamedParameter");
            TestResult result = test.Run(NullListener.NULL, TestFilter.Empty);
            Assert.IsTrue(result.IsFailure, "MismatchedExceptionType should have failed");
            StringAssert.StartsWith(
                "An unexpected exception type was thrown" + Environment.NewLine +
                "Expected: System.ArgumentException" + Environment.NewLine +
                " but was: System.ArgumentOutOfRangeException",
                result.Message);
        }

        [Test]
		public void TestMismatchedExceptionTypeWithUserMessage()
		{
			Type fixtureType = typeof(MismatchedException);
			Test test = TestBuilder.MakeTestCase( fixtureType, "MismatchedExceptionTypeWithUserMessage" );
            TestResult result = test.Run(NullListener.NULL, TestFilter.Empty);
			Assert.IsTrue(result.IsFailure, "Test method should have failed");
			StringAssert.StartsWith(
				"custom message" + Environment.NewLine +
				"An unexpected exception type was thrown" + Environment.NewLine +
				"Expected: System.ArgumentException" + Environment.NewLine +
				" but was: System.ArgumentOutOfRangeException", 
				result.Message);
		}

		[Test]
		public void TestMismatchedExceptionName()
		{
			Type fixtureType = typeof(MismatchedException);
			Test test = TestBuilder.MakeTestCase( fixtureType, "MismatchedExceptionName" );
            TestResult result = test.Run(NullListener.NULL, TestFilter.Empty);
			Assert.IsTrue(result.IsFailure, "MismatchedExceptionName should have failed");
			StringAssert.StartsWith(
				"An unexpected exception type was thrown" + Environment.NewLine +
				"Expected: System.ArgumentException" + Environment.NewLine +
				" but was: System.ArgumentOutOfRangeException", 
				result.Message);
		}

		[Test]
		public void TestMismatchedExceptionNameWithUserMessage()
		{
			Type fixtureType = typeof(MismatchedException);
			Test test = TestBuilder.MakeTestCase( fixtureType, "MismatchedExceptionNameWithUserMessage" );
            TestResult result = test.Run(NullListener.NULL, TestFilter.Empty);
			Assert.IsTrue(result.IsFailure, "Test method should have failed");
			StringAssert.StartsWith(
				"custom message" + Environment.NewLine +
				"An unexpected exception type was thrown" + Environment.NewLine +
				"Expected: System.ArgumentException" + Environment.NewLine +
				" but was: System.ArgumentOutOfRangeException", 
				result.Message);
		}

		[Test]
		public void TestMismatchedExceptionMessage()
		{
			Type fixtureType = typeof(TestThrowsExceptionWithWrongMessage);
			Test test = TestBuilder.MakeTestCase( fixtureType, "TestThrow" );
            TestResult result = test.Run(NullListener.NULL, TestFilter.Empty);
			Assert.IsTrue(result.IsFailure, "TestThrow should have failed");
			Assert.AreEqual(
				"The exception message text was incorrect" + Environment.NewLine +
				"Expected: not the message" + Environment.NewLine +
				" but was: the message", 
				result.Message);
		}

		[Test]
		public void TestMismatchedExceptionMessageWithUserMessage()
		{
			Type fixtureType = typeof(TestThrowsExceptionWithWrongMessage);
			Test test = TestBuilder.MakeTestCase( fixtureType, "TestThrowWithUserMessage" );
            TestResult result = test.Run(NullListener.NULL, TestFilter.Empty);
			Assert.IsTrue(result.IsFailure, "TestThrow should have failed");
			Assert.AreEqual(
				"custom message" + Environment.NewLine +
				"The exception message text was incorrect" + Environment.NewLine +
				"Expected: not the message" + Environment.NewLine +
				" but was: the message", 
				result.Message);
		}

		[Test]
		public void TestUnspecifiedExceptionNotThrown()
		{
			Type fixtureType = typeof(TestDoesNotThrowExceptionFixture);
			Test test = TestBuilder.MakeTestCase( fixtureType, "TestDoesNotThrowUnspecifiedException" );
            TestResult result = test.Run(NullListener.NULL, TestFilter.Empty);
			Assert.IsTrue(result.IsFailure, "Test method should have failed");
			Assert.AreEqual("An Exception was expected", result.Message);
		}

		[Test]
		public void TestUnspecifiedExceptionNotThrownWithUserMessage()
		{
			Type fixtureType = typeof(TestDoesNotThrowExceptionFixture);
			Test test = TestBuilder.MakeTestCase( fixtureType, "TestDoesNotThrowUnspecifiedExceptionWithUserMessage" );
            TestResult result = test.Run(NullListener.NULL, TestFilter.Empty);
			Assert.IsTrue(result.IsFailure, "Test method should have failed");
			Assert.AreEqual("custom message" + Environment.NewLine + "An Exception was expected", result.Message);
		}

		[Test]
		public void TestExceptionTypeNotThrown()
		{
			Type fixtureType = typeof(TestDoesNotThrowExceptionFixture);
			Test test = TestBuilder.MakeTestCase( fixtureType, "TestDoesNotThrowExceptionType" );
            TestResult result = test.Run(NullListener.NULL, TestFilter.Empty);
			Assert.IsTrue(result.IsFailure, "Test method should have failed");
			Assert.AreEqual("System.ArgumentException was expected", result.Message);
		}

		[Test]
		public void TestExceptionTypeNotThrownWithUserMessage()
		{
			Type fixtureType = typeof(TestDoesNotThrowExceptionFixture);
			Test test = TestBuilder.MakeTestCase( fixtureType, "TestDoesNotThrowExceptionTypeWithUserMessage" );
            TestResult result = test.Run(NullListener.NULL, TestFilter.Empty);
			Assert.IsTrue(result.IsFailure, "Test method should have failed");
			Assert.AreEqual("custom message" + Environment.NewLine + "System.ArgumentException was expected", result.Message);
		}

		[Test]
		public void TestExceptionNameNotThrown()
		{
			Type fixtureType = typeof(TestDoesNotThrowExceptionFixture);
			Test test = TestBuilder.MakeTestCase( fixtureType, "TestDoesNotThrowExceptionName" );
            TestResult result = test.Run(NullListener.NULL, TestFilter.Empty);
			Assert.IsTrue(result.IsFailure, "Test method should have failed");
			Assert.AreEqual("System.ArgumentException was expected", result.Message);
		}

		[Test]
		public void TestExceptionNameNotThrownWithUserMessage()
		{
			Type fixtureType = typeof(TestDoesNotThrowExceptionFixture);
			Test test = TestBuilder.MakeTestCase( fixtureType, "TestDoesNotThrowExceptionNameWithUserMessage" );
            TestResult result = test.Run(NullListener.NULL, TestFilter.Empty);
			Assert.IsTrue(result.IsFailure, "Test method should have failed");
			Assert.AreEqual("custom message" + Environment.NewLine + "System.ArgumentException was expected", result.Message);
		}

		[Test] 
		public void MethodThrowsException()
		{
			TestResult result = TestBuilder.RunTestFixture( typeof( TestThrowsExceptionFixture ) );
			Assert.AreEqual(true, result.IsFailure);
		}

		[Test] 
		public void MethodThrowsRightExceptionMessage()
		{
			TestResult result = TestBuilder.RunTestFixture( typeof( TestThrowsExceptionWithRightMessage ) );
			Assert.AreEqual(true, result.IsSuccess);
		}

		[Test]
		public void MethodThrowsArgumentOutOfRange()
		{
			TestResult result = TestBuilder.RunTestFixture( typeof( TestThrowsArgumentOutOfRangeException ) );
			Assert.AreEqual(true, result.IsSuccess );
		}

		[Test] 
		public void MethodThrowsWrongExceptionMessage()
		{
			TestResult result = TestBuilder.RunTestFixture( typeof( TestThrowsExceptionWithWrongMessage ) );
			Assert.AreEqual(true, result.IsFailure);
		}

		[Test]
		public void SetUpThrowsSameException()
		{
			TestResult result = TestBuilder.RunTestFixture( typeof( SetUpExceptionTests ) );
			Assert.AreEqual(true, result.IsFailure);
		}

		[Test]
		public void TearDownThrowsSameException()
		{
			TestResult result = TestBuilder.RunTestFixture( typeof( TearDownExceptionTests ) );
			Assert.AreEqual(true, result.IsFailure);
		}

		[Test]
		public void AssertFailBeforeException() 
		{ 
			TestResult suiteResult = TestBuilder.RunTestFixture( typeof (TestAssertsBeforeThrowingException) );
			Assert.AreEqual( ResultState.Failure, suiteResult.ResultState );
			TestResult result = (TestResult)suiteResult.Results[0];
			Assert.AreEqual( "private message", result.Message );
		} 

		internal class MyAppException : System.Exception
		{
			public MyAppException (string message) : base(message) 
			{}

			public MyAppException(string message, Exception inner) :
				base(message, inner) 
			{}

			protected MyAppException(SerializationInfo info, 
				StreamingContext context) : base(info,context)
			{}
		}

		[Test]
		[ExpectedException(typeof(MyAppException))] 
		public void ThrowingMyAppException() 
		{ 
			throw new MyAppException("my app");
		}

		[Test]
		[ExpectedException(typeof(MyAppException), ExpectedMessage="my app")] 
		public void ThrowingMyAppExceptionWithMessage() 
		{ 
			throw new MyAppException("my app");
		}

		[Test]
		[ExpectedException(typeof(NUnitException))]
		public void ThrowNUnitException()
		{
			throw new NUnitException("Nunit exception");
		}

		[Test]
		public void ExceptionHandlerIsCalledWhenExceptionMatches_AlternateHandler()
		{
			ExceptionHandlerCalledClass fixture = new ExceptionHandlerCalledClass();
			Test test = TestBuilder.MakeTestCase( fixture, "ThrowsArgumentException_AlternateHandler" );
            test.Run(NullListener.NULL, TestFilter.Empty);
			Assert.IsFalse(fixture.HandlerCalled, "Base Handler should not be called" );
			Assert.IsTrue(fixture.AlternateHandlerCalled, "Alternate Handler should be called" );
		}
	
		[Test]
		public void ExceptionHandlerIsCalledWhenExceptionMatches()
		{
			ExceptionHandlerCalledClass fixture = new ExceptionHandlerCalledClass();
			Test test = TestBuilder.MakeTestCase( fixture, "ThrowsArgumentException" );
            test.Run(NullListener.NULL, TestFilter.Empty);
			Assert.IsTrue(fixture.HandlerCalled, "Base Handler should be called");
			Assert.IsFalse(fixture.AlternateHandlerCalled, "Alternate Handler should not be called");
		}
	
		[Test]
		public void ExceptionHandlerIsNotCalledWhenExceptionDoesNotMatch()
		{
			ExceptionHandlerCalledClass fixture = new ExceptionHandlerCalledClass();
			Test test = TestBuilder.MakeTestCase( fixture, "ThrowsApplicationException" );
            test.Run(NullListener.NULL, TestFilter.Empty);
			Assert.IsFalse( fixture.HandlerCalled, "Base Handler should not be called" );
			Assert.IsFalse( fixture.AlternateHandlerCalled, "Alternate Handler should not be called" );
		}

		[Test]
		public void ExceptionHandlerIsNotCalledWhenExceptionDoesNotMatch_AlternateHandler()
		{
			ExceptionHandlerCalledClass fixture = new ExceptionHandlerCalledClass();
			Test test = TestBuilder.MakeTestCase( fixture, "ThrowsApplicationException_AlternateHandler" );
            test.Run(NullListener.NULL, TestFilter.Empty);
			Assert.IsFalse(fixture.HandlerCalled, "Base Handler should not be called");
			Assert.IsFalse(fixture.AlternateHandlerCalled, "Alternate Handler should not be called");
		}

		[Test]
		public void TestIsNotRunnableWhenAlternateHandlerIsNotFound()
		{
			ExceptionHandlerCalledClass fixture = new ExceptionHandlerCalledClass();
			Test test = TestBuilder.MakeTestCase( fixture, "MethodWithBadHandler" );
			Assert.AreEqual( RunState.NotRunnable, test.RunState );
			Assert.AreEqual(
				"The specified exception handler DeliberatelyMissingHandler was not found",
				test.IgnoreReason );
		}
	}
}
