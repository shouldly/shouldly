// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;
using NUnit.Core.Builders;

namespace NUnit.Core.Tests
{
	[TestFixture]
	public class AttributeInheritance
	{
		NUnitTestFixtureBuilder builder;

		[SetUp]
		public void CreateBuilder()
		{
			builder = new NUnitTestFixtureBuilder();
		}

		[Test]
		public void InheritedFixtureAttributeIsRecognized()
		{
			Assert.That( builder.CanBuildFrom( typeof (TestData.When_collecting_test_fixtures) ) );
		}

		[Test]
		public void InheritedTestAttributeIsRecognized()
		{
			Test fixture = builder.BuildFrom( typeof( TestData.When_collecting_test_fixtures ) );
			Assert.AreEqual( 1, fixture.TestCount );
		}
	}
}
