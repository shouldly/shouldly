// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************
//#define DEFAULT_APPLIES_TO_TESTCASE

using System.Diagnostics;

namespace NUnit.Core
{
	using System;
    using System.Collections;
    using System.Runtime.Remoting.Messaging;
    using System.Threading;
	using System.Text;
	using System.Text.RegularExpressions;
	using System.Reflection;

#if CLR_2_0 || CLR_4_0
    using System.Collections.Generic;
#endif

	/// <summary>
	/// The TestMethod class represents a Test implemented as a method.
	/// 
	/// Because of how exceptions are handled internally, this class
	/// must incorporate processing of expected exceptions. A change to
	/// the Test interface might make it easier to process exceptions
	/// in an object that aggregates a TestMethod in the future.
	/// </summary>
	public abstract class TestMethod : Test
	{
        static Logger log = InternalTrace.GetLogger(typeof(TestMethod));
        
		#region Fields
		/// <summary>
		/// The test method
		/// </summary>
		internal MethodInfo method;

		/// <summary>
		/// The SetUp method.
		/// </summary>
		protected MethodInfo[] setUpMethods;

		/// <summary>
		/// The teardown method
		/// </summary>
		protected MethodInfo[] tearDownMethods;

#if CLR_2_0 || CLR_4_0
        /// <summary>
        /// The actions
        /// </summary>
	    protected TestAction[] actions;

        /// <summary>
        /// The parent suite's actions
        /// </summary>
        protected TestAction[] suiteActions;
#endif

        /// <summary>
        /// The ExpectedExceptionProcessor for this test, if any
        /// </summary>
        internal ExpectedExceptionProcessor exceptionProcessor;

        /// <summary>
        /// Arguments to be used in invoking the method
        /// </summary>
	    internal object[] arguments;

        /// <summary>
        /// The expected result of the method return value
        /// </summary>
	    internal object expectedResult;

        /// <summary>
        /// Indicates whether expectedResult was set - thereby allowing null as a value
        /// </summary>
        internal bool hasExpectedResult;

        /// <summary>
        /// The fixture object, if it has been created
        /// </summary>
        private object fixture;

        private Exception builderException;

		#endregion

		#region Constructors
		public TestMethod( MethodInfo method ) 
			: base( method.ReflectedType.FullName, method.Name ) 
		{
            if( method.DeclaringType != method.ReflectedType)
                this.TestName.Name = method.DeclaringType.Name + "." + method.Name;

            this.method = method;
		}
		#endregion

		#region Properties

        public override string TestType
        {
            get { return "TestMethod"; }
        }

		public MethodInfo Method
		{
			get { return method; }
		}

        public override Type FixtureType
        {
            get { return method.ReflectedType; }
        }

        public override string MethodName
        {
            get { return method.Name; }
        }

        public ExpectedExceptionProcessor ExceptionProcessor
        {
            get { return exceptionProcessor; }
            set { exceptionProcessor = value; }
        }

		public bool ExceptionExpected
		{
            get { return exceptionProcessor != null; }
		}

        public override object Fixture
        {
            get { return fixture; }
            set { fixture = value; }
        }

        public int Timeout
        {
            get
            {
                return Properties.Contains("Timeout")
                    ? (int)Properties["Timeout"]
                    : TestExecutionContext.CurrentContext.TestCaseTimeout;
            }
        }
		
		protected override bool ShouldRunOnOwnThread 
		{
			get 
			{
                return base.ShouldRunOnOwnThread || Timeout > 0;
			}
		}

        public Exception BuilderException
        {
            get { return builderException; }
            set { builderException = value; }
        }
        #endregion

		#region Run Methods
        public override TestResult Run(EventListener listener, ITestFilter filter)
        {
            log.Debug("Test Starting: " + this.TestName.FullName);
            listener.TestStarted(this.TestName);
            long startTime = DateTime.Now.Ticks;

            TestResult testResult = this.RunState == RunState.Runnable || this.RunState == RunState.Explicit
				? RunTestInContext() : SkipTest();

			log.Debug("Test result = " + testResult.ResultState);

            long stopTime = DateTime.Now.Ticks;
            double time = ((double)(stopTime - startTime)) / (double)TimeSpan.TicksPerSecond;
            testResult.Time = time;

            listener.TestFinished(testResult);
            return testResult;
        }
		      
		private TestResult SkipTest()
		{
			TestResult testResult = new TestResult(this);
			
            switch (this.RunState)
            {
                case RunState.Skipped:
                default:
                    testResult.Skip(IgnoreReason);
                    break;
                case RunState.NotRunnable:
                    if (BuilderException != null)
                        testResult.Invalid(BuilderException);
                    else
                        testResult.Invalid(IgnoreReason);
                    break;
                case RunState.Ignored:
                    testResult.Ignore(IgnoreReason);
                    break;
            }
			
			return testResult;
		}
		
