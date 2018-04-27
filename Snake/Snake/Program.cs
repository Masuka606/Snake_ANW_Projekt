using System;
using System.Collections.Generic;
using ListardDemo;
using System.Threading;

namespace ConsoleGameDemo
{
    /// <summary>
    /// Demonstrates how to write an iteractive game with the Console-Class
    /// </summary>
    class ConsoleGame
    {

        // Constants
        const int DIM_X = 80;
        const int DIM_Y = 30;
        const int MAX_SPEED = 1;
        const int INFOSCREEN_TIME = 2750;
        const int COUNT_Gems = 5;
        const ConsoleColor backColor = ConsoleColor.Black;
        const ConsoleColor userColor = ConsoleColor.Green;
        const ConsoleColor GemColor = ConsoleColor.Yellow;
        const ConsoleColor textColor = ConsoleColor.Green;


        //int[,] Snake = new int[70, 30];

        //Gameloop Variable
        bool continueloop = true;

        // User-Cursor with speed
        int posX = 20;
        int posY = 20;
        int speedX = 0;
        int speedY = -1;
        char symbol = 'o';
        char headsymbol = 'Ö';
        int GameSpeed = 200; //Spielgeschwindigkeit
        int ItemsCollected; //Gesammelte Items

        // Motion mode: user only motion or animated speedy motion


        // Create random gernerator
        Random random = new Random();

        // Item registry
        Dictionary<int, char> items = new Dictionary<int, char>();

        /// <summary>
        /// The game interaction
        /// </summary>
        static void Main(string[] args)
        {
            ConsoleGame game = new ConsoleGame();
            game.GameRun();

        }
        Listard<Position> SnakeBody = new Listard<Position>();
        Listard<Position> Gems = new Listard<Position>();


        public int MenuSpeed()
        {

            Console.WriteLine("Bitte geben sie die gewünschte Schwierigkeitsstufe ein (1-10)");
            int schwierigkeitsstufe = Convert.ToInt32(Console.ReadLine());



            switch (schwierigkeitsstufe)
            {
                case 1:
                    GameSpeed = 200;
                    break;

                case 2:
                    GameSpeed = 180;
                    break;

                case 3:
                    GameSpeed = 160;
                    break;

                case 4:
                    GameSpeed = 140;
                    break;

                case 5:
                    GameSpeed = 120;
                    break;

                case 6:
                    GameSpeed = 100;
                    break;

                case 7:
                    GameSpeed = 80;
                    break;

                case 8:
                    GameSpeed = 60;
                    break;

                case 9:
                    GameSpeed = 40;
                    break;

                case 10:
                    GameSpeed = 20;
                    break;

                default:
                    
                    break;
                    
            }
            // Setup console
            Console.Title = "Snakerino";
            Console.SetWindowSize(DIM_X, DIM_Y + 1);
            Console.SetBufferSize(DIM_X, DIM_Y + 1);
            Console.BackgroundColor = backColor;
            Console.ForegroundColor = userColor;
            Console.Clear();
            return GameSpeed;
        }


        public void collisionDetection()
        {
            for (int Snakelenght = SnakeBody.Count; Snakelenght > 2; Snakelenght--)
            {
                if (SnakeBody[0].X == SnakeBody[Snakelenght-1].X && SnakeBody[0].Y == SnakeBody[Snakelenght-1].Y)
                {
                    // Show final user information
                    Console.Clear();
                    ShowText("Leider verloren! ", 1, DIM_Y, GemColor);
                    Console.Beep(440, 500);
                    Thread.Sleep(80);
                    Console.Beep(392, 500);
                    Thread.Sleep(80);
                    Console.Beep(349, 500);
                    Thread.Sleep(80);
                    Console.Beep(329, 500);



                    // Wait user to terminate program using ENTER-key
                    Console.ReadLine();

                }

            }
        }


        public void CreateItem()
        {
            
            int x = random.Next(1, DIM_X-1);
            int y = random.Next(1, DIM_Y-1);
            char Gem = '*';

            Gems.Add(new Position(x, y));
            ShowSymbol(Gem, Gems[0].X, Gems[0].Y, GemColor);
        }




