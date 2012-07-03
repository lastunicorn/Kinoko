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
using System.Collections;
using System.Threading;
using NUnit.Framework;

namespace DustInTheWind.SharpKinoko.Tests.KinokoTests
{
    [TestFixture]
    public class RunTaskTests
    {
        private Kinoko kinoko;

        [SetUp]
        public void SetUp()
        {
            kinoko = new Kinoko();
        }

        #region Run(KinokoSubject subject, int repeatCount)

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void throws_if_subject_is_null()
        {
            try
            {
                kinoko.Run(null as KinokoSubject, 10);
            }
            catch (ArgumentNullException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("subject"));
                throw;
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void throws_if_repeatMeasurementCount_is_zero()
        {
            try
            {
                KinokoSubject subject = CreateEmptyTask();

                kinoko.Run(subject, 0);
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
            KinokoSubject subject = CreateEmptyTask();

            KinokoResult result = kinoko.Run(subject, 10);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Result_contains_correct_number_of_measurements([Values(1, 2, 3, 4, 5, 10)]int n)
        {
            KinokoSubject subject = CreateEmptyTask();

            KinokoResult result = kinoko.Run(subject, n);

            Assert.That(result.Measurements, Is.Not.Null);
            Assert.That(result.Measurements.Length, Is.EqualTo(n));
        }

        [Test]
        public void Result_Measurements_contains_correct_values()
        {

            int[] timeIntervals = new int[] { 60, 80, 40 };
            KinokoSubject subject = CreateSleepTask(timeIntervals);

            KinokoResult result = kinoko.Run(subject, timeIntervals.Length);

            AssertAreEqual(timeIntervals, result.Measurements);
        }

        [Test]
        public void the_result_contains_the_calculated_average()
        {
            int[] timeIntervals = new int[] { 60, 80, 40 };
            KinokoSubject subject = CreateSleepTask(timeIntervals);

            KinokoResult result = kinoko.Run(subject, timeIntervals.Length);

            Assert.That(result.Average, Is.EqualTo(60).Within(1));
        }

        #endregion

        #region TaskRunning/TaskRun Events

        [Test]
        public void raises_TaskRunning_event_once()
        {
            int callCount = 0;
            KinokoSubject subject = CreateEmptyTask();
            kinoko.TaskRunning += (sender, e) => {
                callCount++;
            };

            kinoko.Run(subject, 3);

            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void the_TaskRunning_event_contains_the_subject()
        {
            KinokoSubject actualSubject = null;
            KinokoSubject subject = CreateEmptyTask();
            kinoko.TaskRunning += (sender, e) => {
                actualSubject = e.Subject;
            };

            kinoko.Run(subject, 3);

            Assert.That(actualSubject, Is.SameAs(subject));
        }

        [Test]
        public void raises_TaskRun_event_once()
        {
            int callCount = 0;
            KinokoSubject subject = () => { };
            kinoko.TaskRun += (sender, e) => {
                callCount++;
            };

            kinoko.Run(subject, 3);

            Assert.That(callCount, Is.EqualTo(1));
        }

        #endregion

        private KinokoSubject CreateEmptyTask()
        {
            return () => { };
        }

        private KinokoSubject CreateSleepTask(int[] timeIntervals)
        {
            int callIndex = 0;
            return () => Thread.Sleep(callIndex < timeIntervals.Length ? timeIntervals[callIndex++] : 0);
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

