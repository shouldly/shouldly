// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;

namespace NUnit.Core
{
	/// <summary>
	/// The RunState enum indicates whether a test
    /// can be executed. When used on a TestResult
    /// it may also indicate whether the test has
    /// been executed. See individual values for
    /// restrictions on use.
	/// </summary>
	public enum RunState
	{
        /// <summary>
        /// The test is not runnable.
        /// </summary>
		NotRunnable, 

        /// <summary>
        /// The test is runnable. This value would 
        /// normally not appear on a TestResult, since
        /// it would change to Executed.
        /// </summary>
		Runnable,

        /// <summary>
        /// The test can only be run explicitly. Would
        /// normally not appear on a TestResult, since
        /// it would change to Executed or Skipped.
        /// </summary>
		Explicit,

        /// <summary>
        /// The test has been skipped. This value may
        /// appear on a Test when certain attributes
        /// are used to skip the test.
        /// </summary>
		Skipped,

        /// <summary>
        /// The test has been ignored. May appear on
        /// a Test, when the IgnoreAttribute is used.
        /// Appears on a TestResult in that case or
        /// if the test is dynamically ignored.
        /// </summary>
		Ignored
	}
}
