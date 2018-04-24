/*
 * fn@gso-koeln.de 2018
 */
using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ListardDemo
{
    public class UnitTest
    {
        public void AutoRun() {
            string className = this.GetType().Name;
            Console.WriteLine(className);

            // use Reflection to get method information
            MethodInfo[] miList = this.GetType().GetMethods();
            foreach (MethodInfo mi in miList) {
                if (!mi.Name.StartsWith("Test")) continue; // Filter

                try {
                    Console.Write(" - " + mi.Name + " ... ");

                    // Testmethode aufrufen
                    mi.Invoke(this, null);

                    WriteLine(ConsoleColor.Green, "erfolgreich");
                } catch (TargetInvocationException exc) {
                    // Fehler anzeigen
                    Exception ie = exc.InnerException; // nested error info
                    string msg = ie.Message;
                    string info = ErrorInfo(ie, "   * ") + "\n";
                    WriteLine(ConsoleColor.Red, "Fehler in " + mi.Name + 
                        "\n - Exception: " + ie.GetType().Name + 
                        "\n - Message:   " + msg +
                        "\n" + info);
                }
            }
        }

        protected string ErrorInfo(Exception exc, string indent = null) {
            string info = null;
            try {
                // use regular expressions to parse stack trace
                //string re = @"\\(?<info>\w*\.cs:\w* [0-9]*)\.";
                //info = Regex.Match(exc.StackTrace, re).Groups["info"].Value;
                string re = @"\w+\.\w+\:\w+ [0-9]+\.";
                MatchCollection mc = Regex.Matches(exc.StackTrace, re);
                for(int i = 0; i < mc.Count; i++) {
                    string temp = info;
                    info = indent + mc[i].Value;
                    if (i > 0) info += "\n" + temp;
                }
            } catch { }
            return info;
        }

        protected void WriteLine(ConsoleColor color, string text) {
            Write(color, text + "\n");
        }

        protected void Write(ConsoleColor color, string text) {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = oldColor;
        }

        public void TestUnitTest() {
        }
    }
}
