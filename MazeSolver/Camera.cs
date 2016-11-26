using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace PepesComing {

    public class Camera {
        public const float MoveSpeed = 4f;
        public const float ZoomSpeed = 0.1f;

        private Viewport windowViewport;
        public float Zoom { get; private set; }
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

        public Rectangle Viewport { get; private set; }


        public Camera(ref Viewport viewport) {
            windowViewport = viewport;
            Viewport = new Rectangle();
            Zoom = 1;
            origin = new Vector2(viewport.Width / 2f, viewport.Height / 2f);
            position = Vector2.Zero;

            dragStart = Vector2.Zero;
            prevPosition = Vector2.Zero;
        }

        public Matrix GetViewMatrix() {
            return Matrix.CreateTranslation(new Vector3(-position, 0f))
                * Matrix.CreateTranslation(new Vector3(-origin, 0f))
                * Matrix.CreateScale(Zoom, Zoom, 1)
                * Matrix.CreateTranslation(new Vector3(origin, 0));
        }

        public void Update(Controller controller, Viewport windowViewport) {
            // Update windowViewport
            this.windowViewport = windowViewport;
            origin = new Vector2(windowViewport.Width / 2f, windowViewport.Height / 2f);

            // Pan camera
            if (controller.CameraUp) {
                position.Y -= MoveSpeed;
            }
            if (controller.CameraDown) {
                position.Y += MoveSpeed;
            }
            if (controller.CameraLeft) {
                position.X -= MoveSpeed;
            }
            if (controller.CameraRight) {
                position.X += MoveSpeed;
            }

            // Zoom camera
            if (controller.CameraZoomIn) {
                Zoom = MathHelper.Clamp(Zoom + ZoomSpeed, 0.5f, 2f);
            } else if (controller.CameraZoomOut) {
                Zoom = MathHelper.Clamp(Zoom - ZoomSpeed, 0.5f, 2f);
            }

            // Drag camera
            if (controller.MouseBeginDown) {
                //dragStart = controller.MousePosition;
                //prevPosition = position;
                //Console.WriteLine("Mouse start");
            }

            if (controller.MouseDown) {
                //Console.WriteLine(dragStart);
                //Console.WriteLine(prevPosition);
                position += (controller.MouseDelta) / Zoom;
                prevPosition = controller.MousePosition;
            }

            // Update camera viewport
            float minX = (X - Width / Zoom / 2) / 16 - 1;
            float maxX = (X + Width / Zoom / 2) / 16;
            float minY = (Y - Height / Zoom / 2) / 16 - 1;
            float maxY = (Y + Height / Zoom / 2) / 16;
            Viewport = new Rectangle((int)minX, (int)minY, (int)(maxX - minX), (int)(maxY - minY));
        }

    }
}
