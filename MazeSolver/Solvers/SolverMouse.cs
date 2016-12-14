using Microsoft.Xna.Framework;
using static PepesComing.Utils;

namespace PepesComing.Solvers {

    public struct Mouse {
        public Vector2 position;
        public Compass facing;
    }


    public abstract class SolverMouse : Solver {
        protected Mouse mouse;
        public Mouse Mouse {
            get {
                return mouse;
            }
        }

        public SolverMouse(ref World world) : base(ref world) {
            mouse = new Mouse();
            mouse.position = new Vector2(1, 1);
        }

        public override bool Done() {
            return (int)Mouse.position.X == World.width - 2 && (int)Mouse.position.Y == World.height - 2;
        }

        protected void FaceEmpty() {
            // Set Mouse.facing towards empty tile
            Cardinal<Cell> neighbors = world.GetNeighbors((int)Mouse.position.X, (int)Mouse.position.Y);
            if (!neighbors.North.Type.Blocked()) {
                mouse.facing = Compass.North;
            } else if (!neighbors.East.Type.Blocked()) {
                mouse.facing = Compass.East;
            } else if (!neighbors.South.Type.Blocked()) {
                mouse.facing = Compass.South;
            } else if (!neighbors.West.Type.Blocked()) {
                mouse.facing = Compass.West;
            }
        }

        //Gets the tile type ahead in the direction we are Mouse.facing
        protected Cell LookAhead(ref World world) {
            var neighbors = world.GetNeighbors((int)Mouse.position.X, (int)Mouse.position.Y);
            switch (Mouse.facing) {
                case Compass.North:
                    return neighbors.North;
                case Compass.South:
                    return neighbors.South;
                case Compass.East:
                    return neighbors.East;
                case Compass.West:
                    return neighbors.West;
            }
            return null;
        }

        //Gets the tile type to the left of the direction we are Mouse.facing
        protected Cell LookLeft(ref World world) {
            var neighbors = world.GetNeighbors((int)Mouse.position.X, (int)Mouse.position.Y);
            switch (Mouse.facing) {
                case Compass.North:
                    return neighbors.West;
                case Compass.South:
                    return neighbors.East;
                case Compass.East:
                    return neighbors.North;
                case Compass.West:
                    return neighbors.South;
            }
            return null;
        }

        //Gets the tile type to the right of the direction we are Mouse.facing
        protected Cell LookRight(ref World world) {
            var neighbors = world.GetNeighbors((int)Mouse.position.X, (int)Mouse.position.Y);
            switch (Mouse.facing) {
                case Compass.North:
                    return neighbors.East;
                case Compass.South:
                    return neighbors.West;
                case Compass.East:
                    return neighbors.South;
                case Compass.West:
                    return neighbors.North;
            }
            return null;
        }

        //Move in the direction we are Mouse.facing (and add the cell to the solution
        protected void Move() {
            switch (mouse.facing) {
                case Compass.North:
                    mouse.position.Y -= 1;
                    break;
                case Compass.South:
                    mouse.position.Y += 1;
                    break;
                case Compass.East:
                    mouse.position.X += 1;
                    break;
                case Compass.West:
                    mouse.position.X -= 1;
                    break;
            }

            _solution[(int)mouse.position.X, (int)mouse.position.Y] = true;
        }

        //Turn to face the left
        protected void TurnLeft() {
            switch (mouse.facing) {
                case Compass.North:
                    mouse.facing = Compass.West;
                    break;
                case Compass.South:
                    mouse.facing = Compass.East;
                    break;
                case Compass.West:
                    mouse.facing = Compass.South;
                    break;
                case Compass.East:
                    mouse.facing = Compass.North;
                    break;
            }
        }

        //Turn to face the right
        protected void TurnRight() {
            switch (mouse.facing) {
                case Compass.North:
                    mouse.facing = Compass.East;
                    break;
                case Compass.South:
                    mouse.facing = Compass.West;
                    break;
                case Compass.West:
                    mouse.facing = Compass.North;
                    break;
                case Compass.East:
                    mouse.facing = Compass.South;
                    break;
            }
        }
    }
}
