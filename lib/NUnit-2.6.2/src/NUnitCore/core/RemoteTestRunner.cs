// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

namespace NUnit.Core
{
	using System;
    using System.Collections;
    using System.Diagnostics;
    using System.Reflection;
    using System.Threading;

	/// <summary>
	/// RemoteTestRunner is tailored for use as the initial runner to
	/// receive control in a remote domain. It provides isolation for the return
	/// value by using a ThreadedTestRunner and for the events through use of
	/// an EventPump.
	/// </summary>
	public class RemoteTestRunner : ProxyTestRunner
	{
		/// <summary>
		/// Returns a RemoteTestRunner in the target domain. This method
		/// is used in the domain that wants to get a reference to 
		/// a RemoteTestRunnner and not in the test domain itself.
		/// </summary>
		/// <param name="targetDomain">AppDomain in which to create the runner</param>
		/// <param name="ID">Id for the new runner to use</param>
		/// <returns></returns>
        public static RemoteTestRunner CreateInstance(AppDomain targetDomain, int ID)
        {
#if CLR_2_0 || CLR_4_0
            System.Runtime.Remoting.ObjectHandle oh = Activator.CreateInstance(
                targetDomain,
#else
			System.Runtime.Remoting.ObjectHandle oh = targetDomain.CreateInstance(
#endif
                Assembly.GetExecutingAssembly().FullName,
                typeof(RemoteTestRunner).FullName,
                false, BindingFlags.Default, null, new object[] { ID }, null, null, null);

            object obj = oh.Unwrap();
            return (RemoteTestRunner)obj;
        }

		static Logger log = InternalTrace.GetLogger("RemoteTestRunner");

		#region Constructors
		public RemoteTestRunner() : this( 0 ) { }

		public RemoteTestRunner( int runnerID ) : base( runnerID ) { }
		#endregion

		#region Method Overrides

		public override bool Load(TestPackage package)
		{
			log.Info("Loading Test Package " + package.Name );

			// Initialize ExtensionHost if not already done
			if ( !CoreExtensions.Host.Initialized )
				CoreExtensions.Host.InitializeService();

			// Delayed creation of downstream runner allows us to
			// use a different runner type based on the package
			bool useThreadedRunner = package.GetSetting( "UseThreadedRunner", true );
			
			TestRunner runner = new SimpleTestRunner( this.runnerID );
            if (useThreadedRunner)
            {
                ApartmentState apartmentState = (ApartmentState)package.GetSetting("ApartmentState", ApartmentState.Unknown);
                ThreadPriority priority = (ThreadPriority)package.GetSetting("ThreadPriority", ThreadPriority.Normal);
                runner = new ThreadedTestRunner(runner, apartmentState, priority);
            }

			this.TestRunner = runner;

			if( base.Load (package) )
			{
				log.Info("Loaded package successfully" );
				return true;
			}
			else
			{
				log.Info("Package load failed" );
				return false;
			}
		}

        public override void Unload()
        {
            log.Info("Unloading test package");

            base.Unload();
        }

		public override TestResult Run( EventListener listener, ITestFilter filter, bool tracing, LoggingThreshold logLevel )
		{
            log.Debug("Run");

            QueuingEventListener queue = new QueuingEventListener();

			StartTextCapture( queue, tracing, logLevel );

			using( EventPump pump = new EventPump( listener, queue.Events, true ) )
			{
				pump.Start();
				return base.Run( queue, filter, tracing, logLevel );
			}
		}

		public override void BeginRun( EventListener listener, ITestFilter filter, bool tracing, LoggingThreshold logLevel )
		{
            log.Debug("BeginRun");

			QueuingEventListener queue = new QueuingEventListener();

			StartTextCapture( queue, tracing, logLevel );

			EventPump pump = new EventPump( listener, queue.Events, true);
			pump.Start(); // Will run till RunFinished is received
			// TODO: Make sure the thread is cleaned up if we abort the run
		
			base.BeginRun( queue, filter, tracing, logLevel );
		}

		private void StartTextCapture( EventListener queue, bool tracing, LoggingThreshold logLevel )
		{
            TestExecutionContext.CurrentContext.Out = new EventListenerTextWriter(queue, TestOutputType.Out);
            TestExecutionContext.CurrentContext.Error = new EventListenerTextWriter(queue, TestOutputType.Error);

            TestExecutionContext.CurrentContext.Tracing = false;
            if (tracing)
            {
                TestExecutionContext.CurrentContext.TraceWriter = new EventListenerTextWriter(queue, TestOutputType.Trace);
                TestExecutionContext.CurrentContext.Tracing = true;
            }

            TestExecutionContext.CurrentContext.LogLevel = LoggingThreshold.Off;
            if (logLevel != LoggingThreshold.Off)
            {
                TestExecutionContext.CurrentContext.LogWriter = new EventListenerTextWriter(queue, TestOutputType.Log);
                TestExecutionContext.CurrentContext.LogLevel = logLevel;
            }
        }

		#endregion

		private void CurrentDomain_DomainUnload(object sender, EventArgs e)
		{
			log.Debug(AppDomain.CurrentDomain.FriendlyName + " unloaded");
			InternalTrace.Flush();
		}
	}
}
