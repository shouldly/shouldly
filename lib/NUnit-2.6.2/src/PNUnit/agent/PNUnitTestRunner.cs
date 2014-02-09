using System;
using System.IO;
using System.Threading;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Resources;


using PNUnit.Framework;

using NUnit.Core;
using NUnit.Util;

using log4net;

namespace PNUnit.Agent
{
    public class PNUnitTestRunner
    {
        private delegate TestResult RunTestWithTimeoutDelegate(ConsoleWriter outStream, TestDomain testDomain);
        private static readonly ILog log = LogManager.GetLogger(typeof(PNUnitTestRunner));
        private PNUnitTestInfo mPNUnitTestInfo;
        private AgentConfig mConfig;
        private static object obj = new object();
        private bool mbUseDomainPool = false;
        private int TEST_TIMEOUT = 600000;

        public PNUnitTestRunner(
            PNUnitTestInfo info,
            AgentConfig config)
        {
            mConfig = config;
            mPNUnitTestInfo = info;
            mbUseDomainPool = config.UseDomainPool;
        }

        public void Run()
        {
            log.Debug("Spawning a new thread");
            Thread thread = new Thread(new ThreadStart(ThreadProc));
            thread.Start();
        }

        private static Queue mFreeDomains = new Queue();

        private void ThreadProc()
        {
            TestResult result = null;

            TestDomain testDomain = null;

            TestConsoleAccess consoleAccess = new TestConsoleAccess();

            try
            {
                log.DebugFormat("Thread entered for Test {0}:{1} Assembly {2}",
                    mPNUnitTestInfo.TestName,
                    mPNUnitTestInfo.TestToRun,
                    mPNUnitTestInfo.AssemblyName);

                ConsoleWriter outStream = new ConsoleWriter(Console.Out);
                ConsoleWriter errorStream = new ConsoleWriter(Console.Error);

                testDomain = SetupTest(consoleAccess);

                if( testDomain == null )
                    return;

                log.Debug("Running tests");

                try
                {
                    if(mConfig.NoTimeout)
                    {
                        result = RunTest(outStream, testDomain);
                    }
                    else
                    {
                        RunTestWithTimeoutDelegate deleg = new RunTestWithTimeoutDelegate(
                            RunTest);
                        IAsyncResult ar = deleg.BeginInvoke(outStream, testDomain, null, new object());
                        if (!ar.AsyncWaitHandle.WaitOne(TEST_TIMEOUT, false))
                        {
                            testDomain.CancelRun();
                            throw new Exception("Test timeout exception");
                        }
                        else  
                        {
                            result = deleg.EndInvoke(ar);
                        }
                    }
                }
                catch( Exception e )
                {
                    result = BuildError(e, consoleAccess);
                    log.ErrorFormat("Error running test {0}", e.Message);
                }

            }
            finally
            {
                log.Info("Notifying the results");

                log.Debug("////////////////////////////////////////Notifying the results/////////////////////////");

                mPNUnitTestInfo.Services.NotifyResult(
                    mPNUnitTestInfo.TestName, BuildResult(result, consoleAccess, mPNUnitTestInfo));

                log.Debug("////////////////////////////////////////Results NOTIFIED/////////////////////////");
                result = null;

                ReleaseDomain(testDomain);
            }
        }

        private TestResult RunTest(ConsoleWriter outStream, TestDomain testDomain)
        {
            EventListener collector = new EventCollector( outStream );

            ITestFilter filter = new NUnit.Core.Filters.SimpleNameFilter(mPNUnitTestInfo.TestToRun);
            TestResult result =
                FindResult(
                mPNUnitTestInfo.TestToRun,
                testDomain.Run(collector, filter, false, LoggingThreshold.Off) );
            return result;
        }

        private void ReleaseDomain(TestDomain testDomain)
        {
#if !NET_2_0
            lock(obj)
#endif
            {
                lock( mFreeDomains.SyncRoot )
                {
                    log.Debug("************************ RELEASING A TESTDOMAIN ************************************");
                    if( mbUseDomainPool )
                    {
                        mFreeDomains.Enqueue(testDomain);
                    }
                    else
                    {
                        testDomain.Unload();
                    }
                }
            }
        }

