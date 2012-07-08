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
using System.Collections.Generic;
using System.Reflection;
using DustInTheWind.SharpKinoko;
using System.IO;

namespace DustInTheWind.SharpKinokoConsole
{
    class MainClass
    {
        static void Test()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(i);
            }

            int top = Console.WindowTop;
            int left = Console.WindowLeft;
            Console.WriteLine("{0} : {1}", top, left);

            Console.Write("Alez");
        }

        public static void Main(string[] args)
        {
            try
            {
//            Test();
//            return;

                IConsole console = new ConsoleWrapper();
                KinokoConsole kinokoConsole = new KinokoConsole(console, args);
                kinokoConsole.Start();
            }
            catch (Exception ex)
            {
                IConsole console = new ConsoleWrapper();
                GuiHelpers guiHelpers = new GuiHelpers(console);
                guiHelpers.DisplayError(ex);
            }
            finally
            {
                IConsole console = new ConsoleWrapper();
                GuiHelpers guiHelpers = new GuiHelpers(console);
                guiHelpers.Pause();
            }
        }
    }

}
