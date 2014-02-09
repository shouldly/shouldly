using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;

using NUnit.Core;

using PNUnit.Framework;

using log4net;
using log4net.Config;


namespace PNUnit.Launcher
{
    class Launcher
    {
        private static readonly ILog log = LogManager.GetLogger("launcher");

        static string mTestPath = string.Empty;
        static string mTotalLog = string.Empty;

        private static int MAX_TEST_RETRY = 3;

        [STAThread]
        static void Main(string[] args)
        {
            string resultfile = null;
            string failedfile = null;
            string passedfile = null;

            int retryOnFailure = 0;
            int maxRetry = MAX_TEST_RETRY;

            ConfigureLogging();

            try
            {
                // Load the test configuration file
                if( args.Length == 0 )
                {
                    Console.WriteLine(
                        "Usage: launcher configfile [--result=filename] [--failed=filename] [-D:var=value] [-val:variable=value] [--retry=number] [--range=from-to] [--test=testname]");
                    return;
                }

                string configfile = args[0];

                mTestPath = Path.GetDirectoryName(configfile);
                TestGroup group = TestConfLoader.LoadFromFile(configfile, args);

                int startTest = 0;
                int endTest = group.ParallelTests.Length - 1;

                failedfile = Path.Combine(mTestPath, "smokefailed.conf");
                passedfile = Path.Combine(mTestPath, "smokepassed.conf");

                if( args.Length > 1 )
                {
                    foreach( string arg in args )
                    {
                        if( arg.StartsWith("--result=") )
                        {
                            resultfile = arg.Substring(9);
                            resultfile = Path.GetFullPath(resultfile);
                        }

                        if( arg.StartsWith("--failed=") )
                        {
                            failedfile = arg.Substring(9);
                            failedfile = Path.GetFullPath(failedfile);
                        }

                        if( arg.StartsWith("--retry=") )
                        {
                            retryOnFailure = int.Parse(arg.Substring("--retry=".Length));
                            log.InfoFormat("Retry on failure activated. {0} retries", retryOnFailure);
                            maxRetry = retryOnFailure;
                        }

                        if(arg.StartsWith("--test="))
                        {
                            string testName = arg.Substring("--test=".Length);
                            int index = -1;
                            for(int i=0; i< group.ParallelTests.Length; i++)
                            {
                                if(group.ParallelTests[i].Name != testName)
                                    continue;

                                index = i;
                                break;
                            }

                            if(index == -1)
                            {
                                Console.WriteLine("The specified test was not found");
                                return;
                            }

                            startTest = index;
                            endTest = index;
                        }

                        if( arg.StartsWith("--range=") )
                        {
                            string range = arg.Substring("--range=".Length);
                            // now range should be something like xx-xx
                            if( range.IndexOf("-") < 0 )
                            {
                                Console.WriteLine("Test range incorrectly specified, it must be something like 0-10");
                                return;
                            }
                            string[] ranges = range.Split('-');
                            if( ranges.Length != 2 )
                            {
                                Console.WriteLine("Test range incorrectly specified, it must be something like 0-10");
                                return;
                            }

                            startTest = int.Parse(ranges[0]);
                            endTest = int.Parse(ranges[1]);

                            if( (startTest > endTest) ||
                                (startTest < 0) || 
                                (startTest > group.ParallelTests.Length - 1) )
                            {
                                Console.WriteLine("Start test must be in a correct test range");
                                return;
                            }

                            if( (endTest < startTest) ||
                                (endTest < 0) || 
                                (endTest > group.ParallelTests.Length - 1) )
                            {
                                Console.WriteLine("End test must be in a correct test range");
                                return;
                            }

                            log.InfoFormat("Starting test range [{0}-{1}]", startTest, endTest);
                        }
                    }
                }

                if( (group == null) || (group.ParallelTests.Length == 0) )
                {
                    Console.WriteLine("No tests to run");
                    return;
                }

                Hashtable userValues = GetUserValues(args);

                ConfigureRemoting();

                ArrayList failedGroups = new ArrayList();
                ArrayList passedGroups = new ArrayList();

                int testCount = endTest - startTest + 1;

                // Each parallel test is launched sequencially...
                Runner[] runners = new Runner[testCount];

                DateTime beginTimestamp = DateTime.Now;
                for( int i = startTest; i <= endTest; )
                {
                    ParallelTest test = group.ParallelTests[i] as ParallelTest;

                    int retryCount = 0;

                    bool bRetry = true;

                    while( bRetry && retryCount < maxRetry )
                    {
                        bRetry = false;

                        if( testCount != group.ParallelTests.Length )
                            log.InfoFormat("Test {0} of {1}. {2}/{3}",
                                i, group.ParallelTests.Length, i-startTest+1,
                                testCount);
                        else
                            log.InfoFormat("Test {0} of {1}", i+1,
                                group.ParallelTests.Length);

                        Runner runner = new Runner(test, userValues);
                        runner.Run();

                        runners[i-startTest] = runner;
                        // Wait to finish
                        runner.Join();

                        TestResult[] runnerResults = runner.GetTestResults();

                        if( runnerResults == null )
                        {
                            log.Info("Error. Results are NULL");

                            ++i;
                            continue;
                        }

                        bRetry = RetryTest(runnerResults);
                        bool bFailed = FailedTest(runnerResults);

                        if( bRetry ||
                            ((bFailed && (retryOnFailure > 0) &&
                             ((retryCount + 1) < maxRetry ) ) /* so that list time is printed*/) )
                        {
                            bRetry = true;
                            ++retryCount;
                            log.Info("Test failed with retry option, trying again");
                            continue;
                        }

                        if( bFailed )
                        {
                            failedGroups.Add(test);
                            WriteGroup(failedGroups, failedfile);
                        }
                        else
                        {
                            passedGroups.Add(test);
                            WriteGroup(passedGroups, passedfile);
                        }
                    }

                    // updated at the bottom so it's not affected by retries
                    ++i;
                }
                DateTime endTimestamp = DateTime.Now;

                // Print the results
                double TotalBiggerTime = 0;
                int TotalTests = 0;
                int TotalExecutedTests = 0;
                int TotalFailedTests = 0;
                int TotalSuccessTests = 0;

                IList failedTests = new ArrayList();

                int j;
                foreach( Runner runner in runners )
                {
                    int ExecutedTests = 0;
                    int FailedTests = 0;
                    int SuccessTests = 0;
                    double BiggerTime = 0;
                    TestResult[] results = runner.GetTestResults();
                    Log(string.Format("==== Tests Results for Parallel TestGroup {0} ===", runner.TestGroupName));
                    j = 0;
                    foreach( TestResult res in results )
                    {
                        if( res.Executed )
                            ++ExecutedTests;
                        if( res.IsFailure )
                            ++FailedTests;
                        if( res.IsSuccess )
                            ++SuccessTests;

                        PrintResult(++j, res);
                        if( res.Time > BiggerTime )
                            BiggerTime = res.Time;

                        if( res.IsFailure )
                            failedTests.Add(res);
                    }

                    Log("Summary:");
                    Log(string.Format("\tTotal: {0}\r\n\tExecuted: {1}\r\n\tFailed: {2}\r\n\tSuccess: {3}\r\n\t% Success: {4}\r\n\tBiggest Execution Time: {5} s\r\n",
                        results.Length, ExecutedTests, FailedTests, SuccessTests,
                        results.Length > 0 ? 100 * SuccessTests / results.Length : 0,
                        BiggerTime));

                    TotalTests += results.Length;
                    TotalExecutedTests += ExecutedTests;
                    TotalFailedTests += FailedTests;
                    TotalSuccessTests += SuccessTests;
                    TotalBiggerTime += BiggerTime;
                }

                // print all failed tests together
                if( failedTests.Count > 0 )
                {
                    Log("==== Failed tests ===");
                    for( j = 0; j < failedTests.Count; ++j )
                        PrintResult(j, failedTests[j] as PNUnitTestResult);
                }

                if( runners.Length > 1 )
                {

                    Log("Summary for all the parallel tests:");
                    Log(string.Format("\tTotal: {0}\r\n\tExecuted: {1}\r\n\tFailed: {2}\r\n\tSuccess: {3}\r\n\t% Success: {4}\r\n\tBiggest Execution Time: {5} s\r\n",
                        TotalTests, TotalExecutedTests, TotalFailedTests, TotalSuccessTests,
                        TotalTests > 0 ? 100 * TotalSuccessTests / TotalTests : 0,
                        TotalBiggerTime));
                }

                TimeSpan elapsedTime = endTimestamp.Subtract(beginTimestamp);
                Log(string.Format("Launcher execution time: {0} seconds", elapsedTime.TotalSeconds));
            }
            finally
            {
                WriteResult(resultfile);
            }
        }

