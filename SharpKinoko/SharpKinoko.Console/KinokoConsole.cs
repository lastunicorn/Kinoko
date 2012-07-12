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
    public class KinokoConsole
    {
        private readonly IConsole console;
        private readonly string[] args;
        private HelpWritter helpWritter;
        private GuiHelpers guiHelpers;
        private const int RepeatMeasurementCount = 10;
        private ProgressBar progressBar;

        public KinokoConsole(IConsole console, string[] args)
        {
            if (console == null)
                throw new ArgumentNullException("console");

            this.console = console;
            this.args = args;
        }

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
                return;
            }

            Kinoko kinoko = CreateKinoko();
            RunTasksFromAssembly(kinoko, args[0]);
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

        #region Kinoko event handlers

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

        #endregion

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
            return  console.WindowWidth - 1;
        }
    }
}

