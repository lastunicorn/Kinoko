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

using NUnit.Framework;

namespace DustInTheWind.SharpKinoko.Tests.Framework.KinokoResultTests
{
    [TestFixture]
    public class CalculateTests
    {
        private KinokoResult result;

        [SetUp]
        public void SetUp()
        {
            result = new KinokoResult();
        }

        [Test]
        public void Calculate_calculates_average()
        {
            double[] measurements = new double[] { 142, 152, 57, 84 };
            for (int i = 0; i < measurements.Length; i++)
            {
                result.AddMeasurement(measurements[i]);
            }

            result.CalculateAll();

            // (142 + 152 + 57 + 84) / 4 = 108.75
            Assert.That(result.Average, Is.EqualTo(108.75).Within(1));
        }
    }
}

