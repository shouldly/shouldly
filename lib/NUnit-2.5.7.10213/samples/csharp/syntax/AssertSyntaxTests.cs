// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org/?p=license&r=2.4
// ****************************************************************

using System;
using System.Collections;
using NUnit.Framework.Constraints;

namespace NUnit.Framework.Tests
{
	/// <summary>
	/// This test fixture attempts to exercise all the syntactic
	/// variations of Assert without getting into failures, errors 
	/// or corner cases. Thus, some of the tests may be duplicated 
	/// in other fixtures.
	/// 
	/// Each test performs the same operations using the classic
	/// syntax (if available) and the new syntax in both the
	/// helper-based and inherited forms.
	/// 
	/// This Fixture will eventually be duplicated in other
	/// supported languages. 
	/// </summary>
	[TestFixture]
	public class AssertSyntaxTests : AssertionHelper
	{
		#region Simple Constraint Tests
		[Test]
		public void IsNull()
		{
			object nada = null;

			// Classic syntax
			Assert.IsNull(nada);

			// Helper syntax
			Assert.That(nada, Is.Null);

			// Inherited syntax
			Expect(nada, Null);
		}

		[Test]
		public void IsNotNull()
		{
			// Classic syntax
			Assert.IsNotNull(42);

			// Helper syntax
			Assert.That(42, Is.Not.Null);

			// Inherited syntax
			Expect( 42, Not.Null );
		}

		[Test]
		public void IsTrue()
		{
			// Classic syntax
			Assert.IsTrue(2+2==4);

			// Helper syntax
			Assert.That(2+2==4, Is.True);
			Assert.That(2+2==4);

			// Inherited syntax
			Expect(2+2==4, True);
			Expect(2+2==4);
		}

		[Test]
		public void IsFalse()
		{
			// Classic syntax
			Assert.IsFalse(2+2==5);

			// Helper syntax
			Assert.That(2+2== 5, Is.False);
			
			// Inherited syntax
			Expect(2+2==5, False);
		}

		[Test]
		public void IsNaN()
		{
			double d = double.NaN;
			float f = float.NaN;

			// Classic syntax
			Assert.IsNaN(d);
			Assert.IsNaN(f);

			// Helper syntax
			Assert.That(d, Is.NaN);
			Assert.That(f, Is.NaN);
			
			// Inherited syntax
			Expect(d, NaN);
			Expect(f, NaN);
		}

		[Test]
		public void EmptyStringTests()
		{
			// Classic syntax
			Assert.IsEmpty("");
			Assert.IsNotEmpty("Hello!");

			// Helper syntax
			Assert.That("", Is.Empty);
			Assert.That("Hello!", Is.Not.Empty);

			// Inherited syntax
			Expect("", Empty);
			Expect("Hello!", Not.Empty);
		}

		[Test]
		public void EmptyCollectionTests()
		{
			// Classic syntax
			Assert.IsEmpty(new bool[0]);
			Assert.IsNotEmpty(new int[] { 1, 2, 3 });

			// Helper syntax
			Assert.That(new bool[0], Is.Empty);
			Assert.That(new int[] { 1, 2, 3 }, Is.Not.Empty);

			// Inherited syntax
			Expect(new bool[0], Empty);
			Expect(new int[] { 1, 2, 3 }, Not.Empty);
		}
		#endregion

		#region TypeConstraint Tests
		[Test]
		public void ExactTypeTests()
		{
			// Classic syntax workarounds
			Assert.AreEqual(typeof(string), "Hello".GetType());
			Assert.AreEqual("System.String", "Hello".GetType().FullName);
			Assert.AreNotEqual(typeof(int), "Hello".GetType());
			Assert.AreNotEqual("System.Int32", "Hello".GetType().FullName);

			// Helper syntax
			Assert.That("Hello", Is.TypeOf(typeof(string)));
			Assert.That("Hello", Is.Not.TypeOf(typeof(int)));
			
			// Inherited syntax
			Expect( "Hello", TypeOf(typeof(string)));
			Expect( "Hello", Not.TypeOf(typeof(int)));
		}

