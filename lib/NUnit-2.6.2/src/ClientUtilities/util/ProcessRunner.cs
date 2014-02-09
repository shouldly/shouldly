// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Services;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using NUnit.Core;

namespace NUnit.Util
{
	/// <summary>
	/// Summary description for ProcessRunner.
	/// </summary>
	public class ProcessRunner : ProxyTestRunner
	{
        static Logger log = InternalTrace.GetLogger(typeof(ProcessRunner));

		private TestAgent agent;

        private RuntimeFramework runtimeFramework;

		#region Constructors
		public ProcessRunner() : base( 0 ) { }

		public ProcessRunner( int runnerID ) : base( runnerID ) { }
		#endregion

        #region Properties
        public RuntimeFramework RuntimeFramework
        {
            get { return runtimeFramework; }
        }
        #endregion

        public override bool Load(TestPackage package)
		{
            log.Info("Loading " + package.Name);
			Unload();

            runtimeFramework = package.Settings["RuntimeFramework"] as RuntimeFramework;
            if ( runtimeFramework == null )
                 runtimeFramework = RuntimeFramework.CurrentFramework;

            bool loaded = false;

			try
			{
                if (this.agent == null)
                {
                    this.agent = Services.TestAgency.GetAgent(
                        runtimeFramework,
                        30000);

                    if (this.agent == null)
                        return false;
                }
	
				if ( this.TestRunner == null )
					this.TestRunner = agent.CreateRunner(this.runnerID);

				loaded = base.Load (package);
                return loaded;
			}
			finally
			{
                // Clean up if the load failed
				if ( !loaded ) Unload();
			}
		}

        public override void Unload()
        {
            if (Test != null)
            {
                log.Info("Unloading " + Path.GetFileName(Test.TestName.Name));
                this.TestRunner.Unload();
                this.TestRunner = null;
            }
		}

		#region IDisposable Members

		public override void Dispose()
		{
            // Do this first, because the next step will
            // make the downstream runner inaccessible.
            base.Dispose();

            if (this.agent != null)
            {
                log.Info("Stopping remote agent");
                try
                {
                    agent.Stop();
                }
                catch
                {
                    // Ignore any exception
                }
                finally
                {
                    this.agent = null;
                }
            }
        }

		#endregion
	}
}
