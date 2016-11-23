using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PepesComing.Solvers {
    class Recursive : Solver {

        private bool[,] wasHere;
        private int endX;
        private int endY;
        private bool done;

        public Recursive(ref World world) : base(ref world) {
            wasHere = new bool[World.width, World.height];
            endX = World.width - 2;
            endY = World.height - 2;
        }

        public override void Step() {
            recursiveSolve(1, 1);
        }

        public override bool Done() {
            return done;
        }

        private bool recursiveSolve(int x, int y) {
            Console.WriteLine("{0}, {1}", x, y);
            if (x == endX && y == endY) {
                done = true;
                return true;
            }
            
            if (world.GetCell(x, y).Type.Blocked() || wasHere[x, y]) return false;
            wasHere[x, y] = true;
            if (x != 1) {
                if (recursiveSolve(x-1, y)) {
                    _solution[x, y] = true;
                    return true;
                }
            }
            if (x != World.width - 2) {
                if (recursiveSolve(x+1, y)) {
                    _solution[x, y] = true;
                    return true;
                }
            }
            if (y != 1) {
                if (recursiveSolve(x, y-1)) {
                    _solution[x, y] = true;
                    return true;
                }
            }
            if (y != World.height - 2) {
                if (recursiveSolve(x, y+1)) {
                    _solution[x, y] = true;
                    return true;
                }
            }
            return false;
        }
    }
}
