Imports Microsoft.VisualBasic

Public Class Cell
    Private x As Integer
    Private y As Integer

    Public left As Boolean
    Public right As Boolean
    Public up As Boolean
    Public down As Boolean
    Public visited As Boolean

    Sub New(ByVal x As Integer, ByVal y As Integer)
        Me.x = x
        Me.y = y

        left = True
        right = True
        up = True
        down = True
        visited = False
    End Sub

    Public Function getx()
        Return x
    End Function
    Public Function gety()
        Return y
    End Function

    Public Sub print()
        Console.WriteLine("x: {0}, y: {1}, left: {2}, right: {3}, up: {4}, down: {5}, visited: {6}", x, y, left, right, up, down, visited)
    End Sub
End Class
