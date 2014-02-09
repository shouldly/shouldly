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
    public class NamespaceSuite : TestSuite
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamespaceSuite"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public NamespaceSuite(string path) : base(path) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamespaceSuite"/> class.
        /// </summary>
        /// <param name="parentNamespace">The parent namespace.</param>
        /// <param name="suiteName">Name of the suite.</param>
        public NamespaceSuite(string parentNamespace, string suiteName) : base(parentNamespace, suiteName) { }

        /// <summary>
        /// Gets the type of the test.
        /// </summary>
        public override string TestType
        {
            get { return "Namespace"; }
        }
    }
}
