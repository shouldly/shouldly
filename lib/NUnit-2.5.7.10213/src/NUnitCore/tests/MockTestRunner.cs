// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Threading;
using NUnit.Framework;
using NUnit.Core;
using NUnit.Mocks;

namespace NUnit.Core.Tests
{
	/// <summary>
	/// Summary description for MockTestRunner.
	/// </summary>
	public class MockTestRunner : DynamicMock
	{
		private Thread runnerThread = null;

		public MockTestRunner() : base( "MockTestRunner", typeof( TestRunner ) ) { }

		public MockTestRunner( string name ) : base( name, typeof( TestRunner ) ) { }

		public Thread RunnerThread
		{
			get { return runnerThread; }
		}

		public override object Call(string methodName, params object[] args)
		{
			switch ( methodName )
			{
				case "Run":
					return RunCall( args );
				default:
					return base.Call (methodName, args);
			}
		}

		private object RunCall( object[] args )
		{
			EventListener listener = (EventListener) args[0];

			try
			{
				listener.RunStarted( string.Empty, 0 );
				base.Call( "Run", args );
				listener.RunFinished( (TestResult)null );
			}
			catch( Exception e )
			{
				listener.RunFinished( e );
			}

			return null;
		}

		public override void Verify()
		{
			base.Verify();

			if ( LastException != null )
				throw LastException;

			if ( runnerThread != null )
				Assert.IsFalse( Thread.CurrentThread == runnerThread, "Run should execute on a different thread" );
		}
	}
}
