using System;
using System.Collections.Generic;
using System.Reflection;
using DustInTheWind.KinokoConsole;
using DustInTheWind.SharpKinoko;
using System.Text;

namespace DustInTheWind.KinokoConsole
{
	class MainClass
	{
		private const int repeatMeasurementCount = 100;
		private static int percentCompleted;
        private static int progressBarCharCount = 50;

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
                AssemblyTasksProvider tasksProvider = new AssemblyTasksProvider();
                Assembly assembly = Assembly.LoadFile(args[0]);
                tasksProvider.Load(assembly);
							
                Kinoko kinoko = CreateKinoko();

                progressBarCharCount = Console.WindowWidth - 2;
                ConsoleColor oldColor = SetColor(ConsoleColor.Blue);
                Console.WriteLine("Start measuring tasks from assembly " + args[0]);
                SetColor(oldColor);

                IEnumerable<KinokoTask> tasks = tasksProvider.GetTasks();
                List<KinokoResult> results = RunTasks(kinoko, tasks);
				
                DisplayResults(results);
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
            kinoko.Measured += new EventHandler<MeasuredEventArgs>(kinoko_Meadured);
            return kinoko;
        }

		static List<KinokoResult> RunTasks(Kinoko kinoko, IEnumerable<KinokoTask> tasks)
        {
            List<KinokoResult> results = new List<KinokoResult>();

            foreach (KinokoTask task in tasks)
            {
                results.Add(RunTask(kinoko, task));
            }

            return results;
        }

		static KinokoResult RunTask(Kinoko kinoko, KinokoTask task)
        {
            Console.WriteLine();
            Console.WriteLine(string.Format("Running task: {0} ", ((Delegate)task).Method.Name));
            WriteProgressBar();

            percentCompleted = 0;
            KinokoResult result = kinoko.Run(task, repeatMeasurementCount);

            Console.WriteLine();

            return result;
        }

        static void WriteProgressBar()
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

		static void DisplayResults(List<KinokoResult> results)
		{
			foreach (KinokoResult result in results)
			{
				Console.WriteLine();
				Console.WriteLine("Average time: {0:#,##0.00} milisec", result.Average);
			}
		}

		private static void kinoko_Meadured(object sender, MeasuredEventArgs e)
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
            Console.WriteLine("===============================================================================");
            Console.WriteLine();

            SetColor(oldColor);
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
            Console.WriteLine("--------------------------------------------------------------------------------");
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