        private TestDomain SetupTest(TestConsoleAccess consoleAccess)
        {
            try
            {
                TestDomain result;

                lock( mFreeDomains.SyncRoot )
                {
                    log.Debug(">Locking mFreeDomains.SyncRoot");

                    if( mbUseDomainPool && mFreeDomains.Count > 0 )
                    {
                        log.Debug("Reusing a previously created TestDomain");
                        result = mFreeDomains.Dequeue() as TestDomain;
                        CreatePNUnitServices(result, consoleAccess);
                        return result;
                    }

                    log.Debug("Creating a new TestDomain");
                    result = new TestDomain();

                    bool testLoaded = MakeTest(
                        result,
                        Path.Combine(mConfig.PathToAssemblies, mPNUnitTestInfo.AssemblyName),
                        GetShadowCopyCacheConfig());

                    log.Debug("MakeTest executed");

                    if( !testLoaded )
                    {
                        log.InfoFormat("Unable to locate test {0}", mPNUnitTestInfo.TestName);
                        TestResult testResult = BuildError("Unable to locate tests", consoleAccess);

                        mPNUnitTestInfo.Services.NotifyResult(
                            mPNUnitTestInfo.TestName, testResult);

                        return null;
                    }

                    log.Debug("Test loaded, going to set CurrentDirectory");

                    Directory.SetCurrentDirectory(mConfig.PathToAssemblies); // test directory ?

                    log.Debug("Creating PNUnit services");

                    CreatePNUnitServices(result, consoleAccess);

                    return result;
                }
            }
            finally
            {
                log.Debug("<Unlocking mFreeDomains.SyncRoot");
            }
        }

