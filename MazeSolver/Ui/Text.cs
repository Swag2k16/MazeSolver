using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PepesComing.Ui {
    class Text : Element {
        private readonly string text;
        public Vector2 position { get; set; }
        private readonly Color color;

        public Vector2 Size {
            get {
                return sprites.Font.MeasureString(text);
            }
        }

        public Text(string text, Vector2 position, Color color, Sprites sprites) : base(sprites) {
            this.text = text;
            this.position = position;
            this.color = color;
        }


        public override void RenderElement(GraphicsDevice graphics, SpriteBatch spriteBatch, Sprites sprites) {
            spriteBatch.DrawString(sprites.Font, text, position, color);
        }
    }
}
