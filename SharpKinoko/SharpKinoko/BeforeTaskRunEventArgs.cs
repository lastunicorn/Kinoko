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

        //public override bool Equals(object obj)
        //{
        //    if (obj is BeforeTaskRunEventArgs)
        //    {
        //        BeforeTaskRunEventArgs eva = (BeforeTaskRunEventArgs)obj;

        //        return eva.stepIndex == stepIndex;
        //    }
        //    else
        //    {
        //        throw new ArgumentException("obj is not a BeforeTaskRunEventArgs object.");
        //    }
        //}
    }
}
