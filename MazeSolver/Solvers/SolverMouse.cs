using Microsoft.Xna.Framework;
using static PepesComing.Utils;

namespace PepesComing.Solvers {
    public enum compass {
        North,
        East,
        South,
        West
    }

    public struct Mouse {
        public Vector2 position;
        public compass facing;
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

		protected void FaceEmpty() {
            // Set Mouse.facing towards empty tile
            Cardinal<Cell> neighbors = world.GetNeigbors((int)Mouse.position.X, (int)Mouse.position.Y);
            if (!neighbors.North.Type.Blocked()) {
                mouse.facing = compass.North;
            } else if (!neighbors.East.Type.Blocked()) {
                mouse.facing = compass.East;
            } else if (!neighbors.South.Type.Blocked()) {
                mouse.facing = compass.South;
            } else if (!neighbors.West.Type.Blocked()) {
                mouse.facing = compass.West;
            }
        }

        //Gets the tile type ahead in the direction we are Mouse.facing
        protected Cell LookAhead(ref World world) {
            var neighbors = world.GetNeigbors((int)Mouse.position.X, (int)Mouse.position.Y);
            switch (Mouse.facing) {
                case compass.North:
                    return neighbors.North;
                case compass.South:
                    return neighbors.South;
                case compass.East:
                    return neighbors.East;
                case compass.West:
                    return neighbors.West;
            }
            return null;
        }

        //Gets the tile type to the left of the direction we are Mouse.facing
        protected Cell LookLeft(ref World world) {
            var neighbors = world.GetNeigbors((int)Mouse.position.X, (int)Mouse.position.Y);
            switch (Mouse.facing) {
                case compass.North:
                    return neighbors.West;
                case compass.South:
                    return neighbors.East;
                case compass.East:
                    return neighbors.North;
                case compass.West:
                    return neighbors.South;
            }
            return null;
        }

        //Gets the tile type to the right of the direction we are Mouse.facing
        protected Cell LookRight(ref World world) {
            var neighbors = world.GetNeigbors((int)Mouse.position.X, (int)Mouse.position.Y);
            switch (Mouse.facing) {
                case compass.North:
                    return neighbors.East;
                case compass.South:
                    return neighbors.West;
                case compass.East:
                    return neighbors.South;
                case compass.West:
                    return neighbors.North;
            }
            return null;
        }

        //Move in the direction we are Mouse.facing (and add the cell to the solution
        protected void Move() {
            switch (mouse.facing) {
                case compass.North:
                    mouse.position.Y += 1;
                    break;
                case compass.South:
                    mouse.position.Y -= 1;
                    break;
                case compass.East:
                    mouse.position.X += 1;
                    break;
                case compass.West:
                    mouse.position.X -= 1;
                    break;
            }

            _solution[(int)mouse.position.X, (int)mouse.position.Y] = true;
        }

        //Turn to face the left
        protected void TurnLeft() {
            switch (mouse.facing) {
                case compass.North:
                    mouse.facing = compass.West;
                    break;
                case compass.South:
                    mouse.facing = compass.East;
                    break;
                case compass.West:
                    mouse.facing = compass.South;
                    break;
                case compass.East:
                    mouse.facing = compass.North;
                    break;
            }
        }

        //Turn to face the right
        protected void TurnRight() {
            switch (mouse.facing) {
                case compass.North:
                    mouse.facing = compass.East;
                    break;
                case compass.South:
                    mouse.facing = compass.West;
                    break;
                case compass.West:
                    mouse.facing = compass.North;
                    break;
                case compass.East:
                    mouse.facing = compass.South;
                    break;
            }
        }

    }
}
