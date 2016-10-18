Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input

Public Class Sprites
    Private tileset As Texture2D

    Public Shared ReadOnly Property SandWall As Rectangle
        Get
            Dim rndint As Integer = Game.rnd.Next(1, 100)
            If rndint < 80 Then
                Return NormalSandWall
            ElseIf rndint < 90 Then
                Return DamagedSandWall1
            Else
                Return DamagedSandWall2
            End If
        End Get
    End Property

    Public Shared ReadOnly NormalSandWall As Rectangle = New Rectangle(4 * 17, 20 * 17, 16, 16)
    Public Shared ReadOnly DamagedSandWall1 As Rectangle = New Rectangle(4 * 17, 21 * 17, 16, 16)
    Public Shared ReadOnly DamagedSandWall2 As Rectangle = New Rectangle(3 * 17, 21 * 17, 16, 16)
    Public Shared ReadOnly Start As Rectangle = New Rectangle(9 * 17, 24 * 17, 16, 16)
    Public Shared ReadOnly EndPoint As Rectangle = New Rectangle(11 * 17, 24 * 17, 16, 16)
    Public Shared ReadOnly Property SteelFloor As Rectangle = New Rectangle(23 * 17, 1 * 17, 16, 16)


    Sub New(game As Game)
        tileset = game.Content.Load(Of Texture2D)("Tileset.png")
    End Sub

    Sub DrawTile(spritebatch As SpriteBatch, destination As Rectangle, tile As Rectangle)
        spritebatch.Draw(tileset, destination, tile, Color.White)
    End Sub
End Class
