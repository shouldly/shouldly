// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.Collections;
using System.Collections.Specialized;

namespace NUnit.Framework
{
    /// <summary>
    /// TestCaseAttribute is used to mark parameterized test cases
    /// and provide them with their arguments.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited=false)]
    public class TestCaseAttribute : Attribute, ITestCaseData
    {
        #region Instance Variables

        private object[] arguments;
        private object expectedResult;
        private bool hasExpectedResult;
        private Type expectedExceptionType;
        private string expectedExceptionName;
        private string expectedMessage;
        private MessageMatch matchType;
        private string description;
        private string testName;
        private bool isIgnored;
        private bool isExplicit;
        private string reason;
        private string category;

        #endregion

        #region Constructors

        /// <summary>
        /// Construct a TestCaseAttribute with a list of arguments.
        /// This constructor is not CLS-Compliant
        /// </summary>
        /// <param name="arguments"></param>
        public TestCaseAttribute(params object[] arguments)
        {
         	if (arguments == null)
         		this.arguments = new object[] { null };
         	else
 	        	this.arguments = arguments;
        }

        /// <summary>
        /// Construct a TestCaseAttribute with a single argument
        /// </summary>
        /// <param name="arg"></param>
        public TestCaseAttribute(object arg)
        {
            this.arguments = new object[] { arg };
        }

        /// <summary>
        /// Construct a TestCaseAttribute with a two arguments
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public TestCaseAttribute(object arg1, object arg2)
        {
            this.arguments = new object[] { arg1, arg2 };
        }

        /// <summary>
        /// Construct a TestCaseAttribute with a three arguments
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        public TestCaseAttribute(object arg1, object arg2, object arg3)
        {
            this.arguments = new object[] { arg1, arg2, arg3 };
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the list of arguments to a test case
        /// </summary>
        public object[] Arguments
        {
            get { return arguments; }
        }

        /// <summary>
        /// Gets or sets the expected result. Use
        /// ExpectedResult by preference.
        /// </summary>
        /// <value>The result.</value>
        public object Result
        {
            get { return ExpectedResult; }
            set { ExpectedResult = value; }
        }

        /// <summary>
        /// Gets or sets the expected result.
        /// </summary>
        /// <value>The result.</value>
        public object ExpectedResult
        {
            get { return expectedResult; }
            set
            {
                expectedResult = value;
                hasExpectedResult = true;
            }
        }

        /// <summary>
        /// Gets a flag indicating whether an expected
        /// result has been set.
        /// </summary>
        public bool HasExpectedResult
        {
            get { return hasExpectedResult; }
        }

        /// <summary>
        /// Gets a list of categories associated with this test;
        /// </summary>
        public IList Categories
        {
            get { return category == null ? null : category.Split(','); }
        }

        /// <summary>
        /// Gets or sets the category associated with this test.
        /// May be a single category or a comma-separated list.
        /// </summary>
        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        /// <summary>
        /// Gets or sets the expected exception.
        /// </summary>
        /// <value>The expected exception.</value>
        public Type ExpectedException
        {
            get { return expectedExceptionType;  }
            set
            {
                expectedExceptionType = value;
                expectedExceptionName = expectedExceptionType.FullName;
            }
        }

        /// <summary>
        /// Gets or sets the name the expected exception.
        /// </summary>
        /// <value>The expected name of the exception.</value>
        public string ExpectedExceptionName
        {
            get { return expectedExceptionName; }
            set
            {
                expectedExceptionName = value;
                expectedExceptionType = null;
            }
        }

        /// <summary>
        /// Gets or sets the expected message of the expected exception
        /// </summary>
        /// <value>The expected message of the exception.</value>
        public string ExpectedMessage
        {
            get { return expectedMessage; }
         	set { expectedMessage = value; }
        }

        /// <summary>
        ///  Gets or sets the type of match to be performed on the expected message
        /// </summary>
        public MessageMatch MatchType
        {
            get { return matchType; }
            set { matchType = value; }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Gets or sets the name of the test.
        /// </summary>
        /// <value>The name of the test.</value>
        public string TestName
        {
            get { return testName; }
            set { testName = value; }
        }

        /// <summary>
        /// Gets or sets the ignored status of the test
        /// </summary>
        public bool Ignore
        {
            get { return isIgnored; }
            set { isIgnored = value; }
        }

        /// <summary>
        /// Gets or sets the ignored status of the test
        /// </summary>
        public bool Ignored
        {
            get { return isIgnored; }
            set { isIgnored = value; }
        }

        /// <summary>
        /// Gets or sets the explicit status of the test
        /// </summary>
        public bool Explicit
        {
            get { return isExplicit; }
            set { isExplicit = value; }
        }

        /// <summary>
        /// Gets or sets the reason for not running the test
        /// </summary>
        public string Reason
        {
            get { return reason; }
            set { reason = value; }
        }

        /// <summary>
        /// Gets or sets the reason for not running the test.
        /// Set has the side effect of marking the test as ignored.
        /// </summary>
        /// <value>The ignore reason.</value>
        public string IgnoreReason
        {
            get { return reason; }
            set 
            { 
                reason = value;
                isIgnored = reason != null && reason != string.Empty;
            }
        }

        #endregion
    }
}
