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
using DustInTheWind.SharpKinoko.Providers;
using DustInTheWind.SharpKinoko.SharpKinokoConsole.ConsoleControls;
using Ninject;

namespace DustInTheWind.SharpKinoko.SharpKinokoConsole
{
    /// <summary>
    /// Runs the tasks and displays the results to the UI.
    /// </summary>
    internal class KinokoRunner : IKinokoRunner
    {
        /// <summary>
        /// The IOC container.
        /// </summary>
        private readonly IKernel kernel;

        /// <summary>
        /// Instance used to interact with the user interface.
        /// </summary>
        private readonly IUI ui;

        /// <summary>
        /// Kinoko instance that performs the measurements.
        /// </summary>
        private readonly IKinoko kinoko;

        /// <summary>
        /// Represents a progress bar.
        /// </summary>
        private ProgressBar progressBar;

        /// <summary>
        /// The number of times the measurements are performed on a single subject (method).
        /// </summary>
        private int repeatMeasurementCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="DustInTheWind.SharpKinoko.SharpKinokoConsole.KinokoRunner"/> class.
        /// </summary>
        /// <param name='kernel'>The IOC container.</param>
        /// <param name='kinoko'>Kinoko instance that performs the measurements.</param>
        /// <param name='ui'>Instance used to interact with the user interface.</param>
        /// <exception cref="ArgumentNullException">Thrown if one of the parameters is null.</exception>
        public KinokoRunner(IKernel kernel, IKinoko kinoko, IUI ui)
        {
            if (kernel == null)
                throw new ArgumentNullException("kernel");

            if (kinoko == null)
                throw new ArgumentNullException("kinoko");

            if (ui == null)
                throw new ArgumentNullException("ui");

            this.kernel = kernel;
            this.kinoko = kinoko;
            this.ui = ui;

            kinoko.Measured += HandleKinokoMeasured;
            kinoko.TaskRunning += HandleKinokoTaskRunning;
            kinoko.TaskRun += HandleKinokoTaskRun;
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
                kinoko.Run(tasksProvider, repeatMeasurementCount);
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
            ProgressBar progressBar = kernel.Get<ProgressBar>();

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

