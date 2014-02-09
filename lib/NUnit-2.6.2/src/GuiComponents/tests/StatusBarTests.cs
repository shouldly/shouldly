// ****************************************************************
// Copyright 2002-2003, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;
using NUnit.Core;
using NUnit.Util;
using NUnit.Tests.Assemblies;
using NUnit.TestUtilities;

namespace NUnit.UiKit.Tests
{
	/// <summary>
	/// Summary description for StatusBarTests.
	/// </summary>
	[TestFixture]
	public class StatusBarTests
	{
		private StatusBar statusBar;
		private MockTestEventSource mockEvents;
		private static string testsDll = MockAssembly.AssemblyPath;
		TestSuite suite;
	    int testCount = 0;

		[SetUp]
		public void Setup()
		{
			statusBar = new StatusBar();

			TestSuiteBuilder builder = new TestSuiteBuilder();
			suite = builder.Build( new TestPackage( testsDll ) );

			mockEvents = new MockTestEventSource( suite );
		}

		[Test]
		public void TestConstruction()
		{
			Assert.AreEqual( "Status", statusBar.Panels[0].Text );
			Assert.AreEqual( "Test Cases : 0", statusBar.Panels[1].Text );
			Assert.AreEqual( "", statusBar.Panels[2].Text );
			Assert.AreEqual( "", statusBar.Panels[3].Text );
			Assert.AreEqual( "", statusBar.Panels[4].Text );
		}

		[Test]
		public void TestInitialization()
		{
			statusBar.Initialize( 0 );
			Assert.AreEqual( "", statusBar.Panels[0].Text );
			Assert.AreEqual( "Test Cases : 0", statusBar.Panels[1].Text );
			Assert.AreEqual( "", statusBar.Panels[2].Text );
			Assert.AreEqual( "", statusBar.Panels[3].Text );
			Assert.AreEqual( "", statusBar.Panels[4].Text );

			statusBar.Initialize( 50 );
			Assert.AreEqual( "Ready", statusBar.Panels[0].Text );
			Assert.AreEqual( "Test Cases : 50", statusBar.Panels[1].Text );
			Assert.AreEqual( "", statusBar.Panels[2].Text );
			Assert.AreEqual( "", statusBar.Panels[3].Text );
			Assert.AreEqual( "", statusBar.Panels[4].Text );
		}

		[Test]
		public void TestFinalDisplay()
		{
			Assert.AreEqual( false, statusBar.DisplayTestProgress );
			statusBar.Subscribe( mockEvents );

			mockEvents.SimulateTestRun();
			Assert.AreEqual( "Completed", statusBar.Panels[0].Text );
			Assert.AreEqual( 
				PanelMessage( "Test Cases", MockAssembly.Tests ), 
				statusBar.Panels[1].Text );
			Assert.AreEqual( 
				PanelMessage( "Tests Run", MockAssembly.TestsRun ),
				statusBar.Panels[2].Text );
			Assert.AreEqual( 
				PanelMessage( "Errors", MockAssembly.Errors ),
				statusBar.Panels[3].Text );
			Assert.AreEqual( 
				PanelMessage( "Failures", MockAssembly.Failures ),
				statusBar.Panels[4].Text );
		}

        // .NET 1.0 sometimes throws:
        // ExternalException : A generic error occurred in GDI+.
        [Test, Platform(Exclude = "Net-1.0")]
        public void TestProgressDisplay()
		{
			statusBar.DisplayTestProgress = true;
			statusBar.Subscribe( mockEvents );

			testCount = 0;
            testCount = 0;
			mockEvents.TestFinished += new TestEventHandler( OnTestFinished );

			mockEvents.SimulateTestRun();
			Assert.AreEqual( "Completed", statusBar.Panels[0].Text );
			Assert.AreEqual( 
				PanelMessage( "Test Cases", MockAssembly.Tests ), 
				statusBar.Panels[1].Text );
			Assert.AreEqual( 
				PanelMessage( "Tests Run", MockAssembly.TestsRun ),
				statusBar.Panels[2].Text );
			Assert.AreEqual( 
				PanelMessage( "Errors", MockAssembly.Errors ),
				statusBar.Panels[3].Text );
			Assert.AreEqual(
				PanelMessage( "Failures", MockAssembly.Failures ),
				statusBar.Panels[4].Text );
		}

		private void OnTestFinished( object sender, TestEventArgs e )
		{
			Assert.AreEqual( 
				PanelMessage( "Test Cases", MockAssembly.Tests ),
				statusBar.Panels[1].Text );

            StringAssert.EndsWith( e.Result.Test.TestName.Name, statusBar.Panels[0].Text );
            
		    string runPanel = statusBar.Panels[2].Text;
		    int testsRun = 0;
            if (runPanel != "")
            {
                StringAssert.StartsWith( "Tests Run : ", runPanel );
                testsRun = int.Parse(runPanel.Substring(12));
            }

            Assert.GreaterOrEqual( testsRun, testCount);
            Assert.LessOrEqual( testsRun, testCount + 1);
		    testCount = testsRun;
   		}

		private static string PanelMessage( string text, int count )
		{
			return string.Format( "{0} : {1}", text, count );
		}
	}
}
