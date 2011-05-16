// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;

namespace NUnit.Core
{
    /// <summary>
    /// TestAssembly is a TestSuite that represents the execution
    /// of tests in a managed assembly.
    /// </summary>
    public class TestAssembly : TestSuite
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestAssembly"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public TestAssembly(string path) : base(path) { }

        /// <summary>
        /// Gets the type of the test.
        /// </summary>
        public override string TestType
        {
            get { return "Assembly"; }
        }
    }
}