		[Test]
		public void InstanceOfTypeTests()
		{
			// Classic syntax
			Assert.IsInstanceOf(typeof(string), "Hello");
			Assert.IsNotInstanceOf(typeof(string), 5);

			// Helper syntax
			Assert.That("Hello", Is.InstanceOf(typeof(string)));
			Assert.That(5, Is.Not.InstanceOf(typeof(string)));

			// Inherited syntax
			Expect("Hello", InstanceOf(typeof(string)));
			Expect(5, Not.InstanceOf(typeof(string)));
		}

		[Test]
		public void AssignableFromTypeTests()
		{
			// Classic syntax
			Assert.IsAssignableFrom(typeof(string), "Hello");
			Assert.IsNotAssignableFrom(typeof(string), 5);

			// Helper syntax
			Assert.That( "Hello", Is.AssignableFrom(typeof(string)));
			Assert.That( 5, Is.Not.AssignableFrom(typeof(string)));
			
			// Inherited syntax
			Expect( "Hello", AssignableFrom(typeof(string)));
			Expect( 5, Not.AssignableFrom(typeof(string)));
		}
		#endregion

		#region StringConstraint Tests
		[Test]
		public void SubstringTests()
		{
			string phrase = "Hello World!";
			string[] array = new string[] { "abc", "bad", "dba" };
			
			// Classic Syntax
			StringAssert.Contains("World", phrase);
			
			// Helper syntax
			Assert.That(phrase, Text.Contains("World"));
			// Only available using new syntax
			Assert.That(phrase, Text.DoesNotContain("goodbye"));
			Assert.That(phrase, Text.Contains("WORLD").IgnoreCase);
			Assert.That(phrase, Text.DoesNotContain("BYE").IgnoreCase);
			Assert.That(array, Text.All.Contains( "b" ) );

			// Inherited syntax
			Expect(phrase, Contains("World"));
			// Only available using new syntax
			Expect(phrase, Not.Contains("goodbye"));
			Expect(phrase, Contains("WORLD").IgnoreCase);
			Expect(phrase, Not.Contains("BYE").IgnoreCase);
			Expect(array, All.Contains("b"));
		}

		[Test]
		public void StartsWithTests()
		{
			string phrase = "Hello World!";
			string[] greetings = new string[] { "Hello!", "Hi!", "Hola!" };

			// Classic syntax
			StringAssert.StartsWith("Hello", phrase);

			// Helper syntax
			Assert.That(phrase, Text.StartsWith("Hello"));
			// Only available using new syntax
			Assert.That(phrase, Text.DoesNotStartWith("Hi!"));
			Assert.That(phrase, Text.StartsWith("HeLLo").IgnoreCase);
			Assert.That(phrase, Text.DoesNotStartWith("HI").IgnoreCase);
			Assert.That(greetings, Text.All.StartsWith("h").IgnoreCase);

			// Inherited syntax
			Expect(phrase, StartsWith("Hello"));
			// Only available using new syntax
			Expect(phrase, Not.StartsWith("Hi!"));
			Expect(phrase, StartsWith("HeLLo").IgnoreCase);
			Expect(phrase, Not.StartsWith("HI").IgnoreCase);
			Expect(greetings, All.StartsWith("h").IgnoreCase);
		}

		[Test]
		public void EndsWithTests()
		{
			string phrase = "Hello World!";
			string[] greetings = new string[] { "Hello!", "Hi!", "Hola!" };

			// Classic Syntax
			StringAssert.EndsWith("!", phrase);

			// Helper syntax
			Assert.That(phrase, Text.EndsWith("!"));
			// Only available using new syntax
			Assert.That(phrase, Text.DoesNotEndWith("?"));
			Assert.That(phrase, Text.EndsWith("WORLD!").IgnoreCase);
			Assert.That(greetings, Text.All.EndsWith("!"));
		
			// Inherited syntax
			Expect(phrase, EndsWith("!"));
			// Only available using new syntax
			Expect(phrase, Not.EndsWith("?"));
			Expect(phrase, EndsWith("WORLD!").IgnoreCase);
			Expect(greetings, All.EndsWith("!") );
		}

