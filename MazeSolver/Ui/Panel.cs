using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PepesComing.Ui {
    class Panel : Element {
        private readonly Color color;
        private readonly Rectangle position;

        public Panel(Rectangle position, Color color, Sprites sprites) : base(sprites) {
            this.position = position;
            this.color = color;
        }

        public override void RenderElement(GraphicsDevice graphics, SpriteBatch spriteBatch, Sprites sprites) {
            Texture2D rect = new Texture2D(graphics, position.Width, position.Height);

            Color[] data = new Color[position.Width * position.Height];
            for (int i = 0; i < data.Length; ++i) data[i] = color;
            rect.SetData(data);

            Vector2 coor = new Vector2(position.X, position.Y);
            spriteBatch.Draw(rect, coor, Color.White);
        }
    }
}
