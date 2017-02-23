using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PepesComing.Solvers;
using static PepesComing.Utils;

namespace PepesComing {

    public class Game : Microsoft.Xna.Framework.Game {
        // Random number generator
        public static readonly Random rnd = new Random();

        // Graphics
        private readonly GraphicsDeviceManager _graphicsDeviceManager;
        private SpriteBatch spriteBatch;

        // Systems
        private Camera camera;
        private World world;
        private UiManager ui;

        // Solver
        private Solver solver;

        //Frame time calculation
        private int frames;
        private double elapsedTime;
        private int fps;

        private bool play = false;

        public Game() {
            Content.RootDirectory = "Content";

            //Setup window
            Window.AllowUserResizing = true;
            _graphicsDeviceManager = new GraphicsDeviceManager(this);
            _graphicsDeviceManager.IsFullScreen = false;
            _graphicsDeviceManager.PreferredBackBufferWidth = 1024;
            _graphicsDeviceManager.PreferredBackBufferHeight = 768;
        }

        protected override void Initialize() {
            base.Initialize();

            // Setup controller
            Microsoft.Xna.Framework.Input.Mouse.WindowHandle = Window.Handle;
            IsMouseVisible = true;

            // Create world
            world = new World();
            Viewport vp = _graphicsDeviceManager.GraphicsDevice.Viewport;
            camera = new Camera(ref vp);

            // Load sprites
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Sprites.Load(this);

            // Setup Ui
            ui = new UiManager();

            //regenerate maze window button
            ui.Regenerate.AddClickEvent(() => {
                if (solver != null) {
                    solver.Dispose();
                    solver = null;
                }
                World.width = (ui.WidthSlider.currentValue*4)+3;
                World.height = (ui.HeightSlider.currentValue*4)+3;



                world.RegenerateMaze();

            });

            // Register step buttons
            ui.Forward.AddClickEvent(() => {
                if (solver != null && !play) {
                    solver.Step();
                }
            });


            ui.Play.AddClickEvent(() => {
                play = !play;
            });

            // Register solver button handlers
            ui.WallFollower.AddClickEvent(() => {
                if (solver != null) solver.Dispose();
                solver = new WallFollower(ref world);
                if (play) solver.Start();
            });

            ui.RandomMouser.AddClickEvent(() => {
                if (solver != null) solver.Dispose();
                solver = new RandomMouser(ref world);
                if (play) solver.Start();
            });

            ui.DeadEndFilling.AddClickEvent(() => {
                if (solver != null) solver.Dispose();
                solver = new DeadEndFilling(ref world);
                if (play) solver.Start();
            });

            ui.Recursive.AddClickEvent(() => {
                if (solver != null) solver.Dispose();
                solver = new Recursive(ref world);
                if (play) solver.Start();
            });

        }

        protected override void LoadContent() {
        }

        protected override void UnloadContent() {
            spriteBatch.Dispose();
            Sprites.Dispose();
        }

        protected override void Draw(GameTime gameTime) {
            base.Draw(gameTime);

            //Update fps
            frames += 1;
            Window.Title = "FPS: " + fps.ToString();

            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.LightSkyBlue);

            spriteBatch.Begin(transformMatrix: camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);
            for (int x = camera.Viewport.X; x <= camera.Viewport.Width + camera.Viewport.X; x++) {
                for (int y = camera.Viewport.Y; y <= camera.Viewport.Height + camera.Viewport.Y; y++) {
                    if (x >= 0 & x < World.width & y >= 0 & y < World.height) {
                        if (solver != null && solver.Solution[x, y]) {
                            Rectangle drawPositon = new Rectangle(x * 16, y * 16, 16, 16);
                            spriteBatch.Draw(texture: Sprites.Red, destinationRectangle: drawPositon, color: Microsoft.Xna.Framework.Color.White);
                        } else {
                            world.RenderTile(x, y, spriteBatch);
                        }
                    }
                }
            }

            Rectangle drawPosition;

            if (solver != null && solver.GetType().IsSubclassOf(typeof(SolverMouse))) {
                SolverMouse solverMouse = (SolverMouse)solver;
                drawPosition = new Rectangle((int)solverMouse.Mouse.position.X * 16, (int)solverMouse.Mouse.position.Y * 16, 16, 16);
                switch (solverMouse.Mouse.facing) {
                    case Compass.North:
                        spriteBatch.Draw(texture: Sprites.Sheet, destinationRectangle: drawPosition, sourceRectangle: Sprites.ArrowNorth, color: Microsoft.Xna.Framework.Color.White);
                        break;
                    case Compass.East:
                        spriteBatch.Draw(texture: Sprites.Sheet, destinationRectangle: drawPosition, sourceRectangle: Sprites.ArrowEast, color: Microsoft.Xna.Framework.Color.White);
                        break;
                    case Compass.South:
                        spriteBatch.Draw(texture: Sprites.Sheet, destinationRectangle: drawPosition, sourceRectangle: Sprites.ArrowSouth, color: Microsoft.Xna.Framework.Color.White);
                        break;
                    case Compass.West:
                        spriteBatch.Draw(texture: Sprites.Sheet, destinationRectangle: drawPosition, sourceRectangle: Sprites.ArrowWest, color: Microsoft.Xna.Framework.Color.White);
                        break;
                }
            }
            spriteBatch.End();

            spriteBatch.Begin(samplerState: SamplerState.PointWrap);
            ui.Render(GraphicsDevice, spriteBatch);
            spriteBatch.End();
        }

        protected override void Update(GameTime gameTime) {
            if (Controller.Instance.Escape) {
                Exit();
            }

            // Generate world if it dosnt exsist
            if (!world.Generated) world.RegenerateMaze();

            // Calculate fps
            elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsedTime >= 1000f) {
                fps = frames;
                frames = 0;
                elapsedTime = 0;
            }

            // Update systems if window has focus
            if (IsActive) {
                Controller.Instance.Update(Keyboard.GetState(), Microsoft.Xna.Framework.Input.Mouse.GetState(Window), camera);

                if (!ui.Update(Controller.Instance, GraphicsDevice)) {
                    camera.Update(Controller.Instance, _graphicsDeviceManager.GraphicsDevice.Viewport);
                }
            }


            base.Update(gameTime);
        }

        protected override void OnActivated(object sender, EventArgs args) {
            // Update the controller when window gains focus to clear position of the mouse when the windows focus changed
            // which would cause the world to jump out of frame.
            if (Controller.Instance != null && camera != null) {
                Controller.Instance.Update(Keyboard.GetState(), Microsoft.Xna.Framework.Input.Mouse.GetState(Window), camera);
            }
            base.OnActivated(sender, args);
        }
    }
}
