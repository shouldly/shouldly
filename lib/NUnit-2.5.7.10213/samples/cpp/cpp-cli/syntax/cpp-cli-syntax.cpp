// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org/?p=license&r=2.4.
// ****************************************************************

using namespace NUnit::Framework;
using NUnit::Framework::Is;
using NUnit::Framework::Text;
using NUnit::Framework::List;
using NUnit::Framework::Has;
using System::String;

namespace NUnitSamples
{
	[TestFixture]
	public ref class AssertSyntaxTests : AssertionHelper
	{
	public:
		[Test]
		void IsNull()
		{
			Object ^nada = nullptr;

			// Classic syntax
			Assert::IsNull(nada);

			// Helper syntax
			Assert::That(nada, Is::Null);

			// Inherited syntax
			Expect(nada, Null);
		}

		[Test]
		void IsNotNull()
		{
			// Classic syntax
			Assert::IsNotNull(42);

			// Helper syntax
			Assert::That(42, Is::Not->Null);

			// Inherited syntax
			Expect( 42, Not->Null );
		}

		[Test]
		void IsTrue()
		{
			// Classic syntax
			Assert::IsTrue(2+2==4);

			// Helper syntax
			Assert::That(2+2==4, Is::True);
			Assert::That(2+2==4);

			// Inherited syntax
			Expect(2+2==4, True);
			Expect(2+2==4);
		}

		[Test]
		void IsFalse()
		{
			// Classic syntax
			Assert::IsFalse(2+2==5);

			// Helper syntax
			Assert::That(2+2==5, Is::False);
			
			// Inherited syntax
			Expect(2+2==5, False);
		}

		[Test]
		void IsNaN()
		{
			double d = double::NaN;
			float f = float::NaN;

			// Classic syntax
			Assert::IsNaN(d);
			Assert::IsNaN(f);

			// Helper syntax
			Assert::That(d, Is::NaN);
			Assert::That(f, Is::NaN);
			
			// Inherited syntax
			Expect(d, NaN);
			Expect(f, NaN);
		}

		[Test]
		void EmptyStringTests()
		{
			// Classic syntax
			Assert::IsEmpty("");
			Assert::IsNotEmpty("Hello!");

			// Helper syntax
			Assert::That("", Is::Empty);
			Assert::That("Hello!", Is::Not->Empty);

			// Inherited syntax
			Expect("", Empty);
			Expect("Hello!", Not->Empty);
		}

		[Test]
		void EmptyCollectionTests()
		{
			// Classic syntax
			Assert::IsEmpty(gcnew array<bool>(0));
			Assert::IsNotEmpty(gcnew array<int>(3));

			// Helper syntax
			Assert::That(gcnew array<bool>(0), Is::Empty);
			Assert::That(gcnew array<int>(3), Is::Not->Empty);

			// Inherited syntax
			Expect(gcnew array<bool>(0), Empty);
			Expect(gcnew array<int>(3), Not->Empty);
		}

		[Test]
		void ExactTypeTests()
		{
			// Classic syntax workarounds)
			String^ greeting = "Hello";
			Assert::AreEqual(String::typeid, greeting->GetType());
			Assert::AreEqual("System.String", greeting->GetType()->FullName);
			Assert::AreNotEqual(int::typeid, greeting->GetType());
			Assert::AreNotEqual("System.Int32", greeting->GetType()->FullName);

			// Helper syntax
			Assert::That(greeting, Is::TypeOf(String::typeid));
			Assert::That(greeting, Is::Not->TypeOf(int::typeid));
			
			// Inherited syntax
			Expect( "Hello", TypeOf(String::typeid));
			Expect( "Hello", Not->TypeOf(int::typeid));
		}

		[Test]
		void InstanceOfTypeTests()
		{
			// Classic syntax
			Assert::IsInstanceOfType(String::typeid, "Hello");
			Assert::IsNotInstanceOfType(String::typeid, 5);

			// Helper syntax
			Assert::That("Hello", Is::InstanceOfType(String::typeid));
			Assert::That(5, Is::Not->InstanceOfType(String::typeid));

			// Inherited syntax
			Expect("Hello", InstanceOfType(String::typeid));
			Expect(5, Not->InstanceOfType(String::typeid));
		}

		[Test]
		void AssignableFromTypeTests()
		{
			// Classic syntax
			Assert::IsAssignableFrom(String::typeid, "Hello");
			Assert::IsNotAssignableFrom(String::typeid, 5);

			// Helper syntax
			Assert::That( "Hello", Is::AssignableFrom(String::typeid));
			Assert::That( 5, Is::Not->AssignableFrom(String::typeid));
			
			// Inherited syntax
			Expect( "Hello", AssignableFrom(String::typeid));
			Expect( 5, Not->AssignableFrom(String::typeid));
		}

		[Test]
		void SubstringTests()
		{
			String^ phrase = "Hello World!";
			array<String^>^ strings = {"abc", "bad", "dba" };
			
			// Classic Syntax
			StringAssert::Contains("World", phrase);
			
			// Helper syntax
			Assert::That(phrase, Contains("World"));
			// Only available using new syntax
			Assert::That(phrase, Text::DoesNotContain("goodbye"));
			Assert::That(phrase, Text::Contains("WORLD")->IgnoreCase);
			Assert::That(phrase, Text::DoesNotContain("BYE")->IgnoreCase);
			Assert::That(strings, Text::All->Contains( "b" ) );

			// Inherited syntax
			Expect(phrase, Contains("World"));
			// Only available using new syntax
			Expect(phrase, Not->Contains("goodbye"));
			Expect(phrase, Contains("WORLD")->IgnoreCase);
			Expect(phrase, Not->Contains("BYE")->IgnoreCase);
			Expect(strings, All->Contains("b"));
		}

		[Test]
		void StartsWithTests()
		{
			String^ phrase = "Hello World!";
			array<String^>^ greetings = { "Hello!", "Hi!", "Hola!" };

			// Classic syntax
			StringAssert::StartsWith("Hello", phrase);

			// Helper syntax
			Assert::That(phrase, Text::StartsWith("Hello"));
			// Only available using new syntax
			Assert::That(phrase, Text::DoesNotStartWith("Hi!"));
			Assert::That(phrase, Text::StartsWith("HeLLo")->IgnoreCase);
			Assert::That(phrase, Text::DoesNotStartWith("HI")->IgnoreCase);
			Assert::That(greetings, Text::All->StartsWith("h")->IgnoreCase);

			// Inherited syntax
			Expect(phrase, StartsWith("Hello"));
			// Only available using new syntax
			Expect(phrase, Not->StartsWith("Hi!"));
			Expect(phrase, StartsWith("HeLLo")->IgnoreCase);
			Expect(phrase, Not->StartsWith("HI")->IgnoreCase);
			Expect(greetings, All->StartsWith("h")->IgnoreCase);
		}

		[Test]
		void EndsWithTests()
		{
			String^ phrase = "Hello World!";
			array<String^>^ greetings = { "Hello!", "Hi!", "Hola!" };

			// Classic Syntax
			StringAssert::EndsWith("!", phrase);

			// Helper syntax
			Assert::That(phrase, Text::EndsWith("!"));
			// Only available using new syntax
			Assert::That(phrase, Text::DoesNotEndWith("?"));
			Assert::That(phrase, Text::EndsWith("WORLD!")->IgnoreCase);
			Assert::That(greetings, Text::All->EndsWith("!"));
		
			// Inherited syntax
			Expect(phrase, EndsWith("!"));
			// Only available using new syntax
			Expect(phrase, Not->EndsWith("?"));
			Expect(phrase, EndsWith("WORLD!")->IgnoreCase);
			Expect(greetings, All->EndsWith("!") );
		}

		[Test]
		void EqualIgnoringCaseTests()
		{
			String^ phrase = "Hello World!";

			// Classic syntax
			StringAssert::AreEqualIgnoringCase("hello world!",phrase);
            
			// Helper syntax
			Assert::That(phrase, Is::EqualTo("hello world!")->IgnoreCase);
			//Only available using new syntax
			Assert::That(phrase, Is::Not->EqualTo("goodbye world!")->IgnoreCase);
			Assert::That(gcnew array<String^> { "Hello", "World" }, 
				Is::EqualTo(gcnew array<Object^> { "HELLO", "WORLD" })->IgnoreCase);
			Assert::That(gcnew array<String^> {"HELLO", "Hello", "hello" },
				Is::All->EqualTo( "hello" )->IgnoreCase);
		            
			// Inherited syntax
			Expect(phrase, EqualTo("hello world!")->IgnoreCase);
			//Only available using new syntax
			Expect(phrase, Not->EqualTo("goodbye world!")->IgnoreCase);
			Expect(gcnew array<String^> { "Hello", "World" }, 
				EqualTo(gcnew array<Object^> { "HELLO", "WORLD" })->IgnoreCase);
			Expect(gcnew array<String^> {"HELLO", "Hello", "hello" },
				All->EqualTo( "hello" )->IgnoreCase);
		}

		[Test]
		void RegularExpressionTests()
		{
			String^ phrase = "Now is the time for all good men to come to the aid of their country.";
			array<String^>^ quotes = { "Never say never", "It's never too late", "Nevermore!" };

			// Classic syntax
			StringAssert::IsMatch( "all good men", phrase );
			StringAssert::IsMatch( "Now.*come", phrase );

			// Helper syntax
			Assert::That( phrase, Text::Matches( "all good men" ) );
			Assert::That( phrase, Text::Matches( "Now.*come" ) );
			// Only available using new syntax
			Assert::That(phrase, Text::DoesNotMatch("all.*men.*good"));
			Assert::That(phrase, Text::Matches("ALL")->IgnoreCase);
			Assert::That(quotes, Text::All->Matches("never")->IgnoreCase);
		
			// Inherited syntax
			Expect( phrase, Matches( "all good men" ) );
			Expect( phrase, Matches( "Now.*come" ) );
			// Only available using new syntax
			Expect(phrase, Not->Matches("all.*men.*good"));
			Expect(phrase, Matches("ALL")->IgnoreCase);
			Expect(quotes, All->Matches("never")->IgnoreCase);
		}

		[Test]
		void EqualityTests()
		{
			array<int>^ i3 = { 1, 2, 3 };
			array<double>^ d3 = { 1.0, 2.0, 3.0 };
			array<int>^ iunequal = { 1, 3, 2 };

			// Classic Syntax
			Assert::AreEqual(4, 2 + 2);
			Assert::AreEqual(i3, d3);
			Assert::AreNotEqual(5, 2 + 2);
			Assert::AreNotEqual(i3, iunequal);

			// Helper syntax
			Assert::That(2 + 2, Is::EqualTo(4));
			Assert::That(2 + 2 == 4);
			Assert::That(i3, Is::EqualTo(d3));
			Assert::That(2 + 2, Is::Not->EqualTo(5));
			Assert::That(i3, Is::Not->EqualTo(iunequal));
		
			// Inherited syntax
			Expect(2 + 2, EqualTo(4));
			Expect(2 + 2 == 4);
			Expect(i3, EqualTo(d3));
			Expect(2 + 2, Not->EqualTo(5));
			Expect(i3, Not->EqualTo(iunequal));
		}

		[Test]
		void EqualityTestsWithTolerance()
		{
			// CLassic syntax
			Assert::AreEqual(5.0, 4.99, 0.05);
			Assert::AreEqual(5.0F, 4.99F, 0.05F);

			// Helper syntax
			Assert::That(4.99L, Is::EqualTo(5.0L)->Within(0.05L));
			Assert::That(4.99f, Is::EqualTo(5.0f)->Within(0.05f));
		
			// Inherited syntax
			Expect(4.99L, EqualTo(5.0L)->Within(0.05L));
			Expect(4.99f, EqualTo(5.0f)->Within(0.05f));
		}

		[Test]
		void ComparisonTests()
		{
			// Classic Syntax
			Assert::Greater(7, 3);
			Assert::GreaterOrEqual(7, 3);
			Assert::GreaterOrEqual(7, 7);

			// Helper syntax
			Assert::That(7, Is::GreaterThan(3));
			Assert::That(7, Is::GreaterThanOrEqualTo(3));
			Assert::That(7, Is::AtLeast(3));
			Assert::That(7, Is::GreaterThanOrEqualTo(7));
			Assert::That(7, Is::AtLeast(7));

			// Inherited syntax
			Expect(7, GreaterThan(3));
			Expect(7, GreaterThanOrEqualTo(3));
			Expect(7, AtLeast(3));
			Expect(7, GreaterThanOrEqualTo(7));
			Expect(7, AtLeast(7));

			// Classic syntax
			Assert::Less(3, 7);
			Assert::LessOrEqual(3, 7);
			Assert::LessOrEqual(3, 3);

			// Helper syntax
			Assert::That(3, Is::LessThan(7));
			Assert::That(3, Is::LessThanOrEqualTo(7));
			Assert::That(3, Is::AtMost(7));
			Assert::That(3, Is::LessThanOrEqualTo(3));
			Assert::That(3, Is::AtMost(3));
		
			// Inherited syntax
			Expect(3, LessThan(7));
			Expect(3, LessThanOrEqualTo(7));
			Expect(3, AtMost(7));
			Expect(3, LessThanOrEqualTo(3));
			Expect(3, AtMost(3));
		}

		[Test]
		void AllItemsTests()
		{
			array<Object^>^ ints = { 1, 2, 3, 4 };
			array<Object^>^ strings = { "abc", "bad", "cab", "bad", "dad" };

			// Classic syntax
			CollectionAssert::AllItemsAreNotNull(ints);
			CollectionAssert::AllItemsAreInstancesOfType(ints, int::typeid);
			CollectionAssert::AllItemsAreInstancesOfType(strings, String::typeid);
			CollectionAssert::AllItemsAreUnique(ints);

			// Helper syntax
			Assert::That(ints, Is::All->Not->Null);
			Assert::That(ints, Is::All->InstanceOfType(int::typeid));
			Assert::That(strings, Is::All->InstanceOfType(String::typeid));
			Assert::That(ints, Is::Unique);
			// Only available using new syntax
			Assert::That(strings, Is::Not->Unique);
			Assert::That(ints, Is::All->GreaterThan(0));
			Assert::That(strings, Text::All->Contains( "a" ) );
			Assert::That(strings, Has::Some->StartsWith( "ba" ) );
		
			// Inherited syntax
			Expect(ints, All->Not->Null);
			Expect(ints, All->InstanceOfType(int::typeid));
			Expect(strings, All->InstanceOfType(String::typeid));
			Expect(ints, Unique);
			// Only available using new syntax
			Expect(strings, Not->Unique);
			Expect(ints, All->GreaterThan(0));
			Expect(strings, All->Contains( "a" ) );
			Expect(strings, Some->StartsWith( "ba" ) );
		}

		[Test]
		void SomeItemsTests()
		{
			array<Object^>^ mixed = { 1, 2, "3", nullptr, "four", 100 };
			array<Object^>^ strings = { "abc", "bad", "cab", "bad", "dad" };

			// Not available using the classic syntax

			// Helper syntax
			Assert::That(mixed, Has::Some->Null);
			Assert::That(mixed, Has::Some->InstanceOfType(int::typeid));
			Assert::That(mixed, Has::Some->InstanceOfType(String::typeid));
			Assert::That(strings, Has::Some->StartsWith( "ba" ) );
			Assert::That(strings, Has::Some->Not->StartsWith( "ba" ) );
		
			// Inherited syntax
			Expect(mixed, Some->Null);
			Expect(mixed, Some->InstanceOfType(int::typeid));
			Expect(mixed, Some->InstanceOfType(String::typeid));
			Expect(strings, Some->StartsWith( "ba" ) );
			Expect(strings, Some->Not->StartsWith( "ba" ) );
		}

		[Test]
		void NoItemsTests()
		{
			array<Object^>^ ints = { 1, 2, 3, 4, 5 };
			array<Object^>^ strings = { "abc", "bad", "cab", "bad", "dad" };

			// Not available using the classic syntax

			// Helper syntax
			Assert::That(ints, Has::None->Null);
			Assert::That(ints, Has::None->InstanceOfType(String::typeid));
			Assert::That(ints, Has::None->GreaterThan(99));
			Assert::That(strings, Has::None->StartsWith( "qu" ) );
		
			// Inherited syntax
			Expect(ints, None->Null);
			Expect(ints, None->InstanceOfType(String::typeid));
			Expect(ints, None->GreaterThan(99));
			Expect(strings, None->StartsWith( "qu" ) );
		}

		[Test]
		void CollectionContainsTests()
		{
			array<int>^ iarray = { 1, 2, 3 };
			array<String^>^ sarray = { "a", "b", "c" };

			// Classic syntax
			Assert::Contains(3, iarray);
			Assert::Contains("b", sarray);
			CollectionAssert::Contains(iarray, 3);
			CollectionAssert::Contains(sarray, "b");
			CollectionAssert::DoesNotContain(sarray, "x");

			// Helper syntax
			Assert::That(iarray, Has::Member(3));
			Assert::That(sarray, Has::Member("b"));
			Assert::That(sarray, Has::No->Member("x")); // Yuck!
			Assert::That(sarray, !Has::Member("x"));
		
			// Inherited syntax
			Expect(iarray, Contains(3));
			Expect(sarray, Contains("b"));
			Expect(sarray, Not->Contains("x"));
			Expect(sarray, !Contains("x"));
		}

		[Test]
		void CollectionEquivalenceTests()
		{
			array<int>^ ints1to5 = { 1, 2, 3, 4, 5 };

			// Classic syntax
			CollectionAssert::AreEquivalent(gcnew array<int> { 2, 1, 4, 3, 5 }, ints1to5);
			CollectionAssert::AreNotEquivalent(gcnew array<int> { 2, 2, 4, 3, 5 }, ints1to5);
			CollectionAssert::AreNotEquivalent(gcnew array<int> { 2, 4, 3, 5 }, ints1to5);
			CollectionAssert::AreNotEquivalent(gcnew array<int> { 2, 2, 1, 1, 4, 3, 5 }, ints1to5);
		
			// Helper syntax
			Assert::That(gcnew array<int> { 2, 1, 4, 3, 5 }, Is::EquivalentTo(ints1to5));
			Assert::That(gcnew array<int> { 2, 2, 4, 3, 5 }, Is::Not->EquivalentTo(ints1to5));
			Assert::That(gcnew array<int> { 2, 4, 3, 5 }, Is::Not->EquivalentTo(ints1to5));
			Assert::That(gcnew array<int> { 2, 2, 1, 1, 4, 3, 5 }, Is::Not->EquivalentTo(ints1to5));

			// Inherited syntax
			Expect(gcnew array<int> { 2, 1, 4, 3, 5 }, EquivalentTo(ints1to5));
			Expect(gcnew array<int> { 2, 2, 4, 3, 5 }, Not->EquivalentTo(ints1to5));
			Expect(gcnew array<int> { 2, 4, 3, 5 }, Not->EquivalentTo(ints1to5));
			Expect(gcnew array<int> { 2, 2, 1, 1, 4, 3, 5 }, Not->EquivalentTo(ints1to5));
		}

		[Test]
		void SubsetTests()
		{
			array<int>^ ints1to5 = { 1, 2, 3, 4, 5 };

			// Classic syntax
			CollectionAssert::IsSubsetOf(gcnew array<int> { 1, 3, 5 }, ints1to5);
			CollectionAssert::IsSubsetOf(gcnew array<int> { 1, 2, 3, 4, 5 }, ints1to5);
			CollectionAssert::IsNotSubsetOf(gcnew array<int> { 2, 4, 6 }, ints1to5);
			CollectionAssert::IsNotSubsetOf(gcnew array<int> { 1, 2, 2, 2, 5 }, ints1to5);

			// Helper syntax
			Assert::That(gcnew array<int> { 1, 3, 5 }, Is::SubsetOf(ints1to5));
			Assert::That(gcnew array<int> { 1, 2, 3, 4, 5 }, Is::SubsetOf(ints1to5));
			Assert::That(gcnew array<int> { 2, 4, 6 }, Is::Not->SubsetOf(ints1to5));
			Assert::That(gcnew array<int> { 1, 2, 2, 2, 5 }, Is::Not->SubsetOf(ints1to5));
		
			// Inherited syntax
			Expect(gcnew array<int> { 1, 3, 5 }, SubsetOf(ints1to5));
			Expect(gcnew array<int> { 1, 2, 3, 4, 5 }, SubsetOf(ints1to5));
			Expect(gcnew array<int> { 2, 4, 6 }, Not->SubsetOf(ints1to5));
			Expect(gcnew array<int> { 1, 2, 2, 2, 5 }, Not->SubsetOf(ints1to5));
		}

		[Test]
		void PropertyTests()
		{
			array<String^>^ strings = { "abc", "bca", "xyz" };

			// Helper syntax
			Assert::That( "Hello", Has::Property("Length")->EqualTo(5) );
			Assert::That( "Hello", Has::Length->EqualTo( 5 ) );
			Assert::That( strings , Has::All->Property( "Length")->EqualTo(3) );
			Assert::That( strings, Has::All->Length->EqualTo( 3 ) );

			// Inherited syntax
			Expect( "Hello", Property("Length")->EqualTo(5) );
			Expect( "Hello", Length->EqualTo( 5 ) );
			Expect( strings, All->Property("Length")->EqualTo(3) );
			Expect( strings, All->Length->EqualTo( 3 ) );
		}

		[Test]
		void NotTests()
		{
			// Not available using the classic syntax

			// Helper syntax
			Assert::That(42, Is::Not->Null);
			Assert::That(42, Is::Not->True);
			Assert::That(42, Is::Not->False);
			Assert::That(2.5, Is::Not->NaN);
			Assert::That(2 + 2, Is::Not->EqualTo(3));
			Assert::That(2 + 2, Is::Not->Not->EqualTo(4));
			Assert::That(2 + 2, Is::Not->Not->Not->EqualTo(5));

			// Inherited syntax
			Expect(42, Not->Null);
			Expect(42, Not->True);
			Expect(42, Not->False);
			Expect(2.5, Not->NaN);
			Expect(2 + 2, Not->EqualTo(3));
			Expect(2 + 2, Not->Not->EqualTo(4));
			Expect(2 + 2, Not->Not->Not->EqualTo(5));
		}

		[Test]
		void NotOperator()
		{
			// The ! operator is only available in the new syntax
			Assert::That(42, !Is::Null);
			// Inherited syntax
			Expect( 42, !Null );
		}

		[Test]
		void AndOperator()
		{
			// The & operator is only available in the new syntax
			Assert::That(7, Is::GreaterThan(5) & Is::LessThan(10));
			// Inherited syntax
			Expect( 7, GreaterThan(5) & LessThan(10));
		}

		[Test]
		void OrOperator()
		{
			// The | operator is only available in the new syntax
			Assert::That(3, Is::LessThan(5) | Is::GreaterThan(10));
			Expect( 3, LessThan(5) | GreaterThan(10));
		}

		[Test]
		void ComplexTests()
		{
			Assert::That(7, Is::Not->Null & Is::Not->LessThan(5) & Is::Not->GreaterThan(10));
			Expect(7, Not->Null & Not->LessThan(5) & Not->GreaterThan(10));

			Assert::That(7, !Is::Null & !Is::LessThan(5) & !Is::GreaterThan(10));
			Expect(7, !Null & !LessThan(5) & !GreaterThan(10));
		}

		// This method contains assertions that should not compile
		// You can check by uncommenting it.
		//void WillNotCompile()
		//{
		//    Assert::That(42, Is::Not);
		//    Assert::That(42, Is::All);
		//    Assert::That(42, Is::Null->Not);
		//    Assert::That(42, Is::Not->Null->GreaterThan(10));
		//    Assert::That(42, Is::GreaterThan(10)->LessThan(99));

		//    object[] c = new object[0];
		//    Assert::That(c, Is::Null->All);
		//    Assert::That(c, Is::Not->All);
		//    Assert::That(c, Is::All->Not);
		//}
	};
}