Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input

Public Class Game
    Inherits Microsoft.Xna.Framework.Game

    Private rnd = New Random
    Private _graphicsDeviceManager As GraphicsDeviceManager
    Private spriteBatch As SpriteBatch

    Private pepe As Texture2D
    Private Harambae As Texture2D
    Private wall As Texture2D

    Private camera As Camera
    Private controller As Controller

    Private world As World

    Public Sub New()
        Content.RootDirectory = "Content"

        _graphicsDeviceManager = New GraphicsDeviceManager(Me)
        _graphicsDeviceManager.IsFullScreen = False
        _graphicsDeviceManager.PreferredBackBufferWidth = 600
        _graphicsDeviceManager.PreferredBackBufferHeight = 400
    End Sub




    Protected Overrides Sub Initialize()
        MyBase.Initialize()

        'Setup mouse
        Mouse.WindowHandle = Me.Window.Handle
        IsMouseVisible = True
        controller = New Controller()

        'Create world
        world = New World()

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
        For row = 0 To world.rows - 1
            For column = 0 To world.columns - 1
                Dim drawx = 2 * column + 5
                Dim drawy = 2 * row + 5

                If row = 0 Then
                    spriteBatch.Draw(wall, New Rectangle((drawx - 1) * scale, (drawy - 1) * scale, scale, scale), Color.White)
                    spriteBatch.Draw(wall, New Rectangle((drawx + 1) * scale, (drawy - 1) * scale, scale, scale), Color.White)
                ElseIf row = world.rows - 1 Then
                    spriteBatch.Draw(wall, New Rectangle((drawx - 1) * scale, (drawy + 1) * scale, scale, scale), Color.White)
                    spriteBatch.Draw(wall, New Rectangle((drawx + 1) * scale, (drawy + 1) * scale, scale, scale), Color.White)
                ElseIf column = 0 Then
                    spriteBatch.Draw(wall, New Rectangle((drawx - 1) * scale, (drawy + 1) * scale, scale, scale), Color.White)
                    spriteBatch.Draw(wall, New Rectangle((drawx - 1) * scale, (drawy - 1) * scale, scale, scale), Color.White)
                ElseIf column = world.columns - 1 Then
                    spriteBatch.Draw(wall, New Rectangle((drawx + 1) * scale, (drawy + 1) * scale, scale, scale), Color.White)
                    spriteBatch.Draw(wall, New Rectangle((drawx + 1) * scale, (drawy - 1) * scale, scale, scale), Color.White)
                End If

                Dim cell = world.GetCell(row, column)

                'left
                If cell.left Then
                    spriteBatch.Draw(wall, New Rectangle((drawx - 1) * scale, drawy * scale, scale, scale), Color.White)
                End If

                'right
                If cell.right Then
                    spriteBatch.Draw(wall, New Rectangle((drawx + 1) * scale, drawy * scale, scale, scale), Color.White)
                End If

                'up
                If cell.up Then
                    spriteBatch.Draw(wall, New Rectangle(drawx * scale, (drawy + 1) * scale, scale, scale), Color.White)
                End If

                'down
                If cell.down Then
                    spriteBatch.Draw(wall, New Rectangle(drawx * scale, (drawy - 1) * scale, scale, scale), Color.White)
                End If

                'top left
                If cell.left And cell.up Then
                    spriteBatch.Draw(wall, New Rectangle((drawx - 1) * scale, (drawy + 1) * scale, scale, scale), Color.White)
                End If

                'top right
                If cell.right And cell.up Then
                    spriteBatch.Draw(wall, New Rectangle((drawx + 1) * scale, (drawy + 1) * scale, scale, scale), Color.White)
                End If

                'bottom left
                If cell.left And cell.down Then
                    spriteBatch.Draw(wall, New Rectangle((drawx - 1) * scale, (drawy - 1) * scale, scale, scale), Color.White)
                End If

                'bottom right
                If cell.right And cell.down Then
                    spriteBatch.Draw(wall, New Rectangle((drawx + 1) * scale, (drawy - 1) * scale, scale, scale), Color.White)
                End If


                If row = 0 And column = 0 Then
                    spriteBatch.Draw(pepe, New Rectangle(drawx * scale, drawy * scale, scale, scale), Color.White)
                ElseIf row = world.rows - 1 And column = world.columns - 1 Then
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
        controller.Update(Keyboard.GetState(), Mouse.GetState(Me.Window))

        'Update camera
        camera.Update(controller)

        'Regenerate maze
        If controller.RegenerateMaze Then
            world.GenerateMaze()
        End If

        MyBase.Update(gameTime)
    End Sub
End Class