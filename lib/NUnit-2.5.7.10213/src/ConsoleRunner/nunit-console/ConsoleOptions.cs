// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

namespace NUnit.ConsoleRunner
{
	using System;
	using Codeblast;
	using NUnit.Util;
    using NUnit.Core;

	public class ConsoleOptions : CommandLineOptions
	{
		[Option(Short="load", Description = "Test fixture or namespace to be loaded (Deprecated)")]
		public string fixture;

		[Option(Description = "Name of the test case(s), fixture(s) or namespace(s) to run")]
		public string run;

		[Option(Description = "Project configuration (e.g.: Debug) to load")]
		public string config;

		[Option(Description = "Name of XML output file (Default: TestResult.xml)")]
		public string xml;

		[Option(Description = "Display XML to the console (Deprecated)")]
		public bool xmlConsole;

		[Option(Short="out", Description = "File to receive test output")]
		public string output;

		[Option(Description = "File to receive test error output")]
		public string err;

		[Option(Description = "Label each test in stdOut")]
		public bool labels = false;

		[Option(Description = "List of categories to include")]
		public string include;

		[Option(Description = "List of categories to exclude")]
		public string exclude;

		[Option(Description = "Process model for tests: Single, Separate, Multiple")]
		public ProcessModel process;

		[Option(Description = "AppDomain Usage for tests: None, Single, Multiple")]
		public DomainUsage domain;

        [Option(Description = "Framework version to be used for tests")]
        public string framework;

		[Option(Description = "Disable shadow copy when running in separate domain")]
		public bool noshadow;

		[Option (Description = "Disable use of a separate thread for tests")]
		public bool nothread;

        [Option(Description = "Set timeout for each test case in milliseconds")]
        public int timeout;

		[Option(Description = "Wait for input before closing console window")]
		public bool wait = false;

		[Option(Description = "Do not display the logo")]
		public bool nologo = false;

		[Option(Description = "Do not display progress" )]
		public bool nodots = false;

		[Option(Short="?", Description = "Display help")]
		public bool help = false;

		public ConsoleOptions( params string[] args ) : base( args ) {}

		public ConsoleOptions( bool allowForwardSlash, params string[] args ) : base( allowForwardSlash, args ) {}

		public bool Validate()
		{
			if(isInvalid) return false; 

			if(NoArgs) return true; 

			if(ParameterCount >= 1) return true; 

			return false;
		}

//		protected override bool IsValidParameter(string parm)
//		{
//			return Services.ProjectLoadService.CanLoadProject( parm ) || PathUtils.IsAssemblyFileType( parm );
//		}


        public bool IsTestProject
        {
            get
            {
                return ParameterCount == 1 && Services.ProjectService.CanLoadProject((string)Parameters[0]);
            }
        }

		public override void Help()
		{
			Console.WriteLine();
			Console.WriteLine( "NUNIT-CONSOLE [inputfiles] [options]" );
			Console.WriteLine();
			Console.WriteLine( "Runs a set of NUnit tests from the console." );
			Console.WriteLine();
			Console.WriteLine( "You may specify one or more assemblies or a single" );
			Console.WriteLine( "project file of type .nunit." );
			Console.WriteLine();
			Console.WriteLine( "Options:" );
			base.Help();
			Console.WriteLine();
			Console.WriteLine( "Options that take values may use an equal sign, a colon" );
			Console.WriteLine( "or a space to separate the option from its value." );
			Console.WriteLine();
		}
	}
}