// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;
using NUnit.Core;
using NUnit.Core.Filters;

namespace NUnit.Util.Tests
{
	[TestFixture]
	public class CategoryParseTests
	{
		[Test]
		public void EmptyStringReturnsEmptyFilter()
		{
			CategoryExpression expr = new CategoryExpression( "" );
			Assert.That( expr.Filter.IsEmpty );
		}

		[Test]
		public void CanParseSimpleCategory()
		{
			CategoryExpression expr = new CategoryExpression( "Data" );
			CategoryFilter filter = (CategoryFilter)expr.Filter;
			Assert.That( filter.Categories, Is.EqualTo( new string[] { "Data" } ) );
		}

		[Test]
		public void CanParseCompoundCategory()
		{
			CategoryExpression expr = new CategoryExpression( "One , Two; Three,Four" );
			CategoryFilter filter = (CategoryFilter)expr.Filter;
			Assert.That( filter.Categories, Is.EqualTo( new string[] { "One", "Two", "Three", "Four" } ) );
		}

		[Test]
		public void CanParseExcludedCategories()
		{
			CategoryExpression expr = new CategoryExpression( "-One,Two,Three" );
			NotFilter notFilter = (NotFilter)expr.Filter;
			CategoryFilter catFilter = (CategoryFilter)notFilter.BaseFilter;
			Assert.That( catFilter.Categories, Is.EqualTo( new string[] { "One", "Two", "Three" } ) );
		}

		[Test]
		public void CanParseMultipleCategoriesWithAnd()
		{
			CategoryExpression expr = new CategoryExpression( "One + Two+Three" );
			AndFilter andFilter = (AndFilter)expr.Filter;
			Assert.That( andFilter.Filters.Length, Is.EqualTo( 3 ) );
			CategoryFilter catFilter = (CategoryFilter)andFilter.Filters[0];
			Assert.That( catFilter.Categories, Is.EqualTo( new string[] { "One"  } ) );
			catFilter = (CategoryFilter)andFilter.Filters[1];
			Assert.That( catFilter.Categories, Is.EqualTo( new string[] { "Two"  } ) );
			catFilter = (CategoryFilter)andFilter.Filters[2];
			Assert.That( catFilter.Categories, Is.EqualTo( new string[] { "Three"  } ) );
		}

		[Test]
		public void CanParseMultipleAlternatives()
		{
			CategoryExpression expr = new CategoryExpression( "One|Two|Three" );
			OrFilter orFilter = (OrFilter)expr.Filter;
			Assert.That( orFilter.Filters.Length, Is.EqualTo( 3 ) );
			CategoryFilter catFilter = (CategoryFilter)orFilter.Filters[0];
			Assert.That( catFilter.Categories, Is.EqualTo( new string[] { "One"  } ) );
			catFilter = (CategoryFilter)orFilter.Filters[1];
			Assert.That( catFilter.Categories, Is.EqualTo( new string[] { "Two"  } ) );
			catFilter = (CategoryFilter)orFilter.Filters[2];
			Assert.That( catFilter.Categories, Is.EqualTo( new string[] { "Three"  } ) );
		}

		[Test]
		public void PrecedenceTest()
		{
			CategoryExpression expr = new CategoryExpression( "A + B | C + -D,E,F" );
			OrFilter orFilter = (OrFilter)expr.Filter;

			AndFilter andFilter = (AndFilter)orFilter.Filters[0];
			CategoryFilter catFilter = (CategoryFilter)andFilter.Filters[0];
			Assert.That( catFilter.Categories, Is.EqualTo( new string[] { "A" } ) );
			catFilter = (CategoryFilter)andFilter.Filters[1];
			Assert.That( catFilter.Categories, Is.EqualTo( new string[] { "B" } ) );

			andFilter = (AndFilter)orFilter.Filters[1];
			catFilter = (CategoryFilter)andFilter.Filters[0];
			Assert.That( catFilter.Categories, Is.EqualTo( new string[] { "C" } ) );
			NotFilter notFilter = (NotFilter)andFilter.Filters[1];
			catFilter = (CategoryFilter)notFilter.BaseFilter;
			Assert.That( catFilter.Categories, Is.EqualTo( new string[] { "D", "E", "F" } ) );
		}

		[Test]
		public void PrecedenceTestWithParentheses()
		{
			CategoryExpression expr = new CategoryExpression( "A + (B | C) - D,E,F" );
			AndFilter andFilter = (AndFilter)expr.Filter;
			Assert.That( andFilter.Filters.Length, Is.EqualTo( 3 ) );

			CategoryFilter catFilter = (CategoryFilter)andFilter.Filters[0];
			Assert.That( catFilter.Categories, Is.EqualTo( new string[] { "A" } ) );

			OrFilter orFilter = (OrFilter)andFilter.Filters[1];
			catFilter = (CategoryFilter)orFilter.Filters[0];
			Assert.That( catFilter.Categories, Is.EqualTo( new string[] { "B" } ) );
			catFilter = (CategoryFilter)orFilter.Filters[1];
			Assert.That( catFilter.Categories, Is.EqualTo( new string[] { "C" } ) );

			NotFilter notFilter = (NotFilter)andFilter.Filters[2];
			catFilter = (CategoryFilter)notFilter.BaseFilter;
			Assert.That( catFilter.Categories, Is.EqualTo( new string[] { "D", "E", "F" } ) );
		}

		[Test]
		public void OrAndMinusCombined()
		{
			CategoryExpression expr = new CategoryExpression( "A|B-C-D|E" );
			OrFilter orFilter = (OrFilter)expr.Filter;
			Assert.That( orFilter.Filters.Length, Is.EqualTo( 3 ) );
			AndFilter andFilter = (AndFilter)orFilter.Filters[1];
			Assert.That( andFilter.Filters.Length, Is.EqualTo( 3 ) );
			Assert.That( andFilter.Filters[0], Is.TypeOf( typeof( CategoryFilter) ) );
			Assert.That( andFilter.Filters[1], Is.TypeOf( typeof( NotFilter) ) );
			Assert.That( andFilter.Filters[2], Is.TypeOf( typeof( NotFilter) ) );
		}

		[Test]
		public void PlusAndMinusCombined()
		{
			CategoryExpression expr = new CategoryExpression( "A+B-C-D+E" );
			AndFilter andFilter = (AndFilter)expr.Filter;
			Assert.That( andFilter.Filters.Length, Is.EqualTo( 5 ) );
			Assert.That( andFilter.Filters[0], Is.TypeOf( typeof( CategoryFilter) ) );
			Assert.That( andFilter.Filters[1], Is.TypeOf( typeof( CategoryFilter) ) );
			Assert.That( andFilter.Filters[2], Is.TypeOf( typeof( NotFilter) ) );
			Assert.That( andFilter.Filters[3], Is.TypeOf( typeof( NotFilter) ) );
			Assert.That( andFilter.Filters[4], Is.TypeOf( typeof( CategoryFilter) ) );
		}
	}
}
