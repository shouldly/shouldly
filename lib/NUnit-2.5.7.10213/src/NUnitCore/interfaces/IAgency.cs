// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;

namespace NUnit.Core
{
    /// <summary>
    /// The IAgency interface is implemented by a TestAgency in 
    /// order to allow TestAgents to register with it.
    /// </summary>
    public interface IAgency
    {
        /// <summary>
        /// Registers an agent with an agency
        /// </summary>
        /// <param name="agent"></param>
        void Register(TestAgent agent);
    }
}
