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

namespace DustInTheWind.SharpKinoko.Tests
{
    [TestFixture]
    public class KinokoEventTests
    {
        private Kinoko kinoko;

        [SetUp]
        public void SetUp()
        {
            kinoko = new Kinoko();
        }

        [Test]
        public void TestConstructor1()
        {
        }

        [Test]
        public void TestInitial_Task()
        {
            Assert.That(kinoko.Task, Is.Null);
        }

        [Test]
        public void TestInitial_TaskRunCount()
        {
            Assert.That(kinoko.TaskRunCount, Is.EqualTo(1));
        }

        [Test]
        public void TestInitial_Result()
        {
            Assert.That(kinoko.Result, Is.Null);
        }

        [Test]
        public void TestTask()
        {
            KinokoTask task = new KinokoTask(delegate { });
            kinoko.Task = task;

            Assert.That(kinoko.Task, Is.SameAs(task));
        }

        [Test]
        public void TestTaskRunCount1()
        {
            kinoko.TaskRunCount = 10;

            Assert.That(kinoko.TaskRunCount, Is.EqualTo(10));
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestTaskRunCount2([Values(0, -1, -2, -10)]int taskRunCount)
        {
            kinoko.TaskRunCount = taskRunCount;
        }
    }
}