        private static bool FailedTest(TestResult[] results)
        {
            foreach( TestResult res in results )
            {
                if (res == null)
                    continue;
                if( !res.IsSuccess )
                    return true;
            }
            return false;
        }

        private static bool RetryTest(TestResult[] results)
        {
            foreach(TestResult res in results)
            {
                if (res == null)
                    continue;
                if (res is PNUnitTestResult)
                {
                    return ((PNUnitTestResult)res).RetryTest;
                }
            }
            return false;
        }

        private static void ConfigureRemoting()
        {
            if( File.Exists("launcher.remoting.conf") )
            {
                log.InfoFormat("Using launcher.remoting.conf");
#if CLR_2_0 || CLR_4_0
                RemotingConfiguration.Configure("launcher.remoting.conf", false);
#else
                RemotingConfiguration.Configure("launcher.remoting.conf");
#endif
                return;
            }

            BinaryClientFormatterSinkProvider clientProvider = null;
            BinaryServerFormatterSinkProvider serverProvider =
                new BinaryServerFormatterSinkProvider();
            serverProvider.TypeFilterLevel =
                System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;

            IDictionary props = new Hashtable();
            props["port"] = 0;
            props["typeFilterLevel"] = TypeFilterLevel.Full;
            TcpChannel chan = new TcpChannel(
                props,clientProvider,serverProvider);

#if CLR_2_0 || CLR_4_0
            ChannelServices.RegisterChannel(chan, false);
#else
            ChannelServices.RegisterChannel(chan);
#endif
        }


