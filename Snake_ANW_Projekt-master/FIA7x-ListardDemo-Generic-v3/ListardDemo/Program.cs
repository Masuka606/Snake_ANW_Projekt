/*
 * fn@gso-koeln.de 2018
 */
using System;

namespace ListardDemo
{
    class Program
    {
        static void Main(string[] args) {
            Console.WriteLine("ListardDemo\n");

            new TestListardInt().AutoRun();
            new TestListardPosition().AutoRun();

            Console.WriteLine("\nEnde.");
            Console.ReadLine();
        }
    }
}