        private TestResult RunTestInContext()
		{
			TestExecutionContext.Save();

            TestExecutionContext.CurrentContext.CurrentTest = this;

            if (this.Parent != null)
            {
                this.Fixture = this.Parent.Fixture;
                TestSuite suite = this.Parent as TestSuite;
                if (suite != null)
                {
                    this.setUpMethods = suite.GetSetUpMethods();
                    this.tearDownMethods = suite.GetTearDownMethods();
#if CLR_2_0 || CLR_4_0
                    this.suiteActions = suite.GetTestActions();
#endif
                }
            }

            try
            {
#if CLR_2_0 || CLR_4_0
                this.actions = ActionsHelper.GetActionsFromAttributeProvider(method);
#endif

                // Temporary... to allow for tests that directly execute a test case);
                if (Fixture == null && !method.IsStatic)
                    Fixture = Reflect.Construct(this.FixtureType);

                if (this.Properties["_SETCULTURE"] != null)
                    TestExecutionContext.CurrentContext.CurrentCulture =
                        new System.Globalization.CultureInfo((string)Properties["_SETCULTURE"]);
                else if (this.Properties["SetCulture"] != null) // In case we are running NUnitLite tests
                    TestExecutionContext.CurrentContext.CurrentCulture =
                        new System.Globalization.CultureInfo((string)Properties["SetCulture"]);

                if (this.Properties["_SETUICULTURE"] != null)
                    TestExecutionContext.CurrentContext.CurrentUICulture =
                        new System.Globalization.CultureInfo((string)Properties["_SETUICULTURE"]);
                if (this.Properties["SetUICulture"] != null) // In case we are running NUnitLite tests
                    TestExecutionContext.CurrentContext.CurrentUICulture =
                        new System.Globalization.CultureInfo((string)Properties["SetUICulture"]);

				return RunRepeatedTest();
            }
            catch (Exception ex)
            {
				log.Debug("TestMethod: Caught " + ex.GetType().Name);
				
                if (ex is ThreadAbortException)
                    Thread.ResetAbort();

				TestResult testResult = new TestResult(this);
                RecordException(ex, testResult, FailureSite.Test);
				
				return testResult;
            }
            finally
            {
                Fixture = null;

                TestExecutionContext.Restore();
            }
		}
		
		// TODO: Repeated tests need to be implemented as separate tests
		// in the tree of tests. Once that is done, this method will no
		// longer be needed and RunTest can be called directly.
		private TestResult RunRepeatedTest()
		{
			TestResult testResult = null;
			
			int repeatCount = this.Properties.Contains("Repeat")
				? (int)this.Properties["Repeat"] : 1;
			
            while (repeatCount-- > 0)
            {
                testResult = ShouldRunOnOwnThread
                    ? new TestMethodThread(this).Run(NullListener.NULL, TestFilter.Empty)
                    : RunTest();

                if (testResult.ResultState == ResultState.Failure ||
                    testResult.ResultState == ResultState.Error ||
                    testResult.ResultState == ResultState.Cancelled)
                {
                    break;
                }
            }
			
			return testResult;
		}

		/// <summary>
		/// The doRun method is used to run a test internally.
		/// It assumes that the caller is taking care of any 
		/// TestFixtureSetUp and TestFixtureTearDown needed.
		/// </summary>
		/// <param name="testResult">The result in which to record success or failure</param>
		public virtual TestResult RunTest()
		{
			DateTime start = DateTime.Now;

			TestResult testResult = new TestResult(this);
			TestExecutionContext.CurrentContext.CurrentResult =  testResult;
			
			try
			{
                RunSetUp();
#if CLR_2_0 || CLR_4_0
			    RunBeforeActions(testResult);
#endif

				RunTestCase( testResult );
			}
			catch(Exception ex)
			{
                // doTestCase handles its own exceptions so
                // if we're here it's a setup exception
                if (ex is ThreadAbortException)
                    Thread.ResetAbort();

                RecordException(ex, testResult, FailureSite.SetUp);
			}
			finally 
			{
#if CLR_2_0 || CLR_4_0
                RunAfterActions(testResult);
#endif
				RunTearDown( testResult );

				DateTime stop = DateTime.Now;
				TimeSpan span = stop.Subtract(start);
				testResult.Time = (double)span.Ticks / (double)TimeSpan.TicksPerSecond;

                if (testResult.IsSuccess)
				{
					if (this.Properties.Contains("MaxTime"))
                	{
                    int elapsedTime = (int)Math.Round(testResult.Time * 1000.0);
                    int maxTime = (int)this.Properties["MaxTime"];

                    if (maxTime > 0 && elapsedTime > maxTime)
                        testResult.Failure(
                            string.Format("Elapsed time of {0}ms exceeds maximum of {1}ms",
                                elapsedTime, maxTime),
                            null);
					}
					
					if (testResult.IsSuccess && testResult.Message == null && 
					    Environment.CurrentDirectory != TestExecutionContext.CurrentContext.prior.CurrentDirectory)
					{
						// TODO: Introduce a warning result state in NUnit 3.0
						testResult.SetResult(ResultState.Success, "Warning: Test changed the CurrentDirectory", null);
					}
				}
			}
			
			log.Debug("Test result = " + testResult.ResultState);
				
			return testResult;
		}
		#endregion