        private static void PrintResult(int testNumber, TestResult res)
        {
            string[] messages = GetErrorMessages(res);
            Log(string.Format("({0}) {1}", testNumber, messages[0]));
            if( !res.IsSuccess )
                Log(messages[1]);
        }

        private static string[] GetErrorMessages(TestResult res)
        {
            string[] result = new string[2];

            result[0] = string.Format(
                "Name: {0}\n  Result: {1,-12} Assert Count: {2,-2} Time: {3,5}",
                res.Name,
                res.IsSuccess ? "SUCCESS" : (res.IsFailure ? "FAILURE" : (! res.Executed ? "NOT EXECUTED": "UNKNOWN")),
                res.AssertCount,
                res.Time);

            if( !res.IsSuccess )
                result[1] = string.Format(
                    "\nMessage: {0}\nStack Trace:\n{1}\r\n\r\n",
                        res.Message, res.StackTrace);

            return result;
        }

        private static object mWriteTestLogLock = new object();
        internal static void WriteTestLog(
            TestResult result,
            string machine,
            string fileName)
        {
            if( result == null )
                return;

            lock( mWriteTestLogLock )
                DoWriteTestLog(result, machine, fileName);
        }

        private static void DoWriteTestLog(
            TestResult result,
            string machine,
            string fileName)
        {
            FileStream fs = null;
            StreamWriter writer = null;

            try
            {
                fs = new FileStream(
                    Path.Combine(mTestPath, fileName),
                    FileMode.OpenOrCreate,
                    FileAccess.ReadWrite);

                fs.Seek(0, SeekOrigin.End);
                writer = new StreamWriter(fs);

                writer.WriteLine("==============================================================");

                if( result.IsFailure )
                {
                    writer.WriteLine("Errors for test [{0}] run at agent [{1}]",
                        result.Name, machine);

                    string[] messages = GetErrorMessages(result);

                    writer.WriteLine(messages[0]);
                    writer.WriteLine(messages[1]);
                }
                else
                {
                    writer.WriteLine("Log for test [{0}] run at agent [{1}]",
                        result.Name, machine);
                }

                writer.WriteLine("\nOutput:");
                if( result is PNUnitTestResult )
                {
                    writer.Write(((PNUnitTestResult)result).Output);
                }
            }
            catch( Exception e )
            {
                log.ErrorFormat("Error writing to {0}. {1}",
                    fileName, e.Message);
            }
            finally
            {
                if( writer != null )
                {
                    writer.Flush();
                    writer.Close();
                }

                if( fs != null )
                    fs.Close();
            }
        }

        private static void WriteResult(string resultfile)
        {
            if (resultfile == null || resultfile == string.Empty)
                return;

            if (File.Exists(resultfile))
            {
                File.Delete(resultfile);
            }

            FileStream fs = new FileStream(resultfile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter writer = new StreamWriter(fs);

            try
            {
                writer.Write(mTotalLog);

            }
            finally
            {
                writer.Flush();
                writer.Close();
                fs.Close();
            }
        }

        private static void WriteGroup(ArrayList failedTests, string filename)
        {
            TestGroup group = new TestGroup();
            group.ParallelTests = (ParallelTest[]) failedTests.ToArray(typeof(ParallelTest));
            TestConfLoader.WriteToFile(group, filename);
        }

        private static void ConfigureLogging()
        {
            string log4netpath = "launcher.log.conf";
            if (!File.Exists (log4netpath))
                log4netpath = Path.Combine(mTestPath, log4netpath);

            XmlConfigurator.Configure(new FileInfo(log4netpath));
        }

        public static void Log(string msg)
        {
            log.Info(msg);
            mTotalLog += string.Concat(msg, "\r\n");
        }

        public static void LogError(string msg)
        {
            log.Error(msg);
            mTotalLog += string.Concat(msg, "\r\n");
        }

        private const string USER_VALUE_KEY = "-val:";

        private static Hashtable GetUserValues(string[] args)
        {
            Hashtable result = new Hashtable();

            foreach( string s in args )
            {
                if( !s.ToLower().StartsWith(USER_VALUE_KEY) )
                    continue;

                string[] v = s.Substring(USER_VALUE_KEY.Length).Split('=');

                if( v.Length >= 1 )
                {
                    string name = v[0];
                    string val = string.Empty;

                    if( v.Length == 2 )
                        val = v[1];

                    result.Add(name, val);
                }
            }

            return result;
        }
    }
}