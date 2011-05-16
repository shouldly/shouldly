// ****************************************************************
// Copyright 2002-2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

namespace NUnit.Util
{
	using System;
	using System.IO;
	using System.Collections;
	using System.Threading;
	using System.Configuration;
	using NUnit.Core;
	using NUnit.Core.Filters;

	/// <summary>
	/// TestLoader handles interactions between a test runner and a 
	/// client program - typically the user interface - for the 
	/// purpose of loading, unloading and running tests.
	/// 
	/// It implemements the EventListener interface which is used by 
	/// the test runner and repackages those events, along with
	/// others as individual events that clients may subscribe to
	/// in collaboration with a TestEventDispatcher helper object.
	/// 
	/// TestLoader is quite handy for use with a gui client because
	/// of the large number of events it supports. However, it has
	/// no dependencies on ui components and can be used independently.
	/// </summary>
	public class TestLoader : MarshalByRefObject, NUnit.Core.EventListener, ITestLoader, IService
	{
        static Logger log = InternalTrace.GetLogger(typeof(TestLoader));

		#region Instance Variables

		/// <summary>
		/// Our event dispatching helper object
		/// </summary>
		private TestEventDispatcher events;

        /// <summary>
        /// Our TestRunnerFactory
        /// </summary>
        private ITestRunnerFactory factory;

		/// <summary>
		/// Loads and executes tests. Non-null when
		/// we have loaded a test.
		/// </summary>
		private TestRunner testRunner = null;

		/// <summary>
		/// Our current test project, if we have one.
		/// </summary>
		private NUnitProject testProject = null;

		/// <summary>
		/// The currently loaded test, returned by the testrunner
		/// </summary>
		private ITest loadedTest = null;

		/// <summary>
		/// The test name that was specified when loading
		/// </summary>
		private string loadedTestName = null;

		/// <summary>
		/// The currently executing test
		/// </summary>
		private string currentTestName;

        /// <summary>
        /// The currently set runtime framework
        /// </summary>
        private RuntimeFramework currentRuntime;

		/// <summary>
		/// Result of the last test run
		/// </summary>
		private TestResult testResult = null;

		/// <summary>
		/// The last exception received when trying to load, unload or run a test
		/// </summary>
		private Exception lastException = null;

		/// <summary>
		/// Watcher fires when the assembly changes
		/// </summary>
		private AssemblyWatcher watcher;

		/// <summary>
		/// Assembly changed during a test and
		/// needs to be reloaded later
		/// </summary>
		private bool reloadPending = false;

		/// <summary>
		/// The last filter used for a run - used to 
		/// rerun tests when a change occurs
		/// </summary>
		private ITestFilter lastFilter;

        /// <summary>
        /// The runtime framework being used for the currently
        /// loaded tests, or the current framework if no tests
        /// are loaded.
        /// </summary>
        private RuntimeFramework currentFramework = RuntimeFramework.CurrentFramework;

		#endregion

		#region Constructors

		public TestLoader()
			: this( new TestEventDispatcher() ) { }

		public TestLoader(TestEventDispatcher eventDispatcher )
		{
			this.events = eventDispatcher;
            this.factory = new DefaultTestRunnerFactory();
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler( OnUnhandledException );
		}

		#endregion

		#region Properties
		public bool IsProjectLoaded
		{
			get { return testProject != null; }
		}

		public bool IsTestLoaded
		{
			get { return loadedTest != null; }
		}

		public bool Running
		{
			get { return testRunner != null && testRunner.Running; }
		}

		public NUnitProject TestProject
		{
			get { return testProject; }
		}

		public ITestEvents Events
		{
			get { return events; }
		}

		public string TestFileName
		{
			get { return testProject.ProjectPath; }
		}

		public TestResult TestResult
		{
			get { return testResult; }
		}

		public Exception LastException
		{
			get { return lastException; }
		}

		public IList AssemblyInfo
		{
			get { return testRunner == null ? null : testRunner.AssemblyInfo; }
		}

		public int TestCount
		{
			get { return loadedTest == null ? 0 : loadedTest.TestCount; }
		}

        public RuntimeFramework CurrentFramework
        {
            get { return currentFramework; }
        }
		#endregion

		#region EventListener Handlers

		public void RunStarted(string name, int testCount)
		{
            log.Debug("Got RunStarted Event");
			events.FireRunStarting( name, testCount );
		}

		public void RunFinished(NUnit.Core.TestResult testResult)
		{
			this.testResult = testResult;

			try
			{
				this.SaveLastResult( 
					Path.Combine( Path.GetDirectoryName( this.TestFileName ), "TestResult.xml" ) );
				events.FireRunFinished( testResult );
			}
			catch( Exception ex )
			{
				this.lastException = ex;
				events.FireRunFinished( ex );
			}
		}

