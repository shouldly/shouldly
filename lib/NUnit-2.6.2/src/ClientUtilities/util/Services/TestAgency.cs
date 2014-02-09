// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Collections;
using System.Collections.Specialized;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Services;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using NUnit.Core;

namespace NUnit.Util
{
	/// <summary>
	/// Enumeration used to report AgentStatus
	/// </summary>
	public enum AgentStatus
	{
		Unknown,
		Starting,
		Ready,
		Busy,
		Stopping
	}

	/// <summary>
	/// The TestAgency class provides RemoteTestAgents
	/// on request and tracks their status. Agents
	/// are wrapped in an instance of the TestAgent
	/// class. Multiple agent types are supported
	/// but only one, ProcessAgent is implemented
	/// at this time.
	/// </summary>
	public class TestAgency : ServerBase, IAgency, IService
	{
		static Logger log = InternalTrace.GetLogger(typeof(TestAgency));

		#region Private Fields
		private AgentDataBase agentData = new AgentDataBase();
		#endregion

		#region Constructors
		public TestAgency() : this( "TestAgency", 0 ) { }

		public TestAgency( string uri, int port ) : base( uri, port ) { }
		#endregion

		#region ServerBase Overrides
		public override void Stop()
		{
			foreach( AgentRecord r in agentData )
			{
				if ( !r.Process.HasExited )
				{
					if ( r.Agent != null )
					{
						r.Agent.Stop();
						r.Process.WaitForExit(10000);
					}

					if ( !r.Process.HasExited )
						r.Process.Kill();
				}
			}

			agentData.Clear();

			base.Stop ();
		}
		#endregion

		#region Public Methods - Called by Agents
		public void Register( TestAgent agent )
		{
			AgentRecord r = agentData[agent.Id];
			if ( r == null )
                throw new ArgumentException(
                    string.Format("Agent {0} is not in the agency database", agent.Id),
                    "agentId");
            r.Agent = agent;
		}

		public void ReportStatus( Guid agentId, AgentStatus status )
		{
			AgentRecord r = agentData[agentId];

			if ( r == null )
                throw new ArgumentException(
                    string.Format("Agent {0} is not in the agency database", agentId),
                    "agentId" );

			r.Status = status;
		}
		#endregion

		#region Public Methods - Called by Clients

        /// <summary>
        /// Returns true if NUnit support for the runtime specified 
        /// is installed, independent of whether the runtime itself
        /// is installed on the system.
        /// 
        /// In the current implementation, only .NET 1.x requires
        /// special handling, since all higher runtimes are 
        /// supported normally.
        /// </summary>
        /// <param name="version">The desired runtime version</param>
        /// <returns>True if NUnit support is installed</returns>
        public bool IsRuntimeVersionSupported(Version version)
        {
            return GetNUnitBinDirectory(version) != null;
        }

		public TestAgent GetAgent()
		{
			return GetAgent( RuntimeFramework.CurrentFramework, Timeout.Infinite );
		}

        public TestAgent GetAgent(int waitTime)
        {
            return GetAgent(RuntimeFramework.CurrentFramework, waitTime);
        }

        public TestAgent GetAgent(RuntimeFramework framework, int waitTime)
        {
			log.Info("Getting agent for use under {0}", framework);
 
            if (!framework.IsAvailable)
                throw new ArgumentException(
                    string.Format("The {0} framework is not available", framework),
                    "framework");

            // TODO: Decide if we should reuse agents
            //AgentRecord r = FindAvailableRemoteAgent(type);
            //if ( r == null )
            //    r = CreateRemoteAgent(type, framework, waitTime);
            return CreateRemoteAgent(framework, waitTime);
		}

		public void ReleaseAgent( TestAgent agent )
		{
			AgentRecord r = agentData[agent.Id];
			if ( r == null )
				log.Error( string.Format( "Unable to release agent {0} - not in database", agent.Id ) );
			else
			{
				r.Status = AgentStatus.Ready;
				log.Debug( "Releasing agent " + agent.Id.ToString() );
			}
		}

