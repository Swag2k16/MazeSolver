using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PepesComing.Ui {
    class Text : Element {
        private readonly string text;
        private readonly Vector2 position;
        private readonly Color color;

        public Text(string text, Microsoft.Xna.Framework.Vector2 position, Microsoft.Xna.Framework.Color color){
            this.text = text;
            this.position = position;
            this.color = color;
        }

        public override void RenderElement(GraphicsDevice graphics, SpriteBatch spriteBatch, Sprites sprites) {
            spriteBatch.DrawString(sprites.Font, text, position, color);
        }
    }
}
