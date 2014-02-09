// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Threading;
using NUnit.Core.Extensibility;
using NUnit.Framework;

using ThreadState = System.Threading.ThreadState;

namespace NUnit.Core.Tests
{
	/// <summary>
	/// Summary description for EventQueueTests.
	/// </summary>
    [TestFixture]
    public class EventQueueTests
    {
        static readonly Event[] events = {
				new RunStartedEvent( string.Empty, 0 ),
				new SuiteStartedEvent( null ),
                new OutputEvent( new TestOutput( string.Empty, TestOutputType.Log )),
				new TestStartedEvent( null ),
                new OutputEvent( new TestOutput( string.Empty, TestOutputType.Out )),
				new TestFinishedEvent( null ),
                new OutputEvent( new TestOutput( string.Empty, TestOutputType.Trace )),
				new SuiteFinishedEvent( null ),
				new RunFinishedEvent( (TestResult)null )
			};

        private static void EnqueueEvents(EventQueue q)
        {
            foreach (Event e in events)
                q.Enqueue(e);
        }

        private static void SendEvents(EventListener el)
        {
            foreach (Event e in events)
                e.Send(el);
        }

        private static void VerifyQueue(EventQueue q)
        {
            for (int index = 0; index < events.Length; index++)
            {
                Event e = q.Dequeue(false);
                Assert.AreEqual(events[index].GetType(), e.GetType(),
                    string.Format("Event {0}", index));
            }
        }

        private static void StartPump(EventPump pump, int waitTime)
        {
            pump.Start();
            WaitForPumpToStart(pump, waitTime);
        }

        private static void StopPump(EventPump pump, int waitTime)
        {
            pump.Stop();
            WaitForPumpToStop(pump, waitTime);
        }

        private static void WaitForPumpToStart(EventPump pump, int waitTime)
        {
            while (waitTime > 0 && pump.PumpState != EventPumpState.Pumping)
            {
                Thread.Sleep(100);
                waitTime -= 100;
            }
        }

        private static void WaitForPumpToStop(EventPump pump, int waitTime)
        {
            while (waitTime > 0 && pump.PumpState != EventPumpState.Stopped)
            {
                Thread.Sleep(100);
                waitTime -= 100;
            }
        }

        #region EventQueue tests

        [Test]
        public void QueueEvents()
        {
            EventQueue q = new EventQueue();
            EnqueueEvents(q);
            VerifyQueue(q);
        }

         [Test]
        public void DequeueEmpty()
        {
            EventQueue q = new EventQueue();
            Assert.IsNull(q.Dequeue(false));
        }

        [TestFixture]
        public class DequeueBlocking_StopTest : ProducerConsumerTest
        {
            private EventQueue q;
            private volatile int receivedEvents;

            [Test]
            [Timeout(1000)]
            public void DequeueBlocking_Stop()
            {
                this.q = new EventQueue();
                this.receivedEvents = 0;
                this.RunProducerConsumer();
                Assert.AreEqual(events.Length + 1, this.receivedEvents);
            }

            protected override void Producer()
            {
                EnqueueEvents(this.q);
                while (this.receivedEvents < events.Length)
                {
                    Thread.Sleep(30);
                }

                this.q.Stop();
            }

            protected override void Consumer()
            {
                Event e;
                do
                {
                    e = this.q.Dequeue(true);
                    this.receivedEvents++;
                    Thread.MemoryBarrier();
                }
                while (e != null);
            }
        }

        [TestFixture]
        public class SetWaitHandle_Enqueue_SynchronousTest : ProducerConsumerTest
        {
            private EventQueue q;
            private AutoResetEvent waitHandle;
            private volatile bool afterEnqueue;

            [Test]
            [Timeout(1000)]
            public void SetWaitHandle_Enqueue_Synchronous()
            {
                using (this.waitHandle = new AutoResetEvent(false))
                {
                    this.q = new EventQueue();
                    this.q.SetWaitHandleForSynchronizedEvents(this.waitHandle);
                    this.afterEnqueue = false;
                    this.RunProducerConsumer();
                }
            }

            protected override void Producer()
            {
                Event synchronousEvent = new RunStartedEvent(string.Empty, 0);
                Assert.IsTrue(synchronousEvent.IsSynchronous);
                this.q.Enqueue(synchronousEvent);
                this.afterEnqueue = true;
                Thread.MemoryBarrier();
            }

            protected override void Consumer()
            {
                this.q.Dequeue(true);
                Thread.Sleep(30);
                Assert.IsFalse(this.afterEnqueue);
                this.waitHandle.Set();
                Thread.Sleep(30);
                Assert.IsTrue(this.afterEnqueue);
            }
        }

