using System.Collections.Generic;
using Microsoft.Xna.Framework;
using static PepesComing.Utils;
using System;

namespace PepesComing {

    public class WallFollower : Solver {

        private SolverMouse mouse;
        public SolverMouse Mouse {
            get {
                return mouse;
            }
        }

        public WallFollower(ref World world) : base(ref world) {
            mouse = new SolverMouse();
            mouse.position = new Vector2(1, 1);

            // Set Mouse.facing towards empty tile
            Cardinal<Cell> neighbors = world.GetNeigbors((int)Mouse.position.X, (int)Mouse.position.Y);
            if (neighbors.North.Type == Cell.types.FLOOR) {
                mouse.facing = compass.North;
            } else if (neighbors.East.Type == Cell.types.FLOOR) {
                mouse.facing = compass.East;
            } else if (neighbors.South.Type == Cell.types.FLOOR) {
                mouse.facing = compass.South;
            } else if (neighbors.West.Type == Cell.types.FLOOR) {
                mouse.facing = compass.West;
            }
        }

        public override SolverMouse Step() {
            Console.WriteLine("Step...");
            if (!Done()) {
                var ahead = LookAhead(ref world);
                var left = LookLeft(ref world);

                if ((ahead == Cell.types.FLOOR || ahead == Cell.types.ENDPOINT || ahead == Cell.types.START) && left == Cell.types.WALL) {
                    Move();
                } else if (left == Cell.types.FLOOR) {
                    TurnLeft();
                    Move();
                } else {
                    TurnRight();
                }
            }

            return Mouse;
        }

        public override bool Done() {
            return (int)Mouse.position.X == World.width - 2 && (int)Mouse.position.Y == World.height - 2;
        }

        //public override List<Vector2> Solve(ref World world) {
        //    Mouse.position = new Vector2(1, World.height-2);
        //    solution = new List<Vector2>();

        //    // Set Mouse.facing towards empty tile
        //    Cardinal<Cell> neighbors = world.GetNeigbors((int)Mouse.position.X, (int)Mouse.position.Y);
        //    if (neighbors.North.Type == Cell.types.FLOOR) {
        //        Mouse.facing = compass.North;
        //    } else if (neighbors.East.Type == Cell.types.FLOOR) {
        //        Mouse.facing = compass.East;
        //    } else if (neighbors.South.Type == Cell.types.FLOOR) {
        //        Mouse.facing = compass.South;
        //    } else if (neighbors.West.Type == Cell.types.FLOOR) {
        //        Mouse.facing = compass.West;
        //    }

        //    while ((int)Mouse.position.X != World.width - 2 || (int)Mouse.position.Y != 1) {
        //        var ahead = LookAhead(ref world);
        //        var left = LookLeft(ref world);

        //        if ((ahead == Cell.types.FLOOR || ahead == Cell.types.ENDPOINT || ahead == Cell.types.START) && left == Cell.types.WALL) {
        //            Move();
        //        } else if (left == Cell.types.FLOOR) {
        //            TurnLeft();
        //            Move();
        //        } else {
        //            TurnRight();
        //        }
        //    }

        //    return solution;
        //}

        //Gets the tile type ahead in the direction we are Mouse.facing
        private Cell.types LookAhead(ref World world) {
            var neighbors = world.GetNeigbors((int)Mouse.position.X, (int)Mouse.position.Y);
            switch (Mouse.facing) {
                case compass.North:
                    return neighbors.North.Type;
                case compass.South:
                    return neighbors.South.Type;
                case compass.East:
                    return neighbors.East.Type;
                case compass.West:
                    return neighbors.West.Type;
            }
            return Cell.types.WALL;
        }

        //Gets the tile type to the left of the direction we are Mouse.facing
        private Cell.types LookLeft(ref World world) {
            var neighbors = world.GetNeigbors((int)Mouse.position.X, (int)Mouse.position.Y);
            switch (Mouse.facing) {
                case compass.North:
                    return neighbors.West.Type;
                case compass.South:
                    return neighbors.East.Type;
                case compass.East:
                    return neighbors.North.Type;
                case compass.West:
                    return neighbors.South.Type;
            }
            return Cell.types.WALL;
        }

        //Move in the direction we are Mouse.facing (and add the cell to the solution
        private void Move() {
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

            _solution.Add(mouse.position);
            Console.WriteLine("{0} {1}", mouse.position.X, mouse.position.Y);
        }

        //Turn to face the left
        private void TurnLeft() {
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
        private void TurnRight() {
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
