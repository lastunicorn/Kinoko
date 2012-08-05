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
using DustInTheWind.SharpKinoko.Providers;
using DustInTheWind.SharpKinoko.SharpKinokoConsole.ConsoleControls;
using CommandLine;
using CommandLine.Text;

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

        /// <summary>
        /// Gets or sets the <see cref="GuiHelpers"/> instance that contains methods to .
        /// </summary>
        /// <value>
        /// The GUI helpers.
        /// </value>
        public GuiHelpers guiHelpers { get; set; }

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

            if (args == null)
                throw new ArgumentNullException("args");

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

            try
            {
                CommandLineOptions options = ParseArguments();

                if (options.HasErrors)
                {
                    console.WriteLine(GetParsingErrors(options));
                }
                else
                {
                    if (options.DisplayHelp)
                        console.WriteLine(GetUsage(options));

                    if (options.AssemblyFileNames != null && options.AssemblyFileNames.Count > 0)
                    {
                        Kinoko kinoko = CreateKinoko();
                        RunTasksFromAssembly(kinoko, options.AssemblyFileNames[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                guiHelpers.DisplayError(ex);
            }
            finally
            {
                guiHelpers.Pause();
            }
        }

        /// <summary>
        /// Validates the args list. If the arguments are not ok, an exception is thrown.
        /// </summary>
        /// <exception cref='InvalidConsoleArgumentsException'>
        /// Is thrown when the console arguments are invalid.
        /// </exception>
        private CommandLineOptions ParseArguments()
        {
            CommandLineOptions options = new CommandLineOptions();

            CommandLineParserSettings parserSettings = new CommandLineParserSettings();
            CommandLineParser parser = new CommandLineParser(parserSettings);

            parser.ParseArguments(args, options);

//            if (parser.ParseArguments(args, options))
//            {
//                if (options.DisplayHelp)
//                    console.WriteLine(GetUsage(options));
//            }
//            else
//            {
//                console.WriteLine(GetParsingErrors(options));
//                //throw new InvalidConsoleArgumentsException();
//            }

            return options;
        }

        private string GetParsingErrors(CommandLineOptions options)
        {
            HelpText helpText = new HelpText();

            helpText.AdditionalNewLineAfterOption = false;
            helpText.AddDashesToOption = true;

            string errors = helpText.RenderParsingErrorsText(options, 2);
            helpText.AddPreOptionsLine(string.Concat(Environment.NewLine, "ERROR(S):"));
            helpText.AddPreOptionsLine(errors);

            return helpText;
        }

        private string GetUsage(CommandLineOptions options)
        {
            //return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));

            HelpText helpText = new HelpText();

            helpText.AdditionalNewLineAfterOption = false;
            helpText.AddDashesToOption = true;

            helpText.AddPreOptionsLine("Usage: SharpKinokoConsole -assembly <assemblyName>");
            helpText.AddOptions(options);


            return helpText;
        }

        /// <summary>
        /// Creates one <see cref="Kinoko"/> instance and subscribes to the needed events.
        /// </summary>
        /// <returns>
        /// The newly created <see cref="Kinono"/> object.
        /// </returns>
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
            helpWritter.WriteTaskTitle(e.Task);

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

            ITasksProvider tasksProvider = CreateTasksProvider(assemblyFileName);
            kinoko.Run(tasksProvider, RepeatMeasurementCount);
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

