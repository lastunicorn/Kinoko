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
using System.Reflection;

namespace DustInTheWind.SharpKinoko.SharpKinokoConsole.ConsoleControls
{
    /// <summary>
    /// Contains a few usefull methods that help interact with the console.
    /// </summary>
    public class UI
    {
        /// <summary>
        /// The console used to write text into.
        /// </summary>
        private readonly IConsole console;

        public IConsole Console
        {
            get { return console; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UI"/> class.
        /// </summary>
        /// <param name="console">The console used to write text into</param>
        public UI(IConsole console)
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

        public int GetWindowWidth()
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

        /// <summary>
        /// Writes to the console the Sharp Kinoko name and the version.
        /// </summary>
        public void WriteKinokoHeader()
        {
            using (new TemporaryColorSwitcher(console, ConsoleColor.Green))
            {
                Assembly assembly = Assembly.GetAssembly(typeof(Kinoko));
                console.WriteLine("Kinoko Console ver. {0}", assembly.GetName().Version.ToString(3));
                WriteFullLine('=');
                console.WriteLine();
            }
        }

        /// <summary>
        /// Writes to the console the task title that is about to be run.
        /// </summary>
        /// <param name='task'>The task that is about to be run.</param>
        public void WriteTaskTitle(KinokoTask task)
        {
            console.WriteLine();
            console.Write("Measuring subject: ");
            using (new TemporaryColorSwitcher(console, ConsoleColor.White))
            {
                console.WriteLine(task.Subject.Method.Name);
            }
        }

        /// <summary>
        /// Writes to the console the result of the task that was run.
        /// </summary>
        /// <param name='result'>The result of the run of a task.</param>
        public void WriteTaskResult(KinokoResult result)
        {
            console.WriteLine();
            console.Write("Average time: ");
            using (new TemporaryColorSwitcher(console, ConsoleColor.White))
            {
                console.WriteLine("{0:#,##0.00} milisec", result.Average);
            }
        }

        /// <summary>
        /// Writes to the console a line of test to specify what assembly is currently loading.
        /// </summary>
        /// <param name='assemblyFileName'>The file name of the assembly that is loading.</param>
        public void WriteAssemblyLoadingInformation(string assemblyFileName)
        {
            console.Write("Start measuring subjects from assembly ");
            using (new TemporaryColorSwitcher(console, ConsoleColor.White))
            {
                console.WriteLine(assemblyFileName);
            }
        }
    }
}

