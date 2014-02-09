// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;

namespace NUnit.Mocks.Tests
{
	/// <summary>
	/// Summary description for MockTests.
	/// </summary>
    [TestFixture]
    [Obsolete("NUnit now uses NSubstitute")]
	public class MockTests
	{
		private Mock mock;

		[SetUp]
		public void SetUp()
		{
			mock = new Mock( "MyMock" );
		}

		[Test]
		public void MockHasName()
		{
			Assert.AreEqual( "MyMock", mock.Name );
		}

		[Test]
		public void StrictDefaultsToFalse()
		{
			Assert.IsFalse( mock.Strict );
		}

		[Test]
		public void VerifyNewMock()
		{
			mock.Verify();
		}

		[Test]
		public void UnexpectedCallsIgnored()
		{
			mock.Call( "x" );
			mock.Call( "y", 1, 2);
			mock.Verify();
		}

		[Test]
		public void OneExpectation()
		{
			mock.Expect( "x" );
			mock.Call( "x" );
			mock.Verify();
		}

		[Test, ExpectedException(typeof(AssertionException))]
		public void MethodNotCalled()
		{
			mock.Expect( "x" );
			mock.Verify();
		}

		[Test]
		public void MultipleExpectations()
		{
			mock.Expect( "x" );
			mock.Expect( "y", "name" );
			mock.Expect( "z", 1, 2, 3 );
			mock.Call( "x" );
			mock.Call( "y", "name" );
			mock.Call( "z", 1, 2, 3 );
			mock.Verify();
		}

		[Test]
		public void MultipleCallsToSameMethod()
		{
			mock.Expect( "method" );
			mock.Expect( "method" );
			mock.Expect( "method" );
			mock.Call( "method" );
			mock.Call( "method" );
			mock.Call( "method" );
			mock.Verify();
		}

		[Test, ExpectedException( typeof(AssertionException) )]
		public void TooManyCalls()
		{
			mock.Expect( "method" );
			mock.Expect( "method" );
			mock.Call( "method" );
			mock.Call( "method" );
			mock.Call( "method" );
			mock.Verify();
		}

		[Test, ExpectedException( typeof(AssertionException) )]
		public void NotEnoughCalls()
		{
			mock.Expect( "method" );
			mock.Expect( "method" );
			mock.Expect( "method" );
			mock.Call( "method" );
			mock.Call( "method" );
			mock.Verify();
		}

		[Test, ExpectedException( typeof( AssertionException ) )]
		public void RequireArguments()
		{
			mock.Expect("myMethod", new object[0]);
			mock.Call("myMethod", "world", null);
			mock.Verify();
		}

		[Test]
		public void IgnoreArguments()
		{
			mock.Expect("myMethod");
			mock.Call("myMethod", "world", null);
			mock.Verify();
		}

		[Test, ExpectedException( typeof( AssertionException ) )]
		public void FailWithParametersSpecified()
		{
			mock.Expect("myMethod", "junk");
			mock.Call("myMethod", "world", null);
			mock.Verify();
		}

		[Test] 
		public void CallMultipleMethodsInDifferentOrder() 
		{
			mock.Expect("myMethod1");
			mock.Expect("myMethod2");
			mock.Expect("myMethod3");
			mock.Call("myMethod3");
			mock.Call("myMethod1");
			mock.Call("myMethod2");
			mock.Verify();
		}

		[Test] 
		public void CallMultipleMethodsSomeWithoutExpectations() 
		{
			mock.Expect("myMethod1");
			mock.Expect("myMethod3");
			mock.Expect("myMethod3");

			mock.Call("myMethod2");
			mock.Call("myMethod3");
			mock.Call("myMethod1");
			mock.Call("myMethod3");
			mock.Verify();
		}

		[Test] public void ExpectAndReturn() 
		{
			object something = new object();
			mock.ExpectAndReturn("myMethod", something);
			object result = mock.Call("myMethod");
			mock.Verify();
			Assert.AreSame(something, result);
		}

		[Test] public void ExpectAndReturnWithArgument() 
		{
			object something = new object();
			mock.ExpectAndReturn("myMethod", something, "hello" );
			object result = mock.Call("myMethod", "hello");
			mock.Verify();
			Assert.AreSame(something, result);
		}

		[Test, ExpectedException(typeof(AssertionException))]
		public void ExpectAndReturnWithWrongArgument() 
		{
			object something = new object();
			mock.ExpectAndReturn( "myMethod", something, "hello" );
			object result = mock.Call("myMethod", "bye");
			mock.Verify();
			Assert.AreSame(something, result);
		}
	
