// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;

namespace NUnit.TestData.CategoryAttributeTests
{
	[TestFixture, InheritableCategory("MyCategory")]
	public abstract class AbstractBase { }
	
	[TestFixture, Category( "DataBase" )]
	public class FixtureWithCategories : AbstractBase
	{
		[Test, Category("Long")]
		public void Test1() { }

		[Test, Critical]
		public void Test2() { }
	}
	
	[AttributeUsage(AttributeTargets.Method, AllowMultiple=false, Inherited=false)]
	public class CriticalAttribute : CategoryAttribute { }
	
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=true, Inherited=true)]
	public class InheritableCategoryAttribute : CategoryAttribute
	{ 
		public InheritableCategoryAttribute(string name) : base(name) { }
	}
}