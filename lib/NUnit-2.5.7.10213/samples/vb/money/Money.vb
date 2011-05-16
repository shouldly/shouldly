' ****************************************************************
' This is free software licensed under the NUnit license. You
' may obtain a copy of the license as well as information regarding
' copyright ownership at http://nunit.org/?p=license&r=2.4.
' ****************************************************************

Option Explicit On 

Namespace NUnit.Samples

    ' A Simple Money.
    Public Class Money
        Implements IMoney

        Private fAmount As Int32
        Private fCurrency As String

        ' Constructs a money from a given amount and currency.
        Public Sub New(ByVal amount As Int32, ByVal currency As String)
            Me.fAmount = amount
            Me.fCurrency = currency
        End Sub


        ' Adds a money to this money. Forwards the request
        ' to the AddMoney helper.
        Public Overloads Function Add(ByVal m As IMoney) As IMoney Implements IMoney.Add
            Return m.AddMoney(Me)
        End Function

        Public Overloads Function AddMoney(ByVal m As Money) As IMoney Implements IMoney.AddMoney
            If m.Currency.Equals(Currency) Then
                Return New Money(Amount + m.Amount, Currency)
            End If

            Return New MoneyBag(Me, m)
        End Function

        Public Function AddMoneyBag(ByVal s As MoneyBag) As IMoney Implements IMoney.AddMoneyBag
            Return s.AddMoney(Me)
        End Function

        Public ReadOnly Property Amount() As Integer
            Get
                Return fAmount
            End Get
        End Property

        Public ReadOnly Property Currency() As String
            Get
                Return fCurrency
            End Get
        End Property

        Public Overloads Overrides Function Equals(ByVal anObject As Object) As Boolean
            If IsZero And TypeOf anObject Is IMoney Then
                Dim aMoney As IMoney = anObject
                Return aMoney.IsZero
            End If

            If TypeOf anObject Is Money Then
                Dim aMoney As Money = anObject
                If (IsZero) Then
                    Return aMoney.IsZero
                End If

                Return Currency.Equals(aMoney.Currency) And Amount.Equals(aMoney.Amount)
            End If

            Return False
        End Function

        Public Overrides Function GetHashCode() As Int32
            Return fCurrency.GetHashCode() + fAmount
        End Function

        Public ReadOnly Property IsZero() As Boolean Implements IMoney.IsZero
            Get
                Return Amount.Equals(0)
            End Get
        End Property

        Public Function Multiply(ByVal factor As Integer) As IMoney Implements IMoney.Multiply

            Return New Money(Amount * factor, Currency)

        End Function

        Public Function Negate() As IMoney Implements IMoney.Negate

            Return New Money(-Amount, Currency)

        End Function

        Public Function Subtract(ByVal m As IMoney) As IMoney Implements IMoney.Subtract

            Return Add(m.Negate())

        End Function

        Public Overrides Function ToString() As String

            Return String.Format("[{0} {1}]", Amount, Currency)

        End Function

    End Class

End Namespace