        [TestFixture]
        public class SetWaitHandle_Enqueue_AsynchronousTest : ProducerConsumerTest
        {
            private EventQueue q;
            private volatile bool afterEnqueue;

            [Test]
            [Timeout(1000)]
            public void SetWaitHandle_Enqueue_Asynchronous()
            {
                using (AutoResetEvent waitHandle = new AutoResetEvent(false))
                {
                    this.q = new EventQueue();
                    this.q.SetWaitHandleForSynchronizedEvents(waitHandle);
                    this.afterEnqueue = false;
                    this.RunProducerConsumer();
                }
            }

            protected override void Producer()
            {
                Event asynchronousEvent = new OutputEvent(new TestOutput(string.Empty, TestOutputType.Trace));
                Assert.IsFalse(asynchronousEvent.IsSynchronous);
                this.q.Enqueue(asynchronousEvent);
                this.afterEnqueue = true;
                Thread.MemoryBarrier();
            }

            protected override void Consumer()
            {
                this.q.Dequeue(true);
                Thread.Sleep(30);
                Assert.IsTrue(this.afterEnqueue);
            }
        }

        #endregion EventQueue tests

        #region QueuingEventListener tests

        [Test]
        public void SendEvents()
        {
            QueuingEventListener el = new QueuingEventListener();
            SendEvents(el);
            VerifyQueue(el.Events);
        }

        #endregion

        #region EventPump tests

        [Test]
        public void StartAndStopPumpOnEmptyQueue()
        {
            EventQueue q = new EventQueue();
            using (EventPump pump = new EventPump(NullListener.NULL, q, false))
            {
                pump.Name = "StartAndStopPumpOnEmptyQueue";
                StartPump(pump, 1000);
                Assert.That(pump.PumpState, Is.EqualTo(EventPumpState.Pumping));
                StopPump(pump, 1000);
                Assert.That(pump.PumpState, Is.EqualTo(EventPumpState.Stopped));
            }
        }

        [Test]
        public void PumpAutoStopsOnRunFinished()
        {
            EventQueue q = new EventQueue();
            using (EventPump pump = new EventPump(NullListener.NULL, q, true))
            {
                pump.Name = "PumpAutoStopsOnRunFinished";
                Assert.That(pump.PumpState, Is.EqualTo(EventPumpState.Stopped));
                StartPump(pump, 1000);
                Assert.That(pump.PumpState, Is.EqualTo(EventPumpState.Pumping));
                q.Enqueue(new RunFinishedEvent(new Exception()));
                WaitForPumpToStop(pump, 1000);
                Assert.That(pump.PumpState, Is.EqualTo(EventPumpState.Stopped));
            }
        }

        [Test]
        [Timeout(3000)]
        public void PumpEvents()
        {
            EventQueue q = new EventQueue();
            QueuingEventListener el = new QueuingEventListener();
            using (EventPump pump = new EventPump(el, q, false))
            {
                pump.Name = "PumpEvents";
                Assert.That(pump.PumpState, Is.EqualTo(EventPumpState.Stopped));
                StartPump(pump, 1000);
                Assert.That(pump.PumpState, Is.EqualTo(EventPumpState.Pumping));
                EnqueueEvents(q);
                StopPump(pump, 1000);
                Assert.That(pump.PumpState, Is.EqualTo(EventPumpState.Stopped));
            }
            VerifyQueue(el.Events);
        }

        [Test]
        [Timeout(2000)]
        public void PumpEventsWithAutoStop()
        {
             EventQueue q = new EventQueue();
            QueuingEventListener el = new QueuingEventListener();
            using (EventPump pump = new EventPump(el, q, true))
            {
                pump.Name = "PumpEventsWithAutoStop";
                pump.Start();
                EnqueueEvents(q);
                int tries = 10;
                while (--tries > 0 && q.Count > 0)
                {
                    Thread.Sleep(100);
                }
                Assert.That(pump.PumpState, Is.EqualTo(EventPumpState.Stopped));
            }
        }

