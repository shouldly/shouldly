// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System.Diagnostics;

namespace NUnit.ConsoleRunner
{
	using System;
	using System.IO;
	using System.Reflection;
	using System.Xml;
	using System.Resources;
	using System.Text;
	using NUnit.Core;
	using NUnit.Core.Filters;
	using NUnit.Util;
	
	/// <summary>
	/// Summary description for ConsoleUi.
	/// </summary>
	public class ConsoleUi
	{
		public static readonly int OK = 0;
		public static readonly int INVALID_ARG = -1;
		public static readonly int FILE_NOT_FOUND = -2;
		public static readonly int FIXTURE_NOT_FOUND = -3;
		public static readonly int UNEXPECTED_ERROR = -100;

        private string workDir;

		public ConsoleUi()
		{
		}

		public int Execute( ConsoleOptions options )
		{
            this.workDir = options.work;
            if (workDir == null || workDir == string.Empty)
                workDir = Environment.CurrentDirectory;
            else
            {
                workDir = Path.GetFullPath(workDir);
                if (!Directory.Exists(workDir))
                    Directory.CreateDirectory(workDir);
            }

			TextWriter outWriter = Console.Out;
			bool redirectOutput = options.output != null && options.output != string.Empty;
			if ( redirectOutput )
			{
				StreamWriter outStreamWriter = new StreamWriter( Path.Combine(workDir, options.output) );
				outStreamWriter.AutoFlush = true;
				outWriter = outStreamWriter;
			}

			TextWriter errorWriter = Console.Error;
			bool redirectError = options.err != null && options.err != string.Empty;
			if ( redirectError )
			{
				StreamWriter errorStreamWriter = new StreamWriter( Path.Combine(workDir, options.err) );
				errorStreamWriter.AutoFlush = true;
				errorWriter = errorStreamWriter;
			}

            TestPackage package = MakeTestPackage(options);

            ProcessModel processModel = package.Settings.Contains("ProcessModel")
                ? (ProcessModel)package.Settings["ProcessModel"]
                : ProcessModel.Default;

            DomainUsage domainUsage = package.Settings.Contains("DomainUsage")
                ? (DomainUsage)package.Settings["DomainUsage"]
                : DomainUsage.Default;

            RuntimeFramework framework = package.Settings.Contains("RuntimeFramework")
                ? (RuntimeFramework)package.Settings["RuntimeFramework"]
                : RuntimeFramework.CurrentFramework;

#if CLR_2_0 || CLR_4_0
            Console.WriteLine("ProcessModel: {0}    DomainUsage: {1}", processModel, domainUsage);

            Console.WriteLine("Execution Runtime: {0}", framework);
#else
            Console.WriteLine("DomainUsage: {0}", domainUsage);

            if (processModel != ProcessModel.Default && processModel != ProcessModel.Single)
                Console.WriteLine("Warning: Ignoring project setting 'processModel={0}'", processModel);

            if (!RuntimeFramework.CurrentFramework.Supports(framework))
                Console.WriteLine("Warning: Ignoring project setting 'runtimeFramework={0}'", framework);
#endif

            using (TestRunner testRunner = new DefaultTestRunnerFactory().MakeTestRunner(package))
			{
                testRunner.Load(package);

                if (testRunner.Test == null)
				{
					testRunner.Unload();
					Console.Error.WriteLine("Unable to locate fixture {0}", options.fixture);
					return FIXTURE_NOT_FOUND;
				}

				EventCollector collector = new EventCollector( options, outWriter, errorWriter );

				TestFilter testFilter;
					
				if(!CreateTestFilter(options, out testFilter))
					return INVALID_ARG;

				TestResult result = null;
				string savedDirectory = Environment.CurrentDirectory;
				TextWriter savedOut = Console.Out;
				TextWriter savedError = Console.Error;

				try
				{
					result = testRunner.Run( collector, testFilter, false, LoggingThreshold.Off );
				}
				finally
				{
					outWriter.Flush();
					errorWriter.Flush();

					if (redirectOutput)
						outWriter.Close();

					if (redirectError)
						errorWriter.Close();

					Environment.CurrentDirectory = savedDirectory;
					Console.SetOut( savedOut );
					Console.SetError( savedError );
				}

				Console.WriteLine();

                int returnCode = UNEXPECTED_ERROR;

                if (result != null)
                {
                    string xmlOutput = CreateXmlOutput(result);
                    ResultSummarizer summary = new ResultSummarizer(result);

                    if (options.xmlConsole)
                    {
                        Console.WriteLine(xmlOutput);
                    }
                    else
                    {
                        WriteSummaryReport(summary);

                        bool hasErrors = summary.Errors > 0 || summary.Failures > 0 || result.IsError || result.IsFailure;

                        if (options.stoponerror && (hasErrors || summary.NotRunnable > 0))
                        {
                            Console.WriteLine("Test run was stopped after first error, as requested.");
                            Console.WriteLine();
                        }

                        if (hasErrors)
                            WriteErrorsAndFailuresReport(result);

                        if (summary.TestsNotRun > 0)
                            WriteNotRunReport(result);

                        if (!options.noresult)
                        {
                            // Write xml output here
                            string xmlResultFile = options.result == null || options.result == string.Empty
                                ? "TestResult.xml" : options.result;

                            using (StreamWriter writer = new StreamWriter(Path.Combine(workDir, xmlResultFile)))
                            {
                                writer.Write(xmlOutput);
                            }
                        }
                    }

                    returnCode = summary.Errors + summary.Failures + summary.NotRunnable;
                }

				if (collector.HasExceptions)
				{
					collector.WriteExceptions();
					returnCode = UNEXPECTED_ERROR;
				}
            
				return returnCode;
			}
		}

