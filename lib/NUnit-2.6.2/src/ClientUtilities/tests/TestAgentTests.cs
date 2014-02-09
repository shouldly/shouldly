// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Diagnostics;
using NUnit.Framework;

namespace NUnit.Util.Tests
{
    [TestFixture]
    public class RemoteTestAgentTests
    {
        [Test]
        public void AgentReturnsProcessId()
        {
            RemoteTestAgent agent = new RemoteTestAgent(Guid.NewGuid(), null);
            Assert.AreEqual(Process.GetCurrentProcess().Id, agent.ProcessId);
        }
    }
}
