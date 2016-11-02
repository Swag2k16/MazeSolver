using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace PepesComing
{

	public class Controller
	{
		private KeyboardState previousKeyboardState;
		private KeyboardState keyboardState;
		private MouseState mouseState;

		private MouseState previousMouseState;

		private Vector2 DragStart;
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

		public bool Solve {
			get { return keyboardState.IsKeyDown(SOLVE_MAZE) & !previousKeyboardState.IsKeyDown(SOLVE_MAZE); }
		}

		public bool RegenerateMaze {
			get { return keyboardState.IsKeyDown(REGENERATE_MAZE) & !previousKeyboardState.IsKeyDown(REGENERATE_MAZE); }
		}

		public bool MouseDown {
			get { return mouseState.LeftButton == ButtonState.Pressed; }
		}

		public Vector2 MousePosition {
			get { return mouseState.Position.ToVector2(); }
		}

		public void Update(KeyboardState keyboardState, MouseState mouseState)
		{
			//Console.WriteLine("x: {0}, y: {1}, scroll {2}", mouseState.X, mouseState.Y, mouseState.ScrollWheelValue)
			this.previousKeyboardState = this.keyboardState;
			this.keyboardState = keyboardState;
			this.previousMouseState = this.mouseState;
			this.mouseState = mouseState;
		}

		public void DragCamera(Camera camera)
		{
			if (mouseState.LeftButton == ButtonState.Pressed) {
				if (previousMouseState.LeftButton == ButtonState.Released) {
					DragStart = MousePosition;
					camera.DragCamera(DragStart, MousePosition);
				}
				camera.Dragging(MousePosition);
			}
		}
	}
}
