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
                Dim drawx = column
                Dim drawy = row
                Dim cell As Cell = world.GetCell(row, column)

                If cell.wall Then
                    spriteBatch.Draw(wall, New Rectangle(drawx * scale, drawy * scale, scale, scale), Color.White)
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
            world.PrimsMaze()
        End If

        MyBase.Update(gameTime)
    End Sub
End Class