// ****************************************************************
// Copyright 2002-2003, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.IO;
using System.Threading;
using NUnit.Framework;

namespace NUnit.Tests.TimingTests
{
	/// <summary>
	/// Summary description for ClientTimeoutFixture.
	/// </summary>
	[TestFixture,Explicit]
	public class ClientTimeoutFixture
	{
		// Test using timeout greater than default of five minutes
		private readonly TimeSpan timeout = TimeSpan.FromMinutes( 6 );

		/// <summary>
		/// Test that listener is connected after
		/// a long delay. When run from GUI, this
		/// tests that TestLoader is connected. 
		/// When run from console it tests ConsoleUI.
		/// </summary>
		[Test]
		public void ListenerTimeoutTest()
		{
            Console.WriteLine( "Sleeping for {0} minutes", timeout.Minutes);

			Thread.Sleep( timeout );

            Console.WriteLine("Waking");
		}
	}
}
