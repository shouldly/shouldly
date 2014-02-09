// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.Collections;
using NUnit.Core;
using NUnit.Util;

namespace NUnit.TestUtilities
{
	/// <summary>
	/// Summary description for MockTestEventSource.
	/// </summary>
	public class MockTestEventSource : TestEventDispatcher, EventListener
	{
		//private string testFileName;
		private TestSuite suite;

		public MockTestEventSource( TestSuite suite )
		{
			this.suite = suite;
			//this.testFileName = testFileName;
		}

		public void SimulateTestRun()
		{
			FireRunStarting( suite.TestName.FullName, suite.TestCount );

			//TestResult result = SimulateTest( fixture, RunState.Runnable );
            TestResult result = suite.Run(this, TestFilter.Empty);

			FireRunFinished( result );
		}

//		private TestResult SimulateTest( Test test, RunState parentState )
//		{
//			if ( test.IsSuite && test.RunState != RunState.Explicit )
//			{
//				FireSuiteStarting( test.TestName );
//
//				TestResult result = new TestResult( test );
//
//				foreach( TestNode childTest in test.Tests )
//					result.AddResult( SimulateTest( childTest, test.RunState ) );
//
//				FireSuiteFinished( result );
//
//				return result;
//			}
//			else
//			{
//				FireTestStarting( test.TestName );
//				
//				TestResult result = new TestResult( test );
//
//				result.RunState = parentState == RunState.Runnable ? RunState.Executed : parentState;
//				
//				FireTestFinished( result );
//
//				return result;
//			}
//		}

		#region EventListener Members

		void EventListener.TestStarted(TestName testName)
		{
			this.FireTestStarting( testName );
		}

		void EventListener.RunStarted(string name, int testCount)
		{
			this.FireRunStarting( name, testCount );
		}

		void EventListener.RunFinished(Exception exception)
		{
			this.FireRunFinished(exception);
		}

		void EventListener.RunFinished(TestResult result)
		{
			this.FireRunFinished(result);
		}

		void EventListener.SuiteFinished(TestResult result)
		{
			this.FireSuiteFinished(result);
		}

		void EventListener.TestFinished(TestResult result)
		{
			this.FireTestFinished(result);
		}

		void EventListener.UnhandledException(Exception exception)
		{
			this.FireRunFinished(exception);
		}

		void EventListener.TestOutput(TestOutput testOutput)
		{
			this.FireTestOutput(testOutput);
		}

		void EventListener.SuiteStarted(TestName testName)
		{
			this.FireSuiteStarting(testName);
		}

		#endregion
	}
}
