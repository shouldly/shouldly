using System;
using System.Collections;
using System.Threading;

using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;

using log4net;

using NUnit.Core;

using PNUnit.Framework;

#if NUNIT_2_5
using TestInfo = PNUnit.Framework.TestInfo;
#endif

namespace PNUnit.Launcher
{
	public class Runner: MarshalByRefObject, IPNUnitServices
	{

		private static readonly ILog log = LogManager.GetLogger(typeof(Runner));
		private const string agentkey = "_AGENT";

		private ParallelTest mTestGroup;
		private Thread mThread = null;
		private IList mResults = null;
		private Object mResultLock = new Object();
		private ManualResetEvent mFinish;
		private Hashtable mBarriers;
		private int mLaunchedTests;
		private Hashtable mBarriersOfTests;

		public Runner(ParallelTest test)
		{
			mTestGroup = test;
			mResults = new ArrayList();
		}

		public string TestGroupName
		{
			get{ return mTestGroup.Name; }
		}

		public void Run()
		{
			if( mTestGroup.Tests.Length == 0 )
			{
				log.Fatal("No tests to run, exiting");
				return;
			}
			mThread = new Thread(new ThreadStart(ThreadProc));
			mThread.Start();
		}

		public void Join()
		{
			if( mThread != null )
				mThread.Join();
		}

		private void ThreadProc()
		{
			log.DebugFormat("Thread created for TestGroup {0} with {1} tests", mTestGroup.Name, mTestGroup.Tests.Length);
			mFinish = new ManualResetEvent(false);
			mBarriers = new Hashtable();
			mBarriersOfTests = new Hashtable();

			RemotingServices.Marshal(this, mTestGroup.Name);

			mLaunchedTests = 0;
			foreach( TestConf test in mTestGroup.Tests )
			{
				if (test.Machine.StartsWith(agentkey))
					test.Machine = mTestGroup.Agents[int.Parse(test.Machine.Substring(agentkey.Length))-1];

				log.InfoFormat("Starting {0} test {1} on {2}", mTestGroup.Name, test.Name, test.Machine);
				// contact the machine
				try
				{
					IPNUnitAgent agent = (IPNUnitAgent)
						Activator.GetObject(
						typeof(IPNUnitAgent), 
						string.Format(
						"tcp://{0}/{1}", 
						test.Machine, 
						PNUnit.Framework.Names.PNUnitAgentServiceName));

					lock( mResultLock )
					{
						++mLaunchedTests;
					}
                                    
					agent.RunTest(new TestInfo(test.Name, test.Assembly, test.TestToRun, test.TestParams, this));
				}
				catch( Exception e )
				{
					log.ErrorFormat(
						"An error occurred trying to contact {0} [{1}]", 
						test.Machine, e.Message);

					lock( mResultLock )
					{
						--mLaunchedTests;
					}
				}
			}
            
			log.DebugFormat("Thread going to wait for results for TestGroup {0}", mTestGroup.Name);
			if( HasToWait() )
				// wait for all tests to end
				mFinish.WaitOne();

			log.DebugFormat("Thread going to wait for NotifyResult to finish for TestGroup {0}", mTestGroup.Name);
			Thread.Sleep(500); // wait for the NotifyResult call to finish
			RemotingServices.Disconnect(this);
			log.DebugFormat("Thread going to finish for TestGroup {0}", mTestGroup.Name);
		}
        
		private bool HasToWait()
		{
			lock( mResultLock )
			{
				return (mLaunchedTests > 0) && (mResults.Count < mLaunchedTests);
			}
		}

		public PNUnitTestResult[] GetTestResults()
		{
			lock(mResultLock)
			{
				PNUnitTestResult[] result = new PNUnitTestResult[mResults.Count];
				int i = 0;
				foreach( PNUnitTestResult res in mResults )
					result[i++] = res;
                
				return result;
			}
		}

