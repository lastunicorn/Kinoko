﻿// SharpKinoko
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

using System.Collections;
using System.Threading;
using NUnit.Framework;

namespace DustInTheWind.Kinoko.Tests.Framework.MeasurerTests
{
    /// <summary>
    /// Unit tests for the <see cref="Measurer.Run"/> method.
    /// </summary>
    [TestFixture]
    public class RunTests
    {
        [Test]
        public void calls_the_subject()
        {
            bool isCalled = false;
            KinokoSubject subject = () => isCalled = true;
            int repeatMeasurementCount = 10;
            Measurer measurer = new Measurer(subject, repeatMeasurementCount);

            measurer.Run();

            Assert.That(isCalled, Is.True);
        }

        [Test]
        public void calls_the_subject_multiple_times([Values(1, 2, 3, 4, 5, 10)]int n)
        {
            int calledCount = 0;
            KinokoSubject subject = () => calledCount++;
            Measurer measurer = new Measurer(subject, n);

            measurer.Run();

            Assert.That(calledCount, Is.EqualTo(n));
        }

        [Test]
        public void returns_not_null_Result()
        {
            KinokoSubject subject = () => { };
            int repeatMeasurementCount = 10;
            Measurer measurer = new Measurer(subject, repeatMeasurementCount);

            KinokoResult result = measurer.Run();

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Result_contains_correct_number_of_measurements([Values(1, 2, 3, 4, 5, 10)]int n)
        {
            KinokoSubject subject = () => { };
            Measurer measurer = new Measurer(subject, n);

            KinokoResult result = measurer.Run();

            Assert.That(result.Measurements, Is.Not.Null);
            Assert.That(result.Measurements.Length, Is.EqualTo(n));
        }

        [Test]
        public void Result_Measurements_contains_correct_values()
        {
            int callIndex = 0;
            double[] times = new double[] { 60, 80, 40 };
            KinokoSubject subject = () => Thread.Sleep((int)times[callIndex++]);
            Measurer measurer = new Measurer(subject, times.Length);

            KinokoResult result = measurer.Run();

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
