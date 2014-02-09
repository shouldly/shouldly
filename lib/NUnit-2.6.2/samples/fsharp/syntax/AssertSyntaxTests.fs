// ****************************************************************
// Copyright 2012, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

module NUnit.Samples.AssertSyntaxTests

open System
open System.Collections
open NUnit.Framework
open NUnit.Framework.Constraints

/// <summary>
/// This test fixture attempts to exercise all the syntactic
/// variations of Assert without getting into failures, errors 
/// or corner cases. Thus, some of the tests may be duplicated 
/// in other fixtures.
/// 
/// Each test performs the same operations using the classic
/// syntax (if available) and the constraint syntax. The
/// inherited syntax is not used in this example, since it
/// would require using a class to hold the tests, which
/// seems to make it less useful in F#.
/// </summary>

[<Test>]
let IsNull() =
    let nada : obj = null
    Assert.IsNull(nada)
    Assert.That(nada, Is.Null)

[<Test>]
let IsNotNull() =
    Assert.IsNotNull(42)
    Assert.That(42, Is.Not.Null)

[<Test>]
let IsTrue() =
    Assert.IsTrue(2+2=4)
    Assert.True(2+2=4)
    Assert.That(2+2=4, Is.True)
    Assert.That(2+2=4)

[<Test>]
let IsFalse() =
    Assert.IsFalse(2+2=5)
    Assert.That(2+2=5, Is.False)

[<Test>]
let IsNaN() =
    let d : double = Double.NaN
    let f : float = Double.NaN
    Assert.IsNaN(d)
    Assert.IsNaN(f)
    Assert.That(d, Is.NaN)
    Assert.That(f, Is.NaN)

[<Test>]
let EmptyStringTests() =
    Assert.IsEmpty("")
    Assert.IsNotEmpty("Hello!")
    Assert.That("", Is.Empty)
    Assert.That("Hello!", Is.Not.Empty)

[<Test>]
let EmptyCollectionTests() =
    // Lists
    Assert.IsEmpty([])
    Assert.IsNotEmpty([ 1; 2; 3 ])
    Assert.That([], Is.Empty)
    Assert.That([ 1; 2; 3 ], Is.Not.Empty)
    //Arrays
    Assert.IsEmpty([||])
    Assert.IsNotEmpty([| 1; 2; 3 |])
    Assert.That([||], Is.Empty)
    Assert.That([| 1; 2; 3 |], Is.Not.Empty)

[<Test>]
let ExactTypeTests() =
    Assert.AreEqual(typeof<string>, "Hello".GetType())
    Assert.AreEqual("System.String", "Hello".GetType().FullName)
    Assert.AreNotEqual(typeof<int>, "Hello".GetType())
    Assert.AreNotEqual("System.Int32", "Hello".GetType().FullName)
    Assert.That("Hello", Is.TypeOf<string>())
    Assert.That("Hello", Is.Not.TypeOf<int>())

[<Test>]
let InstanceOfTypeTests() =
    Assert.IsInstanceOf(typeof<string>, "Hello")
    Assert.IsNotInstanceOf(typeof<string>, 5)
    Assert.That("Hello", Is.InstanceOf(typeof<string>))
    Assert.That(5, Is.Not.InstanceOf(typeof<string>))

[<Test>]
let AssignableFromTypeTests() =
    Assert.IsAssignableFrom(typeof<string>, "Hello")
    Assert.IsNotAssignableFrom(typeof<string>, 5)
    Assert.That( "Hello", Is.AssignableFrom(typeof<string>))
    Assert.That( 5, Is.Not.AssignableFrom(typeof<string>))

[<Test>]
let SubstringTests() =
    let phrase = "Hello World!"
    let array = [| "abc"; "bad"; "dba" |]
    StringAssert.Contains("World", phrase)
    Assert.That(phrase, Contains.Substring("World"))
    Assert.That(phrase, Is.Not.StringContaining("goodbye"))
    Assert.That(phrase, Contains.Substring("WORLD").IgnoreCase)
    Assert.That(phrase, Is.Not.StringContaining("BYE").IgnoreCase)
    Assert.That(array, Has.All.StringContaining( "b" ) )

