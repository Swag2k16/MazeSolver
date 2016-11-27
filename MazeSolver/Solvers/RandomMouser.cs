using Microsoft.Xna.Framework;
using System.Collections.Generic;
using static PepesComing.Utils;

namespace PepesComing.Solvers {
    public class RandomMouser : SolverMouse {

        public RandomMouser(ref World world) : base(ref world) {
            FaceEmpty();
        }

        public override void Step() {
            if (!Done()) {
                var ahead = LookAhead(ref world);
                var left = LookLeft(ref world);
                var right = LookRight(ref world);

                if (left.Type.Blocked() && right.Type.Blocked()) {
                    if (!ahead.Type.Blocked()) {
                        Move();
                    } else {
                        TurnLeft();
                        TurnLeft();
                        Move();
                    }
                } else if (ahead.Type.Blocked() && right.Type.Blocked() && !left.Type.Blocked()) {
                    TurnLeft();
                    Move();
                } else if (ahead.Type.Blocked() && left.Type.Blocked() && !right.Type.Blocked()) {
                    TurnRight();
                    Move();
                } else {
                    List<Directional<Cell>> floorList = new List<Directional<Cell>>();
                    if (!ahead.Type.Blocked()) floorList.Add(new Directional<Cell>(mouse.facing, ahead));
                    if (!left.Type.Blocked()) floorList.Add(new Directional<Cell>(mouse.facing.Left(), left));
                    if (!right.Type.Blocked()) floorList.Add(new Directional<Cell>(mouse.facing.Right(), right));

                    int randomDirection = Game.rnd.Next(0, floorList.Count);

                    mouse.position = new Vector2(floorList[randomDirection].Value.X, floorList[randomDirection].Value.Y);
                    mouse.facing = floorList[randomDirection].Direction;
                    _solution[(int)mouse.position.X, (int)mouse.position.Y] = true;
                }
            }
        }
    }
}
