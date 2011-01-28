' ****************************************************************
' Copyright 2007, Charlie Poole
' This is free software licensed under the NUnit license. You may
' obtain a copy of the license at http:'nunit.org/?p=license&r=2.4
' ****************************************************************

Option Explicit On 

Imports System
Imports NUnit.Framework
Imports NUnit.Framework.Constraints
Imports Text = NUnit.Framework.Text

Namespace NUnit.Samples

    ' This test fixture attempts to exercise all the syntactic
    ' variations of Assert without getting into failures, errors 
    ' or corner cases. Thus, some of the tests may be duplicated 
    ' in other fixtures.
    ' 
    ' Each test performs the same operations using the classic
    ' syntax (if available) and the new syntax in both the
    ' helper-based and inherited forms.
    ' 
    ' This Fixture will eventually be duplicated in other
    ' supported languages. 

    <TestFixture()> _
    Public Class AssertSyntaxTests
        Inherits AssertionHelper

#Region "Simple Constraint Tests"
        <Test()> _
        Public Sub IsNull()
            Dim nada As Object = Nothing

            ' Classic syntax
            Assert.IsNull(nada)

            ' Helper syntax
            Assert.That(nada, Iz.Null)

            ' Inherited syntax
            Expect(nada, Null)
        End Sub


        <Test()> _
        Public Sub IsNotNull()
            ' Classic syntax
            Assert.IsNotNull(42)

            ' Helper syntax
            Assert.That(42, Iz.Not.Null)

            ' Inherited syntax
            Expect(42, Iz.Not.Null)
        End Sub

        <Test()> _
        Public Sub IsTrue()
            ' Classic syntax
            Assert.IsTrue(2 + 2 = 4)

            ' Helper syntax
            Assert.That(2 + 2 = 4, Iz.True)
            Assert.That(2 + 2 = 4)

            ' Inherited syntax
            Expect(2 + 2 = 4, Iz.True)
            Expect(2 + 2 = 4)
        End Sub

        <Test()> _
        Public Sub IsFalse()
            ' Classic syntax
            Assert.IsFalse(2 + 2 = 5)

            ' Helper syntax
            Assert.That(2 + 2 = 5, Iz.False)

            ' Inherited syntax
            Expect(2 + 2 = 5, Iz.False)
        End Sub

        <Test()> _
        Public Sub IsNaN()
            Dim d As Double = Double.NaN
            Dim f As Single = Single.NaN

            ' Classic syntax
            Assert.IsNaN(d)
            Assert.IsNaN(f)

            ' Helper syntax
            Assert.That(d, Iz.NaN)
            Assert.That(f, Iz.NaN)

            ' Inherited syntax
            Expect(d, NaN)
            Expect(f, NaN)
        End Sub

        <Test()> _
        Public Sub EmptyStringTests()
            ' Classic syntax
            Assert.IsEmpty("")
            Assert.IsNotEmpty("Hello!")

            ' Helper syntax
            Assert.That("", Iz.Empty)
            Assert.That("Hello!", Iz.Not.Empty)

            ' Inherited syntax
            Expect("", Empty)
            Expect("Hello!", Iz.Not.Empty)
        End Sub

        <Test()> _
        Public Sub EmptyCollectionTests()

            Dim boolArray As Boolean() = New Boolean() {}
            Dim nonEmpty As Integer() = New Integer() {1, 2, 3}

            ' Classic syntax
            Assert.IsEmpty(boolArray)
            Assert.IsNotEmpty(nonEmpty)

            ' Helper syntax
            Assert.That(boolArray, Iz.Empty)
            Assert.That(nonEmpty, Iz.Not.Empty)

            ' Inherited syntax
            Expect(boolArray, Iz.Empty)
            Expect(nonEmpty, Iz.Not.Empty)
        End Sub
#End Region

