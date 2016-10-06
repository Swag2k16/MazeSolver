Public Class World

    Private _rnd As Random

    Public Const rows As Integer = 100
    Public Const columns As Integer = 100

    Private Grid(rows - 1, columns - 1) As Cell

    Public Sub New()
        _rnd = New Random()

        'Create maze
        PrimsMaze()
    End Sub

    Public Function GetCell(row As Integer, column As Integer) As Cell
        Return Grid(row, column)
    End Function

    Private Function randomChar(ByRef charlist As List(Of Char))
        Dim randchar = charlist(_rnd.Next(0, charlist.Count))
        Return randchar
    End Function

    Private Function getNearCells(ByVal row As Integer, ByVal column As Integer) As List(Of Cell)
        Dim frontiers As List(Of Cell) = New List(Of Cell)

        'Up
        If row - 2 >= 0 Then
            frontiers.Add(Grid(row - 2, column))
        End If

        'Down
        If row + 2 <= rows - 1 Then
            frontiers.Add(Grid(row + 2, column))
        End If

        'Left
        If column + 2 <= columns - 1 Then
            frontiers.Add(Grid(row, column + 2))
        End If

        'Right
        If column - 2 >= 0 Then
            frontiers.Add(Grid(row, column - 2))
        End If

        Return frontiers
    End Function

    Private Function getFroutiers(ByVal row As Integer, ByVal column As Integer) As List(Of Cell)
        Dim frontiers As List(Of Cell) = getNearCells(row, column)
        'Remove all walls
        frontiers.RemoveAll(Function(c) Not c.wall)
        Return frontiers
    End Function

    Private Function getNeigbors(ByVal row As Integer, ByVal column As Integer) As List(Of Cell)
        Dim neigbors As List(Of Cell) = getNearCells(row, column)
        'Remove all walls
        neigbors.RemoveAll(Function(c) c.wall)
        Return neigbors
    End Function

    Public Sub PrimsMaze()
        'Initialize cells
        For row = 0 To rows - 1
            For column = 0 To rows - 1
                Grid(row, column) = New Cell(row, column)
            Next
        Next

        Dim r As Integer = _rnd.Next(0, rows - 1)
        Dim c As Integer = _rnd.Next(0, columns - 1)
        Grid(r, c).wall = False
        Grid(r, c).print()

        Dim frontiers As List(Of Cell) = getFroutiers(r, c)

        While frontiers.Count
            'Pick a random frountier and neigbor
            Dim frontier As Cell = frontiers(_rnd.Next(0, frontiers.Count - 1))
            Dim neigbors As List(Of Cell) = getNeigbors(frontier.Row, frontier.Column)
            Dim neigbor As Cell = neigbors(_rnd.Next(0, neigbors.Count - 1))

            'Remove wall between frountier and passage
            frontier.print()
            neigbor.print()
            If frontier.Row = neigbor.Row Then
                If frontier.Column > neigbor.Column Then
                    Grid(frontier.Row, frontier.Column - 1).wall = False
                    Console.WriteLine("1")
                ElseIf frontier.Column < neigbor.Column Then
                    Grid(frontier.Row, frontier.Column + 1).wall = False
                    Console.WriteLine("2")
                End If
            ElseIf frontier.Column = neigbor.Column Then
                If frontier.Row > neigbor.Row Then
                    Grid(frontier.Row - 1, frontier.Column).wall = False
                    Console.WriteLine("3")
                ElseIf frontier.Row < neigbor.Row Then
                    Grid(frontier.Row + 1, frontier.Column).wall = False
                    Console.WriteLine("4")
                End If
            End If

            frontier.wall = False
            frontiers.AddRange(getFroutiers(frontier.Row, frontier.Column))
            frontiers.Remove(frontier)
        End While


    End Sub

End Class
