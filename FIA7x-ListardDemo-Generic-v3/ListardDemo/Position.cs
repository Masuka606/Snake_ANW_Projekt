/*
 * fn@gso-koeln.de 2018
 */
using System;

namespace ListardDemo
{
    // 2D position in a coordinate system
    public class Position
    {
        // creates a position upon given int coordinates
        public Position(int x, int y) {
            this.X = x;
            this.Y = y;
        }

        // creates a position upon given string "x|y" e.g. "17|22"
        public Position(string text) {
            string[] parts = text.Split('|');
            if (parts.Length != 2) throw new FormatException();
            this.X = int.Parse(parts[0]);
            this.Y = int.Parse(parts[1]);
        }


        public int getX
        {
            get { return X; }
        }


        // positions X-coordinate
        public int X;

        // positions Y-coordinate
        public int Y;

        // position as string "x|y" e.g. "17|22"
        public string Text { get { return X + "|" + Y; } }
    }
}
