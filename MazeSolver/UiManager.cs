using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PepesComing.Ui;
using System.Collections.Generic;

namespace PepesComing {
    class UiManager {

        private readonly List<Element> elements;
        private readonly Element sidebar;
        private Viewport prevViewport;

        private Window window;

        private readonly Button wallFollower;
        private bool prevWallFollower = false;
        public bool WallFollower { get; private set; }

        private readonly Button randomMouser;
        private bool prevRandomMouser = false;
        public bool RandomMouser { get; private set; }

        private readonly Button tremaux;
        private bool prevTremaux = false;
        public bool Tremaux { get; private set; }

        private readonly Button recursive;
        private bool prevRecursive = false;
        public bool Recursive { get; private set; }

        private readonly Button generateMaze;
        private bool prevGenerateMaze = false;
        public bool GenerateMaze { get; private set; }

        public UiManager(Sprites sprites) {
            elements = new List<Element>();

            Text title = new Text("Maze Solver", new Rectangle(0, 0, 200, 100), Color.White, sprites);

            VerticalLayout algorithms = new VerticalLayout(new Rectangle(0, 0, 200, 430), true, 10, sprites);
            wallFollower = (new Button(new Rectangle(0, 0, 0, 0), Color.IndianRed, Color.White, "Wall Follower", sprites));
            randomMouser = (new Button(new Rectangle(0, 0, 0, 0), Color.IndianRed, Color.White, "Random Mouser", sprites));
            tremaux = (new Button(new Rectangle(0, 0, 0, 0), Color.IndianRed, Color.White, "Tremaux", sprites));
            recursive = (new Button(new Rectangle(0, 0, 0, 0), Color.IndianRed, Color.White, "Recursive", sprites));

            algorithms.AddElement(wallFollower);
            algorithms.AddElement(randomMouser);
            algorithms.AddElement(tremaux);
            algorithms.AddElement(recursive);

            generateMaze = new Button(new Rectangle(0, 0, 200, 100), Color.CadetBlue, Color.White, "Generate new maze", sprites);

            VerticalLayout layout = new VerticalLayout(new Rectangle(200, 200, 200, 300), false, 0, sprites);
            layout.AddElement(title);
            layout.AddElement(algorithms);
            layout.AddElement(generateMaze);

            Panel panel = new Panel(layout, 10, new Rectangle(200, 200, 200, 300), sprites.Grey, sprites);
            sidebar = panel;
            elements.Add(sidebar);

            window = new Window(new Rectangle(150, 200, 400, 200), sprites);
            elements.Add(window);
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

            GenerateMaze = (prevGenerateMaze == false && generateMaze.Clicked);
            WallFollower = (prevWallFollower == false && wallFollower.Clicked);
            RandomMouser = (prevRandomMouser == false && randomMouser.Clicked);
            Tremaux = (prevTremaux == false && tremaux.Clicked);
            Recursive = (prevRecursive == false && recursive.Clicked);

            if (GenerateMaze) {
                window.Show = true;
            }

            prevGenerateMaze = generateMaze.Clicked;
            prevWallFollower = wallFollower.Clicked;
            prevRandomMouser = randomMouser.Clicked;
            prevTremaux = tremaux.Clicked;
            prevRecursive = recursive.Clicked;

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
