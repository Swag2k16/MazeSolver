using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace PepesComing.Ui {
    class Button : Element {
        private readonly Color panelColor;
        private Color renderColor;
        private readonly Color textColor;
        private readonly string textString;

        public bool Clicked { get; private set; }

        private Panel panel;
        private Text text;

        private Rectangle position;
        public override Rectangle Position {
            get {
                return position;
            }
            set {
                position = value;
                text = new Text(textString, position, textColor, sprites);
                panel = new Panel(text, 0, position, sprites.Red, sprites);
            }
        }

        public Button(Rectangle position, Color color, Color textColor, string textString, Sprites sprites) : base(sprites) {
            this.panelColor = color;
            this.renderColor = color;
            this.textColor = textColor;
            this.textString = textString;
            Clicked = false;
            Position = position;
        }

        public override void RenderElement(SpriteBatch spriteBatch, Sprites sprites) {
            panel.RenderElement(spriteBatch, sprites);
        }

        public override bool Update(Controller controller) {
            // Check if button is still being pressed down
            if (Clicked && controller.MouseDown) {
                return true;
            }

            // Check if button was pressed down
            if (controller.MouseBeginDown && Utils.VectorInRectangle(position, controller.MousePosition)) {
                renderColor.R -= 30;
                renderColor.G -= 30;
                renderColor.B -= 30;
                panel = new Panel(text, 0, position, sprites.DarkRed, sprites);
                Console.WriteLine("Button Down!");
                Clicked = true;

                return true;
            }


            // Check if button was released
            if (Clicked && controller.MouseUp) {
                renderColor = panelColor;
                this.panel = new Panel(text, 0, position, sprites.Red, sprites);
                Clicked = false;

                return true;
            }



            return false;
        }
    }
}
