using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PepesComing.Solvers {
    class Tremaux : SolverMouse {
        private int[,] visitedGrid;

        public Tremaux(ref World world) :base(ref world) {
            FaceEmpty();
            visitedGrid = new int[World.width, World.height];
        }

        public override void Step() {
                
            if(!Done()) {
                visitedGrid[(int)mouse.position.X, (int)mouse.position.Y] += 1;

                var ahead = LookAhead(ref world);
                var left = LookLeft(ref world);
                var right = LookRight(ref world);
                var aheadVisited = visitedGrid[ahead.X, ahead.Y];
                var leftVisited = visitedGrid[left.X, left.Y];
                var rightVisited = visitedGrid[right.X, right.Y];
                Console.WriteLine("{0},{1},{2}", aheadVisited, leftVisited, rightVisited);

                List<Cell> floorList = new List<Cell>();
                if (!ahead.Type.Blocked() && aheadVisited == 0) floorList.Add(ahead);
                if (!left.Type.Blocked() && leftVisited == 0) floorList.Add(left);
                if (!right.Type.Blocked() && rightVisited == 0) floorList.Add(right);

                if (floorList.Count != 0) {
                    int randomDirection = Game.rnd.Next(0, floorList.Count);
                    mouse.position = new Vector2(floorList[randomDirection].X, floorList[randomDirection].Y);
                    _solution[(int)mouse.position.X, (int)mouse.position.Y] = true;
                    return;
                }
                
                if(floorList.Count == 0) {
                    if(visitedGrid[(int)mouse.position.X, (int)mouse.position.Y] == 1) {
                        TurnLeft();
                        TurnLeft();
                        Move();
                    }  else {
                       int min = MathHelper.Min(MathHelper.Min(rightVisited, aheadVisited), leftVisited);
                        if (!ahead.Type.Blocked() && aheadVisited == min) floorList.Add(ahead);
                        if (!left.Type.Blocked() && leftVisited == min) floorList.Add(left);
                        if (!right.Type.Blocked() && rightVisited == min) floorList.Add(right);
                        int randomDirection = Game.rnd.Next(0, floorList.Count);
                        mouse.position = new Vector2(floorList[randomDirection].X, floorList[randomDirection].Y);
                        _solution[(int)mouse.position.X, (int)mouse.position.Y] = true;
                        return;
                    }
                }

                

            }
        }

        public override bool Done() {
            return (int)Mouse.position.X == World.width - 2 && (int)Mouse.position.Y == World.height - 2;
        }
    }
}
