module NUnit.Samples.MoneyTest

open System
open NUnit.Framework
open NUnit.Samples.Money

let f12CHF = new Money(12, "CHF")
let f14CHF = new Money(14, "CHF")
let f7USD = new Money(7, "USD")
let f21USD = new Money(21, "USD")

let fMB1 = new MoneyBag( [ f12CHF; f7USD ] )
let fMB2 = new MoneyBag( [ f14CHF; f21USD ] )

[<Test>]
let MoneyIsZero() =
    let zeroUSD = new Money(0, "USD")
    Assert.That(zeroUSD.IsZero)

[<Test>]
let MoneyEquals()  =
    //NOTE: Normally we use Assert.AreEqual to test whether two
    // objects are equal. But here we are testing the Money.Equals()
    // method itself, so using AreEqual would not serve the purpose.
    Assert.IsFalse(f12CHF.Equals(null))
    let equalMoney = new Money(12, "CHF")
    Assert.IsTrue(f12CHF.Equals( f12CHF ))
    Assert.IsTrue(f12CHF.Equals( equalMoney ))
    Assert.IsFalse(f12CHF.Equals(f14CHF))

[<Test>]
let MoneyHashCode() =
    let equal = new Money(12, "CHF")
    Assert.AreEqual(f12CHF.GetHashCode(), equal.GetHashCode())

[<Test>]
let MoneyPrint() =
    Assert.AreEqual("[12 CHF]", f12CHF.ToString())

[<Test>]
let SimpleAdd() =
    // [12 CHF] + [14 CHF] == [26 CHF]
    let expected = new Money(26, "CHF")
    Assert.AreEqual(expected, f12CHF.AddMoney(f14CHF))
    Assert.AreEqual(expected, f12CHF.Add(f14CHF))
    Assert.AreEqual(expected, f12CHF + f14CHF)

[<Test>]
let SimpleMultiply() =
    // [14 CHF] *2 == [28 CHF]
    let expected = new Money(28, "CHF")
    Assert.AreEqual(expected, f14CHF.Multiply(2))
    Assert.AreEqual(expected, f14CHF*2)
    Assert.AreEqual(expected, 2*f14CHF)

[<Test>]
let SimpleNegate() =
    // [14 CHF] negate == [-14 CHF]
    let expected = new Money(-14, "CHF")
    Assert.AreEqual(expected, f14CHF.Negate())
    Assert.AreEqual(expected, -f14CHF)

[<Test>]
let SimpleSubtract()  =
    // [14 CHF] - [12 CHF] == [2 CHF]
    let expected = new Money(2, "CHF")
    Assert.AreEqual(expected, f14CHF.Subtract(f12CHF))
    Assert.AreEqual(expected, f14CHF - f12CHF)

[<Test>]
let MoneyBagEquals() =
    //NOTE: Normally we use Assert.AreEqual to test whether two
    // objects are equal. But here we are testing the MoneyBag.Equals()
    // method itself, so using AreEqual would not serve the purpose.
    Assert.False(fMB1.Equals(null)) 
    Assert.True(fMB1.Equals( fMB1 ))
    let equalBag = new MoneyBag( [new Money(12, "CHF"); new Money(7, "USD")] )
    Assert.True(fMB1.Equals(equalBag))
    Assert.False(fMB1.Equals(f12CHF))
    Assert.False(f12CHF.Equals(fMB1))
    Assert.False(fMB1.Equals(fMB2))

[<Test>]
let MoneyBagHash() =
    let equalBag = new MoneyBag( [new Money(12, "CHF"); new Money(7, "USD")] )
    Assert.AreEqual(fMB1.GetHashCode(), equalBag.GetHashCode())

[<Test>]
let MoneyBagIsZero() =
    Assert.True(fMB1.Subtract(fMB1).IsZero);

    let bag = new MoneyBag( [ new Money(0, "CHF"); new Money(0, "USD") ] )
    Assert.True( bag.IsZero )

[<Test>]
let BagMultiply() =
    // {[12 CHF][7 USD]} *2 == {[24 CHF][14 USD]}
    let expected = new MoneyBag( [new Money(24, "CHF"); new Money(14, "USD")] )
    Assert.AreEqual(expected, fMB1.Multiply(2))
    Assert.AreEqual(expected, fMB1 * 2)
    Assert.AreEqual(expected, 2 * fMB1)
    Assert.AreEqual(fMB1, fMB1.Multiply(1))
    Assert.IsTrue(fMB1.Multiply(0).IsZero)

