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
using DustInTheWind.Kinoko.Providers;

namespace DustInTheWind.Kinoko
{
    /// <summary>
    /// Measures the time needed to run the provided subjects. The measurement is performed multiple times and an
    /// average is calculated.
    /// </summary>
    public interface IKinokoContext
    {
        /// <summary>
        /// Event raised before every measuring of a subject.
        /// </summary>
        event EventHandler<MeasuringEventArgs> Measuring;

        /// <summary>
        /// Event raised after every measuring of a subject.
        /// </summary>
        event EventHandler<MeasuredEventArgs> Measured;

        /// <summary>
        /// Event raised before a task is started. A task is represented by the multiple measurements of a subject.
        /// </summary>
        event EventHandler<TaskRunningEventArgs> TaskRunning;

        /// <summary>
        /// Event raised after a task is finished. A task is represented by the multiple measurements of a subject.
        /// </summary>
        event EventHandler<TaskRunEventArgs> TaskRun;

        /// <summary>
        /// Runs the task multiple times and measures the time intervals spent.
        /// </summary>
        /// <param name="task">The kinoko task to be run.</param>
        /// <param name="repeatCount">Specifies the number of times to repeat the measurement.</param>
        /// <returns>A <see cref="KinokoResult"/> object containing the measured data and the calculated values.</returns>
        /// <remarks>
        /// After the measurements are finished, additional values (for example the average) are calculated from the measured data.
        /// </remarks>
        KinokoResult Run(KinokoTask task, int repeatCount);

        /// <summary>
        /// Measures the time spent to run the subjects received from the subjectProvider.
        /// </summary>
        /// <param name="tasksProvider">Provides a list of kinoko tasks to be run.</param>
        /// <param name="repeatCount">Specifies the number of times to repeat the measurement.</param>
        /// <returns>A list of <see cref="KinokoResult"/> objects containing the measured data and the calculated values.</returns>
        /// <exception cref="ArgumentNullException">Is thrown when the subjectProvider or the repeatCount are <see langword="null" />.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown when the repeatCount is less then 1.</exception>
        IList<KinokoResult> Run(ITasksProvider tasksProvider, int repeatCount);
    }
}