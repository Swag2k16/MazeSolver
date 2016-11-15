using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace PepesComing {
    public abstract class Solver {
        protected Thread solveThread;
        protected World world;

        private Stopwatch timer;

        protected SolverMouse mouse;
        public SolverMouse Mouse {
            get {
                return mouse;
            }
        }

        protected List<Vector2> _solution;
        public List<Vector2> Solution {
            get {
                return _solution;
            }
        }

        // Gets the time elapsed since the solver started
        public long Elapsed {
            get {
                return timer.ElapsedMilliseconds;
            }
        }

        public Solver(ref World world) {
            mouse = new SolverMouse();
            mouse.position = new Vector2(1, 1);

            _solution = new List<Vector2>();

            timer = Stopwatch.StartNew();

            // Start solving maze
            solveThread = new Thread(Solve);
            solveThread.IsBackground = true;
            solveThread.Start();
            this.world = world;
        }

        private void Solve() {
            while (!Done()) {
                Step();
            }
            timer.Stop();
        }

        public abstract SolverMouse Step();
        public abstract bool Done();

        public enum compass {
            North,
            East,
            South,
            West
        }

        public struct SolverMouse {
            public Vector2 position;
            public compass facing;
        }
    }
}
