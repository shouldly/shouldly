// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.IO;
using System.Text;
using System.Collections;

using NUnit.Framework;
using NUnit.Core;
using NUnit.TestData.ConsoleRunnerTest;
using NUnit.Tests.Assemblies;

namespace NUnit.ConsoleRunner.Tests
{
	[TestFixture]
	public class ConsoleRunnerTest
	{
        private static readonly string failureMsg = 
            string.Format( "Errors: {0}, Failures: {1}", 
                MockAssembly.Errors, MockAssembly.Failures );

		private static readonly string xmlFile = "console-test.xml";
		private StringBuilder output;
		TextWriter saveOut;

		[SetUp]
		public void Init()
		{
			output = new StringBuilder();

			Console.Out.Flush();
			saveOut = Console.Out;
			Console.SetOut( new StringWriter( output ) );
		}

		[TearDown]
		public void CleanUp()
		{
			Console.SetOut( saveOut );

			FileInfo file = new FileInfo(xmlFile);
			if(file.Exists) file.Delete();

			file = new FileInfo( "TestResult.xml" );
			if(file.Exists) file.Delete();
		}

		[Test]
		public void FailureFixture() 
		{
			int resultCode = runFixture( typeof ( FailureTest ) ); 
			Assert.AreEqual(1, resultCode);
		}

		[Test]
		public void MultiFailureFixture() 
		{
			int resultCode = runFixture( typeof ( MultiFailureTest ) ); 
			Assert.AreEqual(3, resultCode);
		}

		[Test]
		public void SuccessFixture()
		{
			int resultCode = runFixture( 
				typeof(SuccessTest) );
			Assert.AreEqual(0, resultCode);
		}

		[Test]
		public void XmlResult() 
		{
			FileInfo info = new FileInfo(xmlFile);
			info.Delete();

			int resultCode = runFixture( 
				typeof(SuccessTest),
				"-xml:" + info.FullName );

			Assert.AreEqual(0, resultCode);
			Assert.AreEqual(true, info.Exists);
		}

		[Test]
		public void InvalidFixture()
		{
			int resultCode = executeConsole( new string[] 
				{ GetType().Module.Name, "-fixture:NUnit.Tests.BogusTest" } );
			Assert.AreEqual(ConsoleUi.FIXTURE_NOT_FOUND, resultCode);
		}

		[Test]
		public void AssemblyNotFound()
		{
            int resultCode = executeConsole(new string[] { "badassembly.dll" });
            Assert.AreEqual(ConsoleUi.FILE_NOT_FOUND, resultCode);
        }

        [Test]
        public void OneOfTwoAssembliesNotFound()
        {
            int resultCode = executeConsole(new string[] { GetType().Module.Name, "badassembly.dll" });
            Assert.AreEqual(ConsoleUi.FILE_NOT_FOUND, resultCode);
        }

		[Test]
		public void XmlToConsole() 
		{
			int resultCode = runFixture( 
				typeof(SuccessTest),
				"-xmlconsole", 
				"-nologo" );

			Assert.AreEqual(0, resultCode);
			StringAssert.Contains( @"<?xml version=""1.0""", output.ToString(),
				"Only XML should be displayed in xmlconsole mode");
		}

		[Test]
		public void Bug1073539Test()
		{
			int resultCode = runFixture( typeof( Bug1073539Fixture ) );
			Assert.AreEqual( 1, resultCode );
		}

		[Test]
		public void Bug1311644Test()
		{
			int resultCode = runFixture( typeof( Bug1311644Fixture ) );
			Assert.AreEqual( 1, resultCode );
		}

		[Test]
		public void CanRunWithoutTestDomain()
		{
            Assert.AreEqual(MockAssembly.ErrorsAndFailures, executeConsole("mock-assembly.dll", "-domain:None", "-process:single"));
			StringAssert.Contains( failureMsg, output.ToString() );
		}

		[Test]
		public void CanRunWithSingleTestDomain()
		{
            Assert.AreEqual(MockAssembly.ErrorsAndFailures, executeConsole("mock-assembly.dll", "-domain:Single", "-process:single"));
			StringAssert.Contains( failureMsg, output.ToString() );
		}

		[Test]
		public void CanRunWithMultipleTestDomains()
		{
            Assert.AreEqual(MockAssembly.ErrorsAndFailures, executeConsole("mock-assembly.dll", "nonamespace-assembly.dll", "-domain:Multiple", "-process:single"));
			StringAssert.Contains( failureMsg, output.ToString() );
		}

		[Test,Platform(Exclude="Mono")]
		public void CanRunWithoutTestDomain_NoThread()
		{
            Assert.AreEqual(MockAssembly.ErrorsAndFailures, executeConsole("mock-assembly.dll", "-domain:None", "-nothread", "-process:single"));
			StringAssert.Contains( failureMsg, output.ToString() );
		}

		[Test,Platform(Exclude="Mono")]
		public void CanRunWithSingleTestDomain_NoThread()
		{
            Assert.AreEqual(MockAssembly.ErrorsAndFailures, executeConsole("mock-assembly.dll", "-domain:Single", "-nothread", "-process:single"));
			StringAssert.Contains( failureMsg, output.ToString() );
		}

		[Test,Platform(Exclude="Mono")]
		public void CanRunWithMultipleTestDomains_NoThread()
		{
            Assert.AreEqual(MockAssembly.ErrorsAndFailures, executeConsole("mock-assembly.dll", "nonamespace-assembly.dll", "-domain:Multiple", "-nothread", "-process:single"));
			StringAssert.Contains( failureMsg, output.ToString() );
		}

		private int runFixture( Type type )
		{
			return executeConsole( new string[] { type.Module.Name, "-process:single", "-fixture:" + type.FullName });
		}

		private int runFixture( Type type, params string[] arguments )
		{
			string[] args = new string[arguments.Length+3];
			int n = 0;
			args[n++] = type.Module.Name;
            args[n++] = "-process:single";
			args[n++] = "-fixture:" + type.FullName;
			foreach( string arg in arguments )
				args[n++] = arg;
			return executeConsole( args ); 
		}

		private int executeConsole( params string[] arguments )
		{
			return NUnit.ConsoleRunner.Runner.Main( arguments );
		}
	}
}