        [Test]
        [Timeout(2000)]
        public void PumpPendingEventsAfterAutoStop()
        {
            EventQueue q = new EventQueue();
            EnqueueEvents(q);
            Event[] eventsAfterStop =
            {
                new OutputEvent(new TestOutput("foo", TestOutputType.Out)),
                new OutputEvent(new TestOutput("bar", TestOutputType.Trace)),
            };
            foreach (Event e in eventsAfterStop)
            {
                q.Enqueue(e);
            }

            QueuingEventListener el = new QueuingEventListener();
            using (EventPump pump = new EventPump(el, q, true))
            {
                pump.Name = "PumpPendingEventsAfterAutoStop";
                pump.Start();
                int tries = 10;
                while (--tries > 0 && q.Count > 0)
                {
                    Thread.Sleep(100);
                }

                Assert.That(pump.PumpState, Is.EqualTo(EventPumpState.Stopped));
            }
            Assert.That(el.Events.Count, Is.EqualTo(events.Length + eventsAfterStop.Length));
        }

        [Test]
        [Timeout(1000)]
        public void PumpSynchronousAndAsynchronousEvents()
        {
            EventQueue q = new EventQueue();
            using (EventPump pump = new EventPump(NullListener.NULL, q, false))
            {
                pump.Name = "PumpSynchronousAndAsynchronousEvents";
                pump.Start();

                int numberOfAsynchronousEvents = 0;
                int sumOfAsynchronousQueueLength = 0;
                const int Repetitions = 2;
                for (int i = 0; i < Repetitions; i++)
                {
                    foreach (Event e in events)
                    {
                        q.Enqueue(e);
                        if (e.IsSynchronous)
                        {
                            Assert.That(q.Count, Is.EqualTo(0));
                        }
                        else
                        {
                            sumOfAsynchronousQueueLength += q.Count;
                            numberOfAsynchronousEvents++;
                        }
                    }
                }

                Console.WriteLine("Average queue length: {0}", (float)sumOfAsynchronousQueueLength / numberOfAsynchronousEvents);
            }
        }

        /// <summary>
        /// Verifies that when
        /// (1) Traces are captured and fed into the EventListeners, and
        /// (2) an EventListener writes Traces,
        /// the Trace / EventPump / EventListener do not deadlock.
        /// </summary>
        /// <remarks>
        /// This mainly simulates the object structure created by RemoteTestRunner.Run.
        /// </remarks>
        [Test]
        [Timeout(1000)]
        public void TracingEventListenerDoesNotDeadlock()
        {
            QueuingEventListener upstreamListener = new QueuingEventListener();
            EventQueue upstreamListenerQueue = upstreamListener.Events;

            // Install a TraceListener sending TestOutput events to the upstreamListener.
            // This simulates RemoteTestRunner.StartTextCapture, where TestContext installs such a TraceListener.
            TextWriter traceWriter = new EventListenerTextWriter(upstreamListener, TestOutputType.Trace);
            const string TraceListenerName = "TracingEventListenerDoesNotDeadlock";
            TraceListener feedingTraceToUpstreamListener = new TextWriterTraceListener(traceWriter, TraceListenerName);

            try
            {
                Trace.Listeners.Add(feedingTraceToUpstreamListener);

                // downstreamListenerToTrace simulates an EventListener installed e.g. by an Addin, 
                // which may call Trace within the EventListener methods:
                TracingEventListener downstreamListenerToTrace = new TracingEventListener();
                using (EventPump pump = new EventPump(downstreamListenerToTrace, upstreamListenerQueue, false))
                {
                    pump.Name = "TracingEventListenerDoesNotDeadlock";
                    pump.Start();

                    const int Repetitions = 10;
                    for (int i = 0; i < Repetitions; i++)
                    {
                        foreach (Event e in events)
                        {
                            Trace.WriteLine("Before sending {0} event.", e.GetType().Name);
                            e.Send(upstreamListener);
                            Trace.WriteLine("After sending {0} event.", e.GetType().Name);
                        }
                    }
                }
            }
            finally
            {
                Trace.Listeners.Remove(TraceListenerName);
                feedingTraceToUpstreamListener.Dispose();
            }
        }

