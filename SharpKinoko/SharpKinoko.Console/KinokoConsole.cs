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
using System.IO;
using System.Reflection;
using DustInTheWind.SharpKinoko.SharpKinokoConsole.ConsoleControls;

namespace DustInTheWind.SharpKinoko.SharpKinokoConsole
{
    /// <summary>
    /// Contains the logic of the console application.
    /// </summary>
    public class KinokoConsole
    {
        /// <summary>
        /// The object that is used to interact with the user.
        /// </summary>
        private readonly IConsole console;

        /// <summary>
        /// The list of arguments with which the application was started.
        /// </summary>
        private readonly string[] args;

        private HelpWritter helpWritter;
        private GuiHelpers guiHelpers;
        private const int RepeatMeasurementCount = 10;
        private ProgressBar progressBar;

        /// <summary>
        /// Initializes a new instance of the <see cref="KinokoConsole"/> class.
        /// </summary>
        /// <param name="console">The <see cref="IConsole"/> object to be used to interact with the user.</param>
        /// <param name="args">The list of arguments with which the application was started.</param>
        /// <exception cref="ArgumentNullException">Thrown if the console is null.</exception>
        public KinokoConsole(IConsole console, string[] args)
        {
            if (console == null)
                throw new ArgumentNullException("console");

            this.console = console;
            this.args = args;
        }

        /// <summary>
        /// Starts the application.
        /// </summary>
        public void Start()
        {
            console.ForegroundColor = ConsoleColor.DarkGreen;

            guiHelpers = new GuiHelpers(console);
            helpWritter = new HelpWritter(console, guiHelpers);

            helpWritter.WriteKinokoHeader();

            if (args == null || args.Length != 1)
            {
                guiHelpers.DisplayError("Invalid number of arguments.");
                helpWritter.WriteShortHelp();
                guiHelpers.Pause();
                return;
            }

            Kinoko kinoko = CreateKinoko();
            RunTasksFromAssembly(kinoko, args[0]);
            guiHelpers.Pause();
        }

        private Kinoko CreateKinoko()
        {
            Kinoko kinoko = new Kinoko();

            kinoko.Measured += HandleKinokoMeasured;
            kinoko.TaskRunning += HandleKinokoTaskRunning;
            kinoko.TaskRun += HandleKinokoTaskRun;

            return kinoko;
        }

        private ISubjectsProvider CreateSubjectsProvider(string assemblyFilePath)
        {
            AssemblySubjectsProvider subjectsProvider = new AssemblySubjectsProvider();
            string fullPath = Path.GetFullPath(assemblyFilePath);
            Assembly assembly = Assembly.LoadFile(fullPath);
            subjectsProvider.Load(assembly);

            return subjectsProvider;
        }

        private ProgressBar CreateProgressBar()
        {
            return new ProgressBar(console)
            {
                Width = GetWindowWidth(),
                ForegroundColor = ConsoleColor.Yellow
            };
        }

        private void HandleKinokoTaskRunning(object sender, TaskRunningEventArgs e)
        {
            helpWritter.WriteTaskTitle(e.Subject);

            progressBar = CreateProgressBar();
            progressBar.Display();
        }

        private void HandleKinokoTaskRun(object sender, TaskRunEventArgs e)
        {
            helpWritter.WriteTaskResult(e.Result);
        }

        private void HandleKinokoMeasured(object sender, MeasuredEventArgs e)
        {
            int newPercent = CalculatePercentage(e.StepIndex + 1);

            progressBar.SetProgress(newPercent);
        }

        private void RunTasksFromAssembly(Kinoko kinoko, string assemblyFileName)
        {
            helpWritter.WriteLoadingAssembly(assemblyFileName);

            ISubjectsProvider subjectsProvider = CreateSubjectsProvider(assemblyFileName);
            kinoko.Run(subjectsProvider, RepeatMeasurementCount);
        }

        private int CalculatePercentage(int index)
        {
            return (index * 100) / RepeatMeasurementCount;
        }

        private int GetWindowWidth()
        {
            return console.WindowWidth - 1;
        }
    }
}

