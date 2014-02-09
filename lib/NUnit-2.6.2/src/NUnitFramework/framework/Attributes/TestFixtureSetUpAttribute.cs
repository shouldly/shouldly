// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

namespace NUnit.Framework
{
	using System;

	/// <summary>
	/// Attribute used to identify a method that is 
	/// called before any tests in a fixture are run.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple=false, Inherited=true)]
	public class TestFixtureSetUpAttribute : Attribute
	{
	}
}