		public void RunFinished(Exception exception)
		{
			this.lastException = exception;
			events.FireRunFinished( exception );
		}

		/// <summary>
		/// Trigger event when each test starts
		/// </summary>
		/// <param name="testName">TestName of the Test that is starting</param>
		public void TestStarted(TestName testName)
		{
			this.currentTestName = testName.FullName;
			events.FireTestStarting( testName );
		}

		/// <summary>
		/// Trigger event when each test finishes
		/// </summary>
		/// <param name="result">Result of the case that finished</param>
		public void TestFinished(TestResult result)
		{
			events.FireTestFinished( result );
		}

		/// <summary>
		/// Trigger event when each suite starts
		/// </summary>
		/// <param name="suite">Suite that is starting</param>
		public void SuiteStarted(TestName suiteName)
		{
			events.FireSuiteStarting( suiteName );
		}

		/// <summary>
		/// Trigger event when each suite finishes
		/// </summary>
		/// <param name="result">Result of the suite that finished</param>
		public void SuiteFinished(TestResult result)
		{
			events.FireSuiteFinished( result );
		}

		/// <summary>
		/// Trigger event when an unhandled exception (other than ThreadAbordException) occurs during a test
		/// </summary>
		/// <param name="exception">The unhandled exception</param>
		public void UnhandledException(Exception exception)
		{
			events.FireTestException( this.currentTestName, exception );
		}

		void OnUnhandledException( object sender, UnhandledExceptionEventArgs args )
		{
			switch( args.ExceptionObject.GetType().FullName )
			{
				case "System.Threading.ThreadAbortException":
					break;
				case "NUnit.Framework.AssertionException":
				default:
                    Exception ex = args.ExceptionObject as Exception;
					events.FireTestException( this.currentTestName, ex);
					break;
			}
		}

		/// <summary>
		/// Trigger event when output occurs during a test
		/// </summary>
		/// <param name="testOutput">The test output</param>
		public void TestOutput(TestOutput testOutput)
		{
			events.FireTestOutput( testOutput );
		}

		#endregion

		#region Methods for Loading and Unloading Projects
		
		/// <summary>
		/// Create a new project with default naming
		/// </summary>
		public void NewProject()
		{
            log.Info("Creating empty project");
            try
			{
				events.FireProjectLoading( "New Project" );

				OnProjectLoad( Services.ProjectService.NewProject() );
			}
			catch( Exception exception )
			{
                log.Error("Project creation failed", exception);
                lastException = exception;
				events.FireProjectLoadFailed( "New Project", exception );
			}
		}

		/// <summary>
		/// Create a new project using a given path
		/// </summary>
		public void NewProject( string filePath )
		{
            log.Info("Creating project " + filePath);
            try
			{
				events.FireProjectLoading( filePath );

				NUnitProject project = new NUnitProject( filePath );

				project.Configs.Add( "Debug" );
				project.Configs.Add( "Release" );			
				project.IsDirty = false;

				OnProjectLoad( project );
			}
			catch( Exception exception )
			{
                log.Error("Project creation failed", exception);
                lastException = exception;
				events.FireProjectLoadFailed( filePath, exception );
			}
		}

		/// <summary>
		/// Load a new project, optionally selecting the config and fire events
		/// </summary>
		public void LoadProject( string filePath, string configName )
		{
			try
			{
                log.Info("Loading project {0}, {1} config", filePath, configName == null ? "default" : configName);
                events.FireProjectLoading(filePath);

				NUnitProject newProject = Services.ProjectService.LoadProject( filePath );
				if ( configName != null ) 
				{
					newProject.SetActiveConfig( configName );
					newProject.IsDirty = false;
				}

				OnProjectLoad( newProject );
			}
			catch( Exception exception )
			{
                log.Error("Project load failed", exception);
                lastException = exception;
				events.FireProjectLoadFailed( filePath, exception );
			}
		}

		/// <summary>
		/// Load a new project using the default config and fire events
		/// </summary>
		public void LoadProject( string filePath )
		{
			LoadProject( filePath, null );
		}

		/// <summary>
		/// Load a project from a list of assemblies and fire events
		/// </summary>
		public void LoadProject( string[] assemblies )
		{
			try
			{
                log.Info("Loading multiple assemblies as new project");
				events.FireProjectLoading( "New Project" );

				NUnitProject newProject = Services.ProjectService.WrapAssemblies( assemblies );

				OnProjectLoad( newProject );
			}
			catch( Exception exception )
			{
                log.Error("Project load failed", exception);
                lastException = exception;
				events.FireProjectLoadFailed( "New Project", exception );
			}
		}

