using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PepesComing.Solvers {
    class Tremaux : SolverMouse {
        private int[,] visitedGrid;

        public Tremaux(ref World world) : base(ref world) {
            FaceEmpty();
            visitedGrid = new int[World.width, World.height];
        }

        public override void Step() {
            if (!Done()) {
                bool ahead = LookAhead(ref world).Type.Blocked();
                bool left = LookLeft(ref world).Type.Blocked();
                bool right = LookRight(ref world).Type.Blocked();

                if(!ahead && left && right) {
                    visitedGrid[(int)mouse.position.X, (int)mouse.position.Y] += 1;
                    Move();
                    return;
                }

               if(ahead && !left && right) {
                    TurnLeft();
                    return;
                }

                if (ahead && left && !right) {
                    TurnRight();
                    return;
                }

                List<Cell> floorList = new List<Cell>();
                if (!ahead) floorList.Add(LookAhead(ref world));
                if (!left) floorList.Add(LookLeft(ref world));
                if (!right) floorList.Add(LookRight(ref world));

                List<Cell> floorListZero = floorList.Where(c => visitedGrid[c.X, c.Y] == 0).ToList<Cell>();
                if(floorListZero.Count != 0) {
                    floorListZero.Shuffle();
                    var chosenCell = floorListZero[0];
                    if (chosenCell.X > mouse.position.X) {
                        TurnRight();  
                    }
                    if (chosenCell.X < mouse.position.X) {
                        TurnLeft();   
                    }
                    Move();
                    visitedGrid[(int)mouse.position.X, (int)mouse.position.Y] += 1;
                    return;
                } else {
                    TurnRight();
                    TurnRight();
                    Move();
                    visitedGrid[(int)mouse.position.X, (int)mouse.position.Y] += 1;
                }
            }
            



            
        }

       
    }
}
