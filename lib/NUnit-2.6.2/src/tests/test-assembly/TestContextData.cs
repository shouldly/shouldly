// ****************************************************************
// Copyright 2012, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;

namespace NUnit.TestData.TestContextData
{
    [TestFixture]
    public class TestStateRecordingFixture
    {
        public string stateList;
        public string statusList;

        public bool testFailure;
        public bool testInconclusive;
        public bool setUpFailure;
        public bool setUpIgnore;

        [SetUp]
        public void SetUp()
        {
            //stateList = TestContext.CurrentContext.Result.Outcome + "=>";
            stateList = TestContext.CurrentContext.Result.State + "=>";
            statusList = TestContext.CurrentContext.Result.Status + "=>";

            if (setUpFailure)
                Assert.Fail("Failure in SetUp");
            if (setUpIgnore)
                Assert.Ignore("Ignored in SetUp");
        }

        [Test]
        public void TheTest()
        {
            //stateList += TestContext.CurrentContext.Result.Outcome;
            stateList += TestContext.CurrentContext.Result.State;
            statusList += TestContext.CurrentContext.Result.Status;

            if (testFailure)
                Assert.Fail("Deliberate failure");
            if (testInconclusive)
                Assert.Inconclusive("Inconclusive test");
        }

        [TearDown]
        public void TearDown()
        {
            //stateList += "=>" + TestContext.CurrentContext.Result.Outcome;
            stateList += "=>" + TestContext.CurrentContext.Result.State;
            statusList += "=>" + TestContext.CurrentContext.Result.Status;
        }
    }
}
