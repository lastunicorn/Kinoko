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
using NUnit.Framework;
using System.Threading;
using System.Collections;

namespace DustInTheWind.SharpKinoko.Tests.KinokoTests
{
    [TestFixture]
    public class RunTests
    {
        private Kinoko kinoko;

  [SetUp]
        public void SetUp()
        {
            kinoko = new Kinoko();
        }


        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void throws_if_task_is_null()
        {
            try
            {
                kinoko.Run(null, 10);
            }
            catch (ArgumentNullException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("task"));
                throw;
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void throws_if_repeatMeasurementCount_is_zero()
        {
            try
            {
                KinokoTask task = () => { };
                int repeatMeasurementCount = 0;

                kinoko.Run(task, repeatMeasurementCount);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("repeatMeasurementCount"));
                throw;
            }
        }

        [Test]
        public void returns_not_null_result()
        {
            KinokoTask task = () => {};
            int repeatMeasurementCount = 10;

            KinokoResult result = kinoko.Run(task, repeatMeasurementCount);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Result_contains_correct_number_of_measurements([Values(1, 2, 3, 4, 5, 10)]int n)
        {
            KinokoTask task = () => {};

            KinokoResult result = kinoko.Run(task, n);

            Assert.That(result.Measurements, Is.Not.Null);
            Assert.That(result.Measurements.Length, Is.EqualTo(n));
        }

        [Test]
        public void Result_Measurements_contains_correct_values()
        {
            int callIndex = 0;
            double[] times = new double[] { 60, 80, 40 };
            KinokoTask task = () => Thread.Sleep((int)times[callIndex++]);

            KinokoResult result = kinoko.Run(task, times.Length);

            AssertAreEqual(times, result.Measurements);
        }

        private void AssertAreEqual(IList expected, IList actual)
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Count, Is.EqualTo(expected.Count));
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.That(actual[i], Is.EqualTo(expected[i]).Within(1));
            }
        }
    }
}

