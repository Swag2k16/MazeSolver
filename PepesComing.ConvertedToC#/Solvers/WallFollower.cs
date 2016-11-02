using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PepesComing;
namespace PepesComing
{

	public class WallFollower : Solver
	{
		private enum compass
		{
			North,
			East,
			South,
			West
		}
		private compass facing;
		private Vector2 position;

		private List<Vector2> solution;
		public override List<Vector2> Solve(ref World world)
		{
			position = new Vector2(1, 1);
			solution = new List<Vector2>();

			// Set facing towards empty tile
			var neighbors = world.GetNeigbors((int)position.X, (int)position.Y);
			if (neighbors.North.type == Cell.types.FLOOR) {
				facing = compass.North;
			} else if (neighbors.East.type == Cell.types.FLOOR) {
				facing = compass.East;
			} else if (neighbors.South.type == Cell.types.FLOOR) {
				facing = compass.South;
			} else if (neighbors.West.type == Cell.types.FLOOR) {
				facing = compass.West;
			}

			while (position.X != World.width - 2 | position.Y != 1) {
				var ahead = LookAhead(ref world);
				var left = LookLeft(ref world);

				if ((ahead == Cell.types.FLOOR | ahead == Cell.types.ENDPOINT) & left == Cell.types.WALL) {
					Move();
				} else if (ahead == Cell.types.FLOOR & left == Cell.types.FLOOR) {
					TurnLeft();
					Move();
				} else if (ahead == Cell.types.WALL) {
					//TODO: do we need this?
					TurnRight();
				}
			}

			return solution;
		}

		//Gets the tile type ahead in the direction we are facing
		private Cell.types LookAhead(ref World world)
		{
			var neighbors = world.GetNeigbors((int)position.X, (int)position.Y);
			switch (facing) {
				case compass.North:
					return neighbors.North.type;
				case compass.South:
					return neighbors.South.type;
				case compass.East:
					return neighbors.East.type;
				case compass.West:
					return neighbors.West.type;
			}
			return Cell.types.WALL;
		}

		//Gets the tile type to the left of the direction we are facing
		private Cell.types LookLeft(ref World world)
		{
			var neighbors = world.GetNeigbors((int)position.X, (int)position.Y);
			switch (facing) {
				case compass.North:
					return neighbors.West.type;
				case compass.South:
					return neighbors.East.type;
				case compass.East:
					return neighbors.North.type;
				case compass.West:
					return neighbors.South.type;
			}
			return Cell.types.WALL;
		}

		//Move in the direction we are facing (and add the cell to the solution
		private void Move()
		{
			switch (facing) {
				case compass.North:
					position.Y += 1;
					break;
				case compass.South:
					position.Y -= 1;
					break;
				case compass.East:
					position.X += 1;
					break;
				case compass.West:
					position.X -= 1;
					break;
			}

			solution.Add(position);
		}

		//Turn to face the left
		private void TurnLeft()
		{
			switch (facing) {
				case compass.North:
					facing = compass.West;
					break;
				case compass.South:
					facing = compass.East;
					break;
				case compass.West:
					facing = compass.South;
					break;
				case compass.East:
					facing = compass.North;
					break;
			}
		}

		//Turn to face the right
		private void TurnRight()
		{
			switch (facing) {
				case compass.North:
					facing = compass.East;
					break;
				case compass.South:
					facing = compass.West;
					break;
				case compass.West:
					facing = compass.North;
					break;
				case compass.East:
					facing = compass.South;
					break;
			}
		}
	}
}
