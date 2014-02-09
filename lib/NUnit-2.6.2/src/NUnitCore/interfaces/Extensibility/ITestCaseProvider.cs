// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System.Collections;
using System.Reflection;

namespace NUnit.Core.Extensibility
{
    /// <summary>
    /// The ITestCaseProvider interface is used by extensions
    /// that provide data for parameterized tests, along with
    /// certain flags and other indicators used in the test.
    /// </summary>
    public interface ITestCaseProvider
    {
        /// <summary>
        /// Determine whether any test cases are available for a parameterized method.
        /// </summary>
        /// <param name="method">A MethodInfo representing a parameterized test</param>
        /// <returns>True if any cases are available, otherwise false.</returns>
        bool HasTestCasesFor(MethodInfo method);

        /// <summary>
        /// Return an IEnumerable providing test cases for use in
        /// running a paramterized test.
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        IEnumerable GetTestCasesFor(MethodInfo method);
    }

    /// <summary>
    /// ITestCaseProvider2 extends ITestCaseProvider with methods
    /// that include the suite for which the test case is being
    /// built. TestCaseProviders not needing the suite can
    /// continue to implement ITestCaseBuilder.
    /// </summary>
    public interface ITestCaseProvider2 : ITestCaseProvider
    {
        /// <summary>
        /// Determine whether any test cases are available for a parameterized method.
        /// </summary>
        /// <param name="method">A MethodInfo representing a parameterized test</param>
        /// <param name="suite">The suite for which the test case is being built</param>
        /// <returns>True if any cases are available, otherwise false.</returns>
        bool HasTestCasesFor(MethodInfo method, Test suite);

        /// <summary>
        /// Return an IEnumerable providing test cases for use in
        /// running a paramterized test.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="suite">The suite for which the test case is being built</param>
        /// <returns></returns>
        IEnumerable GetTestCasesFor(MethodInfo method, Test suite);
    }
}
