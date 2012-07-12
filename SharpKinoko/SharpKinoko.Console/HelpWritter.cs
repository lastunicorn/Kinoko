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
    public class HelpWritter
    {
        private readonly IConsole console;
        private readonly GuiHelpers guiHelpers;

        public HelpWritter(IConsole console, GuiHelpers guiHelpers)
        {
            if (console == null)
                throw new ArgumentNullException("console");

            if (guiHelpers == null)
                throw new ArgumentNullException("guiHelpers");

            this.console = console;
            this.guiHelpers = guiHelpers;
        }

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

        public void WriteTaskTitle(KinokoSubject subject)
        {
            console.WriteLine();
            console.Write("Measuring subject: ");
            using (new TemporaryColorSwitcher(console, ConsoleColor.White))
            {
                console.WriteLine(subject.Method.Name);
            }
        }

        public void WriteTaskResult(KinokoResult result)
        {
            console.WriteLine();
            console.Write("Average time: ");
            using (new TemporaryColorSwitcher(console, ConsoleColor.White))
            {
                console.WriteLine("{0:#,##0.00} milisec", result.Average);
            }
        }

        public void WriteLoadingAssembly(string assemblyFileName)
        {
            console.Write("Start measuring subjects from assembly ");
            using (new TemporaryColorSwitcher(console, ConsoleColor.White))
            {
                console.WriteLine(assemblyFileName);
            }
        }

        public void WriteShortHelp()
        {
            console.WriteLine("Expected: KinokoConsole <assemblyFileName>");
        }
    }
}

