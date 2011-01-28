// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;
using NUnit.Core;

namespace NUnit.Core.Tests
{
	[TestFixture]
	public class TestRunnerThreadTests
	{
		private MockTestRunner mockRunner;
		private TestRunnerThread runnerThread;

		[SetUp]
		public void CreateRunnerThread()
		{
			mockRunner = new MockTestRunner( "TestRunner" );
			runnerThread = new TestRunnerThread( (TestRunner)mockRunner.MockInstance );
			// Set Strict false to avoid faults on the worker thread
			mockRunner.Strict = false;
		}

		[Test]
		public void RunTestSuite()
		{
			mockRunner.Expect( "Run" );

			runnerThread.StartRun(NullListener.NULL, null);
			runnerThread.Wait();

			mockRunner.Verify();
		}

		[Test]
		public void RunNamedTest()
		{
			mockRunner.Expect( "Run" );

			runnerThread.StartRun( NullListener.NULL, new NUnit.Core.Filters.NameFilter( TestName.Parse( "SomeTest" ) ) );
			runnerThread.Wait();

			mockRunner.Verify();
		}

		[Test]
		public void RunMultipleTests()
		{
			NUnit.Core.Filters.NameFilter filter = new NUnit.Core.Filters.NameFilter();
			filter.Add( TestName.Parse( "Test1" ) );
			filter.Add( TestName.Parse( "Test2" ) );
			filter.Add( TestName.Parse( "Test3" ) );
			mockRunner.Expect( "Run" );

			runnerThread.StartRun( NullListener.NULL, filter );
			runnerThread.Wait();

			mockRunner.Verify();
		}
	}
}
