// ****************************************************************
// Copyright 2010, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;
using System.Reflection;
using NUnit.Framework;

namespace NUnit.UiKit.Tests
{
	[TestFixture]
	public class TestTreeTests
	{
		[Test]
		public void SameCategoryShouldNotBeSelectedMoreThanOnce()
		{
			// arrange
			TestTree target = new TestTree();

			// we need to populate the available categories
			// this can be done via TestLoader but this way the test is isolated
			FieldInfo fieldInfo = typeof (TestTree).GetField("availableCategories", BindingFlags.NonPublic | BindingFlags.Instance);
			Assert.IsNotNull(fieldInfo, "The field 'availableCategories' should be found.");
			object fieldValue = fieldInfo.GetValue(target);
			Assert.IsNotNull(fieldValue, "The value of 'availableCategories' should not be null.");
			IList availableCategories = fieldValue as IList;
			Assert.IsNotNull(availableCategories, "'availableCategories' field should be of type IList.");

			string[] expectedSelectedCategories = new string[] { "Foo", "MockCategory" };
			foreach (string availableCategory in expectedSelectedCategories)
			{
				availableCategories.Add(availableCategory);
			}

			// act
			target.SelectCategories(expectedSelectedCategories, true);
			target.SelectCategories(expectedSelectedCategories, true);
			string[] actualSelectedCategories = target.SelectedCategories;

			// assert
			CollectionAssert.AreEquivalent(expectedSelectedCategories, actualSelectedCategories);
		}
	}
}
