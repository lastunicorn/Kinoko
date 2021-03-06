﻿// SharpKinoko
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

namespace DustInTheWind.Kinoko
{
    /// <summary>
    /// Provides data for <see cref="KinokoContext.TaskRunning"/> event.
    /// </summary>
    public class TaskRunningEventArgs : EventArgs
    {
        /// <summary>
        /// The task that is about to be run.
        /// </summary>
        private readonly KinokoTask task;

        /// <summary>
        /// Gets the task that is about to be run.
        /// </summary>
        public KinokoTask Task
        {
            get { return task; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskRunningEventArgs"/> class.
        /// </summary>
        /// <param name="task">The task that is about to be run.</param>
        public TaskRunningEventArgs(KinokoTask task)
        {
            this.task = task;
        }
    }
}
