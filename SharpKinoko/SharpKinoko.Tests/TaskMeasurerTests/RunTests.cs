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

using System.Threading;
using NUnit.Framework;
using System.Collections;

namespace DustInTheWind.SharpKinoko.Tests.TaskMeasurerTests
{
    [TestFixture]
    public class RunTests
    {
        [Test]
        public void calls_the_task()
        {
            bool isCalled = false;
            KinokoTask task = () => isCalled = true;
            int repeatMeasurementCount = 10;
            TaskMeasurer taskMeasurer = new TaskMeasurer(task, repeatMeasurementCount);

            taskMeasurer.Run();

            Assert.That(isCalled, Is.True);
        }

        [Test]
        public void calls_the_task_multiple_times([Values(1, 2, 3, 4, 5, 10)]int n)
        {
            int calledCount = 0;
            KinokoTask task = () => calledCount++;
            TaskMeasurer taskMeasurer = new TaskMeasurer(task, n);

            taskMeasurer.Run();

            Assert.That(calledCount, Is.EqualTo(n));
        }

        [Test]
        public void creates_Result()
        {
            KinokoTask task = () => {};
            int repeatMeasurementCount = 10;
            TaskMeasurer taskMeasurer = new TaskMeasurer(task, repeatMeasurementCount);

            taskMeasurer.Run();

            Assert.That(taskMeasurer.Result, Is.Not.Null);
        }

        [Test]
        public void Result_contains_correct_number_of_measurements([Values(1, 2, 3, 4, 5, 10)]int n)
        {
            KinokoTask task = () => {};
            TaskMeasurer taskMeasurer = new TaskMeasurer(task, n);

            taskMeasurer.Run();

            Assert.That(taskMeasurer.Result.Measurements, Is.Not.Null);
            Assert.That(taskMeasurer.Result.Measurements.Length, Is.EqualTo(n));
        }

        [Test]
        public void Result_Measurements_contains_correct_values()
        {
            int callIndex = 0;
            double[] times = new double[] { 60, 80, 40 };
            KinokoTask task = () => Thread.Sleep((int)times[callIndex++]);
            TaskMeasurer taskMeasurer = new TaskMeasurer(task, times.Length);

            taskMeasurer.Run();

            AssertAreEqual(times, taskMeasurer.Result.Measurements);
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