		#region Invoke Methods by Reflection, Recording Errors

#if CLR_2_0 || CLR_4_0

        protected virtual void ExecuteActions(ActionPhase phase)
        {
            List<TestAction> targetActions = new List<TestAction>();

            if (this.suiteActions != null)
            {
                foreach (var action in this.suiteActions)
                {
                    if(action.DoesTarget(TestAction.TargetsTest))
                        targetActions.Add(action);
                }
            }

            if (this.actions != null)
            {
                foreach (var action in this.actions)
                {
#if DEFAULT_APPLIES_TO_TESTCASE
                    if (!(Parent is ParameterizedMethodSuite) && (action.DoesTarget(TestAction.TargetsDefault) || action.DoesTarget(TestAction.TargetsTest)))
#else
                    if (action.DoesTarget(TestAction.TargetsDefault) || (!(Parent is ParameterizedMethodSuite) && action.DoesTarget(TestAction.TargetsTest)))
#endif
                        targetActions.Add(action);
                }
            }

            ActionsHelper.ExecuteActions(phase, targetActions, this);
        }

        private void RunBeforeActions(TestResult testResult)
        {
            ExecuteActions(ActionPhase.Before);
        }

        private void RunAfterActions(TestResult testResult)
        {
            try
            {
                ExecuteActions(ActionPhase.After);
            }
            catch (Exception ex)
            {
                if (ex is NUnitException)
                    ex = ex.InnerException;
                // TODO: What about ignore exceptions in teardown?
                testResult.Error(ex, FailureSite.TearDown);
            }
        }
#endif

	    private void RunSetUp()
        {
            if (setUpMethods != null)
                foreach( MethodInfo setUpMethod in setUpMethods )
                    Reflect.InvokeMethod(setUpMethod, setUpMethod.IsStatic ? null : this.Fixture);
        }

		private void RunTearDown( TestResult testResult )
		{
			try
			{
                if (tearDownMethods != null)
                {
                    int index = tearDownMethods.Length;
                    while (--index >= 0)
                        Reflect.InvokeMethod(tearDownMethods[index], tearDownMethods[index].IsStatic ? null : this.Fixture);
                }
			}
			catch(Exception ex)
			{
				if ( ex is NUnitException )
					ex = ex.InnerException;

                RecordException(ex, testResult, FailureSite.TearDown);
			}
		}

		private void RunTestCase( TestResult testResult )
		{
            try
            {
                object result = RunTestMethod(testResult);

                if (this.hasExpectedResult)
                    NUnitFramework.Assert.AreEqual(expectedResult, result);

                testResult.Success();

                if (testResult.IsSuccess && exceptionProcessor != null)
                    exceptionProcessor.ProcessNoException(testResult);
            }
            catch (Exception ex)
            {
                if (ex is ThreadAbortException)
                    Thread.ResetAbort();

                if (exceptionProcessor == null)
                    RecordException(ex, testResult, FailureSite.Test);
                else
                    exceptionProcessor.ProcessException(ex, testResult);
            }
		}

	    protected virtual object RunTestMethod(TestResult testResult)
		{
            object fixture = this.method.IsStatic ? null : this.Fixture;
            
            return Reflect.InvokeMethod(this.method, fixture, this.arguments);
        }

	    #endregion

		#region Record Info About An Exception
		protected virtual void RecordException( Exception exception, TestResult testResult, FailureSite failureSite )
		{
            if (exception is NUnitException)
                exception = exception.InnerException;

            // Ensure that once a test is cancelled, it stays cancelled
            ResultState finalResultState = testResult.ResultState == ResultState.Cancelled
                ? ResultState.Cancelled
                : NUnitFramework.GetResultState(exception);

            testResult.SetResult(finalResultState, exception, failureSite);
		}
		#endregion
    }
}