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
    /// This class is used to measure a kinoko subject.
    /// </summary>
    public class Measurer
    {
        /// <summary>
        /// The subject that is tested by the current instance.
        /// </summary>
        private KinokoSubject subject;

        /// <summary>
        /// Gets the subject that is tested by the current instance.
        /// </summary>
        public KinokoSubject Subject
        {
            get { return subject; }
        }

        /// <summary>
        /// The number of times the measurements are performed. (To minimize the measurement errors.)
        /// </summary>
        private int repeatCount;

        /// <summary>
        /// Gets or sets the number of times the measurements are performed. The subject should be run multiple
        /// times to minimize the measurement errors.
        /// </summary>
        public int RepeatCount
        {
            get { return repeatCount; }
        }

        /// <summary>
        /// The results of the measurements. It is null if no measurement was performed yet.
        /// </summary>
        private KinokoResult result;

        /// <summary>
        /// Gets the results of the measurements. It is null if no measurement was performed yet.
        /// </summary>
        public KinokoResult Result
        {
            get { return result; }
        }

        #region Event Measuring

        /// <summary>
        /// Event raised before starting a measurement.
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
        /// Event raised after every measurement.
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
        /// Initializes a new instance of the <see cref="Measurer"/> class with
        /// the subject that is to be measured and
        /// the number of times the measurement should be performed.
        /// </summary>
        /// <param name="subject">The subject that is to be measured.</param>
        /// <param name="repeatCount">The number of times the measurement is performed.</param>
        /// <exception cref="ArgumentNullException">It is thrown if the subject is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the repeatCount value is less then 1.</exception>
        public Measurer(KinokoSubject subject, int repeatCount)
        {
            if (subject == null)
                throw new ArgumentNullException("subject");

            if (repeatCount < 1)
                throw new ArgumentOutOfRangeException("repeatCount", "The repeat count should be an integer greater then 0.");

            this.subject = subject;
            this.repeatCount = repeatCount;
        }

        #endregion

        #region Run

        /// <summary>
        /// Runs the subject multiple times and measures the time intervals spent.
        /// </summary>
        /// <remarks>
        /// After the measurement is finished, the <see cref="M:KinokoResult.Calculate"/> method is automatically called.
        /// </remarks>
        public void Run()
        {
            this.result = null;

            KinokoResult result = PerformMeasurements();
            result.CalculateAll();

            this.result = result;
        }

        /// <summary>
        /// Performs the measurements for the specified number of times.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="KinokoResult"/> containing the measurements.
        /// </returns>
        private KinokoResult PerformMeasurements()
        {
            KinokoResult result = new KinokoResult();

            for (int i = 0; i < repeatCount; i++)
            {
                double milliseconds = PerformMeasurementWithEvents(i);
                result.AddMeasurement(milliseconds);
            }

            return result;
        }

        /// <summary>
        /// Performs one measurement and also raises the <see cref="Measuring"/> and <see cref="Measured"/> events.
        /// </summary>
        /// <returns>
        /// The measured time in miliseconds.
        /// </returns>
        /// <param name="measurementIndex">The index representing the number of times the measurement is performing.</param>
        private double PerformMeasurementWithEvents(int measurementIndex)
        {
            OnMeasuring(new MeasuringEventArgs(measurementIndex));
            double milliseconds = PerformMeasurement();
            OnMeasured(new MeasuredEventArgs(measurementIndex, milliseconds));

            return milliseconds;
        }

        private double PerformMeasurement()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            subject();
            stopwatch.Stop();

            return stopwatch.Elapsed.TotalMilliseconds;
        }

        #endregion
    }

}

