namespace NUnit.Samples.Money

open System

type MoneyBag([<ParamArray>] contents: Money[]) =
    let mutable monies:Money[] = contents

    member self.Multiply(factor) =
        let result = array.CreateInstance(typeof<Money>, monies.Length)
        for i in 0..contents.Length-1 do
            result[i] = contents[i] * factor
            
