// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Reflection;
using NUnit.Framework;
using NUnit.Core;
using NUnit.Core.Builders;
using NUnit.Core.Extensibility;

namespace NUnit.TestUtilities
{
	/// <summary>
	/// Utility Class used to build NUnit tests for use as test data
	/// </summary>
	public class TestBuilder
	{
		private static ISuiteBuilder suiteBuilder = (ISuiteBuilder)CoreExtensions.Host.GetExtensionPoint("SuiteBuilders");
        private static ITestCaseBuilder testBuilder = (ITestCaseBuilder)CoreExtensions.Host.GetExtensionPoint("TestCaseBuilders");

		public static TestSuite MakeFixture( Type type )
		{
			return (TestSuite)suiteBuilder.BuildFrom( type );
		}

		public static TestSuite MakeFixture( object fixture )
		{
			TestSuite suite = (TestSuite)suiteBuilder.BuildFrom( fixture.GetType() );
			suite.Fixture = fixture;
			return suite;
		}

		public static Test MakeTestCase( Type type, string methodName )
		{
            MethodInfo method = Reflect.GetNamedMethod(type, methodName);
            if (method == null)
                Assert.Fail("Method not found: " + methodName);
            return testBuilder.BuildFrom(method);
        }

		public static Test MakeTestCase( object fixture, string methodName )
		{
			Test test = MakeTestCase( fixture.GetType(), methodName );
			test.Fixture = fixture;
			return test;
		}

		public static TestResult RunTestFixture( Type type )
		{
            return MakeFixture(type).Run(NullListener.NULL, TestFilter.Empty);
		}

		public static TestResult RunTestFixture( object fixture )
		{
            return MakeFixture(fixture).Run(NullListener.NULL, TestFilter.Empty);
		}

		public static TestResult RunTestCase( Type type, string methodName )
		{
            return MakeTestCase(type, methodName).Run(NullListener.NULL, TestFilter.Empty);
		}

		private TestBuilder() { }
	}
}
