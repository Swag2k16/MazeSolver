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
namespace PepesComing
{


	public class Application
	{

		static Microsoft.Xna.Framework.Game _game;
		public static void Main()
		{
			try {
				_game = new Game();
				_game.Run();
			} catch (Exception ex) {
				Console.WriteLine("Boo! something went wrong!");
				Console.WriteLine(ex.Message);
				Console.WriteLine(ex.InnerException);
				Console.WriteLine(ex.StackTrace);
				Console.ReadLine();
			}

		}
	}
}
