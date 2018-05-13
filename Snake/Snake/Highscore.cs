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
            //string path = Directory.GetCurrentDirectory();

            //if (File.Exists(path+"Highscore.csv" )
            //    {
            //    // do something
            //    // streamwriter.Readfile

            //     }
            //    else
            //    {
            //    //do something else
            //    //streamwriter.writefile
            //    //mit default highscore position =1; Name "";score "0";
            //    //StreamWriter = new StreamWriter("Highscore.csv", false, new UTF8Encoding(true));
            //    // hier keine abfrage ob highscore.csv existiert sondern in der highscore methode, hier nur einschreiben von default oder neuem higscore implementieren


            //    }

            this.Position = "1";
            this.Name = "";
            this.Score = "0";
            writer.Write(this.Position + ";");
            writer.Write(this.Name + ";");
            writer.Write(this.Score + "\r\n");
            return true;

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
            Console.WriteLine(" - Position    : " + this.Position);
            Console.WriteLine(" - Nachname   : " + this.Name);
            Console.WriteLine(" - Score : " + this.Score);
            Console.WriteLine();
        }

    }
}
