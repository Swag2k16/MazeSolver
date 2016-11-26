using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace PepesComing.Ui
{
    public class HorizontalLayout : Element {
        private List<Element> elements;
        private Rectangle position;
        private int padding;
        private bool maximize;

        public HorizontalLayout(Rectangle position, bool maximize, int padding, Sprites sprites) : base(sprites) {
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
                int elementWidth = (this.position.Width - (elements.Count-1) * padding) / elements.Count;
                for (int i = 0; i < elements.Count; i++) {
                    int pad = i == 0 ? 0 : padding;
                    elements[i].Position = new Rectangle((position.X + (i * elementWidth)) + (pad * i), position.Y, elementWidth, position.Height);
                }
            } else {
                int totalWidth = 0;
                elements.ForEach(e => {
                    totalWidth += e.Position.Width;
                });

                int spacing = elements.Count > 1 ? (position.Width - totalWidth) / (elements.Count - 1) : 0;
                int end = position.X;
                elements.ForEach(e => {
                    e.Position = new Rectangle(end, position.Y, e.Position.Width, position.Height);
                    end += e.Position.Width + spacing;
                });
            }
        }
    }
}