		[Test]
		public void EqualIgnoringCaseTests()
		{
			string phrase = "Hello World!";

			// Classic syntax
			StringAssert.AreEqualIgnoringCase("hello world!",phrase);
            
			// Helper syntax
			Assert.That(phrase, Is.EqualTo("hello world!").IgnoreCase);
			//Only available using new syntax
			Assert.That(phrase, Is.Not.EqualTo("goodbye world!").IgnoreCase);
			Assert.That(new string[] { "Hello", "World" }, 
				Is.EqualTo(new object[] { "HELLO", "WORLD" }).IgnoreCase);
			Assert.That(new string[] {"HELLO", "Hello", "hello" },
				Is.All.EqualTo( "hello" ).IgnoreCase);
		            
			// Inherited syntax
			Expect(phrase, EqualTo("hello world!").IgnoreCase);
			//Only available using new syntax
			Expect(phrase, Not.EqualTo("goodbye world!").IgnoreCase);
			Expect(new string[] { "Hello", "World" }, 
				EqualTo(new object[] { "HELLO", "WORLD" }).IgnoreCase);
			Expect(new string[] {"HELLO", "Hello", "hello" },
				All.EqualTo( "hello" ).IgnoreCase);
		}

		[Test]
		public void RegularExpressionTests()
		{
			string phrase = "Now is the time for all good men to come to the aid of their country.";
			string[] quotes = new string[] { "Never say never", "It's never too late", "Nevermore!" };

			// Classic syntax
			StringAssert.IsMatch( "all good men", phrase );
			StringAssert.IsMatch( "Now.*come", phrase );

			// Helper syntax
			Assert.That( phrase, Text.Matches( "all good men" ) );
			Assert.That( phrase, Text.Matches( "Now.*come" ) );
			// Only available using new syntax
			Assert.That(phrase, Text.DoesNotMatch("all.*men.*good"));
			Assert.That(phrase, Text.Matches("ALL").IgnoreCase);
			Assert.That(quotes, Text.All.Matches("never").IgnoreCase);
		
			// Inherited syntax
			Expect( phrase, Matches( "all good men" ) );
			Expect( phrase, Matches( "Now.*come" ) );
			// Only available using new syntax
			Expect(phrase, Not.Matches("all.*men.*good"));
			Expect(phrase, Matches("ALL").IgnoreCase);
			Expect(quotes, All.Matches("never").IgnoreCase);
		}
		#endregion

		#region Equality Tests
		[Test]
		public void EqualityTests()
		{
			int[] i3 = new int[] { 1, 2, 3 };
			double[] d3 = new double[] { 1.0, 2.0, 3.0 };
			int[] iunequal = new int[] { 1, 3, 2 };

			// Classic Syntax
			Assert.AreEqual(4, 2 + 2);
			Assert.AreEqual(i3, d3);
			Assert.AreNotEqual(5, 2 + 2);
			Assert.AreNotEqual(i3, iunequal);

			// Helper syntax
			Assert.That(2 + 2, Is.EqualTo(4));
			Assert.That(2 + 2 == 4);
			Assert.That(i3, Is.EqualTo(d3));
			Assert.That(2 + 2, Is.Not.EqualTo(5));
			Assert.That(i3, Is.Not.EqualTo(iunequal));
		
			// Inherited syntax
			Expect(2 + 2, EqualTo(4));
			Expect(2 + 2 == 4);
			Expect(i3, EqualTo(d3));
			Expect(2 + 2, Not.EqualTo(5));
			Expect(i3, Not.EqualTo(iunequal));
		}

		[Test]
		public void EqualityTestsWithTolerance()
		{
			// CLassic syntax
			Assert.AreEqual(5.0d, 4.99d, 0.05d);
			Assert.AreEqual(5.0f, 4.99f, 0.05f);

			// Helper syntax
			Assert.That(4.99d, Is.EqualTo(5.0d).Within(0.05d));
			Assert.That(4.0d, Is.Not.EqualTo(5.0d).Within(0.5d));
			Assert.That(4.99f, Is.EqualTo(5.0f).Within(0.05f));
			Assert.That(4.99m, Is.EqualTo(5.0m).Within(0.05m));
			Assert.That(3999999999u, Is.EqualTo(4000000000u).Within(5u));
			Assert.That(499, Is.EqualTo(500).Within(5));
			Assert.That(4999999999L, Is.EqualTo(5000000000L).Within(5L));
			Assert.That(5999999999ul, Is.EqualTo(6000000000ul).Within(5ul));
		
			// Inherited syntax
			Expect(4.99d, EqualTo(5.0d).Within(0.05d));
			Expect(4.0d, Not.EqualTo(5.0d).Within(0.5d));
			Expect(4.99f, EqualTo(5.0f).Within(0.05f));
			Expect(4.99m, EqualTo(5.0m).Within(0.05m));
			Expect(499u, EqualTo(500u).Within(5u));
			Expect(499, EqualTo(500).Within(5));
			Expect(4999999999L, EqualTo(5000000000L).Within(5L));
			Expect(5999999999ul, EqualTo(6000000000ul).Within(5ul));
		}

