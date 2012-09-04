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

using System.Collections.Generic;
using CommandLine;

namespace DustInTheWind.SharpKinoko.SharpKinokoConsole
{
    /// <summary>
    /// Contains the value obtained from parsing the command line arguments.
    /// </summary>
    internal class CommandLineOptions : CommandLineOptionsBase
    {
        /// <summary>
        /// Gets or sets the names of the assemblies.
        /// </summary>
        [OptionList("a", "assemblies", Required = true, Separator = ';', HelpText = "A list of assembly file names.")]
        public IList<string> AssemblyFileNames { get; set; }

        /// <summary>
        /// Gets or sets the full names of the methods (subjects) to be measured.
        /// </summary>
        [OptionList("s", "subjects", Separator = ';', HelpText = "A list of methods to be measured. Should include the full path with namespaces and class name.")]
        public IList<string> SubjectFullNames { get; set; }

        /// <summary>
        /// Gets or sets a value specifying if the help text should be displayed.
        /// </summary>
        [Option("h", "help", HelpText = "Display this help screen.")]
        public bool DisplayHelp { get; set; }

        /// <summary>
        /// Gets a value specifying if the parsing of the command line arguments had some errors.
        /// </summary>
        public bool HasErrors
        {
            get { return LastPostParsingState.Errors.Count > 0; }
        }
    }
}
