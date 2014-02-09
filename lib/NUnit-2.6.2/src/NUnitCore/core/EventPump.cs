// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

namespace NUnit.Core
{
	using System;
	using System.Threading;

    /// <summary>
    /// The EventPumpState enum represents the state of an
    /// EventPump.
    /// </summary>
    public enum EventPumpState
    {
        Stopped,
        Pumping,
        Stopping
    }

	/// <summary>
	/// EventPump pulls events out of an EventQueue and sends
	/// them to a listener. It is used to send events back to
	/// the client without using the CallContext of the test
	/// runner thread.
	/// </summary>
	public class EventPump : IDisposable
	{
        private static Logger log = InternalTrace.GetLogger(typeof(EventPump));

		#region Instance Variables

        /// <summary>
        /// The handle on which a thread enqueuing an event with <see cref="Event.IsSynchronous"/> == <c>true</c>
        /// waits, until the EventPump has sent the event to its listeners.
        /// </summary>
        private readonly AutoResetEvent synchronousEventSent = new AutoResetEvent(false);

		/// <summary>
		/// The downstream listener to which we send events
		/// </summary>
		private EventListener eventListener;
		
		/// <summary>
		/// The queue that holds our events
		/// </summary>
		private EventQueue events;
		
		/// <summary>
		/// Thread to do the pumping
		/// </summary>
		private Thread pumpThread;

		/// <summary>
		/// The current state of the eventpump
		/// </summary>
		private volatile EventPumpState pumpState = EventPumpState.Stopped;

		/// <summary>
		/// If true, stop after sending RunFinished
		/// </summary>
		private bool autostop;

        private string name;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="eventListener">The EventListener to receive events</param>
		/// <param name="events">The event queue to pull events from</param>
		/// <param name="autostop">Set to true to stop pump after RunFinished</param>
		public EventPump( EventListener eventListener, EventQueue events, bool autostop)
		{
			this.eventListener = eventListener;
			this.events = events;
            this.events.SetWaitHandleForSynchronizedEvents(this.synchronousEventSent);
            this.autostop = autostop;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the current state of the pump
		/// </summary>
        /// <remarks>
        /// On <c>volatile</c> and <see cref="Thread.MemoryBarrier"/>, see
        /// "http://www.albahari.com/threading/part4.aspx".
        /// </remarks>
		public EventPumpState PumpState
		{
		    get
		    {
                Thread.MemoryBarrier();
		        return pumpState;
		    }

		    set
		    {
		        this.pumpState = value;
                Thread.MemoryBarrier();
		    }
		}

        /// <summary>
        /// Gets or sets the name of this EventPump
        /// (used only internally and for testing).
        /// </summary>
	    public string Name
	    {
	        get
	        {
	            return this.name;
	        }
	        set
	        {
	            this.name = value;
	        }
	    }

		#endregion

		#region Public Methods

		/// <summary>
		/// Disposes and stops the pump.
		/// Disposes the used WaitHandle, too.
		/// </summary>
		public void Dispose()
		{
			Stop();
            ((IDisposable)this.synchronousEventSent).Dispose();
        }

		/// <summary>
		/// Start the pump
		/// </summary>
		public void Start()
		{
			if ( this.PumpState == EventPumpState.Stopped )  // Ignore if already started
			{
				this.pumpThread = new Thread( new ThreadStart( PumpThreadProc ) );
                this.pumpThread.Name = "EventPumpThread" + this.Name;
                this.pumpThread.Priority = ThreadPriority.Highest;
                this.PumpState = EventPumpState.Pumping;
                this.pumpThread.Start();
			}
		}

		/// <summary>
		/// Tell the pump to stop after emptying the queue.
		/// </summary>
		public void Stop()
		{
			if ( this.PumpState == EventPumpState.Pumping ) // Ignore extra calls
			{
                this.PumpState = EventPumpState.Stopping;
                this.events.Stop();
                this.pumpThread.Join();
			}
		}

		#endregion

		#region PumpThreadProc

		/// <summary>
		/// Our thread proc for removing items from the event
		/// queue and sending them on. Note that this would
		/// need to do more locking if any other thread were
		/// removing events from the queue.
		/// </summary>
		private void PumpThreadProc()
		{
			EventListener hostListeners = CoreExtensions.Host.Listeners;
            try
            {
                while (true)
                {
                    Event e = this.events.Dequeue( this.PumpState == EventPumpState.Pumping );
                    if ( e == null )
                        break;
                    try
                    {
                        e.Send(this.eventListener);
						e.Send(hostListeners);
                    }
                    catch (Exception ex)
                    {
                        log.Error( "Exception in event handler", ex );
                    }
                    finally
                    {
                        if ( e.IsSynchronous )
                            this.synchronousEventSent.Set();
                    }

                    if ( this.autostop && e is RunFinishedEvent )
                        this.PumpState = EventPumpState.Stopping;
                }
            }
            catch (Exception ex)
            {
                log.Error( "Exception in pump thread", ex );
            }
			finally
			{
                this.PumpState = EventPumpState.Stopped;
                //pumpThread = null;
			}
		}
		#endregion
	}
}
