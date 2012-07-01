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
using System.Diagnostics;

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
        /// <summary>
        /// The task that is tested by Kinoko.
        /// </summary>
        private KinokoTask task;

        /// <summary>
        /// Gets or sets the task that is tested by Kinoko.
        /// </summary>
        public KinokoTask Task
        {
            get { return task; }
            set { task = value; }
        }

        /// <summary>
        /// The number of times the measurements are performed. (To minimize the measurement errors.)
        /// </summary>
        private int repeatMeasurementCount;

        /// <summary>
        /// Gets or sets the number of times the measurements are performed. The tasks should be run multiple
        /// times to minimize the measurement errors.
        /// </summary>
        public int RepeatMeasurementCount
        {
            get { return repeatMeasurementCount; }
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException("value", "The task run count should be an integer greater then 0.");

                repeatMeasurementCount = value;
            }
        }

        /// <summary>
        /// The results of the test. It is null if no test was run.
        /// </summary>
        private KinokoResult result;

        /// <summary>
        /// Gets the results of the test. It is null if no test was run.
        /// </summary>
        public KinokoResult Result
        {
            get { return result; }
        }


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


        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Kinoko"/> class with
        /// default values.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Kinoko()
            : this(null as KinokoTask, 3)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Kinoko"/> class with
        /// the task that is to be tested and
        /// the number of times the test should be performed.
        /// </summary>
        /// <param name="task">The task that is to be tested.</param>
        /// <param name="repeatMeasurementCount">The number of times the measurements are performed.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Kinoko(KinokoTask task, int repeatMeasurementCount)
        {
            if (repeatMeasurementCount < 1)
                throw new ArgumentOutOfRangeException("taskRunCount", "The task run count should be an integer greater then 0.");

            this.task = task;
            this.repeatMeasurementCount = repeatMeasurementCount;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DustInTheWind.SharpKinoko.Kinoko"/> class.
        /// </summary>
        /// <param name='tasksProvider'>Provides a list of tasks to be run.</param>
        /// <param name="repeatMeasurementCount">The number of times the measurements are performed.</param>
        public Kinoko(ITasksProvider tasksProvider, int repeatMeasurementCount)
        {
            tasksProvider.GetTasks();
        }

        #endregion


        #region Run

        /// <summary>
        /// Runs the task multiple times and measures the time intervals spent.
        /// </summary>
        /// <remarks>
        /// After the test is finished, the <see cref="M:KinokoResult.Calculate"/> method is automatically called. 
        /// </remarks>
        public void Run()
        {
            if (task == null)
                throw new TaskNotSetException();

            this.result = null;

            KinokoResult result = PerformMeasurements();
            result.Calculate();

            this.result = result;
        }

        private KinokoResult PerformMeasurements()
        {
            KinokoResult result = new KinokoResult();

            for (int i = 0; i < repeatMeasurementCount; i++)
            {
                double milliseconds = PerformMeasurement(i);
                result.AddMeasurement(milliseconds);
            }

            return result;
        }

        private double PerformMeasurement(int measurementCount)
        {
            // Announce that the Task is about to be run.
            OnMeasuring(new MeasuringEventArgs(measurementCount));

            double milliseconds = Measure();

            // Announce that the Task was run.
            OnMeasured(new MeasuredEventArgs(measurementCount, milliseconds));

            return milliseconds;
        }


        private double Measure()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            task();
            stopwatch.Stop();

            return stopwatch.Elapsed.TotalMilliseconds;
        }

        #endregion
    }
}