        /// <summary> 
        /// Floods the queue of an EventPump with multiple concurrent event producers.
        /// Prints the maximum queue length to Console, but does not implement an
        /// oracle on what the maximum queue length should be.
        /// </summary>
        /// <param name="numberOfProducers">The number of concurrent producer threads.</param>
        /// <param name="producerDelay">
        /// If <c>true</c>, the producer threads slow down by adding a short delay time.
        /// </param>
        [TestCase(1, false)]
        [TestCase(5, true)]
        [TestCase(5, false)]
        [Explicit("Takes several seconds. Just prints the queue length of the EventPump to Console, but has no oracle regarding this.")]
        public void EventPumpQueueLength(int numberOfProducers, bool producerDelay)
        {
            EventQueue q = new EventQueue();
            EventProducer[] producers = new EventProducer[numberOfProducers];
            for (int i = 0; i < numberOfProducers; i++)
            {
                producers[i] = new EventProducer(q, i, producerDelay);
            }

            using (EventPump pump = new EventPump(NullListener.NULL, q, false))
            {
                pump.Name = "EventPumpQueueLength";
                pump.Start();

                foreach (EventProducer p in producers)
                {
                    p.ProducerThread.Start();
                }
                foreach (EventProducer p in producers)
                {
                    p.ProducerThread.Join();
                }
                pump.Stop();
            }
            Assert.That(q.Count, Is.EqualTo(0));

            foreach (EventProducer p in producers)
            {
                Console.WriteLine(
                    "#Events: {0}, MaxQueueLength: {1}", p.SentEventsCount, p.MaxQueueLength);
                Assert.IsNull(p.Exception, "{0}", p.Exception);
            }
        }

        #endregion
    
        public abstract class ProducerConsumerTest
        {
            private volatile Exception myConsumerException;

            protected void RunProducerConsumer()
            {
                this.myConsumerException = null;
                Thread consumerThread = new Thread(new ThreadStart(this.ConsumerThreadWrapper));
                try
                {
                    consumerThread.Start();
                    this.Producer();
                    bool consumerStopped = consumerThread.Join(1000);
                    Assert.IsTrue(consumerStopped);
                }
                finally
                {
                    ThreadUtility.Kill(consumerThread);
                }

                Assert.IsNull(this.myConsumerException);
            }

            protected abstract void Producer();

            protected abstract void Consumer();

            private void ConsumerThreadWrapper()
            {
                try
                {
                    this.Consumer();
                }
                catch (ThreadAbortException)
                {
                    Thread.ResetAbort();
                }
                catch (Exception ex)
                {
                    this.myConsumerException = ex;
                }
            }
        }

        private class EventProducer
        {
            public readonly Thread ProducerThread;
            public int SentEventsCount;
            public int MaxQueueLength;
            public Exception Exception;
            private readonly EventQueue queue;
            private readonly bool delay;

            public EventProducer(EventQueue q, int id, bool delay)
            {
                this.queue = q;
                this.ProducerThread = new Thread(new ThreadStart(this.Produce));
                this.ProducerThread.Name = this.GetType().FullName + id;
                this.delay = delay;
            }

            private void Produce()
            {
                try
                {
                    Event e = new OutputEvent(new TestOutput(this.ProducerThread.Name, TestOutputType.Log));
                    DateTime start = DateTime.Now;
                    while (DateTime.Now - start <= TimeSpan.FromSeconds(3))
                    {
                        this.queue.Enqueue(e);
                        this.SentEventsCount++;
                        this.MaxQueueLength = Math.Max(this.queue.Count, this.MaxQueueLength);

                        // without Sleep or with just a Sleep(0), the EventPump thread does not keep up and the queue gets very long
                        if (this.delay)
                        {
                            Thread.Sleep(1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.Exception = ex;
                }
            }
        }

        private class TracingEventListener : EventListener
        {
            #region EventListener Members
            public void RunStarted(string name, int testCount)
            {
                WriteTrace("RunStarted({0},{1})", name, testCount);
            }

            public void RunFinished(TestResult result)
            {
                WriteTrace("RunFinished({0})", result);
            }

            public void RunFinished(Exception exception)
            {
                WriteTrace("RunFinished({0})", exception);
            }

            public void TestStarted(TestName testName)
            {
                WriteTrace("TestStarted({0})", testName);
            }

            public void TestFinished(TestResult result)
            {
                WriteTrace("TestFinished({0})", result);
            }

            public void SuiteStarted(TestName testName)
            {
                WriteTrace("SuiteStarted({0})", testName);
            }

            public void SuiteFinished(TestResult result)
            {
                WriteTrace("SuiteFinished({0})", result);
            }

            public void UnhandledException(Exception exception)
            {
                WriteTrace("UnhandledException({0})", exception);
            }

            public void TestOutput(TestOutput testOutput)
            {
                if (testOutput.Type != TestOutputType.Trace)
                {
                    WriteTrace("TestOutput {0}: '{1}'", testOutput.Type, testOutput.Text);
                }
            }
            #endregion

#if CLR_2_0 || CLR_4_0
            private static void WriteTrace(string message, params object[] args)
            {
                Trace.TraceInformation(message, args);
            }
#else
            private static void WriteTrace(string message, params object[] args)
            {
                Trace.WriteLine(string.Format(message, args));
            }
#endif
        }
    }
}
