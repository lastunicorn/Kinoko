using System;
using DustInTheWind.SharpKinoko;
using System.IO;
using System.Reflection;

namespace DustInTheWind.SharpKinokoConsole
{
    public class KinokoConsole
    {
        private IConsole console;
        private string[] args;
        private HelpWritter helpWritter;
        private GuiHelpers guiHelpers;
        private const int repeatMeasurementCount = 10;
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
            ProgressBar progressBar = new ProgressBar(console);
            progressBar.Width = GetWindowWidth();
            progressBar.ForegroundColor = ConsoleColor.Yellow;
            return progressBar;
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
            kinoko.Run(subjectsProvider, repeatMeasurementCount);
        }

        private int CalculatePercentage(int index)
        {
            return (index * 100) / repeatMeasurementCount;
        }

        private int GetWindowWidth()
        {
            return  console.WindowWidth - 1;
        }
    }
}

