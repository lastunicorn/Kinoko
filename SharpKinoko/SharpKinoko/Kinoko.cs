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
using System.Diagnostics;

namespace DustInTheWind.SharpKinoko
{
    /// <summary>
    /// Runs a task and measures the time necessary to finish. The task is run multiple times and an
    /// average is calculated.
    /// The class is not thread safe.
    /// </summary>
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
        /// The number of times the task is run within the test. (To minimize the measurement errors.)
        /// </summary>
        private int taskRunCount;

        /// <summary>
        /// Gets or sets the number of times the task is run within the test. The task should be run multiple
        /// times to minimize the measurement errors.
        /// </summary>
        public int TaskRunCount
        {
            get { return taskRunCount; }
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException("value", "The task run count should be an integer greater then 0.");

                taskRunCount = value;
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

        #region Event BeforeTaskRun

        /// <summary>
        /// Event raised before every call of the task.
        /// </summary>
        public event EventHandler<BeforeTaskRunEventArgs> BeforeTaskRun;

        /// <summary>
        /// Raises the <see cref="BeforeTaskRun"/> event.
        /// </summary>
        /// <param name="e">An <see cref="BeforeTaskRunEventArgs"/> object that contains the event data.</param>
        protected virtual void OnBeforeTaskRun(BeforeTaskRunEventArgs e)
        {
            if (BeforeTaskRun != null)
            {
                BeforeTaskRun(this, e);
            }
        }

        #endregion

        #region Event AfterTaskRunEvent

        /// <summary>
        /// Event raised after every call of the task.
        /// </summary>
        public event EventHandler<AfterTaskRunEventArgs> AfterTaskRun;

        /// <summary>
        /// Raises the <see cref="AfterTaskRun"/> event.
        /// </summary>
        /// <param name="e">An <see cref="AfterTaskRunEventArgs"/> object that contains the event data.</param>
        protected virtual void OnAfterTaskRun(AfterTaskRunEventArgs e)
        {
            if (AfterTaskRun != null)
            {
                AfterTaskRun(this, e);
            }
        }

        #endregion


        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Kinoko"/> class.
        /// </summary>
        public Kinoko()
        {
            taskRunCount = 1;
        }

        #endregion


        #region Run

        /// <summary>
        /// Runs the task multiple times and measures the times.
        /// </summary>
        public void Run()
        {
            if (task == null)
                throw new TaskNotSetException();

            this.result = null;
            KinokoResult result = new KinokoResult();

            // Run the task multiple times.
            for (int i = 0; i < taskRunCount; i++)
            {
                // Announce that the Task is about to be run.
                OnBeforeTaskRun(new BeforeTaskRunEventArgs(i));

                // Run the Task.
                Stopwatch stopwatch = Stopwatch.StartNew();
                task();
                stopwatch.Stop();

                // Store the time in which the task run.
                double millis = stopwatch.Elapsed.TotalMilliseconds;
                result.AddValue(millis);

                // Announce that the Task was run.
                OnAfterTaskRun(new AfterTaskRunEventArgs(i, millis));
            }

            result.Calculate();
            this.result = result;
        }

        #endregion
    }
}