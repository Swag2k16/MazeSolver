using System.Collections.Generic;
using Microsoft.Xna.Framework;
namespace PepesComing {
    public abstract class Solver {

        public abstract List<Vector2> Solve(ref World world);

    }
}
