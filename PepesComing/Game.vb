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
    Private Grid(50, 50) As Cell

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

        For x = 0 To 49
            For y = 0 To 49
                Grid(x, y) = New Cell(x, y)
            Next
        Next
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
            If column < 50 - 1 Then
                If Grid(row, column + 1).visited = False Then
                    check.Add("R")
                End If
            End If
            If row < 50 - 1 Then
                If Grid(row + 1, column).visited = False Then
                    check.Add("U")
                End If
            End If
            If check.Count > 0 Then
                history.Push(Grid(row, column))
                Dim direction = randomchar(check)
                Select Case direction
                    Case "L"
                        Grid(row, column).left = True
                        column = column - 1
                        Grid(row, column).right = True
                    Case "R"
                        Grid(row, column).right = True
                        column = column + 1
                        Grid(row, column).left = True
                    Case "U"
                        Grid(row, column).up = True
                        row = row + 1
                        Grid(row, column).down = True
                    Case "D"
                        Console.WriteLine(row)
                        Console.WriteLine(column)
                        Grid(row, column).down = True
                        row = row - 1
                        Grid(row, column).up = True
                End Select
            Else
                Dim cell = history.Pop()
                row = cell.getx()
                column = cell.gety()
            End If
        End While
        Grid(0, 0).up = True
        Grid(50 - 1, 50 - 1).down = True
    End Sub
    Private Function randomchar(ByRef charlist As List(Of Char))
        Dim randchar = charlist(rnd.next(0, charlist.Count))
        Return randchar
    End Function
    Protected Overrides Sub Initialize()
        MyBase.Initialize()
        MazeMaking()
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
        GraphicsDevice.Clear(Color.Cornsilk)

        'spriteBatch.Begin()
        'spriteBatch.Draw(pepe, New Rectangle(0 + xcounter, 0 + ycounter, 200, 200), Color.White)
        'spriteBatch.End()

        If harambe = True Then
            GraphicsDevice.Clear(Color.Cornsilk)
            spriteBatch.Begin()
            spriteBatch.Draw(Harambae, New Rectangle(0 + xcounter, 0 + ycounter, 200, 200), Color.White)
            spriteBatch.End()
        End If

        spriteBatch.Begin()
        For x = 0 To 49
            For y = 0 To 49
                Dim drawx = 2 * x - 1
                Dim drawy = 2 * y - 1
                'left
                If Grid(x, y).left Then
                    spriteBatch.Draw(wall, New Rectangle((drawx - 1) * 200, drawy * 200, 200, 200), Color.White)
                Else
                    spriteBatch.Draw(Harambae, New Rectangle((drawx - 1) * 200, drawy * 200, 200, 200), Color.White)
                End If
                'right
                If Grid(x, y).right Then
                    spriteBatch.Draw(wall, New Rectangle((drawx + 1) * 200, drawy * 200, 200, 200), Color.White)
                Else
                    spriteBatch.Draw(Harambae, New Rectangle((drawx + 1) * 200, drawy * 200, 200, 200), Color.White)
                End If
                'up
                If Grid(x, y).up Then
                    spriteBatch.Draw(wall, New Rectangle(drawx * 200, (drawy + 1) * 200, 200, 200), Color.White)
                Else
                    spriteBatch.Draw(Harambae, New Rectangle(drawx * 200, (drawy + 1) * 200, 200, 200), Color.White)
                End If
                'down
                If Grid(x, y).down Then
                    spriteBatch.Draw(wall, New Rectangle(drawx * 200, (drawy - 1) * 200, 200, 200), Color.White)
                Else
                    spriteBatch.Draw(Harambae, New Rectangle(drawx * 200, (drawy - 1) * 200, 200, 200), Color.White)
                End If
                'topleft
                If Grid(x, y).left And Grid(x, y).up Then
                    spriteBatch.Draw(wall, New Rectangle((drawx - 1) * 200, (drawy + 1) * 200, 200, 200), Color.White)
                End If
                'topright
                If Grid(x, y).right And Grid(x, y).up Then
                    spriteBatch.Draw(wall, New Rectangle((drawx + 1) * 200, (drawy + 1) * 200, 200, 200), Color.White)
                End If
                'bottomleft
                If Grid(x, y).left And Grid(x, y).down Then
                    spriteBatch.Draw(wall, New Rectangle((drawx - 1) * 200, (drawy - 1) * 200, 200, 200), Color.White)
                End If
                'bottomright
                If Grid(x, y).right And Grid(x, y).down Then
                    spriteBatch.Draw(wall, New Rectangle((drawx + 1) * 200, (drawy - 1) * 200, 200, 200), Color.White)
                End If
                spriteBatch.Draw(Harambae, New Rectangle(drawx * 200, drawy * 200, 200, 200), Color.White)
            Next
        Next
        spriteBatch.End()
        ' Drawing code goes here

        MyBase.Draw(gameTime)
    End Sub

    Protected Overrides Sub Update(gameTime As GameTime)
        Dim State As KeyboardState = Keyboard.GetState()
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
