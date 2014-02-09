module NUnit.Samples.Money

open System
open System.Collections.Generic

type IMoney =
    abstract IsZero : bool
    abstract Add : IMoney -> IMoney
    abstract AddMoney : Money -> IMoney
    abstract Multiply : int -> IMoney
    abstract Negate : unit -> IMoney
    abstract Subtract : IMoney -> IMoney

and Money(amount: int, currency: string) as this =

    member internal this.currency
        with get() = currency
    member internal this.amount
        with get() = amount

    override self.Equals(anObject) =
        match anObject with
            | :? Money as money -> this.IsZero && money.IsZero || currency = money.currency && amount = money.amount
            | _ -> false
    override this.GetHashCode() = currency.GetHashCode() + amount 
    override this.ToString() = System.String.Format("[{0} {1}]", amount, currency)

    interface IMoney with
        member this.IsZero = amount = 0
        member this.Add(m: IMoney) = m.AddMoney(this)
        member this.AddMoney(m: Money) = this.AddMoney(m)
        member this.Multiply(factor) = this.Multiply(factor) :> IMoney
        member this.Negate() = this.Negate() :> IMoney
        member this.Subtract(m: IMoney) = this.Subtract(m) : IMoney

    member this.IsZero = (this :> IMoney).IsZero
    member this.Add(m: IMoney) = m.AddMoney(this)
    member this.AddMoney(m: Money) =
        if m.currency = this.currency then new Money(m.amount + this.amount, this.currency) :> IMoney
        else new MoneyBag( [this; m] ) :> IMoney
    member this.Multiply(factor) = new Money( this.amount * factor, this.currency )
    member this.Negate() = new Money( -this.amount, this.currency)
    member this.Subtract(m: IMoney) = m.Negate().Add(this);

    // Operator overrides
    static member (+) (m1: Money, m2: Money) = m1.AddMoney(m2)
    static member (*) (m1: Money, factor) = m1.Multiply(factor)
    static member (*) (factor, m1: Money) = m1.Multiply(factor)
    static member (~-) (m: Money) = m.Negate()
    static member (-) (m1: Money, m2: Money) = m1.Subtract(m2)

and MoneyBag(contents0: Money list) =
    // Although mutable, this should only be changed in the constructor
    let mutable contents = []
    do
        let mutable map = Map.empty
        for m in contents0 do
            if m.IsZero <> true then
                match map.TryFind(m.currency) with
                    | None -> map <- map.Add(m.currency, m.amount)
                    | Some(_) as old ->
                        map <- map.Add(m.currency, m.amount + old.Value)
        for pair in map do
            contents <- new Money(pair.Value, pair.Key)::contents

    member this.Contents
        with get() = contents

    override this.Equals(anObject) =
        match anObject with
            | :? MoneyBag as bag when this.IsZero -> bag.IsZero
            | :? MoneyBag as bag -> bag.contentsEqual(contents)
            | _ ->false
    override this.GetHashCode() =
        contents.GetHashCode()
    override this.ToString() =
        contents.ToString()

    interface IMoney with
        member this.IsZero = contents.IsEmpty
        member this.Multiply(factor) = this.Multiply(factor) :> IMoney
        member this.Negate() = this.Negate() :> IMoney
        member this.Subtract(m: IMoney) = this.Subtract(m)
        member this.Add(m: IMoney) : IMoney = this.Add(m)
        member this.AddMoney(m: Money) = this.AddMoney(m) :> IMoney

    member this.IsZero = contents.IsEmpty
    member this.Multiply(factor) =
        if factor = 0 then new MoneyBag( [] )
        else new MoneyBag( [ for m in contents -> m * factor ] )
    member this.Negate() = new MoneyBag( [ for m in contents -> -m ] )
    member this.Add(m: IMoney): IMoney =
        match m with
            | :? Money as money -> this.AddMoney(money).simplify()
            | :? MoneyBag as bag -> this.AddMoneyBag(bag).simplify()
            | _ -> failwith "Unexpected Type for IMoney"
    member this.AddMoney(money: Money): MoneyBag =
        new MoneyBag( [ for m in contents -> if m.currency = money.currency then new Money(m.amount + money.amount, m.currency) else m ] )
    member this.AddMoneyBag(bag: MoneyBag): MoneyBag =
        let mutable result = bag
        for m in contents do
            result <- result.AddMoney m
        result
    member this.Subtract(money: IMoney): IMoney = this.Add( money.Negate() )

    member private self.contains(aMoney: Money) =
        List.tryFind (fun m -> m = aMoney) contents <> None            
    member private self.contentsEqual(c: Money list) =
        if c.Length <> contents.Length then false
        else List.forall (fun m -> self.contains m) c
    member private self.simplify() =
        match contents.Length with
            | 1 -> contents.Head :> IMoney
            | _ -> self :> IMoney

    static member (*) (bag: MoneyBag, factor) = bag.Multiply(factor)
    static member (*) (factor, bag: MoneyBag) = bag.Multiply(factor)
    static member (~-) (bag: MoneyBag) = bag.Negate()
    static member (+) (bag: MoneyBag, m: IMoney) = bag.Add(m)
    static member (+) (m: Money, bag: MoneyBag) = bag.AddMoney(m)
    static member (-) (bag: MoneyBag, m: IMoney) = bag.Subtract(m)
    static member (-) (m: Money, bag: MoneyBag) = m.Subtract(bag)
