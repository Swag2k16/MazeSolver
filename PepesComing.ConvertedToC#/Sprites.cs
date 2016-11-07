using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace PepesComing {

    public class Sprites {
        public Texture2D Texture { get; private set; }

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

        public Sprites(Game game) {
            Texture = game.Content.Load<Texture2D>("Tileset.png");
        }
    }
}
