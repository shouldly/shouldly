// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.IO;
using System.Reflection;

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
#if CLR_2_0 || CLR_4_0
            System.Runtime.Remoting.ObjectHandle oh = Activator.CreateInstance(
                targetDomain,
#else
			System.Runtime.Remoting.ObjectHandle oh = targetDomain.CreateInstance(
#endif
 Assembly.GetExecutingAssembly().FullName,
                typeof(DomainAgent).FullName,
                false, BindingFlags.Default, null, null, null, null, null);

            object obj = oh.Unwrap();
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
#if CLR_2_0 || CLR_4_0
            System.Runtime.Remoting.ObjectHandle oh = Activator.CreateInstanceFrom(
                targetDomain,
#else
			System.Runtime.Remoting.ObjectHandle oh = targetDomain.CreateInstanceFrom(
#endif
                typeof(DomainInitializer).Assembly.CodeBase,
                typeof(DomainInitializer).FullName,
                false, BindingFlags.Default, null, null, null, null, null);

            object obj = oh.Unwrap();
            return (DomainInitializer)obj;
        }

        public void InitializeDomain(int level)
        {
            InternalTraceLevel traceLevel = (InternalTraceLevel)level;

            InternalTrace.Initialize("%a_%p.log", traceLevel);
            log = InternalTrace.GetLogger(typeof(DomainInitializer));

            AppDomain domain = AppDomain.CurrentDomain;
            
            log.Info("Initializing domain {0}", domain.FriendlyName);
            log.Debug("  Base Directory: {0}", domain.BaseDirectory);
            log.Debug("  Probing Path: {0}", domain.SetupInformation.PrivateBinPath);

            domain.DomainUnload += new EventHandler(OnDomainUnload);

            AssemblyResolver resolver = new AssemblyResolver();
            resolver.AddDirectory(NUnitConfiguration.NUnitLibDirectory);
            resolver.AddDirectory(NUnitConfiguration.AddinDirectory);
						
			// TODO: Temporary additions till we resolve a problem with pnunit
            // Test for existence is needed to avoid messing when the installation
            // does not include pnunit.
            string binDir = NUnitConfiguration.NUnitBinDirectory;
            string pnunitFrameworkPath = Path.Combine(binDir, "pnunit.framework.dll");
            if (File.Exists(pnunitFrameworkPath))
                resolver.AddFile(pnunitFrameworkPath);
            string pnunitAgentPath = Path.Combine(binDir, "pnunit-agent.exe");
            if (File.Exists(pnunitAgentPath))
                resolver.AddFile(pnunitAgentPath);
        }

        void OnDomainUnload(object sender, EventArgs e)
        {
            log.Info("Unloading domain " + AppDomain.CurrentDomain.FriendlyName);
            InternalTrace.Flush();
        }
    }
}
