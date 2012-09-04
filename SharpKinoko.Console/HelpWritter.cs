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
using DustInTheWind.SharpKinoko.SharpKinokoConsole.ConsoleControls;

namespace DustInTheWind.SharpKinoko.SharpKinokoConsole
{
    /// <summary>
    /// Provides methods that write help information to the user.
    /// </summary>
    public class HelpWritter
    {
        /// <summary>
        /// The console where the information is written.
        /// </summary>
        private readonly IConsole console;

        /// <summary>
        /// Contains a few usefull methods that help interact with the console.
        /// </summary>
        private readonly UI guiHelpers;

        /// <summary>
        /// Initializes a new instance of the <see cref="DustInTheWind.SharpKinoko.SharpKinokoConsole.HelpWritter"/> class.
        /// </summary>
        /// <param name='console'>The console where the information will be written.</param>
        /// <param name='guiHelpers'>Contains a few usefull methods that help interact with the console.</param>
        /// <exception cref='ArgumentNullException'>
        /// Is thrown when an argument passed to a method is invalid because it is <see langword="null" /> .
        /// </exception>
        public HelpWritter(IConsole console, UI guiHelpers)
        {
            if (console == null)
                throw new ArgumentNullException("console");

            if (guiHelpers == null)
                throw new ArgumentNullException("guiHelpers");

            this.console = console;
            this.guiHelpers = guiHelpers;
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
                guiHelpers.WriteFullLine('=');
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

