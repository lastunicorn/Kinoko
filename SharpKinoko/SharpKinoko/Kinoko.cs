// Kinoko
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
    public class Kinoko
    {
        private KinokoTask task;

        public KinokoTask Task
        {
            get { return task; }
            set { task = value; }
        }

        private int taskRunCount;

        public int TaskRunCount
        {
            get { return taskRunCount; }
            set { taskRunCount = value; }
        }

        private KinokoResult result;

        public KinokoResult Result
        {
            get { return result; }
        }

        #region Event BeforeTaskRun

        /// <summary>
        /// Event raised when ... Well, is raised when it should be raised. Ok?
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
        /// Event raised when ... Well, is raised when it should be raised. Ok?
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


        public Kinoko()
        {

        }

        public void Run()
        {
            if (task == null)
                throw new Exception("No task was set to be tested.");

            KinokoResult result = new KinokoResult();

            // The Task is run multiple times and then an average is calculated.

            for (int i = 0; i < taskRunCount; i++)
            {
                // Announce that the Task is about to be run.
                OnBeforeTaskRun(new BeforeTaskRunEventArgs(i));

                // Run the Task.
                Stopwatch stopwatch = Stopwatch.StartNew();
                task();
                stopwatch.Stop();

                // Store the time in which the task run.
                double milis = stopwatch.Elapsed.TotalMilliseconds;
                result.AddValue(milis);

                // Announce that the Task was run.
                OnAfterTaskRun(new AfterTaskRunEventArgs(i, milis));
            }

            result.Calculate();
            this.result = result;
        }
    }
}
