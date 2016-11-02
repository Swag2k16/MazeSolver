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

	public class Camera
	{
		private Viewport windowViewport;
		private decimal _zoom;
		private readonly decimal rotation;
		private Vector2 origin;
		private Vector2 position;
		private Vector2 dragStart;

		private Vector2 prevPosition;
		public int X {
			get { return (int)position.X + windowViewport.Width / 2; }
		}

		public int Y {
			get { return (int)position.Y + windowViewport.Height / 2; }
		}

		public int Width {
			get { return windowViewport.Width; }
		}

		public int Height {
			get { return windowViewport.Height; }
		}

		public decimal Zoom {
			get { return _zoom; }
		}

		public Rectangle Viewport {
			get {
				var minX = (X - Width / Zoom / 2) / 16 - 1;
				var maxX = (X + Width / Zoom / 2) / 16;
				var minY = (Y - Height / Zoom / 2) / 16 - 1;
				var maxY = (Y + Height / Zoom / 2) / 16;

				return new Rectangle((int)minX, (int)minY, (int)(maxX - minX), (int)(maxY - minY));
			}
		}

		private const float moveSpeed = 4f;

		private const float zoomSpeed = 0.1f;

		public Camera(ref Viewport viewport)
		{
			this.windowViewport = viewport;
			rotation = 0;
			_zoom = 1;
			origin = new Vector2(viewport.Width / 2f, viewport.Height / 2f);
			position = Vector2.Zero;
		}

		public Matrix GetViewMatrix()
		{
			return Matrix.CreateTranslation(new Vector3(-position, 0f)) 
                * Matrix.CreateTranslation(new Vector3(-origin, 0f)) 
                * Matrix.CreateRotationZ((float)rotation) 
                * Matrix.CreateScale((float)Zoom, (float)Zoom, 1)
                * Matrix.CreateTranslation(new Vector3(origin, 0));
		}

		public void Update(Controller controller, Viewport viewport)
		{
			//Update viewport
			this.windowViewport = viewport;
			origin = new Vector2(viewport.Width / 2f, viewport.Height / 2f);

			//Pan camera
			if (controller.CameraUp) {
				position.Y -= moveSpeed;
			}
			if (controller.CameraDown) {
				position.Y += moveSpeed;
			}
			if (controller.CameraLeft) {
				position.X -= moveSpeed;
			}
			if (controller.CameraRight) {
				position.X += moveSpeed;
			}

			//Zoom camera
			if (controller.CameraZoomIn) {
				_zoom = (decimal)MathHelper.Clamp((float)Zoom + zoomSpeed, 0.5f, 2f);

			}
			if (controller.CameraZoomOut) {
				_zoom = (decimal)MathHelper.Clamp((float)Zoom - zoomSpeed, 0.5f, 2f);
			}
		}

		public void DragCamera(Vector2 DragStart, Vector2 CurrentPosition)
		{
			this.dragStart = CurrentPosition;
			prevPosition = position;
		}

		public void Dragging(Vector2 Currentposition)
		{
			position = prevPosition + (dragStart - Currentposition) / (float)Zoom;
		}

	}
}
