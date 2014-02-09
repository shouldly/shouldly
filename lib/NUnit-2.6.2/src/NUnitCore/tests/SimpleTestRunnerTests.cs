// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Threading;
using NUnit.Framework;
using NUnit.Tests.Assemblies;

namespace NUnit.Core.Tests
{
	/// <summary>
	/// Tests of BaseTestRunner
	/// </summary>
	[TestFixture]
	public class SimpleTestRunnerTests : BasicRunnerTests
	{
		private SimpleTestRunner myRunner;

		protected override TestRunner CreateRunner( int runnerID )
		{
			myRunner = new SimpleTestRunner( runnerID );
			return myRunner;
		}

//		[Test]
//		public void BeginRunIsSynchronous()
//		{
//			myRunner.Load( "mock-assembly.dll" );
//			myRunner.BeginRun( NullListener.NULL );
//			Assert.IsFalse( myRunner.Running );
//		}
	}
}
