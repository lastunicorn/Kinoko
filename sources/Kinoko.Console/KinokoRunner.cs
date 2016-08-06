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
using System.IO;
using System.Reflection;
using DustInTheWind.Kinoko.KinokoConsole.ConsoleControls;
using DustInTheWind.Kinoko.Providers;

namespace DustInTheWind.Kinoko.KinokoConsole
{
    /// <summary>
    /// Runs the tasks and displays the results to the UI.
    /// </summary>
    internal class KinokoRunner : IKinokoRunner
    {
        /// <summary>
        /// The factory class that creates new instances of <see cref="ProgressBar"/> class.
        /// </summary>
        private readonly ProgressBarFactory progressBarFactory;

        /// <summary>
        /// Instance used to interact with the user interface.
        /// </summary>
        private readonly IUI ui;

        /// <summary>
        /// Kinoko instance that performs the measurements.
        /// </summary>
        private readonly IKinokoContext kinokoContext;

        /// <summary>
        /// Represents a progress bar.
        /// </summary>
        private ProgressBar progressBar;

        /// <summary>
        /// The number of times the measurements are performed on a single subject (method).
        /// </summary>
        private int repeatMeasurementCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="KinokoRunner"/> class.
        /// </summary>
        /// <param name='progressBarFactory'>The factory class that creates new instances of <see cref="ProgressBar"/> class.</param>
        /// <param name='kinokoContext'>Kinoko instance that performs the measurements.</param>
        /// <param name='ui'>Instance used to interact with the user interface.</param>
        /// <exception cref="ArgumentNullException">Thrown if one of the parameters is null.</exception>
        public KinokoRunner(ProgressBarFactory progressBarFactory, IKinokoContext kinokoContext, IUI ui)
        {
            if (progressBarFactory == null)
                throw new ArgumentNullException("progressBarFactory");

            if (kinokoContext == null)
                throw new ArgumentNullException("kinokoContext");

            if (ui == null)
                throw new ArgumentNullException("ui");

            this.progressBarFactory = progressBarFactory;
            this.kinokoContext = kinokoContext;
            this.ui = ui;

            kinokoContext.Measured += HandleKinokoMeasured;
            kinokoContext.TaskRunning += HandleKinokoTaskRunning;
            kinokoContext.TaskRun += HandleKinokoTaskRun;
        }

        /// <summary>
        /// Starts to run the tasks from the specified assemblies and displays the results to the UI.
        /// </summary>
        /// <param name='assemblyFileNames'>The file names of the assemblies to load.</param>
        /// <param name='repeatMeasurementCount'>The number of times the measurements are performed on a single subject (method).</param>
        public void StartMeasuring(IEnumerable<string> assemblyFileNames, int repeatMeasurementCount)
        {
            this.repeatMeasurementCount = repeatMeasurementCount;

            foreach (string assemblyFileName in assemblyFileNames)
            {
                ui.WriteAssemblyLoadingInformation(assemblyFileName);

                ITasksProvider tasksProvider = CreateTasksProvider(assemblyFileName);
                kinokoContext.Run(tasksProvider, repeatMeasurementCount);
            }
        }

        private ITasksProvider CreateTasksProvider(string assemblyFilePath)
        {
            AssemblyTasksProvider tasksProvider = new AssemblyTasksProvider();
            string fullPath = Path.GetFullPath(assemblyFilePath);
            Assembly assembly = Assembly.LoadFile(fullPath);
            tasksProvider.Load(assembly);

            return tasksProvider;
        }

        private void HandleKinokoTaskRunning(object sender, TaskRunningEventArgs e)
        {
            ui.WriteTaskTitle(e.Task);

            progressBar = CreateProgressBar();
            progressBar.Display();
        }

        private ProgressBar CreateProgressBar()
        {
            ProgressBar progressBar = progressBarFactory.CreateProgressBar();

            progressBar.Width = ui.GetWindowWidth();
            progressBar.ForegroundColor = ConsoleColor.Yellow;

            return progressBar;
        }

        private void HandleKinokoTaskRun(object sender, TaskRunEventArgs e)
        {
            ui.WriteTaskResult(e.Result);
        }

        private void HandleKinokoMeasured(object sender, MeasuredEventArgs e)
        {
            int newPercent = CalculatePercentage(e.StepIndex + 1);
            progressBar.SetProgress(newPercent);
        }

        private int CalculatePercentage(int index)
        {
            return (index * 100) / repeatMeasurementCount;
        }
    }
}

