Imports Microsoft.Xna.Framework
Imports PepesComing

Public Class WallFollower
    Inherits Solver
    Private Enum compass
        North
        East
        South
        West
    End Enum
    Private facing As compass
    Private position As Vector2
    Private solution As List(Of Vector2)
    Public Overrides Function Solve(ByRef world As World) As List(Of Vector2)
        position = New Vector2(1, 1)
        facing = compass.South
        solution = New List(Of Vector2)
        While position.X <> World.columns - 2 And position.Y <> World.rows - 2
            Dim ahead = LookAhead(world)
            Dim left = LookLeft(world)
            If (ahead = Cell.types.FLOOR Or ahead = Cell.types.ENDPOINT) And left = Cell.types.WALL Then
                Move()
            ElseIf ahead = Cell.types.WALL Then
                Select Case facing
                    Case compass.North
                        facing = compass.East
                    Case compass.South
                        facing = compass.West
                    Case compass.West
                        facing = compass.North
                    Case compass.East
                        facing = compass.South
                End Select
                Move()
            End If
        End While
        Return solution
    End Function
    Private Function LookAhead(ByRef world As World) As Cell.types
        Select Case facing
            Case compass.North
                Return world.GetCell(position.Y - 1, position.X).type
            Case compass.South
                Return world.GetCell(position.Y + 1, position.X).type
            Case compass.East
                Return world.GetCell(position.Y, position.X + 1).type
            Case compass.West
                Return world.GetCell(position.Y, position.X - 1).type
        End Select
        Return Cell.types.WALL
    End Function
    Private Function LookLeft(ByRef world As World) As Cell.types
        Select Case facing
            Case compass.North
                Return world.GetCell(position.Y, position.X - 1).type
            Case compass.South
                Return world.GetCell(position.Y, position.X + 1).type
            Case compass.East
                Return world.GetCell(position.Y + 1, position.X).type
            Case compass.West
                Return world.GetCell(position.Y - 1, position.X).type
        End Select
        Return Cell.types.WALL
    End Function
    Private Sub Move()
        Select Case facing
            Case compass.North
                position.Y -= 1
            Case compass.South
                position.Y += 1
            Case compass.East
                position.X += 1
            Case compass.West
                position.X -= 1
        End Select
        solution.Add(position)
    End Sub
End Class
