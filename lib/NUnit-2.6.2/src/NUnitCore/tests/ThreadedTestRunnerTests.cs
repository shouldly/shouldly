// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Threading;
using NUnit.Framework;

namespace NUnit.Core.Tests
{
	/// <summary>
	/// Summary description for ThreadedTestRunnerTests.
	/// </summary>
	[TestFixture]
	public class ThreadedTestRunnerTests : BasicRunnerTests
	{
		protected override TestRunner CreateRunner( int runnerID )
		{
			return new ThreadedTestRunner( new SimpleTestRunner( runnerID ), ApartmentState.Unknown, ThreadPriority.Normal );
		}

	}
}
