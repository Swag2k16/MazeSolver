Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input

Public Class Camera
    Private Viewport As Viewport
    Private position As Vector2
    Private zoom As Decimal
    Private rotation As Decimal
    Private origin As Vector2

    Private Const moveSpeed As Single = 4.0F
    Private Const zoomSpeed As Single = 0.1F

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

    Sub Update(controller As Controller)
        If controller.CameraUp Then
            position.Y -= moveSpeed
        End If
        If controller.CameraDown Then
            position.Y += moveSpeed
        End If
        If controller.CameraLeft Then
            position.X -= moveSpeed
        End If
        If controller.CameraRight Then
            position.X += moveSpeed
        End If

        If controller.CameraZoomIn Then
            zoom = MathHelper.Clamp(zoom + zoomSpeed, 0.5F, 2.0F)
        End If
        If controller.CameraZoomOut Then
            zoom = MathHelper.Clamp(zoom - zoomSpeed, 0.5F, 2.0F)
        End If
    End Sub
End Class
