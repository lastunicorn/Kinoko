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

namespace DustInTheWind.SharpKinoko
{
    /// <summary>
    /// Runs a task and measures the time necessary to finish. The task is run multiple times and an
    /// average is calculated.
    /// The class is not thread safe.
    /// </summary>
    /// <remarks>
    /// Every time the <see cref="M:Run"/> method is called, the old result is discarded.
    /// </remarks>
    public class Kinoko
    {
        #region Event Measuring

        /// <summary>
        /// Event raised before every call of the task.
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
        /// Event raised after every call of the task.
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
        /// Event raised before every call of the task.
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
        /// Event raised before every call of the task.
        /// </summary>
        public event EventHandler<EventArgs> TaskRun;

        /// <summary>
        /// Raises the <see cref="TaskRun"/> event.
        /// </summary>
        /// <param name="e">An <see cref="TaskRunEventArgs"/> object that contains the event data.</param>
        protected virtual void OnTaskRun(EventArgs e)
        {
            if (TaskRun != null)
            {
                TaskRun(this, e);
            }
        }

        #endregion


        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Kinoko"/> class with
        /// default values.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Kinoko()
        {
        }

        #endregion


        #region Run

        /// <summary>
        /// Runs the task multiple times and measures the time intervals spent.
        /// </summary>
        /// <remarks>
        /// After the test is finished, the <see cref="M:KinokoResult.Calculate"/> method is automatically called.
        /// </remarks>
        public KinokoResult Run(KinokoTask task, int repeatMeasurementCount)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            if (repeatMeasurementCount < 1)
                throw new ArgumentOutOfRangeException("repeatMeasurementCount", "The task run count should be an integer greater then 0.");

            return RunTaskWithEvents(task, repeatMeasurementCount);
        }

        public IList<KinokoResult> Run(ITasksProvider tasksProvider, int repeatMeasurementCount)
        {
            if (tasksProvider == null)
                throw new ArgumentNullException("tasksProvider");

            if (repeatMeasurementCount < 1)
                throw new ArgumentOutOfRangeException("repeatMeasurementCount", "The task run count should be an integer greater then 0.");

            IEnumerable<KinokoTask> tasks = tasksProvider.GetTasks();
            List<KinokoResult> results = new List<KinokoResult>();

            foreach (KinokoTask task in tasks)
            {
                results.Add(RunTaskWithEvents(task, repeatMeasurementCount));
            }

            return results;
        }

        private KinokoResult RunTaskWithEvents(KinokoTask task, int repeatMeasurementCount)
        {
            OnTaskRunning(new TaskRunningEventArgs(task));
            KinokoResult result = RunTask(task, repeatMeasurementCount);
            OnTaskRun(EventArgs.Empty);

            return result;
        }

        private KinokoResult RunTask(KinokoTask task, int repeatMeasurementCount)
        {
            TaskMeasurer measurer = new TaskMeasurer(task, repeatMeasurementCount);
         
            try
               {
                measurer.Measuring += HandleMeasurerMeasuring;
                measurer.Measured += HandleMeasurerMeasured;
         
                measurer.Run();
            }
            finally
               {
                measurer.Measuring -= HandleMeasurerMeasuring;
                measurer.Measured -= HandleMeasurerMeasured;
            }
         
            measurer.Result.Calculate();
         
            return measurer.Result;
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
