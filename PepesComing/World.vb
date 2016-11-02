Imports Microsoft.VisualBasic
Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input
Imports System.Threading

Public Class World
    Public Const height As Integer = 123
    Public Const width As Integer = 123
    Private Grid(width - 1, height - 1) As Cell

    Private frontiers As List(Of Cell)
    Private genThread As Thread

    Public Sub New()
        'Setup world gen thread
        genThread = New Thread(AddressOf PrimsMaze)
        genThread.IsBackground = True

        'Start world generation
        RegenerateMaze()
    End Sub

    'Structure to hold cells in all four cardinal directions
    Public Structure CardinalCells
        Public North As Cell
        Public East As Cell
        Public South As Cell
        Public West As Cell
    End Structure

    'Get the cell at (x, y)
    Public Function GetCell(x As Integer, y As Integer) As Cell
        Debug.Assert(x >= 0 Or x < width)
        Debug.Assert(y >= 0 Or y < height)
        Return Grid(x, y)
    End Function

    'Get the cells directly connected to the cell at (x, y)
    Public Function GetNeigbors(x As Integer, y As Integer) As CardinalCells
        ' Check row and column are within bounds
        Debug.Assert(x >= 1 Or x < width - 1)
        Debug.Assert(y >= 1 Or y < height - 1)

        ' Get neigbors around cell
        Dim neigbors As CardinalCells = New CardinalCells
        neigbors.North = GetCell(x, y + 1)
        neigbors.East = GetCell(x + 1, y)
        neigbors.South = GetCell(x, y - 1)
        neigbors.West = GetCell(x - 1, y)

        Return neigbors
    End Function

    'Render a tile to the spritebatch
    Public Sub RenderTile(x As Integer, y As Integer, spriteBatch As SpriteBatch, sprites As Sprites)
        spriteBatch.Draw(sprites.Texture, New Rectangle(x * 16, -y * 16, 16, 16), GetCell(x, y).tile, Color.White)
    End Sub

    'Get cells (if any) two units away (in the cardinal directions)
    Private Function getNearCells(ByVal x As Integer, ByVal y As Integer) As List(Of Cell)
        Dim NearCells As List(Of Cell) = New List(Of Cell)

        'Up
        If y <= height - 3 Then
            NearCells.Add(Grid(x, y + 2))
        End If
        'Down
        If y >= 2 Then
            NearCells.Add(Grid(x, y - 2))
        End If
        'Left
        If x >= 2 Then
            NearCells.Add(Grid(x - 2, y))
        End If
        'Right
        If x <= width - 3 Then
            NearCells.Add(Grid(x + 2, y))
        End If

        Return NearCells
    End Function

    'Gets cells (if any) two units away (in the cardinal directions) that are walls
    Private Function getFrontiers(ByVal x As Integer, ByVal y As Integer) As List(Of Cell)
        Dim frontiers As List(Of Cell) = getNearCells(x, y)
        'Remove all walls
        frontiers.RemoveAll(Function(c) Not c.type = Cell.types.WALL)
        Return frontiers
    End Function

    'Gets cells (if any) two units away (in the cardinal directions) that are not walls (paths/start/endpoint)
    Private Function getNeighbors(ByVal x As Integer, ByVal y As Integer) As List(Of Cell)
        Dim neighbors As List(Of Cell) = getNearCells(x, y)
        'Remove all non-walls
        neighbors.RemoveAll(Function(c) c.type = Cell.types.WALL)
        Return neighbors
    End Function

    'Prims maze generation algoritum (blocking)
    Private Sub primsMaze()
        'Start the maze generation at the center
        Dim middleX = (width - 1) / 2
        Dim middleY = (height - 1) / 2
        Grid(middleX, middleY).type = Cell.types.FLOOR

        'Get initial frountiers
        frontiers = getFrontiers(middleX, middleY)

        While frontiers.Count > 0
            'Pick a random frountier and neigbhor
            Dim frontier As Cell = frontiers(Game.rnd.Next(0, frontiers.Count - 1))
            Dim neighbors As List(Of Cell) = getNeighbors(frontier.X, frontier.Y)
            Dim neighbor As Cell = neighbors(Game.rnd.Next(0, neighbors.Count - 1))

            'Make a path between the cell and the frountier
            If frontier.Y = neighbor.Y Then
                If frontier.X > neighbor.X Then
                    Grid(frontier.X - 1, frontier.Y).type = Cell.types.FLOOR
                ElseIf frontier.x < neighbor.x Then
                    Grid(frontier.X + 1, frontier.Y).type = Cell.types.FLOOR
                End If
            ElseIf frontier.x = neighbor.x Then
                If frontier.Y > neighbor.Y Then
                    Grid(frontier.X, frontier.Y - 1).type = Cell.types.FLOOR
                ElseIf frontier.y < neighbor.y Then
                    Grid(frontier.X, frontier.Y + 1).type = Cell.types.FLOOR
                End If
            End If
            frontier.type = Cell.types.FLOOR

            'Update the froutier list
            frontiers.AddRange(getFrontiers(frontier.X, frontier.Y))
            frontiers.Remove(frontier)

            'TODO: is this nessisary?
            frontiers.RemoveAll(Function(j) Not j.type = Cell.types.WALL)
        End While

        'Set the start and end points
        Grid(1, 1).type = Cell.types.START
        Grid(width - 2, height - 2).type = Cell.types.ENDPOINT
    End Sub

    'Maze (re)generation (non-blocking)
    Public Sub RegenerateMaze()
        'Stop thread if its still alive
        If genThread.IsAlive Then
            genThread.Abort()
        End If

        '(Re)initialize cells
        For x = 0 To width - 1
            For y = 0 To height - 1
                Grid(x, y) = New Cell(x, y)
            Next
        Next

        'Start maze generation
        genThread = New Thread(AddressOf PrimsMaze)
        genThread.IsBackground = True
        genThread.Start()
    End Sub
End Class
