// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using NUnit.Core;
using NUnit.Tests.Assemblies;

namespace NUnit.Util.Tests
{
	[TestFixture]
	public class TestDomainFixture
	{
		private static TestDomain testDomain; 
		private static ITest loadedTest;

		[TestFixtureSetUp]
		public static void MakeAppDomain()
		{
			testDomain = new TestDomain();
			testDomain.Load( new TestPackage( "mock-assembly.dll" ) );
			loadedTest = testDomain.Test;
		}

		[TestFixtureTearDown]
		public static void UnloadTestDomain()
		{
            if ( testDomain != null )
                testDomain.Unload();
			loadedTest = null;
			testDomain = null;
		}
			
		[Test]
		public void AssemblyIsLoadedCorrectly()
		{
			Assert.IsNotNull(loadedTest, "Test not loaded");
			Assert.AreEqual(MockAssembly.Tests, loadedTest.TestCount );
		}

		[Test]
		public void AppDomainIsSetUpCorrectly()
		{
			AppDomain domain = testDomain.AppDomain;
			AppDomainSetup setup = testDomain.AppDomain.SetupInformation;

            Assert.That(setup.ApplicationName, Is.StringStarting("Tests_"));
			Assert.AreEqual( Environment.CurrentDirectory, setup.ApplicationBase, "ApplicationBase" );
			Assert.AreEqual( "mock-assembly.dll.config", Path.GetFileName( setup.ConfigurationFile ), "ConfigurationFile" );
			Assert.AreEqual( null, setup.PrivateBinPath, "PrivateBinPath" );
			Assert.AreEqual( Environment.CurrentDirectory, setup.ShadowCopyDirectories, "ShadowCopyDirectories" );

			Assert.AreEqual( Environment.CurrentDirectory, domain.BaseDirectory, "BaseDirectory" );
			Assert.AreEqual( "test-domain-mock-assembly.dll", domain.FriendlyName, "FriendlyName" );
			Assert.IsTrue( testDomain.AppDomain.ShadowCopyFiles, "ShadowCopyFiles" );
		}	

		[Test]
		public void CanRunMockAssemblyTests()
		{
			TestResult result = testDomain.Run( NullListener.NULL );
			Assert.IsNotNull(result);

            ResultSummarizer summarizer = new ResultSummarizer(result);
            Assert.AreEqual(MockAssembly.TestsRun, summarizer.TestsRun, "TestsRun");
            Assert.AreEqual(MockAssembly.Ignored, summarizer.Ignored, "Ignored");
            Assert.AreEqual(MockAssembly.Errors, summarizer.Errors, "Errors");
            Assert.AreEqual(MockAssembly.Failures, summarizer.Failures, "Failures");
        }
	}

	[TestFixture]
	public class TestDomainRunnerTests : NUnit.Core.Tests.BasicRunnerTests
	{
		protected override TestRunner CreateRunner(int runnerID)
		{
			return new TestDomain(runnerID);
		}

	}

	[TestFixture]
	public class TestDomainTests
	{ 
		private TestDomain testDomain;

		[SetUp]
		public void SetUp()
		{
			testDomain = new TestDomain();
		}

		[TearDown]
		public void TearDown()
		{
			testDomain.Unload();
		}

		[Test]
		[ExpectedException(typeof(FileNotFoundException))]
		public void FileNotFound()
		{
			testDomain.Load( new TestPackage( "xxxx.dll" ) );
		}

		[Test]
		public void InvalidTestFixture()
		{
			TestPackage package = new TestPackage( "mock-assembly.dll" );
			package.TestName = "NUnit.Tests.Assemblies.Bogus";
			Assert.IsFalse( testDomain.Load( package ) );
		}

		// Doesn't work under .NET 2.0 Beta 2
		//[Test]
		//[ExpectedException(typeof(BadImageFormatException))]
		public void FileFoundButNotValidAssembly()
		{
			string badfile = "x.dll";
			//FileInfo file = new FileInfo( badfile );
			try
			{
				StreamWriter sw = new StreamWriter( badfile );
				//StreamWriter sw = file.AppendText();

				sw.WriteLine("This is a new entry to add to the file");
				sw.WriteLine("This is yet another line to add...");
				sw.Flush();
				sw.Close();
				testDomain.Load( new TestPackage( badfile ) );
			}
			finally
			{
				if ( File.Exists( badfile ) )
					File.Delete( badfile );
			}

		}

		[Test]
		public void SpecificTestFixture()
		{
			TestPackage package = new TestPackage( "mock-assembly.dll" );
			package.TestName = "NUnit.Tests.Assemblies.MockTestFixture";
			testDomain.Load( package );

			TestResult result = testDomain.Run( NullListener.NULL );

            ResultSummarizer summarizer = new ResultSummarizer(result);
            Assert.AreEqual(MockTestFixture.TestsRun, summarizer.TestsRun, "TestsRun");
            Assert.AreEqual(MockTestFixture.Ignored, summarizer.Ignored, "Ignored");
            Assert.AreEqual(MockTestFixture.Errors, summarizer.Errors, "Errors");
            Assert.AreEqual(MockTestFixture.Failures, summarizer.Failures, "Failures");
        }

		[Test]
		public void ConfigFileOverrideIsHonored()
		{
			TestPackage package = new TestPackage( "MyProject.nunit" );
			package.Assemblies.Add( "mock-assembly.dll" );
			package.ConfigurationFile = "override.config";

			testDomain.Load( package );

			Assert.AreEqual( "override.config", 
				Path.GetFileName( testDomain.AppDomain.SetupInformation.ConfigurationFile ) );
		}

		[Test]
		public void BasePathOverrideIsHonored()
		{
			TestPackage package = new TestPackage( "MyProject.nunit" );
			package.Assemblies.Add( "mock-assembly.dll" );
			package.BasePath = Path.GetDirectoryName( Environment.CurrentDirectory );
			package.PrivateBinPath = Path.GetFileName( Environment.CurrentDirectory );

			testDomain.Load( package );

			Assert.AreEqual(  package.BasePath, testDomain.AppDomain.BaseDirectory );
		}

		[Test]
		public void BinPathOverrideIsHonored()
		{
			TestPackage package = new TestPackage( "MyProject.nunit" );
			package.Assemblies.Add( "mock-assembly.dll" );
			package.PrivateBinPath = "dummy;junk";

			testDomain.Load( package );

			Assert.AreEqual( "dummy;junk", 
				testDomain.AppDomain.SetupInformation.PrivateBinPath );
		}

		// Turning off shadow copy only works when done for the primary app domain
		// So this test can only work if it's already off
		// This doesn't seem to be documented anywhere
		[Test]
		public void TurnOffShadowCopy()
		{
			TestPackage package = new TestPackage( "mock-assembly.dll" );
			package.Settings["ShadowCopyFiles"] = false;
			testDomain.Load( package );
			Assert.IsFalse( testDomain.AppDomain.ShadowCopyFiles );
					
			// Prove that shadow copy is really off
//			string location = "NOT_FOUND";
//			foreach( Assembly assembly in testDomain.AppDomain.GetAssemblies() )
//			{
//				if ( assembly.FullName.StartsWith( "mock-assembly" ) )
//				{
//					location = Path.GetDirectoryName( assembly.Location );
//					break;
//				}
//			}
//		
//			StringAssert.StartsWith( AppDomain.CurrentDomain.BaseDirectory.ToLower(), location.ToLower() );
		}
	}
}
