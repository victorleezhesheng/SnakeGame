using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace SnakeGame
{
    //Position of food : Jonathan
    class Index
    {
        public int indexx;
        public int indexy;
        public Index(int Indexy, int Indexx)
        {
            indexx = Indexx;
            indexy = Indexy;
        }

        public int IndexY
        {
            get { return indexy; }
            set { indexy = value; }

        }

        public int IndexX
        {
            get { return indexy; }
            set { indexy = value; }

        }

    }


    class Program
    {
        public void Run()
        {
            // display this string on the console during the game
            string ch = "***";// Added snake length : Lewis Chin
            bool gameLive = true;
            ConsoleKeyInfo consoleKey; // holds whatever key is pressed

            // location info & display
            int x = 0, y = 2; // y is 2 to allow the top row for directions & space
            int dx = 1, dy = 0;
            int consoleWidthLimit = 79;
            int consoleHeightLimit = 24;


            Random rand = new Random(); //inputs random numbers

            // clear to color
            Console.BackgroundColor = ConsoleColor.Black; //the background of the colour
            Console.Clear();

            // delay to slow down the character movement so you can see it
            int delayInMillisecs = 50;

            // whether to keep trails
            bool trail = false;

            //Location to spawn obstacle
            List<Index> obstacles = new List<Index>() {
                new Index(rand.Next(10,20)/*hieght*/,rand.Next(0,70)/*Width*/),
                new Index(rand.Next( 1,20),rand.Next (34,70)),
                new Index(rand.Next(14,20),rand.Next(12,70)),
                new Index(rand.Next(8,20),rand.Next(26,70)),
                new Index(rand.Next(2,20),rand.Next(3,70)),
                new Index(rand.Next(2,20),rand.Next(1,70)),
                new Index(rand.Next(4,20),rand.Next(3,70)),
                new Index(rand.Next(1,20),rand.Next(1,70)),
            };

            //Spawning obstacle
            foreach (Index obstacle in obstacles)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(obstacle.indexx, obstacle.indexy);
                Console.Write("||");
            }

            //Spawn food
            Index point;
            
                point = new Index(rand.Next(0, consoleHeightLimit),
                rand.Next(0, consoleWidthLimit));


                Console.SetCursorPosition(point.indexx, point.indexy);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("@");


            do // until escape
            {
                // print directions at top, then restore position
                // save then restore current color
                ConsoleColor cc = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Arrows move up/down/right/left. Press 'esc' quit.");
                Console.SetCursorPosition(x, y);
                Console.ForegroundColor = cc;

                //Added a score board: Jonathan
                ConsoleColor score = Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(100, 0);
                Console.Write("Score: 0");
                Console.ForegroundColor = score;
                //

                //Added achievement score: Lee Zhe Sheng 
                ConsoleColor a_score = Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(50, 0);
                Console.Write("Achievement Score: 50");
                Console.ForegroundColor = a_score;


                // see if a key has been pressed
                if (Console.KeyAvailable)
                {
                    // get key and use it to set options
                    consoleKey = Console.ReadKey(true);
                    switch (consoleKey.Key)
                    {

                        case ConsoleKey.UpArrow: //UP
                            dx = 0;
                            dy = -1;
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        case ConsoleKey.DownArrow: // DOWN
                            dx = 0;
                            dy = 1;
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            break;
                        case ConsoleKey.LeftArrow: //LEFT
                            dx = -1;
                            dy = 0;
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case ConsoleKey.RightArrow: //RIGHT
                            dx = 1;
                            dy = 0;
                            Console.ForegroundColor = ConsoleColor.Black;
                            break;
                        case ConsoleKey.Escape: //END
                            gameLive = false;
                            break;
                    }
                }

                // find the current position in the console grid & erase the character there if don't want to see the trail
                Console.SetCursorPosition(x, y);
                if (trail == false)
                    Console.Write("   "); // Added trail space : Lewis Chin

                // calculate the new position
                // note x set to 0 because we use the whole width, but y set to 1 because we use top row for instructions
                x += dx;
                if (x > consoleWidthLimit)
                    x = 0;
                if (x < 0)
                    x = consoleWidthLimit;

                y += dy;
                if (y > consoleHeightLimit)
                    y = 2; // 2 due to top spaces used for directions
                if (y < 2)
                    y = consoleHeightLimit;

                // write the character in the new position
                Console.SetCursorPosition(x, y);
                Console.Write(ch);

                // pause to allow eyeballs to keep up
                System.Threading.Thread.Sleep(delayInMillisecs);

            } while (gameLive);

        }

        public void GameOver()
        {
            // Added Game Over Screen: Lee Zhe Sheng
            Console.Clear();
            ConsoleColor game_over = Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(50, 10);
            Console.Write(" ========Game Over======== ");
            Console.SetCursorPosition(50, 11);
            Console.WriteLine("Press any key to Play Again");
            Console.SetCursorPosition(47, 12);
            Console.WriteLine("Press 'Enter' key to quit the game");
            Console.ForegroundColor = game_over;

        }

        public void GoodBye()
        {
            //Added Good bye Screen: Lee Zhe Sheng
            Console.Clear();
            ConsoleColor good_bye = Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(50, 10);
            Console.Write(" ======Thanks for Playing====== ");
            Console.SetCursorPosition(55, 11);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();            
            Console.ForegroundColor = good_bye;
        }

        static void Main(string[] args)
        {
            Program myGame = new Program(); 
            // start game
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            do
            {
                myGame.Run();
                myGame.GameOver();

                

            } while (Console.ReadKey().Key != ConsoleKey.Enter);

            myGame.GoodBye();

            
        }
    }

}
