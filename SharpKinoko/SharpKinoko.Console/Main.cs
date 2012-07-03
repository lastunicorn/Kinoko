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

namespace DustInTheWind.SharpKinokoConsole
{
    class MainClass
    {
        private const int repeatMeasurementCount = 10;
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
                progressBarCharCount = GetWindowWidth() - 2;

                Kinoko kinoko = CreateKinoko();
                RunTasksFromAssembly(kinoko, args[0]);
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }

            Pause();
        }

        static void RunTasksFromAssembly(Kinoko kinoko, string assemblyFileName)
        {
            using (new TemporaryColorSwitcher(ConsoleColor.Blue))
            {
                Console.WriteLine("Start measuring targets from assembly " + assemblyFileName);
            }

            AssemblySubjectsProvider subjectsProvider = CreateTasksProvider(assemblyFileName);
            IList<KinokoResult> results = kinoko.Run(subjectsProvider, repeatMeasurementCount);
            DisplayResults(results);
        }

        static void HandleKinokoTaskRunning(object sender, TaskRunningEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine(string.Format("Measuring target: {0} ", ((Delegate)e.Subject).Method.Name));
            WriteEmptyProgressBar();

            percentCompleted = 0;
        }

        static void HandleKinokoTaskRun(object sender, EventArgs e)
        {
            Console.WriteLine();
        }

        private static void HandleKinokoMeasured(object sender, MeasuredEventArgs e)
        {
            int newPercent = CalculatePercent(e.StepIndex + 1);

            if (newPercent > percentCompleted)
            {
                WriteProgressChars(newPercent - percentCompleted);

                percentCompleted = newPercent;
            }
        }

        static void WriteProgressChars(int charCount)
        {
            using (new TemporaryColorSwitcher(ConsoleColor.Yellow))
            {
                Console.Write(new string('*', charCount));
            }
        }

        static AssemblySubjectsProvider CreateTasksProvider(string assemblyFilePath)
        {
            AssemblySubjectsProvider subjectsProvider = new AssemblySubjectsProvider();
            Assembly assembly = Assembly.LoadFile(assemblyFilePath);
            subjectsProvider.Load(assembly);

            return subjectsProvider;
        }

        static Kinoko CreateKinoko()
        {
            Kinoko kinoko = new Kinoko();

            kinoko.Measured += HandleKinokoMeasured;
            kinoko.TaskRunning += HandleKinokoTaskRunning;
            kinoko.TaskRun += HandleKinokoTaskRun;

            return kinoko;
        }

        static void WriteEmptyProgressBar()
        {
            using (new TemporaryColorSwitcher(ConsoleColor.Yellow))
            {
                int top = Console.CursorTop;
                int left = Console.CursorLeft;
                Console.Write("[");
                Console.Write(new String(' ', progressBarCharCount));
                Console.Write("]");
                Console.CursorTop = top;
                Console.CursorLeft = left + 1;
            }
        }

        static void DisplayResults(IList<KinokoResult> results)
        {
            foreach (KinokoResult result in results)
            {
                Console.WriteLine();
                Console.WriteLine("Average time: {0:#,##0.00} milisec", result.Average);
            }
        }

        static int CalculatePercent(int index)
        {
            return (index * progressBarCharCount) / repeatMeasurementCount;
        }

        static void WriteHeader()
        {
            using (new TemporaryColorSwitcher(ConsoleColor.Green))
            {
                Console.WriteLine("Kinoko Console");
                WriteFullLine('=');
                Console.WriteLine();
            }
        }

        static void  WriteFullLine(char c)
        {
            Console.WriteLine(new String(c, GetWindowWidth()));
        }

        static void DisplayError(Exception ex)
        {
            DisplayError(ex.Message);
        }

        static void DisplayError(string text)
        {
            using (new TemporaryColorSwitcher(ConsoleColor.Red))
            {
                Console.WriteLine();
                Console.WriteLine("Error");
                WriteFullLine('-');
                Console.WriteLine(text);
                Console.WriteLine();
            }
        }

        private static void Pause()
        {
            using (new TemporaryColorSwitcher(ConsoleColor.Blue))
            {
                Console.WriteLine();
                Console.Write("Press any key to continue...");
                Console.ReadKey(true);
                Console.WriteLine();
            }
        }
    }
}
