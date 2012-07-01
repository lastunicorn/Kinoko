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

namespace DustInTheWind.SharpKinoko.Tests.KinokoTests
{
    [TestFixture]
    public class TaskRunCountTests
    {
        private Kinoko kinoko;

        [SetUp]
        public void SetUp()
        {
            kinoko = new Kinoko();
        }

        [Test]
        public void TaskRunCount_is_initially_3()
        {
            Assert.That(kinoko.RepeatMeasurementCount, Is.EqualTo(3));
        }

        [Test]
        public void TaskRunCount_can_be_Set_when_not_running()
        {
            kinoko.RepeatMeasurementCount = 10;

            Assert.That(kinoko.RepeatMeasurementCount, Is.EqualTo(10));
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TaskRunCount_throws_if_set_to_negative_or_zero_values([Values(0, -1, -2, -10)]int taskRunCount)
        {
            kinoko.RepeatMeasurementCount = taskRunCount;
        }
    }
}

