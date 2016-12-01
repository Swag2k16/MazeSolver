using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace PepesComing.Ui {
    class Panel : Element {
        public Sprite Sprite {
            set {
                box.Sprite = value;
            }
        }
        private readonly Element element;
        private readonly int padding;
        private readonly Box box;

        public Panel(int x, int y, int width, int height, Element element, int padding, Sprite texture)
            : base(x, y, width, height, true) {
            this.element = element;
            this.padding = padding;
            box = new Box(x, y, width, height, texture);
            Sprite = texture;
        }



        public override void RenderElement(SpriteBatch spriteBatch) {
            box.RenderElement(spriteBatch);
            element.RenderElement(spriteBatch);
        }

        public override bool Update(Controller controller) {
            bool handled = false;
            if (element.Update(controller)) handled = true;
            if (box.Update(controller)) handled = true;
            if (base.Update(controller)) handled = true;
            return handled;
        }


        public override void CalculateLayout() {
            element.X = X + padding;
            element.Y = Y + padding;
            element.Width = Width - padding * 2;
            element.Height = Height - padding * 2;
            element.CalculateLayout();

            box.X = X;
            box.Y = Y;
            box.Width = Width;
            box.Height = Height;
            box.CalculateLayout();
        }
    }
}