		[Test]
		public void EqualityTestsWithTolerance_MixedFloatAndDouble()
		{
			// Bug Fix 1743844
			Assert.That(2.20492d, Is.EqualTo(2.2d).Within(0.01f),
				"Double actual, Double expected, Single tolerance");
			Assert.That(2.20492d, Is.EqualTo(2.2f).Within(0.01d),
				"Double actual, Single expected, Double tolerance" );
			Assert.That(2.20492d, Is.EqualTo(2.2f).Within(0.01f),
				"Double actual, Single expected, Single tolerance" );
			Assert.That(2.20492f, Is.EqualTo(2.2f).Within(0.01d),
				"Single actual, Single expected, Double tolerance");
			Assert.That(2.20492f, Is.EqualTo(2.2d).Within(0.01d),
				"Single actual, Double expected, Double tolerance");
			Assert.That(2.20492f, Is.EqualTo(2.2d).Within(0.01f),
				"Single actual, Double expected, Single tolerance");
		}

		[Test]
			public void EqualityTestsWithTolerance_MixingTypesGenerally()
		{
			// Extending tolerance to all numeric types
			Assert.That(202d, Is.EqualTo(200d).Within(2),
				"Double actual, Double expected, int tolerance");
			Assert.That( 4.87m, Is.EqualTo(5).Within(.25),
				"Decimal actual, int expected, Double tolerance" );
			Assert.That( 4.87m, Is.EqualTo(5ul).Within(1),
				"Decimal actual, ulong expected, int tolerance" );
			Assert.That( 487, Is.EqualTo(500).Within(25),
				"int actual, int expected, int tolerance" );
			Assert.That( 487u, Is.EqualTo(500).Within(25),
				"uint actual, int expected, int tolerance" );
			Assert.That( 487L, Is.EqualTo(500).Within(25),
				"long actual, int expected, int tolerance" );
			Assert.That( 487ul, Is.EqualTo(500).Within(25),
				"ulong actual, int expected, int tolerance" );
		}
		#endregion

		#region Comparison Tests
		[Test]
		public void ComparisonTests()
		{
			// Classic Syntax
			Assert.Greater(7, 3);
			Assert.GreaterOrEqual(7, 3);
			Assert.GreaterOrEqual(7, 7);

			// Helper syntax
			Assert.That(7, Is.GreaterThan(3));
			Assert.That(7, Is.GreaterThanOrEqualTo(3));
			Assert.That(7, Is.AtLeast(3));
			Assert.That(7, Is.GreaterThanOrEqualTo(7));
			Assert.That(7, Is.AtLeast(7));

			// Inherited syntax
			Expect(7, GreaterThan(3));
			Expect(7, GreaterThanOrEqualTo(3));
			Expect(7, AtLeast(3));
			Expect(7, GreaterThanOrEqualTo(7));
			Expect(7, AtLeast(7));

			// Classic syntax
			Assert.Less(3, 7);
			Assert.LessOrEqual(3, 7);
			Assert.LessOrEqual(3, 3);

			// Helper syntax
			Assert.That(3, Is.LessThan(7));
			Assert.That(3, Is.LessThanOrEqualTo(7));
			Assert.That(3, Is.AtMost(7));
			Assert.That(3, Is.LessThanOrEqualTo(3));
			Assert.That(3, Is.AtMost(3));
		
			// Inherited syntax
			Expect(3, LessThan(7));
			Expect(3, LessThanOrEqualTo(7));
			Expect(3, AtMost(7));
			Expect(3, LessThanOrEqualTo(3));
			Expect(3, AtMost(3));
		}
		#endregion

