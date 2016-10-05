Public Class World

    Private _rnd As Random

    ReadOnly Property rows As Integer
        Get
            Return 15
        End Get
    End Property

    ReadOnly Property columns As Integer
        Get
            Return 15
        End Get
    End Property

    Private Grid(rows - 1, columns - 1) As Cell

    Public Sub New()
        _rnd = New Random()
        GenerateMaze()
    End Sub

    Public Function GetCell(row As Integer, column As Integer) As Cell
        Return Grid(row, column)
    End Function

    Private Function randomChar(ByRef charlist As List(Of Char))
        Dim randchar = charlist(_rnd.Next(0, charlist.Count))
        Return randchar
    End Function

    Public Sub GenerateMaze()
        Dim row As Integer = 0
        Dim column As Integer = 0

        For x = 0 To rows - 1
            For y = 0 To columns - 1
                Grid(x, y) = New Cell(x, y)
            Next
        Next

        Console.WriteLine("Init grid")

        Dim history As New Stack(Of Cell)
        history.Push(Grid(row, column))
        While history.Count > 0
            Grid(row, column).visited = True
            Dim check As New List(Of Char)

            If column > 0 Then
                If Grid(row, column - 1).visited = False Then
                    check.Add("L")
                End If
            End If
            If row > 0 Then
                If Grid(row - 1, column).visited = False Then
                    check.Add("D")
                End If
            End If
            If column < columns - 1 Then
                If Grid(row, column + 1).visited = False Then
                    check.Add("R")
                End If
            End If
            If row < rows - 1 Then
                If Grid(row + 1, column).visited = False Then
                    check.Add("U")
                End If
            End If
            If check.Count > 0 Then
                history.Push(Grid(row, column))
                Dim direction = randomchar(check)
                Select Case direction
                    Case "L"
                        Grid(row, column).left = False
                        column = column - 1
                        Grid(row, column).right = False
                    Case "U"
                        Grid(row, column).up = False
                        row = row + 1
                        Grid(row, column).down = False
                    Case "R"
                        Grid(row, column).right = False
                        column = column + 1
                        Grid(row, column).left = False
                    Case "D"
                        Grid(row, column).down = False
                        row = row - 1
                        Grid(row, column).up = False
                End Select
            Else
                Dim cell = history.Pop()
                row = cell.getx()
                column = cell.gety()
            End If
        End While
        Grid(0, 0).down = False
        Grid(rows - 1, columns - 1).up = False
    End Sub

End Class
