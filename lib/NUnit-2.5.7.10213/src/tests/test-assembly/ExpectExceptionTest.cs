// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;

namespace NUnit.TestData.ExpectExceptionTest
{
	[TestFixture]
	public class BaseException
	{
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void BaseExceptionTest()
		{
			throw new Exception();
		}
	}

	[TestFixture]
	public class DerivedException
	{
		[Test]
		[ExpectedException(typeof(Exception))]
		public void DerivedExceptionTest()
		{
			throw new ArgumentException();
		}
	}

	[TestFixture]
	public class MismatchedException
	{
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void MismatchedExceptionType()
        {
            throw new ArgumentOutOfRangeException();
        }

        [Test]
        [ExpectedException(ExpectedException=typeof(ArgumentException))]
        public void MismatchedExceptionTypeAsNamedParameter()
        {
            throw new ArgumentOutOfRangeException();
        }

        [Test]
		[ExpectedException(typeof(ArgumentException), UserMessage="custom message")]
		public void MismatchedExceptionTypeWithUserMessage()
		{
			throw new ArgumentOutOfRangeException();
		}

		[Test]
		[ExpectedException("System.ArgumentException")]
		public void MismatchedExceptionName()
		{
			throw new ArgumentOutOfRangeException();
		}

		[Test]
		[ExpectedException("System.ArgumentException", UserMessage="custom message")]
		public void MismatchedExceptionNameWithUserMessage()
		{
			throw new ArgumentOutOfRangeException();
		}
	}

	[TestFixture]
	public class SetUpExceptionTests  
	{
		[SetUp]
		public void Init()
		{
			throw new ArgumentException("SetUp Exception");
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Test() 
		{
		}
	}

	[TestFixture]
	public class TearDownExceptionTests
	{
		[TearDown]
		public void CleanUp()
		{
			throw new ArgumentException("TearDown Exception");
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Test() 
		{}
	}

	[TestFixture]
	public class TestThrowsExceptionFixture
	{
		[Test]
		public void TestThrow()
		{
			throw new Exception();
		}
	}

	[TestFixture]
	public class TestDoesNotThrowExceptionFixture
	{
		[Test, ExpectedException("System.ArgumentException")]
		public void TestDoesNotThrowExceptionName()
		{
		}

		[Test, ExpectedException("System.ArgumentException", UserMessage="custom message")]
		public void TestDoesNotThrowExceptionNameWithUserMessage()
		{
		}

		[Test, ExpectedException( typeof( System.ArgumentException ) )]
		public void TestDoesNotThrowExceptionType()
		{
		}

		[Test, ExpectedException( typeof( System.ArgumentException ), UserMessage="custom message" )]
		public void TestDoesNotThrowExceptionTypeWithUserMessage()
		{
		}

		[Test, ExpectedException]
		public void TestDoesNotThrowUnspecifiedException()
		{
		}

		[Test, ExpectedException( UserMessage="custom message" )]
		public void TestDoesNotThrowUnspecifiedExceptionWithUserMessage()
		{
		}
	}

	[TestFixture]
	public class TestThrowsExceptionWithRightMessage
	{
		[Test]
		[ExpectedException(typeof(Exception), ExpectedMessage="the message")]
		public void TestThrow()
		{
			throw new Exception("the message");
		}
	}

	[TestFixture]
	public class TestThrowsArgumentOutOfRangeException
	{
		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException)) ]
		public void TestThrow()
		{
			throw new ArgumentOutOfRangeException("param", "actual value", "the message");
		}
	}

	[TestFixture]
	public class TestThrowsExceptionWithWrongMessage
	{
		[Test]
		[ExpectedException(typeof(Exception), ExpectedMessage="not the message")]
		public void TestThrow()
		{
			throw new Exception("the message");
		}

		[Test]
		[ExpectedException( typeof(Exception), ExpectedMessage="not the message", UserMessage="custom message" )]
		public void TestThrowWithUserMessage()
		{
			throw new Exception("the message");
		}
	}

	[TestFixture]
	public class TestAssertsBeforeThrowingException
	{
		[Test]
		[ExpectedException(typeof(Exception))]
		public void TestAssertFail()
		{
			Assert.Fail( "private message" );
		}
	}

    [TestFixture]
    public class ExceptionHandlerCalledClass : IExpectException
    {
        public bool HandlerCalled = false;
        public bool AlternateHandlerCalled = false;

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ThrowsArgumentException()
        {
            throw new ArgumentException();
        }

        [Test, ExpectedException(typeof(ArgumentException), Handler = "AlternateExceptionHandler")]
        public void ThrowsArgumentException_AlternateHandler()
        {
            throw new ArgumentException();
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ThrowsApplicationException()
        {
            throw new ApplicationException();
        }

        [Test, ExpectedException(typeof(ArgumentException), Handler = "AlternateExceptionHandler")]
        public void ThrowsApplicationException_AlternateHandler()
        {
            throw new ApplicationException();
        }

        [Test, ExpectedException(typeof(ArgumentException), Handler = "DeliberatelyMissingHandler")]
        public void MethodWithBadHandler()
        {
            throw new ArgumentException();
        }

        public void HandleException(Exception ex)
        {
            HandlerCalled = true;
        }

        public void AlternateExceptionHandler(Exception ex)
        {
            AlternateHandlerCalled = true;
        }
    }
}
