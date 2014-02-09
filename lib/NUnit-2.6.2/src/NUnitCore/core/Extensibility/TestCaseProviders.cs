// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System.Collections;
using System.Reflection;

namespace NUnit.Core.Extensibility
{
    class TestCaseProviders : ExtensionPoint, ITestCaseProvider2
    {
        public TestCaseProviders(IExtensionHost host) : base( "TestCaseProviders", host ) { }

        #region ITestCaseProvider Members

        /// <summary>
        /// Determine whether any test cases are available for a parameterized method.
        /// </summary>
        /// <param name="method">A MethodInfo representing a parameterized test</param>
        /// <returns>True if any cases are available, otherwise false.</returns>
        public bool HasTestCasesFor(MethodInfo method)
        {
            foreach (ITestCaseProvider provider in Extensions)
                if (provider.HasTestCasesFor(method))
                    return true;

            return false;
        }

        /// <summary>
        /// Return an enumeration providing test cases for use in
        /// running a paramterized test.
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public IEnumerable GetTestCasesFor(MethodInfo method)
        {
            ArrayList testcases = new ArrayList();

            foreach (ITestCaseProvider provider in Extensions)
                try
                {
                    if (provider.HasTestCasesFor(method))
                        foreach (object o in provider.GetTestCasesFor(method))
                            testcases.Add(o);
                }
                catch (System.Reflection.TargetInvocationException ex)
                {
                    testcases.Add(new ParameterSet(ex.InnerException));
                }
                catch (System.Exception ex)
                {
                    testcases.Add(new ParameterSet(ex));
                }

            return testcases;
        }

        #endregion

        #region ITestCaseProvider2 Members

        /// <summary>
        /// Determine whether any test cases are available for a parameterized method.
        /// </summary>
        /// <param name="method">A MethodInfo representing a parameterized test</param>
        /// <param name="suite">The suite for which the test case is being built</param>
        /// <returns>True if any cases are available, otherwise false.</returns>
        public bool HasTestCasesFor(MethodInfo method, Test suite)
        {
            foreach (ITestCaseProvider provider in Extensions)
            {
                if (provider is ITestCaseProvider2)
                {
                    ITestCaseProvider2 provider2 = (ITestCaseProvider2)provider;
                    if (provider2.HasTestCasesFor(method, suite))
                        return true;
                }
                else if (provider.HasTestCasesFor(method))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Return an enumeration providing test cases for use in
        /// running a paramterized test.
        /// </summary>
        /// <param name="method">A MethodInfo representing a parameterized test</param>
        /// <param name="suite">The suite for which the test case is being built</param>
        /// <returns></returns>
        public IEnumerable GetTestCasesFor(MethodInfo method, Test suite)
        {
            ArrayList testcases = new ArrayList();

            foreach (ITestCaseProvider provider in Extensions)
                try
                {
                    if (provider is ITestCaseProvider2)
                    {
                        ITestCaseProvider2 provider2 = (ITestCaseProvider2)provider;
                        if (provider2.HasTestCasesFor(method, suite))
                            foreach (object o in provider2.GetTestCasesFor(method, suite))
                                testcases.Add(o);
                    }
                    else if (provider.HasTestCasesFor(method))
                        foreach (object o in provider.GetTestCasesFor(method))
                            testcases.Add(o);
                }
                catch (System.Reflection.TargetInvocationException ex)
                {
                    testcases.Add(new ParameterSet(ex.InnerException));
                }
                catch (System.Exception ex)
                {
                    testcases.Add(new ParameterSet(ex));
                }

            return testcases;
        }
        #endregion

        #region IsValidExtension
        protected override bool IsValidExtension(object extension)
        {
            return extension is ITestCaseProvider;
        }
        #endregion
    }
}
