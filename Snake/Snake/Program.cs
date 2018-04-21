/*
 * (c) fn@gso-koeln.de 2017
 */
using System;
using System.Collections.Generic;
using ListardDemo;

namespace ConsoleGameDemo
{
    /// <summary>
    /// Demonstrates how to write an iteractive game with the Console-Class
    /// </summary>
    class ConsoleGame
    {

        // Constants
        const int DIM_X = 80;
        const int DIM_Y = 40;
        const int MAX_SPEED = 1;
        const int DELTA_TIME = 200;
        const int INFOSCREEN_TIME = 3000;
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
        int speedY = 1;
        char symbol = '#';

        // Motion mode: user only motion or animated speedy motion
        bool speedyMotion = true;

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
        Listard<Position> Gems= new Listard<Position>();

        public void CreateItems()
        {
            for (int Items = 0; Items < 5; ++Items)
            {
                int x = random.Next(DIM_X);
                int y = random.Next(DIM_Y);
                char Gem = '*';

                Gems.Add(new Position(x,y));
                ShowSymbol(Gem, Gems[Items].X, Gems[Items].Y, GemColor);

            }
        }


        //UNFINISHED/Obsolete
        //public void SnakeInit()
        //{

        //    int Snakelenght = 0;
        //    while (Snakelenght < SnakeBody.Count)
        //    {

        //        Position Snpos = SnakeBody.Get(Snakelenght);

        //        ShowSnake("H", Snpos.X, Snpos.Y, ConsoleColor.Cyan);
        //        Snakelenght++;

        //    }
        //}


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

            //////Delete 
            //////Index of last Position
            ////int Snakelastpos = SnakeBody.Count-1;
            ////ShowSymbol(symbol, SnakeBody[Snakelastpos].X, SnakeBody[Snakelastpos].Y, backColor); 

            //SnakeBody[Snakelastpos]
            //Draw Snake




            //posX = (posX + DIM_X + x) % DIM_X;
            //posY = (posY + DIM_Y + y) % DIM_Y;

            //while (Snakelenght < SnakeBody.Count)
            //{

            //    Position Snpos = SnakeBody.Get(Snakelenght);

            //    nSnakepos.X = ((Snpos.X + DIM_X + speedX) % DIM_X);
            //    //Position nSnakepos = SnakeBody.Set(new Position(Snpos.X, Snpos.Y));


            //SnakeBody[pos.X] = SnakeBody.

            //while (Snakelenght < SnakeBody.Count)
            //{

            //    Position Snpos = SnakeBody.Get(Snakelenght);

            //    nSnakepos.X = ((Snpos.X + DIM_X + speedX) % DIM_X);
            //    //Position nSnakepos = SnakeBody.Set(new Position(Snpos.X, Snpos.Y));



            //}

            //    //Clear sympbol at old position
            //ShowSymbol(symbol, posX, posY, backColor);

            //    // Shift the user symbol position
            //    posX = (posX + DIM_X + x) % DIM_X;
            //    posY = (posY + DIM_Y + y) % DIM_Y;
            //    Console.CursorLeft = posX;
            //    Console.CursorTop = posY;

            //    // Clear sympbol at new position
            //    ShowSymbol(symbol, posX, posY, userColor);
        }

        public void ShowSnake()
        {

            for(int Snakelenght = SnakeBody.Count; Snakelenght > 0; Snakelenght--)
            {
                ShowSymbol(symbol, SnakeBody[Snakelenght - 1].X, SnakeBody[Snakelenght - 1].Y, userColor);

            }
        }
        public void DeleteSnake()
        {
            //Delete 
            //Index of last Position
            int Snakelastpos = SnakeBody.Count - 1;
            ShowSymbol(symbol, SnakeBody[Snakelastpos].X, SnakeBody[Snakelastpos].Y, backColor);
        }