        private void CreatePNUnitServices(
            TestDomain testDomain,
            TestConsoleAccess consoleAccess)
        {
            log.Info("Creating PNUnitServices in the AppDomain of the test");

            object[] param = { mPNUnitTestInfo, consoleAccess };

            try
            {
                System.Runtime.Remoting.ObjectHandle obj
#if NET_2_0
                        = Activator.CreateInstance(
                            testDomain.AppDomain,
#else
                    = testDomain.AppDomain.CreateInstance(
#endif
                    typeof(PNUnitServices).Assembly.FullName,
                    typeof(PNUnitServices).FullName,
                    false, BindingFlags.Default, null, param, null, null, null);
                obj.Unwrap();
            }
            catch( Exception e )
            {
                BuildError(e, consoleAccess);
                log.ErrorFormat("Error running test {0}", e.Message);
                return;
            }
        }

        private TestResult BuildResult(
            TestResult result,
            TestConsoleAccess consoleAccess,
            PNUnitTestInfo testInfo)
        {
            //the test namespace contains errors
            if( result == null )
            {
                TestName testName = new TestName();
                testName.Name = testInfo.TestName;

                string errormsg = "The test {0} couldn't be found in the assembly {1}";

                result = new PNUnitTestResult(testName, string.Empty);
                result.Failure(
                    string.Format(errormsg, testInfo.TestToRun, testInfo.AssemblyName),
                    string.Empty);

                return result;
            }

            if( !result.IsSuccess /*|| ReturnTestOutput()*/ )
                return new PNUnitTestResult(result, consoleAccess.GetTestOutput());
            else
                return result;
        }

        private TestResult BuildError(Exception e, TestConsoleAccess consoleAccess)
        {
            TestName testName = new TestName();
            testName.Name = mPNUnitTestInfo.TestName;
            testName.FullName = mPNUnitTestInfo.TestName;
            testName.TestID = new TestID();

            TestResult result = new PNUnitTestResult(testName, consoleAccess.GetTestOutput());
            result.Error(e);
            return result;
        }

        private TestResult BuildError(string message, TestConsoleAccess consoleAccess)
        {
            TestName testName = new TestName();
            testName.Name = mPNUnitTestInfo.TestName;
            testName.FullName = mPNUnitTestInfo.TestName;
            testName.TestID = new TestID();

            TestResult result = new PNUnitTestResult(testName, consoleAccess.GetTestOutput());
            result.Failure(message, string.Empty);
            return result;
        }

        private bool ReturnTestOutput()
        {
            string returnOutput = PNUnitServices.Get().GetUserValue("return_output");
            return returnOutput == "true";
        }

        private static TestResult FindResult(string name, TestResult result)
        {
            if( result.Test.TestName.FullName == name )
                return result;

            if( result.HasResults )
            {
                foreach( TestResult r in result.Results )
                {
                    TestResult myResult = FindResult(name, r);
                    if( myResult != null )
                        return myResult;
                }
            }

            return null;
        }

        private bool MakeTest(
            TestRunner runner,
            string assemblyName,
            bool bShadowCopyCache)
        {
            log.Debug("Entering MakeTest");
            TestPackage package = new TestPackage(
                Path.GetFullPath(assemblyName));
            package.Settings["ShadowCopyFiles"] = bShadowCopyCache;
            return runner.Load(package);
        }

        private bool GetShadowCopyCacheConfig()
        {
            return false;
/*            if (mConfig.ShadowCopyCache != null)
                if (mConfig.ShadowCopyCache.ToLower().IndexOf("false") != -1 )
                    return false;
*/

        }

        #region Nested Class to Handle Events

        [Serializable]
        private class EventCollector : EventListener
        {
            private int testRunCount;
            private int testIgnoreCount;
            private int failureCount;
            private int level;

            private ConsoleWriter writer;

            StringCollection messages = new StringCollection();

            private bool debugger = false;
            private string currentTestName;

            public EventCollector( ConsoleWriter writer )
            {
                debugger = Debugger.IsAttached;
                level = 0;
                this.writer = writer;
                this.currentTestName = string.Empty;
            }

            public void RunStarted(Test[] tests)
            {
            }

            public void RunStarted(string a, int b)
            {
            }

            public void RunFinished(TestResult[] results)
            {
            }

            public void RunFinished(Exception exception)
            {
            }

            public void RunFinished(TestResult result)
            {
            }

            public void TestFinished(TestResult testResult)
            {
                if(testResult.Executed)
                {
                    testRunCount++;

                    if(testResult.IsFailure)
                    {
                        failureCount++;
                        Console.Write("F");
                        if ( debugger )
                        {
                            messages.Add( string.Format( "{0}) {1} :", failureCount, testResult.Test.TestName ) );
                            messages.Add( testResult.Message.Trim( Environment.NewLine.ToCharArray() ) );

                            string stackTrace = StackTraceFilter.Filter( testResult.StackTrace );
                            string[] trace = stackTrace.Split( System.Environment.NewLine.ToCharArray() );
                            foreach( string s in trace )
                            {
                                if ( s != string.Empty )
                                {
                                    string link = Regex.Replace( s.Trim(), @".* in (.*):line (.*)", "$1($2)");
                                    messages.Add( string.Format( "at\n{0}", link ) );
                                }
                            }
                        }
                    }
                }
                else
                {
                    testIgnoreCount++;
                    Console.Write("N");
                }


                currentTestName = string.Empty;
            }

            public void TestStarted(TestMethod testCase)
            {
                currentTestName = testCase.TestName.FullName;
            }

            public void TestStarted(TestName testName)
            {
                currentTestName = testName.FullName;
            }


            public void SuiteStarted(TestName name)
            {
                if ( debugger && level++ == 0 )
                {
                    testRunCount = 0;
                    testIgnoreCount = 0;
                    failureCount = 0;
                    Trace.WriteLine( "################################ UNIT TESTS ################################" );
                    Trace.WriteLine( "Running tests in '" + name + "'..." );
                }
            }

            public void SuiteFinished(TestResult suiteResult)
            {
                if ( debugger && --level == 0)
                {
                    Trace.WriteLine( "############################################################################" );

                    if (messages.Count == 0)
                    {
                        Trace.WriteLine( "##############                 S U C C E S S               #################" );
                    }
                    else
                    {
                        Trace.WriteLine( "##############                F A I L U R E S              #################" );

                        foreach ( string s in messages )
                        {
                            Trace.WriteLine(s);
                        }
                    }

                    Trace.WriteLine( "############################################################################" );
                    Trace.WriteLine( "Executed tests : " + testRunCount );
                    Trace.WriteLine( "Ignored tests  : " + testIgnoreCount );
                    Trace.WriteLine( "Failed tests   : " + failureCount );
                    Trace.WriteLine( "Total time     : " + suiteResult.Time + " seconds" );
                    Trace.WriteLine( "############################################################################");
                }
            }

            public void UnhandledException( Exception exception )
            {
                string msg = string.Format( "##### Unhandled Exception while running {0}", currentTestName );

                // If we do labels, we already have a newline
                //if ( !options.labels ) writer.WriteLine();
                writer.WriteLine( msg );
                writer.WriteLine( exception.ToString() );

                if ( debugger )
                {
                    Trace.WriteLine( msg );
                    Trace.WriteLine( exception.ToString() );
                }
            }

            public void TestOutput( TestOutput output)
            {
            }
        }

        #endregion

        public static bool IsWindows()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32Windows:
                case System.PlatformID.Win32S:
                case PlatformID.Win32NT:
                    return true;
            }
            return false;
        }
    }

    public class TestConsoleAccess: MarshalByRefObject, ITestConsoleAccess
    {
        StringBuilder mBuilder = new StringBuilder();

        public void WriteLine(string s)
        {
#if NET_2_0
            mBuilder.AppendLine(s);
#else
            mBuilder.Append(s + "\n");
#endif
            Console.WriteLine(s);
        }

        public void Write(char[] buf)
        {
            mBuilder.Append(buf);
            Console.Write(buf);
        }

        public void Write(char[] buf, int index, int count)
        {
            mBuilder.Append(buf, index, count);
            Console.Write(buf, index, count);
        }

        public string GetTestOutput()
        {
            return mBuilder.ToString();
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

    }
}
