using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PepesComing.Ui {
    class Button : Element {
        private readonly Sprite buttonUp;
        private readonly Sprite buttonDown;

        private readonly Panel panel;
        private readonly Text text;

        public Button(string textString, int x = 0, int y = 0, int width = 0, int height = 0, Color textColor = Color.WHITE, Sprite buttonUp = Sprite.RED, Sprite buttonDown = Sprite.DARK_RED)
            : base(x, y, width, height, true) {
            this.buttonUp = buttonUp;
            this.buttonDown = buttonDown;

            text = new Text(textString, color: textColor);
            panel = new Panel(text, 0, buttonUp);
        }

        public override void CalculateLayout() {
            panel.X = X;
            panel.Y = Y;
            panel.Width = Width;
            panel.Height = Height;

            text.X = X;
            text.Y = Y;
            text.Width = Width;
            text.Height = Height;

            text.CalculateLayout();
            panel.CalculateLayout();
        }

        public override void RenderElement(SpriteBatch spriteBatch) {
            panel.RenderElement(spriteBatch);
        }

        public delegate void HandleClick();
        private event HandleClick clickHandlers;

        public void AddClickEvent(HandleClick clickHandler) {
            clickHandlers += clickHandler;
        }

        public override bool Update(Controller controller) {
            bool handled = false;
            if (text.Update(controller)) handled = true;
            if (panel.Update(controller)) handled = true;
            if (base.Update(controller)) handled = true;
            return handled;
        }

        protected override void MouseDown() {
            panel.Sprite = buttonDown;
        }

        protected override void MouseUp() {
            panel.Sprite = buttonUp;

            // Execute click handlers
            clickHandlers.Invoke();
        }
    }
}
