// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************
using System;
//using NUnit.Framework.Constraints;

namespace NUnit.Framework.Tests
{
	[TestFixture]
	public class AssertThrowsTests : MessageChecker
	{
        [Test]
        public void CanCatchUnspecifiedException()
        {
            Exception ex = Assert.Catch(new TestDelegate(TestDelegates.ThrowsArgumentException));
            Assert.That(ex, Is.TypeOf(typeof(ArgumentException)));

#if CLR_2_0 || CLR_4_0
            ex = Assert.Catch(TestDelegates.ThrowsArgumentException);
            Assert.That(ex, Is.TypeOf(typeof(ArgumentException)));
#endif
        }

        [Test]
        public void CanCatchExceptionOfExactType()
        {
            Exception ex = Assert.Catch(typeof(ArgumentException), new TestDelegate(TestDelegates.ThrowsArgumentException));
            Assert.That(ex, Is.TypeOf(typeof(ArgumentException)));

#if CLR_2_0 || CLR_4_0
            ex = Assert.Catch<ArgumentException>(new TestDelegate(TestDelegates.ThrowsArgumentException));
            Assert.That(ex, Is.TypeOf(typeof(ArgumentException)));
#endif
        }

        [Test]
        public void CanCatchExceptionOfDerivedType()
        {
            Exception ex = Assert.Catch(typeof(ApplicationException), new TestDelegate(TestDelegates.ThrowsDerivedApplicationException));
            Assert.That(ex, Is.TypeOf(typeof(TestDelegates.DerivedApplicationException)));

#if CLR_2_0 || CLR_4_0
            ex = Assert.Catch<ApplicationException>(TestDelegates.ThrowsDerivedApplicationException);
            Assert.That(ex, Is.TypeOf(typeof(TestDelegates.DerivedApplicationException)));
#endif
        }

        [Test]
		public void CorrectExceptionThrown()
        {
#if CLR_2_0 || CLR_4_0
            Assert.Throws(typeof(ArgumentException), TestDelegates.ThrowsArgumentException);
            Assert.Throws(typeof(ArgumentException),
                delegate { throw new ArgumentException(); });

            Assert.Throws<ArgumentException>(
                delegate { throw new ArgumentException(); });
            Assert.Throws<ArgumentException>(TestDelegates.ThrowsArgumentException);

            // Without cast, delegate is ambiguous before C# 3.0.
            Assert.That((TestDelegate)delegate { throw new ArgumentException(); },
                    Throws.Exception.TypeOf<ArgumentException>() );
            //Assert.Throws( Is.TypeOf(typeof(ArgumentException)),
            //        delegate { throw new ArgumentException(); } );
#else
			Assert.Throws(typeof(ArgumentException),
				new TestDelegate( TestDelegates.ThrowsArgumentException ) );
#endif
        }

		[Test]
		public void CorrectExceptionIsReturnedToMethod()
		{
			ArgumentException ex = Assert.Throws(typeof(ArgumentException),
                new TestDelegate(TestDelegates.ThrowsArgumentException)) as ArgumentException;

            Assert.IsNotNull(ex, "No ArgumentException thrown");
            Assert.That(ex.Message, StartsWith("myMessage"));
            Assert.That(ex.ParamName, Is.EqualTo("myParam"));

#if CLR_2_0 || CLR_4_0
            ex = Assert.Throws<ArgumentException>(
                delegate { throw new ArgumentException("myMessage", "myParam"); }) as ArgumentException;

            Assert.IsNotNull(ex, "No ArgumentException thrown");
            Assert.That(ex.Message, StartsWith("myMessage"));
            Assert.That(ex.ParamName, Is.EqualTo("myParam"));

			ex = Assert.Throws(typeof(ArgumentException), 
                delegate { throw new ArgumentException("myMessage", "myParam"); } ) as ArgumentException;

            Assert.IsNotNull(ex, "No ArgumentException thrown");
            Assert.That(ex.Message, StartsWith("myMessage"));
            Assert.That(ex.ParamName, Is.EqualTo("myParam"));

            ex = Assert.Throws<ArgumentException>(TestDelegates.ThrowsArgumentException) as ArgumentException;

            Assert.IsNotNull(ex, "No ArgumentException thrown");
            Assert.That(ex.Message, StartsWith("myMessage"));
            Assert.That(ex.ParamName, Is.EqualTo("myParam"));
#endif
		}

		[Test, ExpectedException(typeof(AssertionException))]
		public void NoExceptionThrown()
		{
			expectedMessage =
				"  Expected: <System.ArgumentException>" + Environment.NewLine +
				"  But was:  null" + Environment.NewLine;
#if CLR_2_0 || CLR_4_0
            Assert.Throws<ArgumentException>(TestDelegates.ThrowsNothing);
#else
			Assert.Throws( typeof(ArgumentException),
				new TestDelegate( TestDelegates.ThrowsNothing ) );
#endif
		}

        [Test, ExpectedException(typeof(AssertionException))]
        public void UnrelatedExceptionThrown()
        {
            expectedMessage =
                "  Expected: <System.ArgumentException>" + Environment.NewLine +
                "  But was:  <System.ApplicationException> (my message)" + Environment.NewLine;
            matchType = MessageMatch.StartsWith;

#if CLR_2_0 || CLR_4_0
            Assert.Throws<ArgumentException>(TestDelegates.ThrowsApplicationException);
#else
			Assert.Throws( typeof(ArgumentException),
				new TestDelegate(TestDelegates.ThrowsApplicationException) );
#endif
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void BaseExceptionThrown()
        {
            expectedMessage =
                "  Expected: <System.ArgumentException>" + Environment.NewLine +
                "  But was:  <System.Exception> (my message)" + Environment.NewLine;
            matchType = MessageMatch.StartsWith;

#if CLR_2_0 || CLR_4_0
            Assert.Throws<ArgumentException>(TestDelegates.ThrowsSystemException);
#else
            Assert.Throws( typeof(ArgumentException),
                new TestDelegate( TestDelegates.ThrowsSystemException) );
#endif
        }

        [Test,ExpectedException(typeof(AssertionException))]
        public void DerivedExceptionThrown()
        {
            expectedMessage =
                "  Expected: <System.Exception>" + Environment.NewLine +
                "  But was:  <System.ArgumentException> (myMessage" + Environment.NewLine +
                "Parameter name: myParam)" + Environment.NewLine;
            matchType = MessageMatch.StartsWith;

#if CLR_2_0 || CLR_4_0
            Assert.Throws<Exception>(TestDelegates.ThrowsArgumentException);
#else
            Assert.Throws( typeof(Exception),
				new TestDelegate( TestDelegates.ThrowsArgumentException) );
#endif
        }

        [Test]
        public void DoesNotThrowSuceeds()
        {
#if CLR_2_0 || CLR_4_0
            Assert.DoesNotThrow(TestDelegates.ThrowsNothing);
#else
            Assert.DoesNotThrow( new TestDelegate( TestDelegates.ThrowsNothing ) );

			Assert.That( new TestDelegate( TestDelegates.ThrowsNothing ), Throws.Nothing );
#endif
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void DoesNotThrowFails()
        {
#if CLR_2_0 || CLR_4_0
            Assert.DoesNotThrow(TestDelegates.ThrowsArgumentException);
#else
            Assert.DoesNotThrow( new TestDelegate( TestDelegates.ThrowsArgumentException ) );
#endif
        }
    }
}
