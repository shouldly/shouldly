// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System;

namespace NUnit.Framework.Tests
{
	[TestFixture]
	public class NotSameFixture : MessageChecker
	{
		private readonly string s1 = "S1";
		private readonly string s2 = "S2";

		[Test]
		public void NotSame()
		{
			Assert.AreNotSame(s1, s2);
		}

		[Test,ExpectedException(typeof(AssertionException))]
		public void NotSameFails()
		{
			expectedMessage =
				"  Expected: not same as \"S1\"" + Environment.NewLine +
				"  But was:  \"S1\"" + Environment.NewLine;
			Assert.AreNotSame( s1, s1 );
		}
	}
}
