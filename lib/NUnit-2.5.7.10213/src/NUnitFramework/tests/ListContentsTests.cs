// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System;
using System.Collections;
using NUnit.Framework;

namespace NUnit.Framework.Tests
{
	/// <summary>
	/// Summary description for ListContentsTests.
	/// </summary>
	[TestFixture]
	public class ListContentsTests : MessageChecker
	{
		private static readonly object[] testArray = { "abc", 123, "xyz" };

		[Test]
		public void ArraySucceeds()
		{
			Assert.Contains( "abc", testArray );
			Assert.Contains( 123, testArray );
			Assert.Contains( "xyz", testArray );
		}

		[Test,ExpectedException(typeof(AssertionException))]
		public void ArrayFails()
		{
			expectedMessage =
				"  Expected: collection containing \"def\"" + Environment.NewLine + 
				"  But was:  < \"abc\", 123, \"xyz\" >" + Environment.NewLine;	
			Assert.Contains("def", testArray);
		}

		[Test,ExpectedException(typeof(AssertionException))]
		public void EmptyArrayFails()
		{
			expectedMessage =
				"  Expected: collection containing \"def\"" + Environment.NewLine + 
				"  But was:  <empty>" + Environment.NewLine;	
			Assert.Contains( "def", new object[0] );
		}

		[Test,ExpectedException(typeof(ArgumentException))]
		public void NullArrayIsError()
		{
			Assert.Contains( "def", null );
		}

		[Test]
		public void ArrayListSucceeds()
		{
			ArrayList list = new ArrayList( testArray );

			Assert.Contains( "abc", list );
			Assert.Contains( 123, list );
			Assert.Contains( "xyz", list );
		}

		[Test,ExpectedException(typeof(AssertionException))]
		public void ArrayListFails()
		{
			expectedMessage =
				"  Expected: collection containing \"def\"" + Environment.NewLine + 
				"  But was:  < \"abc\", 123, \"xyz\" >" + Environment.NewLine;
			Assert.Contains( "def", new ArrayList( testArray ) );
		}

		[Test]
		public void DifferentTypesMayBeEqual()
		{
			// TODO: Better message for this case
			expectedMessage =
				"  Expected: collection containing 123.0d" + Environment.NewLine + 
				"  But was:  < \"abc\", 123, \"xyz\" >" + Environment.NewLine;
			Assert.Contains( 123.0, new ArrayList( testArray ) );
		}
	}
}
