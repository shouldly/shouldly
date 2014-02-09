// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using NUnit.Framework;

namespace NUnit.TestData
{
	// Sample Test from a post by Scott Bellware

	[AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
	class ConcernAttribute : TestFixtureAttribute
	{
		public Type typeOfConcern;

		public ConcernAttribute( Type typeOfConcern )
		{
			this.typeOfConcern = typeOfConcern;
		}
	}

	[AttributeUsage(AttributeTargets.Method, AllowMultiple=false)]
	class SpecAttribute : TestAttribute
	{
	}

	/// <summary>
	/// Summary description for AttributeInheritance.
	/// </summary>
	[Concern(typeof(NUnit.Core.TestRunner))]
	public class When_collecting_test_fixtures
	{
		[Spec]
		public void should_include_classes_with_an_attribute_derived_from_TestFixtureAttribute()
		{
		}
	}

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    class NYIAttribute : IgnoreAttribute
    {
        public NYIAttribute() : base("Not yet implemented") { }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    class WorkInProcessAttribute : ExplicitAttribute
    {
        public WorkInProcessAttribute() : base("Work in progress") { }
    }

    public class AttributeInheritanceFixture
    {
        [Test, WorkInProcess]
        public void ShouldBeExplicit() { }

        [Test, NYI]
        public void ShouldBeIgnored() { }
    }
}
