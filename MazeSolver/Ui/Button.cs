using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace PepesComing.Ui {
    class Button : Element {
        private readonly Color panelColor;
        private Color renderColor;
        private readonly Color textColor;
        private readonly string textString;

        private bool clicked;

        private Panel panel;
        private Text text;

        private Rectangle position;
        public override Rectangle Position {
            get {
                return position;
            }
            set {
                position = value;
                this.text = new Text(textString, position, textColor, sprites, graphics);
                this.panel = new Panel(text, 0, position, panelColor, sprites, graphics);
            }
        }

        public Button(Rectangle position, Color color, Color textColor, string textString, Sprites sprites, GraphicsDevice graphics) : base(sprites, graphics) {
            this.panelColor = color;
            this.renderColor = color;
            this.textColor = textColor;
            this.textString = textString;
            clicked = false;
            Position = position;
        }

        public override void RenderElement(GraphicsDevice graphics, SpriteBatch spriteBatch, Sprites sprites) {
            panel.RenderElement(graphics, spriteBatch, sprites);
        }

        public override bool Update(Controller controller) {
            // Check if button was pressed down
            if(controller.MouseBeginDown && Utils.VectorInRectangle(position, controller.MousePosition)) {
                renderColor.R -= 30;
                renderColor.G -= 30;
                renderColor.B -= 30;
                panel = new Panel(text, 0, position, renderColor, sprites, graphics);
                clicked = true;
           
                return true;
            }


            // Check if button was released
            if (clicked && controller.MouseUp) {
                renderColor = panelColor;
                this.panel = new Panel(text, 0, position, renderColor, sprites, graphics);
                clicked = false;

                return true;
            }

            // Check if button is still being pressed down
            if (clicked && controller.MouseDown) {
                return true;
            }
            
            return false;
        }

    }
}