[<Test>]
let StartsWithTests() =
    let phrase = "Hello World!"
    let greetings = [| "Hello!"; "Hi!"; "Hola!" |]
    StringAssert.StartsWith("Hello", phrase);
    Assert.That(phrase, Is.StringStarting("Hello"))
    Assert.That(phrase, Is.Not.StringStarting("Hi!"))
    Assert.That(phrase, Is.StringStarting("HeLLo").IgnoreCase)
    Assert.That(phrase, Is.Not.StringStarting("HI").IgnoreCase)
    Assert.That(greetings, Is.All.StringStarting("h").IgnoreCase)

[<Test>]
let EndsWithTests() =
    let phrase = "Hello World!"
    let greetings = [| "Hello!"; "Hi!"; "Hola!" |];
    StringAssert.EndsWith("!", phrase)
    Assert.That(phrase, Is.StringEnding("!"))
    Assert.That(phrase, Is.Not.StringEnding("?"))
    Assert.That(phrase, Is.StringEnding("WORLD!").IgnoreCase)
    Assert.That(greetings, Is.All.StringEnding("!"))

[<Test>]
let EqualIgnoringCaseTests() =
    let phrase = "Hello World!"
    StringAssert.AreEqualIgnoringCase("hello world!",phrase)
    Assert.That(phrase, Is.EqualTo("hello world!").IgnoreCase)
    Assert.That(phrase, Is.Not.EqualTo("goodbye world!").IgnoreCase)
    Assert.That( [| "Hello"; "World" |], 
        Is.EqualTo( [| "HELLO"; "WORLD" |] ).IgnoreCase)
    Assert.That( [| "HELLO"; "Hello"; "hello" |],
        Is.All.EqualTo( "hello" ).IgnoreCase)
          
[<Test>]
let RegularExpressionTests() =
    let phrase = "Now is the time for all good men to come to the aid of their country."
    let quotes = [| "Never say never"; "It's never too late"; "Nevermore!" |]
    StringAssert.IsMatch( "all good men", phrase )
    StringAssert.IsMatch( "Now.*come", phrase )
    Assert.That( phrase, Is.StringMatching( "all good men" ) )
    Assert.That( phrase, Is.StringMatching( "Now.*come" ) )
    Assert.That( phrase, Is.Not.StringMatching("all.*men.*good") )
    Assert.That( phrase, Is.StringMatching("ALL").IgnoreCase )
    Assert.That( quotes, Is.All.StringMatching("never").IgnoreCase )

[<Test>]
let EqualityTests() =
    let i3 = [| 1; 2; 3 |]
    let d3 = [| 1.0; 2.0; 3.0 |]
    let iunequal = [| 1; 3; 2 |]
    Assert.AreEqual(4, 2 + 2)
    Assert.AreEqual(i3, d3)
    Assert.AreNotEqual(5, 2 + 2)
    Assert.AreNotEqual(i3, iunequal)
    Assert.That(2 + 2, Is.EqualTo(4))
    Assert.That(2 + 2 = 4)
    Assert.That(i3, Is.EqualTo(d3))
    Assert.That(2 + 2, Is.Not.EqualTo(5))
    Assert.That(i3, Is.Not.EqualTo(iunequal))

[<Test>]
let EqualityTestsWithTolerance() =
    Assert.AreEqual(5.0, 4.99, 0.05)
    Assert.That(4.99, Is.EqualTo(5.0).Within(0.05))
    Assert.That(4.0, Is.Not.EqualTo(5.0).Within(0.5))
    Assert.That(4.99f, Is.EqualTo(5.0f).Within(0.05f))
    Assert.That(4.99m, Is.EqualTo(5.0m).Within(0.05m))
    Assert.That(3999999999u, Is.EqualTo(4000000000u).Within(5u))
    Assert.That(499, Is.EqualTo(500).Within(5))
    Assert.That(4999999999L, Is.EqualTo(5000000000L).Within(5L))
    Assert.That(5999999999UL, Is.EqualTo(6000000000UL).Within(5UL))

