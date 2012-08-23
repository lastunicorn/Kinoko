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
using DustInTheWind.SharpKinoko.Providers;

namespace DustInTheWind.SharpKinoko
{
    /// <summary>
    /// Measures the time needed to run the provided subjects. The measurement is performed multiple times and an
    /// average is calculated.
    /// The class is not thread safe.
    /// </summary>
    public class Kinoko
    {
        #region Event Measuring

        /// <summary>
        /// Event raised before every measuring of a subject.
        /// </summary>
        public event EventHandler<MeasuringEventArgs> Measuring;

        /// <summary>
        /// Raises the <see cref="Measuring"/> event.
        /// </summary>
        /// <param name="e">An <see cref="MeasuringEventArgs"/> object that contains the event data.</param>
        protected virtual void OnMeasuring(MeasuringEventArgs e)
        {
            if (Measuring != null)
            {
                Measuring(this, e);
            }
        }

        #endregion

        #region Event Measured

        /// <summary>
        /// Event raised after every measuring of a subject.
        /// </summary>
        public event EventHandler<MeasuredEventArgs> Measured;

        /// <summary>
        /// Raises the <see cref="Measured"/> event.
        /// </summary>
        /// <param name="e">An <see cref="MeasuredEventArgs"/> object that contains the event data.</param>
        protected virtual void OnMeasured(MeasuredEventArgs e)
        {
            if (Measured != null)
            {
                Measured(this, e);
            }
        }

        #endregion

        #region Event TaskRunning

        /// <summary>
        /// Event raised before a task is started. A task is represented by the multiple measurements of a subject.
        /// </summary>
        public event EventHandler<TaskRunningEventArgs> TaskRunning;

        /// <summary>
        /// Raises the <see cref="TaskRunning"/> event.
        /// </summary>
        /// <param name="e">An <see cref="TaskRunningEventArgs"/> object that contains the event data.</param>
        protected virtual void OnTaskRunning(TaskRunningEventArgs e)
        {
            if (TaskRunning != null)
            {
                TaskRunning(this, e);
            }
        }

        #endregion

        #region Event TaskRun

        /// <summary>
        /// Event raised after a task is finished. A task is represented by the multiple measurements of a subject.
        /// </summary>
        public event EventHandler<TaskRunEventArgs> TaskRun;

        /// <summary>
        /// Raises the <see cref="TaskRun"/> event.
        /// </summary>
        /// <param name="e">An <see cref="TaskRunEventArgs"/> object that contains the event data.</param>
        protected virtual void OnTaskRun(TaskRunEventArgs e)
        {
            if (TaskRun != null)
            {
                TaskRun(this, e);
            }
        }

        #endregion

        #region Run

        /// <summary>
        /// Runs the task multiple times and measures the time intervals spent.
        /// </summary>
        /// <param name="task">The kinoko task to be run.</param>
        /// <param name="repeatCount">Specifies the number of times to repeat the measurement.</param>
        /// <returns>A <see cref="KinokoResult"/> object containing the measured data and the calculated values.</returns>
        /// <remarks>
        /// After the measurements are finished, additional values (for example the average) are calculated from the measured data.
        /// </remarks>
        public KinokoResult Run(KinokoTask task, int repeatCount)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            if (repeatCount < 1)
                throw new ArgumentOutOfRangeException("repeatCount", "The repeat count should be an integer greater then 0.");

            return RunTaskAndRunEvents(task, repeatCount);
        }

        /// <summary>
        /// Measures the time spent to run the subjects received from the subjectProvider.
        /// </summary>
        /// <param name="tasksProvider">Provides a list of kinoko tasks to be run.</param>
        /// <param name="repeatCount">Specifies the number of times to repeat the measurement.</param>
        /// <returns>A list of <see cref="KinokoResult"/> objects containing the measured data and the calculated values.</returns>
        /// <exception cref="ArgumentNullException">Is thrown when the subjectProvider or the repeatCount are <see langword="null" />.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown when the repeatCount is less then 1.</exception>
        public IList<KinokoResult> Run(ITasksProvider tasksProvider, int repeatCount)
        {
            if (tasksProvider == null)
                throw new ArgumentNullException("tasksProvider");

            if (repeatCount < 1)
                throw new ArgumentOutOfRangeException("repeatCount", "The repeat count should be an integer greater then 0.");

            IEnumerable<KinokoTask> tasks = tasksProvider.GetKinokoTasks();
            List<KinokoResult> results = new List<KinokoResult>();

            foreach (KinokoTask task in tasks)
            {
                results.Add(RunTaskAndRunEvents(task, repeatCount));
            }

            return results;
        }

        /// <summary>
        /// Measures the subject and also raises the needed events.
        /// The additional values are calculated from the measured data.
        /// </summary>
        /// <returns>A <see cref="KinokoResult"/> object containing the measured data.</returns>
        /// <param name='task'>The kinoko task to be run.</param>
        /// <param name='repeatCount'>The number of times to repeat the measurement.</param>
        private KinokoResult RunTaskAndRunEvents(KinokoTask task, int repeatCount)
        {
            OnTaskRunning(new TaskRunningEventArgs(task));
            KinokoResult result = RunTask(task, repeatCount);
            OnTaskRun(new TaskRunEventArgs(result));

            return result;
        }

        private KinokoResult RunTask(KinokoTask task, int repeatCount)
        {
            Measurer measurer = new Measurer(task.Subject, repeatCount);
            measurer.Measuring += HandleMeasurerMeasuring;
            measurer.Measured += HandleMeasurerMeasured;

            try
            {
                return measurer.Run();
            }
            finally
            {
                measurer.Measuring -= HandleMeasurerMeasuring;
                measurer.Measured -= HandleMeasurerMeasured;
            }
        }

        private void HandleMeasurerMeasuring(object sender, MeasuringEventArgs e)
        {
            OnMeasuring(e);
        }

        private void HandleMeasurerMeasured(object sender, MeasuredEventArgs e)
        {
            OnMeasured(e);
        }

        #endregion
    }
}
