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

                // Update texture
                drawRect = new Texture2D(graphics, position.Width, position.Height);
                Color[] data = new Color[position.Width * position.Height];
                for (int i = 0; i < data.Length; ++i) data[i] = color;
                drawRect.SetData(data);

                // Update element
                element.Position = new Rectangle(position.X + padding, position.Y + padding, position.Width - 2 * padding, position.Height - 2 * padding);
            }
        }

        public Panel(Element element, int padding, Rectangle position, Color color, Sprites sprites, GraphicsDevice graphics) : base(sprites, graphics) {
            this.color = color;
            this.padding = padding;
            this.element = element;
            this.element.Position = position;
            Position = position;
        }


        public override void RenderElement(GraphicsDevice graphics, SpriteBatch spriteBatch, Sprites sprites) {
            spriteBatch.Draw(drawRect, new Vector2(position.X, position.Y), Color.White);
            element.RenderElement(graphics, spriteBatch, sprites);
        }

        public override bool Update(Controller controller) {
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

            return element.Update(controller);
        }
    }
}
