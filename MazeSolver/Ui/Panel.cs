using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PepesComing.Ui {
    class Panel : Element {
        private Color color;
        private Rectangle position;

        public Panel(Rectangle position, Color color) {
            this.position = position;
            this.color = color;
        }

        public override void RenderElement(GraphicsDevice graphics, SpriteBatch spriteBatch, Sprites sprites) {
            Texture2D rect = new Texture2D(graphics, position.Width, position.Height);

            Color[] data = new Color[position.Width * position.Height];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
            rect.SetData(data);

            Vector2 coor = new Vector2(position.X, position.Y);
            spriteBatch.Draw(rect, coor, Color.White);
        }
    }
}
