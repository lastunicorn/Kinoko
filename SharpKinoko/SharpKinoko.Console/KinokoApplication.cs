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
using System.Collections.Generic;

namespace DustInTheWind.SharpKinoko.SharpKinokoConsole
{
    /// <summary>
    /// Contains the logic of the Kinoko Console application.
    /// </summary>
    internal class KinokoApplication
    {
        /// <summary>
        /// Gets or sets the <see cref="UI"/> instance that provides methods to easyer interact with the console.
        /// </summary>
        private readonly UI ui;
        private readonly KinokoRunner kinokoRunner;
        private CommandLineOptions options;

        /// <summary>
        /// The number of times the measurement should be performed in order to minimize the error.
        /// </summary>
        private const int RepeatMeasurementCount = 10;

        /// <summary>
        /// Initializes a new instance of the <see cref="KinokoConsole"/> class.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if the console is null.</exception>
        public KinokoApplication(CommandLineOptions options, UI ui, KinokoRunner kinokoRunner)
        {
            if (options == null)
                throw new ArgumentNullException("options");

            if (ui == null)
                throw new ArgumentNullException("ui");

            if (kinokoRunner == null)
                throw new ArgumentNullException("kinokoWrapper");

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

                if (ExistsParsingErrors(options))
                {
                    WriteParsingErrorsToConsole(options);
                }
                else
                {
                    if (HelpWasRequested(options))
                        WriteHelpToConsole(options);

                    if (AssembliesWereProvided(options))
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

        private bool ExistsParsingErrors(CommandLineOptions options)
        {
            return options.HasErrors;
        }

        private void WriteParsingErrorsToConsole(CommandLineOptions options)
        {
            ui.Console.WriteLine(CreateParsingErrorsText(options));
        }

        private string CreateParsingErrorsText(CommandLineOptions options)
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

        private bool HelpWasRequested(CommandLineOptions options)
        {
            return options.DisplayHelp;
        }

        private void WriteHelpToConsole(CommandLineOptions options)
        {
            ui.Console.WriteLine(CreateHelpText(options));
        }

        private string CreateHelpText(CommandLineOptions options)
        {
            //return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));

            HelpText helpText = new HelpText
            {
                AdditionalNewLineAfterOption = false,
                AddDashesToOption = true
            };

            helpText.AddPreOptionsLine("Usage: SharpKinokoConsole -assembly <assemblyName>");
            helpText.AddOptions(options);

            return helpText;
        }

        private bool AssembliesWereProvided(CommandLineOptions options)
        {
            return options.AssemblyFileNames != null && options.AssemblyFileNames.Count > 0;
        }

        private void PerformMeasurementsOnAssemblies(IEnumerable<string> assemblyFileNames)
        {
            kinokoRunner.StartMeasuring(assemblyFileNames, RepeatMeasurementCount);
        }
    }
}

