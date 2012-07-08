using System;

namespace DustInTheWind.SharpKinokoConsole
{
    public class GuiHelpers
    {
        private IConsole console;

        public GuiHelpers(IConsole console)
        {
            if (console == null)
                throw new ArgumentNullException("console");

            this.console = console;
        }

        public void DisplayError(Exception ex)
        {
            DisplayError(ex.Message);
        }

        public void DisplayError(string text)
        {
            using (new TemporaryColorSwitcher(console, ConsoleColor.Red))
            {
                console.WriteLine();
                console.WriteLine("Error");
                WriteFullLine('-');
                console.WriteLine(text);
                console.WriteLine();
            }
        }

        public void Pause()
        {
            console.WriteLine();
            console.Write("Press any key to continue...");
            console.ReadKey(true);
            console.WriteLine();
        }

        public int GetWindowWidth()
        {
            return  console.WindowWidth - 1;
        }

        public void  WriteFullLine(char c)
        {
            console.WriteLine(new String(c, GetWindowWidth()));
        }
    }
}