#Region "TypeConstraint Tests"
        <Test()> _
        Public Sub ExactTypeTests()
            ' Classic syntax workarounds
            Assert.AreEqual(GetType(String), "Hello".GetType())
            Assert.AreEqual("System.String", "Hello".GetType().FullName)
            Assert.AreNotEqual(GetType(Integer), "Hello".GetType())
            Assert.AreNotEqual("System.Int32", "Hello".GetType().FullName)

            ' Helper syntax
            Assert.That("Hello", Iz.TypeOf(GetType(String)))
            Assert.That("Hello", Iz.Not.TypeOf(GetType(Integer)))

            ' Inherited syntax
            Expect("Hello", Iz.TypeOf(GetType(String)))
            Expect("Hello", Iz.Not.TypeOf(GetType(Integer)))
        End Sub

        <Test()> _
        Public Sub InstanceOfTypeTests()
            ' Classic syntax
            Assert.IsInstanceOf(GetType(String), "Hello")
            Assert.IsNotInstanceOf(GetType(String), 5)

            ' Helper syntax
            Assert.That("Hello", Iz.InstanceOf(GetType(String)))
            Assert.That(5, Iz.Not.InstanceOf(GetType(String)))

            ' Inherited syntax
            Expect("Hello", InstanceOf(GetType(String)))
            Expect(5, Iz.Not.InstanceOf(GetType(String)))
        End Sub

        <Test()> _
        Public Sub AssignableFromTypeTests()
            ' Classic syntax
            Assert.IsAssignableFrom(GetType(String), "Hello")
            Assert.IsNotAssignableFrom(GetType(String), 5)

            ' Helper syntax
            Assert.That("Hello", Iz.AssignableFrom(GetType(String)))
            Assert.That(5, Iz.Not.AssignableFrom(GetType(String)))

            ' Inherited syntax
            Expect("Hello", AssignableFrom(GetType(String)))
            Expect(5, Iz.Not.AssignableFrom(GetType(String)))
        End Sub
#End Region

