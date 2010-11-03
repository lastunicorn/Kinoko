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

namespace DustInTheWind.SharpKinoko
{
    /// <summary>
    /// Provides data for <see cref="Kinoko.BeforeTaskRun"/> event.
    /// </summary>
    public class BeforeTaskRunEventArgs : EventArgs
    {
        /// <summary>
        /// The index of the current run of the task.
        /// </summary>
        private int stepIndex;

        /// <summary>
        /// Gets the index of the current run of the task.
        /// </summary>
        public int StepIndex
        {
            get { return stepIndex; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BeforeTaskRunEventArgs"/> class.
        /// </summary>
        /// <param name="stepIndex">The index of the current run of the task.</param>
        public BeforeTaskRunEventArgs(int stepIndex)
            : base()
        {
            this.stepIndex = stepIndex;
        }
    }
}
