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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace PepesComing
{

	public class Cell
	{

		private int _x;
		public int X {
			get { return _x; }
		}

		private int _y;
		public int Y {
			get { return _y; }
		}

		private Rectangle _tile;
		public Rectangle tile {
			get { return _tile; }
			private set { _tile = value; }
		}

		private types _type;
		public types type {
			get { return _type; }
			set {
				switch (value) {
					case types.WALL:
						tile = Sprites.SandWall;
						break;
					case types.START:
						tile = Sprites.Start;
						break;
					case types.ENDPOINT:
						tile = Sprites.EndPoint;
						break;
					case types.FLOOR:
						tile = Sprites.SteelFloor;
						break;
				}
				_type = value;
			}
		}

		public enum types
		{
			WALL,
			FLOOR,
			START,
			ENDPOINT
		}

		public bool frontier { get; set; }

		public Cell(int x, int y)
		{
			_x = x;
			_y = y;
			type = types.WALL;
		}

		public void print()
		{
			Console.WriteLine("x: {0}, y: {1}, wall: {2}", X, Y, type);
		}
	}
}
