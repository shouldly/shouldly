// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

namespace NUnit.Framework
{
	using System;

    /// <summary>
    /// SetUpAttribute is used in a TestFixture to identify a method
    /// that is called immediately before each test is run. It is 
    /// also used in a SetUpFixture to identify the method that is
    /// called once, before any of the subordinate tests are run.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public class SetUpAttribute : Attribute
	{}
}
