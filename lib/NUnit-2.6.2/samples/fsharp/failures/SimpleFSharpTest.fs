// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org
// ****************************************************************

module NUnit.Samples.SimpleCSharpTest

open System
open NUnit.Framework

let fValue1 = 2
let fValue2 = 3

[<Test>]
let Add() =
    let result = fValue1 + fValue2
    Assert.AreEqual(6, result, "Expected Failure")

[<Test>]
let DivideByZero() =
    let zero = 0
    let result = 8 / zero
    Assert.True(true)

[<Test>]
let Equals() =
    Assert.AreEqual(12, 12, "Integer")
    Assert.AreEqual(12L, 12L, "Long")
    Assert.AreEqual('a', 'a', "Char")
    Assert.AreEqual(12, 13, "Expected Failure");

[<Test>]
[<ExpectedException(typeof<InvalidOperationException>)>]
let ExpectAnException() =
    raise(InvalidCastException())
    Assert.True(true)

[<Test>]
[<Ignore("ignored test")>]
let IgnoredTest() =
    Assert.True(true)
