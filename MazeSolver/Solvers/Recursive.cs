namespace PepesComing.Solvers {
    class Recursive : Solver {

        private readonly bool[,] wasHere;
        private readonly int endX;
        private readonly int endY;
        private bool done;

        public Recursive(ref World world) : base(ref world) {
            wasHere = new bool[World.width, World.height];
            endX = World.width - 2;
            endY = World.height - 2;
        }

        public override void Step() {
            RecursiveSolve(1, 1);
        }

        public override bool Done() {
            return done;
        }

        private bool RecursiveSolve(int x, int y) {
            if (x == endX && y == endY) {
                done = true;
                return true;
            }

            if (world.GetCell(x, y).Type.Blocked() || wasHere[x, y]) return false;
            wasHere[x, y] = true;
            // West
            if (x != 1 && RecursiveSolve(x - 1, y)) {
                _solution[x, y] = true;
                return true;
            }
            // East
            if (x != World.width - 2 && RecursiveSolve(x + 1, y)) {
                _solution[x, y] = true;
                return true;
            }
            // North
            if (y != 1 && RecursiveSolve(x, y - 1)) {
                _solution[x, y] = true;
                return true;
            }
            // South
            if (y != World.height - 2 && RecursiveSolve(x, y + 1)) {
                _solution[x, y] = true;
                return true;
            }
            return false;
        }
    }
}
