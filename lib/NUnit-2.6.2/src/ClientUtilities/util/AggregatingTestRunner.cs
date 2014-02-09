// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

namespace NUnit.Util
{
	using System;
	using System.Collections;
	using System.IO;
	using NUnit.Core;

    #region AggregatingTestRunner
    /// <summary>
	/// AggregatingTestRunner allows running multiple TestRunners
	/// and combining the results.
	/// </summary>
    
	public abstract class AggregatingTestRunner : MarshalByRefObject, TestRunner, EventListener
	{
        private Logger log;
        private Logger Log
        {
            get
            {
                if (log == null)
                    log = InternalTrace.GetLogger(this.GetType());

                return log;
            }
        }

		static int AggregateTestID = 1000;

		#region Instance Variables

		/// <summary>
		/// Our runner ID
		/// </summary>
		protected int runnerID;

		/// <summary>
		/// The downstream TestRunners
		/// </summary>
		protected ArrayList runners;

        /// <summary>
        /// Indicates whether we should run test assemblies in parallel
        /// </summary>
        private bool runInParallel;

		/// <summary>
		/// The loaded test suite
		/// </summary>
		protected TestNode aggregateTest;

		/// <summary>
		/// The result of the last run
		/// </summary>
		private TestResult testResult;

		/// <summary>
		/// The event listener for the currently running test
		/// </summary>
		protected EventListener listener;

		protected TestName testName;

		#endregion

		#region Constructors
		public AggregatingTestRunner() : this( 0 ) { }
		public AggregatingTestRunner( int runnerID )
		{
			this.runnerID = runnerID;
			this.testName = new TestName();
			testName.TestID = new TestID( AggregateTestID );
			testName.RunnerID = this.runnerID;
			testName.FullName = testName.Name = "Not Loaded";
		}
		#endregion

		#region Properties

		public virtual int ID
		{
			get { return runnerID; }
		}

		public virtual bool Running
		{
			get 
			{ 
				foreach( TestRunner runner in runners )
					if ( runner.Running )
						return true;
			
				return false;
			}
		}

		public virtual IList AssemblyInfo
		{
			get
			{
				ArrayList info = new ArrayList();
				foreach( TestRunner runner in runners )
					info.AddRange( runner.AssemblyInfo );
				return info;
			}
		}

		public virtual ITest Test
		{
			get
			{
				if ( aggregateTest == null && runners != null )
				{
					// Count non-null tests, in case we specified a fixture
					int count = 0;
					foreach( TestRunner runner in runners )
						if ( runner.Test != null )
							++count;  

					// Copy non-null tests to an array
					int index = 0;
					ITest[] tests = new ITest[count];
					foreach( TestRunner runner in runners )
						if ( runner.Test != null )
							tests[index++] = runner.Test;

					// Return master node containing all the tests
					aggregateTest = new TestNode( testName, tests );
				}

				return aggregateTest;
			}
		}

		public virtual TestResult TestResult
		{
			get { return testResult; }
		}
		#endregion

		#region Load and Unload Methods
        public bool Load(TestPackage package)
        {
            Log.Info("Loading " + package.Name);

            this.testName.FullName = this.testName.Name = package.FullName;
            runners = new ArrayList();

            int nfound = 0;
            int index = 0;

            string targetAssemblyName = null;
            if (package.TestName != null && package.Assemblies.Contains(package.TestName))
            {
                targetAssemblyName = package.TestName;
                package.TestName = null;
            }

            // NOTE: This is experimental. A normally created test package
            // will never have this setting.
            if (package.Settings.Contains("RunInParallel"))
            {
                this.runInParallel = true;
                package.Settings.Remove("RunInParallel");
            }

            //string basePath = package.BasePath;
            //if (basePath == null)
            //    basePath = Path.GetDirectoryName(package.FullName);

            //string configFile = package.ConfigurationFile;
            //if (configFile == null && package.Name != null && !package.IsSingleAssembly)
            //    configFile = Path.ChangeExtension(package.Name, ".config");

            foreach (string assembly in package.Assemblies)
            {
                if (targetAssemblyName == null || targetAssemblyName == assembly)
                {
                    TestRunner runner = CreateRunner(this.runnerID * 100 + index + 1);

                    TestPackage p = new TestPackage(assembly);
                    p.AutoBinPath = package.AutoBinPath;
                    p.ConfigurationFile = package.ConfigurationFile;
                    p.BasePath = package.BasePath;
                    p.PrivateBinPath = package.PrivateBinPath;
                    p.TestName = package.TestName;
                    foreach (object key in package.Settings.Keys)
                        p.Settings[key] = package.Settings[key];

                    if (package.TestName == null)
                    {
                        runners.Add(runner);
                        if (runner.Load(p))
                            nfound++;
                    }
                    else if (runner.Load(p))
                    {
                        runners.Add(runner);
                        nfound++;
                    }
                }
            }

            Log.Info("Load complete");

            if (package.TestName == null && targetAssemblyName == null)
                return nfound == package.Assemblies.Count;
            else
                return nfound > 0;
        }

        protected abstract TestRunner CreateRunner(int runnerID);

		public virtual void Unload()
		{
            if (aggregateTest != null)
                Log.Info("Unloading " + Path.GetFileName(aggregateTest.TestName.Name));

            if (runners != null)
                foreach (TestRunner runner in runners)
                    runner.Unload();

            aggregateTest = null;
            Log.Info("Unload complete");
		}
		#endregion

