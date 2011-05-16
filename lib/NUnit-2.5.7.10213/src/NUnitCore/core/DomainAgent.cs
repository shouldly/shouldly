// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Reflection;
using System.Diagnostics;

namespace NUnit.Core
{
	/// <summary>
	/// Represesents an agent that controls running of tests in
    /// an application domain.
	/// </summary>
	public class DomainAgent : TestAgent
	{
        static Logger log = InternalTrace.GetLogger(typeof(DomainAgent));

        /// <summary>
        /// Factory method used to create a DomainAgent in an AppDomain.
        /// </summary>
        /// <param name="targetDomain">The domain in which to create the agent</param>
        /// <param name="traceLevel">The level of internal tracing to use</param>
        /// <returns>A proxy for the DomainAgent in the other domain</returns>
        static public DomainAgent CreateInstance(AppDomain targetDomain)
        {
#if NET_2_0
            System.Runtime.Remoting.ObjectHandle oh = Activator.CreateInstance(
                targetDomain,
#else
			System.Runtime.Remoting.ObjectHandle oh = targetDomain.CreateInstance(
#endif
 Assembly.GetExecutingAssembly().FullName,
                typeof(DomainAgent).FullName,
                false, BindingFlags.Default, null, null, null, null, null);

            object obj = oh.Unwrap();
            Type type = obj.GetType();
            return (DomainAgent)obj;
        }

        private bool isActive;

        /// <summary>
        /// Constructs a DomainAgent specifying the trace level.
        /// </summary>
        /// <param name="traceLevel">The level of internal tracing to use</param>
		public DomainAgent() : base( Guid.NewGuid() ) { }

		#region Public Methods
        /// <summary>
        /// Creates a TestRunner for use in loading and running
        /// tests in this domain. DomainAgent always creates
        /// a RemoteTestRunner.
        /// </summary>
        /// <param name="runnerID">Runner ID to be used</param>
        /// <returns>A TestRunner</returns>
		public override TestRunner CreateRunner(int runnerID)
		{
            log.Info("Creating RemoteTestRunner");
			return new RemoteTestRunner(runnerID);
		}

        /// <summary>
        /// Starts the agent if it is no aready started.
        /// </summary>
        /// <returns></returns>
        public override bool Start()
        {
            if (!this.isActive)
            {
                log.Info("Starting");
                this.isActive = true;
            }

            return true;
        }

        /// <summary>
        /// Stops the agent if it is running
        /// </summary>
        public override void Stop()
        {
            if (this.isActive)
            {
                log.Info("Stopping");
                this.isActive = false;
            }
        }

        public AppDomain AppDomain
        {
            get { return AppDomain.CurrentDomain; }
        }
		#endregion
    }

    public class DomainInitializer : MarshalByRefObject
    {
        static Logger log;

        /// <summary>
        /// Factory method used to create a DomainInitializer in an AppDomain.
        /// </summary>
        /// <param name="targetDomain">The domain in which to create the agent</param>
        /// <param name="traceLevel">The level of internal tracing to use</param>
        /// <returns>A proxy for the DomainAgent in the other domain</returns>
        static public DomainInitializer CreateInstance(AppDomain targetDomain)
        {
#if NET_2_0
            System.Runtime.Remoting.ObjectHandle oh = Activator.CreateInstanceFrom(
                targetDomain,
#else
			System.Runtime.Remoting.ObjectHandle oh = targetDomain.CreateInstanceFrom(
#endif
                typeof(DomainInitializer).Assembly.CodeBase,
                typeof(DomainInitializer).FullName,
                false, BindingFlags.Default, null, null, null, null, null);

            object obj = oh.Unwrap();
            Type type = obj.GetType();
            return (DomainInitializer)obj;
        }

        public void InitializeDomain(TraceLevel traceLevel)
        {
            InternalTrace.Initialize("%a_%p.log", traceLevel);
            log = InternalTrace.GetLogger(typeof(DomainInitializer));

            log.Info("Initializing domain " + AppDomain.CurrentDomain.FriendlyName);
            AppDomain.CurrentDomain.DomainUnload += new EventHandler(OnDomainUnload);

            AssemblyResolver resolver = new AssemblyResolver();
            resolver.AddDirectory(NUnitConfiguration.NUnitLibDirectory);
            resolver.AddDirectory(NUnitConfiguration.AddinDirectory);
        }

        void OnDomainUnload(object sender, EventArgs e)
        {
            log.Info("Unloading domain " + AppDomain.CurrentDomain.FriendlyName);
            InternalTrace.Flush();
        }
    }
}
