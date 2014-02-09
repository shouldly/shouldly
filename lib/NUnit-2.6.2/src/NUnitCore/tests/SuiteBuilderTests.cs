// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.IO;
using NUnit.Framework;
using NUnit.Core;
using NUnit.TestUtilities;

namespace NUnit.Core.Tests
{
	[TestFixture]
	public class SuiteBuilderTests
	{
        private string testsDll = AssemblyHelper.GetAssemblyPath(typeof(SuiteBuilderTests));
		private string testData = AssemblyHelper.GetAssemblyPath(typeof(NUnit.TestData.EmptyFixture));
		private string tempFile;
		private TestSuiteBuilder builder;

		[SetUp]
		public void CreateBuilder()
		{
			builder = new TestSuiteBuilder();
            tempFile = Path.ChangeExtension(Path.GetTempFileName(), ".dll");
		}
		[TearDown]
		public void TearDown()
		{
			FileInfo info = new FileInfo(tempFile);
			if(info.Exists) info.Delete();
		}

		[Test]
		public void LoadAssembly() 
		{
			Test suite = builder.Build( new TestPackage( testsDll ) );
			Assert.IsNotNull(suite, "Unable to build suite" );
			Assert.AreEqual( 1, suite.Tests.Count );
			Assert.AreEqual( "NUnit", ((ITest)suite.Tests[0]).TestName.Name );
		}

		[Test]
		public void LoadAssemblyWithoutNamespaces()
		{
			TestPackage package = new TestPackage( testsDll );
			package.Settings["AutoNamespaceSuites"] = false;
			Test suite = builder.Build( package );
			Assert.IsNotNull(suite, "Unable to build suite" );
			Assert.Greater( suite.Tests.Count, 1 );
			Assert.AreEqual( "TestFixture", ((ITest)suite.Tests[0]).TestType );
		}

		[Test]
		public void LoadFixture()
		{
			TestPackage package = new TestPackage( testsDll );
			package.TestName = "NUnit.Core.Tests.SuiteBuilderTests";
			Test suite= builder.Build( package );
			Assert.IsNotNull(suite, "Unable to build suite");
		}

		[Test]
		public void LoadSuite()
		{
			TestPackage package = new TestPackage( testsDll );
			package.TestName = "NUnit.Core.Tests.AllTests";
			Test suite= builder.Build( package );
			Assert.IsNotNull(suite, "Unable to build suite");
			Assert.AreEqual( 3, suite.Tests.Count );
		}

		[Test]
		public void LoadNamespaceAsSuite()
		{
			TestPackage package = new TestPackage( testsDll );
			package.TestName = "NUnit.Core.Tests";
			Test suite= builder.Build( package );
			Assert.IsNotNull( suite );
			Assert.AreEqual( testsDll, suite.TestName.Name );
			Assert.AreEqual( "NUnit", ((Test)suite.Tests[0]).TestName.Name );
		}

		[Test]
		public void DiscoverSuite()
		{
			TestPackage package = new TestPackage( testData );
			package.TestName = "NUnit.TestData.LegacySuiteData.Suite";
			Test suite = builder.Build( package );
			Assert.IsNotNull(suite, "Could not discover suite attribute");
		}

		[Test]
		public void WrongReturnTypeSuite()
		{
			TestPackage package = new TestPackage( testData );
			package.TestName = "NUnit.TestData.LegacySuiteData.NonConformingSuite";
			Test suite = builder.Build( package );
			Assert.AreEqual(RunState.NotRunnable, suite.RunState);
            Assert.AreEqual("Suite property must return either TestSuite or IEnumerable", suite.IgnoreReason);
		}

		[Test]
		[ExpectedException(typeof(FileNotFoundException))]
		public void FileNotFound()
		{
			builder.Build( new TestPackage( "/xxxx.dll" ) );
		}

		// Gives FileNotFoundException on Mono
		[Test, ExpectedException(typeof(BadImageFormatException))]
		public void InvalidAssembly()
		{
			FileInfo file = new FileInfo( tempFile );

			StreamWriter sw = file.AppendText();

			sw.WriteLine("This is a new entry to add to the file");
			sw.WriteLine("This is yet another line to add...");
			sw.Flush();
			sw.Close();

			builder.Build( new TestPackage( tempFile ) );
		}

		[Test]
		public void FixtureNotFound()
		{
			TestPackage package = new TestPackage( testsDll );
			package.TestName = "NUnit.Tests.Junk";
			Assert.IsNull( builder.Build( package ) );
		}
	}
}
