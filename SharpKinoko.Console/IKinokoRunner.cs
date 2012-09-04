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

namespace DustInTheWind.SharpKinoko.SharpKinokoConsole
{
    /// <summary>
    /// Runs the tasks and displays the results to the UI.
    /// </summary>
    public interface IKinokoRunner
    {
        /// <summary>
        /// Starts to run the tasks from the specified assemblies and displays the results to the UI.
        /// </summary>
        /// <param name='assemblyFileNames'>The file names of the assemblies to load.</param>
        /// <param name='repeatMeasurementCount'>The number of times the measurements are performed on a single subject (method).</param>
        void StartMeasuring(IEnumerable<string> assemblyFileNames, int repeatMeasurementCount);
    }
}

