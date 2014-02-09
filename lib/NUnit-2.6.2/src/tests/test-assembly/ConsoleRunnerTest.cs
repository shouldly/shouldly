// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;

namespace NUnit.TestData.ConsoleRunnerTest
{
	[TestFixture]
	public class SuccessTest
	{
		public static readonly int Tests = 1;

		[Test]
		public void Success()
		{}
	}
		
	[TestFixture] 
	public class FailureTest
	{
		[Test]
		public void Fail()
		{
			Assert.Fail();
		}
	}

	[TestFixture] 
	public class MultiFailureTest
	{
		[Test] public void Fail1() { Assert.Fail(); }
		[Test] public void Fail2() { Assert.Fail(); }
		[Test] public void Fail3() { Assert.Fail(); }
	}

	[TestFixture]
	public class Bug1073539Fixture
	{
		[Test]
		public void TestCaseMessageOutput()
		{
			//Test with lower 128 characters that are common across Windows code pages.
			byte[] encodedBytes = new byte[255];
			byte y = 0;
			for(int x = 0 ; x < 255 ; x++)
			{
				encodedBytes[x] = y++;
			}
			string specialString = System.Text.Encoding.Default.GetString(encodedBytes);
			throw new ApplicationException("Will I break NUnit XML " + specialString);
		}
	}

	[TestFixture]
	public class Bug1311644Fixture
	{
		[Test]
		public void TestCaseAssertMessageOutput()
		{
			Assert.AreEqual(new char[] {(char)0}, new char[] {' '});
		}
	}
}
