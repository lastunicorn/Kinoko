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

//using Rhino.Mocks;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;
using Rhino.Mocks;

namespace DustInTheWind.SharpKinoko.Tests
{
    [TestFixture]
    public class KinokoRunTests
    {
        private MockRepository mocks;
        private Kinoko kinoko;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            kinoko = new Kinoko();
        }

        [Test]
        public void Test()
        {
            KinokoTask task = mocks.StrictMock<KinokoTask>();

            kinoko.Task = task;

            using (mocks.Record())
            {
                task.Invoke();
                LastCall.Repeat.Twice();
            }

            using (mocks.Playback())
            {
                kinoko.Run();
            }
        }

        [Test]
        [ExpectedException(typeof(TaskNotSetException))]
        public void TestRun_NoTask()
        {
            kinoko.Run();
        }

        [Test]
        public void TestRun_TaskRun1()
        {
            int callCount = 0;
            KinokoTask task = new KinokoTask(delegate
            {
                callCount++;
            });

            kinoko.Task = task;

            kinoko.Run();

            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void TestRun_TaskRunN([Values(1, 2, 3, 4, 5, 10)]int n)
        {
            int callCount = 0;
            KinokoTask task = new KinokoTask(delegate
            {
                callCount++;
            });

            kinoko.Task = task;
            kinoko.TaskRunCount = n;

            kinoko.Run();

            Assert.That(callCount, Is.EqualTo(n));
        }


        [Test]
        public void TestRun_TaskRun_ResultNotNull()
        {
            KinokoTask task = new KinokoTask(delegate { });

            kinoko.Task = task;

            kinoko.Run();

            Assert.That(kinoko.Result, Is.Not.Null);
        }

        [Test]
        public void TestRun_TaskRun_ResultN([Values(1, 2, 3, 4, 5, 10)]int n)
        {
            int callCount = 0;
            KinokoTask task = new KinokoTask(delegate
            {
                callCount++;
            });

            kinoko.Task = task;
            kinoko.TaskRunCount = n;

            kinoko.Run();

            Assert.That(kinoko.Result.Times.Length, Is.EqualTo(n));
        }

        [Test]
        public void TestRun_TaskRun_ResultTimes()
        {
            int callIndex = 0;
            double[] times = new double[] { 100, 150, 50 };
            KinokoTask task = new KinokoTask(delegate
            {
                Thread.Sleep((int)times[callIndex++]);
            });

            kinoko.Task = task;
            kinoko.TaskRunCount = 3;

            kinoko.Run();

            //Assert.That(kinoko.Result.Times, Is.All.EqualTo(times).Within(1));

            Assert.That(kinoko.Result.Times, Is.Not.Null);
            Assert.That(kinoko.Result.Times.Length, Is.EqualTo(times.Length));
            for (int i = 0; i < times.Length; i++)
            {
                Assert.That(kinoko.Result.Times[i], Is.EqualTo(times[i]).Within(1));
            }
        }

        [Test]
        public void TestRun_TaskRun_ResultCalculateAverage()
        {
            int callIndex = 0;
            double[] times = new double[] { 100, 150, 50 };
            KinokoTask task = new KinokoTask(delegate
            {
                Thread.Sleep((int)times[callIndex++]);
            });

            kinoko.Task = task;
            kinoko.TaskRunCount = 3;

            kinoko.Run();

            Assert.That(kinoko.Result.Average, Is.EqualTo(100).Within(2));
        }
    }
}
