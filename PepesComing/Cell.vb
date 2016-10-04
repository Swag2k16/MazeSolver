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
        x = x
        y = y

        left = False
        right = False
        up = False
        down = False
        visited = False
    End Sub

    Public Function getx()
        Return x
    End Function
    Public Function gety()
        Return y
    End Function
End Class
