Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input

Public Class Game1
    Inherits Game
    Private rnd = New Random
    Private _graphicsDeviceManager As GraphicsDeviceManager
    Private spriteBatch As SpriteBatch
    Private pepe As Texture2D
    Private Harambae As Texture2D
    Private wall As Texture2D
    Private Harambe As Boolean
    Private xcounter As Integer = 0
    Private ycounter As Integer = 0
    Private camera As Camera
    Private Const rowCount As Integer = 15
    Private Const columnCount As Integer = 15

    Private Grid(rowCount - 1, columnCount - 1) As Cell

    Public Sub New()
        Content.RootDirectory = "Content"

        _graphicsDeviceManager = New GraphicsDeviceManager(Me)
        _graphicsDeviceManager.IsFullScreen = False
        _graphicsDeviceManager.PreferredBackBufferWidth = 1920
        _graphicsDeviceManager.PreferredBackBufferHeight = 1080

    End Sub

    Private Sub MazeMaking()
        Dim row As Integer = 0
        Dim column As Integer = 0

        For x = 0 To rowCount - 1
            For y = 0 To columnCount - 1
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
            If column < columnCount - 1 Then
                If Grid(row, column + 1).visited = False Then
                    check.Add("R")
                End If
            End If
            If row < rowCount - 1 Then
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
        Grid(rowCount - 1, columnCount - 1).up = False
    End Sub

    Private Function randomchar(ByRef charlist As List(Of Char))
        Dim randchar = charlist(rnd.next(0, charlist.Count))
        Return randchar
    End Function

    Protected Overrides Sub Initialize()
        MyBase.Initialize()
        MazeMaking()
        camera = New Camera(_graphicsDeviceManager.GraphicsDevice.Viewport)

    End Sub

    Protected Overrides Sub LoadContent()
        ' Load assets etc in here
        spriteBatch = New SpriteBatch(GraphicsDevice)
        pepe = Content.Load(Of Texture2D)("pepe")
        Harambae = Content.Load(Of Texture2D)("harambe")
        wall = Content.Load(Of Texture2D)("Wall.png")

    End Sub

    Protected Overrides Sub UnloadContent()
        ' Unload assets etc in here
    End Sub

    Protected Overrides Sub Draw(gameTime As GameTime)
        GraphicsDevice.Clear(Color.DarkGoldenrod)


        spriteBatch.Begin(transformMatrix:=camera.GetViewMatrix())

        Dim scale As Integer = 16
        For row = 0 To rowCount - 1
            For column = 0 To columnCount - 1
                Dim drawx = 2 * column + 5
                Dim drawy = 2 * row + 5

                If row = 0 Then
                    spriteBatch.Draw(wall, New Rectangle((drawx - 1) * scale, (drawy - 1) * scale, scale, scale), Color.White)
                    spriteBatch.Draw(wall, New Rectangle((drawx + 1) * scale, (drawy - 1) * scale, scale, scale), Color.White)
                ElseIf row = rowCount - 1 Then
                    spriteBatch.Draw(wall, New Rectangle((drawx - 1) * scale, (drawy + 1) * scale, scale, scale), Color.White)
                    spriteBatch.Draw(wall, New Rectangle((drawx + 1) * scale, (drawy + 1) * scale, scale, scale), Color.White)
                ElseIf column = 0 Then
                    spriteBatch.Draw(wall, New Rectangle((drawx - 1) * scale, (drawy + 1) * scale, scale, scale), Color.White)
                    spriteBatch.Draw(wall, New Rectangle((drawx - 1) * scale, (drawy - 1) * scale, scale, scale), Color.White)
                ElseIf column = columnCount - 1 Then
                    spriteBatch.Draw(wall, New Rectangle((drawx + 1) * scale, (drawy + 1) * scale, scale, scale), Color.White)
                    spriteBatch.Draw(wall, New Rectangle((drawx + 1) * scale, (drawy - 1) * scale, scale, scale), Color.White)
                End If

                Dim left = Grid(row, column).left
                Dim right = Grid(row, column).right
                Dim up = Grid(row, column).up
                Dim down = Grid(row, column).down

                'left
                If left Then
                    spriteBatch.Draw(wall, New Rectangle((drawx - 1) * scale, drawy * scale, scale, scale), Color.White)
                End If

                'right
                If right Then
                    spriteBatch.Draw(wall, New Rectangle((drawx + 1) * scale, drawy * scale, scale, scale), Color.White)
                End If

                'up
                If up Then
                    spriteBatch.Draw(wall, New Rectangle(drawx * scale, (drawy + 1) * scale, scale, scale), Color.White)
                End If

                'down
                If down Then
                    spriteBatch.Draw(wall, New Rectangle(drawx * scale, (drawy - 1) * scale, scale, scale), Color.White)
                End If

                'top left
                If left And up Then
                    spriteBatch.Draw(wall, New Rectangle((drawx - 1) * scale, (drawy + 1) * scale, scale, scale), Color.White)
                End If

                'top right
                If right And up Then
                    spriteBatch.Draw(wall, New Rectangle((drawx + 1) * scale, (drawy + 1) * scale, scale, scale), Color.White)
                End If

                'bottom left
                If left And down Then
                    spriteBatch.Draw(wall, New Rectangle((drawx - 1) * scale, (drawy - 1) * scale, scale, scale), Color.White)
                End If

                'bottom right
                If right And down Then
                    spriteBatch.Draw(wall, New Rectangle((drawx + 1) * scale, (drawy - 1) * scale, scale, scale), Color.White)
                End If


                If row = 0 And column = 0 Then
                    spriteBatch.Draw(pepe, New Rectangle(drawx * scale, drawy * scale, scale, scale), Color.White)
                ElseIf row = rowCount - 1 And column = columnCount - 1 Then
                    spriteBatch.Draw(pepe, New Rectangle(drawx * scale, drawy * scale, scale, scale), Color.White)
                Else
                    'spriteBatch.Draw(Harambae, New Rectangle(drawx * scale, drawy * scale, scale, scale), Color.White)
                End If

            Next
        Next
        spriteBatch.End()
        ' Drawing code goes here

        MyBase.Draw(gameTime)
    End Sub

    Protected Overrides Sub Update(gameTime As GameTime)
        Dim State As KeyboardState = Keyboard.GetState()
        If State.IsKeyDown(Keys.R) Then
            MazeMaking()
        End If
        If State.IsKeyDown(Keys.Right) Then
            xcounter = xcounter + 5
        End If
        If State.IsKeyDown(Keys.Left) Then
            xcounter = xcounter - 5
        End If
        If State.IsKeyDown(Keys.Up) Then
            ycounter = ycounter - 5
        End If
        If State.IsKeyDown(Keys.Down) Then
            ycounter = ycounter + 5
        End If
        If State.IsKeyDown(Keys.F) Then
            Harambe = True
        End If
        MyBase.Update(gameTime)
    End Sub
End Class