		internal static bool CreateTestFilter(ConsoleOptions options, out TestFilter testFilter)
		{
			testFilter = TestFilter.Empty;

			SimpleNameFilter nameFilter = new SimpleNameFilter();

			if (options.run != null && options.run != string.Empty)
			{
				Console.WriteLine("Selected test(s): " + options.run);

				foreach (string name in TestNameParser.Parse(options.run))
					nameFilter.Add(name);

				testFilter = nameFilter;
			}

			if (options.runlist != null && options.runlist != string.Empty)
			{
				Console.WriteLine("Run list: " + options.runlist);
				
				try
				{
					using (StreamReader rdr = new StreamReader(options.runlist))
					{
						// NOTE: We can't use rdr.EndOfStream because it's
						// not present in .NET 1.x.
						string line = rdr.ReadLine();
						while (line != null && line.Length > 0)
						{
							if (line[0] != '#')
								nameFilter.Add(line);
							line = rdr.ReadLine();
						}
					}
				}
				catch (Exception e)
				{
					if (e is FileNotFoundException || e is DirectoryNotFoundException)
					{
						Console.WriteLine("Unable to locate file: " + options.runlist);
						return false;
					}
					throw;
				}

				testFilter = nameFilter;
			}

			if (options.include != null && options.include != string.Empty)
			{
				TestFilter includeFilter = new CategoryExpression(options.include).Filter;
				Console.WriteLine("Included categories: " + includeFilter.ToString());

				if (testFilter.IsEmpty)
					testFilter = includeFilter;
				else
					testFilter = new AndFilter(testFilter, includeFilter);
			}

			if (options.exclude != null && options.exclude != string.Empty)
			{
				TestFilter excludeFilter = new NotFilter(new CategoryExpression(options.exclude).Filter);
				Console.WriteLine("Excluded categories: " + excludeFilter.ToString());

				if (testFilter.IsEmpty)
					testFilter = excludeFilter;
				else if (testFilter is AndFilter)
					((AndFilter) testFilter).Add(excludeFilter);
				else
					testFilter = new AndFilter(testFilter, excludeFilter);
			}

			if (testFilter is NotFilter)
				((NotFilter) testFilter).TopLevel = true;

			return true;
		}

