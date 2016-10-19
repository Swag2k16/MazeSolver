Imports Microsoft.VisualBasic
Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input
Imports System.Threading

Public Class World

    Private frontiers As List(Of Cell)
    Public Const rows As Integer = 5011
    Public Const columns As Integer = 5011

    Private Grid(rows - 1, columns - 1) As Cell

    Private genThread As Thread

    Public Sub New()
        'Initialize cells
        For row = 0 To rows - 1
            For column = 0 To rows - 1
                Grid(row, column) = New Cell(row, column)
            Next
        Next
        Dim r As Integer = (rows - 1) / 2
        Dim c As Integer = (columns - 1) / 2

        Grid(r, c).type = Cell.types.FLOOR

        frontiers = getFrontiers(r, c)

        'Setup world gen thread
        genThread = New Thread(AddressOf PrimsMaze)
        genThread.IsBackground = True

        RegenerateMaze()
    End Sub

    Public Function GetCell(row As Integer, column As Integer) As Cell
        Debug.Assert(row >= 0 Or row < rows)
        Debug.Assert(column >= 0 Or column < columns)
        Return Grid(row, column)
    End Function

    Public Function GetTile(row As Integer, column As Integer) As Rectangle
        If row >= 0 And row < rows And column >= 0 And column < columns Then
            Return Grid(row, column).tile
        Else
            Return Sprites.NormalSandWall
        End If
    End Function

    Public Function RenderTile(row As Integer, column As Integer, spriteBatch As SpriteBatch, sprites As Sprites)
        spriteBatch.Draw(sprites.Texture, New Rectangle(column * 16, row * 16, 16, 16), GetTile(row, column), Color.White)
    End Function

    Private Function randomChar(ByRef charlist As List(Of Char))
        Dim randchar = charlist(Game.rnd.Next(0, charlist.Count))
        Return randchar
    End Function

    Private Function getNearCells(ByVal row As Integer, ByVal column As Integer) As List(Of Cell)
        Dim NearCells As List(Of Cell) = New List(Of Cell)

        'Up
        If row - 2 >= 0 Then
            NearCells.Add(Grid(row - 2, column))
        End If

        'Down
        If row + 2 <= rows - 1 Then
            NearCells.Add(Grid(row + 2, column))
        End If

        'Left
        If column + 2 <= columns - 1 Then
            NearCells.Add(Grid(row, column + 2))
        End If

        'Right
        If column - 2 >= 0 Then
            NearCells.Add(Grid(row, column - 2))
        End If

        Return NearCells
    End Function

    Private Function getFrontiers(ByVal row As Integer, ByVal column As Integer) As List(Of Cell)
        Dim frontiers As List(Of Cell) = getNearCells(row, column)
        'Remove all walls
        frontiers.RemoveAll(Function(c) Not c.type = Cell.types.WALL)
        Return frontiers
    End Function

    Private Function getNeigbors(ByVal row As Integer, ByVal column As Integer) As List(Of Cell)
        Dim neigbors As List(Of Cell) = getNearCells(row, column)
        'Remove all walls
        neigbors.RemoveAll(Function(c) c.type = Cell.types.WALL)
        Return neigbors
    End Function

    Public Sub PrimsMaze()
        While frontiers.Count > 0
            'Pick a random frountier and neigbor
            Dim frontier As Cell = frontiers(Game.rnd.Next(0, frontiers.Count - 1))
            Dim neigbors As List(Of Cell) = getNeigbors(frontier.Row, frontier.Column)
            Dim neigbor As Cell = neigbors(Game.rnd.Next(0, neigbors.Count - 1))

            'Remove wall between frountier and passage
            'frontier.print()
            'neigbor.print()
            If frontier.Row = neigbor.Row Then
                If frontier.Column > neigbor.Column Then
                    Grid(frontier.Row, frontier.Column - 1).type = Cell.types.FLOOR
                ElseIf frontier.Column < neigbor.Column Then
                    Grid(frontier.Row, frontier.Column + 1).type = Cell.types.FLOOR
                End If
            ElseIf frontier.Column = neigbor.Column Then
                If frontier.Row > neigbor.Row Then
                    Grid(frontier.Row - 1, frontier.Column).type = Cell.types.FLOOR
                ElseIf frontier.Row < neigbor.Row Then
                    Grid(frontier.Row + 1, frontier.Column).type = Cell.types.FLOOR
                End If
            End If

            frontier.type = Cell.types.FLOOR
            frontiers.AddRange(getFrontiers(frontier.Row, frontier.Column))
            frontiers.Remove(frontier)
            frontiers.RemoveAll(Function(j) Not j.type = Cell.types.WALL)
        End While
        Grid(1, 1).type = Cell.types.START
        Grid(rows - 2, columns - 2).type = Cell.types.ENDPOINT
    End Sub

    Public Sub RegenerateMaze()
        If genThread.IsAlive Then
            genThread.Abort()
        End If

        'Initialize cells
        For row = 0 To rows - 1
            For column = 0 To rows - 1
                Grid(row, column) = New Cell(row, column)
            Next
        Next
        Dim r As Integer = (rows - 1) / 2
        Dim c As Integer = (columns - 1) / 2

        Grid(r, c).type = Cell.types.FLOOR

        frontiers = getFrontiers(r, c)

        'Start maze generation
        genThread = New Thread(AddressOf PrimsMaze)
        genThread.IsBackground = True
        genThread.Start()
    End Sub
End Class