		#region Collection Tests
		[Test]
		public void AllItemsTests()
		{
			object[] ints = new object[] { 1, 2, 3, 4 };
			object[] doubles = new object[] { 0.99, 2.1, 3.0, 4.05 };
			object[] strings = new object[] { "abc", "bad", "cab", "bad", "dad" };

			// Classic syntax
			CollectionAssert.AllItemsAreNotNull(ints);
			CollectionAssert.AllItemsAreInstancesOfType(ints, typeof(int));
			CollectionAssert.AllItemsAreInstancesOfType(strings, typeof(string));
			CollectionAssert.AllItemsAreUnique(ints);

			// Helper syntax
			Assert.That(ints, Is.All.Not.Null);
			Assert.That(ints, Has.None.Null);
			Assert.That(ints, Is.All.InstanceOfType(typeof(int)));
			Assert.That(ints, Has.All.InstanceOfType(typeof(int)));
			Assert.That(strings, Is.All.InstanceOfType(typeof(string)));
			Assert.That(strings, Has.All.InstanceOfType(typeof(string)));
			Assert.That(ints, Is.Unique);
			// Only available using new syntax
			Assert.That(strings, Is.Not.Unique);
			Assert.That(ints, Is.All.GreaterThan(0));
			Assert.That(ints, Has.All.GreaterThan(0));
			Assert.That(ints, Has.None.LessThanOrEqualTo(0));
			Assert.That(strings, Text.All.Contains( "a" ) );
			Assert.That(strings, Has.All.Contains( "a" ) );
			Assert.That(strings, Has.Some.StartsWith( "ba" ) );
			Assert.That( strings, Has.Some.Property( "Length" ).EqualTo( 3 ) );
			Assert.That( strings, Has.Some.StartsWith( "BA" ).IgnoreCase );
			Assert.That( doubles, Has.Some.EqualTo( 1.0 ).Within( .05 ) );
		
			// Inherited syntax
			Expect(ints, All.Not.Null);
			Expect(ints, None.Null);
			Expect(ints, All.InstanceOfType(typeof(int)));
			Expect(strings, All.InstanceOfType(typeof(string)));
			Expect(ints, Unique);
			// Only available using new syntax
			Expect(strings, Not.Unique);
			Expect(ints, All.GreaterThan(0));
			Expect(ints, None.LessThanOrEqualTo(0));
			Expect(strings, All.Contains( "a" ) );
			Expect(strings, Some.StartsWith( "ba" ) );
			Expect(strings, Some.StartsWith( "BA" ).IgnoreCase );
			Expect(doubles, Some.EqualTo( 1.0 ).Within( .05 ) );
		}

		[Test]
		public void SomeItemTests()
		{
			object[] mixed = new object[] { 1, 2, "3", null, "four", 100 };
			object[] strings = new object[] { "abc", "bad", "cab", "bad", "dad" };

			// Not available using the classic syntax

			// Helper syntax
			Assert.That(mixed, Has.Some.Null);
			Assert.That(mixed, Has.Some.InstanceOfType(typeof(int)));
			Assert.That(mixed, Has.Some.InstanceOfType(typeof(string)));
			Assert.That(strings, Has.Some.StartsWith( "ba" ) );
			Assert.That(strings, Has.Some.Not.StartsWith( "ba" ) );
		
			// Inherited syntax
			Expect(mixed, Some.Null);
			Expect(mixed, Some.InstanceOfType(typeof(int)));
			Expect(mixed, Some.InstanceOfType(typeof(string)));
			Expect(strings, Some.StartsWith( "ba" ) );
			Expect(strings, Some.Not.StartsWith( "ba" ) );
		}

		[Test]
		public void NoItemTests()
		{
			object[] ints = new object[] { 1, 2, 3, 4, 5 };
			object[] strings = new object[] { "abc", "bad", "cab", "bad", "dad" };

			// Not available using the classic syntax

			// Helper syntax
			Assert.That(ints, Has.None.Null);
			Assert.That(ints, Has.None.InstanceOfType(typeof(string)));
			Assert.That(ints, Has.None.GreaterThan(99));
			Assert.That(strings, Has.None.StartsWith( "qu" ) );
		
			// Inherited syntax
			Expect(ints, None.Null);
			Expect(ints, None.InstanceOfType(typeof(string)));
			Expect(ints, None.GreaterThan(99));
			Expect(strings, None.StartsWith( "qu" ) );
		}

