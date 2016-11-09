using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PepesComing.Ui;

namespace PepesComing {

    public class Game : Microsoft.Xna.Framework.Game {
        // Random number generator
        public static readonly Random rnd = new Random();

        // Graphics
        private readonly GraphicsDeviceManager _graphicsDeviceManager;
        private SpriteBatch spriteBatch;

        // Systems
        private Camera camera;
        private Controller controller;
        private World world;
        private Sprites sprites;

        // Solver
        private WallFollower solver;

        //Frame time calculation
        private int frames;
        private double elapsedTime;
        private int fps;

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

            //Setup mouse
            Mouse.WindowHandle = Window.Handle;
            IsMouseVisible = true;
            controller = new Controller();

            //Create world
            world = new World();
            Viewport vp = _graphicsDeviceManager.GraphicsDevice.Viewport;
            camera = new Camera(ref vp);

            solver = new WallFollower(ref world);
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            sprites = new Sprites(this);
        }

        protected override void UnloadContent() {
            spriteBatch.Dispose();
        }

        protected override void Draw(GameTime gameTime) {
            base.Draw(gameTime);

            //Update fps
            frames += 1;
            Window.Title = "FPS: " + fps.ToString();

            GraphicsDevice.Clear(Color.LightSkyBlue);

            spriteBatch.Begin(transformMatrix: camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);
            for (int x = camera.Viewport.X; x <= camera.Viewport.Width + camera.Viewport.X; x++) {
                for (int y = camera.Viewport.Y; y <= camera.Viewport.Height + camera.Viewport.Y; y++) {
                    if (x >= 0 & x < World.width & y >= 0 & y < World.height) {
                        world.RenderTile(x, y, spriteBatch, sprites);
                    }
                }
            }

            Rectangle drawPositon = new Rectangle((int)solver.Mouse.position.X * 16, (int)solver.Mouse.position.Y * 16, 16, 16);
            switch (solver.Mouse.facing) {
                case Solver.compass.North:
                    spriteBatch.Draw(texture: sprites.Texture, destinationRectangle: drawPositon, sourceRectangle: Sprites.ArrowNorth, color: Color.White);
                    break;
                case Solver.compass.East:
                    spriteBatch.Draw(texture: sprites.Texture, destinationRectangle: drawPositon, sourceRectangle: Sprites.ArrowEast, color: Color.White);
                    break;
                case Solver.compass.South:
                    spriteBatch.Draw(texture: sprites.Texture, destinationRectangle: drawPositon, sourceRectangle: Sprites.ArrowSouth, color: Color.White);
                    break;
                case Solver.compass.West:
                    spriteBatch.Draw(texture: sprites.Texture, destinationRectangle: drawPositon, sourceRectangle: Sprites.ArrowWest, color: Color.White);
                    break;
            }
            spriteBatch.End();

            spriteBatch.Begin();
            Panel panel = new Panel(new Rectangle(0, 0, 200, 200), Color.WhiteSmoke);
            panel.RenderElement(_graphicsDeviceManager.GraphicsDevice, spriteBatch, sprites);
            Text text = new Text("We have the best game", new Vector2(52, 97), Color.GhostWhite);
            text.RenderElement(_graphicsDeviceManager.GraphicsDevice, spriteBatch, sprites);
            spriteBatch.End();
        }

        protected override void Update(GameTime gameTime) {
            // Generate world if it dosnt exsist
            if (!world.Generated) world.RegenerateMaze();

            // Calculate fps
            elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            if ((elapsedTime >= 1000f)) {
                fps = frames;
                frames = 0;
                elapsedTime = 0;
            }

            // Update systems
            controller.Update(Keyboard.GetState(), Mouse.GetState(Window), camera);
            camera.Update(controller, _graphicsDeviceManager.GraphicsDevice.Viewport);

            // Regenerate maze
            if (controller.RegenerateMaze) {
                world.RegenerateMaze();
                solver = new WallFollower(ref world);
                Console.Clear();
            }

            // Solve maze
            if (controller.Solve) {
                solver.Step(ref world);
                     
            }

            base.Update(gameTime);
        }
    }
}
