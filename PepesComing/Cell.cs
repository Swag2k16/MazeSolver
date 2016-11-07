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

    public class Cell {
        // Types of cell
        public enum types {
            WALL,
            FLOOR,
            START,
            ENDPOINT
        }

        public int X { get; private set; }
        public int Y { get; private set; }
        public Rectangle Tile { get; private set; }
        public bool Frontier { get; set; }

        private types _type;
        public types Type {
            get { return _type; }
            set {
                switch (value) {
                    case types.WALL:
                        Tile = Sprites.SandWall;
                        break;
                    case types.START:
                        Tile = Sprites.Start;
                        break;
                    case types.ENDPOINT:
                        Tile = Sprites.EndPoint;
                        break;
                    case types.FLOOR:
                        Tile = Sprites.SteelFloor;
                        break;
                }
                _type = value;
            }
        }

        public Cell(int x, int y) {
            X = x;
            Y = y;
            Type = types.WALL;
        }

        public void Print() {
            Console.WriteLine("x: {0}, y: {1}, wall: {2}", X, Y, Type);
        }
    }
}