[<Test>]
let EqualityTestsWithTolerance_MixedFloatAndDouble() =
    // Bug Fix 1743844
    Assert.That(2.20492, Is.EqualTo(2.2).Within(0.01f),
        "Double actual, Double expected, Single tolerance")
    Assert.That(2.20492, Is.EqualTo(2.2f).Within(0.01),
        "Double actual, Single expected, Double tolerance" )
    Assert.That(2.20492, Is.EqualTo(2.2f).Within(0.01f),
        "Double actual, Single expected, Single tolerance" )
    Assert.That(2.20492f, Is.EqualTo(2.2f).Within(0.01),
        "Single actual, Single expected, Double tolerance")
    Assert.That(2.20492f, Is.EqualTo(2.2).Within(0.01),
        "Single actual, Double expected, Double tolerance")
    Assert.That(2.20492f, Is.EqualTo(2.2).Within(0.01f),
        "Single actual, Double expected, Single tolerance")

[<Test>]
let EqualityTestsWithTolerance_MixingTypesGenerally() =
    Assert.That(202.0, Is.EqualTo(200.0).Within(2),
        "Double actual, Double expected, int tolerance")
    Assert.That( 4.87m, Is.EqualTo(5).Within(0.25),
        "Decimal actual, int expected, Double tolerance" )
    Assert.That( 4.87m, Is.EqualTo(5ul).Within(1),
        "Decimal actual, ulong expected, int tolerance" )
    Assert.That( 487, Is.EqualTo(500).Within(25),
        "int actual, int expected, int tolerance" )
    Assert.That( 487u, Is.EqualTo(500).Within(25),
        "uint actual, int expected, int tolerance" )
    Assert.That( 487L, Is.EqualTo(500).Within(25),
        "long actual, int expected, int tolerance" )
    Assert.That( 487ul, Is.EqualTo(500).Within(25),
        "ulong actual, int expected, int tolerance" )

[<Test>]
let ComparisonTests() =
    Assert.Greater(7, 3)
    Assert.GreaterOrEqual(7, 3)
    Assert.GreaterOrEqual(7, 7)
    Assert.That(7, Is.GreaterThan(3))
    Assert.That(7, Is.GreaterThanOrEqualTo(3))
    Assert.That(7, Is.AtLeast(3))
    Assert.That(7, Is.GreaterThanOrEqualTo(7))
    Assert.That(7, Is.AtLeast(7))

    Assert.Less(3, 7)
    Assert.LessOrEqual(3, 7)
    Assert.LessOrEqual(3, 3)
    Assert.That(3, Is.LessThan(7))
    Assert.That(3, Is.LessThanOrEqualTo(7))
    Assert.That(3, Is.AtMost(7))
    Assert.That(3, Is.LessThanOrEqualTo(3))
    Assert.That(3, Is.AtMost(3))

[<Test>]
let AllItemsTests() =
    let ints = [| 1; 2; 3; 4 |]
    let doubles = [| 0.99; 2.1; 3.0; 4.05 |]
    let strings = [| "abc"; "bad"; "cab"; "bad"; "dad" |]
    CollectionAssert.AllItemsAreNotNull(ints)
    CollectionAssert.AllItemsAreInstancesOfType(ints, typeof<int>)
    CollectionAssert.AllItemsAreInstancesOfType(strings, typeof<string>)
    CollectionAssert.AllItemsAreUnique(ints)
    Assert.That(ints, Is.All.Not.Null)
    Assert.That(ints, Has.None.Null)
    Assert.That(ints, Is.All.InstanceOf(typeof<int>))
    Assert.That(ints, Has.All.InstanceOf(typeof<int>))
    Assert.That(strings, Is.All.InstanceOf(typeof<string>))
    Assert.That(strings, Has.All.InstanceOf(typeof<string>))
    Assert.That(ints, Is.Unique)
    Assert.That(strings, Is.Not.Unique)
    Assert.That(ints, Is.All.GreaterThan(0))
    Assert.That(ints, Has.All.GreaterThan(0));
    Assert.That(ints, Has.None.LessThanOrEqualTo(0))
    Assert.That(strings, Is.All.StringContaining( "a" ) )
    Assert.That(strings, Has.All.Contains( "a" ) )
    Assert.That(strings, Has.Some.StartsWith( "ba" ) )
    Assert.That( strings, Has.Some.Property( "Length" ).EqualTo( 3 ) )
    Assert.That( strings, Has.Some.StartsWith( "BA" ).IgnoreCase )
    Assert.That( doubles, Has.Some.EqualTo( 1.0 ).Within( 0.05 ) )