		/// <summary>
		/// Unload the current project and fire events
		/// </summary>
		public void UnloadProject()
		{
			string testFileName = TestFileName;

            log.Info("Unloading project " + testFileName);
			try
			{
				events.FireProjectUnloading( testFileName );

				if ( IsTestLoaded )
					UnloadTest();

				testProject = null;

				events.FireProjectUnloaded( testFileName );
			}
			catch (Exception exception )
			{
                log.Error("Project unload failed", exception);
                lastException = exception;
				events.FireProjectUnloadFailed( testFileName, exception );
			}

		}

		/// <summary>
		/// Common operations done each time a project is loaded
		/// </summary>
		/// <param name="testProject">The newly loaded project</param>
		private void OnProjectLoad( NUnitProject testProject )
		{
			if ( IsProjectLoaded )
				UnloadProject();

			this.testProject = testProject;

			events.FireProjectLoaded( TestFileName );
		}

		#endregion

		#region Methods for Loading and Unloading Tests

		public void LoadTest()
		{
			LoadTest( null );
		}
		
		public void LoadTest( string testName )
		{
            log.Info("Loading tests for " + Path.GetFileName(TestFileName));

            long startTime = DateTime.Now.Ticks;

			try
			{
				events.FireTestLoading( TestFileName );

                TestPackage package = MakeTestPackage(testName);
				testRunner = factory.MakeTestRunner(package);

                bool loaded = testRunner.Load(package);

				loadedTest = testRunner.Test;
				loadedTestName = testName;
				testResult = null;
				reloadPending = false;
			
				if ( Services.UserSettings.GetSetting( "Options.TestLoader.ReloadOnChange", true ) )
					InstallWatcher( );

                if (loaded)
                {
                    this.currentFramework = package.Settings.Contains("RuntimeFramework")
                        ? package.Settings["RuntimeFramework"] as RuntimeFramework
                        : RuntimeFramework.CurrentFramework;

                    testProject.HasChangesRequiringReload = false;
                    events.FireTestLoaded(TestFileName, loadedTest);
                }
                else
                {
                    lastException = new ApplicationException(string.Format("Unable to find test {0} in assembly", testName));
                    events.FireTestLoadFailed(TestFileName, lastException);
                }
			}
			catch( FileNotFoundException exception )
			{
                log.Error("File not found", exception);
				lastException = exception;

				foreach( string assembly in TestProject.ActiveConfig.Assemblies )
				{
					if ( Path.GetFileNameWithoutExtension( assembly ) == exception.FileName &&
						!PathUtils.SamePathOrUnder( testProject.ActiveConfig.BasePath, assembly ) )
					{
						lastException = new ApplicationException( string.Format( "Unable to load {0} because it is not located under the AppBase", exception.FileName ), exception );
						break;
					}
				}

				events.FireTestLoadFailed( TestFileName, lastException );

                double loadTime = (double)(DateTime.Now.Ticks - startTime) / (double)TimeSpan.TicksPerSecond;
                log.Info("Load completed in {0} seconds", loadTime);
            }
			catch( Exception exception )
			{
                log.Error("Failed to load test", exception);

				lastException = exception;
				events.FireTestLoadFailed( TestFileName, exception );
			}
		}

		/// <summary>
		/// Unload the current test suite and fire the Unloaded event
		/// </summary>
		public void UnloadTest( )
		{
			if( IsTestLoaded )
			{
                log.Info("Unloading tests for " + Path.GetFileName(TestFileName));

				// Hold the name for notifications after unload
				string fileName = TestFileName;

				try
				{
					events.FireTestUnloading( fileName );

					RemoveWatcher();

					testRunner.Unload();
					testRunner = null;

					loadedTest = null;
					loadedTestName = null;
					testResult = null;
					reloadPending = false;

					events.FireTestUnloaded( fileName );
                    log.Info("Unload complete");
				}
				catch( Exception exception )
				{
                    log.Error("Failed to unload tests", exception);
                    lastException = exception;
					events.FireTestUnloadFailed( fileName, exception );
				}
			}
		}

		/// <summary>
		/// Reload the current test on command
		/// </summary>
		public void ReloadTest(RuntimeFramework framework)
		{
            log.Info("Reloading tests for " + Path.GetFileName(TestFileName));
			try
			{
				events.FireTestReloading( TestFileName );

                TestPackage package = MakeTestPackage(loadedTestName);
                if (framework != null)
                    package.Settings["RuntimeFramework"] = framework;

                testRunner.Unload();
                testRunner = factory.MakeTestRunner(package);

                if (testRunner.Load(package))
                    this.currentFramework = package.Settings.Contains("RuntimeFramework")
                        ? package.Settings["RuntimeFramework"] as RuntimeFramework
                        : RuntimeFramework.CurrentFramework;

                loadedTest = testRunner.Test;
                currentRuntime = framework;
				reloadPending = false;

                testProject.HasChangesRequiringReload = false;
                events.FireTestReloaded(TestFileName, loadedTest);

                log.Info("Reload complete");
			}
			catch( Exception exception )
			{
                log.Error("Reload failed", exception);
                lastException = exception;
				events.FireTestReloadFailed( TestFileName, exception );
			}
		}

