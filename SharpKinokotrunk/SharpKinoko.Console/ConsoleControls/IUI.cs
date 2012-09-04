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

namespace DustInTheWind.SharpKinoko.SharpKinokoConsole.ConsoleControls
{
    /// <summary>
    /// Contains a few usefull methods that help interact with the console.
    /// </summary>
    public interface IUI
    {
        /// <summary>
        /// Gets the console used to write text to.
        /// </summary>
        IConsole Console { get; }

        /// <summary>
        /// Displays an exception into the console.
        /// </summary>
        /// <param name="ex">The exception to be displayed.</param>
        void DisplayError(Exception ex);

        /// <summary>
        /// Displays an error message into the console.
        /// </summary>
        /// <param name="text">The error message to be displayed.</param>
        void DisplayError(string text);

        /// <summary>
        /// Pauses the console until the user presses a key.
        /// </summary>
        void Pause();

        /// <summary>
        /// Returns the width of the user interface in columns.
        /// </summary>
        /// <returns>The width of the user interface in columns.</returns>
        int GetWindowWidth();

        /// <summary>
        /// Writes a full line of the console with the specified character.
        /// </summary>
        /// <param name="c">The character used to fill the line.</param>
        void  WriteFullLine(char c);

        /// <summary>
        /// Writes to the console the Sharp Kinoko name and the version.
        /// </summary>
        void WriteKinokoHeader();

        /// <summary>
        /// Writes to the console the task title that is about to be run.
        /// </summary>
        /// <param name='task'>The task that is about to be run.</param>
        void WriteTaskTitle(KinokoTask task);

        /// <summary>
        /// Writes to the console the result of the task that was run.
        /// </summary>
        /// <param name='result'>The result of the run of a task.</param>
        void WriteTaskResult(KinokoResult result);

        /// <summary>
        /// Writes to the console a line of test to specify what assembly is currently loading.
        /// </summary>
        /// <param name='assemblyFileName'>The file name of the assembly that is loading.</param>
        void WriteAssemblyLoadingInformation(string assemblyFileName);
    }
}

