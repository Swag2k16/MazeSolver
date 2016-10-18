Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input

Public Class Game
    Inherits Microsoft.Xna.Framework.Game

    Public Shared rnd As New Random()
    Private _graphicsDeviceManager As GraphicsDeviceManager
    Private spriteBatch As SpriteBatch

    Private camera As Camera
    Private controller As Controller
    Private world As World
    Private sprites As Sprites

    Public Sub New()
        Content.RootDirectory = "Content"

        'Setup window
        Me.Window.AllowUserResizing = True
        _graphicsDeviceManager = New GraphicsDeviceManager(Me)
    End Sub

    Protected Overrides Sub Initialize()
        MyBase.Initialize()

        'Setup mouse
        Mouse.WindowHandle = Me.Window.Handle
        IsMouseVisible = True
        controller = New Controller()

        'Create world
        world = New World()
        sprites = New Sprites(Me)
        camera = New Camera(_graphicsDeviceManager.GraphicsDevice.Viewport)

    End Sub

    Protected Overrides Sub LoadContent()
        ' Load assets etc in here
        spriteBatch = New SpriteBatch(GraphicsDevice)
    End Sub

    Protected Overrides Sub UnloadContent()
        ' Unload assets etc in here
    End Sub

    Protected Overrides Sub Draw(gameTime As GameTime)
        GraphicsDevice.Clear(Color.LightSkyBlue)


        spriteBatch.Begin(transformMatrix:=camera.GetViewMatrix(), samplerState:=SamplerState.PointClamp)

        Dim scale As Integer = 16
        For row = 0 To world.rows - 1
            For column = 0 To world.columns - 1
                Dim drawx = column
                Dim drawy = row
                Dim cell As Cell = world.GetCell(row, column)
                sprites.DrawTile(spriteBatch, New Rectangle(drawx * scale, drawy * scale, scale, scale), cell.tile)
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
            world.RegenerateMaze()
        End If

        MyBase.Update(gameTime)
    End Sub
End Class