		#region Helper Methods
        // TODO: See if this can be unified with the Gui's MakeTestPackage
        private TestPackage MakeTestPackage( ConsoleOptions options )
        {
			TestPackage package;
			DomainUsage domainUsage = DomainUsage.Default;
            ProcessModel processModel = ProcessModel.Default;
            RuntimeFramework framework = null;

            string[] parameters = new string[options.ParameterCount];
            for (int i = 0; i < options.ParameterCount; i++)
                parameters[i] = Path.GetFullPath((string)options.Parameters[i]);

			if (options.IsTestProject)
			{
				NUnitProject project = 
					Services.ProjectService.LoadProject(parameters[0]);

				string configName = options.config;
				if (configName != null)
					project.SetActiveConfig(configName);

				package = project.ActiveConfig.MakeTestPackage();
                processModel = project.ProcessModel;
                domainUsage = project.DomainUsage;
                framework = project.ActiveConfig.RuntimeFramework;
			}
			else if (parameters.Length == 1)
			{
                package = new TestPackage(parameters[0]);
				domainUsage = DomainUsage.Single;
			}
			else
			{
                // TODO: Figure out a better way to handle "anonymous" packages
				package = new TestPackage(null, parameters);
                package.AutoBinPath = true;
				domainUsage = DomainUsage.Multiple;
			}

			if (options.basepath != null && options.basepath != string.Empty)
 			{
 				package.BasePath = options.basepath;
 			}
 
 			if (options.privatebinpath != null && options.privatebinpath != string.Empty)
 			{
 				package.AutoBinPath = false;
				package.PrivateBinPath = options.privatebinpath;
 			}

#if CLR_2_0 || CLR_4_0
            if (options.framework != null)
                framework = RuntimeFramework.Parse(options.framework);

            if (options.process != ProcessModel.Default)
                processModel = options.process;
#endif

			if (options.domain != DomainUsage.Default)
				domainUsage = options.domain;

			package.TestName = options.fixture;
            
            package.Settings["ProcessModel"] = processModel;
            package.Settings["DomainUsage"] = domainUsage;
            
			if (framework != null)
                package.Settings["RuntimeFramework"] = framework;

            if (domainUsage == DomainUsage.None)
            {
                // Make sure that addins are available
                CoreExtensions.Host.AddinRegistry = Services.AddinRegistry;
            }

            package.Settings["ShadowCopyFiles"] = !options.noshadow;
			package.Settings["UseThreadedRunner"] = !options.nothread;
            package.Settings["DefaultTimeout"] = options.timeout;
            package.Settings["WorkDirectory"] = this.workDir;
            package.Settings["StopOnError"] = options.stoponerror;

            if (options.apartment != System.Threading.ApartmentState.Unknown)
                package.Settings["ApartmentState"] = options.apartment;

            return package;
		}

		private static string CreateXmlOutput( TestResult result )
		{
			StringBuilder builder = new StringBuilder();
			new XmlResultWriter(new StringWriter( builder )).SaveTestResult(result);

			return builder.ToString();
		}

		private static void WriteSummaryReport( ResultSummarizer summary )
		{
            Console.WriteLine(
                "Tests run: {0}, Errors: {1}, Failures: {2}, Inconclusive: {3}, Time: {4} seconds",
                summary.TestsRun, summary.Errors, summary.Failures, summary.Inconclusive, summary.Time);
            Console.WriteLine(
                "  Not run: {0}, Invalid: {1}, Ignored: {2}, Skipped: {3}",
                summary.TestsNotRun, summary.NotRunnable, summary.Ignored, summary.Skipped);
            Console.WriteLine();
        }

        private void WriteErrorsAndFailuresReport(TestResult result)
        {
            reportIndex = 0;
            Console.WriteLine("Errors and Failures:");
            WriteErrorsAndFailures(result);
            Console.WriteLine();
        }

        private void WriteErrorsAndFailures(TestResult result)
        {
            if (result.Executed)
            {
                if (result.HasResults)
                {
                    if (result.IsFailure || result.IsError)
                        if (result.FailureSite == FailureSite.SetUp || result.FailureSite == FailureSite.TearDown)
                            WriteSingleResult(result);

                    foreach (TestResult childResult in result.Results)
                        WriteErrorsAndFailures(childResult);
                }
                else if (result.IsFailure || result.IsError)
                {
                    WriteSingleResult(result);
                }
            }
        }

        private void WriteNotRunReport(TestResult result)
        {
	        reportIndex = 0;
            Console.WriteLine("Tests Not Run:");
	        WriteNotRunResults(result);
            Console.WriteLine();
        }

	    private int reportIndex = 0;
        private void WriteNotRunResults(TestResult result)
        {
            if (result.HasResults)
                foreach (TestResult childResult in result.Results)
                    WriteNotRunResults(childResult);
            else if (!result.Executed)
                WriteSingleResult( result );
        }

        private void WriteSingleResult( TestResult result )
        {
            string status = result.IsFailure || result.IsError
                ? string.Format("{0} {1}", result.FailureSite, result.ResultState)
                : result.ResultState.ToString();

            Console.WriteLine("{0}) {1} : {2}", ++reportIndex, status, result.FullName);

            if ( result.Message != null && result.Message != string.Empty )
                 Console.WriteLine("   {0}", result.Message);

            if (result.StackTrace != null && result.StackTrace != string.Empty)
                Console.WriteLine( result.IsFailure
                    ? StackTraceFilter.Filter(result.StackTrace)
                    : result.StackTrace + Environment.NewLine );
        }
	    #endregion
	}
}

