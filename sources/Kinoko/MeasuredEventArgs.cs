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
    /// Provides data for <see cref="KinokoContext.Measured"/> event.
    /// </summary>
    public class MeasuredEventArgs : EventArgs
    {
        /// <summary>
        /// The index of the current measurement.
        /// </summary>
        private readonly int stepIndex;

        /// <summary>
        /// Gets the index of the current measurement.
        /// </summary>
        public int StepIndex
        {
            get { return stepIndex; }
        }

        /// <summary>
        /// The measured time in miliseconds.
        /// </summary>
        private readonly double time;

        /// <summary>
        /// Gets the measured time in miliseconds.
        /// </summary>
        public double Time
        {
            get { return time; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeasuredEventArgs"/> class.
        /// </summary>
        /// <param name="stepIndex">The index of the current measurement.</param>
        /// <param name="time">The measured time in miliseconds.</param>
        public MeasuredEventArgs(int stepIndex, double time)
        {
            this.stepIndex = stepIndex;
            this.time = time;
        }
    }
}
