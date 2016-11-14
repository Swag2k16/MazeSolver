using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PepesComing.Ui;
using System.Collections.Generic;

namespace PepesComing {
    class UiManager {

        private List<Element> elements;
        private Element sidebar;

        public UiManager(Sprites sprites, GraphicsDevice graphics) {
            elements = new List<Element>();

            Text title = new Text("Maze Solver", new Rectangle(0, 0, 200, 100), Color.White, sprites, graphics);

            HoizontalLayout algoritums = new HoizontalLayout(new Rectangle(0, 0, 200, 430), true, 10, sprites, graphics);
            algoritums.AddElement(new Button(new Rectangle(0, 0, 0, 0), Color.IndianRed, Color.White, "Wall Follower", sprites, graphics));
            algoritums.AddElement(new Button(new Rectangle(0, 0, 0, 0), Color.IndianRed, Color.White, "Random Mouser", sprites, graphics));
            algoritums.AddElement(new Button(new Rectangle(0, 0, 0, 0), Color.IndianRed, Color.White, "Pledge", sprites, graphics));
            algoritums.AddElement(new Button(new Rectangle(0, 0, 0, 0), Color.IndianRed, Color.White, "Tremaux", sprites, graphics));

            Button generate = new Button(new Rectangle(0, 0, 200, 100), Color.CadetBlue, Color.White, "Generate new maze", sprites, graphics);

            HoizontalLayout layout = new HoizontalLayout(new Rectangle(200, 200, 200, 300), false, 0, sprites, graphics);
            layout.AddElement(title);
            layout.AddElement(algoritums);
            layout.AddElement(generate);

            Panel panel = new Panel(layout, 10, new Rectangle(200, 200, 200, 300), Color.SlateGray, sprites, graphics);

            
            sidebar = panel;
            elements.Add(sidebar);
        }

        public void AddElement(Element element) {
            elements.Add(element);
        }

        public bool Update(Controller controller, GraphicsDevice graphics) {
            sidebar.Position = new Rectangle(graphics.Viewport.Width - 300, 0, 300, graphics.Viewport.Height);

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
