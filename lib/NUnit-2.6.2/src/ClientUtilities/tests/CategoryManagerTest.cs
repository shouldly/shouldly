// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;
using NUnit.Core;
using NUnit.TestUtilities;
using NUnit.Tests.Assemblies;

namespace NUnit.Util.Tests
{
	[TestFixture]
	public class CategoryManagerTest
	{
		private CategoryManager categoryManager;
        string mockDll = MockAssembly.AssemblyPath;

		[SetUp]
		public void CreateCategoryManager()
		{
			categoryManager = new CategoryManager();
		}

		[Test]
		public void CanAddStringsWithoutDuplicating() 
		{
			categoryManager.Clear();
			string name1 = "Name1";
			string name2 = "Name2";
			string duplicate1 = "Name1";

			categoryManager.Add(name1);
			categoryManager.Add(name2);
			categoryManager.Add(duplicate1);

			Assert.AreEqual(2, categoryManager.Categories.Count);
		}

		[Test]
		public void CanAddStrings()
		{
			categoryManager.Add( "one" );
			categoryManager.Add( "two" );
			Assert.AreEqual( 2, categoryManager.Categories.Count );
		}

		[Test]
		public void CanClearEntries()
		{
			categoryManager.Add( "one" );
			categoryManager.Add( "two" );
			categoryManager.Clear();
			Assert.AreEqual( 0, categoryManager.Categories.Count );
		}

		[Test]
		public void CanAddTestCategories()
		{
			TestSuiteBuilder builder = new TestSuiteBuilder();
			Test suite = builder.Build( new TestPackage( mockDll ) );
			
			Test test = TestFinder.Find( "MockTest3", suite, true );
			categoryManager.AddCategories( test );
			Assert.AreEqual( 2, categoryManager.Categories.Count );
		}

		[Test]
		public void CanAddAllAvailableCategoriesInTestTree()
		{
			TestSuiteBuilder builder = new TestSuiteBuilder();
			Test suite = builder.Build( new TestPackage( mockDll ) );
			
			categoryManager.AddAllCategories( suite );
			Assert.AreEqual( MockAssembly.Categories, categoryManager.Categories.Count );
		}
	}
}
