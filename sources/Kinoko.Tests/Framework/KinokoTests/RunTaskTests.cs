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

namespace DustInTheWind.Kinoko.Tests.Framework.KinokoTests
{
    /// <summary>
    /// Unit tests for the <see cref="KinokoContext.Run(KinokoTask, int)"/> method.
    /// </summary>
    [TestFixture]
    public class RunTaskTests
    {
        private KinokoContext kinokoContext;

        [SetUp]
        public void SetUp()
        {
            kinokoContext = new KinokoContext();
        }

        #region Run(KinokoSubject subject, int repeatCount)

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void throws_if_subject_is_null()
        {
            try
            {
                kinokoContext.Run(null as KinokoTask, 10);
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
                KinokoTask task = CreateEmptyTask();

                kinokoContext.Run(task, 0);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("repeatCount"));
                throw;
            }
        }

        [Test]
        public void returns_not_null_result()
        {
            KinokoTask task = CreateEmptyTask();

            KinokoResult result = kinokoContext.Run(task, 10);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Result_contains_correct_number_of_measurements([Values(1, 2, 3, 4, 5, 10)]int n)
        {
            KinokoTask task = CreateEmptyTask();

            KinokoResult result = kinokoContext.Run(task, n);

            Assert.That(result.Measurements, Is.Not.Null);
            Assert.That(result.Measurements.Length, Is.EqualTo(n));
        }

        [Test]
        public void Result_Measurements_contains_correct_values()
        {

            int[] timeIntervals = new int[] { 60, 80, 40 };
            KinokoTask task = CreateSleepTask(timeIntervals);

            KinokoResult result = kinokoContext.Run(task, timeIntervals.Length);

            AssertAreEqual(timeIntervals, result.Measurements);
        }

        [Test]
        public void the_result_contains_the_calculated_average()
        {
            int[] timeIntervals = new int[] { 60, 80, 40 };
            KinokoTask task = CreateSleepTask(timeIntervals);

            KinokoResult result = kinokoContext.Run(task, timeIntervals.Length);

            Assert.That(result.Average, Is.EqualTo(60).Within(1));
        }

        #endregion

        #region TaskRunning Event

        [Test]
        public void TaskRunning_event_is_raised_once()
        {
            int callCount = 0;
            KinokoTask task = CreateEmptyTask();
            kinokoContext.TaskRunning += (sender, e) =>
            {
                callCount++;
            };

            kinokoContext.Run(task, 3);

            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void TaskRunning_event_contains_the_subject()
        {
            KinokoTask actualTask = null;
            KinokoTask task = CreateEmptyTask();
            kinokoContext.TaskRunning += (sender, e) =>
            {
                actualTask = e.Task;
            };

            kinokoContext.Run(task, 3);

            Assert.That(actualTask, Is.SameAs(task));
        }

        #endregion

        #region TaskRun Event

        [Test]
        public void TaskRun_event_is_raised_once()
        {
            int callCount = 0;
            KinokoTask task = CreateEmptyTask();
            kinokoContext.TaskRun += (sender, e) =>
            {
                callCount++;
            };

            kinokoContext.Run(task, 3);

            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void TaskRun_event_contains_the_result()
        {
            TaskRunEventArgs eva = null;
            KinokoTask task = CreateEmptyTask();
            kinokoContext.TaskRun += (sender, e) =>
            {
                eva = e;
            };

            kinokoContext.Run(task, 3);

            Assert.That(eva.Result, Is.Not.Null);
        }

        #endregion

        #region Measuring Event

        [Test]
        public void Measuring_event_is_raised_3_times_if_repeatCount_is_3()
        {
            int callCount = 0;
            KinokoTask task = CreateEmptyTask();
            kinokoContext.Measuring += (sender, e) =>
            {
                callCount++;
            };

            kinokoContext.Run(task, 3);

            Assert.That(callCount, Is.EqualTo(3));
        }

        [Test]
        public void Measuring_event_is_raised_with_sender_Kinoko()
        {
            object senderObject = null;
            KinokoTask task = CreateEmptyTask();
            kinokoContext.Measuring += (sender, e) =>
            {
                senderObject = sender;
            };

            kinokoContext.Run(task, 1);

            Assert.That(senderObject, Is.SameAs(kinokoContext));
        }

        [Test]
        public void Measuring_event_is_raised_with_not_null_event_args()
        {
            MeasuringEventArgs eventArgs = null;
            KinokoTask task = CreateEmptyTask();
            kinokoContext.Measuring += (sender, e) =>
            {
                eventArgs = e;
            };

            kinokoContext.Run(task, 1);

            Assert.That(eventArgs, Is.Not.Null);
        }

        #endregion

        #region Measured Event

        [Test]
        public void Measured_event_is_raised_3_times_if_repeatCount_is_3()
        {
            int callCount = 0;
            KinokoTask task = CreateEmptyTask();
            kinokoContext.Measured += (sender, e) =>
            {
                callCount++;
            };

            kinokoContext.Run(task, 3);

            Assert.That(callCount, Is.EqualTo(3));
        }

        [Test]
        public void Measured_event_is_raised_with_sender_Kinoko()
        {
            object senderObject = null;
            KinokoTask task = CreateEmptyTask();
            kinokoContext.Measured += (sender, e) =>
            {
                senderObject = sender;
            };

            kinokoContext.Run(task, 1);

            Assert.That(senderObject, Is.SameAs(kinokoContext));
        }

        [Test]
        public void Measured_event_is_raised_with_not_null_event_args()
        {
            MeasuredEventArgs eventArgs = null;
            KinokoTask task = CreateEmptyTask();
            kinokoContext.Measured += (sender, e) =>
            {
                eventArgs = e;
            };

            kinokoContext.Run(task, 1);

            Assert.That(eventArgs, Is.Not.Null);
        }

        #endregion

        private KinokoTask CreateEmptyTask()
        {
            return new KinokoTask { Subject = () => { } };
        }

        private KinokoTask CreateSleepTask(int[] timeIntervals)
        {
            int callIndex = 0;
            return new KinokoTask
            {
                Subject = () => Thread.Sleep(callIndex < timeIntervals.Length ? timeIntervals[callIndex++] : 0)
            };
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

