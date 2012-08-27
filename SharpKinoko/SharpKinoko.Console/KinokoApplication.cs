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
using CommandLine.Text;
using DustInTheWind.SharpKinoko.SharpKinokoConsole.ConsoleControls;

namespace DustInTheWind.SharpKinoko.SharpKinokoConsole
{
    /// <summary>
    /// Contains the logic of the Kinoko Console application.
    /// </summary>
    internal class KinokoApplication
    {
        /// <summary>
        /// Provides methods to easyer interact with the console.
        /// </summary>
        private readonly UI ui;

        /// <summary>
        /// Runs the kinoko tasks and displays the results to the console.
        /// </summary>
        private readonly KinokoRunner kinokoRunner;

        /// <summary>
        /// The option values obtained from parsing the command line arguments.
        /// </summary>
        private readonly CommandLineOptions options;

        /// <summary>
        /// The number of times the measurement should be performed in order to minimize the error.
        /// </summary>
        private const int RepeatMeasurementCount = 10;

        /// <summary>
        /// Initializes a new instance of the <see cref="KinokoApplication"/> class.
        /// </summary>
        /// <param name="options">The option values obtained from parsing the command line arguments.</param>
        /// <param name="ui">Provides methods to easyer interact with the console.</param>
        /// <param name="kinokoRunner">Runs the kinoko tasks and displays the results to the console.</param>
        /// <exception cref="ArgumentNullException">Thrown if the console is null.</exception>
        public KinokoApplication(CommandLineOptions options, UI ui, KinokoRunner kinokoRunner)
        {
            if (options == null)
                throw new ArgumentNullException("options");

            if (ui == null)
                throw new ArgumentNullException("ui");

            if (kinokoRunner == null)
                throw new ArgumentNullException("kinokoRunner");

            this.options = options;
            this.ui = ui;
            this.kinokoRunner = kinokoRunner;
        }

        /// <summary>
        /// Starts the application.
        /// </summary>
        public void Start()
        {
            try
            {
                ui.Console.ForegroundColor = ConsoleColor.DarkGreen;
                ui.WriteKinokoHeader();

                if (ExistsParsingErrors())
                {
                    WriteParsingErrorsToConsole();
                }
                else
                {
                    if (HelpWasRequested())
                        WriteHelpToConsole();

                    if (AssembliesWereProvided())
                        PerformMeasurementsOnAssemblies(options.AssemblyFileNames);
                }
            }
            catch (Exception ex)
            {
                ui.DisplayError(ex);
            }
            finally
            {
                ui.Pause();
            }
        }

        private bool ExistsParsingErrors()
        {
            return options.HasErrors;
        }

        private void WriteParsingErrorsToConsole()
        {
            ui.Console.WriteLine(CreateParsingErrorsText());
        }

        private string CreateParsingErrorsText()
        {
            HelpText helpText = new HelpText
            {
                AdditionalNewLineAfterOption = false,
                AddDashesToOption = true
            };

            string errors = helpText.RenderParsingErrorsText(options, 2);
            helpText.AddPreOptionsLine(string.Concat(Environment.NewLine, "ERROR(S):"));
            helpText.AddPreOptionsLine(errors);

            return helpText;
        }

        private bool HelpWasRequested()
        {
            return options.DisplayHelp;
        }

        private void WriteHelpToConsole()
        {
            ui.Console.WriteLine(CreateHelpText());
        }

        private string CreateHelpText()
        {
            HelpText helpText = new HelpText
            {
                AdditionalNewLineAfterOption = false,
                AddDashesToOption = true
            };

            helpText.AddPreOptionsLine("Usage: SharpKinokoConsole -assembly <assemblyName>");
            helpText.AddOptions(options);

            return helpText;
        }

        private bool AssembliesWereProvided()
        {
            return options.AssemblyFileNames != null && options.AssemblyFileNames.Count > 0;
        }

        private void PerformMeasurementsOnAssemblies(IEnumerable<string> assemblyFileNames)
        {
            kinokoRunner.StartMeasuring(assemblyFileNames, RepeatMeasurementCount);
        }
    }
}

