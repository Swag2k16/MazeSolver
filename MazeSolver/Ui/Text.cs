using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace PepesComing.Ui {
    class Text : Element {
        private string text; 
        private readonly Color color;

        public Vector2 Size {
            get {
                return sprites.Font.MeasureString(text);
            }
        }

        private Vector2 renderPosition;
        private Rectangle position;
        public override Rectangle Position {
            get {
                return position;
            }

            protected set {
                position = value;
                Vector2 textSize = sprites.Font.MeasureString(text);

                // If text overflows its container
                if (position.Width < textSize.X) {
                    // text is to small for elipses so set text to nothing
                    if (textSize.X < sprites.Font.MeasureString("...").X) {
                        text = "";
                        return;
                    }

                    // Remove characters until the text and elispe fit into the area
                    while (position.Width < textSize.X) {
                        text = text.Remove(text.Length - 1);
                        textSize = sprites.Font.MeasureString(text + "...");
                    }

                    text = text + "...";
                }

                // Set the render position
                float marginX = (this.position.Width - textSize.X) / 2;
                float marginY = (this.position.Height - textSize.Y) / 2;
                renderPosition = new Vector2(this.position.X + marginX, this.position.Y + marginY);
            }
        }

        public Text(string text, Rectangle position, Color color, Sprites sprites, GraphicsDevice graphics) : base(sprites, graphics) {
            this.text = text;
            this.color = color;
            Position = position;
        }


        public override void RenderElement(GraphicsDevice graphics, SpriteBatch spriteBatch, Sprites sprites) {
            spriteBatch.DrawString(sprites.Font, text, renderPosition, color);
        }
    }
}
