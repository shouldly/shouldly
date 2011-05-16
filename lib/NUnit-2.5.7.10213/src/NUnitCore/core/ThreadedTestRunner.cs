// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************
namespace NUnit.Core
{
	using System;
	using System.Threading;
	using System.Collections.Specialized;

	/// <summary>
	/// ThreadedTestRunner overrides the Run and BeginRun methods 
	/// so that they are always run on a separate thread. The actual
	/// </summary>
	public class ThreadedTestRunner : ProxyTestRunner
	{
        static Logger log = InternalTrace.GetLogger(typeof(ThreadedTestRunner));

		#region Instance Variables
		private TestRunnerThread testRunnerThread;
		#endregion

		#region Constructors
		public ThreadedTestRunner( TestRunner testRunner ) : base ( testRunner ) { }
		#endregion

		#region Overrides
		public override TestResult Run( EventListener listener )
		{
			BeginRun( listener );
			return EndRun();
		}

		public override TestResult Run( EventListener listener, ITestFilter filter )
		{
			BeginRun( listener, filter );
			return EndRun();
		}

		public override void BeginRun( EventListener listener )
		{
            log.Info("BeginRun");
   			testRunnerThread = new TestRunnerThread( this.TestRunner );
            testRunnerThread.StartRun( listener );
		}

		public override void BeginRun( EventListener listener, ITestFilter filter )
		{
            log.Info("BeginRun");
            testRunnerThread = new TestRunnerThread(this.TestRunner);
			testRunnerThread.StartRun( listener, filter );
		}

		public override TestResult EndRun()
		{
            log.Info("EndRun");
            this.Wait();
			return this.TestRunner.TestResult;
		}


		public override void Wait()
		{
			if ( testRunnerThread != null )
				testRunnerThread.Wait();
		}

		public override void CancelRun()
		{
			if ( testRunnerThread != null )
				testRunnerThread.Cancel();
		}

		#endregion
	}
}
