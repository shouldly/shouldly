// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;

namespace NUnit.Framework
{
    /// <summary>
    /// Attribute used to mark a class that contains one-time SetUp 
    /// and/or TearDown methods that apply to all the tests in a
    /// namespace or an assembly.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class SetUpFixtureAttribute : Attribute
	{
	}
}
