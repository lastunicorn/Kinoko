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

namespace DustInTheWind.SharpKinoko.Tests.KinokoResultTests
{
    [TestFixture]
    public class AddMeasurementsTests
    {
        private KinokoResult result;

        [SetUp]
        public void SetUp()
        {
            result = new KinokoResult();
        }

        [Test]
        public void AddMeasurement_adds_a_nu_alue_to_the_list()
        {
            result.AddMeasurement(7);

            Assert.That(result.Measurements, Contains.Item(7));
        }

        [Test]
        public void AddMeasurement_adds_two_values_to_the_list()
        {
            double[] expectedMeasurements = new double[] { 7,10 };

            result.AddMeasurement(7);
            result.AddMeasurement(10);

            Assert.That(result.Measurements, Is.EqualTo(expectedMeasurements));
        }
    }
}

