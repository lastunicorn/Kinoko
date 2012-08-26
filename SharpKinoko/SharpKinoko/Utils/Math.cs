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

namespace DustInTheWind.SharpKinoko.Utils
{
    internal class Math
    {
        public static double Average(double[] values)
        {
            if (values == null)
                throw new ArgumentNullException("values");

            if (values.Length == 0)
                return 0;

            double sum = 0;

            for (int i = 0; i < values.Length; i++)
            {
                sum += values [i];
            }

            return sum / (double)values.Length;
        }

        public static double Average(IList<double> values)
        {
            if (values == null)
                throw new ArgumentNullException("values");

            if (values.Count == 0)
                return 0;

            double sum = 0;

            for (int i = 0; i < values.Count; i++)
            {
                sum += values [i];
            }

            return sum / (double)values.Count;
        }

        public static double Average(IEnumerable<double> values)
        {
            if (values == null)
                throw new ArgumentNullException("values");

            double sum = 0;
            int count = 0;

            foreach (double value in values)
            {
                sum += value;
                count++;
            }

            if (count == 0)
                return 0;

            return sum / (double)count;
        }
    }
}
