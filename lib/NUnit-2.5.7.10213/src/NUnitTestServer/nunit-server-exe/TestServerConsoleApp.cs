// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using NUnit.Util;

namespace NUnit.TestServerApp
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class TestServerConsoleApp
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{ 
			Console.WriteLine( "Starting Server" );

			string uri = "TestServer";
			int port = 9000;

			if ( args.Length > 0 )
				uri = args[0];

			if ( args.Length > 1 )
				port = int.Parse( args[1] );

			TestServer server = new TestServer( uri, 9000 );
			server.Start();

			Console.WriteLine( "Waiting for Stop" );
			server.WaitForStop();

			Console.WriteLine( "Exiting" );
		}
	}
}
