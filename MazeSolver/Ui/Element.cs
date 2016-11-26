using Microsoft.Xna.Framework.Graphics;
using System;

namespace PepesComing.Ui {
    public abstract class Element {
        private bool handleMouse;
        private bool prevDown;
        protected bool absolute;

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }


        public Element(bool handleMouse) {
            absolute = false;

            this.handleMouse = handleMouse;
        }

        public Element(int x, int y, int width, int height, bool handleMouse) {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            absolute = true;

            this.handleMouse = handleMouse;
        }

        public abstract void RenderElement(SpriteBatch spriteBatch);

        public virtual void CalculateLayout() { }

        public virtual bool Update(Controller controller) {
            // If the component accepts mouse input
            if (handleMouse) {

                // Mouse held down
                if (prevDown && controller.MouseDown) {
                    MouseDown();
                    return true;
                }

                // Mouse begin mouse down
                if (controller.MouseBeginDown &&
                    controller.MousePosition.X >= X &&
                    controller.MousePosition.Y >= Y &&
                    controller.MousePosition.X <= X + Width &&
                    controller.MousePosition.Y <= Y + Height) {

                    prevDown = true;
                    MouseDown();
                    return true;
                }

                // Mouse released
                if (prevDown && controller.MouseUp) {
                    prevDown = false;
                    MouseUp();
                    return true;
                }

                return false;

            }

            return false;
        }

        protected virtual void MouseDown() { }

        protected virtual void MouseUp() { }

    }
}
