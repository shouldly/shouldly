// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System;
using System.Collections;
using System.Threading;
using System.Globalization;
using NUnit.Framework;

namespace NUnit.Framework.Tests
{
	[TestFixture]
	public class ConditionAssertTests : MessageChecker
	{
		[Test]
		public void IsTrue()
		{
			Assert.IsTrue(true);
		}

		[Test,ExpectedException(typeof(AssertionException))]
		public void IsTrueFails()
		{
			expectedMessage =
				"  Expected: True" + Environment.NewLine +
				"  But was:  False" + Environment.NewLine;
			Assert.IsTrue(false);
		}

		[Test]
		public void IsFalse()
		{
			Assert.IsFalse(false);
		}

		[Test]
		[ExpectedException(typeof(AssertionException))]
		public void IsFalseFails()
		{
			expectedMessage =
				"  Expected: False" + Environment.NewLine +
				"  But was:  True" + Environment.NewLine;
			Assert.IsFalse(true);
		}
	
		[Test]
		public void IsNull()
		{
			Assert.IsNull(null);
		}

		[Test]
		[ExpectedException(typeof(AssertionException))]
		public void IsNullFails()
		{
			String s1 = "S1";
			expectedMessage =
				"  Expected: null" + Environment.NewLine +
				"  But was:  \"S1\"" + Environment.NewLine;
			Assert.IsNull(s1);
		}
	
		[Test]
		public void IsNotNull()
		{
			String s1 = "S1";
			Assert.IsNotNull(s1);
		}

		[Test]
		[ExpectedException(typeof(AssertionException))]
		public void IsNotNullFails()
		{
			expectedMessage =
				"  Expected: not null" + Environment.NewLine +
				"  But was:  null" + Environment.NewLine;
			Assert.IsNotNull(null);
		}
	
		[Test]
		public void IsNaN()
		{
			Assert.IsNaN(double.NaN);
		}

		[Test]
		[ExpectedException(typeof(AssertionException))]
		public void IsNaNFails()
		{
			expectedMessage =
				"  Expected: NaN" + Environment.NewLine +
				"  But was:  10.0d" + Environment.NewLine;
			Assert.IsNaN(10.0);
		}

		[Test]
		public void IsEmpty()
		{
			Assert.IsEmpty( "", "Failed on empty String" );
			Assert.IsEmpty( new int[0], "Failed on empty Array" );
			Assert.IsEmpty( new ArrayList(), "Failed on empty ArrayList" );
			Assert.IsEmpty( new Hashtable(), "Failed on empty Hashtable" );
            Assert.IsEmpty( (IEnumerable)new int[0], "Failed on empty IEnumerable");
		}

		[Test, ExpectedException(typeof(AssertionException))]
		public void IsEmptyFailsOnString()
		{
			expectedMessage =
				"  Expected: <empty>" + Environment.NewLine +
				"  But was:  \"Hi!\"" + Environment.NewLine;
			Assert.IsEmpty( "Hi!" );
		}

		[Test, ExpectedException(typeof(AssertionException))]
		public void IsEmptyFailsOnNullString()
		{
			expectedMessage =
				"  Expected: <empty>" + Environment.NewLine +
				"  But was:  null" + Environment.NewLine;
			Assert.IsEmpty( (string)null );
		}

        [Test, ExpectedException(typeof(AssertionException))]
        public void IsEmptyFailsOnNonEmptyArray()
        {
            expectedMessage =
                "  Expected: <empty>" + Environment.NewLine +
                "  But was:  < 1, 2, 3 >" + Environment.NewLine;
            Assert.IsEmpty(new int[] { 1, 2, 3 });
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void IsEmptyFailsOnNonEmptyIEnumerable()
        {
            expectedMessage =
                "  Expected: <empty>" + Environment.NewLine +
                "  But was:  < 1, 2, 3 >" + Environment.NewLine;
            Assert.IsEmpty((IEnumerable)new int[] { 1, 2, 3 });
        }

        [Test]
		public void IsNotEmpty()
		{
			int[] array = new int[] { 1, 2, 3 };
			ArrayList list = new ArrayList( array );
			Hashtable hash = new Hashtable();
			hash.Add( "array", array );

			Assert.IsNotEmpty( "Hi!", "Failed on String" );
			Assert.IsNotEmpty( array, "Failed on Array" );
			Assert.IsNotEmpty( list, "Failed on ArrayList" );
			Assert.IsNotEmpty( hash, "Failed on Hashtable" );
            Assert.IsNotEmpty((IEnumerable)array, "Failed on IEnumerable");
		}

		[Test, ExpectedException(typeof(AssertionException))]
		public void IsNotEmptyFailsOnEmptyString()
		{
			expectedMessage =
				"  Expected: not <empty>" + Environment.NewLine +
				"  But was:  <string.Empty>" + Environment.NewLine;
			Assert.IsNotEmpty( "" );
		}

		[Test, ExpectedException(typeof(AssertionException))]
		public void IsNotEmptyFailsOnEmptyArray()
		{
			expectedMessage =
				"  Expected: not <empty>" + Environment.NewLine +
				"  But was:  <empty>" + Environment.NewLine;
			Assert.IsNotEmpty( new int[0] );
		}

		[Test, ExpectedException(typeof(AssertionException))]
		public void IsNotEmptyFailsOnEmptyArrayList()
		{
			expectedMessage =
				"  Expected: not <empty>" + Environment.NewLine +
				"  But was:  <empty>" + Environment.NewLine;
			Assert.IsNotEmpty( new ArrayList() );
		}

        [Test, ExpectedException(typeof(AssertionException))]
        public void IsNotEmptyFailsOnEmptyHashTable()
        {
            expectedMessage =
                "  Expected: not <empty>" + Environment.NewLine +
                "  But was:  <empty>" + Environment.NewLine;
            Assert.IsNotEmpty(new Hashtable());
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void IsNotEmptyFailsOnEmptyIEnumerable()
        {
            expectedMessage =
                "  Expected: not <empty>" + Environment.NewLine +
                "  But was:  <empty>" + Environment.NewLine;
            Assert.IsNotEmpty((IEnumerable)new int[0]);
        }
    }
}
