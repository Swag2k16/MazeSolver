using Microsoft.Xna.Framework.Graphics;
using PepesComing.Ui;
using System.Collections.Generic;

namespace PepesComing {
    class UiManager {

        private List<Element> elements;

        public UiManager() {
            elements = new List<Element>();
        }

        public void AddElement(Element element) {
            elements.Add(element);
        }

        public bool Update(Controller controller) {
            bool uiHandled = false;
            foreach (Element e in elements) {
                bool handled = e.Update(controller);
                if (handled) uiHandled = true;
            }

            return uiHandled;
        }

        public void Render(GraphicsDevice graphics, SpriteBatch spriteBatch, Sprites sprites) {
            foreach (Element e in elements) {
                e.RenderElement(graphics, spriteBatch, sprites);
            }
        }
    }
}
