// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;

namespace NUnit.Core
{
	/// <summary>
	/// The ResultState enum indicates the result of running a test
	/// </summary>
	public enum ResultState
	{
        /// <summary>
        /// The result is inconclusive
        /// </summary>
        Inconclusive = 0,

        /// <summary>
        /// The test was not runnable.
        /// </summary>
		NotRunnable = 1, 

        /// <summary>
        /// The test has been skipped. 
        /// </summary>
		Skipped = 2,

        /// <summary>
        /// The test has been ignored.
        /// </summary>
		Ignored = 3,

        /// <summary>
        /// The test succeeded
        /// </summary>
		Success = 4,

        /// <summary>
        /// The test failed
        /// </summary>
		Failure = 5,

        /// <summary>
        /// The test encountered an unexpected exception
        /// </summary>
		Error = 6,

        /// <summary>
        /// The test was cancelled by the user
        /// </summary>
        Cancelled =7
	}

    /// <summary>
    /// The FailureSite enum indicates the stage of a test
    /// in which an error or failure occured.
    /// </summary>
    public enum FailureSite
    {
        /// <summary>
        /// Failure in the test itself
        /// </summary>
        Test,

        /// <summary>
        /// Failure in the SetUp method
        /// </summary>
        SetUp,

        /// <summary>
        /// Failure in the TearDown method
        /// </summary>
        TearDown,

        /// <summary>
        /// Failure of a parent test
        /// </summary>
        Parent,

        /// <summary>
        /// Failure of a child test
        /// </summary>
        Child
    }

}
