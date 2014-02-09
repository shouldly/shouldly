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
        private ApartmentState apartmentState;
        private ThreadPriority priority;

		#endregion

		#region Constructor

        public ThreadedTestRunner(TestRunner testRunner) 
            : this(testRunner, ApartmentState.Unknown, ThreadPriority.Normal) { }

        public ThreadedTestRunner(TestRunner testRunner, ApartmentState apartmentState, ThreadPriority priority)
            : base(testRunner)
        {
            this.apartmentState = apartmentState;
            this.priority = priority;
        }

		#endregion

		#region Overrides

        public override TestResult Run(EventListener listener, ITestFilter filter, bool tracing, LoggingThreshold logLevel)
        {
            BeginRun(listener, filter, tracing, logLevel);
            return EndRun();
        }

        public override void BeginRun(EventListener listener, ITestFilter filter, bool tracing, LoggingThreshold logLevel)
        {
            log.Info("BeginRun");
            testRunnerThread = new TestRunnerThread(this.TestRunner, this.apartmentState, this.priority);
            testRunnerThread.StartRun(listener, filter, tracing, logLevel);
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
