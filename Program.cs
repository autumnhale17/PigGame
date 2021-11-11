
using System;

namespace Pig
{
    class Program
    {
        static void Main(string[] args)
        {
            // Welcome message with name, copied format from the instructions
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("========= Welcome to the Pig Game =========");
            Console.WriteLine("---------@Developed by Autumn Hale@--------");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine(" ");
            Console.WriteLine(@"How to play: Two players take turns testing their luck by rolling a virtual die 
to see who hits the winning score first! Start by entering how many sides the die has, 
what you want the winning score to be, and enter both names. During your turn, you can 
either roll again or hold the die. Every roll can add to your score and get you closer to victory, 
but roll a 1 and you lose all points for that round :(
You can avoid this by playing it safe and holding your roll after you accumulate points. 
Note, the program always starts with player 1, and the first automatic roll on your turn may be a 1.

The first player to reach the set winning number of points wins, and the game automatically ends.
");

            Console.WriteLine($@"
            (\____/)
            / @__@ \
           (  (oo)  )
            `-.-~.-'
             /    \
           @/      \_
          (/ /    \ \)
           WW`----'WW");

            Console.WriteLine("-------------------------------------------");

            Console.WriteLine(" ");
            
                Game game = new Game();
                game.SetupDie();
                game.SetupMaxPoints();
                game.SetPlayer1();
                game.SetPlayer2();
                game.PlayGame();
                game.ShowWinner();
            
        }
    }
}