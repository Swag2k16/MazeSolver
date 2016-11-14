using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace PepesComing {

    public class Sprites : IDisposable {
        public Texture2D Texture { get; private set; }
        public Texture2D Grey { get; private set; }
        public Texture2D Red { get; private set; }
        public SpriteFont Font { get; private set; }

        public static Rectangle SandWall {
            get {
                int rndint = Game.rnd.Next(1, 100);
                if (rndint < 80) {
                    return NormalSandWall;
                } else if (rndint < 90) {
                    return DamagedSandWall1;
                } else {
                    return DamagedSandWall2;
                }
            }
        }
        
        // Walls
        public static readonly Rectangle NormalSandWall = new Rectangle(4 * 17, 20 * 17, 16, 16);
        public static readonly Rectangle DamagedSandWall1 = new Rectangle(4 * 17, 21 * 17, 16, 16);
        public static readonly Rectangle DamagedSandWall2 = new Rectangle(3 * 17, 21 * 17, 16, 16);

        // Start/end
        public static readonly Rectangle Start = new Rectangle(9 * 17, 24 * 17, 16, 16);
        public static readonly Rectangle EndPoint = new Rectangle(11 * 17, 24 * 17, 16, 16);

        // Floors
        public static readonly Rectangle SteelFloor = new Rectangle(23 * 17, 1 * 17, 16, 16);

        // Arrows
        public static readonly Rectangle ArrowNorth = new Rectangle(19 * 17, 22 * 17, 16, 16);
        public static readonly Rectangle ArrowSouth = new Rectangle(19*17, 21 * 17, 16, 16);
        public static readonly Rectangle ArrowWest = new Rectangle(19 * 17, 23 * 17, 16, 16);
        public static readonly Rectangle ArrowEast = new Rectangle(19 * 17, 24 * 17, 16, 16);

        public Sprites(Game game) {
            Debug.Write("Loading sprites\n");
            Texture = game.Content.Load<Texture2D>("Tileset.png");
            Grey = game.Content.Load<Texture2D>("Grey.png");
            Red = game.Content.Load<Texture2D>("Red.png");
            Font = game.Content.Load<SpriteFont>("Fonts/Coders-Crux");

        }

        public void Dispose() {
            Texture.Dispose();
            Font.Texture.Dispose();
        }
    }
}
