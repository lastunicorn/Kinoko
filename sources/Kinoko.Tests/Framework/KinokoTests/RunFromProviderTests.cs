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
using System.Threading;
using DustInTheWind.Kinoko.Providers;
using Moq;
using NUnit.Framework;

namespace DustInTheWind.Kinoko.Tests.Framework.KinokoTests
{
    /// <summary>
    /// Unit tests for the <see cref="Kinoko.Run(ITasksProvider, int)"/> method.
    /// </summary>
    [TestFixture]
    public class RunFromProviderTests
    {
        private DustInTheWind.Kinoko.Kinoko kinoko;
        private Mock<ITasksProvider> kinokoTasksProvider;

        [SetUp]
        public void SetUp()
        {
            kinoko = new DustInTheWind.Kinoko.Kinoko();
            kinokoTasksProvider = new Mock<ITasksProvider>();
        }

        #region Run(ITasksProvider tasksProvider, int repeatCount)

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void throws_if_tasksProvider_is_null()
        {
            try
            {
                kinoko.Run(null as ITasksProvider, 10);
            }
            catch (ArgumentNullException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("tasksProvider"));
                throw;
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void throws_if_repeatCount_is_zero()
        {
            try
            {
                int repeatCount = 0;

                kinoko.Run(kinokoTasksProvider.Object, repeatCount);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("repeatCount"));
                throw;
            }
        }

        [Test]
        public void returns_not_null_list_of_results()
        {
            int repeatMeasurementCount = 10;

            IList<KinokoResult> result = kinoko.Run(kinokoTasksProvider.Object, repeatMeasurementCount);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void retrieves_list_of_subjects_from_provider()
        {
            kinoko.Run(kinokoTasksProvider.Object, 10);

            kinokoTasksProvider.Verify(x => x.GetKinokoTasks(), Times.Once());
        }

        [Test]
        public void the_list_of_results_contains_correct_number_of_results()
        {
            KinokoTask[] tasks = CreateEmptyTasks(3);
            kinokoTasksProvider.Setup(x => x.GetKinokoTasks()).Returns(tasks);

            IList<KinokoResult> results = kinoko.Run(kinokoTasksProvider.Object, 10);

            Assert.That(results.Count, Is.EqualTo(3));
        }

        [Test]
        public void the_list_of_results_contains_not_null_values()
        {
            KinokoTask[] tasks = CreateEmptyTasks(3);
            kinokoTasksProvider.Setup(x => x.GetKinokoTasks()).Returns(tasks);

            IList<KinokoResult> results = kinoko.Run(kinokoTasksProvider.Object, 10);

            Assert.That(results, Is.All.Not.Null);
        }

        [Test]
        public void the_list_of_results_contains_correct_values()
        {
            KinokoTask[] tasks = CreateSleepTasks(new[] { 60, 80, 40 });
            kinokoTasksProvider.Setup(x => x.GetKinokoTasks()).Returns(tasks);

            IList<KinokoResult> results = kinoko.Run(kinokoTasksProvider.Object, 3);

            int[] expectedAverages = new[] { 60, 80, 40 };
            AssertEqualsAverages(results, expectedAverages);
        }

        #endregion

        #region TaskRunning Event

        [Test]
        public void raises_TaskRunning_event_once_for_one_subject()
        {
            int callCount = 0;
            KinokoTask[] tasks = CreateEmptyTasks(1);
            kinokoTasksProvider.Setup(x => x.GetKinokoTasks()).Returns(tasks);
            kinoko.TaskRunning += (sender, e) =>
            {
                callCount++;
            };

            kinoko.Run(kinokoTasksProvider.Object, 3);

            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void raises_TaskRunning_event_twice_for_two_subjects()
        {
            int callCount = 0;
            KinokoTask[] tasks = CreateEmptyTasks(2);
            kinokoTasksProvider.Setup(x => x.GetKinokoTasks()).Returns(tasks);
            kinoko.TaskRunning += (sender, e) =>
            {
                callCount++;
            };

            kinoko.Run(kinokoTasksProvider.Object, 3);

            Assert.That(callCount, Is.EqualTo(2));
        }

        #endregion

        #region TaskRun Event

        [Test]
        public void raises_TaskRun_event_once_for_one_subject()
        {
            int callCount = 0;
            KinokoTask[] tasks = CreateEmptyTasks(1);
            kinokoTasksProvider.Setup(x => x.GetKinokoTasks()).Returns(tasks);
            kinoko.TaskRun += (sender, e) =>
            {
                callCount++;
            };

            kinoko.Run(kinokoTasksProvider.Object, 3);

            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void raises_TaskRun_event_twice_for_two_subjects()
        {
            int callCount = 0;
            KinokoTask[] tasks = CreateEmptyTasks(2);
            kinokoTasksProvider.Setup(x => x.GetKinokoTasks()).Returns(tasks);
            kinoko.TaskRun += (sender, e) =>
            {
                callCount++;
            };

            kinoko.Run(kinokoTasksProvider.Object, 3);

            Assert.That(callCount, Is.EqualTo(2));
        }

        #endregion

        private KinokoTask[] CreateEmptyTasks(int count)
        {
            List<KinokoTask> tasks = new List<KinokoTask>();

            for (int i = 0; i < count; i++)
            {
                tasks.Add(new KinokoTask
                {
                    Subject = () => { }
                });
            }

            return tasks.ToArray();
        }

        private KinokoTask[] CreateSleepTasks(IEnumerable<int> times)
        {
            List<KinokoTask> tasks = new List<KinokoTask>();

            foreach (int time in times)
            {
                int t = time;
                tasks.Add(new KinokoTask
                {
                    Subject = () => Thread.Sleep(t)
                });
            }

            return tasks.ToArray();
        }

        private void AssertEqualsAverages(IList<KinokoResult> results, IList<int> expectedAverages)
        {
            Assert.That(results, Is.Not.Null);
            Assert.That(expectedAverages, Is.Not.Null);
            Assert.That(results.Count, Is.EqualTo(expectedAverages.Count));

            for (int i = 0; i < results.Count; i++)
            {
                Assert.That(results[i], Is.Not.Null);
                Assert.That(results[i].Average, Is.EqualTo(expectedAverages[i]).Within(1));
            }
        }
    }
}

