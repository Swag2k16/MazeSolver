Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input

Public Class Controller
    Private previousKeyboardState As KeyboardState
    Private keyboardState As KeyboardState
    Private mouseState As MouseState

    Private Const CAMERA_KEY_UP As Keys = Keys.W
    Private Const CAMERA_KEY_DOWN As Keys = Keys.S
    Private Const CAMERA_KEY_LEFT As Keys = Keys.A
    Private Const CAMERA_KEY_RIGHT As Keys = Keys.D
    Private Const CAMERA_KEY_ZOOM_IN As Keys = Keys.OemPlus
    Private Const CAMERA_KEY_ZOOM_OUT As Keys = Keys.OemMinus

    Private Const REGENERATE_MAZE As Keys = Keys.R

    ReadOnly Property CameraUp As Boolean
        Get
            Return keyboardState.IsKeyDown(CAMERA_KEY_UP)
        End Get
    End Property

    ReadOnly Property CameraDown As Boolean
        Get
            Return keyboardState.IsKeyDown(CAMERA_KEY_DOWN)
        End Get
    End Property

    ReadOnly Property CameraLeft As Boolean
        Get
            Return keyboardState.IsKeyDown(CAMERA_KEY_LEFT)
        End Get
    End Property

    ReadOnly Property CameraRight As Boolean
        Get
            Return keyboardState.IsKeyDown(CAMERA_KEY_RIGHT)
        End Get
    End Property

    ReadOnly Property CameraZoomIn As Boolean
        Get
            Return keyboardState.IsKeyDown(CAMERA_KEY_ZOOM_IN)
        End Get
    End Property

    ReadOnly Property CameraZoomOut As Boolean
        Get
            Return keyboardState.IsKeyDown(CAMERA_KEY_ZOOM_OUT)
        End Get
    End Property

    ReadOnly Property RegenerateMaze As Boolean
        Get
            Return previousKeyboardState.IsKeyDown(REGENERATE_MAZE) And Not keyboardState.IsKeyDown(REGENERATE_MAZE)
        End Get
    End Property

    Public Sub Update(keyboardState As KeyboardState, mouseState As MouseState)
        'Console.WriteLine("x: {0}, y: {1}, scroll {2}", mouseState.X, mouseState.Y, mouseState.ScrollWheelValue)
        Me.previousKeyboardState = Me.keyboardState
        Me.keyboardState = keyboardState
        Me.mouseState = mouseState
    End Sub


End Class
