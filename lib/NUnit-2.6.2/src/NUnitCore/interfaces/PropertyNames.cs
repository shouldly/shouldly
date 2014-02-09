// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;

namespace NUnit.Core
{
    /// <summary>
    /// The PropertyNames struct lists common property names, which are
    /// accessed by reflection in the NUnit core. This provides a modicum 
    /// of type safety as opposed to using the strings directly.
    /// </summary>
    public struct PropertyNames
    {
        /// <summary>Exception Type expected from a test</summary>
        public static readonly string ExpectedException = "ExpectedException";
        /// <summary>Exception Type expected from a test (pre-2.5)</summary>
        public static readonly string LegacyExceptionType = "ExceptionType";
        /// <summary>FullName of the Exception Type expected from a test</summary>
        public static readonly string ExpectedExceptionName = "ExpectedExceptionName";
        /// <summary>FullName of the Exception Type expected from a test (pre-2.5)</summary>
        public static readonly string LegacyExceptionName = "ExceptionName";
        /// <summary>ExpectedException Message</summary>
        public static readonly string ExpectedMessage = "ExpectedMessage";
        /// <summary>ExpectedException MatchType</summary>
        public static readonly string MatchType = "MatchType";
        /// <summary>Expected return result from test</summary>
        public static readonly string ExpectedResult = "Result";
        /// <summary>Indicates whether there is an expected return result</summary>
        public static readonly string HasExpectedResult = "HasExpectedResult";
        /// <summary>Description of the test</summary>
        public static readonly string Description = "Description";
        /// <summary>Alternate test name</summary>
        public static readonly string TestName = "TestName";
        /// <summary>Arguments for the test</summary>
        public static readonly string Arguments = "Arguments";
        /// <summary>Indicates test case is ignored</summary>
        public static readonly string Ignored = "Ignored";
        /// <summary>Indicates test case is explicit</summary>
        public static readonly string Explicit = "Explicit";
        /// <summary>The reason a test case is ignored</summary>
        public static readonly string IgnoreReason = "IgnoreReason";
        /// <summary>Properties of the test</summary>
        public static readonly string Properties = "Properties";
        /// <summary>Categories of the test</summary>
        public static readonly string Categories = "Categories";
        /// <summary>Name of a category</summary>
        public static readonly string CategoryName = "Name";
        /// <summary>Reason for not running a test</summary>
        public static readonly string Reason = "Reason";
        /// <summary>Flag indicating excluded test should be marked as Ignored</summary>
        public static readonly string IgnoreExcluded = "IgnoreExcluded";
        /// <summary>Name of an addin that must be present to run a test</summary>
        public static readonly string RequiredAddin = "RequiredAddin";
    }
}
