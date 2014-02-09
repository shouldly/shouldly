// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.Diagnostics;
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
        private static readonly int expectedReturnCode =
            MockAssembly.Errors + MockAssembly.Failures + MockAssembly.NotRunnable;

		private static readonly string xmlFile = Path.Combine(Path.GetTempPath(), "console-test.xml");
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
            int resultCode = runFixture(typeof(FailureTest), "-noxml"); 
			Assert.AreEqual(1, resultCode);
		}

		[Test]
		public void MultiFailureFixture() 
		{
            int resultCode = runFixture(typeof(MultiFailureTest), "-noxml"); 
			Assert.AreEqual(3, resultCode);
		}

		[Test]
		public void SuccessFixture()
		{
            int resultCode = runFixture(typeof(SuccessTest), "-noxml");
			Assert.AreEqual(0, resultCode);
		}

		[Test]
		public void XmlResult() 
		{
			FileInfo info = new FileInfo(xmlFile);
			info.Delete();

			int resultCode = runFixture(typeof(SuccessTest), "-xml:" + xmlFile);

			Assert.AreEqual(0, resultCode);
			Assert.AreEqual(true, info.Exists);
		}

		[Test]
		public void InvalidFixture()
		{
			int resultCode = executeConsole( new string[] { MockAssembly.AssemblyPath, "-fixture:NUnit.Tests.BogusTest", "-noxml" });
			Assert.AreEqual(ConsoleUi.FIXTURE_NOT_FOUND, resultCode);
		}

		[Test]
		public void AssemblyNotFound()
		{
            int resultCode = executeConsole(new string[] { "badassembly.dll", "-noxml" });
            Assert.AreEqual(ConsoleUi.FILE_NOT_FOUND, resultCode);
        }

        [Test]
        public void OneOfTwoAssembliesNotFound()
        {
            int resultCode = executeConsole(new string[] { GetType().Module.Name, "badassembly.dll", "-noxml" });
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
			int resultCode = runFixture( typeof( Bug1073539Fixture ), "-noxml" );
			Assert.AreEqual( 1, resultCode );
		}

		[Test]
		public void Bug1311644Test()
		{
			int resultCode = runFixture( typeof( Bug1311644Fixture ), "-noxml" );
			Assert.AreEqual( 1, resultCode );
		}

		[Test]
		public void CanRunWithoutTestDomain()
		{
            Assert.AreEqual(expectedReturnCode, executeConsole(MockAssembly.AssemblyPath, "-domain:None", "-noxml"));
			StringAssert.Contains( failureMsg, output.ToString() );
		}

		[Test]
		public void CanRunWithSingleTestDomain()
		{
            Assert.AreEqual(expectedReturnCode, executeConsole(MockAssembly.AssemblyPath, "-domain:Single", "-noxml"));
			StringAssert.Contains( failureMsg, output.ToString() );
		}

		[Test]
		public void CanRunWithMultipleTestDomains()
		{
            Assert.AreEqual(expectedReturnCode, executeConsole(MockAssembly.AssemblyPath, NoNamespaceTestFixture.AssemblyPath, "-domain:Multiple", "-noxml"));
			StringAssert.Contains( failureMsg, output.ToString() );
		}

		[Test]
		public void CanRunWithoutTestDomain_NoThread()
		{
            Assert.AreEqual(expectedReturnCode, executeConsole(MockAssembly.AssemblyPath, "-domain:None", "-nothread", "-noxml"));
			StringAssert.Contains( failureMsg, output.ToString() );
		}

		[Test]
		public void CanRunWithSingleTestDomain_NoThread()
		{
            Assert.AreEqual(expectedReturnCode, executeConsole(MockAssembly.AssemblyPath, "-domain:Single", "-nothread", "-noxml"));
			StringAssert.Contains( failureMsg, output.ToString() );
		}

		[Test]
		public void CanRunWithMultipleTestDomains_NoThread()
		{
            Assert.AreEqual(expectedReturnCode, executeConsole(MockAssembly.AssemblyPath, NoNamespaceTestFixture.AssemblyPath, "-domain:Multiple", "-nothread", "-noxml"));
			StringAssert.Contains( failureMsg, output.ToString() );
		}

        [Test]
        public void CanSpecifyBasePathAndPrivateBinPath()
        {
            // Assuming mock assembly is at ...x/y/z/mock-assembly.dll
            string basePath = Path.GetDirectoryName(MockAssembly.AssemblyPath); // ...x/y/z
            string privateBinPath = Path.GetFileName(basePath); // z
            basePath = Path.GetDirectoryName(basePath); // ...x/y
            privateBinPath = Path.Combine(Path.GetFileName(basePath), privateBinPath); // y/z
            basePath = Path.GetDirectoryName(basePath); // ...x

            Assert.AreEqual(expectedReturnCode, executeConsole("mock-assembly.dll", "-basepath=" + basePath, "-privatebinpath=" + privateBinPath, "-noxml"));
            StringAssert.Contains( failureMsg, output.ToString());
        }

		[Test]
		public void DoesNotFailWithEmptyRunList()
		{
			string path = Path.GetTempFileName();

			int returnCode = runFixture(typeof(SuccessTest), "-runlist=" + path, "-noxml");
			Assert.AreEqual(0, returnCode);
			StringAssert.Contains("Tests run: 0", output.ToString());

			File.Delete(path);
		}

		[Test]
		public void DoesNotFailIfRunListHasEmptyLines()
		{
			string path = Path.GetTempFileName();

			using(StreamWriter writer = File.CreateText(path))
				writer.WriteLine();

			int returnCode = runFixture(typeof(SuccessTest), "-runlist=" + path, "-noxml");
			Assert.AreEqual(0, returnCode);
			StringAssert.Contains("Tests run: 0", output.ToString());
		
			File.Delete(path);
		}

		[Test]
		public void FailsGracefullyIfRunListPointsToNonExistingFile()
		{
			int returnCode = runFixture(typeof(SuccessTest), "-runlist=NonExistingFile.txt");
			Assert.AreEqual(ConsoleUi.INVALID_ARG, returnCode);
			StringAssert.Contains("NonExistingFile.txt", output.ToString());
		}


		[Test]
		public void FailsGracefullyIfRunListPointsToNonExistingDirectory()
		{
			int returnCode = runFixture(typeof(SuccessTest), "-runlist=NonExistingDirectory\\NonExistingFile.txt");
			Assert.AreEqual(ConsoleUi.INVALID_ARG, returnCode);
			StringAssert.Contains("NonExistingDirectory", output.ToString());
		}

		private int runFixture( Type type )
		{
            return executeConsole(new string[] { AssemblyHelper.GetAssemblyPath(type), "-fixture:" + type.FullName, "-noxml" });
		}

		private int runFixture( Type type, params string[] arguments )
		{
			string[] args = new string[arguments.Length+2];
			int n = 0;
			args[n++] = AssemblyHelper.GetAssemblyPath(type);
			args[n++] = "-fixture:" + type.FullName;
			foreach( string arg in arguments )
				args[n++] = arg;
			return executeConsole( args ); 
		}

        // Run test in process using console. For test purposes,
        // avoid use of another process and turn trace off.
        private int executeConsole( params string[] arguments )
		{
            int n = 0;
#if CLR_2_0 || CLR_4_0
            string[] args = new string[arguments.Length + 2];
            args[n++] = "-process:single";
#else
            string[] args = new string[arguments.Length + 1];
#endif
            args[n++] = "-trace:Off";
            foreach (string arg in arguments)
                args[n++] = arg;
			return NUnit.ConsoleRunner.Runner.Main( args );
		}
	}
}
