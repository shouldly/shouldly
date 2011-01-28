' ****************************************************************
' This is free software licensed under the NUnit license. You
' may obtain a copy of the license as well as information regarding
' copyright ownership at http://nunit.org/?p=license&r=2.4.
' ****************************************************************

Option Explicit On 
Imports System
Imports NUnit.Framework

Namespace NUnit.Samples

    <TestFixture()> Public Class SimpleVBTest

        Private fValue1 As Integer
        Private fValue2 As Integer

        Public Sub New()
            MyBase.New()
        End Sub

        <SetUp()> Public Sub Init()
            fValue1 = 2
            fValue2 = 3
        End Sub

        <Test()> Public Sub Add()
            Dim result As Double

            result = fValue1 + fValue2
            Assert.AreEqual(6, result)
        End Sub

        <Test()> Public Sub DivideByZero()
            Dim zero As Integer
            Dim result As Integer

            zero = 0
            result = 8 / zero
        End Sub

        <Test()> Public Sub TestEquals()
            Assert.AreEqual(12, 12)
            Assert.AreEqual(CLng(12), CLng(12))

            Assert.AreEqual(12, 13, "Size")
            Assert.AreEqual(12, 11.99, 0, "Capacity")
        End Sub

        <Test(), ExpectedException(GetType(Exception))> Public Sub ExpectAnException()
            Throw New InvalidCastException()
        End Sub

        <Test(), Ignore("sample ignore")> Public Sub IgnoredTest()
            ' does not matter what we type the test is not run
            Throw New ArgumentException()
        End Sub

    End Class
End Namespace