        //public void DestroyAgent( ITestAgent agent )
        //{
        //    AgentRecord r = agentData[agent.Id];
        //    if ( r != null )
        //    {
        //        if( !r.Process.HasExited )
        //            r.Agent.Stop();
        //        agentData[r.Id] = null;
        //    }
        //}
		#endregion

		#region Helper Methods
		private Guid LaunchAgentProcess(RuntimeFramework targetRuntime)
		{
            string agentExePath = GetTestAgentExePath(targetRuntime.ClrVersion);

            if (agentExePath == null)
                throw new ArgumentException(
                    string.Format("NUnit components for version {0} of the CLR are not installed",
                    targetRuntime.ClrVersion.ToString()), "targetRuntime");

            log.Debug("Using nunit-agent at " + agentExePath);

			Process p = new Process();
			p.StartInfo.UseShellExecute = false;
            Guid agentId = Guid.NewGuid();
            string arglist = agentId.ToString() + " " + ServerUrl;
            if (Debugger.IsAttached)
                arglist += " --launch-debugger";

            switch( targetRuntime.Runtime )
            {
                case RuntimeType.Mono:
                    p.StartInfo.FileName = NUnitConfiguration.MonoExePath;
                    string monoOptions = "--runtime=v" + targetRuntime.ClrVersion.ToString(3);
                    if (Debugger.IsAttached)
                        monoOptions += " --debug";
                    p.StartInfo.Arguments = string.Format("{0} \"{1}\" {2}", monoOptions, agentExePath, arglist);
                    break;
                case RuntimeType.Net:
                    p.StartInfo.FileName = agentExePath;

                    if (targetRuntime.ClrVersion.Build < 0)
                        targetRuntime = RuntimeFramework.GetBestAvailableFramework(targetRuntime);

                    string envVar = "v" + targetRuntime.ClrVersion.ToString(3);
                    p.StartInfo.EnvironmentVariables["COMPLUS_Version"] = envVar;

                    p.StartInfo.Arguments = arglist;
                    break;
                default:
				    p.StartInfo.FileName = agentExePath;
                    p.StartInfo.Arguments = arglist;
                    break;
			}
			
            //p.Exited += new EventHandler(OnProcessExit);
            p.Start();
            log.Info("Launched Agent process {0} - see nunit-agent_{0}.log", p.Id);
            log.Info("Command line: \"{0}\" {1}", p.StartInfo.FileName, p.StartInfo.Arguments);

			agentData.Add( new AgentRecord( agentId, p, null, AgentStatus.Starting ) );
		    return agentId;
		}

        //private void OnProcessExit(object sender, EventArgs e)
        //{
        //    Process p = sender as Process;
        //    if (p != null)
        //        agentData.Remove(p.Id);
        //}

		private AgentRecord FindAvailableAgent()
		{
			foreach( AgentRecord r in agentData )
				if ( r.Status == AgentStatus.Ready)
				{
					log.Debug( "Reusing agent {0}", r.Id );
					r.Status = AgentStatus.Busy;
					return r;
				}

			return null;
		}

		private TestAgent CreateRemoteAgent(RuntimeFramework framework, int waitTime)
		{
            Guid agentId = LaunchAgentProcess(framework);

			log.Debug( "Waiting for agent {0} to register", agentId.ToString("B") );

            int pollTime = 200;
            bool infinite = waitTime == Timeout.Infinite;

			while( infinite || waitTime > 0 )
			{
				Thread.Sleep( pollTime );
				if ( !infinite ) waitTime -= pollTime;
                TestAgent agent = agentData[agentId].Agent;
				if ( agent != null )
				{
					log.Debug( "Returning new agent {0}", agentId.ToString("B") );
                    return agent;
				}
			}

			return null;
		}

