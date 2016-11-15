using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using static PepesComing.Utils;

namespace PepesComing.Solvers {
    public class RandomMouser : Solver {

        public RandomMouser(ref World world) : base(ref world) {

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
            if (!Done()) {
                var ahead = LookAhead(ref world);
                var left = LookLeft(ref world);
                var right = LookRight(ref world);

                if ((left.Type == Cell.types.WALL && right.Type == Cell.types.WALL)) {
                    if (ahead.Type == Cell.types.FLOOR) {
                        Move();
                    } else {
                        TurnLeft();
                        TurnLeft();
                    }
                } else if (ahead.Type == Cell.types.WALL && right.Type == Cell.types.WALL && left.Type == Cell.types.FLOOR) {
                    TurnLeft();
                    Move();
                } else if (ahead.Type == Cell.types.WALL && left.Type == Cell.types.WALL && right.Type == Cell.types.FLOOR) {
                    TurnRight();
                    Move();
                } else {
                    List<Cell> floorList = new List<Cell>();
                    if (ahead.Type == Cell.types.FLOOR) floorList.Add(ahead);
                    if (left.Type == Cell.types.FLOOR) floorList.Add(left);
                    if (right.Type == Cell.types.FLOOR) floorList.Add(right);

                    int randomDirection = Game.rnd.Next(0, floorList.Count - 1);
                    mouse.position = new Vector2(floorList[randomDirection].X, floorList[randomDirection].Y);
                }
            }
            return mouse;
        }

        public override bool Done() {
            return (int)Mouse.position.X == World.width - 2 && (int)Mouse.position.Y == World.height - 2;
        }

        //Gets the tile type ahead in the direction we are Mouse.facing
        private Cell LookAhead(ref World world) {
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
        private Cell LookLeft(ref World world) {
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

        //Gets the tile type to the left of the direction we are Mouse.facing
        private Cell LookRight(ref World world) {
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