        public void ReloadTest()
        {
            ReloadTest(currentRuntime);
        }

		/// <summary>
		/// Handle watcher event that signals when the loaded assembly
		/// file has changed. Make sure it's a real change before
		/// firing the SuiteChangedEvent. Since this all happens
		/// asynchronously, we use an event to let ui components
		/// know that the failure happened.
		/// </summary>
		public void OnTestChanged( string testFileName )
		{
			if ( Running )
				reloadPending = true;
			else
			{
				ReloadTest();

                if (lastFilter != null && Services.UserSettings.GetSetting("Options.TestLoader.RerunOnChange", false))
					testRunner.BeginRun( this, lastFilter );
			}
		}
		#endregion

		#region Methods for Running Tests
		/// <summary>
		/// Run all the tests
		/// </summary>
		public void RunTests()
		{
			RunTests( TestFilter.Empty );
		}

		/// <summary>
		/// Run selected tests using a filter
		/// </summary>
		/// <param name="filter">The filter to be used</param>
		public void RunTests( ITestFilter filter )
		{
			if ( !Running )
			{
                if (reloadPending || Services.UserSettings.GetSetting("Options.TestLoader.ReloadOnRun", false))
					ReloadTest();

				this.lastFilter = filter;
				testRunner.BeginRun( this, filter );
			}
		}

		/// <summary>
		/// Cancel the currently running test.
		/// Fail silently if there is none to
		/// allow for latency in the UI.
		/// </summary>
		public void CancelTestRun()
		{
			if ( Running )
				testRunner.CancelRun();
		}

		public IList GetCategories() 
		{
			CategoryManager categoryManager = new CategoryManager();
			categoryManager.AddAllCategories( this.loadedTest );
			ArrayList list = new ArrayList( categoryManager.Categories );
			list.Sort();
			return list;
		}
		#endregion

		public void SaveLastResult( string fileName )
		{
			new XmlResultWriter( fileName ).SaveTestResult(this.testResult);
		}

		#region Helper Methods

		/// <summary>
		/// Install our watcher object so as to get notifications
		/// about changes to a test.
		/// </summary>
		private void InstallWatcher()
		{
			if(watcher!=null) watcher.Stop();

			watcher = new AssemblyWatcher( 1000, TestProject.ActiveConfig.Assemblies.ToArray() );
			watcher.AssemblyChangedEvent += new AssemblyWatcher.AssemblyChangedHandler( OnTestChanged );
			watcher.Start();
		}

		/// <summary>
		/// Stop and remove our current watcher object.
		/// </summary>
		private void RemoveWatcher()
		{
			if ( watcher != null )
			{
				watcher.Stop();
				watcher = null;
			}
		}

		private TestPackage MakeTestPackage( string testName )
		{
			TestPackage package = TestProject.ActiveConfig.MakeTestPackage();
			package.TestName = testName;

            ISettings settings = Services.UserSettings;
            package.Settings["MergeAssemblies"] = settings.GetSetting("Options.TestLoader.MergeAssemblies", false);
            package.Settings["AutoNamespaceSuites"] = settings.GetSetting("Options.TestLoader.AutoNamespaceSuites", true);
            package.Settings["ShadowCopyFiles"] = settings.GetSetting("Options.TestLoader.ShadowCopyFiles", true);

            ProcessModel processModel = (ProcessModel)settings.GetSetting("Options.TestLoader.ProcessModel", ProcessModel.Default);
            DomainUsage domainUsage = (DomainUsage)settings.GetSetting("Options.TestLoader.DomainUsage", DomainUsage.Default);

            if (processModel != ProcessModel.Default && !package.Settings.Contains("ProcessModel"))
                package.Settings["ProcessModel"] = processModel;

            if (processModel != ProcessModel.Multiple && domainUsage == DomainUsage.Multiple
                    && !package.Settings.Contains("DomainUsage"))
                package.Settings["DomainUsage"] = domainUsage;
			
            return package;
		}
		#endregion

		#region InitializeLifetimeService Override
		public override object InitializeLifetimeService()
		{
			return null;
		}
		#endregion

		#region IService Members

		public void UnloadService()
		{
			// TODO:  Add TestLoader.UnloadService implementation
		}

		public void InitializeService()
		{
			// TODO:  Add TestLoader.InitializeService implementation
		}

		#endregion
	}
}
