using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PepesComing.Ui {
    class Button : Element {
        private readonly Color color;
        private readonly Color textColor;
        private readonly Rectangle position;
        private readonly string text;
        public bool Clicked { get; set; }

        public Button(Rectangle position, Color color, Color textColor, string text, Sprites sprites) : base(sprites) {
            this.color = color;
            this.textColor = textColor;
            this.position = position;
            this.text = text;
        }

        public override void RenderElement(GraphicsDevice graphics, SpriteBatch spriteBatch, Sprites sprites) {
            Color panelColor = color;
            if (Clicked){
                panelColor.R -= 60;
                panelColor.G -= 60;
                panelColor.B -= 60;
            }
            Panel panel = new Panel(position, panelColor, sprites);
            panel.RenderElement(graphics, spriteBatch, sprites);
            Text text = new Text(this.text, new Vector2(position.X, position.Y), textColor, sprites);
            Vector2 size = text.Size;
            int marginX = (position.Width - (int)size.X) / 2;
            int marginY = (position.Height - (int)size.Y) / 2;
            text.position = new Vector2(position.X + marginX, position.Y + marginY);
            text.RenderElement(graphics, spriteBatch, sprites);
        }

    }
}
