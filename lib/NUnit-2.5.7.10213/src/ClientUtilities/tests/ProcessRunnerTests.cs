// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System.Diagnostics;
using System.IO;
using NUnit.Core;
using NUnit.Core.Tests;
using NUnit.Framework;

namespace NUnit.Util.Tests
{
	/// <summary>
	/// Summary description for ProcessRunnerTests.
	/// </summary>
    [TestFixture, Timeout(10000)]
    [Platform(Exclude = "Mono", Reason = "Process Start not working correctly")]
    public class ProcessRunnerTests : BasicRunnerTests
    {
        private ProcessRunner myRunner;

        protected override TestRunner CreateRunner(int runnerID)
        {
            myRunner = new ProcessRunner(runnerID);
            return myRunner;
        }

        [TestFixtureTearDown]
        public void DestroyRunner()
        {
            if (myRunner != null)
                myRunner.Dispose();
        }
    }
}