		[Test]
		public void CollectionContainsTests()
		{
			int[] iarray = new int[] { 1, 2, 3 };
			string[] sarray = new string[] { "a", "b", "c" };

			// Classic syntax
			Assert.Contains(3, iarray);
			Assert.Contains("b", sarray);
			CollectionAssert.Contains(iarray, 3);
			CollectionAssert.Contains(sarray, "b");
			CollectionAssert.DoesNotContain(sarray, "x");
			// Showing that Contains uses NUnit equality
			CollectionAssert.Contains( iarray, 1.0d );

			// Helper syntax
			Assert.That(iarray, Has.Member(3));
			Assert.That(sarray, Has.Member("b"));
			Assert.That(sarray, Has.No.Member("x"));
			// Showing that Contains uses NUnit equality
			Assert.That(iarray, Has.Member( 1.0d ));

			// Only available using the new syntax
			// Note that EqualTo and SameAs do NOT give
			// identical results to Contains because 
			// Contains uses Object.Equals()
			Assert.That(iarray, Has.Some.EqualTo(3));
			Assert.That(iarray, Has.Member(3));
			Assert.That(sarray, Has.Some.EqualTo("b"));
			Assert.That(sarray, Has.None.EqualTo("x"));
			Assert.That(iarray, Has.None.SameAs( 1.0d ));
			Assert.That(iarray, Has.All.LessThan(10));
			Assert.That(sarray, Has.All.Length.EqualTo(1));
			Assert.That(sarray, Has.None.Property("Length").GreaterThan(3));
		
			// Inherited syntax
			Expect(iarray, Contains(3));
			Expect(sarray, Contains("b"));
			Expect(sarray, Not.Contains("x"));

			// Only available using new syntax
			// Note that EqualTo and SameAs do NOT give
			// identical results to Contains because 
			// Contains uses Object.Equals()
			Expect(iarray, Some.EqualTo(3));
			Expect(sarray, Some.EqualTo("b"));
			Expect(sarray, None.EqualTo("x"));
			Expect(iarray, All.LessThan(10));
			Expect(sarray, All.Length.EqualTo(1));
			Expect(sarray, None.Property("Length").GreaterThan(3));
		}

		[Test]
		public void CollectionEquivalenceTests()
		{
			int[] ints1to5 = new int[] { 1, 2, 3, 4, 5 };
			int[] twothrees = new int[] { 1, 2, 3, 3, 4, 5 };
			int[] twofours = new int[] { 1, 2, 3, 4, 4, 5 };

		    // Classic syntax
		    CollectionAssert.AreEquivalent(new int[] { 2, 1, 4, 3, 5 }, ints1to5);
			CollectionAssert.AreNotEquivalent(new int[] { 2, 2, 4, 3, 5 }, ints1to5);
			CollectionAssert.AreNotEquivalent(new int[] { 2, 4, 3, 5 }, ints1to5);
			CollectionAssert.AreNotEquivalent(new int[] { 2, 2, 1, 1, 4, 3, 5 }, ints1to5);
            CollectionAssert.AreNotEquivalent(twothrees, twofours); 
		
			// Helper syntax
			Assert.That(new int[] { 2, 1, 4, 3, 5 }, Is.EquivalentTo(ints1to5));
			Assert.That(new int[] { 2, 2, 4, 3, 5 }, Is.Not.EquivalentTo(ints1to5));
			Assert.That(new int[] { 2, 4, 3, 5 }, Is.Not.EquivalentTo(ints1to5));
			Assert.That(new int[] { 2, 2, 1, 1, 4, 3, 5 }, Is.Not.EquivalentTo(ints1to5));

			// Inherited syntax
			Expect(new int[] { 2, 1, 4, 3, 5 }, EquivalentTo(ints1to5));
			Expect(new int[] { 2, 2, 4, 3, 5 }, Not.EquivalentTo(ints1to5));
			Expect(new int[] { 2, 4, 3, 5 }, Not.EquivalentTo(ints1to5));
			Expect(new int[] { 2, 2, 1, 1, 4, 3, 5 }, Not.EquivalentTo(ints1to5));
		}

