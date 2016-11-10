using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PepesComing.Ui {
    class Panel : Element {
        private readonly Color color;
        private Texture2D drawRect;

        private Rectangle position;
        public override Rectangle Position {
            get {
                return position;
            }
            protected set {
                position = value;

                // Update texture
                drawRect = new Texture2D(graphics, position.Width, position.Height);
                Color[] data = new Color[position.Width * position.Height];
                for (int i = 0; i < data.Length; ++i) data[i] = color;
                drawRect.SetData(data);
            }
        }

        public Panel(Rectangle position, Color color, Sprites sprites, GraphicsDevice graphics) : base(sprites, graphics) {
            this.color = color;
            Position = position;
        }


        public override void RenderElement(GraphicsDevice graphics, SpriteBatch spriteBatch, Sprites sprites) {
            spriteBatch.Draw(drawRect, new Vector2(position.X, position.Y), Color.White);
        }
    }
}
