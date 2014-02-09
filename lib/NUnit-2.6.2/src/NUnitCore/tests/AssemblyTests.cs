// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

namespace NUnit.Core.Tests
{
	using System;
	using System.Configuration;
	using System.Diagnostics;
	using System.IO;
	using System.Reflection;
	using System.Reflection.Emit;
	using System.Threading;
	using NUnit.Framework;

	[TestFixture]
	public class AssemblyTests 
	{
        private string thisDll;
	    private string noTestFixturesDll;

		[SetUp]
		public void InitStrings()
		{
            thisDll = AssemblyHelper.GetAssemblyPath(this.GetType());
            noTestFixturesDll = AssemblyHelper.GetAssemblyPath(typeof(NUnit.TestUtilities.TestBuilder));
		}

		// TODO: Review and remove unnecessary tests

		[Test]
		public void RunSetsCurrentDirectory()
		{
			Assert.IsTrue( File.Exists( thisDll ), "Run does not set current directory" );
		}

        //[Test]
        //public void NUnitTraceIsEnabled()
        //{
        //    Assert.IsNotNull( Trace.Listeners["NUnit"] );
        //}

		[Test]
		public void LoadAssembly()
		{
			TestSuiteBuilder builder = new TestSuiteBuilder();
			Test suite = builder.Build( new TestPackage( thisDll ) );
            Assert.IsNotNull(suite, "Unable to load " + thisDll);
			Assert.IsTrue( File.Exists( thisDll ), "Load does not set current Directory" );
		}

		[Test]
		[ExpectedException(typeof(FileNotFoundException))]
		public void LoadAssemblyNotFound()
		{
			TestSuiteBuilder builder = new TestSuiteBuilder();
			builder.Build( new TestPackage( "/XXXX.dll" ) );
		}

		[Test]
		public void LoadAssemblyWithoutTestFixtures()
		{
			TestSuiteBuilder builder = new TestSuiteBuilder();
			Test suite = builder.Build( new TestPackage( noTestFixturesDll ) );
            Assert.IsNotNull(suite, "Unable to load " + noTestFixturesDll);
            Assert.AreEqual(RunState.Runnable, suite.RunState);
			Assert.AreEqual( 0, suite.Tests.Count );
		}

		[Test]
		public void LoadTestFixtureFromAssembly()
		{
			TestSuiteBuilder builder = new TestSuiteBuilder();
			TestPackage package = new TestPackage( thisDll );
			package.TestName = this.GetType().FullName;
			Test suite= builder.Build( package );
			Assert.IsNotNull(suite, "Should not be Null");
		}

		[Test]
		public void AppSettingsLoaded()
		{
			string configFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
			Assert.IsTrue( File.Exists( configFile ), string.Format( "{0} does not exist", configFile ) );
			Assert.IsNull(ConfigurationSettings.AppSettings["tooltip.ShowAlways"]);
			Assert.AreEqual("54321",ConfigurationSettings.AppSettings["test.setting"]);
		}
	}
}
