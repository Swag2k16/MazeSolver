using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PepesComing.Ui {
    class Button : Element {
        private readonly Color panelColor;
        private readonly Color textColor;
        private readonly string textString;

        public bool Clicked { get; set; }

        private Panel panel;
        private Text text;

        private Rectangle position;
        public override Rectangle Position {
            get {
                return position;
            }
            protected set {
                position = value;
                this.text = new Text(textString, position, textColor, sprites, graphics);
                this.panel = new Panel(position, panelColor, sprites, graphics);
            }
        }

        public Button(Rectangle position, Color color, Color textColor, string textString, Sprites sprites, GraphicsDevice graphics) : base(sprites, graphics) {
            this.panelColor = color;
            this.textColor = textColor;
            this.textString = textString;
            Position = position;
        }

        public override void RenderElement(GraphicsDevice graphics, SpriteBatch spriteBatch, Sprites sprites) {
            //Color panelColor = panelColor;
            //if (Clicked){
            //    panelColor.R -= 60;
            //    panelColor.G -= 60;
            //    panelColor.B -= 60;
            //}

            panel.RenderElement(graphics, spriteBatch, sprites);
            text.RenderElement(graphics, spriteBatch, sprites);
        }

    }
}