        ////private static void ShowSnake(string text, int x, int y, ConsoleColor color)
        ////{
        ////    // Show symbol regarding its paramters
        ////    Console.CursorLeft = x;
        ////    Console.CursorTop = y;
        ////    Console.ForegroundColor = color;
        ////    Console.Write(text);
        ////}

        public void GameRun()
        {


            // Setup console
            Console.Title = "My Console Game";
            Console.SetWindowSize(DIM_X, DIM_Y + 1);
            Console.SetBufferSize(DIM_X, DIM_Y + 1);
            Console.BackgroundColor = backColor;
            Console.ForegroundColor = userColor;
            Console.Clear();

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


            // Create a bunch of items for the user to collect
         //   for (int i = 0; i < COUNT_Gems; i++) CreateItems();

            //Setup Snakebody

            for (int Snakelenght = 0; Snakelenght < 5; ++Snakelenght)
            {
                int Xp = 5;
                int Yp = 10 + Snakelenght;
                SnakeBody.Add(new Position(Xp, Yp));

            }





            //Obsolete
            //SnakeInit();

            CreateItems();
            while (continueloop == true)
            {

                // Show user symbol
                ShowText("" + GetCount() + " Gegenstände sind noch zu sammeln ", 1, DIM_Y, GemColor);



                // Check whether the user did some input
                if (Console.KeyAvailable)
                {
                    // User input - react to the users input

                    ConsoleKeyInfo cki = Console.ReadKey();
                    // Fetch first char of logical key name and set user symbol
                    string keyName = cki.Key.ToString();
                    //symbol = keyName[0];
                    symbol = '#';
                    if (speedyMotion)
                    {
                        // User manipulates the speed parameters
                        if (cki.Key == ConsoleKey.UpArrow)
                        {
                            if (speedY < 1)
                            {
                                speedY = -1;
                                speedX = 0;
                            }

                        }

                        if (cki.Key == ConsoleKey.DownArrow)
                        {
                            if (speedY > -1)
                            {
                                speedY = 1;
                                speedX = 0;
                            }
                        }
                        if (cki.Key == ConsoleKey.LeftArrow)
                        {
                            if (speedX < 1)
                            {
                                speedX = -1;
                                speedY = 0;
                            }
                        }
                        if (cki.Key == ConsoleKey.RightArrow)
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

                    }
                }
                else
                {
                    // No user input - the gaming loop does a time step

                    // Move according to the currrent user symbol speed
                    // if (speedyMotion) UserMove(speedX, speedY);

                    //Move Snake

                    SnakeMove(speedX, speedY);
                    ShowSnake();
                    // Wait some time
                    System.Threading.Thread.Sleep(DELTA_TIME);
                    DeleteSnake();
                }

                // Check whether the user hits an item
               // int itemKey = KeyFromXY(posX, posY);
                //if (items.ContainsKey(itemKey))
                for (int i2 = 0; i2 < GetCount(); ++i2)
                {
                    if (Gems[i2].X == SnakeBody[0].X && Gems[i2].Y == SnakeBody[0].Y)
                    {
                        Console.Beep(); // Give some Beep
                                        // items.Remove(itemKey);
                        Gems.Delete(i2,[1]);

                        if (Gems.Count == 0)
                        {

                            continueloop = false;

                        }

                        //Länger werden

                        SnakeBody.Add(new Position(posX, posY));

                    }
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
        /// Creates, shows and registers a new item
        /// </summary>
        public void CreateItem()
        {
            while (true)
            {
                // Get new random position
                int x = random.Next(DIM_X);
                int y = random.Next(DIM_Y);

                // Get a position code from xy-coordinates
                int itemKey = KeyFromXY(x, y);
                char item = '*';

                // Try if no item at position already existed
                if (!items.ContainsKey(itemKey))
                {
                    items.Add(itemKey, item);
                    ShowSymbol(item, x, y, GemColor);
                    return; // ready with the new item
                }
            }
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


