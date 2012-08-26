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
using DustInTheWind.SharpKinoko.Providers;
using DustInTheWind.SharpKinoko.SharpKinokoConsole.ConsoleControls;
using System.IO;
using System.Reflection;
using Ninject;

namespace DustInTheWind.SharpKinoko.SharpKinokoConsole
{
    public class KinokoWrapper
    {
        private readonly IKernel kernel;
        private readonly UI ui;
        private ProgressBar progressBar;
        private int  repeatMeasurementCount;

        public KinokoWrapper(IKernel kernel, UI ui)
        {
            if (kernel == null)
                throw new ArgumentNullException("kernel");

            if (ui == null)
                throw new ArgumentNullException("ui");

            this.kernel = kernel;
            this.ui = ui;
        }

        public void StartMeasuring(IEnumerable<string> assemblyFileNames, int repeatMeasurementCount)
        {
            this.repeatMeasurementCount = repeatMeasurementCount;

            Kinoko kinoko = CreateKinoko();

            foreach (string assemblyFileName in assemblyFileNames)
            {
                ui.WriteAssemblyLoadingInformation(assemblyFileName);

                ITasksProvider tasksProvider = CreateTasksProvider(assemblyFileName);
                kinoko.Run(tasksProvider, repeatMeasurementCount);
            }
        }

        private Kinoko CreateKinoko()
        {
            Kinoko kinoko = new Kinoko();

            kinoko.Measured += HandleKinokoMeasured;
            kinoko.TaskRunning += HandleKinokoTaskRunning;
            kinoko.TaskRun += HandleKinokoTaskRun;

            return kinoko;
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