#Region "StringConstraintTests"
        <Test()> _
        Public Sub SubstringTests()
            Dim phrase As String = "Hello World!"
            Dim array As String() = New String() {"abc", "bad", "dba"}

            ' Classic Syntax
            StringAssert.Contains("World", phrase)

            ' Helper syntax
            Assert.That(phrase, Text.Contains("World"))
            ' Only available using new syntax
            Assert.That(phrase, Text.DoesNotContain("goodbye"))
            Assert.That(phrase, Text.Contains("WORLD").IgnoreCase)
            Assert.That(phrase, Text.DoesNotContain("BYE").IgnoreCase)
            Assert.That(array, Text.All.Contains("b"))

            ' Inherited syntax
            Expect(phrase, Contains("World"))
            ' Only available using new syntax
            Expect(phrase, Text.DoesNotContain("goodbye"))
            Expect(phrase, Contains("WORLD").IgnoreCase)
            Expect(phrase, Text.DoesNotContain("BYE").IgnoreCase)
            Expect(array, All.Contains("b"))
        End Sub

        <Test()> _
        Public Sub StartsWithTests()
            Dim phrase As String = "Hello World!"
            Dim greetings As String() = New String() {"Hello!", "Hi!", "Hola!"}

            ' Classic syntax
            StringAssert.StartsWith("Hello", phrase)

            ' Helper syntax
            Assert.That(phrase, Text.StartsWith("Hello"))
            ' Only available using new syntax
            Assert.That(phrase, Text.DoesNotStartWith("Hi!"))
            Assert.That(phrase, Text.StartsWith("HeLLo").IgnoreCase)
            Assert.That(phrase, Text.DoesNotStartWith("HI").IgnoreCase)
            Assert.That(greetings, Text.All.StartsWith("h").IgnoreCase)

            ' Inherited syntax
            Expect(phrase, StartsWith("Hello"))
            ' Only available using new syntax
            Expect(phrase, Text.DoesNotStartWith("Hi!"))
            Expect(phrase, StartsWith("HeLLo").IgnoreCase)
            Expect(phrase, Text.DoesNotStartWith("HI").IgnoreCase)
            Expect(greetings, All.StartsWith("h").IgnoreCase)
        End Sub

        <Test()> _
        Public Sub EndsWithTests()
            Dim phrase As String = "Hello World!"
            Dim greetings As String() = New String() {"Hello!", "Hi!", "Hola!"}

            ' Classic Syntax
            StringAssert.EndsWith("!", phrase)

            ' Helper syntax
            Assert.That(phrase, Text.EndsWith("!"))
            ' Only available using new syntax
            Assert.That(phrase, Text.DoesNotEndWith("?"))
            Assert.That(phrase, Text.EndsWith("WORLD!").IgnoreCase)
            Assert.That(greetings, Text.All.EndsWith("!"))

            ' Inherited syntax
            Expect(phrase, EndsWith("!"))
            ' Only available using new syntax
            Expect(phrase, Text.DoesNotEndWith("?"))
            Expect(phrase, EndsWith("WORLD!").IgnoreCase)
            Expect(greetings, All.EndsWith("!"))
        End Sub

        <Test()> _
        Public Sub EqualIgnoringCaseTests()

            Dim phrase As String = "Hello World!"
            Dim array1 As String() = New String() {"Hello", "World"}
            Dim array2 As String() = New String() {"HELLO", "WORLD"}
            Dim array3 As String() = New String() {"HELLO", "Hello", "hello"}

            ' Classic syntax
            StringAssert.AreEqualIgnoringCase("hello world!", phrase)

            ' Helper syntax
            Assert.That(phrase, Iz.EqualTo("hello world!").IgnoreCase)
            'Only available using new syntax
            Assert.That(phrase, Iz.Not.EqualTo("goodbye world!").IgnoreCase)
            Assert.That(array1, Iz.EqualTo(array2).IgnoreCase)
            Assert.That(array3, Iz.All.EqualTo("hello").IgnoreCase)

            ' Inherited syntax
            Expect(phrase, EqualTo("hello world!").IgnoreCase)
            'Only available using new syntax
            Expect(phrase, Iz.Not.EqualTo("goodbye world!").IgnoreCase)
            Expect(array1, EqualTo(array2).IgnoreCase)
            Expect(array3, All.EqualTo("hello").IgnoreCase)
        End Sub

        <Test()> _
        Public Sub RegularExpressionTests()
            Dim phrase As String = "Now is the time for all good men to come to the aid of their country."
            Dim quotes As String() = New String() {"Never say never", "It's never too late", "Nevermore!"}

            ' Classic syntax
            StringAssert.IsMatch("all good men", phrase)
            StringAssert.IsMatch("Now.*come", phrase)

            ' Helper syntax
            Assert.That(phrase, Text.Matches("all good men"))
            Assert.That(phrase, Text.Matches("Now.*come"))
            ' Only available using new syntax
            Assert.That(phrase, Text.DoesNotMatch("all.*men.*good"))
            Assert.That(phrase, Text.Matches("ALL").IgnoreCase)
            Assert.That(quotes, Text.All.Matches("never").IgnoreCase)

            ' Inherited syntax
            Expect(phrase, Matches("all good men"))
            Expect(phrase, Matches("Now.*come"))
            ' Only available using new syntax
            Expect(phrase, Text.DoesNotMatch("all.*men.*good"))
            Expect(phrase, Matches("ALL").IgnoreCase)
            Expect(quotes, All.Matches("never").IgnoreCase)
        End Sub
#End Region

