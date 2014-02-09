// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using NUnit.Framework;
using NUnit.TestUtilities;
using NUnit.TestData.CategoryAttributeTests;

namespace NUnit.Core.Tests
{
	/// <summary>
	/// Summary description for CategoryAttributeTests.
	/// </summary>
	[TestFixture]
	public class CategoryAttributeTests
	{
		TestSuite fixture;

		[SetUp]
		public void CreateFixture()
		{
			fixture = TestBuilder.MakeFixture( typeof( FixtureWithCategories ) );
		}

		[Test]
		public void CategoryOnFixture()
		{
			Assert.Contains( "DataBase", fixture.Categories );
		}

		[Test]
		public void CategoryOnTestCase()
		{
			Test test1 = (Test)fixture.Tests[0];
			Assert.Contains( "Long", test1.Categories );
		}

		[Test]
		public void CanDeriveFromCategoryAttribute()
		{
			Test test2 = (Test)fixture.Tests[1];
			Assert.Contains( "Critical", test2.Categories );
		}
		
		[Test]
		public void DerivedCategoryMayBeInherited()
		{
			Assert.Contains("MyCategory", fixture.Categories);
		}

        [Test]
        public void CountTestsWithoutCategoryFilter()
        {
            Assert.That(fixture.CountTestCases(TestFilter.Empty), Is.EqualTo(2));
        }

        [TestCase("Database", Result = 0)]
        [TestCase("Long", Result = 1)]
        [TestCase("Critical", Result = 1)]
        public int CountTestsUsingCategoryFilter(string name)
        {
            TestFilter filter = new Filters.CategoryFilter(name);
            return fixture.CountTestCases(filter);
        }
    }
}