		#region MarshallByRefObject
		// Lives forever
		public override object InitializeLifetimeService()
		{
			return null;
		}
		#endregion

		#region IPNUnitServices

		public void NotifyResult(string TestName, PNUnitTestResult result)
		{   
			log.DebugFormat("NotifyResult called for TestGroup {0}, Test {1}",
				mTestGroup.Name, TestName);
			lock(mResultLock)
			{
				log.DebugFormat("NotifyResult lock entered for TestGroup {0}, Test {1}",
					mTestGroup.Name, TestName);
                
				mResults.Add(result);
				if( mResults.Count == mLaunchedTests )
				{
					log.DebugFormat("All the tests notified the results, waking up. mResults.Count == {0}",
						mResults.Count);
					mFinish.Set();
				}

			}   
			lock( mBarriers )
			{
				if( mBarriersOfTests.Contains(TestName) )
				{
					log.DebugFormat("Going to abandon barriers of test {0}", TestName);
					IList list = (IList) mBarriersOfTests[TestName];
					foreach( string barrier in list )
					{
						log.DebugFormat("Abandoning barrier {0}", barrier);
						((Barrier)mBarriers[barrier]).Abandon();
					}
				}
			}
			log.DebugFormat("NotifyResult finishing for TestGroup {0}, Test {1}.",
				mTestGroup.Name, TestName); 
			log.InfoFormat("Result for TestGroup {0}, Test {1}: {2}",
				mTestGroup.Name, TestName, result.IsSuccess ? "PASS" : "FAIL");
		}

		public void InitBarrier(string TestName, string barrier, int Max)
		{
			lock( mBarriers )
			{
				if( ! mBarriers.Contains(barrier) )
				{
					mBarriers.Add(barrier, new Barrier(Max));
				}

				if( mBarriersOfTests.Contains(TestName) )
				{
					IList listofbarriers = (IList) mBarriersOfTests[TestName];
					listofbarriers.Add(barrier);
					log.DebugFormat("Adding barrier {0} to {1}", barrier, TestName);
				}
				else
				{
					ArrayList list = new ArrayList();
					list.Add(barrier);
					log.DebugFormat("Adding barrier {0} to {1}", barrier, TestName);
					mBarriersOfTests.Add(TestName, list);
				}

                
			}
		}

		public void InitBarrier(string TestName, string barrier)
		{
			InitBarrier(TestName, barrier, mTestGroup.Tests.Length);
		}

		private const int indexStartBarrier = 2;
		private const int indexEndBarrier = 3;

		public void InitBarriers (string TestName)
		{
			Hashtable barriers = new Hashtable();
			for (int i=1; i< mTestGroup.Tests.Length; i++)
			{
				string sb = mTestGroup.Tests[i].TestParams[indexStartBarrier];
				string eb = mTestGroup.Tests[i].TestParams[indexEndBarrier];

				if (sb.Trim() != "") 
				{
					if(barriers.Contains(sb))
						barriers[sb] = (int)barriers[sb]+1;
					else
						barriers[sb] = 1;
				}

				if (eb.Trim() != "") 
				{
					if(barriers.Contains(eb))
						barriers[eb] = (int)barriers[eb]+1;
					else
						barriers[eb] = 1;
				}

			}

			foreach (string key in barriers.Keys)
			{
				if (!key.Equals(Names.ServerBarrier) && !key.Equals(Names.EndBarrier))
					InitBarrier (TestName, key, (int)barriers[key]);
                
				InitBarrier (TestName, Names.ServerBarrier);
				InitBarrier (TestName, Names.EndBarrier);
			}
		}

		public void EnterBarrier(string barrier)
		{
			log.DebugFormat("Entering Barrier {0}", barrier);
			((Barrier)mBarriers[barrier]).Enter();
		}

		#endregion
	}

	/*    public class TestResult
		{
			public string TestName;
			public string Result;

			public TestResult(string name, string result)
			{
				TestName = name;
				Result = result;
			}
		}*/
}
