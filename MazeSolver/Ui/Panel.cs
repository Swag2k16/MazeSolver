using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PepesComing.Ui {
    class Panel : Element {
        public Sprite Sprite { private get; set; }
        private readonly Element element;
        private readonly int padding;

        public Panel(int x, int y, int width, int height, Element element, int padding, Sprite texture)
            : base(x, y, width, height, true) {
            this.element = element;
            this.padding = padding;
            Sprite = texture;
        }

        public Panel(Element element, int padding, Sprite texture)
            : base(true) {
            this.element = element;
            Sprite = texture;
        }

        public override void RenderElement(SpriteBatch spriteBatch) {
            spriteBatch.Draw(Sprite.Texture(), destinationRectangle: new Rectangle(X, Y, Width, Height), sourceRectangle: new Rectangle(0, 0, 16, 16), color: Microsoft.Xna.Framework.Color.White);
            element.RenderElement(spriteBatch);
        }

        public override bool Update(Controller controller) {
            bool handled = false;
            if (element.Update(controller)) handled = true;
            if (base.Update(controller)) handled = true;
            return handled;
        }

        public override void CalculateLayout() {
            element.X = X + padding;
            element.Y = Y + padding;
            element.Width = Width - padding * 2;
            element.Height = Height - padding * 2;
            element.CalculateLayout();
        }
    }
}
