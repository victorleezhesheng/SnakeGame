using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using System.IO;

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

    //Achievement Score: Lee Zhe Sheng
    class Achievement_Score
    {
        public int ascore;

        public Achievement_Score(int Ascore)
        {
            ascore = Ascore;
        }

        public int AScore
        {
            get { return ascore; }
            set { ascore = value; }
        }
    }


    class Program
    {
        public void Run()
        {
            //Background Music : Jonathan Lee
            System.Media.SoundPlayer BGM = new System.Media.SoundPlayer();
            BGM.SoundLocation = "../../../Music/BGM.wav";
            BGM.Play();
            // display this string on the console during the game
            string ch = "*";
            bool gameLive = true;
            ConsoleKeyInfo consoleKey;
            // location info & display
            int x = 0, y = 2; // y is 2 to allow the top row for directions & space
            int consoleWidthLimit = 79;
            int consoleHeightLimit = 24;
            string food = "@";
            string obs = "|";

            Random rand = new Random(); //inputs random numbers
            // Direction modification: Lewis Chin
            byte right = 0;
            byte left = 1;
            byte down = 2;
            byte up = 3;
            Index[] directions = new Index[]
            {
                new Index(0, 1), // right
                new Index(0, -1), // left
                new Index(1, 0), // down
                new Index(-1, 0), // up
            };
            int direction = right;

            // clear to color
            Console.BackgroundColor = ConsoleColor.Black; //the background of the colour
            Console.Clear();

            // delay to slow down the character movement so you can see it
            int delayInMillisecs = 50;

            int scores = 0;
            //

            //Location to spawn obstacle : Jonathan
            List<Index> obstacles = new List<Index>() {
                new Index(rand.Next(10,20)/*height*/,rand.Next(0,70)/*Width*/),
                new Index(rand.Next( 1,20),rand.Next (34,70)),
                new Index(rand.Next(14,20),rand.Next(12,70)),
                new Index(rand.Next(8,20),rand.Next(26,70)),
                new Index(rand.Next(2,20),rand.Next(3,70)),
                new Index(rand.Next(2,20),rand.Next(1,70)),
                new Index(rand.Next(4,20),rand.Next(3,70)),
                new Index(rand.Next(1,20),rand.Next(1,70)),
            };

            //Spawning obstacle : Jonathan
            foreach (Index obstacle in obstacles)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(obstacle.indexx, obstacle.indexy);
                Console.Write(obs);
            }
            // Merge food and snake elements : Lewis Chin
            Queue<Index> elements = new Queue<Index>();
            for (int i = 0; i <= 2; i++)
            {
                elements.Enqueue(new Index(1, i));
            }
            //Spawn food : Jonathan
            Index fruit;
            do
            {
                fruit = new Index(rand.Next(0, consoleHeightLimit),
                    rand.Next(0, consoleWidthLimit));
            }
            while (elements.Contains(fruit) || obstacles.Contains(fruit));
            Console.SetCursorPosition(fruit.indexx, fruit.indexy);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(food);

            // Snake Character position : Lewis Chin

            foreach (Index position in elements)
            {
                Console.SetCursorPosition(position.indexx, position.indexy);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(ch);
            }
            int Timer = 0; //Timer : Jonathan Lee
            do // until escape
            {
                //Display Timer: Jonathan Lee 
                ConsoleColor time = Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(80, 0);
                Console.Write("Timer: " + Timer);
                Console.ForegroundColor = time;
                Timer++;
                // print directions at top, then restore position
                // save then restore current color
                ConsoleColor cc = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Arrows move up/down/right/left. Press 'esc' quit.");
                Console.SetCursorPosition(x, y);
                Console.ForegroundColor = cc;

                ConsoleColor score = Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(100, 0);
                Console.Write("Score: " + scores);
                Console.ForegroundColor = score;


                /*Added achievement score: Lee Zhe Sheng 
                ConsoleColor a_score = Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(50, 0);
                Console.Write("Achievement Score: 50");
                Console.ForegroundColor = a_score;*/



                // see if a key has been pressed

                // Direction modification : Lewis Chin
                if (Console.KeyAvailable)
                {
                    // get key and use it to set options
                    consoleKey = Console.ReadKey(true);
                    switch (consoleKey.Key)
                    {

                        case ConsoleKey.UpArrow: //UP
                            if (direction != down) direction = up;
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        case ConsoleKey.DownArrow: // DOWN
                            if (direction != up) direction = down;
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            break;
                        case ConsoleKey.LeftArrow: //LEFT
                            if (direction != right) direction = left;
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case ConsoleKey.RightArrow: //RIGHT
                            if (direction != left) direction = right;
                            Console.ForegroundColor = ConsoleColor.Black;
                            break;
                        case ConsoleKey.Escape: //END
                            gameLive = false;
                            break;
                    }
                }
                // Snake Head Direction : Lewis Chin
                Index snakeHead = elements.Last();
                Index nextDirection = directions[direction];
                
                Index snakeNewHead = new Index(snakeHead.indexy + nextDirection.indexy,
                    snakeHead.indexx + nextDirection.indexx);

                if (snakeNewHead.indexx < 0) snakeNewHead.indexx = consoleWidthLimit - 1;
                if (snakeNewHead.indexy < 0) snakeNewHead.indexy = consoleHeightLimit - 1;
                if (snakeNewHead.indexy >= consoleHeightLimit) snakeNewHead.indexy = 0;
                if (snakeNewHead.indexx >= consoleWidthLimit) snakeNewHead.indexx = 0;

                Console.SetCursorPosition(snakeHead.indexx, snakeHead.indexy);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(ch);

                elements.Enqueue(snakeNewHead);
                Console.SetCursorPosition(snakeNewHead.indexx, snakeNewHead.indexy);
                Console.ForegroundColor = ConsoleColor.Green;
                if (direction == right) Console.Write(ch);
                if (direction == left) Console.Write(ch);
                if (direction == up) Console.Write(ch);
                if (direction == down) Console.Write(ch);
                // Snake length growth : Lewis Chin
                if (snakeNewHead.indexx == fruit.indexx && snakeNewHead.indexy == fruit.indexy)
                {
                    //Eat Sound : Jonathan Lee
                    System.Media.SoundPlayer Eat = new System.Media.SoundPlayer();
                    Eat.SoundLocation = "../../../Music/Hit.wav";
                    Eat.Play();
                    do
                    {
                        fruit = new Index(rand.Next(0, consoleHeightLimit),
                            rand.Next(0, consoleWidthLimit));
                    }
                    while (elements.Contains(fruit) || obstacles.Contains(fruit));
                    Console.SetCursorPosition(fruit.indexx, fruit.indexy);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(food);
                    scores += 1;
                }
                else
                {
                    // Added space : Lewis Chin
                    Index last = elements.Dequeue();
                    Console.SetCursorPosition(last.indexx, last.indexy);
                    Console.Write(" ");
                }

                foreach (Index obstacle in obstacles)
                {
                    if (snakeNewHead.indexx == obstacle.indexx && snakeNewHead.indexy == obstacle.indexy)
                    {
                        //bug fixed: Lee Zhe Sheng
                        //Game End Score: Jonathan Lee
                        StreamWriter sw = File.AppendText("../../../ScoreBoard/ScoreBoard.txt");
                        sw.WriteLine("Score: " + scores.ToString() + " Timer: " + Timer.ToString(), "\n");
                        sw.Close();
                        gameLive = false;
                        GameOver();
                    }

                }



                // write the character in the new position
                
                // length add after eat: Lewis Chin


                // pause to allow eyeballs to keep up
                System.Threading.Thread.Sleep(delayInMillisecs);

            } while (gameLive);

        }


        public void GameOver()
        {
            // Added Game Over Screen: Lee Zhe Sheng
            Console.Clear();
            //GameOver BGM : Jonathan Lee
            System.Media.SoundPlayer GameOver = new System.Media.SoundPlayer();
            GameOver.SoundLocation = "../../../Music/GameOver.wav";
            GameOver.Play();
            ConsoleColor game_over = Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(50, 10);
            Console.Write(" ========Game Over======== ");
            Console.SetCursorPosition(50, 11);
            Console.WriteLine("Press any key to Play Again");
            Console.SetCursorPosition(47, 12);
            Console.WriteLine("Press 'Enter' key to quit the game");
            Console.ForegroundColor = game_over;
            //Gameover score : Jonathan Lee
            using (StreamReader file = new StreamReader("../../../ScoreBoard/ScoreBoard.txt"))
            {
                string ln;
                while ((ln = file.ReadLine()) != null)
                {
                    Console.SetCursorPosition(47, 13);
                    Console.WriteLine(ln);

                }
            }
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

            //Show all scoreboard : Jonathan Lee
            using (StreamReader file = new StreamReader("../../../ScoreBoard/ScoreBoard.txt"))
            {
                string ln;
                while ((ln = file.ReadLine()) != null)
                {
                    Console.WriteLine(ln);
                }
            }

            do
            {
                
                myGame.Run();
                myGame.GameOver();



            } while (Console.ReadKey().Key != ConsoleKey.Enter);

            myGame.GoodBye();


        }
    }

}
