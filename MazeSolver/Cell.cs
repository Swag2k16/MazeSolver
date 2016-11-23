using System;
using Microsoft.Xna.Framework;
namespace PepesComing {

    public static class Extensions {
        public static CellType wall = CellType.WALL;
        public static bool Blocked(this CellType cell) {
            return cell == wall;
        }
    }

    // Types of cell
    public enum CellType {
        WALL,
        FLOOR,
        START,
        ENDPOINT
    }

    public class Cell {


        public int X { get; private set; }
        public int Y { get; private set; }
        public bool Frontier { get; set; }

        public Rectangle Tile { get; private set; }
        private CellType _type;
        public CellType Type {
            get { return _type; }
            set {
                switch (value) {
                    case CellType.WALL:
                        Tile = Sprites.SandWall;
                        break;
                    case CellType.START:
                        Tile = Sprites.Start;
                        break;
                    case CellType.ENDPOINT:
                        Tile = Sprites.EndPoint;
                        break;
                    case CellType.FLOOR:
                        Tile = Sprites.SteelFloor;
                        break;
                }
                _type = value;
            }
        }

        public Cell(int x, int y) {
            X = x;
            Y = y;
            Type = CellType.WALL;
        }

        public void Print() {
            Console.WriteLine("x: {0}, y: {1}, wall: {2}", X, Y, Type);
        }
    }
}
