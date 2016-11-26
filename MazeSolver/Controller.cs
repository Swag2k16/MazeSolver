using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace PepesComing {

    public class Controller {
        private KeyboardState previousKeyboardState;
        private KeyboardState keyboardState;
        private MouseState mouseState;
        private MouseState previousMouseState;

        private const Keys CAMERA_KEY_UP = Keys.W;
        private const Keys CAMERA_KEY_DOWN = Keys.S;
        private const Keys CAMERA_KEY_LEFT = Keys.A;
        private const Keys CAMERA_KEY_RIGHT = Keys.D;
        private const Keys CAMERA_KEY_ZOOM_IN = Keys.OemPlus;
        private const Keys CAMERA_KEY_ZOOM_OUT = Keys.OemMinus;

        private const Keys REGENERATE_MAZE = Keys.R;
        private const Keys SOLVE_MAZE = Keys.P;

        public bool CameraUp {
            get { return keyboardState.IsKeyDown(CAMERA_KEY_UP); }
        }

        public bool CameraDown {
            get { return keyboardState.IsKeyDown(CAMERA_KEY_DOWN); }
        }

        public bool CameraLeft {
            get { return keyboardState.IsKeyDown(CAMERA_KEY_LEFT); }
        }

        public bool CameraRight {
            get { return keyboardState.IsKeyDown(CAMERA_KEY_RIGHT); }
        }

        public bool CameraZoomIn {
            get { return keyboardState.IsKeyDown(CAMERA_KEY_ZOOM_IN); }
        }

        public bool CameraZoomOut {
            get { return keyboardState.IsKeyDown(CAMERA_KEY_ZOOM_OUT); }
        }

        public bool Escape {
            get { return keyboardState.IsKeyDown(Keys.Escape); }
        }

        public bool Solve {
            get { return keyboardState.IsKeyDown(SOLVE_MAZE) && !previousKeyboardState.IsKeyDown(SOLVE_MAZE); }
        }

        public bool RegenerateMaze {
            get { return keyboardState.IsKeyDown(REGENERATE_MAZE) && !previousKeyboardState.IsKeyDown(REGENERATE_MAZE); }
        }

        // Mouse down this frame (but not last frame)
        public bool MouseBeginDown {
            get { return previousMouseState.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed; }
        }

        // Mouse down this frame
        public bool MouseDown {
            get { return mouseState.LeftButton == ButtonState.Pressed; }
        }

        // Mouse up this frame
        public bool MouseUp {
            get { return previousMouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released; }
        }

        // Position of the mouse relative to the window
        public Vector2 MousePosition {
            get { return mouseState.Position.ToVector2(); }
        }

        public Vector2 MouseDelta {
            get { return previousMouseState.Position.ToVector2() - mouseState.Position.ToVector2(); }
        }

        public void Reset() {
            Console.WriteLine("Reset");
            Console.WriteLine(previousMouseState.Position);
            Console.WriteLine(mouseState.Position);
            //previousMouseState = new MouseState();
            //mouseState = new MouseState();
        }

        public void Update(KeyboardState keyboardState, MouseState mouseState, Camera camera) {
            // Update keyboard/mouse states
            this.previousKeyboardState = this.keyboardState;
            this.keyboardState = keyboardState;
            this.previousMouseState = this.mouseState;
            this.mouseState = mouseState;
        }
    }
}
