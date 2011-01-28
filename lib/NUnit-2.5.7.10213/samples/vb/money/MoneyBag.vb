' ****************************************************************
' This is free software licensed under the NUnit license. You
' may obtain a copy of the license as well as information regarding
' copyright ownership at http://nunit.org/?p=license&r=2.4.
' ****************************************************************

Option Explicit On 

Namespace NUnit.Samples

    Public Class MoneyBag
        Implements IMoney

        Private fmonies As ArrayList = New ArrayList(5)

        Private Sub New()

        End Sub

        Public Sub New(ByVal bag As Money())
            For Each m As Money In bag
                If Not m.IsZero Then
                    AppendMoney(m)
                End If
            Next
        End Sub

        Public Sub New(ByVal m1 As Money, ByVal m2 As Money)

            AppendMoney(m1)
            AppendMoney(m2)

        End Sub

        Public Sub New(ByVal m As Money, ByVal bag As MoneyBag)
            AppendMoney(m)
            AppendBag(bag)
        End Sub

        Public Sub New(ByVal m1 As MoneyBag, ByVal m2 As MoneyBag)
            AppendBag(m1)
            AppendBag(m2)
        End Sub

        Public Function Add(ByVal m As IMoney) As IMoney Implements IMoney.Add
            Return m.AddMoneyBag(Me)
        End Function

        Public Function AddMoney(ByVal m As Money) As IMoney Implements IMoney.AddMoney
            Return New MoneyBag(m, Me).Simplify
        End Function

        Public Function AddMoneyBag(ByVal s As MoneyBag) As IMoney Implements IMoney.AddMoneyBag
            Return New MoneyBag(s, Me).Simplify()
        End Function

        Private Sub AppendBag(ByVal aBag As MoneyBag)
            For Each m As Money In aBag.fmonies
                AppendMoney(m)
            Next
        End Sub

        Private Sub AppendMoney(ByVal aMoney As Money)

            Dim old As Money = FindMoney(aMoney.Currency)
            If old Is Nothing Then
                fmonies.Add(aMoney)
                Return
            End If
            fmonies.Remove(old)
            Dim sum As IMoney = old.Add(aMoney)
            If (sum.IsZero) Then
                Return
            End If
            fmonies.Add(sum)
        End Sub

        Private Function Contains(ByVal aMoney As Money) As Boolean
            Dim m As Money = FindMoney(aMoney.Currency)
            Return m.Amount.Equals(aMoney.Amount)
        End Function

        Public Overloads Overrides Function Equals(ByVal anObject As Object) As Boolean
            If IsZero Then
                If TypeOf anObject Is IMoney Then
                    Dim aMoney As IMoney = anObject
                    Return aMoney.IsZero
                End If
            End If

            If TypeOf anObject Is MoneyBag Then
                Dim aMoneyBag As MoneyBag = anObject
                If Not aMoneyBag.fmonies.Count.Equals(fmonies.Count) Then
                    Return False
                End If

                For Each m As Money In fmonies
                    If Not aMoneyBag.Contains(m) Then
                        Return False
                    End If

                    Return True
                Next
            End If

            Return False
        End Function

        Private Function FindMoney(ByVal currency As String) As Money
            For Each m As Money In fmonies
                If m.Currency.Equals(currency) Then
                    Return m
                End If
            Next

            Return Nothing
        End Function

        Public Overrides Function GetHashCode() As Int32
            Dim hash As Int32 = 0
            For Each m As Money In fmonies
                hash += m.GetHashCode()
            Next
            Return hash
        End Function

        Public ReadOnly Property IsZero() As Boolean Implements IMoney.IsZero
            Get
                Return fmonies.Count.Equals(0)
            End Get
        End Property

        Public Function Multiply(ByVal factor As Integer) As IMoney Implements IMoney.Multiply
            Dim result As New MoneyBag
            If Not factor.Equals(0) Then
                For Each m As Money In fmonies
                    result.AppendMoney(m.Multiply(factor))
                Next
            End If
            Return result
        End Function

        Public Function Negate() As IMoney Implements IMoney.Negate
            Dim result As New MoneyBag
            For Each m As Money In fmonies
                result.AppendMoney(m.Negate())
            Next
            Return result
        End Function

        Private Function Simplify() As IMoney
            If fmonies.Count.Equals(1) Then
                Return fmonies(0)
            End If
            Return Me
        End Function


        Public Function Subtract(ByVal m As IMoney) As IMoney Implements IMoney.Subtract
            Return Add(m.Negate())
        End Function
    End Class

End Namespace
