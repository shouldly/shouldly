// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;

namespace NUnit.TestData.PropertyAttributeTests
{
	[TestFixture, Property("ClassUnderTest","SomeClass" )]
	public class FixtureWithProperties
	{
		[Test, Property("user","Charlie")]
		public void Test1() { }

		[Test, Property("X",10.0), Property("Y",17.0)]
		public void Test2() { }

		[Test, Priority(5)]
		public void Test3() { }
	}

	[AttributeUsage(AttributeTargets.Method, AllowMultiple=false)]
	public class PriorityAttribute : PropertyAttribute
	{
		public PriorityAttribute( int level ) : base( level ) { }
	}
}
