using System;

namespace DustInTheWind.SharpKinokoConsole
{
    public interface IConsole
    {
        int CursorTop { get; set; }

        int CursorLeft { get; set; }

        int WindowWidth { get; set; }

        ConsoleColor ForegroundColor { get; set; }

        void Write(string value);

        void WriteLine(string value);

        void WriteLine();

        void WriteLine(string format, params object[] args);

        ConsoleKeyInfo ReadKey(bool intercept);
    }
}

