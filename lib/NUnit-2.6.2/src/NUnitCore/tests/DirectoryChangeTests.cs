// ****************************************************************
// Copyright 2010, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;
using NUnit.TestData;
using NUnit.TestUtilities;

namespace NUnit.Core.Tests
{
	[TestFixture]
	public class DirectoryChangeTests
	{
		[Test]
		public void ChangingCurrentDirectoryGivesWarning()
		{
			TestResult result = TestBuilder.RunTestCase(typeof(DirectoryChangeFixture), "ChangeCurrentDirectory");
			Assert.AreEqual(ResultState.Success, result.ResultState);
			Assert.AreEqual("Warning: Test changed the CurrentDirectory", result.Message);
		}
	}
}

