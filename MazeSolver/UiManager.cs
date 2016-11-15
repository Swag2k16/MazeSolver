using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PepesComing.Ui;
using System.Collections.Generic;

namespace PepesComing {
    class UiManager {

        private List<Element> elements;
        private Element sidebar;
        private Viewport prevViewport;

        private Button wallFollower;
        public bool WallFollower {
            get {
                return wallFollower.Clicked;
            }
        }
        private Button randomMouser;
        public bool RandomMouser {
            get {
                return randomMouser.Clicked;
            }
        }
        private Button pledge;
        public bool Pledge {
            get {
                return pledge.Clicked;
            }
        }
        private Button tremaux;
        public bool Tremaux {
            get {
                return tremaux.Clicked;
            }
        }
        private Button generateMaze;
        public bool GenerateMaze {
            get {
                return generateMaze.Clicked;
            }
        }

        public UiManager(Sprites sprites) {
            elements = new List<Element>();

            Text title = new Text("Maze Solver", new Rectangle(0, 0, 200, 100), Color.White, sprites);

            VerticalLayout algorithms = new VerticalLayout(new Rectangle(0, 0, 200, 430), true, 10, sprites);
            wallFollower = (new Button(new Rectangle(0, 0, 0, 0), Color.IndianRed, Color.White, "Wall Follower", sprites));
            randomMouser = (new Button(new Rectangle(0, 0, 0, 0), Color.IndianRed, Color.White, "Random Mouser", sprites));
            pledge = (new Button(new Rectangle(0, 0, 0, 0), Color.IndianRed, Color.White, "Pledge", sprites));
            tremaux = (new Button(new Rectangle(0, 0, 0, 0), Color.IndianRed, Color.White, "Tremaux", sprites));

            algorithms.AddElement(wallFollower);
            algorithms.AddElement(randomMouser);
            algorithms.AddElement(pledge);
            algorithms.AddElement(tremaux);


            generateMaze = new Button(new Rectangle(0, 0, 200, 100), Color.CadetBlue, Color.White, "Generate new maze", sprites);

            VerticalLayout layout = new VerticalLayout(new Rectangle(200, 200, 200, 300), false, 0, sprites);
            layout.AddElement(title);
            layout.AddElement(algorithms);
            layout.AddElement(generateMaze);

            Panel panel = new Panel(layout, 10, new Rectangle(200, 200, 200, 300), sprites.Grey, sprites);


            sidebar = panel;
            elements.Add(sidebar);
        }

        public void AddElement(Element element) {
            elements.Add(element);
        }

        public bool Update(Controller controller, GraphicsDevice graphics) {
            if (graphics.Viewport.Height != prevViewport.Height && graphics.Viewport.Width != prevViewport.Width) {
                sidebar.Position = new Rectangle(graphics.Viewport.Width - 300, 0, 300, graphics.Viewport.Height);
            }
            bool uiHandled = false;
            foreach (Element e in elements) {
                bool handled = e.Update(controller);
                if (handled) uiHandled = true;
            }

            prevViewport = graphics.Viewport;
            return uiHandled;
        }

        public void Render(GraphicsDevice graphics, SpriteBatch spriteBatch, Sprites sprites) {
            foreach (Element e in elements) {
                e.RenderElement(spriteBatch, sprites);
            }
        }
    }
}
