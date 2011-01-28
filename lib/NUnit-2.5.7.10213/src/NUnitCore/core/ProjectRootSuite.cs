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
    public class ProjectRootSuite : TestSuite
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectRootSuite"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public ProjectRootSuite(string path) : base(path) { }

        /// <summary>
        /// Gets the type of the test.
        /// </summary>
        public override string TestType
        {
            get { return "Project"; }
        }
    }
}