[<Test>]
let SomeItemTests() =
    let mixed = [| 1; 2; "3"; null; "four"; 100 |]: obj array
    let strings = [| "abc"; "bad"; "cab"; "bad"; "dad" |]
    Assert.That(mixed, Has.Some.Null)
    Assert.That(mixed, Has.Some.InstanceOf<int>())
    Assert.That(mixed, Has.Some.InstanceOf<string>())
    Assert.That(strings, Has.Some.StartsWith( "ba" ) )
    Assert.That(strings, Has.Some.Not.StartsWith( "ba" ) )

[<Test>]
let NoItemTests() =
    let ints = [| 1; 2; 3; 4; 5 |]
    let strings = [| "abc"; "bad"; "cab"; "bad"; "dad" |]
    Assert.That(ints, Has.None.Null)
    Assert.That(ints, Has.None.InstanceOf<string>());
    Assert.That(ints, Has.None.GreaterThan(99));
    Assert.That(strings, Has.None.StartsWith( "qu" ) );

[<Test>]
let CollectionContainsTests() =
    let iarray = [| 1; 2; 3 |]
    let sarray = [| "a"; "b"; "c" |]

    Assert.Contains(3, iarray)
    Assert.Contains("b", sarray)
    CollectionAssert.Contains(iarray, 3)
    CollectionAssert.Contains(sarray, "b")
    CollectionAssert.DoesNotContain(sarray, "x")
    // Showing that Contains uses NUnit equality
    CollectionAssert.Contains( iarray, 1.0 )

    Assert.That(iarray, Has.Member(3))
    Assert.That(sarray, Has.Member("b"))
    Assert.That(sarray, Has.No.Member("x"))
    // Showing that Contains uses NUnit equality
    Assert.That(iarray, Has.Member( 1.0 ))

    // Only available using the new syntax
    // Note that EqualTo and SameAs do NOT give
    // identical results to Contains because 
    // Contains uses Object.Equals()
    Assert.That(iarray, Has.Some.EqualTo(3))
    Assert.That(iarray, Has.Member(3))
    Assert.That(sarray, Has.Some.EqualTo("b"))
    Assert.That(sarray, Has.None.EqualTo("x"))
    Assert.That(iarray, Has.None.SameAs( 1.0 ))
    Assert.That(iarray, Has.All.LessThan(10))
    Assert.That(sarray, Has.All.Length.EqualTo(1))
    Assert.That(sarray, Has.None.Property("Length").GreaterThan(3))

[<Test>]
let CollectionEquivalenceTests() =
    let ints1to5 = [| 1; 2; 3; 4; 5 |]
    let twothrees = [| 1; 2; 3; 3; 4; 5 |]
    let twofours = [| 1; 2; 3; 4; 4; 5 |]

    CollectionAssert.AreEquivalent( [| 2; 1; 4; 3; 5 |], ints1to5)
    CollectionAssert.AreNotEquivalent( [| 2; 2; 4; 3; 5 |], ints1to5)
    CollectionAssert.AreNotEquivalent( [| 2; 4; 3; 5 |], ints1to5)
    CollectionAssert.AreNotEquivalent( [| 2; 2; 1; 1; 4; 3; 5 |], ints1to5)
    CollectionAssert.AreNotEquivalent(twothrees, twofours)

    Assert.That( [| 2; 1; 4; 3; 5 |], Is.EquivalentTo(ints1to5))
    Assert.That( [| 2; 2; 4; 3; 5 |], Is.Not.EquivalentTo(ints1to5))
    Assert.That( [| 2; 4; 3; 5 |], Is.Not.EquivalentTo(ints1to5))
    Assert.That( [| 2; 2; 1; 1; 4; 3; 5 |], Is.Not.EquivalentTo(ints1to5))

