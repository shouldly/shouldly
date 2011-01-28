// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;
using System.Reflection;
using NUnit.Core.Extensibility;

namespace NUnit.Core.Tests
{
	/// <summary>
	/// Summary description for TestFrameworkTests.
	/// </summary>
	[TestFixture]
	public class TestFrameworkTests
	{
		[Test]
		public void NUnitFrameworkIsKnownAndReferenced()
		{
			FrameworkRegistry frameworks = (FrameworkRegistry)CoreExtensions.Host.GetExtensionPoint("FrameworkRegistry");
			foreach( AssemblyName assemblyName in frameworks.GetReferencedFrameworks( Assembly.GetExecutingAssembly() ) )
				if ( assemblyName.Name == "nunit.framework" ) return;
			Assert.Fail("Cannot find nunit.framework");
		}
	}
}
