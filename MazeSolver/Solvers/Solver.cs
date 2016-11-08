using Microsoft.Xna.Framework;
namespace PepesComing {
    public abstract class Solver {

        //public abstract List<Vector2> Solve(ref World world);
        public abstract SolverMouse Step(ref World world);
        public enum compass {
            North,
            East,
            South,
            West
        }
        public struct SolverMouse {
            public Vector2 position;
            public compass facing;
        }
    }
}
