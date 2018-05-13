using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Snake
{
    class Highscore
    {
        public string Position;
        public string Name;
        public string Score;

        public Highscore() //Konstruktur
        { 
        

        }


        // wenn kein Highscore gebrochen wurde highscore anzeigen, sonst vorher überschreiben und dann anzeigen
        //Erstmal mit einem Highscore anfangen dann auf z.B. 10 erweitern
        public bool CreateHighscoreFile(StreamWriter writer)
        {
            //Highscore[] highscore = new Highscore();
            
            Position = "1";
            Name = "default";
            Score = "0";

            writer.Write(Position + ";");
            writer.Write(Name + ";");
            writer.Write(Score + "\r\n");
            return true;

        }
        public bool Erfassen()
        {
            string position = TextEinlesen(" * Position");
            if (position.Trim() == "")
            {
                // end of input
                this.Position = null;
                this.Name = null;
                this.Score = null;
                return false;
            }
            else
            {
                this.Position = Position;
                this.Name = TextEinlesen(" * Name");
                this.Score = TextEinlesen(" * Score");
                return true;
            }
        }

        public static Highscore Laden(StreamReader reader)
        {
            string zeile = reader.ReadLine();
            string[] werte = zeile.Split(';');
            Highscore highscore = new Highscore(); // neue Person im Heap-Speicher
            highscore.Position = werte[0];
            highscore.Name = werte[1];
            highscore.Score = werte[2];
            return highscore;
        }

      

        public bool Speichern(StreamWriter writer)
        {
            writer.Write(this.Position + ";");
            writer.Write(this.Name + ";");
            writer.Write(this.Score + "\r\n"); // Zeilenumbruch ASCII: CR, LF
            return true;
        }

        public void Anzeigen()
        {
            Console.WriteLine(" - Position    : " + this.Position + " - Name   : " + this.Name + " - Score : " + this.Score);
            Console.WriteLine();
            Console.WriteLine("Weiter mit Enter");
            Console.ReadLine();
        }

        private static string TextEinlesen(string prompt)
        {
            Console.Write(prompt + " : ");
            return Console.ReadLine();
        }
    }
}