[<Test>]
let SubsetTests() =
    let ints1to5 = [| 1; 2; 3; 4; 5 |]

    CollectionAssert.IsSubsetOf( [| 1; 3; 5 |], ints1to5)
    CollectionAssert.IsSubsetOf( [| 1; 2; 3; 4; 5 |], ints1to5)
    CollectionAssert.IsNotSubsetOf( [| 2; 4; 6 |], ints1to5)
    CollectionAssert.IsNotSubsetOf( [| 1; 2; 2; 2; 5 |], ints1to5)

    Assert.That( [| 1; 3; 5 |], Is.SubsetOf(ints1to5))
    Assert.That( [| 1; 2; 3; 4; 5 |], Is.SubsetOf(ints1to5))
    Assert.That( [| 2; 4; 6 |], Is.Not.SubsetOf(ints1to5))

[<Test>]
let PropertyTests() =
    let array = [| "abc"; "bca"; "xyz"; "qrs" |]
    let array2 = [| "a"; "ab"; "abc" |]
    let list = new System.Collections.ArrayList( array )

    Assert.That( list, Has.Property( "Count" ) )
    Assert.That( list, Has.No.Property( "Length" ) )

    Assert.That( "Hello", Has.Length.EqualTo( 5 ) )
    Assert.That( "Hello", Has.Length.LessThan( 10 ) )
    Assert.That( "Hello", Has.Property("Length").EqualTo(5) )
    Assert.That( "Hello", Has.Property("Length").GreaterThan(3) )

    Assert.That( array, Has.Property( "Length" ).EqualTo( 4 ) )
    Assert.That( array, Has.Length.EqualTo( 4 ) )
    Assert.That( array, Has.Property( "Length" ).LessThan( 10 ) )

    Assert.That( array, Has.All.Property("Length").EqualTo(3) )
    Assert.That( array, Has.All.Length.EqualTo( 3 ) )
    Assert.That( array, Is.All.Length.EqualTo( 3 ) )
    Assert.That( array, Has.All.Property("Length").EqualTo(3) )
    Assert.That( array, Is.All.Property("Length").EqualTo(3) )

    Assert.That( array2, Has.Some.Property("Length").EqualTo(2) )
    Assert.That( array2, Has.Some.Length.EqualTo(2) )
    Assert.That( array2, Has.Some.Property("Length").GreaterThan(2) )

    Assert.That( array2, Is.Not.Property("Length").EqualTo(4) )
    Assert.That( array2, Is.Not.Length.EqualTo( 4 ) )
    Assert.That( array2, Has.No.Property("Length").GreaterThan(3) )

    Assert.That( List.Map( array2 ).Property("Length"), Is.EqualTo( [| 1; 2; 3 |] ) )
    Assert.That( List.Map( array2 ).Property("Length"), Is.EquivalentTo( [| 3; 2; 1 |] ) )
    Assert.That( List.Map( array2 ).Property("Length"), Is.SubsetOf( [| 1; 2; 3; 4; 5 |] ) )
    Assert.That( List.Map( array2 ).Property("Length"), Is.Unique )

    Assert.That( list, Has.Count.EqualTo( 4 ) )

[<Test>]
let NotTests() =
    Assert.That(42, Is.Not.Null)
    Assert.That(42, Is.Not.True)
    Assert.That(42, Is.Not.False)
    Assert.That(2.5, Is.Not.NaN)
    Assert.That(2 + 2, Is.Not.EqualTo(3))
    Assert.That(2 + 2, Is.Not.Not.EqualTo(4))
    Assert.That(2 + 2, Is.Not.Not.Not.EqualTo(5))
