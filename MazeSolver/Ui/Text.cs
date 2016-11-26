using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace PepesComing.Ui {
    class Text : Element {
        private string text;
        private readonly Color color;

        private Vector2 renderPosition;

        public Text(string text, int x = 0, int y = 0, Color color = Color.WHITE)
            : base(x, y, (int)Sprites.Font.MeasureString(text).X, (int)Sprites.Font.MeasureString(text).Y, false) {
            this.text = text;
            this.color = color;
        }

        public override void RenderElement(SpriteBatch spriteBatch) {
            spriteBatch.DrawString(Sprites.Font, text, renderPosition, color.Xna());
        }

        public override void CalculateLayout() {
            Vector2 textSize = Sprites.Font.MeasureString(text);

            // Handle text overflow
            if (Sprites.Font.MeasureString(text).X > Width) {
                if (Width < Sprites.Font.MeasureString("...").X) {
                    // text is to small for elipses so set text to nothing
                    Console.WriteLine("Text: Width is to small to display text");
                    text = "";
                } else {
                    // Remove characters until the text and elispe fit into the area
                    while (Width < textSize.X && text.Length > 0) {
                        text = text.Remove(text.Length - 1);
                        textSize = Sprites.Font.MeasureString(text + "...");
                    }

                    text = text + "...";
                }
            }

            // Set the render position
            float marginX = (Width - textSize.X) / 2;
            float marginY = (Height - textSize.Y) / 2;
            renderPosition = new Vector2(X + marginX, Y + marginY);
        }
    }
}
