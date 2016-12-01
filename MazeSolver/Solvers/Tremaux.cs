using System.Collections.Generic;
using System.Linq;
using static PepesComing.Utils;

namespace PepesComing.Solvers
{
    class Tremaux : SolverMouse {
        private enum mark {
            None, X, N
        }

        private int[,] visitedGrid;
        private Cardinal<mark>[,] junctions;
        private bool forwards = true;

        public Tremaux(ref World world) : base(ref world) {
            FaceEmpty();
            visitedGrid = new int[World.width, World.height];
            junctions = new Cardinal<mark>[World.width, World.height];
        }


        public override void Step() {
            if (!Done()) {
                visitedGrid[(int)mouse.position.X, (int)mouse.position.Y] += 1;

                bool aheadBlocked = LookAhead(ref world).Type.Blocked();
                bool leftBlocked = LookLeft(ref world).Type.Blocked();
                bool rightBlocked = LookRight(ref world).Type.Blocked();

                if (!aheadBlocked && leftBlocked && rightBlocked) {
                    Move();
                    return;
                }

                if (aheadBlocked && !leftBlocked && rightBlocked) {
                    TurnLeft();
                    Move();
                    return;
                }

                if (aheadBlocked && leftBlocked && !rightBlocked) {
                    TurnRight();
                    Move();
                    return;
                }



                List<Directional<mark>> junctionMarks = junctions[(int)mouse.position.X, (int)mouse.position.Y].AsList();

                if (forwards) {
                    // Dead end
                    if (aheadBlocked && leftBlocked && rightBlocked) {
                        TurnLeft();
                        TurnLeft();
                        Move();
                        forwards = false;
                        return;
                    }


                    // New juction mark and move
                    if (junctionMarks.Count(j => j.Value == mark.None) == 4) {
                        // Mark exit and entrance
                        junctions[(int)mouse.position.X, (int)mouse.position.Y] = new Cardinal<mark>();
                        junctions[(int)mouse.position.X, (int)mouse.position.Y].Set(mouse.facing, mark.X);
                        Compass entrance = RandomDirection(aheadBlocked, rightBlocked, leftBlocked);
                        junctions[(int)mouse.position.X, (int)mouse.position.Y].Set(entrance, mark.N);

                        mouse.facing = entrance;
                        Move();
                    } else {
                        // Mark exit and turn around
                        junctions[(int)mouse.position.X, (int)mouse.position.Y].Set(mouse.facing, mark.X);
                        TurnLeft();
                        TurnLeft();
                        Move();
                        forwards = false;
                    }
                } else {
                    // Old junction with some unlabeled paths
                    if (junctionMarks.Any(j => j.Value == mark.None)) {
                        // Get all directions with label none
                        var nones = junctionMarks.Where(j => j.Value == mark.None).ToList().Shuffle();
                        // Mark new entrance
                        junctions[(int)mouse.position.X, (int)mouse.position.Y].Set(nones[0].Direction, mark.N);

                        mouse.facing = nones[0].Direction;
                        Move();
                        forwards = true;
                    } else {
                        // Old junction with no unlabeled paths
                        var xPath = junctionMarks.Where(j => j.Value == mark.X).ToList()[0];

                        mouse.facing = xPath.Direction;
                        Move();
                        forwards = false;
                    }
                }

            }


        }

        private Compass RandomDirection(bool aheadBlocked, bool rightBlocked, bool leftBlocked) {
            List<Compass> set = new List<Compass>();
            switch (mouse.facing) {
                case Compass.North:
                    if (!rightBlocked) set.Add(Compass.East);
                    if (!aheadBlocked) set.Add(Compass.North);
                    if (!leftBlocked) set.Add(Compass.West);
                    break;
                case Compass.East:
                    if (!leftBlocked) set.Add(Compass.North);
                    if (!rightBlocked) set.Add(Compass.South);
                    if (!aheadBlocked) set.Add(Compass.East);
                    break;
                case Compass.South:
                    if (!aheadBlocked) set.Add(Compass.South);
                    if (!leftBlocked) set.Add(Compass.East);
                    if (!rightBlocked) set.Add(Compass.West);
                    break;
                case Compass.West:
                    if (!rightBlocked) set.Add(Compass.North);
                    if (!aheadBlocked) set.Add(Compass.West);
                    if (!leftBlocked) set.Add(Compass.South);
                    break;
            }

            set.Shuffle();
            return set[0];
        }

        public override bool Done() {
            return (int)Mouse.position.X == World.width - 2 && (int)Mouse.position.Y == World.height - 2;
        }
    }
}
