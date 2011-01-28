// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;

namespace NUnit.TestData.CultureAttributeTests
{
	[TestFixture, Culture( "en,fr,de" )]
	public class FixtureWithCultureAttribute
	{
		[Test, Culture("en,de")]
		public void EnglishAndGermanTest() { }

		[Test, Culture("fr")]
		public void FrenchTest() { }

		[Test, Culture("fr-CA")]
		public void FrenchCanadaTest() { }
	}

	[TestFixture]
	public class InvalidCultureFixture
	{
		[Test,SetCulture("xx-XX")]
		public void InvalidCultureSet() { }
	}
}