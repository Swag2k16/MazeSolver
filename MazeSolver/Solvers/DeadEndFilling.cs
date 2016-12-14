using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PepesComing.Utils;

namespace PepesComing.Solvers {
    class DeadEndFilling : Solver {
        private readonly bool[,] filled;
        private bool[,] prevfilled;
        private readonly int endX;
        private readonly int endY;
        private bool done;

        public DeadEndFilling(ref World world) : base(ref world) {
            filled = new bool[World.width, World.height];
            prevfilled = new bool[World.width, World.height];
            endX = World.width - 2;
            endY = World.height - 2;
        }

        public override bool Done() {
            return done;
        }

        public override void Step() {
            bool changed = false;
            for (int x = 1; x < World.width - 1; x++) {
                for (int y = 1; y < World.height - 1; y++) {
                   Cardinal<Cell> neighbors = world.GetNeighbors(x, y);
                    int wallCount = neighbors.AsList().Count(c => c.Value.Type == CellType.WALL || prevfilled[c.Value.X, c.Value.Y]);
                    if (wallCount == 3 && world.GetCell(x, y).Type == CellType.FLOOR && !prevfilled[x,y]) {
                        _solution[x, y] = true;
                        filled[x, y] = true;
                        changed = true;
                    }
                }
            }
            done = changed;
            prevfilled = filled;
        }
    }
}
