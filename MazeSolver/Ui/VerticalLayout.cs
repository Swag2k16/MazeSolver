using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace PepesComing.Ui
{
    public class VerticalLayout : Element {
        private readonly List<Element> elements;
        private Rectangle position;
        private readonly int padding;
        private bool maximize;

        public VerticalLayout(Rectangle position, bool maximize, int padding, Sprites sprites) : base(sprites) {
            this.position = position;
            this.maximize = maximize;
            this.padding = padding;
            elements = new List<Element>();
        }

        public void AddElement(Element element) {
            elements.Add(element);
            RecalculateLayout();
        }

        public override Rectangle Position {
            get {
                return position;
            }

            set {
                position = value;
                RecalculateLayout();
            }
        }

        public override void RenderElement(SpriteBatch spriteBatch, Sprites sprites) {
            foreach (Element e in elements) {
                e.RenderElement(spriteBatch, sprites);
            }
        }

        public override bool Update(Controller controller) {
            bool handled = false;
            foreach (Element e in elements) {
                if (e.Update(controller)) handled = true;
            }

            return handled;
        }

        private void RecalculateLayout() {
            if (maximize) {
                int elementHeight = (this.position.Height - (elements.Count - 1) * padding) / elements.Count;

                for (int i = 0; i < elements.Count; i++) {
                    int pad = i == 0 ? 0 : padding;
                    elements[i].Position = new Rectangle(position.X, position.Y + (i * elementHeight) + (pad * i), position.Width, elementHeight);
                }
            } else {
                int totalHeight = 0;
                elements.ForEach(e => {
                    totalHeight += e.Position.Height;
                });

                int spacing = elements.Count > 1 ? (position.Height - totalHeight) / (elements.Count - 1) : 0;
                int end = position.Y;
                elements.ForEach(e => {
                    e.Position = new Rectangle(position.X, end, position.Width, e.Position.Height);
                    end += e.Position.Height + spacing;
                });
            }
        }
    }
}
