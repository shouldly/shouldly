' ****************************************************************
' This is free software licensed under the NUnit license. You
' may obtain a copy of the license as well as information regarding
' copyright ownership at http://nunit.org/?p=license&r=2.4.
' ****************************************************************

Namespace NUnit.Samples

    'The common interface for simple Monies and MoneyBags.
    Public Interface IMoney

        'Adds a money to this money
        Function Add(ByVal m As IMoney) As IMoney

        'Adds a simple Money to this money. This is a helper method for
        'implementing double dispatch.
        Function AddMoney(ByVal m As Money) As IMoney

        'Adds a MoneyBag to this money. This is a helper method for
        'implementing double dispatch.
        Function AddMoneyBag(ByVal s As MoneyBag) As IMoney

        'True if this money is zero.
        ReadOnly Property IsZero() As Boolean

        'Multiplies a money by the given factor.
        Function Multiply(ByVal factor As Int32) As IMoney

        'Negates this money.
        Function Negate() As IMoney

        'Subtracts a money from this money.
        Function Subtract(ByVal m As IMoney) As IMoney

    End Interface

End Namespace
