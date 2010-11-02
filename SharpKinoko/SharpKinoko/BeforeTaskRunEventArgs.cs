using System;
using System.Collections.Generic;
using System.Text;

namespace DustInTheWind.SharpKinoko
{
    /// <summary>
    /// Provides data for <see cref="BeforeTaskRun"/> event.
    /// </summary>
    public class BeforeTaskRunEventArgs : EventArgs
    {
        private int stepIndex;

        public int StepIndex
        {
            get { return stepIndex; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BeforeTaskRunEventArgs"/> class.
        /// </summary>
        public BeforeTaskRunEventArgs(int stepIndex)
            : base()
        {
            this.stepIndex = stepIndex;
        }
    }
}
