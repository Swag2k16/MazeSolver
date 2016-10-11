Imports Microsoft.VisualBasic

Public Class Cell

    Private _row As Integer

    Public ReadOnly Property Row As Integer
        Get
            Return _row
        End Get
    End Property

    Private _column As Integer

    Public ReadOnly Property Column As Integer
        Get
            Return _column
        End Get
    End Property

    Public Property wall As Boolean
    Public Property frontier As Boolean

    Sub New(ByVal row As Integer, ByVal column As Integer)
        _row = row
        _column = column
        wall = True
    End Sub

    Public Sub print()
        Console.WriteLine("row: {0}, column: {1}, wall: {2}", Row, Column, wall)
    End Sub
End Class
