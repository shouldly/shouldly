// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************
using System;
using System.Threading;

namespace NUnit.Core
{
    /// <summary>
    /// Represents a thread of test execution and runs a test
    /// on a thread, implementing timeout and setting the 
    /// apartment state appropriately.
    /// </summary>
    public abstract class TestThread
    {
        static Logger log = InternalTrace.GetLogger(typeof(TestThread));

        #region Private Fields
        /// <summary>
        /// The TestMethod or TestSuite to be run on the thread
        /// </summary>
        private Test test;

        /// <summary>
        /// The Thread object used to run tests
        /// </summary>
        protected Thread thread;

        /// <summary>
        /// The result of the test being run
        /// </summary>
        protected TestResult testResult;

        protected EventListener listener;

        protected ITestFilter filter;

        /// <summary>
        /// Unexpected exception thrown by test thread
        /// </summary>
        protected Exception thrownException;
        #endregion

        #region Constructor
        protected TestThread(Test test)
        {
            this.test = test;

            this.thread = new Thread(new ThreadStart(RunTestProc));
            thread.CurrentCulture = Thread.CurrentThread.CurrentCulture;
            thread.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;

            // Setting to Unknown causes an error under the Mono 1.0 profile
            if ( test.ApartmentState != ApartmentState.Unknown )
                this.ApartmentState = test.ApartmentState;
        }
        #endregion

        #region Properties
        public ApartmentState ApartmentState
        {
#if NET_2_0
            get { return thread.GetApartmentState(); }
            set { thread.SetApartmentState(value); }
#else
            get { return thread.ApartmentState; }
            set { thread.ApartmentState = value; }
#endif
        }
        #endregion

        /// <summary>
        /// Run the test, honoring any timeout value provided. If the
        /// timeout is exceeded, set the testresult as a failure. As
        /// currently implemented, the thread proc calls test.doRun,
        /// which handles all exceptions itself. However, for safety,
        /// any exception thrown is rethrown upwards.
        /// 
        /// TODO: It would be cleaner to call test.Run, since that's
        /// part of the pubic interface, but it would require some
        /// restructuring of the Test hierarchy.
        /// </summary>
        public void Run(TestResult testResult, EventListener listener, ITestFilter filter)
        {
            this.thrownException = null;
            this.testResult = testResult;
            this.listener = listener;
            this.filter = filter;

            log.Debug("Starting TestThread");
            thread.Start();
            thread.Join(this.Timeout);
            log.Debug("Join Complete");

            // Timeout?
            if (thread.IsAlive)
            {
                thread.Abort();
                //thread.Join();
                testResult.Failure(string.Format("Test exceeded Timeout value of {0}ms", Timeout), null);
            }

            // Handle any exception communicated back from the thread, ie. from proc!
            if (thrownException != null && thrownException.GetType() != typeof(ThreadAbortException))
                throw thrownException;
        }

        /// <summary>
        /// This is the engine of this class; the actual call to test.doRun!
        /// Note that any thrown exception is saved for later use!
        /// </summary>
        private void RunTestProc()
        {
            try
            {
                RunTest();
            }
            catch (Exception e)
            {
                thrownException = e;
            }
        }

        protected abstract int Timeout { get; }
        protected abstract void RunTest();
    }

    public class TestMethodThread : TestThread
    {
        private TestMethod testMethod;

        public TestMethodThread(TestMethod testMethod)
            : base(testMethod)
        {
            this.testMethod = testMethod;
        }

        protected override int Timeout
        {
            get 
            { 
                return testMethod.Timeout == 0 || System.Diagnostics.Debugger.IsAttached
                    ? System.Threading.Timeout.Infinite
                    : testMethod.Timeout;
            }
        }

        protected override void RunTest()
        {
            testMethod.doRun(testResult);
        }
    }

    public class TestSuiteThread : TestThread
    {
        private TestSuite suite;

        public TestSuiteThread(TestSuite suite)
            : base(suite)
        {
            this.suite = suite;
        }

        protected override int Timeout
        {
            get { return System.Threading.Timeout.Infinite; }
        }

        protected override void RunTest()
        {
            suite.Run(testResult, listener, filter);
        }
    }
}
