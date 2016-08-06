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

using System.Collections.Generic;
using DustInTheWind.Kinoko.Utils;

namespace DustInTheWind.Kinoko
{
    /// <summary>
    /// Contains the measurements taken by <see cref="KinokoContext"/> after it measures the subjects.
    /// </summary>
    public class KinokoResult
    {
        /// <summary>
        /// The time measurements of every run of the subject.
        /// </summary>
        private readonly List<double> measurements;

        /// <summary>
        /// Gets an array with the time measurements of every run of the subject.
        /// </summary>
        public double[] Measurements
        {
            get { return measurements.ToArray(); }
        }

        /// <summary>
        /// The average value in miliseconds of the time measurements.
        /// </summary>
        private double? average;

        /// <summary>
        /// Gets the average value in miliseconds of the time measurements.
        /// </summary>
        public double Average
        {
            get
            {
                if (!average.HasValue)
                    average = CalculateAverage();

                return average.Value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KinokoResult"/> class.
        /// </summary>
        public KinokoResult()
        {
            measurements = new List<double>();
            average = null;
        }

        /// <summary>
        /// Adds a new time measurement to the list of time measurements.
        /// </summary>
        /// <param name="time">The time measurement value.</param>
        public void AddMeasurement(double time)
        {
            measurements.Add(time);
            average = null;
        }

        /// <summary>
        /// Calculates the average value of the measurements.
        /// </summary>
        private double CalculateAverage()
        {
            return Math.Average(measurements);
        }
    }
}
