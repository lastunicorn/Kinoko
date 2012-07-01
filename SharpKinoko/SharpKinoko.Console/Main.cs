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

namespace DustInTheWind.KinokoConsole
{
    class MainClass
    {
        private const int repeatMeasurementCount = 100;
        private static int percentCompleted;
        private static int progressBarCharCount = 50;

        private static int GetWindowWidth()
        {
            return  Console.WindowWidth - 1;
        }

        public static void Main(string[] args)
        {
            WriteHeader();
         
            if (args == null || args.Length != 1)
            {
                DisplayError("Invalid number of arguments.");
                Console.WriteLine("Expected: KinokoConsole <assemblyFileName>");
                return;
            }

            try
            {
                AssemblyTasksProvider tasksProvider = CreateTasksProvider(args[0]);
                Kinoko kinoko = CreateKinoko();

                progressBarCharCount = GetWindowWidth() - 2;
                ConsoleColor oldColor = SetColor(ConsoleColor.Blue);
                Console.WriteLine("Start measuring tasks from assembly " + args[0]);
                SetColor(oldColor);

                IList<KinokoResult> results = RunTasks(kinoko, tasksProvider);
             
                DisplayResults(results);
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
         
            Pause();
        }

        static void HandleKinokoTaskRunning(object sender, EventArgs e)
        {
            Console.WriteLine();
            //Console.WriteLine(string.Format("Running task: {0} ", ((Delegate)task).Method.Name));
            Console.WriteLine(string.Format("Running task: {0} ", "???"));
            WriteEmptyProgressBar();

            percentCompleted = 0;
        }

        static void HandleKinokoTaskRun(object sender, EventArgs e)
        {
            Console.WriteLine();
        }

        static AssemblyTasksProvider CreateTasksProvider(string assemblyFilePath)
        {
            AssemblyTasksProvider tasksProvider = new AssemblyTasksProvider();
            Assembly assembly = Assembly.LoadFile(assemblyFilePath);
            tasksProvider.Load(assembly);
            return tasksProvider;
        }

        static Kinoko CreateKinoko()
        {
            Kinoko kinoko = new Kinoko();

            kinoko.Measured += HandleMeasured;
            kinoko.TaskRunning += HandleKinokoTaskRunning;
            kinoko.TaskRun += HandleKinokoTaskRun;

            return kinoko;
        }

        static IList<KinokoResult> RunTasks(Kinoko kinoko, ITasksProvider tasksProvider)
        {
            return kinoko.Run(tasksProvider, repeatMeasurementCount);
        }

        static void WriteEmptyProgressBar()
        {
            ConsoleColor oldColor = SetColor(ConsoleColor.Yellow);

            int top = Console.CursorTop;
            int left = Console.CursorLeft;
            Console.Write("[");
            Console.Write(new String(' ', progressBarCharCount));
            Console.Write("]");
            Console.CursorTop = top;
            Console.CursorLeft = left + 1;

            SetColor(oldColor);
        }

        static void DisplayResults(IList<KinokoResult> results)
        {
            foreach (KinokoResult result in results)
            {
                Console.WriteLine();
                Console.WriteLine("Average time: {0:#,##0.00} milisec", result.Average);
            }
        }

        private static void HandleMeasured(object sender, MeasuredEventArgs e)
        {
            int percent = (e.StepIndex * progressBarCharCount) / repeatMeasurementCount;

            if (percent != percentCompleted)
            {
                ConsoleColor oldColor = SetColor(ConsoleColor.Yellow);

                percentCompleted = percent;
                Console.Write("*");

                SetColor(oldColor);
            }
        }

        static void WriteHeader()
        {
            ConsoleColor oldColor = SetColor(ConsoleColor.Green);

            Console.WriteLine("Kinoko Console");
            WriteFullLine('=');
            Console.WriteLine();

            SetColor(oldColor);
        }

        static void  WriteFullLine(char c)
        {
            Console.WriteLine(new String(c, GetWindowWidth()));
        }

        static ConsoleColor SetColor(ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            return oldColor;
        }

        static void DisplayError(Exception ex)
        {
            DisplayError(ex.Message);
        }

        static void DisplayError(string text)
        {
            ConsoleColor oldColor = SetColor(ConsoleColor.Red);

            Console.WriteLine();
            Console.WriteLine("Error");
            WriteFullLine('-');
            Console.WriteLine(text);
            Console.WriteLine();

            Console.ForegroundColor = oldColor;
        }

        private static void Pause()
        {
            SetColor(ConsoleColor.Blue);

            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
            Console.WriteLine();
        }
    }
}
