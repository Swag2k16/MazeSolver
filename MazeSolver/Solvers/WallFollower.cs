using System.Collections.Generic;
using Microsoft.Xna.Framework;
using static PepesComing.Utils;
using System;
using PepesComing.Solvers;

namespace PepesComing {

    public class WallFollower : SolverMouse {

        public WallFollower(ref World world) : base(ref world) {
            FaceEmpty();
        }

        public override void Step() {
            if (!Done()) {
                var ahead = LookAhead(ref world);
                var left = LookLeft(ref world);

                if (!ahead.Type.Blocked() && left.Type.Blocked()) {
                    Move();
                } else if (left.Type == CellType.FLOOR) {
                    TurnLeft();
                    Move();
                } else {
                    TurnRight();
                }
            }
        }

        public override bool Done() {
            return (int)Mouse.position.X == World.width - 2 && (int)Mouse.position.Y == World.height - 2;
        }

    }
}
