//Snake
//04.05.2018  Created By: Nico Jankowski, Lukas Mrziglod


using System;
using System.Collections.Generic;
using ListardDemo;
using System.Threading;
using System.IO;
using System.Text;

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
        const ConsoleColor gemColor = ConsoleColor.Yellow;
        const ConsoleColor textColor = ConsoleColor.Green;
        const ConsoleColor obstacleColor = ConsoleColor.Red;



        //Gameloop Variable
        bool continueloop = true;

        // User-Cursor with speed
        int posX = 20;          //startposition
        int posY = 20;          //Startposition
        int speedX = 0;         //richtung auf der X-Achse
        int speedY = -1;        //richtung auf der Y-Achse
        char symbol = 'o';      // Snakebody Symbol
        char headsymbol = 'Ö';  //Snakekopf Symbol
        int gameSpeed = 200;    //Spielgeschwindigkeit
        int ItemsCollected;     //Gesammelte Items
        bool running = true;    //Gibt an, ob das Programm läuft
        bool snakemoved = true; //Variable, die verhindert, dass mehrere Richtungswechsel innerhalb eines Spielschrittes stattfinden können
        int PunkteMultiplikator = 0; //Punkte Multiplikator, abhängig von Schwierigkeitstufe
        int punkte = 0;
        string path = "";


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
        Listard<Position> Obstacle = new Listard<Position>();



        //public void Neustarten()
        //{
        //    //bool success = false;
        //    string restartInput = "B";
        

        //    //while (success)
        //    //{
        //    try
        //    {
        //        Console.Clear();
        //        ShowText("Möchten Sie das Programm neustarten?", 20, 15, textColor);
        //        ShowText("(j/n)", 30, 16, textColor);

        //        restartInput = Convert.ToString(Console.ReadKey());
        //        restartInput.ToLower();

        //        if (restartInput == "j")
        //        {

        //            running = true;


        //        }
        //        if (restartInput == "n")
        //        {
        //            Environment.Exit(0);


        //        }



        //    }
        //    catch
        //    {

        //        //success = false;
        //    }

        //}


        public int MenuSpeed()
        {
            //Spielgeschwindigkeit zu beginn einstellen

            bool success = false;
            int schwierigkeitsstufe = 0;

            while (!success)
            {
                try
                {
                    Console.WriteLine("Bitte geben sie die gewünschte Schwierigkeitsstufe ein (1-10)");
                    schwierigkeitsstufe = Convert.ToInt32(Console.ReadLine());
                    success = true;
                }
                catch
                {
                    success = false;
                }
            }




            switch (schwierigkeitsstufe)
            {
                case 1:
                    PunkteMultiplikator = 1;
                    gameSpeed = 200;
                    break;

                case 2:
                    PunkteMultiplikator = 2;
                    gameSpeed = 180;
                    break;

                case 3:
                    PunkteMultiplikator = 3;
                    gameSpeed = 160;
                    break;

                case 4:
                    PunkteMultiplikator = 4;
                    gameSpeed = 140;
                    break;

                case 5:
                    PunkteMultiplikator = 5;
                    gameSpeed = 120;
                    break;

                case 6:
                    PunkteMultiplikator = 6;
                    gameSpeed = 100;
                    break;

                case 7:
                    PunkteMultiplikator = 7;
                    gameSpeed = 80;
                    break;

                case 8:
                    PunkteMultiplikator = 8;
                    gameSpeed = 60;
                    break;

                case 9:
                    PunkteMultiplikator = 9;
                    gameSpeed = 40;
                    break;

                case 10:
                    PunkteMultiplikator = 10;
                    gameSpeed = 20;
                    break;

                default:

                    break;

            }
            // Console Initialisieren
            Console.Title = "Snakerino";
            Console.SetWindowSize(DIM_X, DIM_Y + 1);
            Console.SetBufferSize(DIM_X, DIM_Y + 1);
            Console.BackgroundColor = backColor;
            Console.ForegroundColor = userColor;
            Console.Clear();
            return gameSpeed;




        }

        public void LoseScreen()
        {
            //Bei Spielende Ausgabe
            Console.Clear();
            ShowText("Leider verloren! ", 30, 20, obstacleColor);
            ShowText("Sie haben " + punkte + " Punkte erreicht", 25, 22, userColor);
            //Consolentöne
            Console.Beep(440, 500);
            Thread.Sleep(80);
            Console.Beep(392, 500);
            Thread.Sleep(80);
            Console.Beep(349, 500);
            Thread.Sleep(80);
            Console.Beep(329, 500);
            Thread.Sleep(2000);

            Rangliste();


            Console.Clear();




            // Wait user to terminate program using ENTER-key
            ShowText("Zum Beenden Drücken Sie ENTER", 25, 20, obstacleColor);
            Console.ReadLine();
            Environment.Exit(0);
        }


        public void Rangliste()
        {

            const int maxZahl = 10;
            int anzahl = 0;



            //Snake.Highscore[] highscore = new Snake.Highscore[maxZahl];
            path = @"C:\Users\soad_\Documents\Highscore.csv";// Directory.GetCurrentDirectory() + @"\Highscores5.csv";
            

            //while (!File.Exists(path))
            {
                if (!File.Exists(path))
                {
                    ShowText("Datei existiert nicht", 25, 20, obstacleColor);
                    Snake.Highscore[] highscores = new Snake.Highscore[maxZahl];
                    StreamWriter writer = new StreamWriter(path, false, new UTF8Encoding(true));


                    Snake.Highscore highscore = new Snake.Highscore();
                    highscores[0] = highscore;
                    highscores[0].CreateHighscoreFile(writer);
                    
                    writer.Close();
                    Console.WriteLine("Default Datei erstellt");
                    Thread.Sleep(2000);
                }
                else
                { 
                    
                    // ShowText("Datei existiert", 25, 20, obstacleColor);
                    //hier die CSV laden und die Scores vergleichen
                    StreamReader reader = new StreamReader(path, Encoding.UTF8);
                    Snake.Highscore[] highscores = new Snake.Highscore[maxZahl];
                      while (!reader.EndOfStream)
                    {
                        
                        // Console.WriteLine("\r\n\t"+reader.ReadLine());
                        // if (anzahl >= maxZahl) break;
                        highscores[anzahl++] = Snake.Highscore.Laden(reader);
                    }
                    

                    if (Int32.Parse(highscores[0].Score) <= punkte)
                    {
                        Console.WriteLine("Herzlichen Glückwunsch");
                        Console.WriteLine("Neuen Highscore Eingeben");
                        
                        while (anzahl < maxZahl)
                        {
                            Snake.Highscore highscore = new Snake.Highscore();
                            if (!highscore.Erfassen()) break;
                        highscores[anzahl++] = highscore;
                        }
                        reader.Close();
                        
                        StreamWriter writer = new StreamWriter(path, false, new UTF8Encoding(true));
                        highscores[1].Speichern(writer);
                        writer.Close();
                        anzahl = 0;
                        highscores = new Snake.Highscore[maxZahl];
                        

                    }


                    StreamReader reader2 = new StreamReader(path, Encoding.UTF8);
                   // Snake.Highscore[] highscores = new Snake.Highscore[maxZahl];
                    while (!reader2.EndOfStream)
                    {

                        // Console.WriteLine("\r\n\t"+reader.ReadLine());
                        // if (anzahl >= maxZahl) break;
                        highscores[anzahl++] = Snake.Highscore.Laden(reader2);
                    }

                    Console.WriteLine("\r\n\tHighscore anzeigen");
                    for (int i = 0; i < anzahl; i++)
                    {
                        highscores[i].Anzeigen();
                    }
                    reader.Close();

                    Thread.Sleep(2000);
                }
            }

            //Mit dem Streamreader die Highscore csv lesen
            //die Highscores vergleichen 
            //maximale anzahl an zeilen zu lassen
            //wenn der neue score höher ist als einer der highscores diese zeile überschreiben
            //dazu den Inhalt der CSV in einzelne Zeilen aufsplitten (1-10)
            //die einzelnen Felder Position, Name, Score in Array laden
            //Array Position [2] mit aktuellem Score vergleichen
            //
        }


        public void CollisionDetection()
        {
            for (int Snakelenght = SnakeBody.Count; Snakelenght > 2; Snakelenght--)
            {
                if (SnakeBody[0].X == SnakeBody[Snakelenght - 1].X && SnakeBody[0].Y == SnakeBody[Snakelenght - 1].Y)
                {
                    LoseScreen();
                }

            }
            for (int Obstaclelenght = Obstacle.Count; Obstaclelenght > 0; Obstaclelenght--)
            {
                if (SnakeBody[0].X == Obstacle[Obstaclelenght - 1].X && SnakeBody[0].Y == Obstacle[Obstaclelenght - 1].Y)
                {
                    LoseScreen();
                }
            }


        }
        public void CreateSnake()
        {
            //Snake an zufälliger Position erstellen

            int Xp = random.Next(5, DIM_X - 5);
            int Yp = random.Next(5, DIM_Y - 5);
            for (int Snakelenght = 0; Snakelenght < 5; ++Snakelenght)
            {

                Yp = Yp + Snakelenght;
                SnakeBody.Add(new Position(Xp, Yp));

            }
        }

        public void CreateGems()
        {
            //Sterne erstellen

            int x = random.Next(1, DIM_X - 1);
            int y = random.Next(1, DIM_Y - 1);
            char Gem = '*';


            // Falls ein Stern ein Hinderniss überschreibt

            for (int Obstaclelenght = Obstacle.Count; Obstaclelenght > 0; Obstaclelenght--)
            {
                if ( x == Obstacle[Obstaclelenght - 1].X && y == Obstacle[Obstaclelenght - 1].Y)
                {
                    x = random.Next(1, DIM_X - 1);
                    y = random.Next(1, DIM_Y - 1);
                }
            }

            //Sternposition in Listard übernehmen (Erweiterbar auf mehrere Sterne)
            Gems.Add(new Position(x, y));
           
            
            ShowSymbol(Gem, Gems[0].X, Gems[0].Y, gemColor);
        }

        public void CreateObstacle()
        {
            for (int i = 0; i < 25; i++)
            {
                int x = random.Next(1, DIM_X - 1);
                int y = random.Next(1, DIM_Y - 1);
                char Obstaclesymbol = '#';
                //Obstaclepositionen in Listard übernehmen (Erweiterbar auf mehrere Sterne)
                Obstacle.Add(new Position(x, y));
                ShowSymbol(Obstaclesymbol, Obstacle[i].X, Obstacle[i].Y, obstacleColor);
            }

            //Kein Obstacle erstellen, wenn es mit den Items oder dem Snakekopf Kollidiert
            for (int Obstaclelenght = Obstacle.Count; Obstaclelenght > 0; Obstaclelenght--)
            {
                if (SnakeBody[0].X == Obstacle[Obstaclelenght - 1].X && SnakeBody[0].Y == Obstacle[Obstaclelenght - 1].Y || Gems[0].X == Obstacle[Obstaclelenght - 1].X && Gems[0].Y == Obstacle[Obstaclelenght - 1].Y)
                {
                    Obstacle.Delete(Obstaclelenght - 1);
                }
            }


        }

        public void SnakeControl()
        {
            
            // Check whether the user did some input
            if (Console.KeyAvailable && snakemoved == true )
            {
                // User input - react to the users input

                ConsoleKeyInfo cki = Console.ReadKey();
                // Fetch first char of logical key name and set user symbol
                string keyName = cki.Key.ToString();

                // Richtungsänderung
                if (cki.Key == ConsoleKey.UpArrow)
                {
                    if (speedY < 1)
                    {
                        speedY = -1;
                        speedX = 0;
                    }
                    snakemoved = false;
                }
                else if (cki.Key == ConsoleKey.DownArrow)
                {
                    if (speedY > -1)
                    {
                        speedY = 1;
                        speedX = 0;
                    }
                    snakemoved = false;
                }
                else if (cki.Key == ConsoleKey.LeftArrow)
                {
                    if (speedX < 1)
                    {
                        speedX = -1;
                        speedY = 0;
                    }
                    snakemoved = false;
                }
                else if (cki.Key == ConsoleKey.RightArrow)
                {
                    if (speedX > -1)
                    {
                        speedX = 1;
                        speedY = 0;
                    }
                    snakemoved = false;
                }


            }
            else
            {
                // No user input - the gaming loop does a time step

                //Move Snake
                SnakeMove(speedX, speedY);
                snakemoved = true;
                DrawSnake();
                // Wait some time
                System.Threading.Thread.Sleep(gameSpeed);
                DeleteSnake();
            }

        }
        public void SnakeMove(int speedX, int speedY)
        {

            //Snake Körper bewegen
            for (int Snakelenght = SnakeBody.Count; Snakelenght > 1; Snakelenght--)
            {
                SnakeBody[Snakelenght - 1] = SnakeBody[Snakelenght - 2];

            }

            //Snake Kopf bewegen
            Position newHeadPos = new Position(SnakeBody.Get(0).X, SnakeBody.Get(0).Y);

            newHeadPos.X = (newHeadPos.X + speedX) % DIM_X;
            newHeadPos.Y = (newHeadPos.Y + speedY) % DIM_Y;

            //Position an Spielfeld anpassen
            if (newHeadPos.Y < 0)
                newHeadPos.Y += newHeadPos.Y + DIM_Y;

            if (newHeadPos.X < 0)
                newHeadPos.X += newHeadPos.X + DIM_X;

            SnakeBody.Set(newHeadPos, 0);

            CollisionDetection();



        }

        public void DrawSnake()
        {
            //Snakeelemente anzeigen
            for (int Snakelenght = SnakeBody.Count; Snakelenght > 1; Snakelenght--)
            {
                ShowSymbol(symbol, SnakeBody[Snakelenght - 1].X, SnakeBody[Snakelenght - 1].Y, userColor);

            }
            ShowSymbol(headsymbol, SnakeBody[0].X, SnakeBody[0].Y, userColor);
        }
        public void DeleteSnake()
        {
            //Snakebewegung korrigieren
            //Index of last Position
            int Snakelastpos = SnakeBody.Count - 1;
            ShowSymbol(symbol, SnakeBody[Snakelastpos].X, SnakeBody[Snakelastpos].Y, backColor);
        }



        public void GameRun()
        {
            if (running)
            {

                Console.Clear();

                MenuSpeed();

                //ASCII ART
                Console.WriteLine("\n\n");
                var arr = new[]
                {
            @"                                             88                   ",
            @"                                             88                   ",
            @"                                             88                   ",
            @"            ,adPPYba, 8b,dPPYba,  ,adPPYYba, 88   ,d8  ,adPPYba,  ",
            @"            I8[    ""  88P'   ``8a ""      `Y8 88 ,a8`  a8P_____88  ",
            @"             ``Y8ba,  88       88 ,adPPPPP88 8888    [8PP```````  ",
            @"            aa    ]8I 88       88 88,    ,88 88``Yba, `8b,   ,aa  ",
            @"            ``YbbdP`' 88       88 `´8bbdP´Y8 88   `Y8a `´Ybbd8´'  ",

            };


                //ASCII ART ausgeben
                foreach (string line in arr)
                    Console.WriteLine(line);

                //Gameinfo
                ShowText("Nutze die Pfeiltasten zum Navigieren    ", 22, 15, textColor);
                ShowText("Sammeln Sie die Sterne und weichen Sie den Hindenissen aus", 12, 17, textColor);
                // Warten, um Steuerung zu erklären
                System.Threading.Thread.Sleep(INFOSCREEN_TIME);
                Console.Clear();



                //Spielfeld aufbauen

                //Snake erstellen
                CreateSnake();
                //Sterne erstellen
                CreateGems();
                //Hindernisse erstellen
                CreateObstacle();

                //Spielschleife
                while (continueloop == true)
                {
                    // Show user symbol
                    if (punkte == 1)
                    {

                        ShowText("" + punkte + " Punkt erreicht ", 1, DIM_Y, gemColor);
                    }
                    else ShowText("" + punkte + " Punkte erreicht ", 1, DIM_Y, gemColor);

                    
                    
                    SnakeControl();

                    // Check whether the user hits an item


                    if (Gems[0].X == SnakeBody[0].X && Gems[0].Y == SnakeBody[0].Y)
                    {
                        ItemsCollected++;
                        punkte = ItemsCollected * PunkteMultiplikator;
                        Console.Beep(1108, 70);

                        Gems.Delete(0);
                        CreateGems();
                        //Länger werden

                        SnakeBody.Add(new Position(posX, posY));

                    }


                }
            }

        }

        private int GetCount()
        {
            //Anzahl der gesammelten Items zählen
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


