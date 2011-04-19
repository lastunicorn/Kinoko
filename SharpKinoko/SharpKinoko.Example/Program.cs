﻿// SharpKinoko
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
using System.Threading;

namespace DustInTheWind.SharpKinoko.Example
{
    class Program
    {
        private const int sleepTime = 100;
        private const int repeatCount = 100;

        private static void Main(string[] args)
        {
            Console.WriteLine("C# Kinoko Usage Example");
            Console.WriteLine("===============================================================================");
            Console.WriteLine("This example is running a task that just sleeps for {0} milliseconds.", sleepTime);
            Console.WriteLine("Obs: The measured time is little bigger then the right one (around 1 milisecond");
            Console.WriteLine("bigger). It represents the measurement error cumulated from:");
            Console.WriteLine("  1) the time necessary to actually call the task method;");
            Console.WriteLine("  2) the measurement error of the Stopwatch class;");
            Console.WriteLine("  3) the error of the Thread.Sleep() method.");
            Console.WriteLine();

            // Create the testing environment.
            Kinoko kinoko = new Kinoko(new KinokoTask(Task), repeatCount);
            kinoko.BeforeTaskRun += new EventHandler<BeforeTaskRunEventArgs>(kinoko_BeforeTaskRun);
            kinoko.AfterTaskRun += new EventHandler<AfterTaskRunEventArgs>(kinoko_AfterTaskRun);

            // Run the test.
            kinoko.Run();

            // Collect the results.
            KinokoResult result = kinoko.Result;

            // Display the results.
            Console.WriteLine();
            Console.WriteLine("Average time: {0:#,##0.00} milisec", result.Average);

            Pause();
        }

        private static void kinoko_BeforeTaskRun(object sender, BeforeTaskRunEventArgs e)
        {
            Console.Write("Running: {0,2}", e.StepIndex);
        }

        private static void kinoko_AfterTaskRun(object sender, AfterTaskRunEventArgs e)
        {
            Console.WriteLine(" - {0:#,##0.00}", e.Time);
        }

        private static void Task()
        {
            Thread.Sleep(sleepTime);
        }

        private static void Pause()
        {
            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            Console.WriteLine();
        }
    }
}
