using System;
using System.Collections.Generic;
using System.Text;

namespace DustInTheWind.SharpKinoko
{
    /// <summary>
    /// Provides data for <see cref="AfterTaskRun"/> event.
    /// </summary>
    public class AfterTaskRunEventArgs : EventArgs
    {
        private int stepIndex;

        public int StepIndex
        {
            get { return stepIndex; }
        }

        private double time;

        public double Time
        {
            get { return time; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AfterTaskRunEventArgs"/> class.
        /// </summary>
        public AfterTaskRunEventArgs(int stepIndex, double time)
            : base()
        {
            this.stepIndex = stepIndex;
            this.time = time;
        }
    }
}
