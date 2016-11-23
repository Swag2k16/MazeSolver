using Microsoft.Xna.Framework;
using System.Collections.Generic;

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
                    }
                } else if (ahead.Type.Blocked() && right.Type.Blocked() && !left.Type.Blocked()) {
                    TurnLeft();
                } else if (ahead.Type.Blocked() && left.Type.Blocked() && !right.Type.Blocked()) {
                    TurnRight();
                } else {
                    List<Cell> floorList = new List<Cell>();
                    if (!ahead.Type.Blocked()) floorList.Add(ahead);
                    if (!left.Type.Blocked()) floorList.Add(left);
                    if (!right.Type.Blocked()) floorList.Add(right);

                    int randomDirection = Game.rnd.Next(0, floorList.Count);
                    if (randomDirection == 1) TurnLeft();
                    if (randomDirection == 2) TurnRight(); 
                    mouse.position = new Vector2(floorList[randomDirection].X, floorList[randomDirection].Y);
                    _solution[(int)mouse.position.X,(int)mouse.position.Y] = true;
                }
            }
        }

       
    }
}
