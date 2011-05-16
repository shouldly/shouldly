using System;
using System.IO;
using System.Threading;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Resources;


using PNUnit.Framework;

using NUnit.Core;
using NUnit.Util;
#if NUNIT_2_5
using TestInfo = PNUnit.Framework.TestInfo;
using TestCaseResult = NUnit.Core.TestResult;
using TestSuiteResult = NUnit.Core.TestResult;
using TestCase = NUnit.Core.Test;
#endif

using log4net;

namespace PNUnit.Agent
{
	public class PNUnitTestRunner: MarshalByRefObject, ITestConsoleAccess
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(PNUnitTestRunner));
		private TestInfo mTestInfo;
		private Thread mThread;
		private AgentConfig mConfig;
		private static object obj = new object();

		public PNUnitTestRunner(TestInfo info, AgentConfig config)
		{
			mConfig = config;
			mTestInfo = info;
		}

		public void Run()
		{
			log.Info("Spawning a new thread");
			mThread = new Thread(new ThreadStart(ThreadProc));
			mThread.Start();
		}

		private void ThreadProc()
		{
			PNUnitTestResult result = null;
			TestDomain testDomain = new TestDomain();

			try
			{
				log.InfoFormat("Thread entered for Test {0}:{1} Assembly {2}",
					mTestInfo.TestName, mTestInfo.TestToRun, mTestInfo.AssemblyName);
				ConsoleWriter outStream = new ConsoleWriter(Console.Out);

				ConsoleWriter errorStream = new ConsoleWriter(Console.Error);                     
          
#if NUNIT_2_5
                ITest test = MakeTest(testDomain, Path.Combine(mConfig.PathToAssemblies, mTestInfo.AssemblyName));
#else
				testDomain.ShadowCopyFiles = false;

				Test test = MakeTest(testDomain, Path.Combine(mConfig.PathToAssemblies, mTestInfo.AssemblyName));
#endif

                if (test == null)
				{
					Console.Error.WriteLine("Unable to locate tests");
                
					mTestInfo.Services.NotifyResult(
						mTestInfo.TestName, null);
                
					return;
				}

				Directory.SetCurrentDirectory(mConfig.PathToAssemblies); // test directory ?
		
				EventListener collector = new EventCollector( outStream );

				string savedDirectory = Environment.CurrentDirectory;

				log.Info("Creating PNUnitServices in the AppDomain of the test");
				object[] param = { mTestInfo, (ITestConsoleAccess)this }; 

				object obj = testDomain.AppDomain.CreateInstanceAndUnwrap(
					typeof(PNUnitServices).Assembly.FullName, 
					typeof(PNUnitServices).FullName,
					false, BindingFlags.Default, null, param, null, null, null);

				log.Info("Running tests");

				try
				{
#if NUNIT_2_5
                    TestFilter filter = new NUnit.Core.Filters.SimpleNameFilter(mTestInfo.TestToRun);
                    result = new PNUnitTestResult(testDomain.Run(collector, filter));
#else
                    result = new PNUnitTestResult(testDomain.Run(collector, new string[1] { mTestInfo.TestToRun })[0]);
#endif
                }
				catch( Exception e )
				{
					result = new PNUnitTestResult(e);
				}
                
			}
			finally
			{
				log.Info("Notifying the results");
				mTestInfo.Services.NotifyResult(
					mTestInfo.TestName, result);
				//Bug with framework
				if (IsWindows())
				{
					lock(obj)
					{
						log.Info("Unloading test appdomain");
						testDomain.Unload();
						log.Info("Unloaded test appdomain");
					}
				}
			}

		}

#if NUNIT_2_5
        private ITest MakeTest(TestDomain testDomain, string assemblyName)
        {
            TestPackage package = new TestPackage(assemblyName);
            package.Settings["ShadowCopyFiles"] = false;

            return testDomain.Load(package) ? testDomain.Test : null;
        }
#else
        private Test MakeTest(TestDomain testDomain, string assemblyName)
		{
			NUnitProject project;
                
			project = NUnitProject.FromAssembly(assemblyName);
                                 
			return testDomain.Load(project);
        }
#endif

		#region MarshallByRefObject
		// Lives forever
		public override object InitializeLifetimeService()
		{
			return null;
		}
		#endregion


		#region Nested Class to Handle Events

#if NUNIT_2_5
        private class EventCollector : MarshalByRefObject, EventListener
#else
        private class EventCollector : LongLivingMarshalByRefObject, EventListener
#endif
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

#if NUNIT_2_5
            public void RunStarted(string name, int testCount)
            {
            }

            public void RunFinished(TestResult result)
            {
            }
#else
			public void RunStarted(Test[] tests)
			{
			}

			public void RunFinished(TestResult[] results)
			{
			}
#endif

			public void RunFinished(Exception exception)
			{
			}

			public void TestFinished(TestCaseResult testResult)
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
#if NUNIT_2_5
                            messages.Add(string.Format("{0}) {1} :", failureCount, testResult.Test.TestName.FullName));
#else
							messages.Add( string.Format( "{0}) {1} :", failureCount, testResult.Test.FullName ) );
#endif
                            messages.Add(testResult.Message.Trim(Environment.NewLine.ToCharArray()));

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

#if NUNIT_2_5
			public void TestStarted(TestName testName)
			{
				currentTestName = testName.FullName;

				//                if ( options.labels )
				//                    writer.WriteLine("***** {0}", testCase.FullName );
				//                else if ( !options.xmlConsole )
				//                    Console.Write(".");
			}

			public void SuiteStarted(TestName testName) 
			{
				if ( debugger && level++ == 0 )
				{
					testRunCount = 0;
					testIgnoreCount = 0;
					failureCount = 0;
					Trace.WriteLine( "################################ UNIT TESTS ################################" );
					Trace.WriteLine( "Running tests in '" + testName.FullName + "'..." );
				}
			}
#else
			public void TestStarted(TestCase testCase)
			{
				currentTestName = testCase.FullName;

				//                if ( options.labels )
				//                    writer.WriteLine("***** {0}", testCase.FullName );
				//                else if ( !options.xmlConsole )
				//                    Console.Write(".");
			}

			public void SuiteStarted(TestSuite suite) 
			{
				if ( debugger && level++ == 0 )
				{
					testRunCount = 0;
					testIgnoreCount = 0;
					failureCount = 0;
					Trace.WriteLine( "################################ UNIT TESTS ################################" );
					Trace.WriteLine( "Running tests in '" + suite.FullName + "'..." );
				}
			}
#endif

			public void SuiteFinished(TestSuiteResult suiteResult) 
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

#if NUNIT_2_5
            public override object InitializeLifetimeService()
            {
                return null;
            }
#endif
		}

		#endregion

		public void WriteLine(string s)
		{
			Console.WriteLine(s);
		}

		public void Write(char[] buf)
		{
			Console.Write(buf);
		}
        
		public void Write(char[] buf, int index, int count)
		{
			Console.Write(buf, index, count);
		}

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
}
