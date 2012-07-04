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
        private const int repeatMeasurementCount = 10;
        private static ProgressBar progressBar;

        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            WriteHeader();

            if (args == null || args.Length != 1)
            {
                DisplayError("Invalid number of arguments.");
                Console.WriteLine("Expected: KinokoConsole <assemblyFileName>");
                return;
            }

            try
            {
                Kinoko kinoko = CreateKinoko();
                RunTasksFromAssembly(kinoko, args[0]);
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }

            Pause();
        }

        static Kinoko CreateKinoko()
        {
            Kinoko kinoko = new Kinoko();

            kinoko.Measured += HandleKinokoMeasured;
            kinoko.TaskRunning += HandleKinokoTaskRunning;
            kinoko.TaskRun += HandleKinokoTaskRun;

            return kinoko;
        }

        static ISubjectsProvider CreateSubjectsProvider(string assemblyFilePath)
        {
            AssemblySubjectsProvider subjectsProvider = new AssemblySubjectsProvider();
            string fullPath = Path.GetFullPath(assemblyFilePath);
            Assembly assembly = Assembly.LoadFile(fullPath);
            subjectsProvider.Load(assembly);

            return subjectsProvider;
        }

        static ProgressBar CreateProgressBar()
        {
            ProgressBar progressBar = new ProgressBar();
            progressBar.Width = GetWindowWidth();
            progressBar.ForegroundColor = ConsoleColor.Yellow;
            return progressBar;
        }

        #region Kinoko event handlers

        static void HandleKinokoTaskRunning(object sender, TaskRunningEventArgs e)
        {
            Console.WriteLine();
            Console.Write("Measuring target: ");
            using (new TemporaryColorSwitcher(ConsoleColor.White))
            {
                Console.WriteLine(((Delegate)e.Subject).Method.Name);
            }

            progressBar = CreateProgressBar();
            progressBar.Display();
        }

        static void HandleKinokoTaskRun(object sender, TaskRunEventArgs e)
        {
            Console.WriteLine();
            Console.Write("Average time: ");
            using (new TemporaryColorSwitcher(ConsoleColor.White))
            {
                Console.WriteLine("{0:#,##0.00} milisec", e.Result.Average);
            }
        }

        private static void HandleKinokoMeasured(object sender, MeasuredEventArgs e)
        {
            int newPercent = CalculatePercentage(e.StepIndex + 1);

            progressBar.SetProgress(newPercent);
        }

        #endregion

        static void RunTasksFromAssembly(Kinoko kinoko, string assemblyFileName)
        {
            Console.Write("Start measuring targets from assembly ");
            using (new TemporaryColorSwitcher(ConsoleColor.White))
            {
                Console.WriteLine(assemblyFileName);
            }

            ISubjectsProvider subjectsProvider = CreateSubjectsProvider(assemblyFileName);
            IList<KinokoResult> results = kinoko.Run(subjectsProvider, repeatMeasurementCount);
        }

        static int CalculatePercentage(int index)
        {
            return (index * 100) / repeatMeasurementCount;
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

        #region GUI Helpers

        private static int GetWindowWidth()
        {
            return  Console.WindowWidth - 1;
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
            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
            Console.WriteLine();
        }

        #endregion
    }
}
