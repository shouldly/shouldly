// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;

namespace NUnit.Core
{
    /// <summary>
	/// Abstract base for all types of TestAgents.
    /// A TestAgent provides services of locating,
    /// loading and running tests in a particular
    /// context such as an AppDomain or Process.
	/// </summary>
	public abstract class TestAgent : MarshalByRefObject, IDisposable
	{
		#region Fields
		/// <summary>
		/// Reference to the TestAgency that controls this agent
		/// </summary>
		private IAgency agency;

		/// <summary>
		/// This agent's assigned id
		/// </summary>
		private Guid agentId;
		#endregion

		#region Constructors
        /// <summary>
        /// Constructs a TestAgent
        /// </summary>
        /// <param name="agentId"></param>
        public TestAgent(Guid agentId)
        {
            this.agentId = agentId;
        }

        /// <summary>
        /// Consructor used by TestAgency when creating
        /// an agent.
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="agency"></param>
		public TestAgent( Guid agentId, IAgency agency )
		{
			this.agency = agency;
			this.agentId = agentId;
		}
		#endregion

		#region Properties
        /// <summary>
        /// The TestAgency with which this agent is asssociated,
        /// or null if the agent is not tied to an agency.
        /// </summary>
		public IAgency Agency
		{
			get { return agency; }
		}

        /// <summary>
        /// A Guid that uniquely identifies this agent.
        /// </summary>
		public Guid Id
		{
			get { return agentId; }
		}
		#endregion

		#region Absract Methods
        /// <summary>
        /// Starts the agent, performing any required initialization
        /// </summary>
        /// <returns></returns>
        public abstract bool Start();

        /// <summary>
        /// Stops the agent, releasing any resources
        /// </summary>
        public abstract void Stop();

		/// <summary>
		///  Creates a runner using a given runner id
		/// </summary>
        public abstract TestRunner CreateRunner(int runnerId);
		#endregion

        #region IDisposable Members
        /// <summary>
        /// Dispose is overridden to stop the agent
        /// </summary>
        public void Dispose()
        {
            this.Stop();
        }
        #endregion

        #region InitializeLifeTimeService
        /// <summary>
        /// Overridden to cause object to live indefinitely
        /// </summary>
        public override object InitializeLifetimeService()
        {
            return null;
        }
        #endregion
    }
}
