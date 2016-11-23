using System;
namespace PepesComing {


    public static class Application {

        //static Microsoft.Xna.Framework.Game _game;
        public static void Main() {
            try {
                Microsoft.Xna.Framework.Game _game = new Game();
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
