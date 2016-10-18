Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input
Imports System.Windows.Forms
Imports System.Drawing

Public Class Game
    Inherits Microsoft.Xna.Framework.Game

    Public Shared rnd As New Random()
    Private _graphicsDeviceManager As GraphicsDeviceManager
    Private spriteBatch As SpriteBatch

    Private camera As Camera
    Private controller As Controller
    Private world As World
    Private sprites As Sprites

    'Frame time calculation
    Private frames As Integer
    Private elapsedTime As Double
    Private fps As Integer

    Public Sub New()
        Content.RootDirectory = "Content"

        'Setup window
        Me.Window.AllowUserResizing = True

        _graphicsDeviceManager = New GraphicsDeviceManager(Me)
        _graphicsDeviceManager.IsFullScreen = False
        _graphicsDeviceManager.PreferredBackBufferWidth = 1020
        _graphicsDeviceManager.PreferredBackBufferHeight = 768
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
        'Update fps
        frames += 1
        Me.Window.Title = "FPS: " + fps.ToString()

        GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.LightSkyBlue)

        spriteBatch.Begin(transformMatrix:=camera.GetViewMatrix(), samplerState:=SamplerState.PointClamp)

        Dim minRow As Integer = (camera.Y - camera.Height / 2) / 16 - 1
        Dim maxRow As Integer = (camera.Y + camera.Height / 2) / 16
        Dim minColumn As Integer = (camera.X - camera.Width / 2) / 16 - 1
        Dim maxColumn As Integer = (camera.X + camera.Width / 2) / 16

        Console.WriteLine("{0}, {1}", maxRow - minRow, maxColumn - minColumn)

        For row = minRow To maxRow
            For column = minColumn To maxColumn
                If row >= 0 And row < World.rows And column >= 0 And column < World.columns Then
                    world.RenderTile(row, column, spriteBatch, sprites)
                End If
            Next
        Next

        spriteBatch.End()
        ' Drawing code goes here

        MyBase.Draw(gameTime)
    End Sub

    Protected Overrides Sub Update(gameTime As GameTime)
        'Calculate fps
        elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds

        If (elapsedTime >= 1000.0F) Then
            fps = frames
            frames = 0
            elapsedTime = 0
        End If

        controller.Update(Keyboard.GetState(), Mouse.GetState(Me.Window))

        'Update camera
        camera.Update(controller, _graphicsDeviceManager.GraphicsDevice.Viewport)

        'Regenerate maze
        If controller.RegenerateMaze Then
            world.RegenerateMaze()
        End If

        MyBase.Update(gameTime)
    End Sub
End Class