        /// <summary>
        /// Return the NUnit Bin Directory for a particular
        /// runtime version, or null if it's not installed.
        /// For normal installations, there are only 1.1 and
        /// 2.0 directories. However, this method accomodates
        /// 3.5 and 4.0 directories for the benefit of NUnit
        /// developers using those runtimes.
        /// </summary>
        private static string GetNUnitBinDirectory(Version v)
        {
            // Get current bin directory
            string dir = NUnitConfiguration.NUnitBinDirectory;

            // Return current directory if current and requested
            // versions are both >= 2 or both 1
            if ((Environment.Version.Major >= 2) == (v.Major >= 2))
                return dir;

            // Check whether special support for version 1 is installed
            if (v.Major == 1)
            {
                string altDir = Path.Combine(dir, "net-1.1");
                if (Directory.Exists(altDir))
                    return altDir;

                // The following is only applicable to the dev environment,
                // which uses parallel build directories. We try to substitute
                // one version number for another in the path.
                string[] search = new string[] { "2.0", "3.0", "3.5", "4.0" };
                string[] replace = v.Minor == 0
                    ? new string[] { "1.0", "1.1" }
                    : new string[] { "1.1", "1.0" };

                // Look for current value in path so it can be replaced
                string current = null;
                foreach (string s in search)
                    if (dir.IndexOf(s) >= 0)
                    {
                        current = s;
                        break;
                    }

                // Try the substitution
                if (current != null)
                {
                    foreach (string target in replace)
                    {
                        altDir = dir.Replace(current, target);
                        if (Directory.Exists(altDir))
                            return altDir;
                    }
                }
            }

            return null;
        }

        private static string GetTestAgentExePath(Version v)
        {
            string binDir = GetNUnitBinDirectory(v);
            if (binDir == null) return null;

#if CLR_2_0 || CLR_4_0
            Assembly a = System.Reflection.Assembly.GetEntryAssembly();
            string agentName = v.Major > 1 && a != null && a.GetName().ProcessorArchitecture == ProcessorArchitecture.X86
                ? "nunit-agent-x86.exe"
                : "nunit-agent.exe";
#else
            string agentName = "nunit-agent.exe";
#endif

            string agentExePath = Path.Combine(binDir, agentName);
            return File.Exists(agentExePath) ? agentExePath : null;
        }

        #endregion

		#region IService Members

		public void UnloadService()
		{
			this.Stop();
		}

		public void InitializeService()
		{
			this.Start();
		}

		#endregion

		#region Nested Class - AgentRecord
		private class AgentRecord
		{
			public Guid Id;
			public Process Process;
			public TestAgent Agent;
			public AgentStatus Status;

			public AgentRecord( Guid id, Process p, TestAgent a, AgentStatus s )
			{
				this.Id = id;
				this.Process = p;
				this.Agent = a;
				this.Status = s;
			}

		}
		#endregion

		#region Nested Class - AgentDataBase
		/// <summary>
		///  A simple class that tracks data about this
		///  agencies active and available agents
		/// </summary>
		private class AgentDataBase : IEnumerable
		{
			private ListDictionary agentData = new ListDictionary();

			public AgentRecord this[Guid id]
			{
				get { return (AgentRecord)agentData[id]; }
				set
				{
					if ( value == null )
						agentData.Remove( id );
					else
						agentData[id] = value;
				}
			}

			public AgentRecord this[TestAgent agent]
			{
				get
				{
					foreach( System.Collections.DictionaryEntry entry in agentData )
					{
						AgentRecord r = (AgentRecord)entry.Value;
						if ( r.Agent == agent )
							return r;
					}

					return null;
				}
			}

			public void Add( AgentRecord r )
			{
				agentData[r.Id] = r;
			}

            public void Remove(Guid agentId)
            {
                agentData.Remove(agentId);
            }

			public void Clear()
			{
				agentData.Clear();
			}

			#region IEnumerable Members
			public IEnumerator GetEnumerator()
			{
				return new AgentDataEnumerator( agentData );
			}
			#endregion

			#region Nested Class - AgentDataEnumerator
			public class AgentDataEnumerator : IEnumerator
			{
				IEnumerator innerEnum;

				public AgentDataEnumerator( IDictionary list )
				{
					innerEnum = list.GetEnumerator();
				}

				#region IEnumerator Members
				public void Reset()
				{
					innerEnum.Reset();
				}

				public object Current
				{
					get { return ((DictionaryEntry)innerEnum.Current).Value; }
				}

				public bool MoveNext()
				{
					return innerEnum.MoveNext();
				}
				#endregion
			}
			#endregion
		}

		#endregion
	}
}
