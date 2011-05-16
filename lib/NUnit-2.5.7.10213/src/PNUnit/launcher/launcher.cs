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
		[STAThread]
		static void Main(string[] args)
		{   
			// Load the test configuration file
			if( args.Length == 0 )
			{
				Console.WriteLine("Usage: launcher configfile");
				return;
			}

			string configfile = args[0];

			TestGroup group = TestConfLoader.LoadFromFile(configfile);

			if( (group == null) || (group.ParallelTests.Length == 0) )
			{
				Console.WriteLine("No tests to run");
				return;
			}
                        
			ConfigureLogging();
            
			ConfigureRemoting();

			// Each parallel test is launched sequencially...
			Runner[] runners = new Runner[group.ParallelTests.Length];
			int i = 0;
			DateTime beginTimestamp = DateTime.Now;
			foreach( ParallelTest test in group.ParallelTests )
			{
				Console.WriteLine("Test {0} of {1}", i + 1, group.ParallelTests.Length);
				Runner runner = new Runner(test);
				runner.Run();
				runners[i++] = runner;                
				// Wait to finish
				runner.Join();
			}

			DateTime endTimestamp = DateTime.Now;
           
			// Print the results
			double TotalBiggerTime = 0;
			int TotalTests = 0;
			int TotalExecutedTests = 0;
			int TotalFailedTests = 0;
			int TotalSuccessTests = 0;
			foreach( Runner runner in runners )
			{
				int ExecutedTests = 0;
				int FailedTests = 0;
				int SuccessTests = 0;
				double BiggerTime = 0;
				PNUnitTestResult[] results = runner.GetTestResults();
				Console.WriteLine("==== Tests Results for Parallel TestGroup {0} ===", runner.TestGroupName);
                
				i = 0;
				foreach( PNUnitTestResult res in results )
				{
					if( res.Executed )
						++ExecutedTests;
					if( res.IsFailure )
						++FailedTests;
					if( res.IsSuccess )
						++SuccessTests;                    

					PrintResult(++i, res);
					if( res.Time > BiggerTime )
						BiggerTime = res.Time;
				}
				Console.WriteLine();
				Console.WriteLine("Summary:");
				Console.WriteLine("\tTotal: {0}\n\tExecuted: {1}\n\tFailed: {2}\n\tSuccess: {3}\n\t% Success: {4}\n\tBiggest Execution Time: {5} s\n",
					results.Length, ExecutedTests, FailedTests, SuccessTests, 
					results.Length > 0 ? 100 * SuccessTests / results.Length : 0,
					BiggerTime);

				TotalTests += results.Length;
				TotalExecutedTests += ExecutedTests;
				TotalFailedTests += FailedTests;
				TotalSuccessTests += SuccessTests;
				TotalBiggerTime += BiggerTime;
			}           

			if( runners.Length > 1 )
			{
				Console.WriteLine();
				Console.WriteLine("Summary for all the parallel tests:");
				Console.WriteLine("\tTotal: {0}\n\tExecuted: {1}\n\tFailed: {2}\n\tSuccess: {3}\n\t% Success: {4}\n\tBiggest Execution Time: {5} s\n",
					TotalTests, TotalExecutedTests, TotalFailedTests, TotalSuccessTests, 
					TotalTests > 0 ? 100 * TotalSuccessTests / TotalTests : 0,
					TotalBiggerTime);
			}

			TimeSpan elapsedTime = endTimestamp.Subtract(beginTimestamp);
			Console.WriteLine("Launcher execution time: {0} seconds", elapsedTime.TotalSeconds);
		}

		private static void ConfigureRemoting()
		{
			BinaryClientFormatterSinkProvider clientProvider = null;
			BinaryServerFormatterSinkProvider serverProvider = 
				new BinaryServerFormatterSinkProvider();
			serverProvider.TypeFilterLevel = 
				System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
                
			IDictionary props = new Hashtable();
			props["port"] = 8090;
			props["typeFilterLevel"] = TypeFilterLevel.Full;
			TcpChannel chan = new TcpChannel(
				props,clientProvider,serverProvider);

			ChannelServices.RegisterChannel(chan);
		}


		private static void PrintResult(int testNumber, PNUnitTestResult res)
		{   
			Console.WriteLine(
				"({0})Name: {1}\n  Result: {2,-12} Assert Count: {3,-2} Time: {4,5}", 
				testNumber,
				res.Name, 
				res.IsSuccess ? "SUCCESS" : (res.IsFailure ? "FAILURE" : (! res.Executed ? "NOT EXECUTED": "UNKNOWN")),
				res.AssertCount,
				res.Time);
			if( !res.IsSuccess )
				Console.WriteLine("\nMessage: {0}\nStack Trace:\n{1}\n\n",
					res.Message, res.StackTrace);

		}

		private static void ConfigureLogging()
		{
			string log4netpath = "launcher.log.conf";
			XmlConfigurator.Configure(new FileInfo(log4netpath));
		}


	}
}
