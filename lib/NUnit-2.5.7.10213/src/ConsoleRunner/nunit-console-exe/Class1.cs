// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using NUnit.Core;

namespace NUnit.ConsoleRunner
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		public static int Main(string[] args)
		{
            InternalTrace.Initialize("nunit-console_%p.log");

            return Runner.Main(args);

            //InternalTrace.Close();
        }
	}
}
