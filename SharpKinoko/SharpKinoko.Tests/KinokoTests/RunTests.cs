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
        [ExpectedException(typeof(TaskNotSetException))]
        public void TestRun_throws_if_no_task_was_provided()
        {
            kinoko.Run();
        }

        [Test]
        public void Run_calls_the_task()
        {
            bool isCalled = false;
            kinoko.Task = () => isCalled = true;

            kinoko.Run();

            Assert.That(isCalled, Is.True);
        }

        [Test]
        public void Run_calls_the_task_multiple_times([Values(1, 2, 3, 4, 5, 10)]int n)
        {
            int calledCount = 0;
            kinoko.Task = () => calledCount++;
            kinoko.RepeatMeasurementCount = n;

            kinoko.Run();

            Assert.That(calledCount, Is.EqualTo(n));
        }

        [Test]
        public void Run_creates_Result()
        {
            kinoko.Task = () => {};

            kinoko.Run();

            Assert.That(kinoko.Result, Is.Not.Null);
        }

        [Test]
        public void Result_contains_correct_number_of_measurements([Values(1, 2, 3, 4, 5, 10)]int n)
        {
            kinoko.Task = () => {};
            kinoko.RepeatMeasurementCount = n;

            kinoko.Run();

            Assert.That(kinoko.Result.Measurements, Is.Not.Null);
            Assert.That(kinoko.Result.Measurements.Length, Is.EqualTo(n));
        }

        [Test]
        public void Result_Measurements_contains_correct_values()
        {
            int callIndex = 0;
            double[] times = new double[] { 60, 80, 40 };

            KinokoTask task = new KinokoTask(delegate
            {
                Thread.Sleep((int)times[callIndex++]);
            });

            kinoko.Task = task;
            kinoko.RepeatMeasurementCount = times.Length;

            kinoko.Run();

            AssertAreEqual(times, kinoko.Result.Measurements);
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
