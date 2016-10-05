Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input
Public Class Camera
    Private Viewport As Viewport
    Private position As Vector2
    Private zoom As Decimal
    Private rotation As Decimal
    Private origin As Vector2

    Sub New(ByRef viewport As Viewport)
        Me.Viewport = viewport
        rotation = 0
        zoom = 1
        origin = New Vector2(viewport.Width / 2.0F, viewport.Height / 2.0F)
        position = Vector2.Zero
    End Sub

    Function GetViewMatrix()
        Return Matrix.CreateTranslation(New Vector3(-position, 0.0F)) *
            Matrix.CreateTranslation(New Vector3(-origin, 0.0F)) *
            Matrix.CreateRotationZ(rotation) *
            Matrix.CreateScale(zoom, zoom, 1) *
            Matrix.CreateTranslation(New Vector3(origin, 0))
    End Function
End Class