		#region CountTestCases
		public virtual int CountTestCases( ITestFilter filter )
		{
			int count = 0;
			foreach( TestRunner runner in runners )
				count += runner.CountTestCases( filter );
			return count;
		}
		#endregion

		#region Methods for Running Tests

		public virtual TestResult Run(EventListener listener, ITestFilter filter, bool tracing, LoggingThreshold logLevel)
		{
            Log.Info("Run - EventListener={0}", listener.GetType().Name);

			// Save active listener for derived classes
			this.listener = listener;

			ITest[] tests = new ITest[runners.Count];
			for( int index = 0; index < runners.Count; index++ )
				tests[index] = ((TestRunner)runners[index]).Test;

            string name = this.testName.Name;
            int count = this.CountTestCases(filter);
            Log.Info("Signalling RunStarted({0},{1})", name, count);
            this.listener.RunStarted(name, count);

			long startTime = DateTime.Now.Ticks;

		    TestResult result = new TestResult(new TestInfo(testName, tests));

            if (this.runInParallel)
            {
                foreach (TestRunner runner in runners)
                    if (filter.Pass(runner.Test))
                        runner.BeginRun(this, filter, tracing, logLevel);

                result = this.EndRun();
            }
            else
            {
                foreach (TestRunner runner in runners)
                    if (filter.Pass(runner.Test))
                        result.AddResult(runner.Run(this, filter, tracing, logLevel));
            }
			
			long stopTime = DateTime.Now.Ticks;
			double time = ((double)(stopTime - startTime)) / (double)TimeSpan.TicksPerSecond;
			result.Time = time;

			this.listener.RunFinished( result );

			this.testResult = result;

			return result;
		}

		public virtual void BeginRun( EventListener listener, ITestFilter filter, bool tracing, LoggingThreshold logLevel )
		{
			// Save active listener for derived classes
			this.listener = listener;

            Log.Info("BeginRun");

            // ThreadedTestRunner will call our Run method on a separate thread
            ThreadedTestRunner threadedRunner = new ThreadedTestRunner(this);
            threadedRunner.BeginRun(listener, filter, tracing, logLevel);
		}

		public virtual TestResult EndRun()
		{
            Log.Info("EndRun");
            TestResult suiteResult = new TestResult(Test as TestInfo);
			foreach( TestRunner runner in runners )
				suiteResult.Results.Add( runner.EndRun() );

			return suiteResult;
		}

		public virtual void CancelRun()
		{
			foreach( TestRunner runner in runners )
				runner.CancelRun();
		}

		public virtual void Wait()
		{
			foreach( TestRunner runner in runners )
				runner.Wait();
		}
        #endregion

		#region EventListener Members
		public void TestStarted(TestName testName)
		{
			this.listener.TestStarted( testName );
		}

		public void RunStarted(string name, int testCount)
		{
			// TODO: We may want to count how many runs are started
			// Ignore - we provide our own
		}

		public void RunFinished(Exception exception)
		{
			// Ignore - we provide our own
		}

		void NUnit.Core.EventListener.RunFinished(TestResult result)
		{
            if (this.runInParallel)
            {
                foreach (TestRunner runner in runners)
                    if (runner.Running)
                        return;

                this.testResult = new TestResult(this.aggregateTest);
                foreach (TestRunner runner in runners)
                    this.testResult.AddResult(runner.TestResult);

                listener.RunFinished(this.TestResult);
            }
		}

		public void SuiteFinished(TestResult result)
		{
			this.listener.SuiteFinished( result );
		}

		public void TestFinished(TestResult result)
		{
			this.listener.TestFinished( result );
		}

		public void UnhandledException(Exception exception)
		{
			this.listener.UnhandledException( exception );
		}

		public void TestOutput(TestOutput testOutput)
		{
			this.listener.TestOutput( testOutput );
		}

		public void SuiteStarted(TestName suiteName)
		{
			this.listener.SuiteStarted( suiteName );
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
            foreach (TestRunner runner in runners)
                if (runner != null)
                    runner.Dispose();
        }

        #endregion
    }
    #endregion

    #region MultipleTestDomainRunner
    /// <summary>
    /// Summary description for MultipleTestDomainRunner.
    /// </summary>
    public class MultipleTestDomainRunner : AggregatingTestRunner
    {
        #region Constructors
        public MultipleTestDomainRunner() : base(0) { }

        public MultipleTestDomainRunner(int runnerID) : base(runnerID) { }
        #endregion

        #region CreateRunner
        protected override TestRunner CreateRunner(int runnerID)
        {
            return new TestDomain(runnerID);
        }
        #endregion
    }
    #endregion

    #region MultipleTestProcessRunner
#if CLR_2_0 || CLR_4_0
    public class MultipleTestProcessRunner : AggregatingTestRunner
    {
        #region Constructors
        public MultipleTestProcessRunner() : base(0) { }

        public MultipleTestProcessRunner(int runnerID) : base(runnerID) { }
        #endregion

        #region CreateRunner
        protected override TestRunner CreateRunner(int runnerID)
        {
            return new ProcessRunner(runnerID);
        }
        #endregion
    }
#endif
    #endregion
}
