using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PepesComing.Ui;
using System;
using System.Collections.Generic;

namespace PepesComing {
    class UiManager {

        private readonly List<Element> elements;
        private readonly Element sidebar;
        private Viewport prevViewport;

        //private Window window;

        public Button WallFollower { get; private set; }
        public Button RandomMouser { get; private set; }
        public Button DeadEndFilling { get; private set; }
        public Button Recursive { get; private set; }
        public Button GenerateMaze { get; private set; }

        public ToggleButton Play { get; private set; }
        public Button Forward { get; private set; }

        public UiManager() {
            elements = new List<Element>();

            Text title = new Text("Maze Solver");

            // Algorithums
            VerticalLayout algorithms = new VerticalLayout(height: 430, padding: 10);
            WallFollower = new Button("Wall Follower");
            RandomMouser = new Button("Random Mouser");
            DeadEndFilling = new Button("Dead-End Filling");
            Recursive = new Button("Recursive");
            algorithms.AddElements(WallFollower, RandomMouser, DeadEndFilling, Recursive);

            GenerateMaze = new Button("Generate new maze", height: 100);

          
            Play = new ToggleButton("Step", "Play");
            Forward = new Button(">");
            HorizontalLayout stepControl = new HorizontalLayout(height: 100, padding: 10);
            stepControl.AddElements(Play, Forward);

            VerticalLayout layout = new VerticalLayout(maximize: false, padding: 10);
            layout.AddElements(title, algorithms, GenerateMaze, stepControl);

            sidebar = new Panel(0, 0, 300, 100, layout, 10, Sprite.GREY);
            elements.Add(sidebar);

            Slider widthSlider = new Slider();
            Text widthText = new Text("Width: ");
            HorizontalLayout widthLayout = new HorizontalLayout();
            widthLayout.AddElements(widthText, widthSlider);

            Slider heighSlider = new Slider();
            Text heightText = new Text("Height: ");
            HorizontalLayout heightLayout = new HorizontalLayout();
            heightLayout.AddElements(heightText, heighSlider);

            VerticalLayout windowLayout = new VerticalLayout(padding: 50, maximize: false);
            windowLayout.AddElements(heightLayout, widthLayout);


            //Window window = new Window(200, 200, 500, 500, "Test", windowLayout);
            //elements.Add(window);

            elements.ForEach(e => e.CalculateLayout());
        }

        public bool Update(Controller controller, GraphicsDevice graphics) {
            if (graphics.Viewport.Height != prevViewport.Height ||
                graphics.Viewport.Width != prevViewport.Width) {

                sidebar.X = graphics.Viewport.Width - sidebar.Width;
                sidebar.Height = graphics.Viewport.Height;
                sidebar.CalculateLayout();
            }

            bool handled = false;
            foreach (Element e in elements) {
                if (e.Update()) handled = true;
            }

            prevViewport = graphics.Viewport;
            return handled;
        }

        public void Render(GraphicsDevice graphics, SpriteBatch spriteBatch) {
            foreach (Element e in elements) {
                e.RenderElement(spriteBatch);
            }
        }
    }
}
