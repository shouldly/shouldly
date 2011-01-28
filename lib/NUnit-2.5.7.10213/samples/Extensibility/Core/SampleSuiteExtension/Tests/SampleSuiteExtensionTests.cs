// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org/?p=license&r=2.4
// ****************************************************************

using System;
using System.Reflection;

namespace NUnit.Core.Extensions.Tests
{
	/// <summary>
	/// Test class that demonstrates SampleSuiteExtension
	/// </summary>
	[SampleSuiteExtension]
	public class SampleSuiteExtensionTests
	{
		public void SampleTest1()
		{
			Console.WriteLine( "Hello from sample test 1" );
		}

		public void SampleTest2()
		{
			Console.WriteLine( "Hello from sample test 2" );
		}

		public void NotATest()
		{
			Console.WriteLine( "I shouldn't be called!" );
		}
	}
}
