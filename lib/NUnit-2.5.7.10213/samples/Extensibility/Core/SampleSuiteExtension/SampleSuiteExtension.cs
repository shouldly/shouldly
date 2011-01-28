// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org/?p=license&r=2.4
// ****************************************************************

using System;
using System.Reflection;

namespace NUnit.Core.Extensions
{
	/// <summary>
	/// SampleSuiteExtension is a minimal example of a suite extension. It 
	/// extends test suite and creates a fixture that runs every test starting 
	/// with "SampleTest..." Because it inherits from TestSuite, rather than
	/// TestFixture, it has to construct its own fixture object and find its 
	/// own tests. Everything is done in the constructor for simplicity.
	/// </summary>
	class SampleSuiteExtension : TestSuite
	{
		public SampleSuiteExtension( Type fixtureType ) 
			: base( fixtureType )
		{
			// Create the fixture object. We could wait to do this when
			// it is needed, but we do it here for simplicity.
			this.Fixture = Reflect.Construct( fixtureType );

			// Locate our test methods and add them to the suite using
			// the Add method of TestSuite. Note that we don't do a simple
			// Tests.Add, because that wouldn't set the parent of the tests.
			foreach( MethodInfo method in fixtureType.GetMethods( 
				BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly ) )
			{
				if ( method.Name.StartsWith( "SampleTest" ) )
					this.Add( new NUnitTestMethod( method ) );
			}
		}
	}
}
