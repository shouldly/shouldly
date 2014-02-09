// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.IO;
using System.Collections;

using NUnit.Framework;
using NUnit.Core;
using NUnit.Util;
using NUnit.Tests.Assemblies;

namespace NUnit.Core.Tests
{
	/// <summary>
	/// Base class for tests of various kinds of runners. The derived
	/// class should use SetUp to create the runner and assign it to
	/// the protected field runner and perform any cleanup in TearDown.
	/// If creating the runner is resource intensive, it may be possible
	/// to use TestFixtureSetUp and TestFixtureTearDown instead. 
	/// </summary>
	public abstract class BasicRunnerTests
	{
		private static readonly string testsDll = NoNamespaceTestFixture.AssemblyPath;
		private static readonly string mockDll = MockAssembly.AssemblyPath;
		private readonly string[] assemblies = new string[] { testsDll, mockDll };
        private readonly RuntimeFramework currentFramework = RuntimeFramework.CurrentFramework;

		protected TestRunner runner;
        private TestPackage package1;
        private TestPackage package2;

		[TestFixtureSetUp]
		public void SetUpRunner()
		{
			runner = CreateRunner( 123 );
			Assert.IsNotNull( runner, "Derived test class failed to set runner" );
		}

        [TestFixtureTearDown]
        public void TearDownRunner()
        {
            DestroyRunner();
        }

        [SetUp]
        public void CreatePackages()
        {
            package1 = new TestPackage(MockAssembly.AssemblyPath);
            package2 = new TestPackage("TestSuite", assemblies);

            // Set current framework explicitly to avoid running out of process
            // unless explicitly called for by derived test.
            package1.Settings["RuntimeFramework"] = currentFramework;
            package2.Settings["RuntimeFramework"] = currentFramework;
        }

		protected abstract TestRunner CreateRunner( int runnerID );

        protected virtual void DestroyRunner()
        {
            if (runner != null)
                runner.Unload();
        }

        [Test]
        public void CheckRunnerID()
        {
            Assert.AreEqual(123, runner.ID);
        }

        [Test]
		public virtual void LoadAndReloadAssembly() 
		{
            Assert.IsTrue(runner.Load(package1), "Unable to load assembly");
            runner.Unload();
            Assert.IsTrue(runner.Load(package1), "Unable to reload assembly");
        }

		[Test]
		public void LoadAssemblyWithoutNamespaces()
		{
            package1.Settings["AutoNamespaceSuites"] = false;
            Assert.IsTrue(runner.Load(package1), "Unable to load assembly");
			ITest test = runner.Test;
			Assert.IsNotNull( test );
			Assert.AreEqual( MockAssembly.Classes, test.Tests.Count );
			Assert.AreEqual( "MockTestFixture", ((ITest)test.Tests[0]).TestName.Name );
		}

		[Test]
		public void LoadAssemblyWithFixture()
		{
			package1.TestName = "NUnit.Tests.Assemblies.MockTestFixture";
			Assert.IsTrue( runner.Load( package1 ) );
		}

		[Test]
		public void LoadAssemblyWithSuite()
		{
			package1.TestName = "NUnit.Tests.Assemblies.MockSuite";
			runner.Load( package1 );
			Assert.IsNotNull(runner.Test, "Unable to build suite");
		}

		[Test]
		public void CountTestCases()
		{
			runner.Load(package1);
			Assert.AreEqual( MockAssembly.Tests, runner.Test.TestCount );
		}

		[Test]
		public void LoadMultipleAssemblies()
		{
			runner.Load( package2 );
			Assert.IsNotNull( runner.Test, "Unable to load assemblies" );
		}

		[Test]
		public void LoadMultipleAssembliesWithFixture()
		{
            package2.TestName = "NUnit.Tests.Assemblies.MockTestFixture";
			runner.Load( package2 );
			Assert.IsNotNull(runner.Test, "Unable to build suite");
		}

		[Test]
		public void LoadMultipleAssembliesWithSuite()
		{
            package2.TestName = "NUnit.Tests.Assemblies.MockSuite";
			runner.Load( package2 );
			Assert.IsNotNull(runner.Test, "Unable to build suite");
		}

		[Test]
		public void CountTestCasesAcrossMultipleAssemblies()
		{
			runner.Load(package2);
			Assert.AreEqual( NoNamespaceTestFixture.Tests + MockAssembly.Tests, 
				runner.Test.TestCount );			
		}

		[Test]
		public void RunAssembly()
		{
			runner.Load(package1);
			TestResult result = runner.Run( NullListener.NULL, TestFilter.Empty, false, LoggingThreshold.Off );
			ResultSummarizer summary = new ResultSummarizer(result);
			Assert.AreEqual( MockAssembly.Tests - MockAssembly.NotRun, summary.TestsRun );
		}

		[Test]
		public void RunAssemblyUsingBeginAndEndRun()
		{
			runner.Load(package1);
			runner.BeginRun( NullListener.NULL, TestFilter.Empty, false, LoggingThreshold.Off );
			TestResult result = runner.EndRun();
			Assert.IsNotNull( result );
			ResultSummarizer summary = new ResultSummarizer( result );
			Assert.AreEqual( MockAssembly.Tests - MockAssembly.NotRun, summary.TestsRun );
		}

		[Test]
		public void RunMultipleAssemblies()
		{
			runner.Load( package2 );
			TestResult result = runner.Run( NullListener.NULL, TestFilter.Empty, false, LoggingThreshold.Off );
			ResultSummarizer summary = new ResultSummarizer(result);
			Assert.AreEqual( 
				NoNamespaceTestFixture.Tests + MockAssembly.Tests - MockAssembly.NotRun, 
				summary.TestsRun);
		}

		[Test]
		public void RunMultipleAssembliesUsingBeginAndEndRun()
		{
			runner.Load( package2 );
			runner.BeginRun( NullListener.NULL, TestFilter.Empty, false, LoggingThreshold.Off );
			TestResult result = runner.EndRun();
			Assert.IsNotNull( result );
			ResultSummarizer summary = new ResultSummarizer( result );
			Assert.AreEqual( 
				NoNamespaceTestFixture.Tests + MockAssembly.Tests - MockAssembly.NotRun, 
				summary.TestsRun);
		}
	}
}
