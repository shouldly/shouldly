' ****************************************************************
' This is free software licensed under the NUnit license. You
' may obtain a copy of the license as well as information regarding
' copyright ownership at http://nunit.org/?p=license&r=2.4.
' ****************************************************************

Option Explicit On 

Imports System
Imports NUnit.Framework

Namespace NUnit.Samples

    <TestFixture()> _
    Public Class MoneyTest

        Private f12CHF As Money
        Private f14CHF As Money
        Private f7USD As Money
        Private f21USD As Money

        Private fMB1 As MoneyBag
        Private fMB2 As MoneyBag

        <SetUp()> _
        Protected Sub SetUp()

            f12CHF = New Money(12, "CHF")
            f14CHF = New Money(14, "CHF")
            f7USD = New Money(7, "USD")
            f21USD = New Money(21, "USD")

            fMB1 = New MoneyBag(f12CHF, f7USD)
            fMB2 = New MoneyBag(f14CHF, f21USD)

        End Sub

        <Test()> _
        Public Sub BagMultiply()
            ' {[12 CHF][7 USD]} *2 == {[24 CHF][14 USD]}
            Dim bag() As Money = New Money() {New Money(24, "CHF"), New Money(14, "USD")}
            Dim expected As New MoneyBag(bag)
            Assert.AreEqual(expected, fMB1.Multiply(2))
            Assert.AreEqual(fMB1, fMB1.Multiply(1))
            Assert.IsTrue(fMB1.Multiply(0).IsZero)
        End Sub

        <Test()> _
        Public Sub BagNegate()
            ' {[12 CHF][7 USD]} negate == {[-12 CHF][-7 USD]}
            Dim bag() As Money = New Money() {New Money(-12, "CHF"), New Money(-7, "USD")}
            Dim expected As New MoneyBag(bag)
            Assert.AreEqual(expected, fMB1.Negate())
        End Sub

        <Test()> _
        Public Sub BagSimpleAdd()

            ' {[12 CHF][7 USD]} + [14 CHF] == {[26 CHF][7 USD]}
            Dim bag() As Money = New Money() {New Money(26, "CHF"), New Money(7, "USD")}
            Dim expected As New MoneyBag(bag)
            Assert.AreEqual(expected, fMB1.Add(f14CHF))

        End Sub

        <Test()> _
        Public Sub BagSubtract()
            ' {[12 CHF][7 USD]} - {[14 CHF][21 USD] == {[-2 CHF][-14 USD]}
            Dim bag() As Money = New Money() {New Money(-2, "CHF"), New Money(-14, "USD")}
            Dim expected As New MoneyBag(bag)
            Assert.AreEqual(expected, fMB1.Subtract(fMB2))
        End Sub

        <Test()> _
        Public Sub BagSumAdd()
            ' {[12 CHF][7 USD]} + {[14 CHF][21 USD]} == {[26 CHF][28 USD]}
            Dim bag() As Money = New Money() {New Money(26, "CHF"), New Money(28, "USD")}
            Dim expected As New MoneyBag(bag)
            Assert.AreEqual(expected, fMB1.Add(fMB2))
        End Sub

        <Test()> _
        Public Sub IsZero()
            Assert.IsTrue(fMB1.Subtract(fMB1).IsZero)

            Dim bag() As Money = New Money() {New Money(0, "CHF"), New Money(0, "USD")}
            Assert.IsTrue(New MoneyBag(bag).IsZero)
        End Sub

        <Test()> _
        Public Sub MixedSimpleAdd()
            ' [12 CHF] + [7 USD] == {[12 CHF][7 USD]}
            Dim bag() As Money = New Money() {f12CHF, f7USD}
            Dim expected As New MoneyBag(bag)
            Assert.AreEqual(expected, f12CHF.Add(f7USD))
        End Sub

        <Test()> _
        Public Sub MoneyBagEquals()
            ' NOTE: Normally we use Assert.AreEqual to test whether two
            ' objects are equal. But here we are testing the MoneyBag.Equals()
            ' method itself, so using AreEqual would not serve the purpose.
            Assert.IsFalse(fMB1.Equals(Nothing))

            Assert.IsTrue(fMB1.Equals(fMB1))
            Dim equal As MoneyBag = New MoneyBag(New Money(12, "CHF"), New Money(7, "USD"))
            Assert.IsTrue(fMB1.Equals(equal))
            Assert.IsFalse(fMB1.Equals(f12CHF))
            Assert.IsFalse(f12CHF.Equals(fMB1))
            Assert.IsFalse(fMB1.Equals(fMB2))
        End Sub

        <Test()> _
        Public Sub MoneyBagHash()
            Dim equal As MoneyBag = New MoneyBag(New Money(12, "CHF"), New Money(7, "USD"))
            Assert.AreEqual(fMB1.GetHashCode(), equal.GetHashCode())
        End Sub

        <Test()> _
        Public Sub MoneyEquals()
            ' NOTE: Normally we use Assert.AreEqual to test whether two
            ' objects are equal. But here we are testing the MoneyBag.Equals()
            ' method itself, so using AreEqual would not serve the purpose.
            Assert.IsFalse(f12CHF.Equals(Nothing))
            Dim equalMoney As Money = New Money(12, "CHF")
            Assert.IsTrue(f12CHF.Equals(f12CHF))
            Assert.IsTrue(f12CHF.Equals(equalMoney))
            Assert.IsFalse(f12CHF.Equals(f14CHF))
        End Sub

        <Test()> _
        Public Sub MoneyHash()
            Assert.IsFalse(f12CHF.Equals(Nothing))
            Dim equal As Money = New Money(12, "CHF")
            Assert.AreEqual(f12CHF.GetHashCode(), equal.GetHashCode())
        End Sub

        <Test()> _
        Public Sub Normalize()
            Dim bag() As Money = New Money() {New Money(26, "CHF"), New Money(28, "CHF"), New Money(6, "CHF")}
            Dim moneyBag As New MoneyBag(bag)
            Dim expected() As Money = New Money() {New Money(60, "CHF")}
            '	// note: expected is still a MoneyBag
            Dim expectedBag As New MoneyBag(expected)
            Assert.AreEqual(expectedBag, moneyBag)
        End Sub

        <Test()> _
        Public Sub Normalize2()
            ' {[12 CHF][7 USD]} - [12 CHF] == [7 USD]
            Dim expected As Money = New Money(7, "USD")
            Assert.AreEqual(expected, fMB1.Subtract(f12CHF))
        End Sub

        <Test()> _
        Public Sub Normalize3()
            ' {[12 CHF][7 USD]} - {[12 CHF][3 USD]} == [4 USD]
            Dim s1() As Money = New Money() {New Money(12, "CHF"), New Money(3, "USD")}
            Dim ms1 As New MoneyBag(s1)
            Dim expected As New Money(4, "USD")
            Assert.AreEqual(expected, fMB1.Subtract(ms1))
        End Sub

        <Test()> _
        Public Sub Normalize4()
            ' [12 CHF] - {[12 CHF][3 USD]} == [-3 USD]
            Dim s1() As Money = New Money() {New Money(12, "CHF"), New Money(3, "USD")}
            Dim ms1 As New MoneyBag(s1)
            Dim expected As New Money(-3, "USD")
            Assert.AreEqual(expected, f12CHF.Subtract(ms1))
        End Sub

        <Test()> _
        Public Sub Print()
            Assert.AreEqual("[12 CHF]", f12CHF.ToString())
        End Sub

        <Test()> _
        Public Sub SimpleAdd()

            ' [12 CHF] + [14 CHF] == [26 CHF]
            Dim expected As Money = New Money(26, "CHF")
            Assert.AreEqual(expected, f12CHF.Add(f14CHF))

        End Sub

        <Test()> _
        Public Sub SimpleNegate()

            ' [14 CHF] negate == [-14 CHF]
            Dim expected As New Money(-14, "CHF")
            Assert.AreEqual(expected, f14CHF.Negate())

        End Sub

        <Test()> _
        Public Sub SimpleSubtract()

            ' [14 CHF] - [12 CHF] == [2 CHF]
            Dim expected As New Money(2, "CHF")
            Assert.AreEqual(expected, f14CHF.Subtract(f12CHF))

        End Sub

        <Test()> _
        Public Sub SimpleMultiply()

            ' [14 CHF] *2 == [28 CHF]
            Dim expected As New Money(28, "CHF")
            Assert.AreEqual(expected, f14CHF.Multiply(2))

        End Sub

    End Class

End Namespace