		[Test]
		public void SubsetTests()
		{
			int[] ints1to5 = new int[] { 1, 2, 3, 4, 5 };

			// Classic syntax
			CollectionAssert.IsSubsetOf(new int[] { 1, 3, 5 }, ints1to5);
			CollectionAssert.IsSubsetOf(new int[] { 1, 2, 3, 4, 5 }, ints1to5);
			CollectionAssert.IsNotSubsetOf(new int[] { 2, 4, 6 }, ints1to5);
			CollectionAssert.IsNotSubsetOf(new int[] { 1, 2, 2, 2, 5 }, ints1to5);

			// Helper syntax
			Assert.That(new int[] { 1, 3, 5 }, Is.SubsetOf(ints1to5));
			Assert.That(new int[] { 1, 2, 3, 4, 5 }, Is.SubsetOf(ints1to5));
			Assert.That(new int[] { 2, 4, 6 }, Is.Not.SubsetOf(ints1to5));
		
			// Inherited syntax
			Expect(new int[] { 1, 3, 5 }, SubsetOf(ints1to5));
			Expect(new int[] { 1, 2, 3, 4, 5 }, SubsetOf(ints1to5));
			Expect(new int[] { 2, 4, 6 }, Not.SubsetOf(ints1to5));
		}
		#endregion

		#region Property Tests
		[Test]
		public void PropertyTests()
		{
			string[] array = { "abc", "bca", "xyz", "qrs" };
			string[] array2 = { "a", "ab", "abc" };
			ArrayList list = new ArrayList( array );

			// Not available using the classic syntax

			// Helper syntax
			Assert.That( list, Has.Property( "Count" ) );
			Assert.That( list, Has.No.Property( "Length" ) );

			Assert.That( "Hello", Has.Length.EqualTo( 5 ) );
			Assert.That( "Hello", Has.Length.LessThan( 10 ) );
			Assert.That( "Hello", Has.Property("Length").EqualTo(5) );
			Assert.That( "Hello", Has.Property("Length").GreaterThan(3) );

			Assert.That( array, Has.Property( "Length" ).EqualTo( 4 ) );
			Assert.That( array, Has.Length.EqualTo( 4 ) );
			Assert.That( array, Has.Property( "Length" ).LessThan( 10 ) );

			Assert.That( array, Has.All.Property("Length").EqualTo(3) );
			Assert.That( array, Has.All.Length.EqualTo( 3 ) );
			Assert.That( array, Is.All.Length.EqualTo( 3 ) );
			Assert.That( array, Has.All.Property("Length").EqualTo(3) );
			Assert.That( array, Is.All.Property("Length").EqualTo(3) );
			
			Assert.That( array2, Has.Some.Property("Length").EqualTo(2) );
			Assert.That( array2, Has.Some.Length.EqualTo(2) );
			Assert.That( array2, Has.Some.Property("Length").GreaterThan(2) );

			Assert.That( array2, Is.Not.Property("Length").EqualTo(4) );
			Assert.That( array2, Is.Not.Length.EqualTo( 4 ) );
			Assert.That( array2, Has.No.Property("Length").GreaterThan(3) );

			Assert.That( List.Map( array2 ).Property("Length"), Is.EqualTo( new int[] { 1, 2, 3 } ) );
			Assert.That( List.Map( array2 ).Property("Length"), Is.EquivalentTo( new int[] { 3, 2, 1 } ) );
			Assert.That( List.Map( array2 ).Property("Length"), Is.SubsetOf( new int[] { 1, 2, 3, 4, 5 } ) );
			Assert.That( List.Map( array2 ).Property("Length"), Is.Unique );

			Assert.That( list, Has.Count.EqualTo( 4 ) );
			
			// Inherited syntax
			Expect( list, Property( "Count" ) );
			Expect( list, Not.Property( "Nada" ) );

			Expect( "Hello", Length.EqualTo( 5 ) );
			Expect( "Hello", Property("Length").EqualTo(5) );
			Expect( "Hello", Property("Length").GreaterThan(0) );

			Expect( array, Property("Length").EqualTo(4) );
			Expect( array, Length.EqualTo(4) );
			Expect( array, Property("Length").LessThan(10));

			Expect( array, All.Length.EqualTo( 3 ) );
			Expect( array, All.Property("Length").EqualTo(3));

			Expect( array2, Some.Property("Length").EqualTo(2) );
			Expect( array2, Some.Length.EqualTo( 2 ) );
			Expect( array2, Some.Property("Length").GreaterThan(2));

			Expect( array2, None.Property("Length").EqualTo(4) );
			Expect( array2, None.Length.EqualTo( 4 ) );
			Expect( array2, None.Property("Length").GreaterThan(3));

			Expect( Map( array2 ).Property("Length"), EqualTo( new int[] { 1, 2, 3 } ) );
			Expect( Map( array2 ).Property("Length"), EquivalentTo( new int[] { 3, 2, 1 } ) );
			Expect( Map( array2 ).Property("Length"), SubsetOf( new int[] { 1, 2, 3, 4, 5 } ) );
			Expect( Map( array2 ).Property("Length"), Unique );

			Expect( list, Count.EqualTo( 4 ) );

		}
		#endregion