    public void SnakeMove(int speedX, int speedY)
        {



            //Move Body
            for (int Snakelenght = SnakeBody.Count; Snakelenght > 1; Snakelenght--)
            {
                SnakeBody[Snakelenght - 1] = SnakeBody[Snakelenght - 2];
                
                //set Snakeposition to previous Snakebody element



            }

            //Move Head
            Position newHeadPos = new Position(SnakeBody.Get(0).X, SnakeBody.Get(0).Y);

            newHeadPos.X = (newHeadPos.X + speedX) % DIM_X;
            newHeadPos.Y = (newHeadPos.Y + speedY) % DIM_Y;

            if (newHeadPos.Y < 0)
                newHeadPos.Y += newHeadPos.Y + DIM_Y;

            if (newHeadPos.X < 0)
                newHeadPos.X += newHeadPos.X + DIM_X;

            SnakeBody.Set(newHeadPos, 0);

            collisionDetection();



        }

        public void DrawSnake()
        {
            //Draw Snake
            for(int Snakelenght = SnakeBody.Count; Snakelenght > 1; Snakelenght--)
            {
                ShowSymbol(symbol, SnakeBody[Snakelenght - 1].X, SnakeBody[Snakelenght - 1].Y, userColor);

            }
            ShowSymbol(headsymbol, SnakeBody[0].X, SnakeBody[0].Y, userColor);
        }
        public void DeleteSnake()
        {
            //Delete 
            //Index of last Position
            int Snakelastpos = SnakeBody.Count - 1;
            ShowSymbol(symbol, SnakeBody[Snakelastpos].X, SnakeBody[Snakelastpos].Y, backColor);
        }



        public void GameRun()
        {


            MenuSpeed();

            //ASCII ART
            Console.WriteLine("\n\n");
            var arr = new[]
            {
            @"                                             88                   ",
            @"                                             88                   ",
            @"                                             88                   ",
            @"            ,adPPYba, 8b,dPPYba,  ,adPPYYba, 88   ,d8  ,adPPYba,  ",
            @"            I8[    "" 88P'   ``8a ""       `Y8 88 ,a8`  a8P_____88  ",
            @"             ``Y8ba,  88       88 ,adPPPPP88 8888    [8PP```````  ",
            @"            aa    ]8I 88       88 88,    ,88 88``Yba, `8b,   ,aa  ",
            @"            ``YbbdP`' 88       88 `´8bbdP´Y8 88   `Y8a `´Ybbd8´'  ",

            };


            //ASCII ART PRINT
            foreach (string line in arr)
                Console.WriteLine(line);


            ShowText("Nutze die Pfeiltasten zum Navigieren    ", 20, 15, textColor);
            // Wait some time
            System.Threading.Thread.Sleep(INFOSCREEN_TIME);
            Console.Clear();




            //Setup Snakebody

            for (int Snakelenght = 0; Snakelenght < 5; ++Snakelenght)
            {
                int Xp = 5;
                int Yp = 10 + Snakelenght;
                SnakeBody.Add(new Position(Xp, Yp));

            }






            CreateItem();
            while (continueloop == true)
            {
                // Show user symbol
                if ( ItemsCollected == 1)
                {
                    ShowText("" + ItemsCollected + " Stern gesammelt ", 1, DIM_Y, GemColor);
                }
                else ShowText("" + ItemsCollected + " Sterne gesammelt ", 1, DIM_Y, GemColor);



                // Check whether the user did some input
                if (Console.KeyAvailable)
                {
                    // User input - react to the users input

                    ConsoleKeyInfo cki = Console.ReadKey();
                    // Fetch first char of logical key name and set user symbol
                    string keyName = cki.Key.ToString();

                        // User manipulates the speed parameters
                        if (cki.Key == ConsoleKey.UpArrow)
                        {
                            if (speedY < 1)
                            {
                                speedY = -1;
                                speedX = 0;
                            }

                        }
                        
                        else if (cki.Key == ConsoleKey.DownArrow)
                        {
                            if (speedY > -1)
                            {
                                speedY = 1;
                                speedX = 0;
                            }
                        }
                        else if (cki.Key == ConsoleKey.LeftArrow)
                        {
                            if (speedX < 1)
                            {
                                speedX = -1;
                                speedY = 0;
                            }
                        }
                        else if (cki.Key == ConsoleKey.RightArrow)
                        {
                            if (speedX > -1)
                            {
                                speedX = 1;
                                speedY = 0;
                            }
                        }


                }
                else
                {
                    // No user input - the gaming loop does a time step

                    //Move Snake

                    SnakeMove(speedX, speedY);

                    DrawSnake();
                    // Wait some time
                    System.Threading.Thread.Sleep(GameSpeed);
                    DeleteSnake();
                }

                // Check whether the user hits an item


                if (Gems[0].X == SnakeBody[0].X && Gems[0].Y == SnakeBody[0].Y)
                {
                    ItemsCollected++;
                    Console.Beep(1108, 70);
                    

                    //items.Remove(itemKey);
                    Gems.Delete(0);


                    CreateItem();


                    //Länger werden

                        SnakeBody.Add(new Position(posX, posY));

                }

                
            }
            

            // Show final user information
            Console.Clear();
            ShowText("Gratuliere, Du hast es geschaft ", 1, DIM_Y, GemColor);

            // Wait user to terminate program using ENTER-key
            Console.ReadLine();

        }

