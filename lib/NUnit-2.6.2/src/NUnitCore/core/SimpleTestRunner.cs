// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************
using System;
using System.IO;
using System.Threading;
using System.Collections;
using System.Collections.Specialized;
using NUnit.Core.Filters;
using System.Reflection;

namespace NUnit.Core
{
	/// <summary>
	/// SimpleTestRunner is the simplest direct-running TestRunner. It
	/// passes the event listener interface that is provided on to the tests
	/// to use directly and does nothing to redirect text output. Both
	/// Run and BeginRun are actually synchronous, although the client
	/// can usually ignore this. BeginRun + EndRun operates as expected.
	/// </summary>
	public class SimpleTestRunner : MarshalByRefObject, TestRunner
	{
        static Logger log = InternalTrace.GetLogger(typeof(SimpleTestRunner));

		#region Instance Variables

		/// <summary>
		/// Identifier for this runner. Must be unique among all
		/// active runners in order to locate tests. Default
		/// value of 0 is adequate in applications with a single
		/// runner or a non-branching chain of runners.
		/// </summary>
		private int runnerID = 0;

		/// <summary>
		/// The loaded test suite
		/// </summary>
		private Test test;

		/// <summary>
		/// The builder we use to load tests, created for each load
		/// </summary>
		private TestSuiteBuilder builder;

		/// <summary>
		/// Results from the last test run
		/// </summary>
		private TestResult testResult;

		/// <summary>
		/// The thread on which Run was called. Set to the
		/// current thread while a run is in process.
		/// </summary>
		private Thread runThread;

		#endregion

		#region Constructor
		public SimpleTestRunner() : this( 0 ) { }

		public SimpleTestRunner( int runnerID )
		{
			this.runnerID = runnerID;
		}
		#endregion

		#region Properties
		public virtual int ID
		{
			get { return runnerID; }
		}

		public IList AssemblyInfo
		{
			get { return builder.AssemblyInfo; }
		}
		
		public ITest Test
		{
			get { return test == null ? null : new TestNode( test ); }
		}

		/// <summary>
		/// Results from the last test run
		/// </summary>
		public TestResult TestResult
		{
			get { return testResult; }
		}

		public virtual bool Running
		{
			get { return runThread != null && runThread.IsAlive; }
		}
		#endregion

		#region Methods for Loading Tests
		/// <summary>
		/// Load a TestPackage
		/// </summary>
		/// <param name="package">The package to be loaded</param>
		/// <returns>True on success, false on failure</returns>
		public bool Load( TestPackage package )
		{
            log.Debug("Loading package " + package.Name);

			this.builder = new TestSuiteBuilder();

			this.test = builder.Build( package );
			if ( test == null ) return false;

			test.SetRunnerID( this.runnerID, true );
            TestExecutionContext.CurrentContext.TestPackage = package;
			return true;
		}

		/// <summary>
		/// Unload all tests previously loaded
		/// </summary>
		public void Unload()
		{
            log.Debug("Unloading");
			this.test = null; // All for now
		}
		#endregion

		#region CountTestCases
		public int CountTestCases( ITestFilter filter )
		{
			return test.CountTestCases( filter );
		}
		#endregion

		#region Methods for Running Tests

		public virtual TestResult Run( EventListener listener, ITestFilter filter, bool tracing, LoggingThreshold logLevel )
		{
			try
			{
                log.Debug("Starting test run");

				// Take note of the fact that we are running
				this.runThread = Thread.CurrentThread;

				listener.RunStarted( this.Test.TestName.FullName, test.CountTestCases( filter ) );
				
				testResult = test.Run( listener, filter );

				// Signal that we are done
				listener.RunFinished( testResult );
                log.Debug("Test run complete");

				// Return result array
				return testResult;
			}
			catch( Exception exception )
			{
				// Signal that we finished with an exception
				listener.RunFinished( exception );
				// Rethrow - should we do this?
				throw;
			}
			finally
			{
				runThread = null;
			}
		}

        public void BeginRun(EventListener listener, ITestFilter filter, bool tracing, LoggingThreshold logLevel)
        {
            testResult = this.Run(listener, filter, tracing, logLevel);
        }

        public virtual TestResult EndRun()
		{
			return TestResult;
		}

		/// <summary>
		/// Wait is a NOP for SimpleTestRunner
		/// </summary>
		public virtual void Wait()
		{
		}

		public virtual void CancelRun()
		{
			if (this.runThread != null)
			{
				// Cancel Synchronous run only if on another thread
				if ( this.runThread == Thread.CurrentThread )
					throw new InvalidOperationException( "May not CancelRun on same thread that is running the test" );

                ThreadUtility.Kill(this.runThread);
			}
		}
		#endregion

        #region InitializeLifetimeService Override
        public override object InitializeLifetimeService()
        {
            return null;
        }
	#endregion

        #region IDisposable Members

        public void Dispose()
        {
            Unload();
        }

        #endregion
    }
}
