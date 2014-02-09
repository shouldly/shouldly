using System;
using System.Diagnostics;
using System.Threading;


using NUnit.Framework;
using PNUnit.Framework;

namespace TestLibraries
{
    [TestFixture, Explicit("PNUnit Test")]
    //TestGroup that runs their tests without any synchronization. Each test runs in parallel
    public class ParallelExample
    {
        private const int MAX_ITERATIONS = 5;
        [Test]
        public void ParallelTest_A()
        {
            //Obtain the sleep time from the configuration file parameters
            string[] testParams = PNUnitServices.Get().GetTestParams();
            int sleepTime = int.Parse(testParams[0]);

            int count = 0;
            while (count < MAX_ITERATIONS)
            {
                PNUnitServices.Get().WriteLine(
                    string.Format("Starting  ParallelTest_A, Time: {0}. Iteration:{1}",
                        DateTime.Now.ToString(),
                        count + 1));

                PNUnitServices.Get().WriteLine(
                    string.Format("Sleeping  ParallelTest_A for {1} seconds, Time: {0}", 
                        DateTime.Now.ToString(),
                        sleepTime));

                Thread.Sleep(sleepTime * 1000);

                PNUnitServices.Get().WriteLine(
                    string.Format("Waking up ParallelTest_A, Time: {0}", 
                    DateTime.Now.ToString()));
                
                count++;
            }
        }

        [Test]
        public void ParallelTest_B()
        {
            string[] testParams = PNUnitServices.Get().GetTestParams();
            int sleepTime = int.Parse(testParams[0]);

            int count = 0;
            while (count < MAX_ITERATIONS)
            {
                PNUnitServices.Get().WriteLine(
                    string.Format("Starting  ParallelTest_B, Time: {0}. Iteration:{1}",
                    DateTime.Now.ToString(),
                    count + 1));

                PNUnitServices.Get().WriteLine(
                    string.Format("Sleeping  ParallelTest_B for {1} seconds, Time: {0}", 
                    DateTime.Now.ToString(),
                    sleepTime));

                Thread.Sleep(sleepTime * 1000);

                PNUnitServices.Get().WriteLine(
                    string.Format("Waking up ParallelTest_B, Time: {0}", 
                    DateTime.Now.ToString()));
                
                count++;
            }
        }
    }

    [TestFixture, Explicit("PNUnit Test")]
    public class ParallelExampleWithBarriers
    {        
        public const string START_BARRIER = "START_BARRIER";
        public const string WAIT_BARRIER  = "WAIT_BARRIER";

        [Test]
        public void ParallelTestWithBarriersA()
        {
            //First step should be the initialization of synchronization barriers
            PNUnitServices.Get().InitBarriers();

            PNUnitServices.Get().WriteLine(
                string.Format(
                    "ParallelTestWithBarriersA: Waiting for peer synchronization before starting... Time:{0}", 
                    DateTime.Now.ToString()));

            PNUnitServices.Get().EnterBarrier(START_BARRIER);
            PNUnitServices.Get().WriteLine(string.Format(
                "ParallelTestWithBarriersA: Sync start performed!... Time:{0}", DateTime.Now.ToString()));

            PNUnitServices.Get().WriteLine(
                string.Format("Sleeping  ParallelTestWithBarriersA for 5 seconds, Time: {0}", 
                DateTime.Now.ToString()));

            Thread.Sleep(5000);

            PNUnitServices.Get().WriteLine(
                string.Format(
                "ParallelTestWithBarriersA: Waiting for peer synchronization after sleeping... Time:{0}", 
                DateTime.Now.ToString()));

            PNUnitServices.Get().EnterBarrier(WAIT_BARRIER);
            PNUnitServices.Get().WriteLine(string.Format("ParallelTestWithBarriersA: Sync end!. Time:{0}", DateTime.Now.ToString()));
        }

        [Test]
        public void ParallelTestWithBarriersB()
        {
            PNUnitServices.Get().InitBarriers();

            PNUnitServices.Get().WriteLine(
                string.Format(
                "ParallelTestWithBarriersB: About to sleep for 4 seconds before starting... Time:{0}", 
                DateTime.Now.ToString()));

            Thread.Sleep(4000);

            PNUnitServices.Get().EnterBarrier(START_BARRIER);
            PNUnitServices.Get().WriteLine(string.Format(
                "ParallelTestWithBarriersB: Sync start performed!... Time:{0}", DateTime.Now.ToString()));

            PNUnitServices.Get().WriteLine(
                string.Format("Sleeping  ParallelTestWithBarriersB for 1 second, Time: {0}", 
                DateTime.Now.ToString()));

            Thread.Sleep(1000);

            PNUnitServices.Get().WriteLine(
                string.Format(
                "ParallelTestWithBarriersB: Waiting for peer synchronization after sleeping... Time:{0}", 
                DateTime.Now.ToString()));

            PNUnitServices.Get().EnterBarrier(WAIT_BARRIER);
            PNUnitServices.Get().WriteLine(string.Format("ParallelTestWithBarriersB: Sync end!. Time:{0}", DateTime.Now.ToString()));
        }
    }
}