        private int GetCount()
        {
            return Gems.Count;
        }





        /// <summary>
        /// Move the current user to the given position
        /// </summary>
        /// <param name="x">x-coordinate</param>
        /// <param name="y">y-coordinate</param>
        public void UserMove(int x, int y)
        {

            // Clear sympbol at old position
            ShowSymbol(symbol, posX, posY, backColor);

            // Shift the user symbol position
            posX = (posX + DIM_X + x) % DIM_X;
            posY = (posY + DIM_Y + y) % DIM_Y;
            Console.CursorLeft = posX;
            Console.CursorTop = posY;

            // Clear sympbol at new position
            ShowSymbol(symbol, posX, posY, userColor);
        }


        /// <summary>
        /// Gets a position code from the given xy-coordinates
        /// </summary>
        /// <param name="x">x-coordinate</param>
        /// <param name="y">y-coordinate</param>
        /// <returns></returns>
        public int KeyFromXY(int x, int y) { return y * 1000 + x; }

        /// <summary>
        /// Gets the x-coordinate from the given position code
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public int XFromKey(int pos) { return pos % 1000; }

        /// <summary>
        /// Gets the y-coordinate from the given position code
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public int YFromKey(int pos) { return pos / 1000; }

        /// <summary>
        /// Show a symbol at a position with color and keep position
        /// </summary>
        /// <param name="symbol">Symbol char</param>
        /// <param name="x">X-coordinate</param>
        /// <param name="y">Y-coordinate</param>
        /// <param name="color">Symbol color</param>
        public void ShowSymbol(char symbol, int x, int y, ConsoleColor color)
        {
            // Remember current state
            int memX = Console.CursorLeft;
            int memY = Console.CursorTop;
            ConsoleColor memColor = Console.ForegroundColor;

            ShowText("" + symbol, x, y, color);

            // Restore remembered state
            Console.CursorLeft = memX;
            Console.CursorTop = memY;
            Console.ForegroundColor = memColor;
        }

        /// <summary>
        /// Show a text at a position with color and shift postion 
        /// </summary>
        /// <param name="text">Text to show</param>
        /// <param name="x">X-coordinate</param>
        /// <param name="y">Y-coordinate</param>
        /// <param name="color">Symbol color</param>
        private static void ShowText(string text, int x, int y, ConsoleColor color)
        {
            // Show symbol regarding its paramters
            Console.CursorLeft = x;
            Console.CursorTop = y;
            Console.ForegroundColor = color;
            Console.Write(text);
        }
    }
}