#Region "Equality Tests"
        <Test()> _
        Public Sub EqualityTests()

            Dim i3 As Integer() = {1, 2, 3}
            Dim d3 As Double() = {1.0, 2.0, 3.0}
            Dim iunequal As Integer() = {1, 3, 2}

            ' Classic Syntax
            Assert.AreEqual(4, 2 + 2)
            Assert.AreEqual(i3, d3)
            Assert.AreNotEqual(5, 2 + 2)
            Assert.AreNotEqual(i3, iunequal)

            ' Helper syntax
            Assert.That(2 + 2, Iz.EqualTo(4))
            Assert.That(2 + 2 = 4)
            Assert.That(i3, Iz.EqualTo(d3))
            Assert.That(2 + 2, Iz.Not.EqualTo(5))
            Assert.That(i3, Iz.Not.EqualTo(iunequal))

            ' Inherited syntax
            Expect(2 + 2, EqualTo(4))
            Expect(2 + 2 = 4)
            Expect(i3, EqualTo(d3))
            Expect(2 + 2, Iz.Not.EqualTo(5))
            Expect(i3, Iz.Not.EqualTo(iunequal))
        End Sub

        <Test()> _
        Public Sub EqualityTestsWithTolerance()
            ' CLassic syntax
            Assert.AreEqual(5.0R, 4.99R, 0.05R)
            Assert.AreEqual(5.0F, 4.99F, 0.05F)

            ' Helper syntax
            Assert.That(4.99R, Iz.EqualTo(5.0R).Within(0.05R))
            Assert.That(4D, Iz.Not.EqualTo(5D).Within(0.5D))
            Assert.That(4.99F, Iz.EqualTo(5.0F).Within(0.05F))
            Assert.That(4.99D, Iz.EqualTo(5D).Within(0.05D))
            Assert.That(499, Iz.EqualTo(500).Within(5))
            Assert.That(4999999999L, Iz.EqualTo(5000000000L).Within(5L))

            ' Inherited syntax
            Expect(4.99R, EqualTo(5.0R).Within(0.05R))
            Expect(4D, Iz.Not.EqualTo(5D).Within(0.5D))
            Expect(4.99F, EqualTo(5.0F).Within(0.05F))
            Expect(4.99D, EqualTo(5D).Within(0.05D))
            Expect(499, EqualTo(500).Within(5))
            Expect(4999999999L, EqualTo(5000000000L).Within(5L))
        End Sub

        <Test()> _
        Public Sub EqualityTestsWithTolerance_MixedFloatAndDouble()
            ' Bug Fix 1743844
            Assert.That(2.20492R, Iz.EqualTo(2.2R).Within(0.01F), _
                "Double actual, Double expected, Single tolerance")
            Assert.That(2.20492R, Iz.EqualTo(2.2F).Within(0.01R), _
                "Double actual, Single expected, Double tolerance")
            Assert.That(2.20492R, Iz.EqualTo(2.2F).Within(0.01F), _
                "Double actual, Single expected, Single tolerance")
            Assert.That(2.20492F, Iz.EqualTo(2.2F).Within(0.01R), _
                "Single actual, Single expected, Double tolerance")
            Assert.That(2.20492F, Iz.EqualTo(2.2R).Within(0.01R), _
                "Single actual, Double expected, Double tolerance")
            Assert.That(2.20492F, Iz.EqualTo(2.2R).Within(0.01F), _
                "Single actual, Double expected, Single tolerance")
        End Sub

        <Test()> _
        Public Sub EqualityTestsWithTolerance_MixingTypesGenerally()
            ' Extending tolerance to all numeric types
            Assert.That(202.0R, Iz.EqualTo(200.0R).Within(2), _
                "Double actual, Double expected, int tolerance")
            Assert.That(4.87D, Iz.EqualTo(5).Within(0.25R), _
                "Decimal actual, int expected, Double tolerance")
            Assert.That(4.87D, Iz.EqualTo(5L).Within(1), _
                "Decimal actual, long expected, int tolerance")
            Assert.That(487, Iz.EqualTo(500).Within(25), _
                "int actual, int expected, int tolerance")
            Assert.That(487L, Iz.EqualTo(500).Within(25), _
                "long actual, int expected, int tolerance")
        End Sub
#End Region

#Region "Comparison Tests"
        <Test()> _
        Public Sub ComparisonTests()
            ' Classic Syntax
            Assert.Greater(7, 3)
            Assert.GreaterOrEqual(7, 3)
            Assert.GreaterOrEqual(7, 7)

            ' Helper syntax
            Assert.That(7, Iz.GreaterThan(3))
            Assert.That(7, Iz.GreaterThanOrEqualTo(3))
            Assert.That(7, Iz.AtLeast(3))
            Assert.That(7, Iz.GreaterThanOrEqualTo(7))
            Assert.That(7, Iz.AtLeast(7))

            ' Inherited syntax
            Expect(7, GreaterThan(3))
            Expect(7, GreaterThanOrEqualTo(3))
            Expect(7, AtLeast(3))
            Expect(7, GreaterThanOrEqualTo(7))
            Expect(7, AtLeast(7))

            ' Classic syntax
            Assert.Less(3, 7)
            Assert.LessOrEqual(3, 7)
            Assert.LessOrEqual(3, 3)

            ' Helper syntax
            Assert.That(3, Iz.LessThan(7))
            Assert.That(3, Iz.LessThanOrEqualTo(7))
            Assert.That(3, Iz.AtMost(7))
            Assert.That(3, Iz.LessThanOrEqualTo(3))
            Assert.That(3, Iz.AtMost(3))

            ' Inherited syntax
            Expect(3, LessThan(7))
            Expect(3, LessThanOrEqualTo(7))
            Expect(3, AtMost(7))
            Expect(3, LessThanOrEqualTo(3))
            Expect(3, AtMost(3))
        End Sub
