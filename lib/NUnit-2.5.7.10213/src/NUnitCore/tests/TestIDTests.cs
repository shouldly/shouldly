// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;

namespace NUnit.Core.Tests
{
	[TestFixture]
	public class TestIDTests
	{
		[Test]
		public void ClonedTestIDsAreEqual()
		{
			TestID testID = new TestID();
			TestID cloneID = (TestID)testID.Clone();
			Assert.AreEqual( testID, cloneID );

			Assert.IsTrue( testID == cloneID, "operator ==" );
			Assert.IsFalse( testID != cloneID, "operator !=" );
		}

		[Test]
		public void DifferentTestIDsAreNotEqual()
		{
			TestID testID1 = new TestID();
			TestID testID2 = new TestID();
			Assert.AreNotEqual( testID1, testID2 );

			Assert.IsFalse( testID1 == testID2, "operator ==" );
			Assert.IsTrue( testID1 != testID2, "operator !=" );
		}

		[Test]
		public void DifferentTestIDsDisplayDifferentStrings()
		{
			TestID testID1 = new TestID();
			TestID testID2 = new TestID();
			Assert.AreNotEqual( testID1.ToString(), testID2.ToString() );
		}
	}
}
