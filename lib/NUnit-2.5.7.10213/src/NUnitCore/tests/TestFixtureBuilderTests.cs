// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.Reflection;
using NUnit.Framework;
using NUnit.Core;
using NUnit.TestUtilities;
using NUnit.TestData.TestFixtureBuilderTests;

namespace NUnit.Core.Tests
{
	// TODO: Figure out what this is really testing and eliminate if not needed
	[TestFixture]
	public class TestFixtureBuilderTests
	{
		[Test]
		public void GoodSignature()
		{
			string methodName = "TestVoid";
			Test fixture = TestFixtureBuilder.BuildFrom( typeof( SignatureTestFixture ) );
			Test foundTest = TestFinder.Find( methodName, fixture, true );
			Assert.IsNotNull( foundTest );
			Assert.AreEqual( RunState.Runnable, foundTest.RunState );
		}

		[Test]
		public void LoadCategories() 
		{
			Test fixture = TestFixtureBuilder.BuildFrom( typeof( HasCategories ) );
			Assert.IsNotNull(fixture);
			Assert.AreEqual(2, fixture.Categories.Count);
		}
	}
}
