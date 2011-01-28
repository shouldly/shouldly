// ****************************************************************
// Copyright 2010, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;

namespace NUnit.Framework
{
    /// <summary>
    /// Provide the context information of the current test
    /// </summary>
#if NET_2_0
    public static class TestContext
#else
    public class TestContext
#endif
    {
        private const string contextKey = "NUnit.Framework.TestContext";
        private const string stateKey = "State";

        private static IDictionary _context;

        private static IDictionary Context
        {
            get
            {
                if (_context == null)
                    _context = (IDictionary)CallContext.GetData(contextKey);

                return _context;
            }
        }

        /// <summary>
        /// The TestState of current test. This maps to the ResultState
        /// used in nunit.core and is subject to change in the future.
        /// </summary>
        public static TestState State
        {
            get
            {
                return (TestState)Enum.ToObject(typeof(TestState), (int)Context[stateKey]);
            }
        }

        /// <summary>
        /// The TestStatus of current test. This enum will be used
        /// in future versions of NUnit and so is to be preferred
        /// to the TestState value.
        /// </summary>
        public static TestStatus Status
        {
            get
            {
                switch (State)
                {
                    default:
                    case TestState.Inconclusive:
                        return TestStatus.Inconclusive;
                    case TestState.Skipped:
                    case TestState.Ignored:
                    case TestState.NotRunnable:
                        return TestStatus.Skipped;
                    case TestState.Success:
                        return TestStatus.Passed;
                    case TestState.Failure:
                    case TestState.Error:
                    case TestState.Cancelled:
                        return TestStatus.Failed;
                }
            }
        }

        /// <summary>
        /// The name of the currently executing test. If no
        /// test is running, the name of the last test run.
        /// </summary>
        public static string TestName
        {
            get
            {
                return Context["TestName"] as string;
            }
        }

        /// <summary>
        /// The properties of the currently executing test
        /// or, if no test is running, of the last test run.
        /// </summary>
        public static IDictionary Properties
        {
            get
            {
                return Context["Properties"] as IDictionary;
            }
        }
    }
}
