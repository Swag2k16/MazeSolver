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
        _graphicsDeviceManager.PreferredBackBufferWidth = 1024
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

        For x = camera.Viewport.X To camera.Viewport.Width - camera.Viewport.X
            For y = camera.Viewport.Y To camera.Viewport.Height - camera.Viewport.Y
                If x >= 0 And x < World.width And y >= 0 And y < World.height Then
                    world.RenderTile(x, y, spriteBatch, sprites)
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
        controller.DragCamera(camera)
        camera.Update(controller, _graphicsDeviceManager.GraphicsDevice.Viewport)

        'Regenerate maze
        If controller.RegenerateMaze Then
            world.RegenerateMaze()
        End If
        If controller.Solve Then
            Dim solver As WallFollower = New WallFollower
            Dim solution As List(Of Vector2) = solver.Solve(world)
            For Each coord In solution
                Console.WriteLine("{0} {1}", coord.X, coord.Y)
            Next
        End If

        MyBase.Update(gameTime)
    End Sub
End Class