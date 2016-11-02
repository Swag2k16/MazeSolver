using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PepesComing
{

    public class Game : Microsoft.Xna.Framework.Game
	{

		public static readonly Random rnd = new Random();
		private readonly GraphicsDeviceManager _graphicsDeviceManager;

		private SpriteBatch spriteBatch;
		private Camera camera;
		private Controller controller;
		private World world;

		private Sprites sprites;

		//Frame time calculation
		private int frames;
		private double elapsedTime;

		private int fps;
		public Game()
		{
			Content.RootDirectory = "Content";

			//Setup window
			this.Window.AllowUserResizing = true;

			_graphicsDeviceManager = new GraphicsDeviceManager(this);
			_graphicsDeviceManager.IsFullScreen = false;
			_graphicsDeviceManager.PreferredBackBufferWidth = 1024;
			_graphicsDeviceManager.PreferredBackBufferHeight = 768;
		}

		protected override void Initialize()
		{
			base.Initialize();

			//Setup mouse
			Mouse.WindowHandle = this.Window.Handle;
			IsMouseVisible = true;
			controller = new Controller();

			//Create world
			world = new World();
			sprites = new Sprites(this);
            Viewport vp = _graphicsDeviceManager.GraphicsDevice.Viewport;
            camera = new Camera(ref vp);

		}

		protected override void LoadContent()
		{
			// Load assets etc in here
			spriteBatch = new SpriteBatch(GraphicsDevice);
		}

		protected override void UnloadContent()
		{
			// Unload assets etc in here
		}

		protected override void Draw(GameTime gameTime)
		{
			//Update fps
			frames += 1;
			this.Window.Title = "FPS: " + fps.ToString();

			GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.LightSkyBlue);

			spriteBatch.Begin(transformMatrix: camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);

			for (int x = camera.Viewport.X; x <= camera.Viewport.Width - camera.Viewport.X; x++) {
				for (int y = camera.Viewport.Y; y <= camera.Viewport.Height - camera.Viewport.Y; y++) {
					if (x >= 0 & x < World.width & y >= 0 & y < World.height) {
						world.RenderTile(x, y, spriteBatch, sprites);
					}
				}
			}

			spriteBatch.End();
			// Drawing code goes here

			base.Draw(gameTime);
		}

		protected override void Update(GameTime gameTime)
		{
			//Calculate fps
			elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;

			if ((elapsedTime >= 1000f)) {
				fps = frames;
				frames = 0;
				elapsedTime = 0;
			}

			controller.Update(Keyboard.GetState(), Mouse.GetState(this.Window));

			//Update camera
			controller.DragCamera(camera);
			camera.Update(controller, _graphicsDeviceManager.GraphicsDevice.Viewport);

			//Regenerate maze
			if (controller.RegenerateMaze) {
				world.RegenerateMaze();
			}
			if (controller.Solve) {
				WallFollower solver = new WallFollower();
				List<Vector2> solution = solver.Solve(ref world);
				foreach (Vector2 coord_loopVariable in solution) {
					Vector2 coord = coord_loopVariable;
					Console.WriteLine("{0} {1}", coord.X, coord.Y);
				}
			}

			base.Update(gameTime);
		}
	}
}
