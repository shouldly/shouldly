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
    public class TestContext
    {
        private const string contextKey = "NUnit.Framework.TestContext";
        private const string stateKey = "Result.State";

        private IDictionary _context;

        private TestAdapter _test;
        private ResultAdapter _result;

        #region Constructor

        /// <summary>
        /// Constructs a TestContext using the provided context dictionary
        /// </summary>
        /// <param name="context">A context dictionary</param>
        public TestContext(IDictionary context)
        {
            _context = context;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get the current test context. This is created
        /// as needed. The user may save the context for
        /// use within a test, but it should not be used
        /// outside the test for which it is created.
        /// </summary>
        public static TestContext CurrentContext
        {
            get
            {
#if CLR_2_0 || CLR_4_0
                return new TestContext((IDictionary)CallContext.LogicalGetData(contextKey));
#else
                return new TestContext((IDictionary)CallContext.GetData(contextKey));
#endif
            }
        }

        /// <summary>
        /// Gets a TestAdapter representing the currently executing test in this context.
        /// </summary>
        public TestAdapter Test
        {
            get
            {
                if (_test == null)
                    _test = new TestAdapter(_context);

                return _test;
            }
        }

        /// <summary>
        /// Gets a ResultAdapter representing the current result for the test 
        /// executing in this context.
        /// </summary>
        public ResultAdapter Result
        {
            get
            {
                if (_result == null)
                    _result = new ResultAdapter(_context);

                return _result;
            }
        }

        /// <summary>
        /// Gets the directory containing the current test assembly.
        /// </summary>
        public string TestDirectory
        {
            get
            {
                return (string)_context["TestDirectory"];
            }
        }

        /// <summary>
        /// Gets the directory to be used for outputing files created
        /// by this test run.
        /// </summary>
        public string WorkDirectory
        {
            get
            {
                return (string)_context["WorkDirectory"];
            }
        }

        #endregion

        #region Nested TestAdapter Class

        /// <summary>
        /// TestAdapter adapts a Test for consumption by
        /// the user test code.
        /// </summary>
        public class TestAdapter
        {
            private IDictionary _context;

            #region Constructor

            /// <summary>
            /// Constructs a TestAdapter for this context
            /// </summary>
            /// <param name="context">The context dictionary</param>
            public TestAdapter(IDictionary context)
            {
                _context = context;
            }

            #endregion

            #region Properties

            /// <summary>
            /// The name of the test.
            /// </summary>
            public string Name
            {
                get
                {
                    return _context["Test.Name"] as string;
                }
            }

            /// <summary>
            /// The FullName of the test
            /// </summary>
            public string FullName
            {
                get
                {
                    return _context["Test.FullName"] as string;
                }
            }

            /// <summary>
            /// The properties of the test.
            /// </summary>
            public IDictionary Properties
            {
                get
                {
                    return _context["Test.Properties"] as IDictionary;
                }
            }

            #endregion
        }

        #endregion

        #region Nested ResultAdapter Class

        /// <summary>
        /// ResultAdapter adapts a TestResult for consumption by
        /// the user test code.
        /// </summary>
        public class ResultAdapter
        {
            private IDictionary _context;

            #region Constructor

            /// <summary>
            /// Construct a ResultAdapter for a context
            /// </summary>
            /// <param name="context">The context holding the result</param>
            public ResultAdapter(IDictionary context)
            {
                this._context = context;
            }

            #endregion

            #region Properties

            /// <summary>
            /// The TestState of current test. This maps to the ResultState
            /// used in nunit.core and is subject to change in the future.
            /// </summary>
            public TestState State
            {
                get
                {
                    return (TestState)_context["Result.State"];
                }
            }

            /// <summary>
            /// The TestStatus of current test. This enum will be used
            /// in future versions of NUnit and so is to be preferred
            /// to the TestState value.
            /// </summary>
            public TestStatus Status
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


            #endregion
        }

        #endregion
    }
}