#End Region

#Region "Collection Tests"
        <Test()> _
        Public Sub AllItemsTests()

            Dim ints As Object() = {1, 2, 3, 4}
            Dim doubles As Object() = {0.99, 2.1, 3.0, 4.05}
            Dim strings As Object() = {"abc", "bad", "cab", "bad", "dad"}

            ' Classic syntax
            CollectionAssert.AllItemsAreNotNull(ints)
            CollectionAssert.AllItemsAreInstancesOfType(ints, GetType(Integer))
            CollectionAssert.AllItemsAreInstancesOfType(strings, GetType(String))
            CollectionAssert.AllItemsAreUnique(ints)

            ' Helper syntax
            Assert.That(ints, Iz.All.Not.Null)
            Assert.That(ints, Has.None.Null)
            Assert.That(ints, Iz.All.InstanceOfType(GetType(Integer)))
            Assert.That(ints, Has.All.InstanceOfType(GetType(Integer)))
            Assert.That(strings, Iz.All.InstanceOfType(GetType(String)))
            Assert.That(strings, Has.All.InstanceOfType(GetType(String)))
            Assert.That(ints, Iz.Unique)
            ' Only available using new syntax
            Assert.That(strings, Iz.Not.Unique)
            Assert.That(ints, Iz.All.GreaterThan(0))
            Assert.That(ints, Has.All.GreaterThan(0))
            Assert.That(ints, Has.None.LessThanOrEqualTo(0))
            Assert.That(strings, Text.All.Contains("a"))
            Assert.That(strings, Has.All.Contains("a"))
            Assert.That(strings, Has.Some.StartsWith("ba"))
            Assert.That(strings, Has.Some.Property("Length").EqualTo(3))
            Assert.That(strings, Has.Some.StartsWith("BA").IgnoreCase)
            Assert.That(doubles, Has.Some.EqualTo(1.0).Within(0.05))

            ' Inherited syntax
            Expect(ints, All.Not.Null)
            Expect(ints, None.Null)
            Expect(ints, All.InstanceOfType(GetType(Integer)))
            Expect(strings, All.InstanceOfType(GetType(String)))
            Expect(ints, Unique)
            ' Only available using new syntax
            Expect(strings, Iz.Not.Unique)
            Expect(ints, All.GreaterThan(0))
            Expect(strings, All.Contains("a"))
            Expect(strings, Some.StartsWith("ba"))
            Expect(strings, Some.StartsWith("BA").IgnoreCase)
            Expect(doubles, Some.EqualTo(1.0).Within(0.05))
        End Sub

        <Test()> _
       Public Sub SomeItemsTests()

            Dim mixed As Object() = {1, 2, "3", Nothing, "four", 100}
            Dim strings As Object() = {"abc", "bad", "cab", "bad", "dad"}

            ' Not available using the classic syntax

            ' Helper syntax
            Assert.That(mixed, Has.Some.Null)
            Assert.That(mixed, Has.Some.InstanceOfType(GetType(Integer)))
            Assert.That(mixed, Has.Some.InstanceOfType(GetType(String)))
            Assert.That(strings, Has.Some.StartsWith("ba"))
            Assert.That(strings, Has.Some.Not.StartsWith("ba"))

            ' Inherited syntax
            Expect(mixed, Some.Null)
            Expect(mixed, Some.InstanceOfType(GetType(Integer)))
            Expect(mixed, Some.InstanceOfType(GetType(String)))
            Expect(strings, Some.StartsWith("ba"))
            Expect(strings, Some.Not.StartsWith("ba"))
        End Sub

        <Test()> _
        Public Sub NoItemsTests()

            Dim ints As Object() = {1, 2, 3, 4, 5}
            Dim strings As Object() = {"abc", "bad", "cab", "bad", "dad"}

            ' Not available using the classic syntax

            ' Helper syntax
            Assert.That(ints, Has.None.Null)
            Assert.That(ints, Has.None.InstanceOfType(GetType(String)))
            Assert.That(ints, Has.None.GreaterThan(99))
            Assert.That(strings, Has.None.StartsWith("qu"))

            ' Inherited syntax
            Expect(ints, None.Null)
            Expect(ints, None.InstanceOfType(GetType(String)))
            Expect(ints, None.GreaterThan(99))
            Expect(strings, None.StartsWith("qu"))
        End Sub

        <Test()> _
        Public Sub CollectionContainsTests()

            Dim iarray As Integer() = {1, 2, 3}
            Dim sarray As String() = {"a", "b", "c"}

            ' Classic syntax
            Assert.Contains(3, iarray)
            Assert.Contains("b", sarray)
            CollectionAssert.Contains(iarray, 3)
            CollectionAssert.Contains(sarray, "b")
            CollectionAssert.DoesNotContain(sarray, "x")
            ' Showing that Contains uses NUnit equality
            CollectionAssert.Contains(iarray, 1.0R)

            ' Helper syntax
            Assert.That(iarray, Has.Member(3))
            Assert.That(sarray, Has.Member("b"))
            Assert.That(sarray, Has.No.Member("x"))
            ' Showing that Contains uses NUnit equality
            Assert.That(iarray, Has.Member(1.0R))

            ' Only available using the new syntax
            ' Note that EqualTo and SameAs do NOT give
            ' identical results to Contains because 
            ' Contains uses Object.Equals()
            Assert.That(iarray, Has.Some.EqualTo(3))
            Assert.That(iarray, Has.Member(3))
            Assert.That(sarray, Has.Some.EqualTo("b"))
            Assert.That(sarray, Has.None.EqualTo("x"))
            Assert.That(iarray, Has.None.SameAs(1.0R))
            Assert.That(iarray, Has.All.LessThan(10))
            Assert.That(sarray, Has.All.Length.EqualTo(1))
            Assert.That(sarray, Has.None.Property("Length").GreaterThan(3))

            ' Inherited syntax
            Expect(iarray, Contains(3))
            Expect(sarray, Contains("b"))
            Expect(sarray, Has.No.Member("x"))

            ' Only available using new syntax
            ' Note that EqualTo and SameAs do NOT give
            ' identical results to Contains because 
            ' Contains uses Object.Equals()
            Expect(iarray, Some.EqualTo(3))
            Expect(sarray, Some.EqualTo("b"))
            Expect(sarray, None.EqualTo("x"))
            Expect(iarray, All.LessThan(10))
            Expect(sarray, All.Length.EqualTo(1))
            Expect(sarray, None.Property("Length").GreaterThan(3))
        End Sub

        <Test()> _
        Public Sub CollectionEquivalenceTests()

            Dim ints1to5 As Integer() = {1, 2, 3, 4, 5}
            Dim twothrees As Integer() = {1, 2, 3, 3, 4, 5}
            Dim twofours As Integer() = {1, 2, 3, 4, 4, 5}

            ' Classic syntax
            CollectionAssert.AreEquivalent(New Integer() {2, 1, 4, 3, 5}, ints1to5)
            CollectionAssert.AreNotEquivalent(New Integer() {2, 2, 4, 3, 5}, ints1to5)
            CollectionAssert.AreNotEquivalent(New Integer() {2, 4, 3, 5}, ints1to5)
            CollectionAssert.AreNotEquivalent(New Integer() {2, 2, 1, 1, 4, 3, 5}, ints1to5)
            CollectionAssert.AreNotEquivalent(twothrees, twofours)

            ' Helper syntax
            Assert.That(New Integer() {2, 1, 4, 3, 5}, Iz.EquivalentTo(ints1to5))
            Assert.That(New Integer() {2, 2, 4, 3, 5}, Iz.Not.EquivalentTo(ints1to5))
            Assert.That(New Integer() {2, 4, 3, 5}, Iz.Not.EquivalentTo(ints1to5))
            Assert.That(New Integer() {2, 2, 1, 1, 4, 3, 5}, Iz.Not.EquivalentTo(ints1to5))
            Assert.That(twothrees, Iz.Not.EquivalentTo(twofours))

            ' Inherited syntax
            Expect(New Integer() {2, 1, 4, 3, 5}, EquivalentTo(ints1to5))
        End Sub

        <Test()> _
        Public Sub SubsetTests()

            Dim ints1to5 As Integer() = {1, 2, 3, 4, 5}

            ' Classic syntax
            CollectionAssert.IsSubsetOf(New Integer() {1, 3, 5}, ints1to5)
            CollectionAssert.IsSubsetOf(New Integer() {1, 2, 3, 4, 5}, ints1to5)
            CollectionAssert.IsNotSubsetOf(New Integer() {2, 4, 6}, ints1to5)
            CollectionAssert.IsNotSubsetOf(New Integer() {1, 2, 2, 2, 5}, ints1to5)

            ' Helper syntax
            Assert.That(New Integer() {1, 3, 5}, Iz.SubsetOf(ints1to5))
            Assert.That(New Integer() {1, 2, 3, 4, 5}, Iz.SubsetOf(ints1to5))
            Assert.That(New Integer() {2, 4, 6}, Iz.Not.SubsetOf(ints1to5))

            ' Inherited syntax
            Expect(New Integer() {1, 3, 5}, SubsetOf(ints1to5))
            Expect(New Integer() {1, 2, 3, 4, 5}, SubsetOf(ints1to5))
            Expect(New Integer() {2, 4, 6}, Iz.Not.SubsetOf(ints1to5))
        End Sub
