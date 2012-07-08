using System;

namespace DustInTheWind.SharpKinokoConsole
{
    public class ConsoleWrapper : IConsole
    {
        public int CursorTop
        {
            get { return Console.CursorTop; }
            set { Console.CursorTop = value; }
        }

        public int CursorLeft
        {
            get { return Console.CursorLeft; }
            set { Console.CursorLeft = value; }
        }

        public int WindowWidth
        {
            get { return Console.WindowWidth; }
            set { Console.WindowWidth = value; }
        }

        public ConsoleColor ForegroundColor
        {
            get { return Console.ForegroundColor; }
            set { Console.ForegroundColor = value; }
        }

        public void Write(string value)
        {
            Console.Write(value);
        }

        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }

        public void WriteLine(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public ConsoleKeyInfo ReadKey(bool intercept)
        {
            return Console.ReadKey(intercept);
        }
    }

}

