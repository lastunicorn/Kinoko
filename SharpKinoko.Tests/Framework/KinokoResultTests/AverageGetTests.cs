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
using NUnit.Framework;

namespace DustInTheWind.SharpKinoko.Tests.Framework.KinokoResultTests
{
    /// <summary>
    /// Unit tests for the <see cref="KinokoResult.Average/"/> "get" property.
    /// </summary>
    [TestFixture]
    public class AverageGetTests
    {
        private KinokoResult result;

        [SetUp]
        public void SetUp()
        {
            result = new KinokoResult();
        }

        [Test]
        public void Average_calculates_and_returns_the_average()
        {
            double[] measurements = new double[] { 142, 152, 57, 84 };
            AddMeasurementsToResult(measurements);

            double actual = result.Average;

            // (142 + 152 + 57 + 84) / 4 = 108.75
            Assert.That(actual, Is.EqualTo(108.75));
        }

        [Test]
        public void Average_calculates_and_returns_the_new_average_after_adding_some_more_values()
        {
            AddMeasurementsToResult(new double[] { 2, 4, 6, 8 });
            double actualOld = result.Average;
            AddMeasurementsToResult(new double[] { 10 });
            double actual = result.Average;

            // (2 + 4 + 6 + 8 + 10) / 5 = 6
            Assert.That(actual, Is.EqualTo(6));
        }

        private void AddMeasurementsToResult(IEnumerable<double> measurements)
        {
            foreach (double time in measurements)
            {
                result.AddMeasurement(time);
            }
        }
    }
}