#End Region

#Region "Property Tests"
        <Test()> _
        Public Sub PropertyTests()

            Dim array As String() = {"abc", "bca", "xyz", "qrs"}
            Dim array2 As String() = {"a", "ab", "abc"}
            Dim list As New ArrayList(array)

            ' Not available using the classic syntax

            ' Helper syntax
            ' Assert.That(list, Has.Property("Count"))
            ' Assert.That(list, Has.No.Property("Length"))

            Assert.That("Hello", Has.Length.EqualTo(5))
            Assert.That("Hello", Has.Property("Length").EqualTo(5))
            Assert.That("Hello", Has.Property("Length").GreaterThan(3))

            Assert.That(array, Has.Property("Length").EqualTo(4))
            Assert.That(array, Has.Length.EqualTo(4))
            Assert.That(array, Has.Property("Length").LessThan(10))

            Assert.That(array, Has.All.Property("Length").EqualTo(3))
            Assert.That(array, Has.All.Length.EqualTo(3))
            Assert.That(array, Iz.All.Length.EqualTo(3))
            Assert.That(array, Has.All.Property("Length").EqualTo(3))
            Assert.That(array, Iz.All.Property("Length").EqualTo(3))

            Assert.That(array2, Iz.Not.Property("Length").EqualTo(4))
            Assert.That(array2, Iz.Not.Length.EqualTo(4))
            Assert.That(array2, Has.No.Property("Length").GreaterThan(3))

            ' Inherited syntax
            ' Expect(list, Has.Property("Count"))
            ' Expect(list, Has.No.Property("Nada"))

            Expect(array, All.Property("Length").EqualTo(3))
            Expect(array, All.Length.EqualTo(3))
        End Sub
#End Region

#Region "Not Tests"
        <Test()> _
        Public Sub NotTests()
            ' Not available using the classic syntax

            ' Helper syntax
            Assert.That(42, Iz.Not.Null)
            Assert.That(42, Iz.Not.True)
            Assert.That(42, Iz.Not.False)
            Assert.That(2.5, Iz.Not.NaN)
            Assert.That(2 + 2, Iz.Not.EqualTo(3))
            Assert.That(2 + 2, Iz.Not.Not.EqualTo(4))
            Assert.That(2 + 2, Iz.Not.Not.Not.EqualTo(5))

            ' Inherited syntax
            Expect(42, Iz.Not.Null)
            Expect(42, Iz.Not.True)
            Expect(42, Iz.Not.False)
            Expect(2.5, Iz.Not.NaN)
            Expect(2 + 2, Iz.Not.EqualTo(3))
            Expect(2 + 2, Iz.Not.Not.EqualTo(4))
            Expect(2 + 2, Iz.Not.Not.Not.EqualTo(5))
        End Sub
#End Region

    End Class

End Namespace