[<Test>]
let BagNegate() =
    // {[12 CHF][7 USD]} negate == {[-12 CHF][-7 USD]}
    let expected = new MoneyBag( [new Money(-12, "CHF"); new Money(-7, "USD")] )
    Assert.AreEqual(expected, -fMB1)
    Assert.AreEqual(expected, fMB1.Negate())

[<Test>]
let BagSimpleAdd() =
    // {[12 CHF][7 USD]} + [14 CHF] == {[26 CHF][7 USD]}
    let bag = [ new Money(26, "CHF"); new Money(7, "USD") ]
    let expected = new MoneyBag(bag)
    Assert.AreEqual(expected, fMB1.AddMoney(f14CHF))
    Assert.AreEqual(expected, fMB1.Add(f14CHF))
    Assert.AreEqual(expected, fMB1 + f14CHF)

[<Test>]
let BagSubtract() =
    // {[12 CHF][7 USD]} - {[14 CHF][21 USD] == {[-2 CHF][-14 USD]}
    let bag= [ new Money(-2, "CHF"); new Money(-14, "USD") ]
    let expected= new MoneyBag(bag)
    Assert.AreEqual(expected, fMB1.Subtract(fMB2))
    Assert.AreEqual(expected, fMB1 - fMB2)

[<Test>]
let BagSumAdd() =
    // {[12 CHF][7 USD]} + {[14 CHF][21 USD]} == {[26 CHF][28 USD]}
    let contents = [ new Money(26, "CHF"); new Money(28, "USD") ]
    let expected= new MoneyBag(contents)
    Assert.AreEqual(expected, fMB1.AddMoneyBag(fMB2))
    Assert.AreEqual(expected, fMB1.Add(fMB2))
    Assert.AreEqual(expected, fMB1 + fMB2)

[<Test>]
let MixedSimpleAdd() =
    // [12 CHF] + [7 USD] == {[12 CHF][7 USD]}
    let contents = [ f12CHF; f7USD ]
    let expected = new MoneyBag(contents)
    Assert.AreEqual(expected, f12CHF.AddMoney(f7USD))
    Assert.AreEqual(expected, f12CHF.Add(f7USD))
    Assert.AreEqual(expected, f12CHF + f7USD)
    Assert.AreEqual(expected, f7USD + f12CHF)

[<Test>]
let SimpleBagAdd() =
    // [14 CHF] + {[12 CHF][7 USD]} == {[26 CHF][7 USD]}
    let contents = [ new Money(26, "CHF"); new Money(7, "USD") ]
    let expected= new MoneyBag(contents)
    Assert.AreEqual(expected, f14CHF.Add(fMB1))
    Assert.AreEqual(expected, f14CHF + fMB1)

[<Test>]
let Normalize() =
    let contents = [ new Money(26, "CHF"); new Money(28, "CHF"); new Money(6, "CHF") ]
    let moneyBag= new MoneyBag(contents)
    let expected = [ new Money(60, "CHF") ]
    // note: expected is still a MoneyBag
    let expectedBag = new MoneyBag( [new Money(60, "CHF")] )
    Assert.AreEqual(expectedBag, moneyBag)

[<Test>]
let Normalize2() =
    // {[12 CHF][7 USD]} - [12 CHF] == [7 USD]
    let expected = new Money(7, "USD")
    Assert.AreEqual(expected, fMB1.Subtract(f12CHF))
    Assert.AreEqual(expected, fMB1 - f12CHF)

[<Test>]
let Normalize3() =
    // {[12 CHF][7 USD]} - {[12 CHF][3 USD]} == [4 USD]
    let contents = [ new Money(12, "CHF"); new Money(3, "USD") ]
    let bag = new MoneyBag(contents)
    let expected = new Money(4, "USD")
    Assert.AreEqual(expected, fMB1.Subtract(bag))
    Assert.AreEqual(expected, fMB1 - bag)

[<Test>]
let Normalize4() =
    // [12 CHF] - {[12 CHF][3 USD]} == [-3 USD]
    let contents = [ new Money(12, "CHF"); new Money(3, "USD") ]
    let bag = new MoneyBag(contents)
    let expected = new Money(-3, "USD")
    Assert.AreEqual(expected, f12CHF.Subtract(bag));
    Assert.AreEqual(expected, f12CHF - bag)