		#region Not Tests
		[Test]
		public void NotTests()
		{
			// Not available using the classic syntax

			// Helper syntax
			Assert.That(42, Is.Not.Null);
			Assert.That(42, Is.Not.True);
			Assert.That(42, Is.Not.False);
			Assert.That(2.5, Is.Not.NaN);
			Assert.That(2 + 2, Is.Not.EqualTo(3));
			Assert.That(2 + 2, Is.Not.Not.EqualTo(4));
			Assert.That(2 + 2, Is.Not.Not.Not.EqualTo(5));

			// Inherited syntax
			Expect(42, Not.Null);
			Expect(42, Not.True);
			Expect(42, Not.False);
			Expect(2.5, Not.NaN);
			Expect(2 + 2, Not.EqualTo(3));
			Expect(2 + 2, Not.Not.EqualTo(4));
			Expect(2 + 2, Not.Not.Not.EqualTo(5));
		}
		#endregion

		#region Operator Tests
		[Test]
		public void NotOperator()
		{
			// The ! operator is only available in the new syntax
			Assert.That(42, !Is.Null);
			// Inherited syntax
			Expect( 42, !Null );
		}

		[Test]
		public void AndOperator()
		{
			// The & operator is only available in the new syntax
			Assert.That(7, Is.GreaterThan(5) & Is.LessThan(10));
			// Inherited syntax
			Expect( 7, GreaterThan(5) & LessThan(10));
		}

		[Test]
		public void OrOperator()
		{
			// The | operator is only available in the new syntax
			Assert.That(3, Is.LessThan(5) | Is.GreaterThan(10));
			Expect( 3, LessThan(5) | GreaterThan(10));
		}

		[Test]
		public void ComplexTests()
		{
			Assert.That(7, Is.Not.Null & Is.Not.LessThan(5) & Is.Not.GreaterThan(10));
			Expect(7, Not.Null & Not.LessThan(5) & Not.GreaterThan(10));

			Assert.That(7, !Is.Null & !Is.LessThan(5) & !Is.GreaterThan(10));
			Expect(7, !Null & !LessThan(5) & !GreaterThan(10));

			// TODO: Remove #if when mono compiler can handle null
#if MONO
            Constraint x = null;
            Assert.That(7, !x & !Is.LessThan(5) & !Is.GreaterThan(10));
			Expect(7, !x & !LessThan(5) & !GreaterThan(10));
#else
			Assert.That(7, !(Constraint)null & !Is.LessThan(5) & !Is.GreaterThan(10));
			Expect(7, !(Constraint)null & !LessThan(5) & !GreaterThan(10));
#endif
		}
		#endregion
 
		#region Invalid Code Tests
		// This method contains assertions that should not compile
		// You can check by uncommenting it.
		//public void WillNotCompile()
		//{
		//    Assert.That(42, Is.Not);
		//    Assert.That(42, Is.All);
		//    Assert.That(42, Is.Null.Not);
		//    Assert.That(42, Is.Not.Null.GreaterThan(10));
		//    Assert.That(42, Is.GreaterThan(10).LessThan(99));

		//    object[] c = new object[0];
		//    Assert.That(c, Is.Null.All);
		//    Assert.That(c, Is.Not.All);
		//    Assert.That(c, Is.All.Not);
		//}
		#endregion
	}

}
