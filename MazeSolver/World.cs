using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;
using static PepesComing.Utils;

namespace PepesComing {

    public class World {
        // World dimensions should follow x % 4 == 3
        public const int width = 39;
        public const int height = 19;

        private readonly Cell[,] Grid = new Cell[width, height];
        private Thread genThread;

        public World() {
            // Check world is a valid size
            Debug.Assert(width % 4 == 3, "Invalid world width");
            Debug.Assert(height % 4 == 3, "Invalid world height");

            Debug.Print("{0}, {1}", (width - 2) % 4, (height - 2) % 4);

            // Setup world gen thread
            genThread = new Thread(PrimsMaze);
            genThread.IsBackground = true;

            // Start world generation
            RegenerateMaze();
        }

        //Get the cell at (x, y)
        public Cell GetCell(int x, int y) {
            Debug.Assert(x >= 0 | x < width);
            Debug.Assert(y >= 0 | y < height);
            return Grid[x, y];
        }

        //Get the cells directly connected to the cell at (x, y)
        public Cardinal<Cell> GetNeigbors(int x, int y) {
            // Check row and column are within bounds
            Debug.Assert(x >= 1 | x < width - 1);
            Debug.Assert(y >= 1 | y < height - 1);

            // Get neigbors around cell
            Cardinal<Cell> neigbors = new Cardinal<Cell>();
            neigbors.North = GetCell(x, y + 1);
            neigbors.East = GetCell(x + 1, y);
            neigbors.South = GetCell(x, y - 1);
            neigbors.West = GetCell(x - 1, y);

            return neigbors;
        }

        //Render a tile to the spritebatch
        public void RenderTile(int x, int y, SpriteBatch spriteBatch, Sprites sprites) {
            spriteBatch.Draw(texture: sprites.Texture, destinationRectangle: new Rectangle(x * 16, y * 16, 16, 16), sourceRectangle: GetCell(x, y).Tile, color: Color.White);
        }

        //Get cells (if any) two units away (in the cardinal directions)
        private List<Cell> GetNearCells(int x, int y) {
            List<Cell> NearCells = new List<Cell>();

            //Up
            if (y <= height - 3) {
                NearCells.Add(Grid[x, y + 2]);
            }
            //Down
            if (y >= 2) {
                NearCells.Add(Grid[x, y - 2]);
            }
            //Left
            if (x >= 2) {
                NearCells.Add(Grid[x - 2, y]);
            }
            //Right
            if (x <= width - 3) {
                NearCells.Add(Grid[x + 2, y]);
            }

            return NearCells;
        }

        //Gets cells (if any) two units away (in the cardinal directions) that are walls
        private List<Cell> GetFrontiers(int x, int y) {
            List<Cell> frontiers = GetNearCells(x, y);
            //Remove all walls
            frontiers.RemoveAll(c => (c.Type != Cell.types.WALL));
            return frontiers;
        }

        //Gets cells (if any) two units away (in the cardinal directions) that are not walls (paths/start/endpoint)
        private List<Cell> GetNeighbors(int x, int y) {
            List<Cell> neighbors = GetNearCells(x, y);
            //Remove all non-walls
            neighbors.RemoveAll(c => c.Type == Cell.types.WALL);
            return neighbors;
        }

        //Prims maze generation algoritum (blocking)
        private void PrimsMaze() {
            List<Cell> frontiers;

            //Start the maze generation at the center
            int middleX = (width - 1) / 2;
            int middleY = (height - 1) / 2;
            Grid[middleX, middleY].Type = Cell.types.FLOOR;

            //Get initial frountiers
            frontiers = GetFrontiers(middleX, middleY);

            while (frontiers.Count > 0) {
                //Pick a random frountier and neigbhor
                Cell frontier = frontiers[Game.rnd.Next(0, frontiers.Count - 1)];
                List<Cell> neighbors = GetNeighbors(frontier.X, frontier.Y);
                Cell neighbor = neighbors[Game.rnd.Next(0, neighbors.Count - 1)];

                //Make a path between the cell and the frountier
                if (frontier.Y == neighbor.Y) {
                    if (frontier.X > neighbor.X) {
                        Grid[frontier.X - 1, frontier.Y].Type = Cell.types.FLOOR;
                    } else if (frontier.X < neighbor.X) {
                        Grid[frontier.X + 1, frontier.Y].Type = Cell.types.FLOOR;
                    }
                } else if (frontier.X == neighbor.X) {
                    if (frontier.Y > neighbor.Y) {
                        Grid[frontier.X, frontier.Y - 1].Type = Cell.types.FLOOR;
                    } else if (frontier.Y < neighbor.Y) {
                        Grid[frontier.X, frontier.Y + 1].Type = Cell.types.FLOOR;
                    }
                }
                frontier.Type = Cell.types.FLOOR;

                //Update the froutier list
                frontiers.AddRange(GetFrontiers(frontier.X, frontier.Y));
                frontiers.Remove(frontier);

                //TODO: is this nessisary?
                frontiers.RemoveAll(j => (j.Type != Cell.types.WALL));
            }

            //Set the start and end points
            Grid[1, 1].Type = Cell.types.START;
            Grid[width - 2, height - 2].Type = Cell.types.ENDPOINT;
        }

        //Maze (re)generation (non-blocking)
        public void RegenerateMaze() {
            //Stop thread if its still alive
            if (genThread.IsAlive) {
                genThread.Abort();
            }

            //(Re)initialize cells
            for (int x = 0; x <= width - 1; x++) {
                for (int y = 0; y <= height - 1; y++) {
                    Grid[x, y] = new Cell(x, y);
                }
            }

            //Start maze generation
            genThread = new Thread(PrimsMaze);
            genThread.IsBackground = true;
            genThread.Start();
        }
    }
}
