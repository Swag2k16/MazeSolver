Imports Microsoft.VisualBasic
Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input

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
    Private _tile As Rectangle
    Public Property tile As Rectangle
        Get
            Return _tile
        End Get
        Private Set(value As Rectangle)
            _tile = value
        End Set
    End Property
    Private _wall As Boolean

    Public Property wall As Boolean
        Get
            Return _wall
        End Get
        Set(value As Boolean)
            If value = True Then
                tile = Sprites.SandWall
            Else
                tile = Sprites.SteelFloor
            End If
            _wall = value
        End Set
    End Property
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
