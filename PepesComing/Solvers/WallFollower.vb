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
        solution = New List(Of Vector2)

        ' Set facing towards empty tile
        Dim neighbors = world.GetNeigbors(position.X, position.Y)
        If neighbors.North.type = Cell.types.FLOOR Then
            facing = compass.North
        ElseIf neighbors.East.type = Cell.types.FLOOR Then
            facing = compass.East
        ElseIf neighbors.South.type = Cell.types.FLOOR Then
            facing = compass.South
        ElseIf neighbors.West.type = Cell.types.FLOOR Then
            facing = compass.West
        End If

        While position.X <> World.width - 2 Or position.Y <> 1
            Dim ahead = LookAhead(world)
            Dim left = LookLeft(world)

            If (ahead = Cell.types.FLOOR Or ahead = Cell.types.ENDPOINT) And left = Cell.types.WALL Then
                Move()
            ElseIf ahead = Cell.types.FLOOR And left = Cell.types.FLOOR Then
                TurnLeft()
                Move()
            ElseIf ahead = Cell.types.WALL Then
                'TODO: do we need this?
                TurnRight()
            End If
        End While

        Return solution
    End Function

    'Gets the tile type ahead in the direction we are facing
    Private Function LookAhead(ByRef world As World) As Cell.types
        Dim neighbors = world.GetNeigbors(position.X, position.Y)
        Select Case facing
            Case compass.North
                Return neighbors.North.type
            Case compass.South
                Return neighbors.South.type
            Case compass.East
                Return neighbors.East.type
            Case compass.West
                Return neighbors.West.type
        End Select
        Return Cell.types.WALL
    End Function

    'Gets the tile type to the left of the direction we are facing
    Private Function LookLeft(ByRef world As World) As Cell.types
        Dim neighbors = world.GetNeigbors(position.X, position.Y)
        Select Case facing
            Case compass.North
                Return neighbors.West.type
            Case compass.South
                Return neighbors.East.type
            Case compass.East
                Return neighbors.North.type
            Case compass.West
                Return neighbors.South.type
        End Select
        Return Cell.types.WALL
    End Function

    'Move in the direction we are facing (and add the cell to the solution
    Private Sub Move()
        Select Case facing
            Case compass.North
                position.Y += 1
            Case compass.South
                position.Y -= 1
            Case compass.East
                position.X += 1
            Case compass.West
                position.X -= 1
        End Select

        solution.Add(position)
    End Sub

    'Turn to face the left
    Private Sub TurnLeft()
        Select Case facing
            Case compass.North
                facing = compass.West
            Case compass.South
                facing = compass.East
            Case compass.West
                facing = compass.South
            Case compass.East
                facing = compass.North
        End Select
    End Sub

    'Turn to face the right
    Private Sub TurnRight()
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
    End Sub
End Class
