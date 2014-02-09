// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.IO;
using System.Threading;
using NUnit.Core;
using NUnit.Framework;
using NUnit.Tests.Assemblies;

namespace NUnit.Util.Tests
{
	/// <summary>
	/// 
	/// </summary>
	[TestFixture]
	public class TestLoaderAssemblyTests
	{
		private readonly string assembly = MockAssembly.AssemblyPath;
		private readonly string badFile = "/x.dll";

		private TestLoader loader;
		private TestEventCatcher catcher;

		private void LoadTest( string assembly )
		{
			loader.LoadProject( assembly );
			if (loader.IsProjectLoaded && loader.TestProject.IsLoadable)
				loader.LoadTest();
		}
		
		[SetUp]
		public void SetUp()
		{
			loader = new TestLoader( );
			catcher = new TestEventCatcher( loader.Events );
		}

		[TearDown]
		public void TearDown()
		{
			if ( loader.IsTestLoaded )
				loader.UnloadTest();

			if ( loader.IsProjectLoaded )
				loader.UnloadProject();
		}

		[Test]
		public void LoadProject()
		{
			loader.LoadProject( assembly );
			Assert.IsTrue(loader.IsProjectLoaded,  "Project not loaded");
			Assert.IsFalse(loader.IsTestLoaded,  "Test should not be loaded");
			Assert.AreEqual( 2, catcher.Events.Count );
			Assert.AreEqual( TestAction.ProjectLoading, ((TestEventArgs)catcher.Events[0]).Action );
			Assert.AreEqual( TestAction.ProjectLoaded, ((TestEventArgs)catcher.Events[1]).Action );
		}

		[Test]
		public void UnloadProject()
		{
			loader.LoadProject( assembly );
			loader.UnloadProject();
			Assert.IsFalse( loader.IsProjectLoaded, "Project not unloaded" );
			Assert.IsFalse( loader.IsTestLoaded, "Test not unloaded" );
			Assert.AreEqual( 4, catcher.Events.Count );
			Assert.AreEqual( TestAction.ProjectUnloading, ((TestEventArgs)catcher.Events[2]).Action );
			Assert.AreEqual( TestAction.ProjectUnloaded, ((TestEventArgs)catcher.Events[3]).Action );
		}

		[Test]
		public void LoadTest()
		{
			LoadTest( assembly );
			Assert.IsTrue( loader.IsProjectLoaded, "Project not loaded" );
			Assert.IsTrue( loader.IsTestLoaded, "Test not loaded" );
			Assert.AreEqual( 4, catcher.Events.Count );
			Assert.AreEqual( TestAction.TestLoading, ((TestEventArgs)catcher.Events[2]).Action );
			Assert.AreEqual( TestAction.TestLoaded, ((TestEventArgs)catcher.Events[3]).Action );
			Assert.AreEqual( MockAssembly.Tests, ((TestEventArgs)catcher.Events[3]).TestCount );
		}

		[Test]
		public void UnloadTest()
		{
			LoadTest( assembly );
			loader.UnloadTest();
			Assert.AreEqual( 6, catcher.Events.Count );
			Assert.AreEqual( TestAction.TestUnloading, ((TestEventArgs)catcher.Events[4]).Action );
			Assert.AreEqual( TestAction.TestUnloaded, ((TestEventArgs)catcher.Events[5]).Action );
		}

		[Test]
		public void FileNotFound()
		{
			LoadTest( "xxxxx" );
			Assert.IsFalse( loader.IsProjectLoaded, "Project should not load" );
			Assert.IsFalse( loader.IsTestLoaded, "Test should not load" );
			Assert.AreEqual( 2, catcher.Events.Count );
			Assert.AreEqual( TestAction.ProjectLoadFailed, ((TestEventArgs)catcher.Events[1]).Action );
			Assert.AreEqual( typeof( FileNotFoundException ), ((TestEventArgs)catcher.Events[1]).Exception.GetType() );
		}

		// Doesn't work under .NET 2.0 Beta 2
		//[Test]
		public void InvalidAssembly()
		{
			FileInfo file = new FileInfo(badFile);
			try
			{
				StreamWriter sw = file.AppendText();
				sw.WriteLine("This is a new entry to add to the file");
				sw.WriteLine("This is yet another line to add...");
				sw.Flush();
				sw.Close();

				LoadTest(badFile);
				Assert.IsTrue(loader.IsProjectLoaded, "Project not loaded");
				Assert.IsFalse(loader.IsTestLoaded, "Test should not be loaded");
				Assert.AreEqual(4, catcher.Events.Count);
				Assert.AreEqual(TestAction.TestLoadFailed, ((TestEventArgs)catcher.Events[3]).Action);
				Assert.AreEqual(typeof(BadImageFormatException), ((TestEventArgs)catcher.Events[3]).Exception.GetType());
			}
			finally
			{
				if ( file.Exists )
				    file.Delete();
			}
		}

		[Test]
		public void AssemblyWithNoTests()
		{
			LoadTest( "nunit.framework.dll" );
			Assert.IsTrue( loader.IsProjectLoaded, "Project not loaded" );
			Assert.IsTrue( loader.IsTestLoaded, "Test not loaded" );
			Assert.AreEqual( 4, catcher.Events.Count );
			Assert.AreEqual( TestAction.TestLoaded, ((TestEventArgs)catcher.Events[3]).Action );
		}

		// TODO: Should wrapper project be unloaded on failure?

		[Test]
		public void RunTest()
		{
            //loader.ReloadOnRun = false;
			
			LoadTest( assembly );
			loader.RunTests(TestFilter.Empty);
			do 
			{
				// TODO: Find a more robust way of handling this
				Thread.Sleep( 500 );
			}
			while( !catcher.GotRunFinished );

            Assert.AreEqual(TestAction.ProjectLoading, ((TestEventArgs)catcher.Events[0]).Action);
            Assert.AreEqual(TestAction.ProjectLoaded, ((TestEventArgs)catcher.Events[1]).Action);
            Assert.AreEqual(TestAction.TestLoading, ((TestEventArgs)catcher.Events[2]).Action);
            Assert.AreEqual(TestAction.TestLoaded, ((TestEventArgs)catcher.Events[3]).Action);
            Assert.AreEqual(TestAction.RunStarting, ((TestEventArgs)catcher.Events[4]).Action);

            int eventCount = 4 /* for loading */+ 2 * (MockAssembly.Nodes - MockAssembly.Explicit);
            if (eventCount != catcher.Events.Count)
                foreach (TestEventArgs e in catcher.Events)
                    Console.WriteLine(e.Action);
            Assert.AreEqual(eventCount, catcher.Events.Count);

            Assert.AreEqual(TestAction.RunFinished, ((TestEventArgs)catcher.Events[eventCount - 1]).Action);

			int nTests = 0;
			int nRun = 0;
			foreach( object o in catcher.Events )
			{
				TestEventArgs e = o as TestEventArgs;

				if ( e != null && e.Action == TestAction.TestFinished )
				{
					++nTests;
					if ( e.Result.Executed )
						++nRun;
				}
			}
			Assert.AreEqual( MockAssembly.ResultCount, nTests );
			Assert.AreEqual( MockAssembly.TestsRun, nRun );
		}
	}
}
