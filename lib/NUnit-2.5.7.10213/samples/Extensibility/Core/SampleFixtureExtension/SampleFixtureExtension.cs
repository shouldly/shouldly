// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org/?p=license&r=2.4
// ****************************************************************

using System;

namespace NUnit.Core.Extensions
{
	/// <summary>
	/// SampleFixtureExtension extends NUnitTestFixture and adds a custom setup
	/// before running TestFixtureSetUp and after running TestFixtureTearDown.
	/// Because it inherits from NUnitTestFixture, a lot of work is done for it.
	/// </summary>
	class SampleFixtureExtension : NUnitTestFixture
	{
		public SampleFixtureExtension( Type fixtureType ) 
			: base( fixtureType )
		{
			// NOTE: Since we are inheriting from NUnitTestFixture we don't 
			// have to do anything if we don't want to. All the attributes
			// that are normally used with an NUnitTestFixture will be
			// recognized.
			//
			// Just to have something to do, we override DoOneTimeSetUp and 
			// DoOneTimeTearDown below to do some special processing before 
			// and after the normal TestFixtureSetUp and TestFixtureTearDown.
			// In this example, we simply display a message.
		}

		protected override void DoOneTimeSetUp(TestResult suiteResult)
		{
			Console.WriteLine( "Extended Fixture SetUp called" );
			base.DoOneTimeSetUp (suiteResult);
		}

		protected override void DoOneTimeTearDown(TestResult suiteResult)
		{
			base.DoOneTimeTearDown (suiteResult);
			Console.WriteLine( "Extended Fixture TearDown called" );
		}
	}
}
