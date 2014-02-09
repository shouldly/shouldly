// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

namespace NUnit.Framework
{
	using System;

	/// <summary>
	/// Attribute used in a TestFixture to identify a method that is 
    /// called immediately after each test is run. It is also used
    /// in a SetUpFixture to identify the method that is called once,
    /// after all subordinate tests have run. In either case, the method 
    /// is guaranteed to be called, even if an exception is thrown.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple=false, Inherited=true)]
	public class TearDownAttribute : Attribute
	{}
}
