// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.Collections;
using NUnit.Framework;
using NUnit.Core;
using NUnit.Util;
using NUnit.Tests.Assemblies;

namespace NUnit.Core.Tests
{
	[TestFixture]
	public class SuiteBuilderTests_Multiple
	{
		private static readonly int totalTests = NoNamespaceTestFixture.Tests + MockAssembly.Tests;

		private TestSuiteBuilder builder;
		private static string[] assemblies = new string[] {
            NoNamespaceTestFixture.AssemblyPath,
            MockAssembly.AssemblyPath
        };
		private Test loadedSuite;

		[SetUp]
		public void LoadSuite()
		{
			builder = new TestSuiteBuilder();
			loadedSuite = builder.Build( new TestPackage( "TestSuite", assemblies ) );
		}

		[Test]
		public void BuildSuite()
		{
			Assert.IsNotNull( loadedSuite );
		}

		[Test]
		public void RootNode()
		{
			Assert.AreEqual( "TestSuite", loadedSuite.TestName.Name );
		}

		[Test]
		public void TestCaseCount()
		{
			Assert.AreEqual( totalTests , loadedSuite.TestCount);
		}

		[Test]
		public void LoadFixture()
		{
			TestPackage package = new TestPackage( "Multiple Assemblies", assemblies );
			package.TestName = "NUnit.Tests.Assemblies.MockTestFixture";
			TestSuite suite = builder.Build( package );
			Assert.IsNotNull( suite );
			Assert.AreEqual( MockTestFixture.Tests, suite.TestCount );
		}
	}
}
