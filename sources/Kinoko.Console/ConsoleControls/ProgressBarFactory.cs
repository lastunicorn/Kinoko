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

namespace DustInTheWind.Kinoko.KinokoConsole.ConsoleControls
{
    public class ProgressBarFactory
    {
        /// <summary>
        /// The console that will be passed to each <see cref="ProgressBar"/> instace.
        /// </summary>
        private IConsole console;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBarFactory"/> class.
        /// </summary>
        /// <param name='console'>
        /// The console that will be passed to each <see cref="ProgressBar"/> instace.
        /// </param>
        public ProgressBarFactory(IConsole console)
        {
            if (console == null)
                throw new ArgumentNullException("console");

            this.console = console;
        }

        public ProgressBar CreateProgressBar()
        {
            return new ProgressBar(console);
        }
    }
}

