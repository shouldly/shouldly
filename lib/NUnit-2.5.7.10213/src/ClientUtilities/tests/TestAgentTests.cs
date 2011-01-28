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

        [Test]
        public void CanLocateAgentExecutable()
        {
            string path = NUnit.Core.NUnitConfiguration.GetTestAgentExePath(Environment.Version);
            Assert.That(System.IO.File.Exists(path), "Cannot find " + path);
        }

        [Test]
        public void CanLocateBinDirForAllInstalledVersions()
        {
            foreach (NUnit.Core.RuntimeFramework availableVersion in NUnit.Core.RuntimeFramework.AvailableFrameworks)
            {
                string path = NUnit.Core.NUnitConfiguration.GetNUnitBinDirectory(availableVersion.ClrVersion);
                if (path != null) // All version may not be installed
                    Assert.That(System.IO.Directory.Exists(path), "Directory {0} does not exist", path);
            }
        }

        [Test]
        public void CanLocateAgentExeForAllInstalledVersions()
        {
            foreach (NUnit.Core.RuntimeFramework availableVersion in NUnit.Core.RuntimeFramework.AvailableFrameworks)
            {
                string path = NUnit.Core.NUnitConfiguration.GetTestAgentExePath(availableVersion.ClrVersion);
                if (path != null)
                    Assert.That(System.IO.File.Exists(path), "File {0} does not exist", path);
            }
        }
    }
}
