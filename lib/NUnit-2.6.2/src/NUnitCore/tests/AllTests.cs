// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using NUnit.Framework;
using NUnit.Core.Builders;
using NUnit.TestData;

namespace NUnit.Core.Tests
{
	public class AllTests
	{
		[Suite]
		public static TestSuite Suite
		{
			get 
			{
				TestSuite suite = new TestSuite("All Tests");
				suite.Add( new OneTestCase() );
				suite.Add( new AssemblyTests() );
				suite.Add( new NoNamespaceTestFixture() );
				return suite;
			}
		}
	}
}
