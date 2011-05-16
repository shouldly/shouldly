// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;
using NUnit.TestData;

namespace NUnit.Core.Tests
{
	/// <summary>
	/// Tests for MaxTime decoration.
	/// </summary>
    [TestFixture]
    public class MaxTimeTests
	{
		[Test,MaxTime(1000)]
		public void MaxTimeNotExceeded()
		{
		}

        // TODO: We need a way to simulate the clock reliably
        [Test]
        public void MaxTimeExceeded()
        {
            Test test = TestFixtureBuilder.BuildFrom(typeof(MaxTimeFixture));
            TestResult result = test.Run(NullListener.NULL, TestFilter.Empty);
            Assert.AreEqual(ResultState.Failure, result.ResultState);
            result = (TestResult)result.Results[0];
            StringAssert.IsMatch(@"Elapsed time of \d*ms exceeds maximum of 1ms", result.Message);
        }

        [Test, MaxTime(1000)]
        [ExpectedException(typeof(AssertionException), ExpectedMessage = "Intentional Failure")]
        public void FailureReport()
        {
            Assert.Fail("Intentional Failure");
        }

        [Test]
        public void FailureReportHasPriorityOverMaxTime()
		{
            Test test = TestFixtureBuilder.BuildFrom(typeof(MaxTimeFixtureWithFailure));
            TestResult result = test.Run(NullListener.NULL, TestFilter.Empty);
            Assert.AreEqual(ResultState.Failure, result.ResultState);
            result = (TestResult)result.Results[0];
            Assert.AreEqual(ResultState.Failure, result.ResultState);
            StringAssert.IsMatch("Intentional Failure", result.Message);
        }

        [Test, MaxTime(1000), ExpectedException]
        public void ErrorReport()
        {
            throw new Exception();
        }

        [Test]
        public void ErrorReportHasPriorityOverMaxTime()
        {
            Test test = TestFixtureBuilder.BuildFrom(typeof(MaxTimeFixtureWithError));
            TestResult result = test.Run(NullListener.NULL, TestFilter.Empty);
            Assert.AreEqual(ResultState.Failure, result.ResultState);
            result = (TestResult)result.Results[0];
            Assert.AreEqual(ResultState.Error, result.ResultState);
            StringAssert.IsMatch("Exception message", result.Message);
        }
    }
}
