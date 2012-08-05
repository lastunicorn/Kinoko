// SharpKinoko
// Copyright (C) 2010 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;

namespace DustInTheWind.SharpKinoko.SharpKinokoConsole.ConsoleControls
{
    /// <summary>
    /// Contains methods that ... what?
    /// </summary>
    public class GuiHelpers
    {
        /// <summary>
        /// The console used to write text into.
        /// </summary>
        private readonly IConsole console;

        /// <summary>
        /// Initializes a new instance of the <see cref="GuiHelpers"/> class.
        /// </summary>
        /// <param name="console">The console used to write text into</param>
        public GuiHelpers(IConsole console)
        {
            if (console == null)
                throw new ArgumentNullException("console");

            this.console = console;
        }
        
        /// <summary>
        /// Displays an exception into the console.
        /// </summary>
        /// <param name="ex">The exception to be displayed.</param>
        public void DisplayError(Exception ex)
        {
            DisplayError(ex.Message);
        }

        /// <summary>
        /// Displays an error message into the console.
        /// </summary>
        /// <param name="text">The error message to be displayed.</param>
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

        /// <summary>
        /// Pauses the console until the user presses a key.
        /// </summary>
        public void Pause()
        {
            console.WriteLine();
            console.Write("Press any key to continue...");
            console.ReadKey(true);
            console.WriteLine();
        }

        private int GetWindowWidth()
        {
            return  console.WindowWidth - 1;
        }

        /// <summary>
        /// Writes a full line of the console with the specified character.
        /// </summary>
        /// <param name="c">The character used to fill the line.</param>
        public void  WriteFullLine(char c)
        {
            console.WriteLine(new String(c, GetWindowWidth()));
        }
    }
}

