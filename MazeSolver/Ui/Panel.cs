using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PepesComing.Ui {
    class Panel : Element {
        private readonly Color color;
        private Texture2D drawRect;
        private Element element;
        private int padding;
        private bool clicked;

        private Rectangle position;
        public override Rectangle Position {
            get {
                return position;
            }
            set {
                position = value;
                element.Position = new Rectangle(position.X + padding, position.Y + padding, position.Width - 2 * padding, position.Height - 2 * padding);
            }
        }

        public Panel(Element element, int padding, Rectangle position, Texture2D texture, Sprites sprites) : base(sprites) {
            drawRect = texture;
            this.padding = padding;
            this.element = element;
            this.element.Position = position;
            Position = position;
        }


        public override void RenderElement(SpriteBatch spriteBatch, Sprites sprites) {
            spriteBatch.Draw(drawRect, destinationRectangle: position, sourceRectangle: new Rectangle(0, 0, 16, 16), color: Color.White);
            element.RenderElement(spriteBatch, sprites);
        }

        public override bool Update(Controller controller) {
            if (element.Update(controller)) {
                return true;
            }

            if (controller.MouseBeginDown && Utils.VectorInRectangle(position, controller.MousePosition)) {
                clicked = true;
                return true;
            }

            if (clicked && controller.MouseUp) {
                clicked = false;
                return true;
            }

            if (clicked && controller.MouseDown) {
                return true;
            }

            return false;
        }
    }
}
