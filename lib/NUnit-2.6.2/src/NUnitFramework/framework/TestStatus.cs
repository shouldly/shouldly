// ****************************************************************
// Copyright 2010, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

namespace NUnit.Framework
{
    /// <summary>
    /// The TestStatus enum indicates the result of running a test
    /// </summary>
    public enum TestStatus
    {
        /// <summary>
        /// The test was inconclusive
        /// </summary>
        Inconclusive = 0,

        /// <summary>
        /// The test has skipped 
        /// </summary>
        Skipped = 1,

        /// <summary>
        /// The test succeeded
        /// </summary>
        Passed = 2,

        /// <summary>
        /// The test failed
        /// </summary>
        Failed = 3
    }
}
