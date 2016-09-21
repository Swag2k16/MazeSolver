Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input

Public Class Game1
    Inherits Game

    Private _graphicsDeviceManager As GraphicsDeviceManager
    Private spriteBatch As SpriteBatch
    Private pepe As Texture2D
    Private counter As Integer = 0

    Public Sub New()
        Content.RootDirectory = "Content"

        _graphicsDeviceManager = New GraphicsDeviceManager(Me)
        _graphicsDeviceManager.IsFullScreen = False

    End Sub

    Protected Overrides Sub Initialize()
        MyBase.Initialize()
    End Sub

    Protected Overrides Sub LoadContent()
        ' Load assets etc in here
        spriteBatch = New SpriteBatch(GraphicsDevice)
        pepe = Content.Load(Of Texture2D)("pepe")
    End Sub

    Protected Overrides Sub UnloadContent()
        ' Unload assets etc in here
    End Sub

    Protected Overrides Sub Draw(gameTime As GameTime)
        GraphicsDevice.Clear(Color.Cornsilk)

        spriteBatch.Begin()
        spriteBatch.Draw(pepe, New Rectangle(0 + counter, 0, 400, 400), Color.White)
        spriteBatch.End()

        ' Drawing code goes here

        MyBase.Draw(gameTime)
    End Sub

    Protected Overrides Sub Update(gameTime As GameTime)
        counter = counter + 1
        MyBase.Update(gameTime)
    End Sub
End Class