		[Test] 
		public void MultipleExpectAndReturn() 
		{
			object something = new object();
			object anotherthing = new object();
			int x = 3;
			mock.ExpectAndReturn("myMethod", something);
			mock.ExpectAndReturn("myMethod", anotherthing);
			mock.ExpectAndReturn("myMethod", x);
			Assert.AreSame(something, mock.Call("myMethod"));
			Assert.AreSame(anotherthing, mock.Call("myMethod"));
			Assert.AreEqual(x, mock.Call("myMethod"));
			mock.Verify();
		}

		[Test] 
		public void ExpectAndReturnNull() 
		{
			mock.ExpectAndReturn("myMethod", null);
			Assert.IsNull( mock.Call("myMethod") );
			mock.Verify();
		}

		[Test]
		public void SetReturnValue()
		{
			mock.SetReturnValue( "myMethod", 5 );
			Assert.AreEqual( 5, mock.Call( "myMethod") );
			mock.Verify();
		}

		[Test]
		public void SetReturnValueRepeatedCalls()
		{
			mock.SetReturnValue( "myMethod", 5 );
			Assert.AreEqual( 5, mock.Call( "myMethod") );
			Assert.AreEqual( 5, mock.Call( "myMethod") );
			Assert.AreEqual( 5, mock.Call( "myMethod") );
			mock.Verify();
		}

		[Test]
		public void SetReturnValueMultipleTimesOnMultipleMethods()
		{
			mock.SetReturnValue( "myMethod1", "something" );
			mock.SetReturnValue( "myMethod2", "else" );
			Assert.AreEqual( "something", mock.Call( "myMethod1") );
			Assert.AreEqual( "something", mock.Call( "myMethod1") );
			Assert.AreEqual( "else", mock.Call( "myMethod2") );
			Assert.AreEqual( "something", mock.Call( "myMethod1") );
			Assert.AreEqual( "else", mock.Call( "myMethod2") );
			mock.SetReturnValue( "myMethod2", "magnificent" );
			Assert.AreEqual( "something", mock.Call( "myMethod1") );
			Assert.AreEqual( "magnificent", mock.Call( "myMethod2") );
			mock.Verify();
		}

		[Test]
		public void SetReturnValueWithoutCalling()
		{
			mock.SetReturnValue( "hello", "goodbye" );
			mock.Verify();
		}

		[Test, ExpectedException( typeof( System.IO.IOException ) )] 
		public void ExpectAndThrowException()
		{
			mock.ExpectAndThrow( "method", new System.IO.IOException() );
			mock.Call( "method" );
		}

		[Test, ExpectedException( typeof( AssertionException ) )]
		public void ExpectNoCallFails()
		{
			mock.ExpectNoCall( "myMethod" );
			mock.Call( "myMethod" );
		}

		[Test]
		public void ExpectNoCallSucceeds()
		{
			mock.ExpectNoCall( "myMethod" );
			mock.Call("yourMethod");
			mock.Verify();
		}

        [Test, ExpectedException(typeof(AssertionException))]
        public void StrictMode()
        {
            mock.Strict = true;
            mock.Expect("method1");
            mock.Expect("method2");
            mock.Call("method1");
            mock.Call("method2");
            mock.Call("method3");
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void StrictMode_ExceptionsCaught()
        {
            mock.Strict = true;
            mock.Expect("method1");
            mock.Expect("method2");
            try
            {
                mock.Call("method1");
                mock.Call("method2");
                mock.Call("method3");
            }
            catch(Exception)
            {}

            mock.Verify();
        }

        [Test]
		public void ChangeFixedReturnToExpectAndReturn()
		{
			mock.SetReturnValue( "MyMethod", "x" );
			Assert.AreEqual( "x", mock.Call( "MyMethod" ) );
			Assert.AreEqual( "x", mock.Call( "MyMethod", 1, 2, 3 ) );
			mock.ExpectAndReturn( "MyMethod", "y", 1, 2, 3 );
			Assert.AreEqual( "y", mock.Call( "MyMethod", 1, 2, 3 ) );
			mock.Verify();
		}

		[Test]
		public void ChangeExpectAndReturnToFixedReturn()
		{
			mock.ExpectAndReturn( "MyMethod", "y", 1, 2, 3 );
			Assert.AreEqual( "y", mock.Call( "MyMethod", 1, 2, 3 ) );
			mock.SetReturnValue( "MyMethod", "x" );
			Assert.AreEqual( "x", mock.Call( "MyMethod" ) );
			Assert.AreEqual( "x", mock.Call( "MyMethod", 1, 2, 3 ) );
			mock.Verify();
		}

		[Test]
		public void ConstraintArgumentSucceeds()
		{
			mock.Expect( "MyMethod", Is.GreaterThan(10) );
			mock.Call( "MyMethod", 42 );
		}

		[Test,ExpectedException(typeof(AssertionException))]
		public void ConstraintArgumentThatFails()
		{
			mock.Expect( "MyMethod", Is.GreaterThan(10) );
			mock.Call( "MyMethod", 8 );
		}
	}
}
