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

    Private _type As types
    Public Property type As types
        Get
            Return _type
        End Get
        Set(value As types)
            Select Case value
                Case types.WALL
                    tile = Sprites.SandWall
                Case types.START
                    tile = Sprites.Start
                Case types.ENDPOINT
                    tile = Sprites.EndPoint
                Case types.FLOOR
                    tile = Sprites.SteelFloor
            End Select
            _type = value
        End Set
    End Property

    Public Enum types
        WALL
        FLOOR
        START
        ENDPOINT
    End Enum

    Public Property frontier As Boolean

    Sub New(ByVal row As Integer, ByVal column As Integer)
        _row = row
        _column = column
        type = types.WALL
    End Sub

    Public Sub print()
        Console.WriteLine("row: {0}, column: {1}, wall: {2}", Row, Column, type)
    End Sub
End Class
