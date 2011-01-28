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
            return GetAgent(framework, waitTime, false);
        }

        public TestAgent GetAgent(RuntimeFramework framework, int waitTime, bool enableDebug)
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
            return CreateRemoteAgent(framework, waitTime, enableDebug);
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
		private Guid LaunchAgentProcess(RuntimeFramework targetRuntime, bool enableDebug)
		{
            string agentExePath = NUnitConfiguration.GetTestAgentExePath(targetRuntime.ClrVersion);

            if (agentExePath == null)
                throw new ArgumentException(
                    string.Format("NUnit components for version {0} of the CLR are not installed",
                    targetRuntime.ClrVersion.ToString()), "targetRuntime");

            log.Debug("Using nunit-agent at " + agentExePath);

			Process p = new Process();
			p.StartInfo.UseShellExecute = false;
            Guid agentId = Guid.NewGuid();
            string arglist = agentId.ToString() + " " + ServerUrl;
            if (enableDebug)
                arglist += " --pause";

            switch( targetRuntime.Runtime )
            {
                case RuntimeType.Mono:
                    p.StartInfo.FileName = NUnitConfiguration.MonoExePath;
                    if (enableDebug)
                        p.StartInfo.Arguments = string.Format("--debug \"{0}\" {1}", agentExePath, arglist);
                    else
                        p.StartInfo.Arguments = string.Format("\"{0}\" {1}", agentExePath, arglist);
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

		private TestAgent CreateRemoteAgent(RuntimeFramework framework, int waitTime, bool enableDebug)
		{
            Guid agentId = LaunchAgentProcess(framework, enableDebug);

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
